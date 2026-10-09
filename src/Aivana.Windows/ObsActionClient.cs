using System.Net;
using System.Net.WebSockets;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using Aivana.Core;
using Localization = Aivana.Core.Localization;

namespace Aivana.Windows;

/// <summary>Read-only inventory returned by OBS Studio WebSocket v5.</summary>
public sealed record ObsInventory(string Version, IReadOnlyList<string> Scenes, IReadOnlyList<string> Inputs);

/// <summary>Executes a bounded set of OBS Studio WebSocket v5 actions.</summary>
public static class ObsActionClient
{
    private const int RpcVersion = 1;
    private const int MaximumMessageBytes = 1024 * 1024;
    private static readonly TimeSpan OperationTimeout = TimeSpan.FromSeconds(10);

    /// <summary>
    /// Executes one OBS action. <paramref name="step"/>.WorkingDirectory optionally contains
    /// the OBS WebSocket endpoint; the default is ws://127.0.0.1:4455.
    /// </summary>
    public static async Task ExecuteAsync(ActionStep step, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(step);
        cancellationToken.ThrowIfCancellationRequested();
        var command = BuildCommand(step);
        var endpoint = ParseEndpoint(step.WorkingDirectory);
        await WithAuthenticatedConnectionAsync(endpoint, cancellationToken,
            "Die Verbindung zu OBS hat das Zeitlimit von 10 Sekunden überschritten. Wenn der Befehl bereits gesendet wurde, kann sein Ergebnis unklar sein.",
            async (socket, token) =>
            {
                _ = await SendRequestAsync(socket, command.RequestType, command.RequestData, token).ConfigureAwait(false);
                return true;
            }).ConfigureAwait(false);
    }

    /// <summary>Discovers the OBS version, scenes, and inputs using read-only requests.</summary>
    public static async Task<ObsInventory> DiscoverAsync(string? endpoint, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        var address = ParseEndpoint(endpoint);
        return await WithAuthenticatedConnectionAsync(address, cancellationToken,
            "Die OBS-Erkennung hat das Zeitlimit von 10 Sekunden überschritten.",
            async (socket, token) =>
            {
                var version = await SendRequestAsync(socket, "GetVersion", null, token, discovery: true).ConfigureAwait(false);
                var scenes = await SendRequestAsync(socket, "GetSceneList", null, token, discovery: true).ConfigureAwait(false);
                var inputs = await SendRequestAsync(socket, "GetInputList", null, token, discovery: true).ConfigureAwait(false);
                return ParseInventory(version, scenes, inputs);
            }).ConfigureAwait(false);
    }

    private static async Task<T> WithAuthenticatedConnectionAsync<T>(Uri endpoint, CancellationToken cancellationToken,
        string timeoutMessage, Func<ClientWebSocket, CancellationToken, Task<T>> operation)
    {
        using var timeout = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
        timeout.CancelAfter(OperationTimeout);

        using var socket = new ClientWebSocket();
        try
        {
            await socket.ConnectAsync(endpoint, timeout.Token).ConfigureAwait(false);
            using var hello = await ReceiveJsonAsync(socket, timeout.Token).ConfigureAwait(false);
            var helloData = RequireData(hello, expectedOpcode: 0);
            var rpcVersion = ReadInt32(helloData, "rpcVersion", defaultValue: RpcVersion);
            var password = ObsCredentials.Read(endpoint.AbsoluteUri) ?? Environment.GetEnvironmentVariable("AIVANA_OBS_PASSWORD");
            var identify = CreateIdentifyMessage(helloData, password, rpcVersion);
            await SendJsonAsync(socket, identify, timeout.Token).ConfigureAwait(false);

            using var identified = await ReceiveJsonAsync(socket, timeout.Token).ConfigureAwait(false);
            _ = RequireData(identified, expectedOpcode: 2);
            return await operation(socket, timeout.Token).ConfigureAwait(false);
        }
        catch (OperationCanceledException) when (!cancellationToken.IsCancellationRequested)
        {
            throw new TimeoutException(Localization.Translate(timeoutMessage));
        }
        catch (WebSocketException ex)
        {
            throw new InvalidOperationException(Localization.Translate((FormattableString)$"Die Verbindung zu OBS ist fehlgeschlagen: {Bound(ex.Message)}."), ex);
        }
        catch (JsonException ex)
        {
            throw new InvalidOperationException(Localization.Translate("OBS hat eine ungültige WebSocket-Nachricht gesendet."), ex);
        }
    }

