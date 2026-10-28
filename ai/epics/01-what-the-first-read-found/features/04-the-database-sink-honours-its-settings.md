# 04: The Database Sink Honours Its Settings

Epic 01. Size: small. Changes: `source\Roy.Logging\Roy.Logging\` (`Helpers\DatabaseService`,
`Aspect\Database\Helpers\DatabaseLogic`, `Domain\Settings\Attributes\IssueSetting`,
`Domain\Settings\Database\DatabaseSetting`, `Domain\Settings\Web\APIAspect\APISetting`).

## Status

Not started. Waits for 01.

## Blocked By

Nothing.

## Depends On

01. Independent of 03; if 03 lands first, the method here is the `SaveAsync` it made.

## Goal

A `DatabaseSetting` with `LevelsToSave` set saves only those levels, as the API and email
settings already do. The level lists and the `Databases` list start empty. The connection and
the command are disposed once, in order.

## What Happens Today

See the epic's *A setting that is read but not honoured*.

## Scope

- `DatabaseService.Save`: the same guard `APIService.Post` has, on `setting.LevelsToSave`, with
  `!setting.Disable`.
- `IssueSetting()` initialises `Databases`. `DatabaseSetting()` initialises `LevelsToSave`;
  `APISetting()` initialises `LevelsToPost`. The configuration binder fills a list that exists
  the same way it creates one, so `appsettings.json` binding does not change.
- `DatabaseLogic.ExecuteQuery`: `using` for the `SqlConnection` and the `SqlCommand`; the
  `Dispose()` then `Close()` pair goes with the `finally`. The `catch { throw; }` goes too: it
  does nothing.

## Not In This Feature

- `DatabaseSettingExtensions.SetDefaultValues` takes a `level` it does not use; harmless, left.
- The SQL itself, the table names, the schema scripts under `source\database\`.

## Tests

- `Exception.Databases` with one `DatabaseSetting` whose `StringConnection` is
  `"not a connection string"` (the `SqlConnection` constructor throws `ArgumentException`
  before any network): with `LevelsToSave = [Warning]` and an `Error` save, `Errors` is empty
  (the sink was skipped); with `LevelsToSave` empty, `Errors` has the `ArgumentException` (the
  sink ran). Offline, fast. The first case fails before and passes after.
- `new IssueSetting().Databases`, `new DatabaseSetting().LevelsToSave` and
  `new APISetting().LevelsToPost` are not null.

## As Built

Built 2026-10-27, commit `2f4b483` (before 03, so the method was still `Save`; 03 made it
`SaveAsync` on top).

- **Before** (`DatabaseService` alone stashed, the constructors in): 1 of 16 fails,
  `TestDatabaseLevelFilter`.
- **After:** 16 of 16.
- `SettingTest.TestListsStartEmpty` covers the three constructors.
