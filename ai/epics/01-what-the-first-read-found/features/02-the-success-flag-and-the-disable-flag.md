# 02: The Success Flag And The Disable Flag

Epic 01. Size: small. Changes: `source\Roy.Logging\Roy.Logging\`.

## Status

Not started. Waits for 01.

## Blocked By

Nothing.

## Depends On

01.

## Goal

`ProcessMessageDTO.Succeed` is true when every sink finished without an error.
`Settings.Exception.Disable` disables exception logging, and `Settings.Log.Disable` disables
logs, each on its own.

## What Happens Today

- `InternalProcessMessage.Succeed => this.Errors.HasElements()` and the same line in
  `ProcessMessageDTO`: true when there are errors.
- `ExceptionExtension.SaveAsync(exception, level, message, identity, setting, frame,
  webApplicationHttpContext, listOfParameters)` tests `setting.Log.Disable.IsNotTrue()`.
  `Exception.Disable` is never read anywhere.

## Scope

- `Succeed => !this.Errors.HasElements()` in both classes. The XML comment already says "Flag
  that indicates whether the process succeeded"; it stays.
- `setting.Exception.Disable` in `ExceptionExtension`. `LoggerExtension` already tests
  `setting.Log.Disable` and is not touched.

## Not In This Feature

- Anything about how errors are collected (feature 03 changes when they arrive, not where).

## Tests

- A clean save to a temp folder: `Succeed` true, `Errors` empty.
- Feature 01's unwritable folder: `Succeed` false, one error.
- `Exception.Disable = true`: `SaveAsync` writes no file and returns `Succeed` true (nothing
  was asked, nothing failed). `Log.Disable = true` with `Exception.Disable = false`: `SaveAsync`
  still writes the file; `LogAsync` writes none.

## As Built

Built 2026-10-27: `8ffba0b` (the three library lines) and `eca4707` (the log tests). The
exception tests travelled in feature 04's commit, `2f4b483`, because `ExceptionTest.cs`
already carried 04's tests by then.

- **Before** (the old library with the new tests): 6 of 13 fail, every `Succeed` assertion.
- **After:** 13 of 13.
