<!-- generated-doc locale=en-US source=docs/superpowers/specs/2026-10-08-windows-controller-design.md sha256=f36d496c6af5190889e9a31487a8fc6afdeba5af18facdd600639ede5ddbd169 -->

> Machine-assisted translation. Structure, code and links are checked; native-speaker and legal review are not implied. Historical reports retain their original dates and scope. [Original source](../../../superpowers/specs/2026-10-08-windows-controller-design.md)

# MAKRIVO – Draft

[English (US)](2026-10-08-windows-controller-design.md) | [Deutsch](2026-10-08-windows-controller-design.md) | [Français](2026-10-08-windows-controller-design.md) | [Italiano](2026-10-08-windows-controller-design.md)

Status: 8. October 2026. Status: for review before implementation.

## Goal and agreed scope

A standalone Windows app turns a normal PC keyboard into a freely configurable action panel similar to a stream deck. The user confirmed all suggested action types, macros, device detection, profiles and a good GUI. Connected Corsair and other keyboards are recognized; additional manufacturer functions are only offered if they are actually supported.

Assumptions: German interface, local use without an account, Windows 11 x64 as the first target, no driver installation. The keyboard remains usable for normal typing. By default, global mappings use keyboard shortcuts with modifiers. Individual letter keys are not globally reserved by default.

## Technical decision

Native C#/.NET-10-App with WPF and MVVM. The stable .NET-10-SDK is available in the workspace. WPF allows a customized desktop interface and direct Windows integration. Alternatives would be a web interface with a separate Windows service or a cross-platform desktop app; both bring additional runtime and communication complexity to this Windows project. The native variant is therefore the chosen basis.

Division: Desktop project for views and tray, core project for configuration and execution logic, Windows project for hotkeys, input simulation and devices, test project for platform-independent rules. Manufacturer adapters depend on a fixed capability interface and are independent of the standard actions.

## surface

Dark design with calm anthracite-colored surfaces, easy-to-read font and turquoise accents. Links navigation for dashboard, profiles, devices and settings. Active profile, search and switch to pause all hotkeys at the top. The interface remains usable at 100–200 percent Windows scaling.

The dashboard includes a customizable tile grid. Each tile shows icon, name, hotkey and execution status. Click performs the same action as the hotkey. An editor opens on the right with action type, parameters, hotkey recording, symbol/color, activation and test button. New, Duplicate, Delete and Reorder are available. A keyboard view shows assignments schematically for the standard German layout; it does not claim the exact geometry of the detected device.

Profiles can be created, copied, renamed and activated manually. A process name is optionally assigned to a profile. A suitable foreground program activates its profile, otherwise the global standard profile applies. If there are several matching profiles, an explicitly visible order applies. The editor explains hotkey conflicts directly at the assignment.

## Hotkeys and inputs

Windows RegisterHotKey handles normal global combinations with key repetition suppressed. Only the active profile is registered. Reserved or already occupied combinations appear as errors; the app does not report it as successfully activated. Recording in the editor does not trigger any actions.

Before simulated inputs, the triggering modifier keys are released or they are waited for to be physically released. Unicode text and key combinations are sent using SendInput. Every key you press will be released again even if you cancel. Actions run outside the UI thread; Execution and profile changes must not block the surface.

## Actions and macros

Action types: Start program with arguments and working directory, open file/folder via Windows, open HTTP/HTTPS website, enter Unicode text, send key combination, media play/pause/next/previous/stop, volume up/down/mute and switch profile. Parameters are validated before saving and again before execution. Programs are started directly; Arguments are not implicitly interpreted as shell commands.

Macros contain an ordered list of named actions and explicit pause steps. Steps can be added, edited and moved. Repeats are finally configurable (1–100); Infinite loops are not part of the first version. A running macro can be ended via the interface or your own cancel hotkey. Repeated triggering of the same current assignment does not start a parallel run. Different macros with input simulation are serialized so that their keypresses do not mix. If a step error occurs, the macro stops and displays the affected step.

## Devices and manufacturers

Device inventory via Windows Raw Input and additional device information: display name, device path, manufacturer/product identifier if available, connection status and detected capabilities. Plugging/unplugging updates the list without restarting. A physical device can have multiple HID entries; These are grouped according to reliable identifiers, otherwise they are kept visibly as separate entries.

Normal hotkeys apply across keyboards. The device view provides information and capabilities; a separate keyboard with exclusively intercepted keys is not part of this agreed mode.

Corsair detection also works without iCUE. An optional iCUE SDK adapter provides connection status and supported lighting actions when SDK, iCUE and compatible device are present. The UI differentiates recognized, SDK available, connected and actually controllable. The SDK primarily documents LED control; Proprietary G-keys are only supported like normal hotkeys if Windows receives appropriate input. There is no blanket promise for all special keys.

Other manufacturers initially receive generic detection and standard actions. The adapter interface allows later integrations; unsupported skills are honestly displayed. Hardware tests for non-existent models are not reported as passing.

## Storage and everyday life

Versioned JSON configuration under the local app data directory. Atomic save with last valid backup; incorrect files are not silently overwritten. Import checks format, version, IDs and parameters and shows a preview before applying. Imported actions can only be executed after explicit activation. Export contains profiles and assignments.

Tray menu: Open, profile selection, pause hotkeys, cancel macros, exit. Close window minimizes to tray by default and explains this the first time. Autostart is optional and off by default. Only one instance manages hotkeys. Exit registers hotkeys, aborts executions, and releases resources.

Local activity view with limited history for success, failure and cancellation. No log of all keystrokes. Text action content is not spelled out in error messages. No cloud service and no telemetry.

## Error handling and limits

Missing programs, invalid URLs, occupied hotkeys, damaged profiles and unreachable manufacturer SDK receive specific German messages. An adapter failure does not affect standard actions. Entries to higher privileged applications may fail due to Windows permission limits; Errors are explained, the app does not require administrator rights by default.

## Testing and acceptance

Automated checks for profile priority, conflict detection, configuration import, atomic storage, macro ordering, retry, cancellation, and error propagation. Windows smoke tests for real hotkeys, text and keyboard shortcuts, media actions, device inventory, tray and clean exit. No tests that accidentally launch arbitrary programs or websites.

GUI check: Create and edit assignment, recording without execution, click/hotkey with the same effect, macro cancellation, conflict message, profile change, device view, import/export and scaling. Manufacturer-specific hardware testing is documented separately with the actual device.

Delivery: Source code, reproducible build/startup instructions, German user manual and local Windows-x64-Publish-Artefakt. Success means an executable app with all generic features; Corsair capabilities are only marked as available with proven SDK/device support.

## Technical sources

- Microsoft Keyboard Input / RegisterHotKey: https://learn.microsoft.com/en-us/windows/win32/inputdev/about-keyboard-input
- Microsoft Raw Input: https://learn.microsoft.com/en-us/windows/win32/inputdev/about-raw-input
- Microsoft WPF Input: https://learn.microsoft.com/dotnet/desktop/wpf/advanced/input-overview
- Official Corsair iCUE SDK: https://github.com/CorsairOfficial/cue-sdk/blob/master/README.md

## Next step

After reviewing this draft: create a concrete implementation plan and submit it for selection of implementation. There is no Git repository in the empty working folder yet; This document has therefore not yet been committed.
