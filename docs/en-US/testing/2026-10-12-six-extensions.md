<!-- generated-doc locale=en-US source=docs/testing/2026-10-12-six-extensions.md sha256=ff2eb755b2f18dce8c7bd7c832018ae71ee706bcca65e37c191dbb55ff999c1e -->

> Machine-assisted translation. Structure, code and links are checked; native-speaker and legal review are not implied. Historical reports retain their original dates and scope. [Original source](../../testing/2026-10-12-six-extensions.md)

# Six extensions: implementation and test bench

Status: 12. October 2026, Branch `codex/six-extensions`. Working status, no production release.

## Service

The command palette opens the entries **Set up workstation**, **Keyboard manufacturer and SDKs**, **Mobile panel**, **Extensions**, **UI automation**, **UI record flow** and **Execution test**.

- Workspace Assistant creates Workplace, Meeting, Streaming, Research, and Project profiles. Programs from Registry/Start Menu or File Selector, URLs, Output/Screen and OBS Scene are displayed as steps before saving. The facility does not perform any actions.
- In the step editor, **Check result** switches on the preliminary check and post-check. History and mobile panel distinguish actual confirmation from unverifiable effect. Relative changes, keyboard shortcuts and text entry do not provide a general confirmation of effectiveness. Proof of the process does not prove the technical readiness of the application.
- UI Automation uses a separate, time-limited worker. Selection via mouse position, selector check, Invoke/Select/Toggle/Focus/SetValue/WaitState; multiple hits, disabled targets and password fields are rejected. Recording captures supported click actions and pauses; Text entries are not recorded. The admission will be checked before acceptance.
- Extensions: ZIP inspection, hashes, schema, disabled import, explicit trust before activation, isolated process with limited IPC, export, deactivation, uninstall, restore/discard previous version. Process isolation is not a sandbox: shared plugins run with user rights. Sample package `makrivo.example.zip` contains countdown and status action. Supported schemas represent a documented simple JSON schema subset in the source code; no public marketplace.
- Mobile panel: select local IP and shared profiles, open HTTPS address, check certificate using the displayed SHA-256-Fingerprints and consciously trust it, then enter a one-time six-digit code. Private keys are protected with Windows-DPAPI; only hashes of client access are saved. Certificates change after renewal or IP change. The Windows firewall is released by the user if necessary. No automatic certificate installation, port forwarding or firewall change. Client access can be revoked. The panel shows action status and test history; it is not yet a universal live sensor dashboard.

## Keyboards

| Manufacturer | Implementation | Limitation |
|---|---|---|
| Corsair | existing iCUE-G keys and RGB | Local iCUE connection and suitable model required |
| Logitech G | G-Key SDK and separate LED SDK | explicitly release original local x64-DLL; G-Key SDK does not provide individual identity of multiple keyboards |
| Razer | Chroma RGB and standard key recognition | Assign proprietary keys previously in Synapse |
| SteelSeries | GameSense-RGB and standard key recognition | assign proprietary keys previously in GG |
| Wooting | Device-related analog values, pressure/release thresholds | original local x64-SDK-DLL; normal HID input remains active |
| Keychron/QMK/VIA/Ducky/ASUS/HyperX/Roccat/others | Standard physical buttons by device and scan code | Fn and proprietary controls depend on model |

No new proprietary SDK DLLs are included. The new manufacturer adapters are not yet hardware validated without suitable physical devices.

**Exclusive second macropad is not ready:** Raw Input observes devices separately, but does not suppress their normal Windows input. A production release requires a licensed, signed filter driver verified under Windows 11/Secure Boot and emergency release, crash, suspend and reconnect testing. No driver was installed. The application explicitly marks this limit.

## Exams

- Release build: successful, zero warnings/errors.
- Core Console: All test groups pass, including vendor key conflicts, plugin rollback, and actual execution errors in the test history.
- Extension integration: real UIA worker with WPF test application; unique invoke, ambiguity, disabled targets, password exclusion. Real named pipe plugin process, protocol boundaries, analog hysteresis and Logitech ABI decoding successful.
- Real HTTPS host: pinned certificate, unpaired access, false/unique code, origin protection, browser files, hash storage and protected key successful. An Schannel bug with ephemeral keys discovered in testing has been fixed.
- Windows suite with explicitly omitted foreground success test: successful; Audio/OBS/iCUE/Raw Input and screen status checked. In the previous run, the full suite failed because the focus switch to the isolated test window was refused. The subsequent run to the AI ​​prototype also included the full suite including focus switching and a connected iCUE keyboard. Another repeat run failed again due to the change in focus; The context-dependent test is therefore not stable green. This test was not attenuated. DDC read accesses that fail for a short time are repeated for a limited time; no brightness was changed.
- WPF-Smoke: all previous and new dialogs rendered, configuration invariance, editor and CRUD successful; Screenshots in build folder `smoke`.
- Browser: Playwright with Edge, 390×844 and 1024×768, coupling flow/UUID/one-time send/no horizontal overflow successful. API Mock; real HTTPS test separately. Pictures at `artifacts/mobile-browser`. Browser plugin was not available, hence regular Playwright fallback.
- Independent code review conducted; Fixed delayed mobile commands, missing terminal check results, process path check, plugin rollback and RGB heartbeat restart.

Still open: exclusive driver integration, physical manufacturer/phone/tablet LAN testing, Windows scaling 150/200 percent and full translation checking of all new dialog text. New dialogue texts are predominantly German; Existing language catalogs are retained. Therefore, the overall requirement ** has not been completely accepted**.

## Generate package

`./tools/publish-complete.ps1 -SelfContained` creates a new folder under `artifacts/release-*`, including desktop, UIA worker, HTTPS host, browser files, importable sample plugin and runtime license notes. The installed application is not overwritten. Without `-SelfContained`, the package needs matching .NET-10-Desktop-/ASP.NET-Runtimes.

The package is a local testbed, not a signed retail installer. Own Terms of Use and SDK distribution rights are separate releases; see existing documentation under `docs/licensing`.
