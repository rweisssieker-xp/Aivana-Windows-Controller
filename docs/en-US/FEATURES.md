<!-- generated-doc locale=en-US source=docs/FEATURES.md sha256=ea712f39457ce69f772f09a1294201705a5f36a92a8bf15f356bd80132c3e723 -->

> Machine-assisted translation. Structure, code and links are checked; native-speaker and legal review are not implied. Historical reports retain their original dates and scope. [Original source](../FEATURES.md)

# MAKRIVO – Feature overview

Manufacturer: **Aivana**. As of **12 October 2026**. This describes the development state, not approval for sale or hardware compatibility.

## Windows control

- Tiles, global hotkeys, command palette, favorites and application-dependent profiles.
- Multi-step, repeat, pause, cancel and preview macros.
- Open programs, files and websites; Use text, clipboard and keyboard shortcuts.
- Arrange windows, lock Windows, control audio outputs and microphones, and change screen brightness via supported interfaces.
- OBS WebSocket v5 integration for scenes, recording, streaming and audio.
- Templates, Workplace Assistant, Device Discovery and Keyboard Overlay.
- Import and export profile configurations; review drafts and detect conflicts.
- Locally saved settings; no account required for everyday use.

## Advanced features in current source code

| Area | Capabilities | Limitations |
| --- | --- | --- |
| Result verification | Preflight checks, post-execution checks and verification history | Confirm only verifiable effects; a successful key press does not prove the intended result. |
| Workplace assistant | Prepare profiles and workflows from selected programs, files, URLs and other actions | Preview and saving are required; setup executes no actions. |
| Macro recording and workflows | Recording, conditions, gestures and local profile synchronization | Behavior and limits are documented in the implementation and test reports. |
| UI automation | Select targets, check selectors, record actions and limited click sequences | Separate worker; ambiguous selectors, disabled controls and password fields are rejected; no general text-input recording. |
| Extensions | Inspect, import, enable after explicit trust, export and roll back packages | Process isolation is not a sandbox; not a public plugin marketplace. |
| Mobile panel | Local HTTPS panel, pairing, selected profiles and status/audit history | Real phone/tablet LAN acceptance still open; no universal sensor dashboard. |
| Manufacturer adapters | Corsair, Logitech-G-SDK, Razer/SteelSeries-RGB and Wooting analog adapter | Model/SDK dependent; new adapters not fully validated with real hardware. No new proprietary SDK DLLs bundled. |
| Physical keyboard mapping | Recognize standard keys by device and scan code | Raw Input does not suppress normal Windows input. |
| Local AI meeting assistant | Optional prototype for Ollama, LM Studio, llama.cpp and ONNX Runtime GenAI CPU; produces constrained drafts from explicitly selected resources | Disabled by default; native CPU runtime tested with a random model, but planning quality and time savings are unproven; no model weights included. |

## Not yet ready to offer as finished commercial features

- **Exclusive secondary keyboard/macro pad:** A signed, licensed and hardware-tested filter driver is missing. Normal keystrokes can still reach Windows.
- Complete hardware acceptance of all manufacturer adapters as well as real mobile device LAN tests.
- Real transitions between monitors with different DPI settings and specialist review of all new translations. PerMonitorV2 and simulated 150/200-percent layouts were checked; physical display tests remain separate. The interface provides 27 languages; detailed new text has not yet received complete linguistic review.
- Signed retail installer, final EULA, verified SDK redistribution rights and production purchase-license/checkout integration.

## Details and evidence

- [User documentation](README.md)
- [Extensions: features, tests and open boundaries](testing/2026-10-12-six-extensions.md)
- [Local AI Assistant: Prototype and Testing](testing/2026-10-12-local-meeting-assistant.md)
- [Local AI runtimes and model requirements](testing/2026-10-12-local-ai-runtimes.md)
- [DPI and scaling checks](testing/2026-10-12-dpi-scaling.md)
- [License review](licensing/2026-10-12-commercial-license-audit.md) – historical inventory; the new publishing script adds runtime notices. The final package still requires a separate review.
- [Sales platforms](licensing/makrivo-vertriebsplattformen-2026-10-12.html) – comparison snapshot; Stripe is also a possible payment platform; consult the current provider terms.

## Generate complete development package

```powershell
./tools/publish-complete.ps1 -SelfContained
```

The package contains the desktop app, UI automation worker, mobile host, browser files, example plugin and runtime license notices. Build artifacts are not committed to Git. A GitHub source revision is not a published Windows installer.
