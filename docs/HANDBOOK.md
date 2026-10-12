# MAKRIVO user handbook

Manufacturer: **Aivana**. Documentation snapshot: **2026-10-12**. This handbook describes the current working-tree implementation. Experimental integrations and hardware-dependent functions are identified explicitly. Historical reports describe their dated snapshot, not a guarantee for a newer build.

## Documentation and languages

Use the documentation language selector to open the complete manual, feature inventory, troubleshooting, development notes, research and historical test reports in any of the 27 application languages. Generated translations are machine-assisted and have structural checks; they have not all received native-speaker review. Technical identifiers and executable examples remain unchanged. The original source remains available for comparison. Translated licensing analysis is informational and does not replace the original third-party legal terms or a final EULA.

## Installation and first launch

The initial target is Windows 11 x64. Keep every file and subdirectory of the distribution together. Launch `MAKRIVO.exe`. A source checkout is not an installer. Self-contained packages include their .NET runtime; framework-dependent packages require the appropriate .NET 10 Desktop and ASP.NET runtimes. ONNX and companion hosts must be included for their respective features.

1. Start MAKRIVO and choose the interface language in Settings. Automatic follows Windows. Keyboard layout is a separate setting.
2. Choose a profile, then add a tile through the searchable action catalog. Give it a recognizable name and choose its actions.
3. Review the steps and assign a shortcut if needed. Avoid combinations already used by Windows or your target application.
4. Save the draft. Unsaved changes are not the saved configuration.
5. Test with a harmless action, such as opening a document, and inspect the execution feedback.

Closing the main window normally leaves the application in the notification area. Double-click its tray icon to reopen it, or use the tray menu to exit. Starting with Windows is optional and initially disabled. The pause control suspends bindings; the icon shows a pause badge. Cancel running macros through the UI, tray or the configured cancellation shortcut; the default is `Ctrl+Alt+Escape`.

## Tiles, actions and the command palette

Tiles belong to profiles and can contain multiple ordered steps. The editor supports save/discard, preview, undo/redo and step reordering. The catalog supplies presets for programs, documents, URLs, text, clipboard, shortcuts, media, audio, windows, system actions and supported integrations. A preset requiring parameters opens as a draft.

Open the palette with `Ctrl+K` while MAKRIVO has focus. Search saved actions, presets, profile switching and tools. Arrow keys select; Enter activates; `Ctrl+Enter` edits a supported item; Escape closes. Some ready-to-use commands execute immediately, while configured actions require review. A separate global palette shortcut can be configured; new configurations use `Ctrl+Shift+Space`.

Text and clipboard steps support `{{date}}`, `{{time}}`, `{{datetime}}`, `{{clipboard}}`, `{{profile}}`, `{{process}}` and `{{user}}`. Date formatting is `yyyy-MM-dd`; time formatting is `HH:mm`. Unknown variables remain unchanged. Clipboard reading occurs only when requested. A clipboard action writes text without automatically pasting it.

Actions launched from the UI can target the last external application. If the target disappears or Windows blocks foreground activation, manually activate the target and retry. Successful key delivery does not prove that the target completed its task.

## Profiles and workplace setup

Profiles can match a process name and part of a window title. When both are set, both must match. Profile order resolves competing matches; the default profile is the fallback. Enable automatic selection explicitly. Lock Profile prevents automatic changes for the current session.

Open Workplace Setup from Tools or the palette. Select programs, documents, websites, an audio/display target or an OBS scene. The assistant prepares workplace, meeting, streaming, research or project profiles. Inspect the generated steps before saving. Setup does not execute the prepared actions.

## Macros, conditions and recording

Macros execute their steps in order and support waits, repetitions and cancellation. The general repetition limit is 100. Use Preview before running actions that type into another application or change system state.

Wait for Target Window accepts a process, a title substring and a timeout from 1 to 600,000 ms. At least one filter is required. A matching visible window must respond and become foreground before subsequent steps continue. A ready window does not prove that a document or service has finished loading.

The workflow editor adds file-exists, process-running and window-title conditions. A false condition skips the step and can skip additional steps forward. Negate the condition to express an alternative branch. Select stop-on-error or continue-on-error deliberately; continuing can leave later steps without their expected preconditions.

