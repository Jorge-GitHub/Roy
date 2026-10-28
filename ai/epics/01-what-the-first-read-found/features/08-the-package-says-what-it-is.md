# 08: The Package Says What It Is

Epic 01. Size: small. Changes: `Roy.Logging.csproj`, `Roy.Logging.MVC.csproj`, `README.md`,
`Roy.Mvc.UT\appsettings.json`, and the `LICENSE` file if decision 3 says MIT.

## Status

Not started. Waits for decisions 3, 4 and 9. Last in the epic.

## Blocked By

Decisions 3, 4, 9.

## Depends On

01 to 07 (it bumps the version they ship under).

## Goal

The two csproj files, the `LICENSE` file and the README say the same version and the same
license; the MVC package has the metadata the library has; the README names the DLL the build
reads; no password is in the tree.

## What Happens Today

See the epic's *The package and the README*.

## Scope

- `Roy.Logging.csproj`: `Version` to decision 9's; `Copyright` to `2023-2026`;
  `PackageLicenseExpression` to decision 3's. `Roy.Logging.MVC.csproj`: `Title`, `Authors`,
  `Version`, `PackageId` (`Roy.Logging.MVC`), `Description`, `PackageProjectUrl`,
  `RepositoryUrl`, `PackageLicenseExpression`, `PackageTags`, the same icon and README, so
  `dotnet pack` gives a package that matches the library's.
- Decision 3: if MIT, `LICENSE` becomes the MIT text and the README badge says MIT; if GPLv3,
  both csproj files say `GPL-3.0-only` and the `LICENSE` file stays.
- `README.md`: `Avalon.Base.Extensions.dll` in *How to Use*; the MVC paragraph feature 05
  wrote stays; the sample JSON is unchanged.
- Decision 4: `Roy.Mvc.UT\appsettings.json` keeps both `Databases` entries with
  `"StringConnection": ""`; `Roy.Mvc.UT.csproj` gets a `UserSecretsId`, and *As Built* gives
  George the one `dotnet user-secrets set` line for each. The ethereal accounts stay: they are
  the fake SMTP service. Changing the password on `AVALONDEV` is George's and is not a step
  here.

## Not In This Feature

- Publishing to NuGet, a GitHub release, the website under `docs\`.
- Rewriting the history to remove the password (it is in 200-odd commits; rotation is the fix).

## Tests

- `dotnet pack` on both projects: two `.nupkg` files; each `.nuspec` has the version, license
  and id above.
- `git grep` for the old password and for `Password=` under `source\`: no hit.
- The build and `Roy.UT` as before.

## As Built

Not built.
