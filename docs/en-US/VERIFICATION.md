<!-- generated-doc locale=en-US source=docs/VERIFICATION.md sha256=f19afb9e7b5ffac90c83167416ab3af0964a4481dd16b7357f8585480c6aae65 -->

> Machine-assisted translation. Structure, code and links are checked; native-speaker and legal review are not implied. Historical reports retain their original dates and scope. [Original source](../VERIFICATION.md)

# Verification – 8. October 2026

[English (US)](VERIFICATION.md) | [Deutsch](../de-DE/VERIFICATION.md) | [Français](../fr-FR/VERIFICATION.md) | [Italiano](../it-IT/VERIFICATION.md)

The application was built on Windows x64 with the stable .NET SDK 10.0.202 and published as a self-contained Windows-x64 release.

## Checked automatically

- Overall build: no warnings and no errors.
- 19 core behavior tests: default configuration, validation, null/duplicate data, profile priority, import/export and backup restoration, macro order and repetition, cancellation, duplicate triggering, serialization, immutable execution snapshot, and step errors.
- Windows verification: hotkey normalization and aliases, native structure sizes, real keyboard device list, safe registration and release, synthetic WM_HOTKEY for tiles and cancellation, and disabled tile registration during input simulation.
- The standalone EXE was started with `--smoke-test`: all five WPF views are rendered, tiles can be created/copied/deleted, drafts are retained during refresh, configuration is saved and reloaded. This test uses an isolated temporary configuration directory and does not perform any text or media actions.
- Dashboard, editor and compact representation (900 × 720) were visually inspected as real WPF renderings. Draft saving, discarding, tile/macro order, German hotkey aliases, individual macro cancellations and step progress are checked.

## Hardware dependent limits

The last local run detected four Windows keyboard interfaces. This number describes HID/Windows interfaces, not necessarily four physical keyboards.

The official iCUE SDK is installed and is located next to the EXE. The live test connects to iCUE and detects a Corsair keyboard with model and LED information. Lighting changes were not triggered automatically; Model-dependent special keys and other manufacturers are not hardware tested. Generic hotkeys apply across keyboards.

Text/media input was not injected into other open user applications. Native data structures and error/release logic were checked; behavior in a specific target application depends, among other things, on its privilege level.

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

Windows locking, clipboard changes, and window movements were not applied to user applications during testing. Window actions post Windows requests asynchronously; the specific target application may have its own minimum sizes or privilege limits. Profile locking and execution statistics apply to the current session.


Extension verification from 12.10.2026: 30 core checks passed; Windows adapters passed, including key release during input injection, read-only monitor detection, and the OBS status mock. WPF checks applying/canceling workflows, recorder start/stop, profile exchange preview, gestures, and icon resources. Localization: 27 catalogs with 967 keys each; documentation: 84 pages in four languages. Proprietary keys from manufacturers other than Corsair were not tested on hardware; generic support includes device-specific standard events F13–F24 and media keys. Cloud transport uses a selected synchronized folder; imports are checked and remain disabled.