The action recorder captures shortcut chords and waits, not ordinary typing. It supports modifier shortcuts and function keys. Escape stops recording. Review the list and adopt it as an unsaved tile draft, then save explicitly. UI click recording is a separate tool described below.

## Keyboards, gestures and layers

The keyboard page lists detected interfaces and reference layouts. A single physical keyboard can expose several interfaces. Select the correct language and geometry; a product name alone does not identify ISO or ANSI layout.

Standard global hotkeys apply across keyboards. Device-specific Raw Input bindings observe a particular device and scan code, including supported function and media keys. **Raw Input does not suppress ordinary Windows input. An exclusive second keyboard is not production-ready.** A signed, licensed and tested filter-driver backend is still required; none is installed by MAKRIVO.

Supported special-key bindings can use immediate press, tap, hold or double tap. Held-layer keys temporarily select another mapping and restore the base layer on release; layer-selector keys do not run their own macro. Hold thresholds range from 150 to 5,000 ms; double-tap windows from 150 to 1,000 ms. These mappings are distinct from ordinary global hotkeys.

The special-key wizard detects compatible keys without executing their assigned actions during capture. Diagnostics explains unavailable devices, paused bindings, profile mismatches and conflicts. The overlay shows the selected layout and bindings; its default hold shortcut is `Ctrl+Alt+F1`.

| Integration | Requirements and limitations |
| --- | --- |
| Corsair | Running iCUE, SDK access and a compatible model. G1–G6 and supported RGB functions; proprietary Fn, wheel, lock and profile controls are not generally exposed by the G-key API. |
| Logitech G | Original local x64 G-key/LED SDK libraries require explicit approval. G-key events do not uniquely identify several identical physical keyboards. |
| Razer | Chroma RGB and standard input; map proprietary controls in Synapse where needed. |
| SteelSeries | GameSense RGB and standard input; map proprietary controls in GG where needed. |
| Wooting | Compatible original local x64 SDK for analog values and thresholds; normal HID input remains active. |
| Other keyboards | Standard hotkeys or supported physical scan-code bindings. Fn and proprietary controls depend on the model and vendor configuration. |

Manufacturer research describes documented capabilities, not implemented or tested compatibility with every listed device. New adapters still need real hardware acceptance. Do not download SDK binaries from untrusted sources; SDK distribution rights are separate from technical compatibility.

## Audio, displays and OBS

Audio steps set volume from 0 to 100 percent and mute/unmute/toggle supported output or microphone endpoints. Choose a specific endpoint or use the Windows default. A missing saved target produces an error instead of silently controlling another device. Refresh Devices after reconnection. Endpoint control is not a claim of independent per-application mixer support.

Display actions increase/decrease brightness by 10 percentage points or set a value from 0 to 100. Internal displays use supported WMI interfaces; external monitors require DDC/CI support and usually its monitor-menu setting. Docks, adapters and cables can block control. Choose all supported displays or one monitor. Reconnection can change available identities; review the saved target. Missing readings are shown as unavailable rather than measured values.

OBS requires WebSocket v5 enabled in OBS Tools. Configure the endpoint, test the connection and load scene/input names before saving. Supported actions include scene switching, stream/record start/stop/toggle, recording pause/resume and input mute. Passwords can be stored in Windows Credential Manager and are excluded from configuration exports. A successful OBS command does not establish every downstream streaming effect.

## Result verification and execution history

Enable Verify Result for suitable steps. The app distinguishes preflight checks, confirmed effects, failures, cancellation and effects that cannot be verified. Process existence is not application readiness. Relative changes, shortcuts and text entry cannot receive a blanket success claim. Inspect the activity log and verification history when an action fails. Configuration errors should be corrected before rerunning the complete macro.

## UI automation and click recording

Add a UI Automation step, select the operation and capture the target under the pointer after the displayed delay. Capture alone executes nothing. Check the selector and require a unique match. Supported operations include `Invoke`, `Select`, `Toggle`, `Focus`, `SetValue` and `WaitState`. Password fields, disabled controls and ambiguous selectors are rejected.

UI automation runs in a bounded companion process. It depends on the target application's Windows accessibility support and permissions. The click recorder captures supported actions and pauses; it does not record general text input. Review and validate its draft before adopting it. UI changes in an external app can invalidate selectors. Process isolation limits crashes and cancellation, but is not a security sandbox.

