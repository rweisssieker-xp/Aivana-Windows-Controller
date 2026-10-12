using System.Text.Json;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Net.WebSockets;
using System.Text;
using Aivana.Core;
using Aivana.Windows;

internal static class ObsProtocolChecks
{
    public static void Run()
    {
        Check(ObsActionClient.ComputeAuthentication(
            "supersecretpassword",
            "lM1GncleQOaCu9lT1yeUZhFYnqhsLLP1G5lAGo3ixaI=",
            "+IxH4CnCiqpX1rM9scsNynZzbOe4KhDeYcTNS3PDaeY=") == "1Ct943GAT+6YQUUX47Ia/ncufilbe6+oD6lY+5kaCu4=",
            "OBS WebSocket challenge authentication must follow the two-stage SHA-256 specification.");

        using var request = JsonDocument.Parse(ObsActionClient.CreateRequestMessage(
            "SetCurrentProgramScene", "sample-id", new Dictionary<string, object> { ["sceneName"] = "Main & Guest" }));
        var requestRoot = request.RootElement;
        Check(requestRoot.GetProperty("op").GetInt32() == 6, "OBS requests must use opcode 6.");
        Check(requestRoot.GetProperty("d").GetProperty("requestType").GetString() == "SetCurrentProgramScene", "OBS request type must be preserved.");
        Check(requestRoot.GetProperty("d").GetProperty("requestId").GetString() == "sample-id", "OBS request id must be preserved.");
        Check(requestRoot.GetProperty("d").GetProperty("requestData").GetProperty("sceneName").GetString() == "Main & Guest", "Request data must be JSON escaped and preserved.");

        using var good = JsonDocument.Parse("""{"op":7,"d":{"requestStatus":{"result":true,"code":100}}}""");
        Check(ObsActionClient.IsSuccessfulResponse(good.RootElement, out _), "Successful OBS request responses should be accepted.");
        using var bad = JsonDocument.Parse("""{"op":7,"d":{"requestStatus":{"result":false,"code":608,"comment":"Parameter: sceneName"}}}""");
        Check(!ObsActionClient.IsSuccessfulResponse(bad.RootElement, out var error) && error.Contains("608", StringComparison.Ordinal), "OBS request failures should retain their status code.");

        var noArgs = ObsActionClient.BuildCommand(new ActionStep { Value = "ToggleRecord" });
        Check(noArgs.RequestType == "ToggleRecord" && noArgs.RequestData is null, "Output commands should map to the fixed OBS request with no arguments.");
        var scene = ObsActionClient.BuildCommand(new ActionStep { Value = "SetCurrentProgramScene", Arguments = "Main & Guest" });
        Check((string)scene.RequestData!["sceneName"] == "Main & Guest", "Scene changes should send the configured scene name.");
        var mute = ObsActionClient.BuildCommand(new ActionStep { Value = "ToggleInputMute", Arguments = "Mic/Aux" });
        Check((string)mute.RequestData!["inputName"] == "Mic/Aux", "Mute toggle should preserve the named OBS input.");
        Throws<ArgumentException>(() => ObsActionClient.BuildCommand(new ActionStep { Value = "SetInputMute", Arguments = "Mic/Aux|true" }), "Commands outside the controller allowlist should be rejected.");

        Check(ObsActionClient.ParseEndpoint(null).AbsoluteUri == "ws://127.0.0.1:4455/", "The default endpoint should use the OBS loopback port.");
        Check(ObsActionClient.ParseEndpoint("wss://obs.example.test:4455").Scheme == "wss", "Secure remote endpoints should be allowed.");
        Check(ObsCredentials.NormalizeEndpoint("WSS://OBS.EXAMPLE.TEST:443/control") == ObsCredentials.NormalizeEndpoint("wss://obs.example.test/control"), "Credential targets should share a stable, normalized endpoint key.");
        Throws<ArgumentException>(() => ObsActionClient.ParseEndpoint("ws://obs.example.test:4455"), "Unencrypted remote endpoints should be rejected.");
        Throws<ArgumentException>(() => ObsActionClient.ParseEndpoint("ws://user:password@127.0.0.1:4455"), "Endpoint credentials should be rejected.");
        Throws<ArgumentException>(() => ObsActionClient.ParseEndpoint("wss://obs.example.test:4455/control?secret=value"), "Endpoint query strings should be rejected.");
        Throws<ArgumentException>(() => ObsActionClient.BuildCommand(new ActionStep { Value = "RunArbitraryRequest" }), "Unknown request types should never be sent.");

        RunProtocolExchangeAsync(rejectRequest: false).GetAwaiter().GetResult();
        RunProtocolExchangeAsync(rejectRequest: true).GetAwaiter().GetResult();
        RunDiscoveryExchangeAsync().GetAwaiter().GetResult();
        RunDiscoveryExchangeAsync(liveStatus: true).GetAwaiter().GetResult();

        using var versionData = JsonDocument.Parse("""{"obsVersion":"31.0.0"}""");
        using var missingScenes = JsonDocument.Parse("""{}""");
        using var inputData = JsonDocument.Parse("""{"inputs":[]}""");
        Throws<InvalidOperationException>(() => ObsActionClient.ParseInventory(versionData.RootElement, missingScenes.RootElement, inputData.RootElement),
            "Discovery must reject a missing scenes array instead of returning an incomplete inventory.");
        using var malformedInputs = JsonDocument.Parse("""{"inputs":[{"inputName":42}]}""");
        using var sceneData = JsonDocument.Parse("""{"scenes":[]}""");
        Throws<InvalidOperationException>(() => ObsActionClient.ParseInventory(versionData.RootElement, sceneData.RootElement, malformedInputs.RootElement),
            "Discovery must reject malformed input entries instead of returning an incomplete inventory.");
    }

