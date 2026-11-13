# Real WPF UI preview renderings — 2026-10-26

Source: clean exported Git commit bf99712d0cbfcf3f9fa3842c1257ceb7c8adf655. These are actual MAKRIVO MainWindow.Content renderings, not AI images or UI mockups. The helper references the clean Desktop project, creates a plain WPF Application and a DesktopController with a new isolated temporary configuration per language, pauses execution, creates the real MainWindow and renders its content with RenderTargetBitmap at 1600x900, 96 DPI. It navigates only to Dashboard and Profile. Synthetic profile/tile labels are used; no private windows, file paths, device code or license is shown.

Files: de-dashboard.png, de-profiles.png, en-dashboard.png, en-profiles.png. All four were visually inspected. The DE and EN UI text is genuine application localization. The blank activity state, unbound actions and paused hotkeys reflect the isolated fixture.

No controller.Attach, ExecuteAsync, macro, global hotkey registration, license import, installed MSIX or Store purchase was used. The helper did not show/activate a native window or control another app. It closes its own windows and disposes its controllers; the final run exited zero. Temporary configuration folders are retained and contain only fixture state.

These are **unpackaged offscreen UI previews**, not evidence of installed MSIX behavior or Store certification. Before submission, capture and verify the final installed package, its normal responsive layout and any changed UI. Do not call these packaged acceptance screenshots. The existing UI uses its compact navigation in this offscreen rendering.

Reproduction helper source is CapturePreview.cs.txt. Its temporary project targets net10.0-windows with UseWPF=true and a ProjectReference to the clean exported src/Aivana.Desktop/Aivana.Desktop.csproj. Build/run used a separate --artifacts-path and the screenshot directory as the sole program argument. No repository application source was edited.