<!-- generated-doc locale=en-US source=docs/superpowers/plans/2026-10-12-controller-usps.md sha256=b7af0d36209e04b458ce64a5db78ab7e552a84c5e9b450ea0bd97179a4eb7f68 -->

> Machine-assisted translation. Structure, code and links are checked; native-speaker and legal review are not implied. Historical reports retain their original dates and scope. [Original source](../../../superpowers/plans/2026-10-12-controller-usps.md)

# Controller extension implementation plan

**Goal:** Implement the requested monitor, gesture, workflow, recording, device diagnosis, portable profile exchange and tray improvements.

**Architecture:** Extend the existing Core/Windows/Desktop layers. Pure state machines and portable configuration logic live in Core, native integrations in Windows and modal draft editors in Desktop. Preserve existing dirty workspace edits and deployed user configuration.

**Spec:** ../specs/2026-10-12-controller-usps-design.md

- [x] Monitor service, target persistence and asynchronous live status; test unavailable IDs and cancellation.
- [x] Gesture state machine and held layers; test edges, repeat, reset and existing immediate bindings.
- [x] Workflow conditions, forward branches and error policy; test snapshots and cancellation.
- [x] Explicit shortcut recording with draft review; test filtering and hook disposal.
- [x] Shared-folder profile exchange with revision checks and previews; test conflicts and portable mappings.
- [x] Generic per-device standard special keys and diagnosis; test parsing, duplicate mappings and pause behavior.
- [x] Tray/window/executable icon resources and paused appearance; verify resources in published executable.
- [x] Integrate all palette/editor entry points and localized text; run Core, Windows and WPF checks.
- [x] Publish to a separate staging directory, preserve user settings and restart the updated app.
