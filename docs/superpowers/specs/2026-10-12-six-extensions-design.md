# MAKRIVO: six product extensions and keyboard adapters

Date: 2026-10-12
Status: proposed design for user review; implementation has not started.

## Goal
Deliver all six requested extensions in the existing Windows/WPF application, preserving existing profiles, G1/G2 assignments, local operation and account-free usage. Expand keyboard integration to Razer and Logitech, with a reusable capability-based architecture for other manufacturers. A manufacturer name or keyboard layout is never evidence that proprietary controls are supported.

## Architecture decision
Extend Aivana.Core with serializable contracts and validation, Aivana.Windows with Windows and vendor adapters, and Aivana.Desktop with transactional editors and status views. Use separate companion projects for isolated plugin execution and the local mobile panel service. Avoid a rewrite or a single privileged process hosting every extension. Existing configuration must load unchanged; append action kinds rather than reorder their persisted enum values.

Alternatives considered: a monolithic implementation would entangle hardware, network and workflow execution; replacing WPF would add unrelated migration work. Modular extensions are recommended.

## 1. Independent keyboard macro pad
Store bindings by device identity plus physical scan code and extended-key flags. Add explicit observe and exclusive modes, device discovery, key capture, repeat suppression, gestures, held layers and reconnect diagnostics. Preserve main/numpad Enter distinctions and test German/US layouts. Bindings target a selected physical keyboard; ambiguous reconnect identities require re-selection rather than controlling another device.

Raw Input provides observation only. Exclusive per-device suppression requires an actual keyboard filter backend; never correlate global low-level hooks with Raw Input and claim reliable device-specific suppression. Provide a watchdog, emergency release, fail-open behavior and installer/uninstaller. Do not capture secure desktop input. Driver installation and exclusive-mode activation are explicit separate user actions.

Driver gate: select a supported, properly licensed and signed backend compatible with the target Windows 11 configurations, or develop/sign a dedicated filter. Interception is a candidate to evaluate, not a committed production dependency: its official README describes testing through Windows 10 and distinct commercial licensing. Until this gate is met, exclusive mode must be shown as unavailable; the feature cannot be declared complete.

Acceptance: with two physical keyboards, the same key executes a macro and produces no normal application input only on the selected exclusive device; other keyboard input remains unaffected. Verify disconnect, crash, cancellation, suspend/resume and driver removal on actual hardware.

## 2. Guided workspace setup
Add a wizard for workplace, meeting, streaming, research and project switching. Discover installed applications, audio devices, monitors and OBS scenes. Users choose targets and bindings; preview the resulting steps and validate missing requirements. Save only after review, with no actions running during setup. Generated profiles remain editable in existing editors.

Acceptance: create a working profile without manually editing executable paths or device IDs; cancellation leaves settings unchanged; missing software has an actionable explanation.

## 3. Verified execution
Introduce preflight checks, typed per-step results and optional postconditions. Distinguish requested, completed, verified, failed, timed out, cancelled and unverifiable states. Verify audio/microphone, brightness, process/window readiness and OBS state with bounded asynchronous reads. Shortcut/text delivery is not proof that the target application accepted the command. External side effects are not universally reversible; offer compensation only where implemented.

Acceptance: disconnected explicit targets never silently fall back; wrong observed state never reports verified success; cancellation stops polling; failures identify the step and recovery action. Execution history omits text payloads and secrets.

## 4. Plugins and shareable packages
Versioned JSON manifests declare actions, parameter schemas, state providers, dependencies and capabilities. Run executable plugins in a separate process over bounded local IPC with handshake, deadlines and cancellation. Reject malformed messages and incompatible versions; handle crashes without terminating the controller. Capability declarations are disclosure and mediation, not a claim of OS sandboxing.

Provide package export/import preview, content hashes, path-traversal protection, size limits, version/dependency checks, explicit enablement and upgrade rollback. Imported executable plugins must not execute automatically. Include a working example plugin with an end-to-end install/action/state/uninstall flow. No public marketplace service is required for this release.

Acceptance: create, export, install and execute a real example integration; demonstrate timeout, crash isolation, dependency failure, traversal rejection and disabled import.

## 5. Windows UI Automation
Add an element picker and bounded actions for invoke, value, selection, toggle, focus and waiting for an element/state. Persist selectors using process, automation ID, control type and ancestor context; avoid coordinate-only recording. Show selector match count and test selectors read-only. Password fields are excluded from recording. Handle unsupported controls and Windows integrity-level restrictions explicitly.

