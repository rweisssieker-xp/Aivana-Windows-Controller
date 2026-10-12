<!-- generated-doc locale=en-US source=docs/FEATURES.md sha256=ea712f39457ce69f772f09a1294201705a5f36a92a8bf15f356bd80132c3e723 -->

> Machine-assisted translation. Structure, code and links are checked; native-speaker and legal review are not implied. Historical reports retain their original dates and scope. [Original source](../FEATURES.md)

# MAKRIVO – Feature overview

Manufacturer: **Aivana**. Status: **12. October 2026**. This is the development status, not a sales or hardware release.

## Windows control

- Tiles, global hotkeys, command palette, favorites and application-dependent profiles.
- Multi-step, repeat, pause, cancel and preview macros.
- Open programs, files and websites; Use text, clipboard and keyboard shortcuts.
- Arrange windows, lock Windows, control audio outputs and microphones, and change screen brightness via supported interfaces.
- OBS-WebSocket-v5-Anbindung for scenes, recording, streaming and audio.
- Templates, Workplace Assistant, Device Discovery and Keyboard Overlay.
- Import and export of profile configurations; Design review and conflict detection.
- Locally saved settings; no account required for everyday use.

## Advanced features in current source code

| area | Range of functions | Boundaries |
| --- | --- | --- |
| Results check | Pre-inspection, post-inspection and test history | Only confirm verifiable effects; A successful key press does not prove the target effect. |
| Workplace Assistant | Prepare profiles and processes from selected programs, files, URLs and other actions | Preview and save required; Setup does nothing. |
| Macro recording and workflows | Recording, conditions, gestures and local profile synchronization | Behavior and limits are documented in the implementation and test reports. |
| UI automation | Select targets, check selectors, record actions and limited click sequences | Separate worker; ambiguous, disabled and password targets are rejected; no general text input recording. |
| Extensions | Inspect, import, activate based on explicit trust, export and roll back packages | Process isolation is not a sandbox; not a public plugin marketplace. |
| Mobile panel | Local HTTPS panel, pairing, selected profiles and status/audit history | Real phone/tablet LAN acceptance still open; no universal sensor dashboard. |
| Manufacturer adapter | Corsair, Logitech-G-SDK, Razer/SteelSeries-RGB and Wooting analog adapter | Model/SDK dependent; new adapters not fully validated with real hardware. No new proprietary SDK DLLs bundled. |
| Physical keyboard mapping | Recognize standard keys based on device and scan code | Raw Input does not suppress normal Windows input. |
| Local AI meeting assistant | Optional prototype for Ollama, LM Studio, llama.cpp and ONNX Runtime GenAI CPU produces limited designs from explicitly selected resources | Off by default; native CPU runtime tested with random model, but no proven planning quality or time savings; no model weights included. |

## Not yet available as a finished sales feature

- **Exclusive secondary keyboard/macro pad:** A signed, licensed and hardware-tested filter driver is missing. Normal keystrokes can still reach Windows.
- Complete hardware acceptance of all manufacturer adapters as well as real mobile device LAN tests.
- True mixed monitor/DPI changes and technical approval of all new translations. PerMonitorV2 and simulated 150/200-Prozent-Layouts were checked; Real display tests remain separate. The interface offers 27 languages; Detailed new texts have not yet been completely linguistically approved.
- Signed retail installer, final EULA, verified SDK distribution rights, and productive purchase license/checkout integration.

## Details and evidence

- [Service documentation](README.md)
- [Extensions: features, tests and open boundaries](testing/2026-10-12-six-extensions.md)
- [Local AI Assistant: Prototype and Testing](testing/2026-10-12-local-meeting-assistant.md)
- [Local AI times and model requirements](testing/2026-10-12-local-ai-runtimes.md)
- [DPI and Scalation Testing](testing/2026-10-12-dpi-scaling.md)
- [license examination](licensing/2026-10-12-commercial-license-audit.md) – historic recording; the new publish script supplements runtime instructions. The final package remains to be examined separately.
- [Platforms of sale](licensing/makrivo-vertriebsplattformen-2026-10-12.html) – comparison status; Stripe is an additional possible payment platform, see current provider terms.

## Generate complete development package

```powershell
./tools/publish-complete.ps1 -SelfContained
```

The package includes desktop app, UI automation worker, mobile host, browser files, sample plugin and runtime license notes. Build artifacts are not checked into Git. A GitHub source code version is not yet a published Windows installer.
