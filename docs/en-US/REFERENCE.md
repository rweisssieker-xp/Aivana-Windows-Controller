<!-- generated-doc locale=en-US source=README.md sha256=eb6f3fe7810ed3e10793e8938e908e151a84c4afbea2f22cba2656bfe4e8dfc3 -->

> Machine-assisted translation. Structure, code and links are checked; native-speaker and legal review are not implied. Historical reports retain their original dates and scope. [Original source](../../README.md)

# MAKRIVO

[Complete documentation in all 27 languages](README.md) · [Full user handbook](HANDBOOK.md)

**By Aivana.** [Current feature inventory and release limitations](FEATURES.md) · [Extension test report](testing/2026-10-12-six-extensions.md) · [Local AI assistant prototype](testing/2026-10-12-local-meeting-assistant.md)

**English (US)** | [Deutsch](../de-DE/REFERENCE.md) | [Français](../fr-FR/REFERENCE.md) | [Italiano](../it-IT/REFERENCE.md)

A local Windows hotkey controller with Stream Deck-style tiles, profiles, macros, and keyboard detection. The interface supports 27 languages. No account or cloud connection is required for everyday use.

[Documentation](README.md) · [Verification](VERIFICATION.md) · [Feature test report](testing/2026-10-09-usp-release.md)

## Getting started

After publishing, the application is at `artifacts/win-x64/MAKRIVO.exe`. Keep the entire output folder together; you can copy it elsewhere. Windows 11 x64 is the initial target. Build artifacts are excluded from Git; a source checkout does not include a prebuilt executable.

Install the .NET 10 SDK specified in `global.json` to build and run from source:

```powershell
dotnet build Aivana.slnx -c Release
dotnet run --project src/Aivana.Desktop -c Release
```

Publish a self-contained build without requiring a separately installed .NET runtime:

```powershell
dotnet publish src/Aivana.Desktop -c Release -r win-x64 --self-contained true -o artifacts/win-x64
```

## Basic workflow

1. Select a profile and create an action tile.
2. Set its name, icon, color, and keyboard shortcut. Ctrl/Alt/Shift combinations reduce accidental activation while typing.
3. Add one or more steps: applications, files, websites, text, shortcuts, media, volume, profile switching, or delays.
4. Save. Changes remain a draft until saved; discard restores the saved version. The tile Play button and its hotkey use the same execution path.
5. Reorder tiles and macro steps with drag-and-drop or arrow buttons. Templates simplify setup. Macros can repeat up to 100 times. Cancel through the interface, system tray menu, or Ctrl+Alt+Escape by default.

Application-specific profiles use a process name without its full path. Profile order breaks ties; the default profile is the fallback. Automatic profile selection must be enabled in Settings.

Closing the main window keeps the app in the system tray by default. Double-click its tray icon to reopen it; use the tray menu to exit. Startup with Windows is optional and disabled by default. Hotkeys can be paused centrally.

## Actions and command palette

Add Action opens a searchable catalog with 90 presets in eight categories. Each has a description and preview. The macro editor uses the same catalog to add or replace steps. Selecting a preset in this editor workflow creates a draft without execution.

The command palette opens from the header or with Ctrl+K inside the app. It searches saved actions across profiles, catalog presets, profile switching, navigation, pause/resume, macro cancellation, and import/export. Filter by saved actions, catalog functions, or app commands. Arrow keys select, Enter activates, Ctrl+Enter edits an editable entry, and Escape closes. Favorites appear first. Ready-to-use catalog commands can run immediately; presets requiring setup open a draft. Settings can define a global palette shortcut. New configurations use Ctrl+Shift+Space; existing configurations do not receive a new global shortcut automatically.

Audio actions set volume precisely from 0 to 100 percent and mute, unmute, or toggle output and microphone devices. Choose a specific endpoint or leave it unset for the Windows default. An unavailable saved endpoint produces an error instead of controlling another device. Refresh Devices reloads the connected devices.

OBS Studio uses WebSocket v5: start/stop/toggle/pause/resume recording; start/stop/toggle streaming; change scenes; toggle input mute. Enable the WebSocket server under Tools in OBS and set its endpoint in the editor. Check Connection and Load Names reads the OBS version, scenes, and inputs without starting a recording or stream. Choose names from the list or enter them manually. Passwords can be saved in Windows Credential Manager and are excluded from exported configurations. Unavailable OBS connections report an error.