    private static async Task<JsonElement> SendRequestAsync(ClientWebSocket socket, string requestType,
        IReadOnlyDictionary<string, object>? requestData, CancellationToken cancellationToken, bool discovery = false)
    {
        var requestId = Guid.NewGuid().ToString("D");
        await SendJsonAsync(socket, CreateRequestMessage(requestType, requestId, requestData), cancellationToken).ConfigureAwait(false);
        while (true)
        {
            using var response = await ReceiveJsonAsync(socket, cancellationToken).ConfigureAwait(false);
            var opcode = GetOpcode(response.RootElement);
            if (opcode == 5) continue;
            if (opcode != 7) throw ProtocolError();
            var data = GetData(response.RootElement);
            if (!string.Equals(ReadString(data, "requestId"), requestId, StringComparison.Ordinal)) continue;
            if (!IsSuccessfulResponse(response.RootElement, out var error))
                throw new InvalidOperationException(discovery
                    ? Localization.Translate((FormattableString)$"OBS hat die Anfrage {requestType} abgelehnt: {error}.")
                    : Localization.Translate((FormattableString)$"OBS hat die Aktion {requestType} abgelehnt: {error}."));
            if (!data.TryGetProperty("responseData", out var responseData))
            {
                using var emptyResponse = JsonDocument.Parse("{}");
                return emptyResponse.RootElement.Clone();
            }
            return responseData.Clone();
        }
    }

    internal static ObsInventory ParseInventory(JsonElement versionData, JsonElement sceneData, JsonElement inputData)
    {
        if (versionData.ValueKind != JsonValueKind.Object)
            throw new InvalidOperationException(Localization.Translate("OBS hat keine gültige Versionsantwort für die Erkennung gesendet."));
        var version = ReadString(versionData, "obsVersion");
        if (string.IsNullOrWhiteSpace(version))
            throw new InvalidOperationException(Localization.Translate("OBS hat keine gültige Versionsantwort für die Erkennung gesendet."));
        return new ObsInventory(version, ReadNames(sceneData, "scenes", "sceneName",
            "OBS hat keine gültige Szenenliste für die Erkennung gesendet."),
            ReadNames(inputData, "inputs", "inputName", "OBS hat keine gültige Eingangsliste für die Erkennung gesendet."));
    }

    private static IReadOnlyList<string> ReadNames(JsonElement data, string arrayName, string nameProperty, string error)
    {
        if (data.ValueKind != JsonValueKind.Object || !data.TryGetProperty(arrayName, out var items) || items.ValueKind != JsonValueKind.Array)
            throw new InvalidOperationException(Localization.Translate(error));
        var names = new List<string>();
        foreach (var item in items.EnumerateArray())
        {
            if (item.ValueKind != JsonValueKind.Object || !item.TryGetProperty(nameProperty, out var name) ||
                name.ValueKind != JsonValueKind.String || string.IsNullOrWhiteSpace(name.GetString()))
                throw new InvalidOperationException(Localization.Translate(error));
            names.Add(name.GetString()!);
        }
        return names.AsReadOnly();
    }

    internal static string ComputeAuthentication(string password, string salt, string challenge)
    {
        ArgumentNullException.ThrowIfNull(password);
        ArgumentNullException.ThrowIfNull(salt);
        ArgumentNullException.ThrowIfNull(challenge);
        var secret = Convert.ToBase64String(SHA256.HashData(Encoding.UTF8.GetBytes(password + salt)));
        return Convert.ToBase64String(SHA256.HashData(Encoding.UTF8.GetBytes(secret + challenge)));
    }

