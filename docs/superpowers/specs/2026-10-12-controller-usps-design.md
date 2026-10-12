# MAKRIVO control extensions

The user requested all nine proposed improvements and a recognizable tray icon. Existing configurations, particularly G1/G2 brightness, must remain compatible. Existing uncommitted branding and documentation edits belong to the shared workspace and must be preserved.

## Deliverables and boundaries

1. Brightness actions select all supported monitors or a stable specific monitor ID. Missing explicit targets fail visibly. Dashboard shows current brightness, output volume, microphone state and OBS status through bounded asynchronous reads.
2. Special keys support immediate press, tap, hold and double tap, with repeat suppression. A held layer selector temporarily changes the active mapping and release restores it. Existing immediate bindings retain their behavior. Pause, profile changes, disconnects and cancellation clear pending gestures.
3. The workflow designer edits conditions, forward branches and stop/continue failure policy transactionally in a tile draft. Conditions cover files, running processes and window titles. No unbounded jump cycles.
4. Explicit recording captures shortcut chords and waits, ignores injected input and stops on Escape. Recorded actions are reviewed before adoption; ordinary text is not recorded.
5. Per-device standard special keys from other manufacturers use Windows Raw Input. Proprietary keys require a supported vendor API or vendor mapping to a standard key; unsupported functionality is shown honestly. Diagnosis explains profile, pause, binding conflicts and actual adapter state.
6. Profile exchange uses a user-selected shared folder (including folders synchronized by an existing cloud client). Revision checks prevent silent overwrites. Pull previews changes and imports disabled actions with hardware mappings removed. Action content inclusion is explicit. No cloud credentials are stored by MAKRIVO.
7. UI strings belong to the existing 27-language catalogs; DE/EN/FR/IT remain the minimum reviewed documentation languages. Translation gaps must be disclosed rather than claimed complete.
8. A recognizable teal M keyboard mark replaces the generic Windows tray icon. Paused mode has an amber pause badge. Executable/window branding uses the same mark. Tooltip includes state and profile.

## Verification

Core checks cover backwards compatibility, snapshots, cancellation, gesture timing, workflow branches/errors, recorder filtering and synchronization conflicts. Windows checks cover read-only display enumeration, SDK release events and Raw Input parsing. WPF smoke checks cover new entry points and resources without changing real monitor brightness, typing into another application or overwriting user profiles. Deployment preserves a backup and G1/G2 configuration.
