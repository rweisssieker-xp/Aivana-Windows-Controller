using System.Text.Json;
using Aivana.Core;
using Aivana.Windows;
try
{
    var line = await Console.In.ReadLineAsync();
    if (line == null || line.Length > 65536) return 2;
    var request = JsonSerializer.Deserialize<AutomationRequest>(line)!;
    object? result = request.Method switch
    {
        "capture" => UiAutomationService.CaptureAt(request.X, request.Y),
        "inspect" => UiAutomationService.Resolve(request.Action!.Selector).Count,
        "execute" => await Execute(request.Action!),
        _ => throw new ArgumentException("Unknown automation method")
    };
    Console.WriteLine(JsonSerializer.Serialize(new AutomationResponse(true, JsonSerializer.SerializeToElement(result), ""))); return 0;
}
catch (Exception ex) { Console.WriteLine(JsonSerializer.Serialize(new AutomationResponse(false, JsonSerializer.SerializeToElement(false), ex is OperationCanceledException ? "UI-Zeitlimit überschritten." : "UI-Aktion fehlgeschlagen: " + ex.Message))); return 1; }
static async Task<bool> Execute(UiAutomationAction action) { await UiAutomationService.ExecuteAsync(action, CancellationToken.None); return true; }
