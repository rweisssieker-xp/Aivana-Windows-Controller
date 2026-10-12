<!-- generated-doc locale=en-US source=docs/superpowers/plans/2026-10-12-local-meeting-assistant.md sha256=1f105661dc4244fce4110ddae1484b4ce819e2d4c6945ada84a27704d95db72a -->

> Machine-assisted translation. Structure, code and links are checked; native-speaker and legal review are not implied. Historical reports retain their original dates and scope. [Original source](../../../superpowers/plans/2026-10-12-local-meeting-assistant.md)

# Local Meeting Assistant – 2026-10-12

Authorized by “ok go” and existing specifications without questions. Preserve existing branch/changes. Short prototype, no automatic execution.

1. Core: selected resources with IDs; Model plan resource IDs only, microphone mute, open items. Strict size/type/ID limits; Compiler only produces Launch/OpenPath/Url/Volume. Tests first: unknown IDs, executable documents, non-HTTP URLs, duplicates, empty plans, feature off.
2. Windows: Ollama loopback-only HTTP without proxy/redirect, JSON schema, token/response limits, cancellation and 90s-Zeitlimit; no automatic install/download, no cloud/API keys. Fake transport tests check request and errors; real model only if available locally.
3. WPF: Command palette, switch disabled by default, model selection from local service, target and explicit resource list, preview/open items, abort, commit to existing draft without execution. Changes made after preview invalidate them; Check profile revision.
4. Experiment: manual stopwatch and local measurement events (excluding target/paths), design acceptance/correction/rejection, and actual macro end states. No proof of success from Completed alone; existing results check separately.
5. Tests: Core, Transport, WPF-Smoke; no live user actions. Report highlights lack of live model testing and legacy release gates still open. Do not re-declare existing six-part release as AI-enabled.

Ollama log sources: https://docs.ollama.com/api/chat and https://docs.ollama.com/capabilities/structured-outputs (12.10.2026).

## Execution status

- Core Compiler/Feature/Data Economy green after initial Compile-RED.
- Local model client green after initial compile-RED; Remote alias, cloud name, schema, missing path in prompt, broken/too large output and abort with fake handler checked.
- WPF-Handoff: unsaved, no execution; Review/Save/Correction and Discard in Smoke green.
- Fresh-context reviewer /root/meeting_review (read-only): two P2-Funde, discarded previews and long links. Both corrected with regressions RED→GREEN. No other important findings reported.
- Release build zero warnings/errors, all core groups and full Windows suite without skip green. Native focus check now also green; existing iCUE keyboard connected. No real user programs started by model.
- No live Ollama available; no model/service installed and no cloud/keys used. Quality/time savings remain experimental.
- Self-contained package with accompanying programs and new operating/testing instructions under artifacts/release-20261012-183635. Preserve existing artifacts and uncommitted changes; no merge/push.

- Package smoke showed access-denied on ConfigStore atomic File.Move; A fresh repeat run later got stuck in an existing error dialog and was specifically terminated. Regression with FileShare.Read (read allowed, delete sharing denied) reproduces the File.Move error. Bounded retry only for Win32 5/32/33 up to 500ms RED→GREEN; Permanently missing rights remain errors. New package status is checked again.

- Final complete Windows repeat run fails again due to refused focus change; no new compiler/model bug. Remaining suite --skip-foreground-fixture green. Not asserting stable green for contextual native foreground test. Current package path artifacts/release-20261012-184105.

- Current standalone package artifacts/release-20261012-184105: WPF smoke including AI dialog, unsaved editor commit, correction measurement and discard successful (exit 0). No further code changed after this test.
