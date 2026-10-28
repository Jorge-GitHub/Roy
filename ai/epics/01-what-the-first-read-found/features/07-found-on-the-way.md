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

Not built.