Extend recording with reviewable UIA actions, retaining current shortcut recording. Ambiguous selectors fail instead of clicking an arbitrary control. A normal browser accessibility tree is not a promise of a full DOM automation engine.

Acceptance: automate a dedicated test application with stable selectors across position/size changes; test ambiguity, missing elements, disabled controls, timeouts and cancellation.

## 6. Local phone/tablet panel
Serve a responsive panel from an opt-in LAN service. Pair explicitly using a short-lived one-time code and issue revocable credentials protected locally. Use HTTPS with documented local certificate trust/pinning; no fallback sending credentials in plaintext. Default to disabled and restrict configured interfaces. Origin validation, authenticated commands, rate limits and bounded messages are mandatory.

Expose allowed profiles/actions, live state, progress and cancel; use stable IDs and the same execution queue as desktop and physical keys. Do not expose arbitrary commands, executable paths, secrets or plugin installation. Provide connection, pairing and revocation screens. No cloud account or internet relay is included.

Acceptance: operate from a real phone/tablet on the same LAN; unpaired clients cannot list or run actions; revoked tokens fail; duplicate/replayed commands do not double-execute; disconnect does not silently retry actions.

## Keyboard manufacturer scope

| Adapter/family | Planned integration | Explicit boundary |
| --- | --- | --- |
| Corsair | Preserve current iCUE G-key input and RGB integration | Model/software capability detection remains required |
| Logitech G | G-key SDK adapter with press/release and runtime availability checks; lighting through its separate SDK where supported | Do not equate G HUB detection with supported G-key API on every model |
| Razer | Chroma lighting adapter and guided Synapse-to-standard-key mapping, plus generic HID input | Chroma is a lighting API; do not label it native proprietary macro-key input |
| SteelSeries | GameSense state/lighting adapter plus generic HID input | GameSense event submission is not a general proprietary key capture API |
| Wooting | Analog SDK adapter for supported analog input, plus normal HID keys | Capabilities and SDK distribution terms must be verified per supported model |
| Keychron/QMK/VIA | Standard HID keys and firmware-mapping instructions; known layouts only with evidence | VIA configuration support is not a universal runtime macro-key API |
| Other standard HID keyboards | Per-device physical key discovery and mapping; exclusive mode only with the validated filter backend | No fabricated model-specific Fn, dial or proprietary key support |

Each device shows detected, observed, mapped through vendor software, native SDK, driver-exclusive, unavailable, or hardware-untested status as applicable. Preserve capability evidence with adapter versions and tested model/firmware identifiers.

## Delivery order
1. Configuration compatibility, capability contracts and verified execution.
2. Guided setup using real discovery and existing action executors.
3. UI Automation and its recorder/test fixture.
4. Plugin host, package flow and example integration.
5. Paired LAN panel and mobile verification.
6. Expanded vendor adapters and exclusive keyboard backend integration, with actual hardware validation.

All six remain required. Driver signing/licensing, inaccessible SDKs or absent test hardware are release gates, not reasons to substitute mock behavior and call the work complete.

## Verification and release criteria
Run existing core, Windows and WPF checks and add focused integration tests for each new boundary. Verify JSON round trips and legacy profiles; test imported executable content, network authentication, cancellation, plugin crashes and device reconnect. Inspect desktop and mobile layouts. Validate high Windows scaling separately. Build release artifacts only after passing checks; distinguish implemented/tested, implemented/hardware-untested and externally blocked functionality in release notes. Do not replace the installed application, install drivers, alter real keyboard mappings or open network access as an incidental test.

## Evidence
- Microsoft Raw Input semantics: https://learn.microsoft.com/en-us/windows/win32/api/winuser/ns-winuser-rawinputdevice
- Logitech G developer SDKs: https://www.logitechg.com/en-us/programs/developer-alliance
- Razer Chroma SDK FAQ: https://developer.razer.com/works-with-chroma/chroma-faq/
- Interception repository and dependency conditions: https://github.com/oblitum/Interception
- Existing repository manufacturer inventory: docs/de-DE/research/tastaturhersteller-sonderfunktionen.md

## Review decisions
The user is asked to approve this written design before the implementation plan. The principal external dependency is a production-ready exclusive-input driver backend. Recommended scope retains this as a gated deliverable rather than silently reducing it to Raw Input observation.
