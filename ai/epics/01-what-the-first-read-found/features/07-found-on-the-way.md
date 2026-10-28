# 07: Found On The Way

Epic 01. Size: small. Changes: `source\Roy.Logging\Roy.Logging\`, `Roy.Logging.MVC.csproj`,
`Roy.UT.csproj`, `Roy.Mvc.UT.csproj`, `source\libraries\`.

## Status

Not started. Waits for decision 10 and for 01.

## Blocked By

Nothing (decision 10 only decides whether the last item is done).

## Depends On

01. After 03, so the package-reference check runs against the final code.

## Goal

What is there is used. Nothing changes what Roy does.

## What Happens Today

See the epic's *Found on the way*.

## Scope

- `LabelDecorator.ReplaceMachineInformationLabels`: one of the two `OperativeSystemTag` lines
  goes.
- `EmailSetting._Language`, the `JSType` using in `MessageDetailDTOExtensions.cs`, the
  `System.Numerics` using in `WebApplication.cs`: removed.
- `Domain\Settings\Attributes\LogSetting.cs` becomes `InformationSetting.cs` (`git mv`); the
  class and namespace are unchanged.
- Package references, one at a time, the build and `Roy.UT` after each: a reference whose
  removal keeps the build and the tests as they were is removed; one the build needs stays,
  and *As Built* says which and why. Candidates: the five in `Roy.Logging.MVC.csproj` listed
  in the epic, and `Azure.Identity`, `BouncyCastle.Cryptography`, `MimeKit`,
  `Microsoft.Data.SqlClient` in `Roy.UT.csproj` and `Roy.Mvc.UT.csproj`. The test projects'
  copies are the likeliest to be needed: `Microsoft.Data.SqlClient` and MailKit's `MimeKit`
  reach a test project through `Roy.Logging` already, but a direct package can be what makes a
  native or satellite file land in the test output.
- `source\libraries\Avalon\` (decision 10): `git rm` of the folder.

## Not In This Feature

- `Domain\Contants` (the epic's *Out Of Scope*).
- Any nullable annotation, any warning not on a changed line.
- `TagDecorator.CleanTagDetails` runs the same loop twice; harmless, left.

## Tests

The build and `Roy.UT` after each removal, as in *Scope*. The *As Built* table: each reference,
kept or removed, the warning count after. The output folder's DLL list before and after, the
same except what a removed package carried.

## As Built

Built 2026-10-27, commit `1b308e2`.

- The duplicate replace, the field and the two usings are gone; `LogSetting.cs` is
  `InformationSetting.cs` (`git mv`, the class and namespace unchanged).
- **Package references:** all of the candidates were removed at once, and the solution built
  with 0 errors and the suite passed 27 of 27, so none was needed and the one-at-a-time pass
  was not required.

| Project | Removed | Kept |
|---|---|---|
| `Roy.Logging.MVC` | `Microsoft.AspNetCore.Authentication.JwtBearer`, `Microsoft.IdentityModel.Tokens`, `Microsoft.AspNetCore.Http.Abstractions` 2.3.10, `Microsoft.Extensions.Configuration.Abstractions`, `Microsoft.Extensions.DependencyInjection.Abstractions`, `Microsoft.Extensions.Hosting.Abstractions`, `Microsoft.Extensions.Configuration.Binder` | a `FrameworkReference` to `Microsoft.AspNetCore.App`, which is where `WebApplication`, `HttpContext` and the configuration binder come from |
| `Roy.UT` | `Azure.Identity`, `BouncyCastle.Cryptography`, `MimeKit`, `Microsoft.Data.SqlClient` | the test packages, `Microsoft.AspNetCore.TestHost` (feature 05) |
| `Roy.Mvc.UT` | the same four | nothing; it is a Web SDK project |

- `Roy.UT`'s output holds 42 DLLs after; the count before was not recorded, and the bytes were
  not compared.
- `source\libraries\Avalon\` is left (decision 10 is open). `TagDecorator`'s double loop is left.
