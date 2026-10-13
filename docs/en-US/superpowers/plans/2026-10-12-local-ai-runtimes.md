<!-- generated-doc locale=en-US source=docs/superpowers/plans/2026-10-12-local-ai-runtimes.md sha256=f24c6b3bce4d0b4dd9d42732c363941a36644f90cf8d664d61ea0c60eb950079 -->

> Machine-assisted translation. Structure, code and links are checked; native-speaker and legal review are not implied. Historical reports retain their original dates and scope. [Original source](../../../superpowers/plans/2026-10-12-local-ai-runtimes.md)

# Local AI runtimes and remaining checks

Goal: actually connect additional local AI runtimes, receive design reviews and make open releases visible. User authorizes implementation without questions. Existing changes are retained.

1. Loopback transport and ONNX pre-implementation packet inspection tests; Check network addresses, output limits, abort and unknown resources.
2. Shared planner messages; preserve Ollama and add LM Studio (127.0.0.1:1234) and llama.cpp (127.0.0.1:8080). No cloud URLs or automatic provider switching. Local servers can themselves forward requests; operating them remains explicitly the operator’s responsibility.
3. ONNX Runtime GenAI CPU in its own process with timeout, limited JSON protocol and process termination. Explicitly select model folder; no model is downloaded automatically. Only GenAI compatible text models, no arbitrary .onnx networks.
4. WPF selection for runtime, model, ONNX folder; all deliver the same rigorously tested design.
5. Test bench with clear limits for hardware, DPI, mobile panel and exclusive macro pad. Physical checks and a signed/licensed driver cannot be replaced by software here.
6. Release build, core/Windows testing, WPF smoke, worker/package testing and independent review. Create new complete package; Only report model quality as tested if the model actually exists.

Result: four runtimes implemented, ONNX tested natively separately, choice of provider and test bench in the packed WPF smoke passed. Core/Windows suites green. Review finding Unicode limit with common constant and regression fixed. Real hardware/mobile/DPI change approvals, driver release, full technical translations and live instruct quality remain explicit open work; not issued as complete.

Continued: separate Jinja and named/default templates implemented; type/limit/fallback regression checks passed. Tool navigation added in 27 catalogs. A real Instruct model was downloaded separately and its SHA256 checked: native CPU inference works, but 0/4 planning cases satisfy the JSON contract; no actions executed. Independent review: null fallback corrected and tested, then no further concrete findings. Core and Windows AI tests passed, release build 0 warnings/errors.
