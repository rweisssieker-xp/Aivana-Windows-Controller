# Physical Keyboard View for Mapping

[English (US)](../../../en-US/superpowers/specs/2026-10-08-keyboard-mapping.md) | [Deutsch](../../../de-DE/superpowers/specs/2026-10-08-keyboard-mapping.md) | [Français](../../../fr-FR/superpowers/specs/2026-10-08-keyboard-mapping.md) | [Italiano](../../../it-IT/superpowers/specs/2026-10-08-keyboard-mapping.md)

## Goal

When mapping actions, the keyboard view should show the actual key positions. Equal-width rows are insufficient: Enter, Shift, Space, navigation, the numeric keypad, and manufacturer-specific controls differ.

## Implementation

- Catalog with separate physical IDs, geometry in key units, printed legends, and Windows input codes.
- ISO-DE and ANSI-US in both full-size and TKL layouts; additionally, the Corsair K100 RGB, which iCUE identified as the connected model.
- L-shaped ISO Enter, numeric keypad with tall Plus/Enter keys and a wide zero, and navigation and arrow keys in their own positions.
- Explicitly select the model/layout. USB VID/PID and model name do not prove the language variant. Generic profiles and profiles modeled from manufacturer materials are labeled differently; no millimeter-accuracy or firmware-compatibility guarantee.
- Map scancodes to the active Windows input layout so a physical key click does not accidentally bind a different key, such as Y/Z when switching between US and DE.
- Mapping opens a draft in the existing action editor. Save, conflict checking, and discard remain the existing mechanisms. Clicks do not trigger actions.
- Proprietary G-keys, profile/lock keys, and the iCUE wheel are displayed, but are not emitted as ordinary Windows hotkeys without an implemented input adapter. Configurable manufacturer shortcuts remain a possible path.
- Bindings remain global Windows hotkeys; a device view does not imply device-specific suppression or exclusive binding to a single physical keyboard.

## Evidence

[K100 manual with controls](https://assets.corsair.com/image/upload/corsairmedia/sys_master/productcontent/AP_K100_RGB_Mechanical_QSG_AD.pdf), [Corsair explanation of ANSI/ISO](https://www.corsair.com/au/en/explorer/gamer/keyboards/ansi-vs-iso-whats-the-best-keyboard-layout/).

## Checks

Geometry, variant, and input-code tests; builds and existing Core/Windows tests; WPF smoke test for rendering and mapping without executing actions or changing the real user configuration. Manufacturer research documents additional model/SKU requirements independently of the currently implemented layout catalog.
