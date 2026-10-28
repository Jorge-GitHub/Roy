# 01: Tests That Can Prove A Fix

Epic 01. Size: small. Changes: `source\Roy.Logging\Roy.UT\`.

## Status

Not started. Waits for decisions 8 and 11.

## Blocked By

Nothing.

## Depends On

Nothing. Every other feature depends on this one.

## Goal

`Roy.UT` runs green in any order, offline, in seconds, and a feature can add a test that fails
before its fix and passes after, with an assertion that says what was checked.

## What Happens Today

12 tests in `ExceptionTest` and `LogTest`. They share the static `LogExtension.Settings` and
leave it changed: `SaveLogOnFile = false` is set by two tests and never restored,
`Exception.Emails` grows by one per email test. Four tests send to `smtp.ethereal.email` or post
to public APIs, one writes to the Windows Application event log, one sleeps ten seconds. Only
two tests assert anything (that a file exists). Files land in `bin\Debug\net10.0\exceptions\`
and `logs\` and stay there between runs.

## Scope

- A `[TestInitialize]` in each test class sets `LogExtension.Settings = new RoySetting()` and
  points `Exception.FolderLocation` and `Log.FolderLocation` at a folder made for that test
  (`Path.Combine(Path.GetTempPath(), "Roy.UT", Guid)`); a `[TestCleanup]` deletes it. No test
  reads or writes `bin\`.
- `UTHelper` gains the two things every later test needs: the path of the one file a test
  wrote, and that file parsed as JSON (`System.Text.Json`) so a test can assert on `Id`,
  `Level`, `StackTrace` and the rest.
- The two existing file tests assert on the file's content, not only that it exists. The
  ten-second sleep goes (with feature 03 the call returns when the file is written; until
  then the pipeline is synchronous, so the file is there on return either way).
- The tests that need the network or the event log get `[TestCategory("Live")]`. They stay
  runnable; the default run is `dotnet test --filter TestCategory!=Live`, written at the top
  of `ExceptionTest.cs` and in the epic's *How Each Feature Is Done*.
- A test that proves the error path is observable offline: `Exception.FolderLocation` set to
  a path that cannot be created (a file's path used as a folder), `SaveAsync` returns a
  `ProcessMessageDTO` with one error. It is the test feature 02 turns into a `Succeed`
  assertion.

## Not In This Feature

- Any change under `Roy.Logging` or `Roy.Logging.MVC`.
- A test host for the MVC project (feature 05, decision 7).
- Replacing MSTest or adding a mocking library.

## Tests

The suite itself: `dotnet test --filter TestCategory!=Live` green; then each test run alone,
green; then the suite twice in a row, green, with no file left in `bin\`. The `Live` tests run
once by hand and reported in *As Built*.

## As Built

Not built.
