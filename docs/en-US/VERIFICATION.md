# Verification — October 8, 2026

[English (US)](../en-US/VERIFICATION.md) | [Deutsch](../de-DE/VERIFICATION.md) | [Français](../fr-FR/VERIFICATION.md) | [Italiano](../it-IT/VERIFICATION.md)

The application was built on Windows x64 with the stable .NET SDK 10.0.202 and published as a standalone Windows x64 build.

## Automated checks

- Full build: no warnings or errors.
- 19 Core behavior tests: default configuration, validation, null/duplicate data, profile precedence, import/export and backup restoration, macro order and repetition, cancellation, duplicate triggering, serialization, immutable execution snapshot, and step failure.
- Windows checks: hotkey normalization and aliases, native structure sizes, real keyboard device list, safe registration and unregistration, synthetic WM_HOTKEY for tile and cancellation, and disabled tile registration during input simulation.
- The standalone EXE was started with `--smoke-test`: all five WPF views render, tiles can be created/copied/deleted, drafts survive refresh, configuration is saved and reloaded. This test uses an isolated temporary configuration directory and performs no text or media actions.
- Dashboard, editor, and compact view (900 × 720) were visually inspected as actual WPF renderings. Draft saving, discarding, tile/macro ordering, German hotkey aliases, individual macro cancellation, and step progress were checked.

## Hardware-dependent limitations

The latest local run detected four Windows keyboard interfaces. This number describes HID/Windows interfaces, not necessarily four physical keyboards.

The official iCUE SDK is installed next to the EXE. The live test connects to iCUE and detects a Corsair keyboard with model and LED information. Lighting changes were not triggered automatically; model-specific special keys and other manufacturers have not been hardware-tested. Generic hotkeys apply across keyboards.

Text/media input was not injected into other open user programs. Native data structures and error/release logic were checked; behavior in a specific target program also depends on its permission level, among other factors.

## Repeat

```powershell
dotnet build Aivana.slnx -c Release
dotnet run --project tests/Aivana.Tests -c Release
dotnet run --project tests/Aivana.Windows.Tests -c Release
dotnet publish src/Aivana.Desktop -c Release -r win-x64 --self-contained true -o artifacts/win-x64
& ./artifacts/win-x64/Aivana.Controller.exe --smoke-test
```

The GUI test writes its result and screenshot to `smoke` next to the tested EXE. It is intended as a diagnostic mode and does not start a persistent background instance when it finishes.

## Extended control

Also checked: profile conditions using process and window title, variable substitution without recursive expansion, macro preview without execution, new action validation, and backward compatibility with old JSON files. Native checks cover the command palette registration, conflicts, WM_HOTKEY dispatch and release, as well as monitor-only layout and on-demand clipboard reads. The WPF test opens and renders the actual palette, checks undo/redo and favorites, checks clean drafts after rendering, and exits successfully.

Windows locking, clipboard changes, and window movement were not applied to user programs during testing. Window actions post asynchronous Windows requests; the target program may impose its own minimum sizes or permission boundaries. Profile locking and execution statistics apply to the current session.
