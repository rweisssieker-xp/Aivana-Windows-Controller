# MAKRIVO Extensions Implementation Plan

> **For agentic workers:** REQUIRED SUB-SKILL: Use superpowers:executing-plans for native execution or superpowers:subagent-driven-development after the user selects delegation. Track steps with checkboxes.

**Goal:** Deliver six integrated extensions and capability-based multi-manufacturer keyboard support.

**Architecture:** Preserve the Core / Windows / WPF separation. Introduce isolated plugin and mobile-service processes with narrow authenticated IPC; all execution goes through the existing controller and MacroRunner queue. Hardware adapters advertise detected capabilities rather than inferred brand-wide support.

**Tech Stack:** C#, .NET 10, WPF, Windows UI Automation, Win32 Raw Input, documented vendor SDKs, ASP.NET Core HTTPS/WebSocket, System.Text.Json.

**Spec:** `docs/superpowers/specs/2026-10-12-six-extensions-design.md` (approved by user).

## Global Constraints

- Existing configuration must load unchanged; append action kinds rather than reorder their persisted enum values.
- Preserve existing profiles, G1/G2 assignments, local operation and account-free usage.
- Raw Input provides observation only. Exclusive per-device suppression requires an actual keyboard filter backend.
- Imported executable plugins must not execute automatically.
- Default to disabled and restrict configured interfaces for the mobile service; no plaintext authentication fallback.
- Shortcut/text delivery is not proof that the target application accepted the command.
- Do not replace the installed application, install drivers, alter real keyboard mappings or open network access as an incidental test.
- Preserve unrelated uncommitted changes. Do not commit existing shared edits as part of this feature. Stage only independently reviewable feature hunks if commits are requested.

## Review Focus

1. Duplicate identical keyboard models: ambiguous reconnect must require re-selection (Tasks 10–12).
2. Cancellation during UIA/provider/plugin/network waits: no deferred action after cancellation (Tasks 1, 4, 6, 8).
3. User edits while wizard or package preview is open: revision mismatch must prevent silent overwrite (Tasks 3, 7).
4. Language and physical layout differ: selectors and scan-code bindings must not depend on localized display names (Tasks 4, 10).
5. Network retry and plugin restart: commands must not silently execute twice (Tasks 6, 8).

## Shared contracts and dependency order

Tasks 1–3 establish execution and setup. Tasks 4–5 build UIA. Tasks 6–7 deliver plugins. Tasks 8–9 deliver mobile access. Tasks 10–12 deliver keyboard adapters and filter integration. Task 13 validates the complete product.

Each task follows a failing-test → minimal implementation → passing-test cycle. A task is complete only when reachable through the application and its checks pass. Scaffolds, mock adapters and documentation alone do not satisfy a deliverable.

### Task 1: Typed verification and compatible persistence

**Files:** Create `src/Aivana.Core/ActionVerification.cs`, `tests/Aivana.Tests/VerificationChecks.cs`; modify `src/Aivana.Core/Models.cs`, `src/Aivana.Core/MacroRunner.cs`, `src/Aivana.Core/ConfigurationValidator.cs`, `tests/Aivana.Tests/Program.cs`.

**Interfaces:** Produce `VerificationState { Requested, Completed, Verified, Failed, TimedOut, Cancelled, Unverifiable }`, `ActionVerificationResult(VerificationState State, string Code, string Message)`, `IActionVerificationProvider.PreflightAsync(ActionStep, CancellationToken)` and `VerifyAsync(ActionStep, CancellationToken)`, both returning `Task<ActionVerificationResult>`. Add optional serializable `VerificationOptions` with enabled flag and timeout (100–60000 ms), default disabled. Publish typed per-step results including run ID, tile ID and step index without action payloads.

- [ ] Write checks: legacy JSON retains existing action numeric values; absent options disable verification; invalid timeout fails validation; preflight failure prevents executor invocation; failed postcondition never produces verified success; unsupported executor is Unverifiable; cancellation stops verification and subsequent steps.
- [ ] Run `dotnet run --project tests/Aivana.Tests -c Release`; confirm new checks fail before implementation.
- [ ] Implement contracts, validation, cloning/snapshot preservation and MacroRunner integration. Respect OnError stop/continue policies; preserve existing execution feedback subscribers.
- [ ] Run core tests; require all checks pass.

### Task 2: Windows verification providers and result UI

**Files:** Create `src/Aivana.Windows/WindowsActionVerification.cs`, `src/Aivana.Desktop/MainWindow.Verification.cs`, `tests/Aivana.Windows.Tests/VerificationChecks.cs`; modify `src/Aivana.Windows/WinActionExecutor.cs`, `src/Aivana.Desktop/DesktopController.Feedback.cs`, `src/Aivana.Desktop/MainWindow.Editor.cs`, test entry points.

