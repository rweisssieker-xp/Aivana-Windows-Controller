Retail checks use temporary license directories and ephemeral RSA keys. They never read or change the user's configuration.

Run:
  dotnet run --project tests/Aivana.Retail.Tests
  pwsh -NoProfile -File tests/Aivana.Retail.Tests/BuildValidation.Tests.ps1

The console suite links the production license store and queued-dispatch helper. It validates persistence, tampering, missing keys, wrong devices, failed import recovery, and entitlement loss while waiting for target restoration. Functional lookups exercise the retail labels in all 27 application languages and the unknown-key fallback. Execution routes and cancellation/export were independently reviewed. These checks do not substitute for manual signed-installer/hardware acceptance.

Retail builds require PowerShell 7 (pwsh) on the build machine for strict public-key validation. There is no PowerShell requirement for license verification at application runtime. Public RSA keys must be at least 3072 bits. Set MakrivoRetailBuild=true, MakrivoLicensePublicKeyFile to an operator-provided public-only JSON ring, and MakrivoCommerceUrl to the configured HTTPS customer page. Ordinary development builds are labeled as development builds.
