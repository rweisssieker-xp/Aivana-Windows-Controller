<!-- generated-doc locale=en-US source=docs/VERIFICATION.md sha256=f19afb9e7b5ffac90c83167416ab3af0964a4481dd16b7357f8585480c6aae65 -->

> Machine-assisted translation. Structure, code and links are checked; native-speaker and legal review are not implied. Historical reports retain their original dates and scope. [Original source](../VERIFICATION.md)

# Exam – 8. October 2026

[English (US)](VERIFICATION.md) | [Deutsch](../de-DE/VERIFICATION.md) | [Français](../fr-FR/VERIFICATION.md) | [Italiano](../it-IT/VERIFICATION.md)

The application was built on Windows x64 with the stable .NET SDK 10.0.202 and released as a standalone Windows-x64-Ausgabe.

## Checked automatically

- Overall build: no warnings and no errors.
- 19 Core Behavior Tests: Default Configuration, Validation, Null/Duplicate Data, Profile Priority, Import/Export and Backup Restore, Macro Order and Retry, Abort, Double Trigger, Serialization, Immutable Execution Snapshot, and Step Failure.
- Windows check: hotkey normalization and aliases, native structure sizes, real keyboard device list, secure registration and re-sharing, synthetic WM_HOTKEY for tile and cancel, disabled tile registration during input simulation.
- The standalone EXE was started with `--smoke-test`: all five WPF views are rendered, tiles can be created/copied/deleted, drafts are retained during refresh, configuration is saved and reloaded. This test uses an isolated temporary configuration directory and does not perform any text or media actions.
- Dashboard, editor and compact representation (900 × 720) were visually inspected as real WPF renderings. Draft saving, discarding, tile/macro order, German hotkey aliases, individual macro cancellations and step progress are checked.

## Hardware dependent limits

The last local run detected four Windows keyboard interfaces. This number describes HID/Windows interfaces, not necessarily four physical keyboards.

The official iCUE SDK is installed and is located next to the EXE. The live test connects to iCUE and detects a Corsair keyboard with model and LED information. Lighting changes were not triggered automatically; Model-dependent special keys and other manufacturers are not hardware tested. Generic hotkeys apply across keyboards.

Text/media input was not injected into other open user agents. The native data structures and error/release logic were checked; The behavior in a specific target program depends, among other things, on its authorization level.

## Repeat

```powershell
dotnet build Aivana.slnx -c Release
dotnet run --project tests/Aivana.Tests -c Release
dotnet run --project tests/Aivana.Windows.Tests -c Release
dotnet publish src/Aivana.Desktop -c Release -r win-x64 --self-contained true -o artifacts/win-x64
& ./artifacts/win-x64/MAKRIVO.exe --smoke-test
```

The GUI test writes result and screenshot to `smoke` next to the tested EXE. It is intended as a diagnostic mode and does not launch a persistent background instance upon completion.


## Advanced controls

Additionally checked: profile conditions with process and window title, variable replacement without recursive expansion, macro preview without execution, new action validation and backwards compatibility of old JSON files. Native checks cover the command palette with registration, conflicts, WM_HOTKEY dispatch and release, as well as pure monitor splitting and on-demand clipboard read access. The WPF test opens and renders the actual palette, checks undo/redo and favorites, checks clean drafts after rendering, and completes the process successfully.

Windows locking, clipboard changes, and window movement were not applied to user agents during testing. Post window actions Windows requests asynchronously; The specific target program can have its own minimum sizes or authorization limits. Profile lock and execution statistics apply to the current session.


Extension exam from 12.10.2026: 30 core exams passed; Windows adapter including release during input injection, read-only monitor detection and OBS status mock passed. WPF checks flow commit/cancel, recorder start/stop, profile exchange preview, gestures and symbol resources. Localization: 27 catalogs each with 967 keys; Documentation: 84 pages in four languages. Proprietary buttons other than Corsair have not been tested on hardware; Generic support includes device-specific standard events F13–F24 and media keys. Cloud transport occurs via a selected synced folder; Imports are checked and remain disabled.