**Interfaces:** WindowsActionVerification implements Task 1 provider using existing audio, display, process/window and OBS clients. Preflight tests availability and identifiers; verification compares desired state after bounded polling. Produce controller result snapshots consumed by the WPF history view.

- [ ] Add failing tests for unavailable explicit audio/display IDs, OBS timeout, mismatched observed state, unknown shortcut effects, sensitive-payload omission and cancelled polling, using injectable read providers.
- [ ] Implement real read-backed verification for absolute volume, mute, absolute brightness, process/window readiness and supported OBS commands. Relative/toggle actions require a captured pre-state; otherwise report Unverifiable.
- [ ] Add per-step verification controls and safe execution history showing state, failed requirement and recovery text.
- [ ] Run core/Windows checks and WPF smoke with injected state providers; no real brightness or audio writes.

### Task 3: Guided workspace creation

**Files:** Create `src/Aivana.Core/WorkspaceSetup.cs`, `src/Aivana.Windows/WorkspaceDiscovery.cs`, `src/Aivana.Desktop/MainWindow.WorkspaceWizard.cs`, `tests/Aivana.Tests/WorkspaceSetupChecks.cs`; modify palette and dashboard entry points.

**Interfaces:** `WorkspaceSetup.CreateProfile(WorkspaceSelection selection) -> Profile`; selection contains template ID, chosen executable paths, URLs, audio/display IDs, OBS scene and selected bindings. Discovery returns named candidates with stable IDs and availability. Draft records configuration revision for conflict detection.

- [ ] Add failing tests for all five requested wizard families, independent generated steps, missing required targets, cancel without mutation and save against changed revision.
- [ ] Discover applications through registered app paths and Start Menu shortcuts; devices through existing providers; scenes through read-only OBS query. Keep manual selection available for undiscovered apps.
- [ ] Implement Choose workflow → Select targets → Choose controls → Preview/validate → Save. Generate editable profiles and never run during setup.
- [ ] Add actual WPF wizard smoke checks for completion/cancel/conflict; run all tests.

### Task 4: UIA selectors and bounded actions

**Files:** Create `src/Aivana.Core/UiAutomationAction.cs`, `src/Aivana.Windows/UiAutomationService.cs`, `tests/Aivana.Windows.Tests/UiAutomationChecks.cs`; modify models, configuration validation and WinActionExecutor.

**Interfaces:** Append `ActionKind.UiAutomation`; `UiElementSelector` stores process, automation ID, control type and ordered ancestor criteria. `UiAutomationService.ExecuteAsync(UiAutomationAction action, CancellationToken)` returns `Task`; operations Invoke, SetValue, Select, Toggle, Focus and WaitState. Selector resolution must return exactly one eligible element.

- [ ] Add failing tests against a dedicated test window for relocation/resize, duplicate matches, missing elements, unsupported patterns, disabled controls, password values, Unicode labels and timeout/cancellation.
- [ ] Implement UIA on a dedicated worker with bounded requests and process targeting. Potentially hanging providers run behind a restartable worker boundary; abandoning a request must not leave queued side effects.
- [ ] Exclude password values; report access/integrity restrictions instead of bypassing them.
- [ ] Run Windows checks using isolated fixtures; append enum values only and rerun legacy persistence checks.

### Task 5: UIA picker and reviewed recording

**Files:** Create `src/Aivana.Desktop/MainWindow.UiAutomation.cs`, `src/Aivana.Windows/UiAutomationRecorder.cs`; modify action catalog, editor and recorder integration; create UIA WPF smoke checks.

**Interfaces:** Picker returns a `UiElementSelector`; recorder returns proposed UIA steps and never writes directly into a saved profile. Test-selector command returns match count without invocation.

- [ ] Add failing checks: picker excludes MAKRIVO and password-value capture; record/cancel leaves existing profile unchanged; ambiguous preview reports match count; accepting recorded steps creates an editable draft.
- [ ] Implement element highlight/picker, operation-specific parameters, selector preview and recorder review window. Retain existing shortcut recorder behavior.
- [ ] Add catalog entries and palette access; validate editor errors before save.
- [ ] Run Windows and WPF smoke checks on the fixture; inspect picker/editor screenshots.

### Task 6: Isolated plugin host and protocol

**Files:** Create `src/Aivana.Core/Plugins/PluginContracts.cs`, `src/Aivana.Windows/Plugins/PluginProcessClient.cs`, `src/Aivana.PluginHost/Aivana.PluginHost.csproj`, `src/Aivana.PluginHost/Program.cs`, `tests/Aivana.Tests/PluginProtocolChecks.cs`; modify solution and action dispatch.

