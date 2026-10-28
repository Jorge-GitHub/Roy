# 03: Async Means Async

Epic 01. Size: medium. Changes: `source\Roy.Logging\Roy.Logging\` (`ExceptionExtension`,
`LoggerExtension`, `Services\RecordService`, `Helpers\*`, `Aspect\API\APIUtility`,
`Aspect\Email\EmailUtility`, `Aspect\Database\DatabaseUtility` and
`Aspect\Database\Helpers\DatabaseLogic`).

## Status

Not started. Waits for decision 1 and for 01.

## Blocked By

Decision 1.

## Depends On

01. Feature 05 depends on this one.

## Goal

`SaveAsync` and `LogAsync` return a `Task` that completes when every enabled sink has finished
or failed. The API post is sent and its failure, if any, is in `Errors`. No `async void`, no
`async` method without an `await`.

## What Happens Today

See the epic's *Work that may not finish*. The chain is `ExceptionExtension.SaveAsync` or
`LoggerExtension.LogAsync` (public, `async`, no await on the work) to `RecordService.Save`
(sync) to the five `Helpers` (sync, except `EmailService.SendAsync`, `async void` with no await)
to the `Aspect` utilities (sync; `APIUtility.Post` starts a `Task` and drops it).

## Scope

- `APIUtility.Post` becomes `PostAsync` returning `Task`: `await client.PostAsJsonAsync(...)`
  inside a `using` for the client, then `response.EnsureSuccessStatusCode()`, so a 4xx or 5xx
  from the API is an error in `Errors` like any other sink failure. `APIService.Post` becomes
  `PostAsync` and awaits each API in turn.
- `EmailUtility.Send` becomes `SendAsync` on MailKit's `ConnectAsync`, `AuthenticateAsync`,
  `SendAsync` and `DisconnectAsync`. `EmailService.SendAsync` becomes `async Task` and awaits
  each email in turn. This is the one public signature change in the epic (`EmailService` is
  public).
- `DatabaseLogic.ExecuteQuery` becomes `ExecuteQueryAsync` on `OpenAsync` and
  `ExecuteNonQueryAsync`; `DatabaseUtility.Save` and `DatabaseService.Save` become `SaveAsync`.
- `FileService.Save` becomes `SaveAsync` on `File.AppendAllTextAsync` and `WriteAllTextAsync`.
- `SystemEventLogService.LogAsync` becomes `Log`: `EventLog` and `syslog` have no asynchronous
  form, and the name should not promise one. Internal, so free to rename.
- `RecordService.Save` becomes `SaveAsync`, awaiting each sink in the order it runs them today
  (file, event system, email, API, database), each still in its own try/catch, so `Errors`
  keeps its order and one failure still does not stop the next sink.
- The two public overloads that do the work await `RecordService.SaveAsync`. The thin overloads
  already `return await` the full one and are not touched.
- Every `await` inside `Roy.Logging` takes `.ConfigureAwait(false)`: it is a library, and a
  caller on a UI or request thread should not be made to wait for that thread.

## Not In This Feature

- A shared `HttpClient`, parallel sinks, cancellation tokens, retries.
- The callers in `Roy.Logging.MVC` (feature 05 awaits them).
- The tests' own `await`s beyond what the new tests need (the old tests still compile: a
  dropped `Task` is a warning, not an error, and 01 already made them assert on what returns).

## Tests

- `await new Exception("x").SaveAsync()`, then the file exists and parses, with no sleep. The
  same for `LogAsync`.
- `Exception.APIs` with `URL = "http://localhost:1/"` (nothing listens; the connection is
  refused at once): `Errors` has one `HttpRequestException`. Before this feature the task is
  dropped and `Errors` is empty, so this test fails before and passes after.
- A `Live` test keeps the ethereal email path, awaited.
- The build: the CS1998 warnings ("async method lacks await") on these methods are gone; the
  count in *As Built*.

## As Built

Not built.
