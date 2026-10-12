<!-- generated-doc locale=en-US source=docs/testing/2026-10-12-responsive-layout.md sha256=8b3710a27f806575e88b4d9a0c4e80de4f26539ea1b5826cfe291764228b4ca6 -->

> Machine-assisted translation. Structure, code and links are checked; native-speaker and legal review are not implied. Historical reports retain their original dates and scope. [Original source](../../testing/2026-10-12-responsive-layout.md)

# Responsive WPF interface

The main pages are based on the usable content width, not a fixed window width. Under 900 DIPs content width, the dashboard and editor are one below the other. Under 620 DIPs, key figures are in two columns. The sidebar is collapsed under 820 DIPs window width. Minimum size: 640 × 480 DIPs.

Profile and settings panes and keyboard controls switch their columns based on the actual grid width. Existing controls are rearranged instead of creating new entries. Action cards divide the available line width. Toolbars break. The keyboard side is reachable vertically, the physical keyboard retains its proportions.

Automatic WPF test: all five main pages at 640 × 480, 900 × 620 and 1440 × 900; Screenshots per page; Column change in both directions with checking of the received text. DIPs are logical WPF units. True Windows scaling at 150/200 percent and multiple monitors are not yet validated.

Test bench: Release build without errors/warnings. Complete WPF smoke test passed with all sizes, column changes, keyboard layouts and language checks. git diff --check passed.