**Interfaces:** Version-1 manifest specifies package ID/version, entry point, parameter schemas, actions, state providers and capability declarations. IPC messages contain protocol version, unique request ID, method, JSON parameters and typed result. `PluginProcessClient.InvokeAsync(string actionId, JsonElement parameters, CancellationToken)` returns a typed result. Append `ActionKind.Plugin`.

- [ ] Add failing tests for incompatible protocol, malformed/oversized (>1 MiB) message, duplicate request ID, cancellation, crash, nonresponsive process and missing action.
- [ ] Implement same-user named-pipe IPC with 10-second handshake and configurable execution timeout capped at 60 seconds. Terminate unresponsive worker; do not automatically replay an in-flight action after restart.
- [ ] Enforce schemas and permitted controller services. Explain executable-plugin trust; process isolation is not an OS sandbox.
- [ ] Run core tests and integration tests against real test plugin processes.

### Task 7: Plugin packages and example integration

**Files:** Create `src/Aivana.Core/Plugins/PluginPackage.cs`, `src/Aivana.Desktop/MainWindow.Plugins.cs`, `samples/Aivana.ExamplePlugin/`, package tests; modify palette/settings entries.

**Interfaces:** Package service exposes `InspectAsync(path)`, `InstallAsync(reviewedPackage, revision)`, `ExportAsync(packageId, destination)`, `DisableAsync(packageId)` and `UninstallAsync(packageId)`. Reviewed package records hashes, dependencies, version and declared capabilities.

- [ ] Add failing tests for zip traversal, absolute paths, symlink/reparse escape, duplicate entries, >100 MiB expanded data, hash mismatch, downgrade, stale review and disabled imports.
- [ ] Implement preview and explicit enablement, dependency errors, staged installation and upgrade rollback; unload worker before removal.
- [ ] Deliver a real example plugin returning a configurable countdown action and live state through IPC, including cancellation; package/export/install/uninstall through UI.
- [ ] Run package/host tests and WPF end-to-end checks. Verify no imported executable runs before enablement.

### Task 8: HTTPS mobile service and pairing

**Files:** Create `src/Aivana.MobileHost/` ASP.NET Core project, `src/Aivana.Core/MobilePanelContracts.cs`, `src/Aivana.Windows/MobilePanelClient.cs`, integration tests; modify solution and controller.

**Interfaces:** Mobile service talks to controller through same-user IPC. Routes: pairing, allowed profiles/actions, authenticated action invocation, cancellation and WebSocket status. Invocation includes unique command ID; controller owns bounded deduplication. Credentials are revocable and stored via Windows protection.

- [ ] Add failing tests for unpaired enumeration/execution, expired code, reused code, revoked token, invalid origin, duplicate command, oversized frame and flood limits.
- [ ] Implement HTTPS-only listener, user-selected interfaces, local certificate provisioning/trust instructions, one-time 6-digit pairing codes expiring after 120 seconds, max five attempts per code and token revocation. Store hashed random tokens; never log credentials.
- [ ] Implement allowlisted stable action IDs and existing queue routing; refuse arbitrary shell, paths and package-install commands. Configure explicit per-client rate limits (10 commands/sec, 30 burst) and 64 KiB message limit.
- [ ] Verify loopback integration tests without adding firewall rules or enabling LAN access; test shutdown and token persistence.

### Task 9: Mobile panel and desktop service controls

**Files:** Create `src/Aivana.MobileHost/wwwroot/` responsive panel and `src/Aivana.Desktop/MainWindow.MobilePanel.cs`; add browser/mobile verification scripts and WPF smoke coverage.

**Interfaces:** Consume Task 8 routes only. Expose touch action grid, profile picker, live values/progress, cancel, connection status and pairing. Desktop controls service start/stop, address/certificate instructions, allowed profiles and paired-client revocation.

- [ ] Add failing integration checks for auth flow, stale/missing action IDs, reconnect without action replay, revoked client and service stop.
- [ ] Implement accessible responsive UI using local assets, no third-party scripts or internet dependencies. Tokens never appear in URLs.
- [ ] Test browser layout at 360/768/1280 CSS px; inspect actual screenshots. Verify with a real phone/tablet only after user explicitly enables LAN service.
- [ ] Document and report mobile hardware verification separately from browser emulation.

### Task 10: Device capabilities and ordinary-key observation

**Files:** Create `src/Aivana.Core/KeyboardCapabilities.cs`, `src/Aivana.Windows/KeyboardAdapterRegistry.cs`, `src/Aivana.Desktop/MainWindow.KeyboardAdapters.cs`; modify RawKeyboardBindingService, KeyboardBinding, validator and device diagnosis; add tests.

**Interfaces:** `IKeyboardAdapter` exposes capability snapshots, stable device identity and physical key edges (scan code, extended flags, press/release). Modes distinguish observation, mapped standard input, native SDK and driver-exclusive. Existing virtual-key special bindings retain their representation and migration behavior.

