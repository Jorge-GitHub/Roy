# Epic 01: What The First Read Found

## Status

Written 2026-10-27 from a read of the whole codebase on `dev/1.0.19` (`22fb062`, clean).
Built the same day on `feature/first-read-fixes` from `dev/1.0.19`, in George's checkout: one
commit per feature, `6428949` to `a755cb5`, and one for warnings, `fe1b7a1`. Not merged, not
pushed. George's "fix the bugs you found" started it; where he had not answered a question,
its recommended answer was taken (*Decisions Made*). Two questions are still his, and the rest
of what is left is in *What Is Left*.

Final numbers, a clean rebuild of the solution: 211 warnings (225 at the baseline), 114
distinct file and line (131), none in a file that did not have that warning before; 0 errors.
`Roy.UT`: 27 of 27 in the default run, offline, in 5 s. Live: 2 pass, 3 email tests fail on
the expired ethereal account. The builds refreshed the four Roy files in
`C:\Repos\Avalon.Libraries\Avalon\OpenSource\`, uncommitted there, as always.

## Goal

Every sink does what its settings say, every `...Async` call is one, a caller can trust the
result it gets back, an exception record says where the exception was thrown, and the package
says what it is. The public API stays: no public type, member or namespace is renamed in this
epic.

## Where This Came From

George, 2026-10-27: "read the code. do not code yet", then "create an ai folder and write down
the epic to fix these issues". The issues are the ones that read turned up. Each is in *What
There Is Today* with the file it lives in, and each belongs to one feature.

## What There Is Today

Read 2026-10-27. Solution `source\Roy.Logging\Roy.Logging.sln`, four projects, all `net10.0`:
`Roy.Logging` (the library), `Roy.Logging.MVC` (the ASP.NET Core hook), `Roy.UT` (12 MSTest
tests), `Roy.Mvc.UT` (a Razor Pages app for trying things by hand, no test framework). The only
reference outside NuGet is `Avalon.Base.Extensions.dll`, read from
`C:\Repos\Avalon.Libraries\Avalon\Base\`; the build publishes `Roy.Logging.dll` and
`Roy.Logging.MVC.dll` into `C:\Repos\Avalon.Libraries\Avalon\OpenSource\`
(`source\Roy.Logging\Directory.Build.props`). The last build George recorded (Avalon.Libraries
epic 02, feature 02, 2026-10-07): 209 warnings, 0 errors, 12 of 12 passed. Paths below are
under `source\Roy.Logging\`.

### Wrong answers (feature 02)

- `InternalProcessMessage.Succeed` and `ProcessMessageDTO.Succeed` are
  `this.Errors.HasElements()`: true when something failed, false when all went well
  (`Roy.Logging\Domain\Communication\InternalProcessMessage.cs`,
  `Roy.Logging\Domain\DTO\Communication\ProcessMessageDTO.cs`).
- The `ExceptionExtension.SaveAsync` overload that does the work tests `setting.Log.Disable`, so
  `Settings.Exception.Disable` does nothing, and `Settings.Log.Disable` silences exceptions
  (`Roy.Logging\ExceptionExtension.cs`). `LoggerExtension` tests the right one.

### Work that may not finish (feature 03)

- `APIUtility.Post` calls `client.PostAsJsonAsync(...)` and does not await it, then `finally`
  disposes the `HttpClient`. Whether the request leaves the machine depends on timing, and any
  failure is lost (`Roy.Logging\Aspect\API\APIUtility.cs`).
- Every public `SaveAsync` and `LogAsync` is `async` with no `await` on any sink: the whole
  pipeline runs on the caller's thread before the `Task` comes back. `EmailService.SendAsync` is
  `async void` with no await inside; `SystemEventLogService.LogAsync` is a plain `void`. The
  callers in `Roy.Logging.MVC` (`ErrorService.LogError`, `FileRequestMiddleware.InvokeAsync`) and
  the tests never await, and work today only because nothing is asynchronous.
  `Roy.UT\ExceptionTest.TestFileNameException` sleeps ten seconds "to be sure the file is
  there": the author expects the call to be asynchronous.

### A setting that is read but not honoured (feature 04)

- `DatabaseSetting.LevelsToSave` is documented ("If null or empty, it will save all the issues")
  and never read: `DatabaseService.Save` has no level check, where `APIService.Post` and
  `EmailService.SendAsync` have one (`Roy.Logging\Helpers\DatabaseService.cs`).
- `IssueSetting()` initialises `Emails`, `APIs` and the two level lists but not `Databases`;
  `DatabaseSetting` and `APISetting` have no constructor, so `LevelsToSave` and `LevelsToPost`
  start null. `HasElements()` tolerates null, so nothing breaks today.
- `DatabaseLogic.ExecuteQuery` calls `connection.Dispose()` and then `connection.Close()`, and
  never disposes the `SqlCommand` (`Roy.Logging\Aspect\Database\Helpers\DatabaseLogic.cs`).

### The MVC handler (feature 05)

- `MvcExtensions.UseRoyExceptionHandler(app, builder, ...)` binds the `RoyLogging` section twice
  (`LoadRoySettings`, then `Get<RoySetting>()` again) and carries a note in Spanish: "creo que
  aqui se me durmio. es por eso que no esta logueando errores" (I think I fell asleep here;
  that is why it is not logging errors) (`Roy.Logging.MVC\Extensions\MvcExtensions.cs`).
- The likely reason for the note is not in that method. `UseRoyExceptionHandler` registers
  `app.UseExceptionHandler(...)` whose handler only logs. ASP.NET Core runs the exception handler
  nearest the throw. `Roy.Mvc.UT\Program.cs` calls Roy's handler first and then, outside
  Development, `app.UseExceptionHandler("/Error")`: that second handler is nearer the pages,
  catches the exception, re-executes `/Error`, and Roy's handler never sees it. In Development
  the developer exception page is outermost and Roy's handler is nearest, so it works. Any app
  with its own error page registered after Roy's call logs nothing in production. This is a
  reading of the middleware order, not something run; feature 05 proves it before it fixes it.
- `FileRequestMiddleware` logs every response with status 404, whatever the path: an API route
  that answers 404 on purpose is logged as a missing file
  (`Roy.Logging.MVC\Middleware\FileRequestMiddleware.cs`).

### What a record and an email carry (feature 06)

- `ExceptionDetail.SetExceptionTrace` sets `StackTrace` and `ExceptionTrace` only when the level
  is `Trace` or `Debug`. At the default level, `Error`, the record has the exception's message
  and nothing of where it was thrown. The `Level` enum's comments say the full exception is
  meant for those two levels; nothing says the stack trace string is. The test output in
  `Roy.UT\bin\Debug\net10.0\exceptions\` shows an `Emergency` record with no stack trace
  (`Roy.Logging\Domain\Attributes\ExceptionDetail.cs`).
- `EmailExtensions.SetDefaultValues` fills `setting.DefaultEmailSubject` only when it is empty,
  and the text it fills in already has this issue's Id and level substituted. The setting is
  shared, so from the second email on, every subject from that `EmailSetting` carries the first
  issue's Id and level. A subject George sets himself with `*|IssueId|*` in it is never
  substituted at all (`Roy.Logging\Extensions\EmailExtensions.cs`).
- `MessageDetailDTOExtensions.ToDTO(this Method)` leaves `MethodDTO.Parameters` null, though
  `ToDTO(this List<Parameter>)` exists for it, so an API never receives the parameters
  (`Roy.Logging\Extensions\DTO\MessageDetailDTOExtensions.cs`).

### Found on the way (feature 07)

- `LabelDecorator.ReplaceMachineInformationLabels` replaces `OperativeSystemTag` twice.
- Unused: the field `EmailSetting._Language`; `using static
  System.Runtime.InteropServices.JavaScript.JSType;` in `MessageDetailDTOExtensions.cs`; `using
  System.Numerics;` in `Domain\Program\WebApplication.cs`.
- `Domain\Settings\Attributes\LogSetting.cs` holds the class `InformationSetting`.
- `Roy.Logging.MVC.csproj` references `Microsoft.AspNetCore.Authentication.JwtBearer`,
  `Microsoft.IdentityModel.Tokens`, `Microsoft.AspNetCore.Http.Abstractions` 2.3.10,
  `Microsoft.Extensions.Hosting.Abstractions` and
  `Microsoft.Extensions.DependencyInjection.Abstractions`; no source file uses them.
  `Roy.UT.csproj` and `Roy.Mvc.UT.csproj` reference `Azure.Identity`,
  `BouncyCastle.Cryptography`, `MimeKit` and `Microsoft.Data.SqlClient` directly though both
  have a project reference to `Roy.Logging`. Whether the build needs any of them is a build
  question, answered one reference at a time.
- `source\libraries\Avalon\Avalon.Base.Extension.dll` (the old singular name, built 2024) is
  still in the repository; nothing has referenced it since `22fb062`.

### The package and the README (feature 08)

- `Roy.Logging.csproj`: `Version` 1.0.0.9 on branch `dev/1.0.19`; `Copyright` 2023;
  `PackageLicenseExpression` MIT, while `LICENSE` is GPLv3 and the README badge says GPLv3.
  `Roy.Logging.MVC.csproj` has no package metadata at all.
- `README.md` tells users to reference `Avalon.Base.Extension.dll`; since `22fb062` the build
  reads `Avalon.Base.Extensions.dll`. Its MVC section does not say where `UseRoyExceptionHandler`
  goes relative to the app's own error handling (feature 05 makes that matter).
- `Roy.Mvc.UT\appsettings.json` holds a SQL Server connection string for `AVALONDEV`, user
  `madona`, with a password, and `ethereal.email` accounts with theirs. All of them are test
  values (decision 4), like the README's sample JSON; the ethereal ones are the fake SMTP
  service the tests use on purpose (`UTHelper.GetEmailSetting`: "This is a fake SMTP that is
  why I am hard coding this fake password"). Nothing to do.

### The tests (feature 01)

- `Roy.UT`: 12 tests. Most assert nothing; they pass when nothing throws, and the pipeline
  catches everything, so nothing throws. They share the static `LogExtension.Settings`:
  `TestEmailExceptionNotSendingEmailToLevel` and `TestAPIException` set `SaveLogOnFile = false`
  and never restore it, the email tests append to `Exception.Emails` and never remove, so what
  a test does depends on which ran before it. Four of them talk to `smtp.ethereal.email` or
  public APIs; one writes to the Windows Application event log; one sleeps ten seconds.

## Decisions Made (George, 2026-10-27)

4. **The credentials in `Roy.Mvc.UT\appsettings.json` are fake** ("those password and ids are
   fake. nothing to worry."). The `AVALONDEV` connection string and the ethereal accounts are
   test values. Nothing to rotate, nothing to move out of the file; it stays as it is, and
   feature 08 no longer touches it.

Taken as recommended on George's "fix the bugs you found" (2026-10-27), since he had not
answered them and each had a recommendation he could read: 1 (async for real), 2 (the stack
trace at every level), 5 (only paths with an extension), 6 (the rethrowing middleware), 7 (the
proof in `Roy.UT`) and 9 (`1.0.19`). For 8, a feature branch in George's checkout and no
worktree: the tree was clean and nothing else was in flight. 11: started.

3, answered (2026-10-27): "is ok, ignore this". The license stays as it is, MIT in the csproj
files and GPLv3 in the `LICENSE` file and the README badge.

10, answered (2026-10-27): "yes keep it. this is an open source project. a person should be
able to download it and compile it." The folder stays. The DLL in it was the old singular
`Avalon.Base.Extension.dll`, which the code no longer compiles against, so the current
`Avalon.Base.Extensions.dll` was added under `source\libraries\Avalon\Base\`, and
`Directory.Build.props` falls back to that folder when Avalon.Libraries is absent. Proven with
the Avalon.Libraries path pointed at nothing: the solution builds, 27 of 27 pass, nothing is
published. With Avalon.Libraries present, nothing changes. The README says how to build from
source. The `.pdb` is left out, as `.gitignore` ignores every `.pdb` and the old one was never
committed either.

Also on 2026-10-27, outside the epic: the `System.Security.Cryptography.Pkcs` 10.0.0
vulnerability (through MailKit and Microsoft.Data.SqlClient) is cleared by a direct reference
to 10.0.14 in `Roy.Logging` (`70bd789`); the build is down to 195 warnings. The ethereal
account and the by-hand run of `Roy.Mvc.UT`: "ignore is ok".

## Waiting For George

Answered or taken, 2026-10-27, as *Decisions Made* says. Open: 3 and 10. The questions stay
as they were asked.

1. **Async for real?** Feature 03 makes the pipeline asynchronous end to end: every sink is
   awaited, `SaveAsync` and `LogAsync` return when the sinks are done, and the MVC callers await
   them. The other way is to keep it synchronous and leave the names. Recommended: for real; the
   names, the README and the ten-second sleep all say that is what was meant.
2. **The stack trace at every level?** Feature 06 keeps `StackTrace` (the string) on every
   exception record and keeps the full `Exception` object (`ExceptionTrace`) behind `Trace` and
   `Debug` as today. The other way is to leave both gated. Recommended: the string always; it is
   the one field an error log is for.
3. **The license.** MIT (the csproj) or GPLv3 (the `LICENSE` file and the README badge). Not a
   choice a session makes. Feature 08 makes the three agree once George says which.
4. Answered: *Decisions Made*.
5. **Which 404s are missing files?** Every 404 (today), or only requests whose path has a file
   extension. Recommended: only paths with an extension, so an API's own 404s are not logged as
   files.
6. **The handler design.** Feature 05 replaces `app.UseExceptionHandler(...)` with a middleware
   that catches, logs through Roy, and rethrows the same exception, so the app's own error page
   still runs; and the README says the call goes after the app's own `UseExceptionHandler`,
   before `app.Run()`. Confirm, or say to keep `UseExceptionHandler` and only document the
   order (then an app's own error page and Roy cannot both work).
7. **Where the MVC proof lives.** Feature 05 needs a test that stands up a pipeline with an
   error page and an endpoint that throws. Recommended: in `Roy.UT`, with a project reference
   to `Roy.Logging.MVC` and `Microsoft.AspNetCore.TestHost`. The other way is a new test project.
8. **The branch.** A worktree at `source\worktree` on `feature/first-read-fixes` from
   `dev/1.0.19`, one commit per feature, George merges, as Avalon.Libraries' epic 02 did here;
   or straight on `dev/1.0.19`.
9. **The version after the epic.** `1.0.19`, to match the branch, in both csproj files.
10. **The legacy folder.** Delete `source\libraries\Avalon\` (nothing reads it), or keep it.
11. **Start.** Approve the epic and start with feature 01.

## What Is Left (2026-10-27)

- **George:** merge `feature/first-read-fixes` into `dev/1.0.19` and push; that was the goal.
- **The Avalon fork,** `Avalon.Service.Logging`, copies the code after the merge.
- **For the record,** found and fixed on the way: the full exception at `Trace` and `Debug` had
  never serialized for a thrown exception (feature 06); `Level.MissingFile` was never used
  (feature 05); the clone of the repository did not build without Avalon.Libraries (decision
  10). Set aside by George: the expired ethereal account behind the three Live email failures,
  and the by-hand run of `Roy.Mvc.UT`.

## Features

- `features/01-tests-that-can-prove-a-fix.md`: `Roy.UT` runs green in any order, offline, in
  seconds, and each later feature adds a test that fails before its fix and passes after.
- `features/02-the-success-flag-and-the-disable-flag.md`: `Succeed` means succeeded;
  `Exception.Disable` disables exceptions.
- `features/03-async-means-async.md`: every sink is awaited; the API post finishes; the public
  calls return when the work is done.
- `features/04-the-database-sink-honours-its-settings.md`: `LevelsToSave` is read; the lists
  start empty, not null; connection and command are disposed once, in order.
- `features/05-the-mvc-handler-and-the-apps-own-error-page.md`: Roy logs and the app's error
  page still shows; settings bind once; only file-looking 404s are logged.
- `features/06-what-a-record-and-an-email-carry.md`: the stack trace at every level; each
  email's subject has its own Id; the DTO carries the parameters.
- `features/07-found-on-the-way.md`: the duplicate replace, the unused usings and field, the
  file name, the package references nothing uses, the legacy DLL folder.
- `features/08-the-package-says-what-it-is.md`: version, copyright, license, MVC metadata, the
  README.

## Order

01 first: every other feature adds its test there. 02 and 04 next, small and independent. 03
before 05: the MVC callers await what 03 makes awaitable. 06 and 07 in any order after 03. 08
last: it bumps the version. Each feature is approved, built, tested and merged before the next.

## How Each Feature Is Done

1. Read this epic and `C:\Repos\AGENTS.md`: `this.` for own members, `base.` for inherited ones,
   methods public then protected then private.
2. **Before:** build the solution and run `Roy.UT` in George's checkout; record warnings,
   errors and test results. The build publishes into
   `C:\Repos\Avalon.Libraries\Avalon\OpenSource\`; `git status` there shows what changed, and
   nothing there is committed (that stays George's).
3. **Change:** only lines that trace to the feature. No public type, member or namespace is
   renamed; a public signature changes only where the feature says so (03's `EmailService`).
   Match the style around the change. Keep the BOM and CRLF the file has.
4. **Test:** a test in `Roy.UT` that fails before the change and passes after, with no network,
   no sleep and no shared state between tests (feature 01's rules).
5. **After:** build and test. The same warnings or fewer, never more; any difference in the
   count is explained in *As Built*. `Roy.UT` green.
6. One commit per feature on the branch of decision 8. George merges and says when to push.
7. *As Built* in the feature file, with the before and after numbers.

## Dependencies

- `Avalon.Base.Extensions.dll` in `C:\Repos\Avalon.Libraries\Avalon\Base\` (build of
  2026-10-24), read through `Directory.Build.props`. Nothing in that repository changes.
- .NET SDK 10.0.301 (installed).

## What "Done" Means For This Epic

- Each item in *What There Is Today* is fixed, or George has said it stays, and its feature's
  *As Built* says which.
- `Roy.UT` is green, offline, in any order, with a test for each fixed behaviour.
- An app with its own error page and `UseRoyExceptionHandler` gets both: the page, and an
  exception record, in Development and in Production (feature 05's test).
- `SaveAsync` and `LogAsync` return when every enabled sink has finished or failed, and the
  `ProcessMessageDTO` says which.
- Every public type, member and namespace that existed before exists after, with the same
  signature, except `EmailService.SendAsync` (decision 1).
- The two csproj files, the `LICENSE` file and the README agree on version and license.

## Out Of Scope

- Renaming `Roy.Logging.Domain.Contants` (the typo is in a public namespace that holds `Level`)
  or any other public name. A later major version, if George wants it.
- The 209 warnings as a whole: most are nullable annotations across the domain classes. Only
  the warnings on lines a feature touches.
- `Method.LoadParametersInformation`: it serialises `ParameterInfo` (the method's signature),
  not the call's argument values, which a `StackFrame` does not have. A limitation, not a bug.
- A shared `HttpClient` for the API sink, parallel sinks, cancellation tokens: none asked for.
- New sinks, new settings, the website under `docs\`, the GitHub wiki, releases and NuGet
  publishing, pushing.
- `AI\PROJECT_CONTEXT.md` and `AI\SESSION_HANDOFF.md` for this repository: George's call.
