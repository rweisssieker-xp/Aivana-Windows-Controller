# MAKRIVO certification notes — draft for operator completion

Status 2026-10-26: **not submitted, not certified**. These notes describe the intended external-license MSIX distribution. Before pasting into Partner Center, reconcile them with the exact package, actual deployment and completed packaged-app tests. Do not submit placeholder service URLs, test identities or unavailable reviewer credentials.

## Product and payment

MAKRIVO is a non-game x64 Windows desktop productivity application by Aivana GmbH. Store installation is free. Running actions requires a separately purchased, device-bound license: EUR 29 gross once, one person on up to four own PCs, commercial use permitted, perpetual major version 1. No subscription or renewal. Profile editing, configuration export and cancellation remain available without a license.

The app opens the configured HTTPS purchase/activation page after installation. The current implementation uses Stripe-hosted Checkout, an Aivana-operated Cloudflare Worker/D1 entitlement service and RSA-signed local license files. The Windows app does not collect card details. The production provider, domain, legal documents and service availability must be verified before submission. Activation sends a product-scoped device hash; the raw Windows device identifier is not transmitted by the licensing implementation. Existing offline licenses have no periodic online check or runtime expiry. Deactivation frees a server seat but cannot remotely erase an issued offline file.

Declare this external commerce in Partner Center. Microsoft Store policy 10.8 permits secure external commerce for non-game PC apps and requires accurate purchase disclosure. Policy 10.3 requires a testable product and working required servers. This is a policy mapping, not a prediction of certification approval. [Current Store policies](https://learn.microsoft.com/en-us/windows/apps/publish/store-policies).

## Reviewer access — required before submission

The operator must provide a private, working reviewer activation route at no charge to the reviewer, with sufficient device slots for independent test machines. An imported offline license is device-bound; a license from the developer's PC will not authorize the reviewer's PC. Put the actual reviewer-only access details in Partner Center's private certification notes, never in this repository, screenshots or public listing. Do not ask reviewers to make a real purchase. A recovery code is a bearer credential and must not be published.

Record the actual HTTPS endpoint, method for obtaining a reviewer entitlement, availability dates and support contact in the private submission. The current test candidate and test-key configuration are not such access. No working reviewer account or activation entitlement is asserted here.

## Why full trust is required

The package contains a WPF desktop application running as the interactive user. It uses Windows desktop APIs for global hotkeys, Raw Input device identification, deliberate keyboard/mouse automation, application/window operations and optional device integrations. UI Automation is performed by the bundled automation worker. This cannot be described as an AppContainer-only web application. The package requests `runFullTrust`; explain this restricted capability in the submission. [Capability declarations and approval](https://learn.microsoft.com/en-us/windows/uwp/packaging/app-capability-declarations).

No installer-granted administrator rights, UIAccess entitlement, filter driver, NT service or automatic startup registration is introduced by the Store packaging path. Windows privilege boundaries continue to apply. Raw Input does not exclusively suppress the normal input of a second keyboard. Do not advertise bypass of application security or automation of elevated/protected targets.

## Optional processes, networks and extensions

- The bundled automation helper performs requested UI Automation work. Verify helper launch, cancellation, target selection and error handling after MSIX installation.
- The optional mobile host serves a user-enabled local HTTPS panel; the current source listens on port 47831 at the configured local address and uses pairing/authentication. It is not a public cloud-control service. Verify firewall behavior, certificate presentation, authorization, disconnect and shutdown on real LAN devices before offering it as a supported Store feature.
- User-imported plugin packages require explicit trust in the existing app. Their separate process is **not a security sandbox**: a trusted plugin can act with the user's permissions. There is no public plugin store and no third-party driver bundle. Explain extension import/export and executable-code behavior to the reviewer; test it under package identity rather than claiming MSIX sandboxes plugin code.
- Optional integrations can connect to services/programs the user configures, such as OBS. Device support remains model- and SDK-dependent; no blanket vendor compatibility is claimed in the prepared listing. Proprietary SDK redistributables are not added by this packaging work.
- Experimental local AI is not an advertised Store feature. If the selected payload includes an experimental host, disclose it and its disabled default; do not claim validated planning quality, included model weights or accepted Store review of this behavior.

## Reproduction sequence for the final package

1. Install the real MSIX through an authorized test or Store flight route; record Windows build, package identity/version and package SHA-256. Launch normally from Start. Do not use the existing `--smoke-test` path as packaged acceptance: it writes snapshots below the app base directory, which is read-only when installed.
2. Without a license, create/edit a profile, export it to a user-selected path and attempt an action. Confirm a clear license requirement, no execution and accessible export/cancellation.
3. Obtain the private reviewer entitlement, copy this machine's device code to the real activation page, download the resulting license and import it. Confirm a simple action against a test application. Verify wrong-device or damaged license rejection does not replace a valid file.
4. Disconnect networking after activation and run/cancel a harmless local macro. Confirm purchased-version operation remains available.
5. Test tray hide/show/exit and hotkeys. Verify helpers stop on exit and no action remains stuck after cancel.
6. Test upgrade and uninstall/reinstall on a separate test machine. Verify configuration/export and license handling under MSIX AppData virtualization; do not assume the MSI preservation behavior carries over.

## Known packaged-app acceptance gaps

The existing optional autostart control writes `HKCU\Software\Microsoft\Windows\CurrentVersion\Run` using the executable path. No MSIX StartupTask is introduced by this design. Do not list packaged autostart as supported until a supported packaged mechanism is implemented and tested, or disable that control in the Store build. No startup behavior is enabled by the packager.

Packaged device/Raw Input access, mixed-DPI monitors, UI Automation, helper-process startup, LAN pairing, input cancellation, plugin permissions, read-only package paths and configuration migration all require actual acceptance. Compilation, MakeAppx validation and archive integrity alone do not establish these results. Windows App Certification Kit results and actual installation evidence are pending until recorded by the operator.

## Privacy and support

Support contact: info@aivana-gmbh.ai. Seller details are documented in the [existing official imprint](https://aivana-gmbh.ai/de/Imprint). The [existing website privacy notice](https://aivana-gmbh.ai/de/Privacy) is not confirmed to cover MAKRIVO's new purchase, activation and optional integration flows. Publish and link a reviewed, accessible MAKRIVO notice before submission; use `release/privacy-data-flows.md` as an unapproved working input, not as proof of compliance.
