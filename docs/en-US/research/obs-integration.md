# OBS Studio integration

[English (US)](../../en-US/research/obs-integration.md) · [Deutsch](../../de-DE/research/obs-integration.md) · [Français](../../fr-FR/research/obs-integration.md) · [Italiano](../../it-IT/research/obs-integration.md)

Aivana uses the OBS Studio obs-websocket v5 protocol over ClientWebSocket; it does not install a third-party OBS client library. The protocol opens with server Hello (opcode 0), answers with Identify (opcode 1), waits for Identified (opcode 2), sends an allowlisted request (opcode 6), then matches its RequestResponse (opcode 7) by request ID. Password authentication follows OBS's documented two-stage SHA-256 challenge calculation.

Enable **Tools → WebSocket Server Settings** in OBS. The default endpoint is **ws://127.0.0.1:4455**. Set the action's **OBS-WebSocket-Adresse** field to use another endpoint. A remote endpoint must use wss://; unencrypted ws:// is accepted only for a loopback address. The endpoint may also include a path or custom port. Aivana rejects credentials embedded in the URL.

When OBS authentication is enabled, save its password in the OBS action editor. Aivana stores it in the current Windows user's Credential Manager as a generic credential named **Aivana.OBS:<normalized endpoint>**. It is never written to an action or settings file. For managed deployments, **AIVANA_OBS_PASSWORD** remains available as a fallback when the endpoint has no saved password. OBS can also remain unauthenticated, in which case neither is needed. Avoid command-line arguments for the password because process command lines can be inspected.

The controller catalog and configuration support **StartRecord**, **StopRecord**, **ToggleRecord**, **PauseRecord**, **ResumeRecord**, **StartStream**, **StopStream**, **ToggleStream**, **SetCurrentProgramScene**, and **ToggleInputMute**. The first eight take no arguments. SetCurrentProgramScene uses the scene name entered in the editor; ToggleInputMute uses its input name. The protocol client accepts the same allowlist.

Each action opens a fresh connection with a 10-second timeout and no automatic retry. This matters for toggle commands: after sending a request, a timeout or dropped connection can leave the outcome unknown, and retrying could toggle the state a second time. OBS request failures are returned with their status code and, when provided, the server comment.

Protocol references:

- [OBS WebSocket v5 protocol specification](https://github.com/obsproject/obs-websocket/blob/master/docs/generated/protocol.md)
- [OBS WebSocket authentication formula and message opcodes](https://github.com/obsproject/obs-websocket/blob/master/docs/generated/protocol.md#creating-an-authentication-string)
- [OBS WebSocket media input request implementation and accepted action enum values](https://github.com/obsproject/obs-websocket/blob/master/src/requesthandler/RequestHandler_MediaInputs.cpp)

The protocol notes and operation names were checked against OBS project documentation on 2026-10-08.
