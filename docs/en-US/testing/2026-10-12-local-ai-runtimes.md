<!-- generated-doc locale=en-US source=docs/testing/2026-10-12-local-ai-runtimes.md sha256=3dfbb80761aad758ea6a9307999ff530dfdd4a0cf685804e15052508cf107fd7 -->

> Machine-assisted translation. Structure, code and links are checked; native-speaker and legal review are not implied. Historical reports retain their original dates and scope. [Original source](../../testing/2026-10-12-local-ai-runtimes.md)

# Local AI runtimes

## Usage

Open Tools → AI meeting assistant, explicitly enable it and select an AI runtime. Authorize resources, describe the goal, create a draft and review it in the editor. No provider executes the draft automatically. There is no automatic provider fallback.

| Runtime | Requirement | Connection |
|---|---|---|
| Ollama | locally installed model | 127.0.0.1:11434 |
| LM Studio | running local server with locally loaded model and JSON schema support | 127.0.0.1:1234 |
| llama.cpp | running llama server with local model and JSON schema support | 127.0.0.1:8080 |
| ONNX Runtime GenAI CPU | compatible, trusted local GenAI text model folder | separate bundled process, no HTTP |

The HTTP clients do not use proxies and do not follow redirects. Local servers can themselves address additional services; In particular, LM Link/Remote Routing may not be used for local processing. MAKRIVO cannot reliably detect this through the compatible interface. Ollama remote metadata and cloud names are additionally rejected. Authenticated local servers cannot be configured in this version.

## ONNX

Select genai_config.json in the dialog. The folder must contain tokenizer.json and a chat template. Supported forms are chat_template as a string, a named default template (array or dictionary), or a separate chat_template.jinja when the inline template is absent or null. Ambiguous templates are rejected; the maximum length is 65536 characters. Arbitrary .onnx files, vision/embedding models and GPU/NPU-only exports are not meeting-assistant models.

Bundled: Microsoft.ML.OnnxRuntimeGenAI 0.17.1, CPU. Provider configuration is reset to CPU. No downloads and no model weights in the package. No Python installation required. Use only trusted model packages; the separate process contains crashes and supports cancellation, but is not a security sandbox.

Limits: 90 seconds including model loading, at most 3072 prompt tokens and 1024 generated tokens, further restricted by model context; strict validation of JSON and known resource IDs. Invalid or truncated responses are discarded. Closing or cancelling terminates the started worker process. No unrestricted model commands and no programs/file paths in the separate prompt resource list; the goal text entered by the user can itself contain such data.

## Verification

- Core suite, loopback transport tests with simulated LM-Studio/llama.cpp/Ollama responses, schema/abort/resource check.
- Unicode protocol regression with maximum long targets and 24 resource names.
- Real native CPU inference with the official tiny-random-gpt2-fp32 test graph from microsoft/onnxruntime-genai, commit 5c19002edce50beaa3a5c7eeefd8fe5269ee8d19. A test template was added and the byte tokenizer adapted to the small random graph. This random model does not test quality and is not included in the release.
- Reproducible native runtime test: tools/check-onnx-runtime.ps1 -ModelDirectory <GenAI model folder>.
- WPF-Smoke checks the selection of all four providers and the test bench; Selection alone does not generate a model request.

## Remaining acceptance work

Hardware, smartphone, tablet and monitor-transition tests are still required. Exclusive keyboard suppression remains blocked without a signed, licensed and tested filter driver; dummy drivers and Raw Input do not replace it. Windows can still block focus changes; the error now instructs the user to activate the target window manually. Translations for central AI controls were added to all 27 catalogs; detailed new text and specialist translation review remain outstanding. Random-model and simulated-HTTP tests do not demonstrate live quality with a suitable Instruct model, sustained operation or measured time savings.

## Sources

- https://onnxruntime.ai/docs/genai/api/csharp.html
- https://www.nuget.org/packages/Microsoft.ML.OnnxRuntimeGenAI/0.17.1
- https://lmstudio.ai/docs/developer/openai-compat/chat-completions
- https://github.com/ggml-org/llama.cpp/blob/master/tools/server/README.md

Final test state: Release build without warnings/errors; full Core and Windows suites passed, including the native focus fixture, HTTPS, UIA, plugin IPC and compatible AI transport tests. New package release-20261012-190618: WPF smoke passed (19:07), bundled ONNX CPU inference with the test graph passed. Independent review: Unicode protocol limit corrected, no other concrete findings. This does not establish physical manufacturer-device, phone or monitor acceptance.

## Follow-up: real model quality and templates

Additional CPU run with allama-ai/SmolLM-360M-Instruct-onnx-int4, revision 82a3d1a8a86c8053f2a859a90ed4d344fc0725b0: native inference passed. Model-weights SHA256: 48e9c4ef12f7f995ca11fc7020b39990aa27bc67cc0fe116067676a000a04429. Community export used only in the test area, not included in the release.

Four real planning tests (German/English; document, program, explicit microphone mute): **0/4 valid plans**; each produced JsonException during strict response validation. Durations: 63501, 6376, 72118 and 6774 ms. No actions were executed. This small model does not meet the planning contract and is not recommended as suitable. Report: artifacts/local-ai-evaluation/smollm360-results.json. Reproduce: Windows-Tests --evaluate-onnx <model folder> <report path>. Runtime functionality does not establish model quality.

Tool navigation and the test report were additionally translated in all 27 language catalogs. Long-form text and linguistic review remain outstanding. Template regression checks also cover wrong JSON types, empty/ambiguous/overlong templates and null with Jinja fallback.

Updated package: artifacts/release-20261012-201134. Packaged WPF smoke passed on 12 October 2026 at 20:12; native CPU inference with the separate Instruct test model passed in the bundled worker. Core and Windows AI tests passed; build without warnings/errors. Model planning quality remains 0/4.