## Plugins

Open Extensions to inspect a package before importing it. Package structure, manifest and hashes are checked. An import starts disabled. Read the declared actions and explicitly trust the publisher before enabling. Plugins run with your user permissions in a separate process; that process is **not a sandbox**. Only install packages you trust.

The manager supports export, disable, uninstall and restore/discard of a previous version. Missing dependencies block invocation. The example package provides countdown and local-state actions. The supported schema is a documented JSON-Schema subset, not every possible JSON-Schema feature. There is no public plugin marketplace.

## Mobile panel

1. Open Mobile Panel, select a local IP and explicitly choose the profiles to expose. The default loopback address is only reachable on the PC itself; select the appropriate LAN address for a phone or tablet.
2. Start the panel and read its HTTPS address, certificate fingerprint and single-use six-digit pairing code.
3. On a device in the same trusted network, open the address. Compare the SHA-256 fingerprint through a trusted channel before trusting the local certificate. The app does not install a trusted certificate automatically.
4. Pair using the displayed code and test one harmless tile. A manual Windows Firewall permission may be needed for LAN access. Do not expose the port to the internet.
5. Stop the panel when finished, or revoke paired clients through the app.

Private server keys are protected with Windows DPAPI; client credentials are stored as hashes. Changing the IP or renewing the certificate can require renewed trust. The panel shows action and verification states, not every possible live sensor. Browser-layout and local HTTPS checks do not replace real phone/tablet LAN acceptance.

## Local AI meeting assistant

This optional experimental assistant is disabled by default. Open it from Tools, explicitly enable it, select a runtime/model and add only the resources you want it to consider. State the goal, create a draft, inspect unresolved requests and transfer the result to the normal editor. Nothing is automatically executed; saving and running remain explicit actions. There is no automatic runtime fallback.

| Runtime | Setup |
| --- | --- |
| Ollama | Separate local service and a locally installed model at `127.0.0.1:11434`. Declared remote/cloud models are rejected. |
| LM Studio | Local server with a locally loaded model and JSON-schema support at `127.0.0.1:1234`. Do not enable remote routing for local processing. |
| llama.cpp | Local `llama-server` and compatible model with JSON-schema support at `127.0.0.1:8080`. |
| ONNX Runtime GenAI CPU | Trusted compatible local GenAI text-model folder; select `genai_config.json`. Inference uses a bundled companion process without HTTP. |

HTTP clients use loopback only, do not use proxies and do not follow redirects. MAKRIVO cannot reliably establish whether a compatible local server internally calls a remote service. Authenticated local servers are not configurable in this version. Model weights, Python and local inference servers are not automatically downloaded or installed.

ONNX packages need `tokenizer.json` and a valid chat template. Supported template forms in `tokenizer_config.json` are a string `chat_template`, a dictionary with a string `default`, or an unambiguous named/default template list. If the template is missing or null, `chat_template.jinja` is used as a fallback. Empty, ambiguous, oversized or incorrectly typed templates are rejected. Arbitrary ONNX files, embedding/vision models and GPU-only packages are not interchangeable with compatible GenAI text models. The bundled GenAI runtime version is 0.17.1, CPU. Use only trusted models; process separation is not a sandbox.

The assistant supports selected programs without arguments, selected documents and websites, and microphone mute when expressly requested. It does not create messages, appointments, arbitrary scripts, free clicks, downloads or headset switching. The goal and resource names are sent to the selected local runtime; resource paths and file contents are not separately supplied. A goal typed by the user can itself contain personal data. A local runtime's own network behavior remains the user's responsibility.

The ONNX request budget is 90 seconds including model loading, up to 3,072 prompt tokens and 1,024 generated tokens, also limited by model context. Malformed/truncated JSON and unknown resource IDs are rejected. Cancel or close to end a request. A random-model runtime test establishes execution, not useful planning quality or time savings. Local experiment logs contain event identifiers and timings, not goal text or document contents; they are not automatically uploaded.

The later small Instruct-model evaluation produced zero valid plans in four German/English trials. Native inference worked, but the model failed the strict planning contract. This is a negative quality result, not a recommended model or a proven useful assistant. See the dated local-runtime test report for the exact model revision and limits.

