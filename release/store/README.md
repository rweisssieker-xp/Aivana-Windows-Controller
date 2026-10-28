# Microsoft Store preparation

MAKRIVO uses **free Store installation with a separate EUR 29 gross one-time license for one person on four own PCs**. This was the user's selected model on 2026-10-26. Microsoft does not collect the purchase price in this flow. The existing retail license remains device-bound and works offline after import; this is not a Microsoft account purchase entitlement.

## Build an MSIX candidate

First build a complete retail package with `tools/publish-retail.ps1`. It must contain the retail public-key resource and pass the retail inventory checker. Development builds cannot be used. Then provide the exact case-sensitive identity from Partner Center:

```powershell
pwsh -NoProfile -File tools/publish-store.ps1 `
  -RetailPackagePath C:/operator/retail-1.0.0 `
  -OutputDirectory C:/operator/store-1.0.0.0 `
  -PackageName '<Package/Identity/Name from Partner Center>' `
  -Publisher '<Package/Identity/Publisher from Partner Center>' `
  -PublisherDisplayName '<actual publisher display name>' `
  -PackageVersion 1.0.0.0

pwsh -NoProfile -File tools/check-store-package.ps1 `
  -PackagePath C:/operator/store-1.0.0.0
```

Requires PowerShell 7 and Windows SDK MakeAppx. Set `-MakeAppx` if the SDK tool is elsewhere. Output directories must be new and cannot overlap the source. Store versions have four numeric parts and the final part is zero; increment the version for each distributed update. The output includes `MAKRIVO.msix`, the inspected payload, SHA-256 inventory and `store-status.json`. It does not install anything, enable developer mode, create a certificate, sign a sideload package or upload to Partner Center.

For a packaging-only test, replace the identity arguments with `-PackageName MAKRIVO.TestOnly -Publisher 'CN=MAKRIVO Test Only' -PublisherDisplayName 'MAKRIVO Test Only' -AllowTestIdentity`. This explicit test identity must never be submitted as the Aivana product. Test keys and `.invalid` commerce URLs also remain test configuration. No private keys belong in either package.

## Checks and evidence

```powershell
python -m unittest discover -s tests -p test_store_package.py
```

MakeAppx checks the manifest schema and produces a real MSIX. The checker compares every archive payload member with the staged payload, validates declared identity/capabilities/logo sizes/commercial terms and checks the full inventory. These checks establish package structure and integrity, not Windows installation behavior or Microsoft certification. Both `submissionReady` and `releaseApproved` remain false. Test-identity packages are unsigned and are not ready to install through a normal production Store flow.

See `BUILD-EVIDENCE.md` for the local test candidate and reproducible source revision. The Store scripts add no startup task, administrator request, UIAccess entitlement or driver. Existing optional registry-based autostart remains an acceptance gap for packaged operation.

## Complete the submission

Use the German and English listing drafts, `CERTIFICATION-NOTES.md` and `SUBMISSION-CHECKLIST.md`. Still needed: verification of any remaining company-account requirements, functioning production purchase/activation service, reviewed privacy/contract documents, private reviewer activation, real screenshots, WACK and actual packaged install/upgrade/uninstall/device acceptance.

Microsoft signs accepted Store MSIX packages and provides hosting/update delivery; a separately purchased CA certificate is not required for that Store path. Direct MSI/EXE distribution remains a different path. See [Microsoft's distribution comparison](https://learn.microsoft.com/en-us/windows/apps/package-and-deploy/choose-distribution-path) and [MSIX package requirements](https://learn.microsoft.com/en-us/windows/apps/publish/publish-your-app/app-package-requirements?pivots=store-installer-msix).

Partner Center update, 2026-10-26: the existing Aivana account is accessible, MAKRIVO is reserved as product 9NKW4WK59QX8, and the exact public package identity is recorded in partner-center-identity.json. A submission draft has been started; no package or public listing has been released.