    private static void Check(bool condition, string message)
    {
        if (!condition) throw new InvalidOperationException(message);
    }

    private static void Throws<TException>(Action action, string message) where TException : Exception
    {
        try { action(); }
        catch (TException) { return; }
        throw new InvalidOperationException(message);
    }

    private static async Task RunProtocolExchangeAsync(bool rejectRequest)
    {
        var portProbe = new TcpListener(IPAddress.Loopback, 0);
        portProbe.Start();
        var port = ((IPEndPoint)portProbe.LocalEndpoint).Port;
        portProbe.Stop();

        using var listener = new HttpListener();
        listener.Prefixes.Add($"http://127.0.0.1:{port}/");
        listener.Start();
        using var timeout = new CancellationTokenSource(TimeSpan.FromSeconds(5));
        var server = HandleProtocolClientAsync(listener, rejectRequest, timeout.Token);
        try
        {
            var step = new ActionStep
            {
                Value = "SetCurrentProgramScene",
                Arguments = "Main & Guest",
                WorkingDirectory = $"ws://127.0.0.1:{port}"
            };
            if (rejectRequest)
            {
                try
                {
                    await ObsActionClient.ExecuteAsync(step, timeout.Token).ConfigureAwait(false);
                    throw new InvalidOperationException("OBS rejection should be returned to the action caller.");
                }
                catch (InvalidOperationException ex) when (ex.Message.Contains("608", StringComparison.Ordinal)) { }
            }
            else
            {
                await ObsActionClient.ExecuteAsync(step, timeout.Token).ConfigureAwait(false);
            }
            await server.ConfigureAwait(false);
        }
        finally
        {
            listener.Stop();
        }
    }

