<!-- generated-doc locale=en-US source=docs/superpowers/specs/2026-10-08-keyboard-mapping.md sha256=fb766a2a2b4698ae3e1c3f63b50bb4f5c605da3dd56c61c05fcc0832b5cf8aeb -->

> Machine-assisted translation. Structure, code and links are checked; native-speaker and legal review are not implied. Historical reports retain their original dates and scope. [Original source](../../../superpowers/specs/2026-10-08-keyboard-mapping.md)

# Physical keyboard view for mapping

[English (US)](2026-10-08-keyboard-mapping.md) | [Deutsch](2026-10-08-keyboard-mapping.md) | [Français](2026-10-08-keyboard-mapping.md) | [Italiano](2026-10-08-keyboard-mapping.md)

## Target

The keyboard view is intended to show the actual key positions when assigning actions. Lines of the same width are not enough: Enter, Shift, Space, Navigation, number pad and additional manufacturer controls differ.

## Implementation

- Catalog with separated physical IDs, geometry in key units, printed legends and Windows input codes.
- ISO-DE and ANSI-US each as full format and TKL; additionally Corsair K100 RGB, which was recognized as a connected model via iCUE.
- ISO Enter with L shape, numeric keypad with tall plus/enter keys and wide zero; Navigation and arrows in their own positions.
- Explicitly select model/layout. USB VID/PID and model name do not prove language variant. Generic profiles and profiles modeled based on manufacturer documents are clearly labeled differently; no millimeter scale or firmware compatibility guarantee.
- Map scancode to the active Windows input layout so that a physical key click does not accidentally occupy another key, such as Y/Z when switching between US and DE.
- Assignment opens a draft in the existing action editor. Saving, conflict checking and discarding remain the existing mechanisms. Clicks do not initiate actions.
- Proprietary G-keys, profile/lock keys, and iCUE wheel are represented, but are not output as normal Windows hotkeys without an implemented input adapter. Configurable manufacturer shortcuts remain a possible route.
- Assignments remain global Windows hotkeys; a device view does not imply device-specific suppression or exclusive assignment of a single physical keyboard.

## Evidence

[K100-Handbuch with control elements](https://assets.corsair.com/image/upload/corsairmedia/sys_master/productcontent/AP_K100_RGB_Mechanical_QSG_AD.pdf), [Corsair ANSI/ISO declaration](https://www.corsair.com/au/en/explorer/gamer/keyboards/ansi-vs-iso-whats-the-best-keyboard-layout/).

## Exam

geometry, variant and input code testing; Builds and existing core/Windows tests; WPF smoke for display and mapping without executing or changing the real user configuration. Manufacturer research documents additional model/SKU requirements independent of the currently implemented layout catalog.
