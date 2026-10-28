# Avalon.Service.Logging

**To:** Avalon.Service.Logging, `C:\Repos\Service\Logging` (origin
`AvalonTeamRepo/Avalon.Service.Logging`, branch `dev/1.0.1`).
**From:** the Roy session, `C:\Repos\OpenSource\Roy`, 2026-10-27. George sends this and pastes
the answer back.

## The goal

George wants Avalon.Service.Logging built from Roy.Logging, the open-source library: the same
architecture and the same idea. One extension method call saves an exception or logs any
object to files, the Windows or Linux event log, email, SQL Server and HTTP APIs; an ASP.NET
Core middleware logs every unhandled exception; a product turns it on with a few lines in
`Program.cs`. Because this library is Avalon's own, it uses the Avalon libraries below the
Service tier for what Roy hand-rolled, the database above all, and drops that boilerplate.
Nexus, Beat, Vibevana and the product Roy read `Roy.Logging.dll` from
`C:\Repos\Avalon.Libraries\Avalon\OpenSource\` today; this library is what they will read
instead.

## What is already decided

George, 2026-10-27, word for word:

- "this new repo Avalon.Service.Logging would be pretty much a copy of roy logger."
- "Because avalon.service.logging is for our own use we should use all the other libraries
  available. i believe below service is only framework and base. That means we should point to
  them and use the libraries for saving the info on the database and remove any boiler plate
  code. but the architecture and idea would be the same. enable logging by just adding a few
  lines on the program.cs"
- The name is `Avalon.Service.Logging`, in the Service tier: "Logging", not "Telemetry" and not
  "ErrorHandler".
- **Names in the code:** "use avalon not roy. remove anything roy related." No `Roy` anywhere:
  not in a type (`RoySetting`, `RoyExceptionMiddleware`), a method (`UseRoyExceptionHandler`),
  the configuration section (`RoyLogging`), a resource file (`RoyValues.resx`, the
  `RoyLoginSubject` key), a string (`"ROY Logger"`, the `Roy` syslog identity, the email
  footer), a table, a file name or a comment. Avalon names instead, for example
  `UseAvalonLogging`, an `AvalonLogging` section, `LoggingSetting`.
- **Tables:** "same select the default names removing roy." Roy's `RoyException` and `RoyLog`
  become `Exception` and `Log`, in whatever database the connection string names, as today.
  Whether they sit in a schema of their own is this repository's `sql-rules.md` to say.
- **`Avalon.Service.ErrorHandler`:** "ignore that library do not care about." Nothing in it is
  read, retired or touched.
- **Products:** "we will use it with nexus but this is not something this agent should worry.
  the goal is to create the library not replace it in another projects." The library is the
  whole job; switching Nexus or any product to it is someone else's, later.

Also settled, for the record:

- What this library may reference: Avalon.Framework.*, Avalon.Base.*, and its Service
  siblings (Templates, Communication, Monitor, …): `ai\platform\avalon-layers.md` in this repo
  says a layer may use the layers below it and the other libraries in its own layer. Never
  Aspect, Adapter, Connector, Foundation or a product. Layer (Application, Repository) sits
  below Service in that document, so the rule would allow it, but no Service library uses it
  today: every one of them reads Framework and Base only, and Layer's base classes serve the
  products' own application services and repositories, in the tiers above. George's words,
  "below service is only framework and base", are the practice; this library follows it, and
  touches Layer only on his word. George also said on 2026-10-27 that AI sits below Service
  (DocumentSearch uses Avalon.Ai.Llm), which `avalon-layers.md` (AI at 4) does not say yet;
  Logging does not need AI, so only the document is affected.
- Roy stays the public project. Nothing in Roy changes from this repository's work. If the
  port finds a bug in logic Roy shares, write it up for George; he decides whether it goes
  back to Roy.
- Roy's credentials in its test files (`ethereal.email`, the `AVALONDEV` connection string)
  are test values, not secrets.

## What we already do

The code to copy is in `C:\Repos\OpenSource\Roy`, branch `dev/1.0.19`, at `a361ea2` or later,
pushed to GitHub on 2026-10-27. It holds the bug fixes of that day. (An earlier version of this
message named a branch `feature/first-read-fixes`; it was merged into `dev/1.0.19` and deleted.)
Take it from there, not from `main`, which is far behind. Read first
`ai\epics\01-what-the-first-read-found\README.md` and its eight feature files: they describe
the architecture, every bug fixed with the proof, and the traps below. Roy's README shows the
calls and the `appsettings.json` shape.

Roy's shape, in short:

- **Public API:** `exception.SaveAsync(…)` (`ExceptionExtension`) and `anything.LogAsync(…)`
  (`LoggerExtension`), with overloads for a level, a message, an identity, the caller's
  `StackFrame`, a settings object and a list of parameters. Both return `ProcessMessageDTO`
  (`Succeed`, `Errors`, one per sink that failed).
- **Settings:** the static `LogExtension.Settings`, a `RoySetting` with `Exception` and `Log`
  halves, each an `IssueSetting`: folder, file name, append, disable, level filters, `Emails`,
  `APIs`, `Databases`, and which of method, machine and application details a record carries.
  The MVC package binds it from the `RoyLogging` section of `appsettings.json`.
- **Pipeline:** `ExceptionDetail` or `LogDetail` (a `MessageDetail`: id, date, level, message,
  machine, application, web request, calling method) goes to `RecordService.SaveAsync`, which
  awaits five sinks in turn, each in its own try/catch, errors collected: `FileService` (one
  JSON file per record), `SystemEventLogService` (Windows `EventLog`, Linux `syslog` by
  P/Invoke), `EmailService` (MailKit SMTP; HTML templates in `RoyValues.resx`, labels in
  English, Spanish, French and German, tags replaced by `LabelDecorator` and
  `MessageDecorator`), `APIService` (`HttpClient`, posts `ExceptionDTO` or `LogDTO` as JSON) and
  `DatabaseService` (`Microsoft.Data.SqlClient`; `QueryBuilder`, `ParametersBuilder` with 36
  hand-listed `SqlParameter`s, `Queries.cs` with two INSERT templates, `Record`,
  `ExceptionRecord` and `LogRecord` mapping columns; tables `RoyException` and `RoyLog`, scripts
  in `source\database\schema\`).
- **MVC:** `UseRoyExceptionHandler(app[, builder][, logMissingFiles])` registers
  `RoyExceptionMiddleware` (catch, log, throw again, so the application's own error page still
  runs) and, when asked, `FileRequestMiddleware` (a 404 for a path with an extension is saved at
  `Level.MissingFile`). It goes after the application's own `UseExceptionHandler` and before
  `app.Run()`. One line in `Program.cs`.
- **Dependencies:** `Avalon.Base.Extensions` (`ToJson`, `IsNotNull`, `HasElements`,
  `LimitLength`, `Copy`, …), MailKit 4.17.0, Microsoft.Data.SqlClient 7.0.1,
  Microsoft.Extensions.Configuration and Logging.EventLog 10.0.8, and
  `System.Security.Cryptography.Pkcs` pinned at 10.0.14 (10.0.0, which MailKit and SqlClient
  bring, has two advisories).
- **Build:** `source\Roy.Logging\Directory.Build.props` imports
  `C:\Repos\Avalon.Libraries\Avalon.Libraries.props`; HintPaths are
  `$(AvalonLibraries)Base/Avalon.Base.Extensions.dll`; `<AvalonLibraryFolder>` makes each build
  publish the DLL into Avalon.Libraries. Roy also falls back to `source\Libraries\Avalon\` so
  an open-source clone builds; this library does not need that.
- **Tests:** `Roy.UT`, 27 MSTest tests, offline, each with fresh settings and its own temp
  folder, the network and event-log ones tagged `Live` and left out of the default run; the
  middleware proven on `Microsoft.AspNetCore.TestHost`, including the two registration orders.

## What we need

The library, in this shape, in this repository:

1. **Projects under `src\`:** the library, its ASP.NET Core package, and their tests, named by
   this repository's conventions; `net10.0`; namespace `Avalon.Service.Logging`;
   `<AvalonLibraryFolder>Service</AvalonLibraryFolder>` so the build publishes into
   `C:\Repos\Avalon.Libraries\Avalon\Service\`. The README in this repository was drafted from
   the Roy session before the repository existed: its source path and its `source\` folder are
   wrong (`src\` here) and its "depends only on Avalon.Base.Extensions" will stop being true.
2. **The same public idea:** the two extension methods, the settings object bound from
   `appsettings.json`, the five sinks, the middleware, the few lines in `Program.cs`.
3. **Avalon libraries instead of Roy's own code, wherever one exists.** The catalog in
   `ai\specs\architecture\library-catalog.md` is empty, so read each library's source under
   `C:\Repos\Base\`, `C:\Repos\Framework\`, `C:\Repos\Layer\` and `C:\Repos\Service\`:
   - **Database** (George's example): `Avalon.Framework.DataAccess`, with
     `Avalon.Framework.Domain` entities, the way this repository's `clean-architecture.md` and
     `coding-rules.md` say (a repository runs a configured query by
     its ID and maps rows; a row is a DTO mapped to an entity at the boundary). This replaces
     `DatabaseLogic`, `QueryBuilder`, `ParametersBuilder`, `Queries.cs`, `Record`,
     `ExceptionRecord` and `LogRecord`, about 600 lines. The two tables become entities.
   - **Email:** `Avalon.Base.Web.Utility` sends email (its `SendEmail` test logs into a live
     SMTP server, per Avalon.Libraries epic 02, feature 03). If it covers SMTP with
     authentication and an HTML body, it replaces `EmailUtility` and the MailKit reference.
     `Avalon.Service.Templates`, a sibling, may serve the templating that Roy does with
     `Decorator`, `LabelDecorator`, `MessageDecorator` and the resx files; if not, those stay.
   - **HTTP post:** look in `Avalon.Framework.Web` for a client helper; otherwise Roy's twenty
     lines of `HttpClient` stay.
   - **Settings:** look for a settings convention in Framework or Layer; otherwise Roy's
     section binding stays.
   - **JSON and helpers:** `Avalon.Base.Extensions`, as Roy already uses. Keep Roy's
     `ExceptionJsonConverter` unless Avalon has one: System.Text.Json cannot serialize a thrown
     exception (`TargetSite`), and without it no sink can write the full exception.
   - **File and event log:** nothing in Avalon does these; Roy's code stays.
4. **Roy's traps, fixed on 2026-10-27, not to come back:** every sink awaited with
   `ConfigureAwait(false)` (the API post used to be dropped); the middleware throws the
   exception again and is documented as going after the application's handler (the old
   `UseExceptionHandler` version swallowed it, so Roy and an error page could never both work);
   `Succeed` means succeeded; `Exception.Disable` and `Log.Disable` each disable their own half;
   the email subject is built per message, never stored on the setting; every settings list
   starts empty for the configuration binder; the stack trace is kept at every level and the
   full exception only at Trace and Debug.
5. **Tests like Roy's,** and this repository's `testing-rules.md` for the rest.

## Questions

George answered the names, the tables, ErrorHandler and the products on 2026-10-27 (*What is
already decided*). Two remain:

1. Do all five places a record is written stay: the file, the event log, the email, the HTTP
   API and the database? Recommended: yes, "the architecture and idea would be the same".
2. Do the four email languages (English, Spanish, French, German) stay? Recommended: yes.

## What to send back

When the library builds and publishes into `Avalon.Libraries\Avalon\Service\`: the commit or
version to pick up, and the lines a product adds to `Program.cs` and to `appsettings.json`.
Until then, nothing waits on Roy's side.

## The answer
