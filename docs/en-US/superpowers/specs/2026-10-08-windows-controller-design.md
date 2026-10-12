# MAKRIVO — Design

[English (US)](../../../en-US/superpowers/specs/2026-10-08-windows-controller-design.md) | [Deutsch](../../../de-DE/superpowers/specs/2026-10-08-windows-controller-design.md) | [Français](../../../fr-FR/superpowers/specs/2026-10-08-windows-controller-design.md) | [Italiano](../../../it-IT/superpowers/specs/2026-10-08-windows-controller-design.md)

As of: October 8, 2026. Status: ready for review before implementation.

## Goal and agreed scope

A standalone Windows app turns a regular PC keyboard into a freely configurable action panel, similar to a Stream Deck. The user approved all proposed action types, macros, device detection, profiles, and a polished GUI. Connected Corsair and other keyboards are detected; additional manufacturer features are offered only when actually supported.

Assumptions: German UI, local use without an account, Windows 11 x64 as the first target, no driver installation. The keyboard remains usable for ordinary typing. Global bindings use key combinations with modifiers by default. Individual letter keys are not reserved globally by default.

## Technical decision

Native C#/.NET 10 app using WPF and MVVM. The stable .NET 10 SDK is available in the workspace. WPF supports a custom desktop interface and direct Windows integration. Alternatives would be a web interface with a separate Windows service or a cross-platform desktop app; both add runtime and communication complexity for this Windows project. The native option was therefore selected as the foundation.

Structure: Desktop project for views and tray; Core project for configuration and execution logic; Windows project for hotkeys, input simulation, and devices; test project for platform-independent rules. Manufacturer adapters use a fixed capability interface and remain independent of standard actions.

## Interface

Dark design with calm anthracite surfaces, readable type, and a turquoise accent. Navigation on the left for Dashboard, Profiles, Devices, and Settings. At the top: active profile, search, and a switch to pause all hotkeys. The interface remains usable at 100–200 percent Windows scaling.

The dashboard contains a customizable tile grid. Each tile shows an icon, name, hotkey, and execution status. Clicking runs the same action as the hotkey. An editor opens on the right with action type, parameters, hotkey capture, icon/color, activation, and a test button. Create, duplicate, delete, and reorder are available. A keyboard view shows bindings schematically for the standard German layout; it does not claim exact geometry for the detected device.

Profiles can be created, copied, renamed, and activated manually. Optionally, a process name can be associated with a profile. A matching foreground application activates its profile; otherwise, the global default profile applies. When multiple profiles match, an explicitly visible order determines precedence. The editor explains hotkey conflicts beside the binding.

## Hotkeys and input

Windows RegisterHotKey handles ordinary global combinations with key-repeat suppression. Only the active profile is registered. Reserved or already-used combinations appear as errors; the app does not report them as successfully activated. Capturing a hotkey in the editor does not trigger actions.

Before simulated input, the triggering modifier keys are released or the app waits for their physical release. Unicode text and key combinations are sent with SendInput. Every key pressed by the app is also released if execution is cancelled. Actions run outside the UI thread; execution and profile switching must not block the interface.

## Actions and macros

Action types: launch a program with arguments and a working directory; open a file/folder through Windows; open an HTTP/HTTPS webpage; enter Unicode text; send a key combination; media Play/Pause/Next/Previous/Stop; volume Up/Down/Mute; and switch profiles. Parameters are validated before saving and again before execution. Programs are launched directly; arguments are not implicitly interpreted as shell commands.

Macros contain an ordered list of the actions above and explicit pause steps. Steps can be added, edited, and moved. Repetitions are configurable and finite (1–100); infinite loops are outside the first version. A running macro can be stopped through the interface or a dedicated cancel hotkey. Triggering the same running binding again does not start a parallel run. Different macros that simulate input are serialized so their keystrokes do not mix. If a step fails, the macro stops and identifies the affected step.

## Devices and manufacturers

Device inventory through Windows Raw Input and supplementary device information: display name, device path, manufacturer/product identifiers when available, connection status, and detected capabilities. Plugging in or disconnecting a device updates the list without a restart. One physical device may have multiple HID entries; these are grouped by reliable identifiers, or shown as separate entries when reliable identifiers are unavailable.

Ordinary hotkeys apply across keyboards. The device view provides information and capabilities; exclusive capture of keys on a separate keyboard is outside the agreed mode.

Corsair detection works even without iCUE. An optional iCUE SDK adapter provides connection status and supported lighting actions when the SDK, iCUE, and a compatible device are available. The UI distinguishes detected, SDK available, connected, and actually controllable. The SDK mainly documents LED control; proprietary G-keys are supported only as ordinary hotkeys when Windows receives their input. There is no blanket guarantee for all special keys.

Other manufacturers initially receive generic detection and standard actions. The adapter interface allows later integrations; unsupported capabilities are shown honestly. Hardware tests for unavailable models are not reported as passed.

## Storage and everyday use

Versioned JSON configuration under the local app data directory. Atomic saves with the last valid backup; malformed files are not silently overwritten. Import validates format, version, IDs, and parameters, and shows a preview before applying. Imported actions become executable only after explicit activation. Export includes profiles and bindings.

Tray menu: Open, choose profile, pause hotkeys, cancel macros, exit. Closing the window minimizes to the tray by default and explains this the first time. Startup is optional and off by default. Only one instance manages hotkeys. Exit unregisters hotkeys, cancels executions, and releases resources.

Local activity view with a limited history of success, errors, and cancellations. No logging of all keyboard input. Text action contents are not written in error messages. No cloud service or telemetry.

## Error handling and limitations

Missing programs, invalid URLs, occupied hotkeys, damaged profiles, and an unavailable manufacturer SDK receive specific German messages. An adapter failure does not affect standard actions. Input to applications running at a higher privilege level may fail at Windows permission boundaries; the app explains the error and does not request administrator privileges by default.

## Checks and acceptance

Automated checks for profile precedence, conflict detection, configuration import, atomic storage, macro order, repetition, cancellation, and error propagation. Windows smoke tests for real hotkeys, text and key combinations, media actions, device inventory, tray, and clean exit. No tests that might accidentally launch arbitrary programs or webpages.

GUI checks: create and edit a binding, capture without execution, same result from click/hotkey, cancel a macro, conflict message, switch profile, view devices, import/export, and scaling. Manufacturer-specific hardware checks are documented separately using an actually available device.

Delivery: source code, reproducible build/start instructions, German user guide, and a local Windows x64 publish artifact. Success means an executable app with all generic features; Corsair capabilities are marked available only when SDK/device support is confirmed.

## Technical sources

- Microsoft Keyboard Input / RegisterHotKey: https://learn.microsoft.com/en-us/windows/win32/inputdev/about-keyboard-input
- Microsoft Raw Input: https://learn.microsoft.com/en-us/windows/win32/inputdev/about-raw-input
- Microsoft WPF Input: https://learn.microsoft.com/dotnet/desktop/wpf/advanced/input-overview
- Official Corsair iCUE SDK: https://github.com/CorsairOfficial/cue-sdk/blob/master/README.md

## Next step

After reviewing this design: create a concrete implementation plan and present it for selection of the execution approach. The empty working folder does not yet contain a Git repository; this document has therefore not been committed.
