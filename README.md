# Aivana Windows Controller

**English (US)** | [Deutsch](README.de-DE.md) | [Français](README.fr-FR.md) | [Italiano](README.it-IT.md)

A local Windows hotkey controller with Stream Deck-style tiles, profiles, macros, and keyboard detection. The interface supports 27 languages. No account or cloud connection is required for everyday use.

[Documentation](docs/en-US/README.md) · [Verification](docs/en-US/VERIFICATION.md) · [Feature test report](docs/en-US/testing/2026-10-09-usp-release.md)

## Getting started

After publishing, the application is at `artifacts/win-x64/Aivana.Controller.exe`. Keep the entire output folder together; you can copy it elsewhere. Windows 11 x64 is the initial target. Build artifacts are excluded from Git; a source checkout does not include a prebuilt executable.

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

Add Action opens a searchable catalog with 80 presets in seven categories. Each has a description and preview. The macro editor uses the same catalog to add or replace steps. Selecting a preset in this editor workflow creates a draft without execution.

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

Repository documentation is available in US English (`en-US`), German (`de-DE`), French (`fr-FR`), and Italian (`it-IT`). Documentation locales do not change the app's existing language setting codes.

## Layouts and manufacturer research

The keyboard view provides full-size and tenkeyless ISO-DE/ANSI-US references plus Corsair K100 RGB layouts. Geometry represents positions, wide/tall keys, and ISO Enter. Standard layouts are generic references. K100 layouts follow documented controls without claiming millimeter-accurate dimensions. Choose the correct language/geometry variant: a model name or USB VID/PID does not establish a specific variant.

Mapping resolves physical scan codes using the active Windows input layout. The workflow prepares a draft and retains save/conflict checks. Standard bindings are global Windows hotkeys, not isolated to one connected keyboard. Corsair G1–G6 use device-specific iCUE integration. Other proprietary controls require an adapter or manufacturer-configured shortcuts. Main Enter and numeric-keypad Enter share a virtual-key identity for global hotkeys.

The [manufacturer research](docs/en-US/research/tastaturhersteller-sonderfunktionen.md) and [CSV](docs/en-US/research/tastaturhersteller-sonderfunktionen.csv) cover 74 brand/manufacturer groups, distinguishing configurators, input SDKs, and lighting APIs. [Layout evidence](docs/en-US/research/keyboard-layout-evidence.md) records model, SKU, and module differences. Research coverage does not mean hardware support is implemented.

## Data

Configuration is stored in local application data. Import/export use JSON. Import previews the configuration, replaces the current configuration after confirmation, and disables imported bindings until manually enabled. Text-action content is not collected as a keystroke log. Credential Manager passwords are separate from exported profiles.

## Verification

```powershell
dotnet run --project tests/Aivana.Tests -c Release
dotnet run --project tests/Aivana.Windows.Tests -c Release
```

Windows checks may inspect hardware and temporarily configure/release supported iCUE bindings. Run them without another controller using those bindings. Audio endpoint checks do not change live volume or mute settings.

Native input cannot bypass Windows privileges. A normally launched controller may not send input to an administrator application. Reserved or occupied hotkeys appear as conflicts.

The [documentation index](docs/en-US/README.md) links historical designs, plans, research, and dated test reports. Historical plans describe their recorded scope, not a current guarantee of feature support.
