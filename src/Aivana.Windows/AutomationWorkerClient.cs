using System.Diagnostics;
using System.Text.Json;
using System.IO;
using Aivana.Core;
namespace Aivana.Windows;
public sealed record AutomationRequest(string Method, UiAutomationAction? Action = null, double X = 0, double Y = 0);
public sealed record AutomationResponse(bool Success, JsonElement Data, string Error);
public static class AutomationWorkerClient
{
    public static async Task<JsonElement> RequestAsync(AutomationRequest request, CancellationToken token)
    {
        var executable = Path.Combine(AppContext.BaseDirectory, "Aivana.AutomationHost.exe");
        if (!File.Exists(executable)) throw new FileNotFoundException("UI-Automation-Worker fehlt. Vollständiges Paket verwenden.");
        using var process = new Process { StartInfo = new(executable) { UseShellExecute = false, CreateNoWindow = true, RedirectStandardInput = true, RedirectStandardOutput = true, RedirectStandardError = true } };
        process.Start();
        using var timeout = CancellationTokenSource.CreateLinkedTokenSource(token); timeout.CancelAfter((request.Action?.TimeoutMs ?? 5000) + 5000);
        using var registration = timeout.Token.Register(() => { try { if (!process.HasExited) process.Kill(true); } catch (InvalidOperationException) { } });
        try
        {
        await process.StandardInput.WriteLineAsync(JsonSerializer.Serialize(request)); process.StandardInput.Close();
        var output = new char[65537]; var used = 0;
        while (true) { var read = await process.StandardOutput.ReadAsync(output.AsMemory(used), timeout.Token); if (read == 0) break; used += read; if (used == output.Length) throw new InvalidDataException("Worker response too large."); }
        await process.WaitForExitAsync(timeout.Token);
        var response = JsonSerializer.Deserialize<AutomationResponse>(new string(output, 0, used)) ?? throw new InvalidDataException("Empty worker response.");
        if (!response.Success) throw new InvalidOperationException(response.Error);
        return response.Data;
        }
        finally { try { if (!process.HasExited) process.Kill(true); } catch (InvalidOperationException) { } }
    }
}
