# Keyboard Assistance, Overlay, and Execution Feedback

[English (US)](../../en-US/testing/2026-10-09-usp-release.md) | [Deutsch](../../de-DE/testing/2026-10-09-usp-release.md) | [Français](../../fr-FR/testing/2026-10-09-usp-release.md) | [Italiano](../../it-IT/testing/2026-10-09-usp-release.md)

Implemented the three prioritized improvements in the existing Windows app:

- Special-key wizard: actual iCUE-supported device/key inventory, temporary G-key capture, configuration-success verification, conflict validation, and an unsaved action draft. Capture rollback, queued callback invalidation, and normal-binding restoration cover errors and dialog closure.
- Keyboard overlay: physical reference geometry, active-profile bindings, live profile/edit refresh, and ordinary Windows key highlighting. Hold Ctrl+Alt+F1 or toggle through the palette/keyboard page. Nonactivating, click-through, and absent from the taskbar.
- Execution feedback: typed Waiting/Running/Step/Completed/Cancelled/Failed/Rejected phases, step/repetition progress, and a nonactivating toast. Feedback excludes action content and exception messages. Last-step cancellation cannot report completion. Completion describes execution, not an independent OBS state query.

Verification:

- 24 Core checks passed, including phase ordering, failure/cancellation outcomes, final-step cancellation, listener exception isolation, and exclusion of private action values.
- Release solution build passed without warnings/errors.
- Windows checks passed; the connected K100 reported G1–G6 and native configure/release succeeded. Audio endpoints were inspected without changing their state.
- WPF smoke passed with all reference overlay layouts, wizard/catalog/conflicts/draft persistence, live native capture configuration, callback forwarding, stale callback rejection, and paused-binding restoration. Native callback input was simulated; a physical human G-key press remains a manual check.
- Actual foreground-window invariance was checked for the execution toast. Overlay interaction properties and show/hide were checked; screenshots were rendered for the wizard, overlay, and toast.
- 27 languages contain all 819 canonical strings; offline placeholder/key validation passed.

Snapshots and the full GUI result are in artifacts/usp-win-x64/smoke. Tests use temporary profiles; production settings are backed up and hash-checked during deployment.
