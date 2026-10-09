# Controller usability and capability improvement plan

[English (US)](../../../en-US/superpowers/plans/2026-10-08-controller-polish.md) | [Deutsch](../../../de-DE/superpowers/plans/2026-10-08-controller-polish.md) | [Français](../../../fr-FR/superpowers/plans/2026-10-08-controller-polish.md) | [Italiano](../../../it-IT/superpowers/plans/2026-10-08-controller-polish.md)

User request: improve appearance, usability, and functionality; prior authorization requests the maximum useful number of subagents on necessary smaller models and autonomous completion.

## Scope
Preserve existing profiles and the working Corsair SDK integration. Improve the existing WPF application rather than creating a separate prototype.

- [x] Shell/dashboard worker: responsive layout, active navigation, readable controls, compact activity/status, drag-and-drop tile reordering, helpful empty states and templates. Own MainWindow.xaml, MainWindow.xaml.cs, and MainWindow.Dashboard.cs.
- [x] Editor worker: MainWindow.Editor.cs and optional editor-only partial files. Explicit staged edits, Save/Discard, inline validation, dedicated macro list with drag-and-drop, duplicate steps, reliable hotkey capture, and file/URL fields.
- [x] Windows worker: native validation, robust device names and SDK capabilities, bounded modifier waits, and safe injection; own Windows and Windows tests.
- [x] Parent: core execution progress, controller interfaces, configuration preservation, integration testing, actual rendered screenshots, publish/package, and replacement of the running application.

## Interface contract
Controller provides existing APIs plus string LastStatus, bool HasRunningActions, bool IsRunning(Guid), string GetTileStatus(Guid), string ForegroundProcess, bool CorsairAvailable, IReadOnlyList<CorsairKeyboardInfo> CorsairKeyboards, Task TestCorsairColorAsync(string), and void OpenIcue(). Editor exposes bool HasUnsavedChanges, void DiscardDraft(), and bool CommitDraft(); existing field/method names remain available across partial classes.

## Acceptance
No reset of real user configuration; valid staged changes save once, invalid drafts stay editable, and discard restores saved data. Native hotkey aliases are validated without injecting test inputs into unrelated windows. Tile and macro reordering is tested; all views render at desktop sizes; running/failed/complete status is visible; the actual SDK device model is displayed. Build/tests and published smoke pass, and the updated application is launched.
