<!-- generated-doc locale=en-US source=docs/testing/2026-10-12-gui-polish.md sha256=ac39188358e51c68964421d3e855161d607b02a41da2b1a1fad70244f9feff97 -->

> Machine-assisted translation. Structure, code and links are checked; native-speaker and legal review are not implied. Historical reports retain their original dates and scope. [Original source](../../testing/2026-10-12-gui-polish.md)

# GUI revision from 12.10.2026

- Direct access to “Tools” in the navigation; three groups with declared actions for setup, creation and testing.
- Fourteen existing dialog areas use a common WPF dialog class with the main window resources, consistent font and positioning at the owner.
- Dialog size is limited to Windows workspace; long content gets scroll bars. Settings and profile pages remain scrollable even at low heights.
- Action filter only appears on the dashboard. Profile selection and search have explicit names for assistive technologies. Field labels are more contrasting.
- Dialog smoke test includes tool overview and extension dialogs in 560 × 440 and normal size; Existing recorder and language tests have been adapted to the scroll structure.

Limitations: Compact windows are not a substitute for real Windows DPI tests with 150/200 percent. The new German texts have not yet been fully translated for all 27 languages. No new hardware validation and no statement about measured usability.

Test bench: Release build without warnings/errors; Passed full WPF smoke test, including compact tool and extension dialogs and language switching checks. git diff --check passed.
