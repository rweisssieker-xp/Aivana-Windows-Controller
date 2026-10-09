# Windows Controller Implementation Plan

[English (US)](../../en-US/superpowers/plans/2026-10-08-windows-controller.md) | [Deutsch](../../de-DE/superpowers/plans/2026-10-08-windows-controller.md) | [Français](../../fr-FR/superpowers/plans/2026-10-08-windows-controller.md) | [Italiano](../../it-IT/superpowers/plans/2026-10-08-windows-controller.md)

> For agentic workers: use parallel isolated ownership; the user explicitly requested complete implementation with maximum useful subagents.

**Goal:** Deliver the native Windows hotkey panel in the approved design.
**Architecture:** Core configuration and cancellable execution; Windows adapters; WPF desktop composition.
**Tech Stack:** .NET 10, WPF, Win32, JSON. No paid dependencies.
**Spec:** ../specs/2026-10-08-windows-controller-design.md

## Global Constraints
German UI; local storage; Windows x64; truthful hardware capability status; no keyboard logging; no default autostart.

## Review Focus
Malformed imports must not replace saved settings; held modifiers must not contaminate output; unplugged hardware must not crash; closing must preserve tray availability; cancelled macros must release resources.

## Tasks and ownership
- [x] Core agent: src/Aivana.Core and tests/Aivana.Tests. Models, JSON persistence, validation, profile selection and MacroRunner. Run failing behavioral tests then implement and run all core tests.
- [x] Windows agent: src/Aivana.Windows. RegisterHotKey, device inventory, SendInput, foreground process and optional Corsair lighting adapter. Verify native struct sizes and registration cleanup with safe smoke tests.
- [x] GUI agent: src/Aivana.Desktop/MainWindow.xaml and .cs plus UI helpers only. Dark dashboard, full editor, profiles, devices, settings, keyboard overview and macro editor. Build once contracts exist.
- [x] Parent: shared contracts, project files, DesktopController composition, application/tray lifecycle, import/export dialogs, documentation, integration checks, publish and running-app smoke test.

## Shared interfaces
Contracts live in Core/Models.cs. Controller implementation lives in Desktop/DesktopController.cs. UI calls controller methods through that concrete class. Agents receive exact signatures in their tasks. Each owns separate files. Parent resolves cross-project failures after all agents finish.

## Verification and delivery
- [x] dotnet build Aivana.slnx -c Release: zero errors.
- [x] dotnet run --project tests/Aivana.Tests -c Release: behavioral checks pass.
- [x] dotnet publish src/Aivana.Desktop -c Release -r win-x64 --self-contained true -o artifacts/win-x64.
- [x] Launch published executable, inspect Windows UI and process stability, exercise safe profile/editing/persistence flow.
- [x] Write README with build/start instructions and hardware limitations; record checks and remaining hardware-only tests.