    internal static string CreateRequestMessage(string requestType, string requestId, IReadOnlyDictionary<string, object>? requestData = null)
    {
        using var stream = new MemoryStream();
        using (var writer = new Utf8JsonWriter(stream))
        {
            writer.WriteStartObject();
            writer.WriteNumber("op", 6);
            writer.WritePropertyName("d");
            writer.WriteStartObject();
            writer.WriteString("requestType", requestType);
            writer.WriteString("requestId", requestId);
            if (requestData is { Count: > 0 })
            {
                writer.WritePropertyName("requestData");
                writer.WriteStartObject();
                foreach (var (key, value) in requestData)
                {
                    writer.WritePropertyName(key);
                    JsonSerializer.Serialize(writer, value);
                }
                writer.WriteEndObject();
            }
            writer.WriteEndObject();
            writer.WriteEndObject();
        }
        return Encoding.UTF8.GetString(stream.ToArray());
    }

    internal static bool IsSuccessfulResponse(JsonElement root, out string error)
    {
        error = Localization.Translate("Unbekannter OBS-Fehler");
        if (GetOpcode(root) != 7) return false;
        var data = GetData(root);
        if (!data.TryGetProperty("requestStatus", out var status) || status.ValueKind != JsonValueKind.Object) return false;
        var success = status.TryGetProperty("result", out var result) && result.ValueKind == JsonValueKind.True;
        if (success) { error = ""; return true; }
        var code = status.TryGetProperty("code", out var codeElement) && codeElement.TryGetInt32(out var number) ? number.ToString() : "?";
        var comment = status.TryGetProperty("comment", out var commentElement) && commentElement.ValueKind == JsonValueKind.String
            ? Bound(commentElement.GetString()) : Localization.Translate("keine weiteren Angaben");
        error = Localization.Translate((FormattableString)$"Fehler {code}: {comment}");
        return false;
    }

    private static string CreateIdentifyMessage(JsonElement helloData, string? password, int rpcVersion)
    {
        using var stream = new MemoryStream();
        using (var writer = new Utf8JsonWriter(stream))
        {
            writer.WriteStartObject();
            writer.WriteNumber("op", 1);
            writer.WritePropertyName("d");
            writer.WriteStartObject();
            writer.WriteNumber("rpcVersion", rpcVersion > 0 ? Math.Min(rpcVersion, RpcVersion) : RpcVersion);
            if (helloData.TryGetProperty("authentication", out var auth) && auth.ValueKind == JsonValueKind.Object)
            {
                if (string.IsNullOrEmpty(password))
                    throw new InvalidOperationException(Localization.Translate("OBS benötigt ein Passwort. Speichere es in den OBS-Aktionseinstellungen oder setze AIVANA_OBS_PASSWORD für den Benutzer, der Aivana startet."));
                var salt = ReadString(auth, "salt");
                var challenge = ReadString(auth, "challenge");
                if (salt is null || challenge is null) throw ProtocolError();
                writer.WriteString("authentication", ComputeAuthentication(password, salt, challenge));
            }
            writer.WriteNumber("eventSubscriptions", 0);
            writer.WriteEndObject();
            writer.WriteEndObject();
        }
        return Encoding.UTF8.GetString(stream.ToArray());
    }

    internal static (string RequestType, IReadOnlyDictionary<string, object>? RequestData) BuildCommand(ActionStep step)
    {
        var request = step.Value.Trim();
        if (request is "StartRecord" or "StopRecord" or "ToggleRecord" or "PauseRecord" or "ResumeRecord" or
            "StartStream" or "StopStream" or "ToggleStream")
            return (request, null);

        var argument = step.Arguments.Trim();
        if (request == "SetCurrentProgramScene")
        {
            RequireArgument(argument, "Bitte gib den Szenennamen in das Argumentfeld ein.");
            return (request, new Dictionary<string, object> { ["sceneName"] = argument });
        }
        if (request == "ToggleInputMute")
        {
            RequireArgument(argument, "Bitte gib den Namen des OBS-Eingangs in das Argumentfeld ein.");
            return (request, new Dictionary<string, object> { ["inputName"] = argument });
        }
        throw new ArgumentException(Localization.Translate((FormattableString)$"Nicht unterstützter OBS-Befehl: {request}."), nameof(step));
    }