Profiles can match a process name and part of a window title. Both specified conditions must match. Lock Profile prevents automatic switching for the session. Capture Conditions from Foreground uses the last focused external application.

Text and clipboard actions support `{{date}}`, `{{time}}`, `{{datetime}}`, `{{clipboard}}`, `{{profile}}`, `{{process}}`, and `{{user}}`. Dates use `yyyy-MM-dd`; times use `HH:mm`. Unknown placeholders remain unchanged. `{{profile}}` identifies the launched action's profile; `{{process}}` identifies the external application context. Clipboard content is read only when that placeholder is used. The clipboard action writes text but does not paste automatically.

Window actions minimize, maximize, restore, or arrange a window on the left/right side of its monitor's work area. The system action locks Windows. These actions run only when explicitly invoked.

Preview shows steps, repetitions, and variables without execution. Undo and Redo retain up to 100 draft states; text boxes keep native Ctrl+Z editing. Mark favorites in the editor or tile menu and filter them on the dashboard. Execution counts apply only to the current session.

Text, shortcut, and window actions launched from the interface activate the last external target window. The palette displays that target context. If the target has closed or Windows refuses activation, execution stops with a message.

## Keyboard assistance and feedback

The Special-Key Wizard is on the Keyboard page and in the palette. It lists the G-keys reported by iCUE. Detect Key Press temporarily captures them without executing their assigned actions. Normal bindings return after detection, cancellation, or closing. A catalog function and selected key open as an unsaved draft; duplicate bindings are checked.

The Keyboard Overlay shows the selected reference layout and active bindings. Hold Ctrl+Alt+F1 or toggle it from the palette or Keyboard page. It does not take focus and lets mouse clicks pass through. Profile and binding changes update while visible; ordinary Windows key presses are highlighted. Manufacturer-specific controls may not supply an equivalent Windows key signal.

Execution feedback shows waiting, step/repetition progress, completion, cancellation, or failure without taking focus or exposing entered text. Commands Executed confirms command execution, not an independently queried live state in a third-party application. Error details appear in the activity log.

## Devices and Corsair

Corsair G1–G6 can be assigned directly through the official iCUE SDK. Select the device and key in the action editor and save, or prepare a draft from the K100 keyboard view. Bindings apply to the selected device and active profile; pause disables them too. iCUE must be running and allow access. Fn, the iCUE wheel, profile, and lock controls cannot be mapped through this G-key API.

The Devices page lists Windows keyboard interfaces and refreshes periodically and when devices connect. A physical keyboard may expose multiple HID interfaces. Standard global hotkeys work across manufacturers.

Corsair detection uses available device information. RGB control requires compatible hardware, running iCUE, and the official iCUE SDK; the app reports actual availability. Other manufacturers currently receive detection and standard actions. Outside Corsair G-key integration, proprietary controls work when Windows receives them as regular input. The app does not install a keyboard driver.

## Languages

Settings offers 27 languages: German, English, French, Spanish, Italian, Portuguese, Dutch, Polish, Czech, Slovak, Hungarian, Romanian, Bulgarian, Greek, Danish, Swedish, Norwegian, Finnish, Estonian, Latvian, Lithuanian, Slovenian, Croatian, Ukrainian, Russian, Turkish, and Serbian in Latin script. Automatic follows the Windows display language. Translations are bundled locally. Layouts, hotkeys, and user-created names are independent of the interface language.

Complete repository documentation is available in all 27 application languages through the [language index](README.md). Translations are machine-assisted, with original-source links and structural checks; native-speaker review is separate. The app also accepts the regional codes `en-US`, `de-DE`, `fr-FR` and `it-IT` and uses their number formats. Existing language codes remain compatible.

## Layouts and manufacturer research

The keyboard view provides full-size and tenkeyless ISO-DE/ANSI-US references plus Corsair K100 RGB layouts. Geometry represents positions, wide/tall keys, and ISO Enter. Standard layouts are generic references. K100 layouts follow documented controls without claiming millimeter-accurate dimensions. Choose the correct language/geometry variant: a model name or USB VID/PID does not establish a specific variant.

