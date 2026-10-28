# 05: The MVC Handler And The App's Own Error Page

Epic 01. Size: medium. Changes: `source\Roy.Logging\Roy.Logging.MVC\`, `Roy.Mvc.UT\Program.cs`,
`Roy.UT\` (the proof), `README.md` (the MVC section).

## Status

Not started. Waits for decisions 5, 6 and 7, and for 03.

## Blocked By

Decisions 5, 6, 7.

## Depends On

01, 03.

## Goal

An app that calls `UseRoyExceptionHandler` gets an exception record for every unhandled
exception, in Development and in Production, whether or not it has its own error page, and its
own error page still shows. Settings bind once. The Spanish note has nothing left to say and
goes. A 404 is logged as a missing file only when the request looks like one for a file.

## What Happens Today

See the epic's *The MVC handler*. The diagnosis there is from reading the middleware order and
is proved first (*Tests*, the first test, written before the change and expected to fail).

## Scope

- A `RoyExceptionMiddleware` in `Roy.Logging.MVC\Middleware\`: `try { await next(context); }`,
  `catch (Exception ex) { await ex.SaveAsync(context details); ExceptionDispatchInfo.Capture(ex).Throw(); }`.
  Roy sees every exception that passes through it and changes nothing about what happens next:
  an outer error page handles it, or Kestrel answers 500 as it would have. `ErrorService`
  takes the exception and the context instead of reading `IExceptionHandlerFeature`, which only
  exists inside `UseExceptionHandler`.
- `UseRoyExceptionHandler(app, logMissingFiles)` registers that middleware instead of
  `app.UseExceptionHandler(...)`. The `(app, settings, ...)` overload is unchanged. The
  `(app, builder, ...)` overload calls `builder.LoadRoySettings()` once and passes nothing else
  (today it binds twice and the second binding is what gets used). The note goes.
- `FileRequestMiddleware.InvokeAsync` awaits `SaveAsync`, and (decision 5) logs only when
  `Path.HasExtension(context.Request.Path)`.
- `Roy.Mvc.UT\Program.cs`: `app.UseRoyExceptionHandler(builder, true)` moves after the app's
  own `UseExceptionHandler("/Error")` block, since Roy must be nearer the endpoints than the
  app's page to see the exception before the page handles it. `Index.cshtml.cs` keeps its
  `throw`.
- `README.md`, the MVC section: the call goes after the app's own error handling and before
  `app.Run()`, and why, in two sentences.

## Not In This Feature

- `IExceptionHandler` and `AddExceptionHandler` (the .NET 8 way): they need
  `builder.Services`, and `UseRoyExceptionHandler` runs after `Build()`, when services are
  frozen. A `builder.Services.AddRoyExceptionHandler()` would be a new public API; not asked
  for.
- `WebApplicationHttpContextExtensions` (what gets captured from the request).

## Tests

In `Roy.UT`, with a project reference to `Roy.Logging.MVC` and `Microsoft.AspNetCore.TestHost`
(decision 7). Each builds a `WebApplication` on `TestServer` with `Exception.FolderLocation` at
the test's temp folder:

1. **The proof:** `app.UseRoyExceptionHandler()`, then
   `app.UseExceptionHandler(a => a.Run(c => c.Response.WriteAsync("error page")))`, then an
   endpoint that throws, in that order (the order `Roy.Mvc.UT` has today). `GET` it: the body
   is "error page" and the exceptions folder is empty. This test documents the bug and is kept,
   renamed, as the case the README warns about.
2. **The fix:** the app's handler first, then `UseRoyExceptionHandler()`, then the endpoint: the
   body is "error page" and the folder has one record whose `ExceptionMessage` is the thrown
   one. Fails before (Roy's `UseExceptionHandler` swallows; no page), passes after.
3. **Roy alone:** `UseRoyExceptionHandler()` and the endpoint, no app handler: status 500 and
   one record, as today.
4. **404s:** with `logMissingFiles: true`, `GET /missing.css` writes a record and
   `GET /api/nothing` does not.

By hand, in *As Built*: `Roy.Mvc.UT` run with `ASPNETCORE_ENVIRONMENT=Production` and
`Development`, `GET /` once each, the `/Error` page shown in Production, one record in
`exceptionsUT\` each time.

## As Built

Not built.