    internal static Uri ParseEndpoint(string? value)
    {
        var text = string.IsNullOrWhiteSpace(value) ? "ws://127.0.0.1:4455" : value.Trim();
        if (!Uri.TryCreate(text, UriKind.Absolute, out var endpoint) || endpoint.Scheme is not ("ws" or "wss") ||
            !string.IsNullOrEmpty(endpoint.UserInfo) || !string.IsNullOrEmpty(endpoint.Query) || !string.IsNullOrEmpty(endpoint.Fragment))
            throw new ArgumentException(Localization.Translate("Die OBS-Adresse muss eine gültige ws://- oder wss://-Adresse ohne Zugangsdaten sein."), nameof(value));
        if (endpoint.Scheme == "ws" && !IsLoopbackHost(endpoint.Host))
            throw new ArgumentException(Localization.Translate("Unverschlüsseltes ws:// ist nur für einen lokalen OBS-Server erlaubt. Verwende für entfernte Server wss://."), nameof(value));
        return endpoint;
    }

    internal static string NormalizeEndpoint(string? value) => ParseEndpoint(value).AbsoluteUri;

    private static bool IsLoopbackHost(string host) => host.Equals("localhost", StringComparison.OrdinalIgnoreCase) ||
        IPAddress.TryParse(host, out var address) && IPAddress.IsLoopback(address);

    private static async Task SendJsonAsync(ClientWebSocket socket, string json, CancellationToken cancellationToken)
    {
        var bytes = Encoding.UTF8.GetBytes(json);
        await socket.SendAsync(bytes.AsMemory(), WebSocketMessageType.Text, endOfMessage: true, cancellationToken).ConfigureAwait(false);
    }

    private static async Task<JsonDocument> ReceiveJsonAsync(ClientWebSocket socket, CancellationToken cancellationToken)
    {
        var buffer = new byte[8192];
        using var stream = new MemoryStream();
        ValueWebSocketReceiveResult result;
        do
        {
            result = await socket.ReceiveAsync(buffer.AsMemory(), cancellationToken).ConfigureAwait(false);
            if (result.MessageType == WebSocketMessageType.Close)
            {
                if (socket.CloseStatus == (WebSocketCloseStatus)4009)
                    throw new InvalidOperationException(Localization.Translate("OBS hat das Passwort abgelehnt. Prüfe das gespeicherte OBS-Passwort und die OBS-WebSocket-Einstellungen."));
                throw new InvalidOperationException(Localization.Translate("OBS hat die Verbindung geschlossen, bevor die Aktion bestätigt wurde."));
            }
            if (result.MessageType != WebSocketMessageType.Text) throw ProtocolError();
            if (stream.Length + result.Count > MaximumMessageBytes) throw ProtocolError();
            stream.Write(buffer, 0, result.Count);
        } while (!result.EndOfMessage);
        return JsonDocument.Parse(stream.ToArray());
    }

    private static JsonElement RequireData(JsonDocument document, int expectedOpcode)
    {
        if (GetOpcode(document.RootElement) != expectedOpcode) throw ProtocolError();
        return GetData(document.RootElement);
    }

    private static int GetOpcode(JsonElement root) => root.ValueKind == JsonValueKind.Object && root.TryGetProperty("op", out var op) && op.TryGetInt32(out var result)
        ? result : throw ProtocolError();

    private static JsonElement GetData(JsonElement root) => root.ValueKind == JsonValueKind.Object && root.TryGetProperty("d", out var data) && data.ValueKind == JsonValueKind.Object
        ? data : throw ProtocolError();

    private static string? ReadString(JsonElement data, string property) =>
        data.TryGetProperty(property, out var value) && value.ValueKind == JsonValueKind.String ? value.GetString() : null;

    private static int ReadInt32(JsonElement data, string property, int defaultValue) =>
        data.TryGetProperty(property, out var value) && value.TryGetInt32(out var result) ? result : defaultValue;

    private static void RequireArgument(string argument, string error)
    {
        if (string.IsNullOrWhiteSpace(argument)) throw new ArgumentException(Localization.Translate(error));
    }

    private static InvalidOperationException ProtocolError() => new(Localization.Translate("Die OBS-WebSocket-Nachricht ist ungültig oder verwendet ein nicht unterstütztes Protokoll."));

    private static string Bound(string? value)
    {
        var text = string.IsNullOrWhiteSpace(value) ? Localization.Translate("keine weiteren Angaben") : value.Replace('\r', ' ').Replace('\n', ' ').Trim();
        return text.Length <= 180 ? text : text[..180];
    }
}