## Import, export, synchronization and backup

Review import previews before replacing a configuration. Imported actions start disabled, and portable synchronization removes hardware-specific bindings. Reassign devices and enable actions deliberately after inspection. Review text/action contents before sharing an export; excluding passwords does not make all user-created action contents non-sensitive.

Profile synchronization exchanges files in a selected folder, optionally one managed by your existing cloud client. MAKRIVO supplies no cloud account and stores no cloud credentials. Revisions prevent silent overwrites. Including action contents is an explicit choice. Back up your configuration before a replacement import. The default configuration is `%LOCALAPPDATA%\AivanaWindowsController\settings.json`; the historical directory name is retained for compatibility. Close the app before manually copying/restoring it. Stored SDK approvals and OS-protected credentials may need to be recreated on another PC.

## Troubleshooting

| Symptom | What to check |
| --- | --- |
| Shortcut does nothing | App running, bindings not paused, correct active profile, saved/enabled tile, shortcut conflicts and available target. |
| Target window cannot activate | Manually activate it. Windows foreground restrictions and privilege differences can block automation. |
| Special key is not detected | Vendor service/SDK/model support; configure proprietary controls to emit a supported standard key if possible. |
| Second keyboard also types normally | Expected with Raw Input. Exclusive suppression is not implemented for production. |
| Audio or brightness unavailable | Refresh devices; inspect selected identity, endpoint sessions, WMI/DDC/CI support and dock/cable restrictions. |
| OBS offline | WebSocket v5 enabled, correct endpoint/password, reachable service and valid scene/input names. |
| UI automation fails | Companion host included, unique valid selector, accessible enabled target, supported control and permissions. Retry a transient timeout only after reviewing the target. |
| Mobile panel unreachable | LAN rather than loopback address, same network, server running, approved profiles, certificate fingerprint/trust and manual firewall permissions. |
| Plugin fails | Package integrity, explicit trust/enabled state, dependencies, protocol compatibility and execution log. |
| AI request fails | Correct selected local runtime, service/model ready, supported JSON schema, trusted compatible model folder and request limits. |
| Layout or text seems wrong | Separate UI language from keyboard geometry; resize the window, check display scale and report the exact page/language/build. |

For a useful bug report include Windows version, app build/commit, language, display scaling, hardware model, a minimal sequence and the relevant redacted log. Remove personal paths, action contents and credentials before publishing.

## Building, testing and packaging

Use the SDK pinned in `global.json`. From the repository root:

```powershell
dotnet build Aivana.slnx -c Release
dotnet run --project tests/Aivana.Tests -c Release
dotnet run --project tests/Aivana.Windows.Tests -c Release
dotnet run --project src/Aivana.Desktop -c Release -- --smoke-test
./tools/publish-complete.ps1 -SelfContained
python tools/check_documentation.py
```

Windows integration tests can open isolated windows and temporarily configure/release supported vendor bindings. Run them without another controller owning those bindings. The default brightness/audio probes are read-only; explicit hardware round-trip tests are separate. Mock protocols, rendered screenshots and random inference graphs must not be represented as real hardware/model-quality acceptance.

The complete publisher assembles companion hosts, mobile assets, the example plugin, the ONNX runtime and available dependency notices. Keep its subdirectories. A successful build is not a signed installer or legal release approval. Source and builds may evolve separately; compare the documentation snapshot and commit with the installed version.

## Licensing, privacy and release status

The intended commercial model is a one-time purchase with perpetual use of the purchased major version; final price, device allowance and upgrade terms must be set in the actual sales terms. A production purchase/activation system is not established by this handbook. There is no required cloud account for everyday control.

Do not distribute proprietary SDK binaries without proven permission. Preserve license and third-party notices for every shipped dependency, including .NET and ONNX packages. Model weights have their own terms. The dated license audit predates some new dependencies and is not a clearance for those additions. The repository being public does not itself grant a general open-source license.

Real vendor hardware, phone/tablet LAN operation, mixed-monitor/DPI transitions, long-running use, complete linguistic review, exclusive driver support and useful AI planning quality remain separate acceptance work. Never infer approval from feature presence alone.