Mapping resolves physical scan codes using the active Windows input layout. The workflow prepares a draft and retains save/conflict checks. Standard bindings are global Windows hotkeys, not isolated to one connected keyboard. Corsair G1–G6 use device-specific iCUE integration. Other proprietary controls require an adapter or manufacturer-configured shortcuts. Main Enter and numeric-keypad Enter share a virtual-key identity for global hotkeys.

The [manufacturer research](research/tastaturhersteller-sonderfunktionen.md) and [CSV](research/tastaturhersteller-sonderfunktionen.csv) cover 74 brand/manufacturer groups, distinguishing configurators, input SDKs, and lighting APIs. [Layout evidence](research/keyboard-layout-evidence.md) records model, SKU, and module differences. Research coverage does not mean hardware support is implemented.

## Data

Configuration is stored in local application data. Import/export use JSON. Import previews the configuration, replaces the current configuration after confirmation, and disables imported bindings until manually enabled. Text-action content is not collected as a keystroke log. Credential Manager passwords are separate from exported profiles.

## Verification

```powershell
dotnet run --project tests/Aivana.Tests -c Release
dotnet run --project tests/Aivana.Windows.Tests -c Release
```

Windows checks may inspect hardware and temporarily configure/release supported iCUE bindings. Run them without another controller using those bindings. Audio endpoint checks do not change live volume or mute settings.

Native input cannot bypass Windows privileges. A normally launched controller may not send input to an administrator application. Reserved or occupied hotkeys appear as conflicts.

The [documentation index](README.md) links historical designs, plans, research, and dated test reports. Historical plans describe their recorded scope, not a current guarantee of feature support.

### Workflow templates and target-window waits

The catalog includes six editable workflows: workspace, meeting, streaming, research, project switch, and text snippet. Search for `workflow.` in the catalog or command palette. Selecting a workflow opens an unsaved draft; it never runs immediately. All steps are retained when adding a workflow to an existing macro or assigning it to a Corsair key. Configure example URLs, program paths, profiles, and OBS scenes before saving.

The **Wait for target window** step accepts a process name (with or without `.exe`), a case-insensitive title substring, and a timeout of 1–600,000 ms. At least one filter is required; both filters must match when supplied. The macro continues only after a matching visible window responds and reaches the foreground. Use a specific title when multiple windows share a process. Cancellation or timeout stops subsequent steps. Window readiness does not guarantee that a document has loaded or an external service such as OBS WebSocket is connected.

Display brightness: search for **Increase screen brightness**, **Decrease screen brightness**, or **Screen brightness at 50 percent** in the action catalog. Relative actions change brightness by 10 percentage points; fixed values range from 0 to 100. Supported internal displays use WMI; compatible external monitors require DDC/CI enabled in their menu. By default, the action affects all supported displays; an explicit monitor can be selected in the editor. Connections and docks can restrict support.

### Extended controls

Open the command palette with **Ctrl+K while the app window has focus**. Search for **Workflow designer**, **Record macro**, **Synchronize profiles**, or **Key diagnostics** (the wording follows your selected language). The workflow designer edits the selected tile draft: file/process/window conditions, forward skip counts and stop/continue error handling. The recorder captures shortcut chords and waits, not ordinary typing; Escape stops recording. Review the result before adopting it.

In a brightness step, select all supported displays or a specific monitor. An unavailable selected monitor produces an error rather than silently changing another display. The dashboard shows live brightness, audio and OBS readings when their respective integrations are available; an unavailable reading is not presented as a measured value.

In the tile editor, assign a supported physical special key and choose immediate press, tap, hold or double tap. Held layer selectors temporarily activate another mapping and restore the previous layer on release. Gestures and layers apply to Corsair SDK G-keys and device-specific standard special keys, not ordinary global hotkeys. Standard-key capture supports F13–F24 and keyboard media keys. Proprietary Fn, profile and dial controls still require a compatible vendor adapter or a vendor mapping to a standard key. Raw Input observes the key and does not suppress its normal Windows behavior. Key diagnostics explains profile, pause, connection and binding problems.

Profile synchronization exchanges files through a folder you choose, including a folder managed by your existing cloud client. Review the preview before importing; imports start disabled and remove hardware-specific mappings. Revisions prevent silent overwrites, and including action contents is an explicit choice. MAKRIVO does not supply a cloud account or retain cloud credentials. The tray now uses MAKRIVO’s own teal M icon and shows an amber pause badge when paused.
