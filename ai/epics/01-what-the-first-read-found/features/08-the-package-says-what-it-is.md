# 08: The Package Says What It Is

Epic 01. Size: small. Changes: `Roy.Logging.csproj`, `Roy.Logging.MVC.csproj`, `README.md`,
and the `LICENSE` file if decision 3 says MIT.

## Status

Not started. Waits for decisions 3 and 9. Last in the epic.

## Blocked By

Decisions 3, 9.

## Depends On

01 to 07 (it bumps the version they ship under).

## Goal

The two csproj files, the `LICENSE` file and the README say the same version and the same
license; the MVC package has the metadata the library has; the README names the DLL the build
reads.

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

## Not In This Feature

- Publishing to NuGet, a GitHub release, the website under `docs\`.
- `Roy.Mvc.UT\appsettings.json`: its credentials are test values (decision 4) and stay.

## Tests

- `dotnet pack` on both projects: two `.nupkg` files; each `.nuspec` has the version, license
  and id above.
- The build and `Roy.UT` as before.

## As Built

Built 2026-10-27, commit `a755cb5`. Decision 9 taken as recommended: `1.0.19`.

- `Roy.Logging.csproj`: version `1.0.19`, copyright `2023-2026`. `Roy.Logging.MVC.csproj`: the
  same, with title, authors, description, `PackageId` `Roy.Logging.MVC`, icon, README,
  repository, tags, and `MIT` like the library's. The README names `Avalon.Base.Extensions.dll`.
- `dotnet pack` on both: `Roy.Logging.1.0.19.nupkg` and `Roy.Logging.MVC.1.0.19.nupkg`, each
  `.nuspec` with the id, version, `MIT`, `roy-nutget.png` and `README.md`.
- **Not done, decision 3 open:** the license. The csproj files say MIT, the `LICENSE` file and
  the README badge say GPLv3, as before. One line in each csproj and the `LICENSE` file once
  George says which.
- `Roy.Mvc.UT\appsettings.json` untouched (decision 4).
