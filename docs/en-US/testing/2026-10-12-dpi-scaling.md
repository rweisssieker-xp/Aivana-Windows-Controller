<!-- generated-doc locale=en-US source=docs/testing/2026-10-12-dpi-scaling.md sha256=dcf6d4c1a21eec9be9f07a38cab0aff308587c78b85fb2b24c69f04d28d82864 -->

> Machine-assisted translation. Structure, code and links are checked; native-speaker and legal review are not implied. Historical reports retain their original dates and scope. [Original source](../../testing/2026-10-12-dpi-scaling.md)

# DPI check

MAKRIVO declares PerMonitorV2 in the embedded application manifest. The WPF test uses GetWindowDpiAwarenessContext and AreDpiAwarenessContextsEqual to check the actual effective context of the main window.

The additional layout/grid tests simulate 150 and 200 percent on a 1920 × 1080 display with taskbar reserve: logical window dimensions 1280 × 693 respectively 960 × 520 DIPs. All five main pages are rendered as PNG at 144 or 192 DPI. Files are called scale-simulated-150-* or scale-simulated-200-*.

These tests do not change any Windows settings. They do not validate true WM_DPICHANGED monitor switching, mixed monitor configuration, and actual sharpness on a different display. The physical scaling tests remain open.

The WinForms analyzer warning WFO0003 is suppressed exclusively for this WPF startup project: WinForms is used for notification area integration, WPF controls startup; DPI is set via manifest before window creation.

Microsoft documentation: https://learn.microsoft.com/en-us/windows/win32/hidpi/setting-the-default-dpi-awareness-for-a-process

Visual inspection: The dashboard also switches to the stacked display under 650 DIPs window height so that maps and editor are not squeezed into a middle area that is too small.

Test bench: Release build without errors/warnings; Passed full WPF smoke test including actual PerMonitorV2-Fensterkontext and simulated 150/200-Prozent-Renderings. The 200-Prozent-Dashboardaufnahme was visually inspected after the height adjustment. git diff --check passed.
