<!-- generated-doc locale=en-US source=docs/testing/2026-10-12-local-meeting-assistant.md sha256=56631ca85dca5f785448d12586b0741628728925a649f6124f7aebffb19bb615 -->

> Machine-assisted translation. Structure, code and links are checked; native-speaker and legal review are not implied. Historical reports retain their original dates and scope. [Original source](../../testing/2026-10-12-local-meeting-assistant.md)

# Local AI meeting assistant – prototype

Status: 12. October 2026. ForgeMind Outcome Agent Hypothesis, Bounded Meeting Flow. No proven competitive advantage and no measured time savings without real comparison tests.

## Usage

1. Command Palette → **KI Meeting Assistant**. Experiment is off by default and is reset during configuration import or portable export.
2. Enable local assistant. Ollama must run separately on `127.0.0.1:11434`; a suitable local model must already be installed. **Load local models** and select model. MAKRIVO does not install models/services, contact OpenAI/Cloud API, or use API keys. The offline configuration and trustworthiness of the local Ollama service rests with the user; declared remote/cloud models are rejected.
3. Enter destination and explicitly add programs/documents/websites. Target, resource ID, name and type are transmitted; Target paths and contents of selected files are not additionally transmitted; the destination entry itself is sent verbatim. Website labels include schema/host only, the full link remains local.
4. **KI create draft**. Maximum 90 seconds, 1024 output tokens, limited response size. Target/model/resource changes discard preview; Cancel/Close ends the request. The model may only suggest selected IDs. Unknown IDs, free commands, additional JSON fields and executable document types are rejected.
5. Check preview and open points → **Check in editor**. The result is initially an unsaved draft. First save in the existing editor and execute it explicitly if necessary. Normal macro validation, pre/post check, pause and abort remain responsible.

Supported: Start programs without arguments, open explicitly selected local PDF/DOCX/XLSX/PPTX/TXT/MD, open HTTP(S) websites, mute microphone upon explicit request. No headset default change, no messages, appointments, downloads, scripts, free clicks or text input. Wishes that cannot be represented appear as open points if the model recognizes them correctly; The user must check whether the technical objectives have been met. Document opening is not automatically technically verified.

## Comparison experiment

**Measure manual setup · Start / End** measures the complete manual comparison flow. Wizard assumptions measure the time from opening the wizard to the first save in the editor; `generated`, on the other hand, only contains the planning duration. Do not compare two unequal durations as a time saver.

Local events are in the configuration folder under `Experiments/meeting-experiment.jsonl`: timestamp, random attempt/tile ID, event, duration, correction yes/no. No target texts, resource names, paths, model answers or document content. File rotates from 2 MiB, a previous file is retained. Measurement errors must not abort macros and configuration saving.

Captures: manual baseline, model generation/errors/abort, discarding preview changes/closes, initial saves and corrections in the editor, and actual runner end states for tiles saved from them. `run_completed` just means the runner has completed; Confirmed target effects are listed separately in the test process. Repeats of the same macro are not new independent setup attempts. The initial experiment can continue after restart because the experiment ID remains on the saved tile. Duplicated tiles share this origin; group by trial ID when evaluating.

First experiment: 20 comparable meeting facilities or two weeks with volunteer test users; initially internal use. Compare manual setup time vs. full assisted setup time; Additionally evaluate correction/rejection rate, runner errors and real test results. If there is no improvement or if the protection limits are violated, deactivate the switch. No usage or willingness to pay has been demonstrated in the repository. No automatic telemetry transmission or price adjustment.

## Test bench

- Core: Limits, types, unknown/duplicate IDs, script exclusion, long links, optional legacy configuration, import/export opt-in, data-sparing metering, and one-time discard and correct commit.
- Transport with fake model: Ollama JSON schema, loopback destination, no file paths in prompt, cloud name and remote alias rejected, corrupt/overlong response rejected, abort.
- WPF-Smoke: Dialog rendered; Draft submission does not change saved tiles, review/save preserves provenance and records correction, discard does not save anything.
- Independent Review: Discarded preview and found and corrected long links; Regression testing RED→GREEN.
- Release build with zero warnings/errors, entire core suite and WPF smoke successful. The complete Windows suite initially existed including focus changes. Another run failed again because the foreground change was refused; the test is context dependent and remains unchanged. The remaining Windows tests including model transport pass with the foreground success test explicitly omitted.
- Package inspection revealed an existing bug in the atomic exchange of briefly opened configuration files. A read lock reproduced the same access denied error; limited repetition of only the atomic exchange is checked RED→GREEN. Permanent access errors remain visible.
- Current standalone package `artifacts/release-20261012-184105`: WPF smoke successful after memory correction, including AI dialog and draft acceptance/rejection.
- Not a true model test: the local Ollama service did not respond when tested. No statements about model quality, latency, semantic completeness or time savings. Hardware/driver/scaling limitations of the previous release remain.

The Protocol Sources: [Ollama Chat](https://docs.ollama.com/api/chat), [Structured outputs](https://docs.ollama.com/capabilities/structured-outputs), [Official type definitions including remote model data](https://github.com/ollama/ollama/blob/main/api/types.go).
