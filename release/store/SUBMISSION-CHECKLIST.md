# Microsoft Store submission handoff

Status: preparation only, 2026-10-26. No Partner Center submission, name reservation, identity, age rating, certification result or live listing is asserted. The selected model is **free Store installation with a separately purchased EUR 29 gross one-time license for one person on four own PCs**.

## Account and product identity

- [ ] Sign in to the Aivana GmbH Partner Center **company** account and complete Microsoft's actual verification requirements. Use an authorized operator; do not put credentials in this repository.
- [ ] Reserve MAKRIVO as a Windows desktop MSIX app, subject to name availability. A GitHub repository name does not reserve a Store name.
- [ ] Copy the exact case-sensitive `Package/Identity/Name` and `Package/Identity/Publisher` from Product identity, together with the actual publisher display name. Do not replace the publisher value with an invented `CN=Aivana`.
- [ ] Configure Windows Desktop/x64 and review the minimum supported OS against the final manifest and actual tests. No Xbox, ARM or Windows S-mode compatibility claim is prepared.

[Microsoft Store setup](https://learn.microsoft.com/en-us/windows/apps/publish/get-started) and [MSIX identity/package requirements](https://learn.microsoft.com/en-us/windows/apps/publish/publish-your-app/app-package-requirements?pivots=store-installer-msix).

## Package and commercial configuration

- [ ] Build from the intended, reviewable source revision. Record both source revision and full package digest; the 2026-10-26 candidate in BUILD-EVIDENCE.md was rebuilt from clean bf99712 source; the older 2026-10-13 candidate is not the Store source.
- [ ] Supply the exact Partner Center identity, an increasing Store-compatible four-part version, real operator HTTPS purchase URL and the public key corresponding to the production license signer.
- [ ] Review dependency notices and bundled files; include no private license key, purchase secret, card data or proprietary vendor SDK without verified distribution rights.
- [ ] Use the MSIX submission path. Microsoft signs accepted Store MSIX packages; that does not make an unsigned local package generally installable or waive certification. MSI/EXE is a different path requiring publisher signing and hosting.
- [ ] Configure the Store acquisition as **free**, not a second paid purchase or a subscription. Complete the applicable external/in-product-purchase disclosure in the current Partner Center UI.
- [ ] Put the separate EUR 29 purchase and four-PC limit at the beginning of descriptions and explain it on first run. A Store installation alone does not grant the paid execution license.
- [ ] Keep production checkout disabled until legal/service acceptance is complete. Provide reviewers with working private activation access without charging them.

## Store listing fields

- [ ] Copy and review field contents from `LISTING.de-DE.md` and `LISTING.en-US.md`, including the opening purchase disclosure. Category proposal: Productivity; the operator must choose the matching current category in Partner Center.
- [ ] Initially declare only the submission languages for which complete listing material and accepted localized behavior exist. The prepared listing material covers de-DE and en-US. The app's wider translation inventory does not mean 27 Store descriptions have been supplied.
- [ ] Set the actual support contact and reachable privacy URL. Publish reviewed MAKRIVO-specific privacy/contract documents first; existing website pages and repository drafts are not approval.
- [ ] Complete the age-rating questionnaire from actual app content and functionality. No rating is invented here.
- [ ] Explain `runFullTrust` and external commerce in the private certification notes. Reconcile `CERTIFICATION-NOTES.md` with the final payload, actual optional integrations and operator deployment.

## Real screenshots and Store imagery

- [ ] Capture at least one real Desktop screenshot; four useful views are recommended. Use the actual submission UI, preferably in each listing language: profile tiles, macro editor, command palette, license information.
- [ ] Desktop screenshots must be PNG, at least 1366 × 768 and below 50 MB each. Do not upscale a small UI capture and represent it as a tested high-resolution layout. Use up to ten Desktop screenshots and review the current portal limits.
- [ ] Remove personal filenames, window titles, device IDs, recovery codes and license material. Avoid added marketing banners/logos over screenshots. Captions in the prepared listings are conditional on the real view captured.
- [ ] Provide a clean 300 × 300 app tile icon if desired; manifest icon assets and Store screenshot images are distinct outputs. No screenshot is asserted to exist in these documents.

[Microsoft screenshot and image requirements](https://learn.microsoft.com/en-us/windows/apps/publish/publish-your-app/msix/screenshots-and-images).

## Actual acceptance before upload

- [ ] Run Windows App Certification Kit and record the report for the exact package.
- [ ] On a clean authorized test machine/flight: install, normal Start launch, missing-license recovery, import of valid/invalid licenses, offline macro, cancellation, tray lifecycle, helpers and safe shutdown.
- [ ] Verify upgrade, profile/license behavior and uninstall/reinstall under MSIX package identity and AppData virtualization. Export data first; do not infer persistence from the direct-download MSI tests.
- [ ] Verify global hotkeys, Raw Input, supported device models, mixed-DPI displays and privilege-boundary behavior. Avoid real-user data/actions in test macros.
- [ ] Either implement and test a supported packaged autostart mechanism or disable the existing registry-based optional control for the Store build. Do not advertise it as accepted.
- [ ] Complete real LAN/mobile pairing, firewall, certificate and unauthorized-client tests before offering that integration as supported. Verify user-trusted plugin behavior and disclosures.
- [ ] Test the real purchase, recovery, device activation/deactivation, seat limit and refund flow. Confirm external services remain available for certification.
- [ ] Record legal approval and the actual deployment/data flows; update `release/acceptance.json` only with genuine evidence. Keep approval false/pending for unperformed work.

## Submit and publish

- [ ] Fill the remaining private reviewer access details, upload the real-identity MSIX and submit for certification. Do not upload the test-identity/test-key candidate as a production package.
- [ ] Review Microsoft feedback and fix actual failures. Only after approval and the operator's release decision publish the listing. Do not call preparation, upload or MakeAppx validation “Microsoft certification.”

Policy basis checked 2026-10-26: [current Microsoft Store policies](https://learn.microsoft.com/en-us/windows/apps/publish/store-policies), especially 10.1, 10.3, 10.6–10.8 and 10.14. Final eligibility is Microsoft's decision; this checklist does not assert approval.
