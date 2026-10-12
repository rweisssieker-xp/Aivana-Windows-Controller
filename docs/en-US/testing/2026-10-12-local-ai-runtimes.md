<!-- generated-doc locale=en-US source=docs/testing/2026-10-12-local-ai-runtimes.md sha256=3dfbb80761aad758ea6a9307999ff530dfdd4a0cf685804e15052508cf107fd7 -->

> Machine-assisted translation. Structure, code and links are checked; native-speaker and legal review are not implied. Historical reports retain their original dates and scope. [Original source](../../testing/2026-10-12-local-ai-runtimes.md)

# Local AI runtimes

## Service

Tools → Open AI meeting assistant, activate it explicitly and select AI runtime. Release resources, formulate a goal, create a draft and check it in the editor. No provider runs the design automatically. There is no automatic change of provider.

| Duration | Requirement | connection |
|---|---|---|
| Ollama | locally installed model | 127.0.0.1:11434 |
| LM Studio | running local server with locally loaded model and JSON schema support | 127.0.0.1:1234 |
| llama.cpp | running llama server with local model and JSON schema support | 127.0.0.1:8080 |
| ONNX Runtime GenAI CPU | compatible, trusted local GenAI text model folder | separate bundled process, no HTTP |

The HTTP clients do not use proxies and do not follow redirects. Local servers can themselves address additional services; In particular, LM Link/Remote Routing may not be used for local processing. MAKRIVO cannot reliably detect this through the compatible interface. Ollama remote metadata and cloud names are additionally rejected. Authenticated local servers cannot be configured in this version.

## ONNX

In the dialogue select genai_config.json. the folder must contain tokenizer.json and a chat template. chat_template is supported as a character chain, a designated default template (Array or Dictionary) as well as a separate chat_template.jinja, if no inline template is present or this is null. multi-template are rejected; 65536 Popular .onnx files, vision/embedding models or pure GPU/NPU exports are not meeting models.

Bundled: Microsoft.ML.OnnxRuntimeGenAI 0.17.1, CPU. Provider configuration is reset to CPU. No download and no model weights in the package. No Python installation required. Use only trusted model packages; the separate process is a crash/abort boundary, not a security sandbox.

Limits: 90 seconds including model loading, maximum 3072 prompt tokens and 1024 generated tokens, additional model context limit; rigorously checked JSON and known resource IDs. Incorrect/truncated answer will be discarded. Close/Cancel terminates the started worker process. No free model commands and no programs/file paths in the prompt's separate resource list; the target text written by the user may itself contain such data.

## Verification

- Core suite, loopback transport tests with simulated LM-Studio/llama.cpp/Ollama responses, schema/abort/resource check.
- Unicode protocol regression with maximum long targets and 24 resource names.
- Real native CPU inference with official tiny-random-gpt2-fp32-Testgraph from microsoft/onnxruntime-genai, commit 5c19002edce50beaa3a5c7eeefd8fe5269ee8d19. Test template added and byte tokenizer adapted to the small random graph. This random model does not check quality and is not in release.
- Reproducible native runtime test: tools/check-onnx-runtime.ps1 -ModelDirectory <GenAI model folder>.
- WPF-Smoke checks the selection of all four providers and the test bench; Selection alone does not generate a model request.

## Remaining real shares

Hardware/smartphone/tablet/monitor replacement testing is still required. Exclusive keyboard suppression remains blocked without a signed, licensed and verified filter driver. Not replaced by dummy drivers or Raw Input. The focus change can still be blocked by Windows; the error now mentions manual activation of the target window. Translations have been added to central AI controls in all 27 catalogs; Detailed new texts and technical translation approval are still open. Live quality with a suitable instruction model, continuous operation and measured time savings are not proven by random model/fake HTTP tests.

## Sources

- https://onnxruntime.ai/docs/genai/api/csharp.html
- https://www.nuget.org/packages/Microsoft.ML.OnnxRuntimeGenAI/0.17.1
- https://lmstudio.ai/docs/developer/openai-compat/chat-completions
- https://github.com/ggml-org/llama.cpp/blob/master/tools/server/README.md

Final test: release build without warnings/errors; Passed full core and Windows suite, including native focus fixture, HTTPS, UIA, plugin IPC and compatible AI transport tests. New package release-20261012-190618: WPF smoke passed (19:07 clock), bundled ONNX CPU inference with test graph passed. Independent review: Unicode protocol limit corrected, no other concrete findings. No physical manufacturer/phone/monitor acceptance derived from this.

## Continued: Real model quality and models

Additional CPU running allama-ai/SmolLM-360M-Instruct-onnx-int4Revision 82a3d1a8a86c8053f2a859a90ed4d344fc0725b0: native inference survived. Modellgewichte-SHA25648e9c4ef12f7f995ca11fc7020b39990aa27bc67cc0fe1160676a000a04429.

Four real planning tests (German/English, Document, Program, explicit microphone sounding): **0/4 valid plans**; each JsonException in strict response examination. 63501, 6376, 72118 and 6774 This small model does not comply with the planning contract and is not recommended as suitable. report: artifacts/local-ai-evaluation/smollm360-results.json. reproduction: Windows-Tests --evaluate-onnx <model order> <report path>. The performance function does not prove model quality.

Tools navigation and test reports were added in all 27 Language catalogues are translated. long texts and language recordings remain open. template regression also examines false JSON types, empty/multiple/over-long templates and zero with Jinja Fallback.

Updated package: Artifacts/Release-20261012-201134Packed WPFSmoke on 12.10.2026 by 20:12 Native CPU reference with the separate Instruct test model in the binding worker. Windows-KI tests passed; Build without warnings/errors. 0/4.