- [ ] Add failing tests for old bindings, Enter distinctions, language/layout independence, repeated downs, unplug while held, duplicate-model ambiguity and injected-input rejection.
- [ ] Extend observation to ordinary physical keys without suppressing them. Add explicit capture and reconnect identity handling.
- [ ] Show actual adapter capabilities and hardware-untested status in device UI; never infer Fn/dial support from brand names.
- [ ] Run core/Windows/WPF checks with simulated reports and read-only device enumeration.

### Task 11: Manufacturer adapters

**Files:** Create separate files under `src/Aivana.Windows/KeyboardAdapters/` for LogitechG, RazerChroma, SteelSeriesGameSense, WootingAnalog and generic QmkVia guidance; modify registry/UI and add adapter protocol checks.

**Interfaces:** Consume Task 10 adapter contracts. Native libraries load only from validated app/vendor locations; SDK absence is an unavailable capability. Lighting and input capabilities are separate.

- [ ] Verify current official SDK headers, redistribution terms, software dependencies and supported models before implementing each adapter; record sources and exact versions.
- [ ] Add failing tests for SDK absent/wrong architecture, event press/release/repeat, disconnect, malformed response and unsupported device. Use injectable native-call abstractions for deterministic checks.
- [ ] Implement Logitech G-key input and supported lighting separately; Razer Chroma lighting plus Synapse standard-key mapping wizard; SteelSeries GameSense state/lighting; Wooting analog polling with configurable thresholds/hysteresis; QMK/VIA standard-key setup guidance. Do not claim proprietary input from lighting APIs.
- [ ] Validate compatible real devices where available. Mark others implemented/hardware-untested, with model/firmware evidence; missing usable SDK is a disclosed external gate.

### Task 12: Exclusive keyboard filter backend

**Files:** Create `src/Aivana.Windows/KeyboardFilter/`, installer source under `tools/keyboard-filter/`, backend integration tests and deployment documentation. A dedicated signed-driver project is required if no suitable licensed backend passes evaluation.

**Interfaces:** Backend reports installed/compatible/signed state; exposes device-specific capture start/stop, key edges and emergency release. A separate watchdog restores pass-through on controller loss. The main keyboard never becomes exclusive by default.

- [ ] Evaluate candidate driver licensing, signed binary provenance and current Windows 11/Secure Boot compatibility. Record a go/no-go decision. No driver installation during ordinary tests.
- [ ] Write contract tests for filter target selection, fail-open, heartbeat expiry, loss of IPC, reconnect ambiguity, pause/cancel, held-key release and disposal.
- [ ] Implement backend, explicit selection/activation UI, emergency release and installer/uninstaller. Do not substitute low-level-hook/Raw Input timing heuristics.
- [ ] Validate in a recoverable test machine with two keyboards: selected key suppressed only on selected device; main typing unaffected; crash/suspend/uninstall restores input.
- [ ] Gate production completion on actual signed/licensed backend and physical tests. If external signing/licensing is unavailable, keep task incomplete with precise dependency evidence.

### Task 13: Integrated verification, translations and release package

**Files:** Add `docs/testing/2026-10-12-six-extensions.md`; update relevant DE/EN/FR/IT documentation, localization catalogs and smoke check entry points. Preserve other documentation edits.

- [ ] Integrate all entry points and snapshots; complete translations using existing 27-language catalog workflow and disclose unreviewed translations.
- [ ] Run `dotnet build Aivana.slnx -c Release`, `dotnet run --project tests/Aivana.Tests -c Release`, `dotnet run --project tests/Aivana.Windows.Tests -c Release`, and `dotnet run --project src/Aivana.Desktop -c Release -- --smoke-test`.
- [ ] Inspect desktop/mobile screenshots and verify 100/150/200 percent Windows scaling, configuration round trips, plugin lifecycle, network revocation and real keyboard reconnect.
- [ ] Publish into a new artifact directory with `dotnet publish src/Aivana.Desktop -c Release -r win-x64 --self-contained true -o artifacts/six-extensions/win-x64`; include companion service/host binaries and license notices. Do not overwrite the installed app.
- [ ] Report completed tests, hardware-untested adapters and external release gates. All six deliverables must meet their acceptance criteria before calling the entire request complete.

## Plan self-review

All six spec sections map to Tasks 1–12; Task 13 covers localization, integrated verification and release. Readiness reporting, execution serialization, legacy configuration, secrets, package trust and physical-device ambiguity have explicit owners and tests. External driver/SDK/hardware gates remain incomplete until proven, rather than counted as software success.

## Execution choice for user review

Recommended: native execution in this chat, task by task, because most modules share contracts and the current workspace contains substantial existing edits. Delegated execution is available if the user chooses it. Review this plan and select the execution method before implementation, as required by the writing-plans workflow.