    private static async Task HandleProtocolClientAsync(HttpListener listener, bool rejectRequest, CancellationToken cancellationToken)
    {
        var context = await listener.GetContextAsync().WaitAsync(cancellationToken).ConfigureAwait(false);
        if (!context.Request.IsWebSocketRequest)
            throw new InvalidOperationException("OBS client should use an HTTP WebSocket upgrade.");
        var accepted = await context.AcceptWebSocketAsync(subProtocol: null).ConfigureAwait(false);
        using var socket = accepted.WebSocket;

        await SendTextAsync(socket, """{"op":0,"d":{"obsWebSocketVersion":"5.5.2","rpcVersion":1}}""", cancellationToken).ConfigureAwait(false);
        using var identify = await ReceiveJsonAsync(socket, cancellationToken).ConfigureAwait(false);
        var identifyRoot = identify.RootElement;
        Check(identifyRoot.GetProperty("op").GetInt32() == 1, "Client must respond to Hello with Identify.");
        Check(identifyRoot.GetProperty("d").GetProperty("rpcVersion").GetInt32() == 1, "Client should negotiate RPC version 1.");
        Check(identifyRoot.GetProperty("d").GetProperty("eventSubscriptions").GetInt32() == 0, "Client should not subscribe to unused OBS events.");

        await SendTextAsync(socket, """{"op":2,"d":{"negotiatedRpcVersion":1}}""", cancellationToken).ConfigureAwait(false);
        using var request = await ReceiveJsonAsync(socket, cancellationToken).ConfigureAwait(false);
        var data = request.RootElement.GetProperty("d");
        Check(request.RootElement.GetProperty("op").GetInt32() == 6, "Client should send request opcode 6 after identification.");
        Check(data.GetProperty("requestType").GetString() == "SetCurrentProgramScene", "Client should send the requested scene action.");
        Check(data.GetProperty("requestData").GetProperty("sceneName").GetString() == "Main & Guest", "Client should preserve the scene name.");
        var requestId = data.GetProperty("requestId").GetString();
        var result = rejectRequest
            ? """{"result":false,"code":608,"comment":"Parameter: sceneName"}"""
            : """{"result":true,"code":100}""";
        var response = "{\"op\":7,\"d\":{\"requestType\":\"SetCurrentProgramScene\",\"requestId\":\"" + requestId + "\",\"requestStatus\":" + result + "}}";
        await SendTextAsync(socket, response, cancellationToken).ConfigureAwait(false);
        await WaitForClientDisconnectAsync(socket, cancellationToken).ConfigureAwait(false);
    }

    private static async Task RunDiscoveryExchangeAsync(bool liveStatus = false)
    {
        var portProbe = new TcpListener(IPAddress.Loopback, 0);
        portProbe.Start();
        var port = ((IPEndPoint)portProbe.LocalEndpoint).Port;
        portProbe.Stop();

        using var listener = new HttpListener();
        listener.Prefixes.Add($"http://127.0.0.1:{port}/");
        listener.Start();
        using var timeout = new CancellationTokenSource(TimeSpan.FromSeconds(5));
        var server = HandleDiscoveryClientAsync(listener, timeout.Token, liveStatus);
        try
        {
            if (liveStatus)
            {
                var status = await ObsActionClient.ReadStatusAsync($"ws://127.0.0.1:{port}", timeout.Token).ConfigureAwait(false);
                Check(status.Recording && !status.Streaming && status.Scene == "Main", "Live status must retain recording, streaming and current scene state.");
            }
            else
            {
            var inventory = await ObsActionClient.DiscoverAsync($"ws://127.0.0.1:{port}", timeout.Token).ConfigureAwait(false);
            Check(inventory.Version == "31.0.0", "Discovery should return the OBS version.");
            Check(inventory.Scenes.SequenceEqual(["Main", "Guest"]), "Discovery should return scene names in server order.");
            Check(inventory.Inputs.SequenceEqual(["Mic/Aux", "Desktop Audio"]), "Discovery should return input names in server order.");
            }
            await server.ConfigureAwait(false);
        }
        finally
        {
            listener.Stop();
        }
    }

    private static async Task HandleDiscoveryClientAsync(HttpListener listener, CancellationToken cancellationToken, bool liveStatus)
    {
        var context = await listener.GetContextAsync().WaitAsync(cancellationToken).ConfigureAwait(false);
        if (!context.Request.IsWebSocketRequest)
            throw new InvalidOperationException("OBS client should use an HTTP WebSocket upgrade.");
        var accepted = await context.AcceptWebSocketAsync(subProtocol: null).ConfigureAwait(false);
        using var socket = accepted.WebSocket;

        await SendTextAsync(socket, """{"op":0,"d":{"obsWebSocketVersion":"5.5.2","rpcVersion":1}}""", cancellationToken).ConfigureAwait(false);
        using var identify = await ReceiveJsonAsync(socket, cancellationToken).ConfigureAwait(false);
        Check(identify.RootElement.GetProperty("op").GetInt32() == 1, "Discovery client must identify before requests.");
        await SendTextAsync(socket, """{"op":2,"d":{"negotiatedRpcVersion":1}}""", cancellationToken).ConfigureAwait(false);

        var expected = liveStatus ? new[]
        {
            ("GetRecordStatus", """{"outputActive":true}"""),
            ("GetStreamStatus", """{"outputActive":false}"""),
            ("GetCurrentProgramScene", """{"currentProgramSceneName":"Main"}""")
        } : new[]
        {
            ("GetVersion", """{"obsVersion":"31.0.0"}"""),
            ("GetSceneList", """{"scenes":[{"sceneName":"Main"},{"sceneName":"Guest"}]}"""),
            ("GetInputList", """{"inputs":[{"inputName":"Mic/Aux"},{"inputName":"Desktop Audio"}]}""")
        };
        foreach (var (requestType, responseData) in expected)
        {
            using var request = await ReceiveJsonAsync(socket, cancellationToken).ConfigureAwait(false);
            var data = request.RootElement.GetProperty("d");
            Check(request.RootElement.GetProperty("op").GetInt32() == 6, "Discovery requests should use opcode 6.");
            var actualType = data.GetProperty("requestType").GetString();
            Check(actualType == requestType, $"Discovery should send {requestType} in sequence.");
            Check(actualType is "GetVersion" or "GetSceneList" or "GetInputList" or "GetRecordStatus" or "GetStreamStatus" or "GetCurrentProgramScene", "Discovery must only send read-only requests.");
            var requestId = data.GetProperty("requestId").GetString();
            var response = "{\"op\":7,\"d\":{\"requestType\":\"" + requestType + "\",\"requestId\":\"" + requestId +
                "\",\"requestStatus\":{\"result\":true,\"code\":100},\"responseData\":" + responseData + "}}";
            await SendTextAsync(socket, response, cancellationToken).ConfigureAwait(false);
        }
        await WaitForClientDisconnectAsync(socket, cancellationToken).ConfigureAwait(false);
    }

    // Keep the final response readable until the client has consumed it and disposed its socket.
    // Disposing the server immediately after SendAsync races the client's final ReceiveAsync.
    private static async Task WaitForClientDisconnectAsync(WebSocket socket, CancellationToken token)
    {
        try
        {
            var result = await socket.ReceiveAsync(new byte[1].AsMemory(), token).ConfigureAwait(false);
            Check(result.MessageType == WebSocketMessageType.Close, "Client should disconnect after the final response.");
        }
        catch (WebSocketException) { /* ClientWebSocket.Dispose aborts without a close handshake. */ }
    }

    private static async Task SendTextAsync(WebSocket socket, string text, CancellationToken cancellationToken)
    {
        var bytes = Encoding.UTF8.GetBytes(text);
        await socket.SendAsync(bytes.AsMemory(), WebSocketMessageType.Text, endOfMessage: true, cancellationToken).ConfigureAwait(false);
    }

    private static async Task<JsonDocument> ReceiveJsonAsync(WebSocket socket, CancellationToken cancellationToken)
    {
        using var stream = new MemoryStream();
        var buffer = new byte[4096];
        ValueWebSocketReceiveResult result;
        do
        {
            result = await socket.ReceiveAsync(buffer.AsMemory(), cancellationToken).ConfigureAwait(false);
            if (result.MessageType != WebSocketMessageType.Text)
                throw new InvalidOperationException("The OBS client should send text JSON messages.");
            stream.Write(buffer, 0, result.Count);
        } while (!result.EndOfMessage);
        return JsonDocument.Parse(stream.ToArray());
    }
}
