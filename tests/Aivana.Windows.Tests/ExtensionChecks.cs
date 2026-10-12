using System.IO;
using System.Diagnostics;
using System.Text.Json;
using Aivana.Core;
using Aivana.Core.Plugins;
using Aivana.Windows;
using Aivana.Windows.KeyboardAdapters;
internal static class ExtensionChecks
{
    public static async Task RunAsync()
    {
        var decoded=LogitechGAdapter.Decode(6 | 256 | 2<<9);
        Require(decoded.Key==6 && decoded.Mode==2 && decoded.Pressed && !decoded.Mouse,"G-key ABI decoding");
        var threshold=new AnalogThreshold();
        Require(threshold.Update("a",4,.7f,.6f,.4f)==true,"Analog down");
        Require(threshold.Update("a",4,.5f,.6f,.4f)==null,"Hysteresis");
        Require(threshold.Update("a",4,.3f,.6f,.4f)==false,"Analog release");
        var preflight=await WindowsActionVerification.PreflightAsync(new() { Kind=ActionKind.Launch,Value=Path.Combine(Path.GetTempPath(),Guid.NewGuid()+".exe") },CancellationToken.None);
        Require(preflight.State==VerificationState.Failed,"Missing executable must fail preflight");
        var unsupported=await WindowsActionVerification.VerifyAsync(new() { Kind=ActionKind.Shortcut,Value="Ctrl+A" },CancellationToken.None);
        Require(unsupported.State==VerificationState.Unverifiable,"Shortcut delivery cannot prove target effect");
        using var stream=new MemoryStream(); var id=Guid.NewGuid();
        await PluginWire.WriteAsync(stream,new(1,id,"hello",JsonSerializer.SerializeToElement(new {})),CancellationToken.None);stream.Position=0;
        Require((await PluginWire.ReadAsync(stream,CancellationToken.None)).Id==id,"Wire round trip");
        using var malformed=new MemoryStream(BitConverter.GetBytes(PluginWire.MaximumBytes+1));
        try { await PluginWire.ReadAsync(malformed,CancellationToken.None);throw new Exception("Oversized message accepted"); } catch(InvalidDataException) { }
        await MeetingModelChecks.RunAsync();
        await PluginRoundTrip();
        await UiAutomationRoundTrip();
        await MobileHostChecks.RunAsync();
        Console.WriteLine("PASS extension verification, adapter ABI, analog hysteresis and real plugin IPC");
    }
    private static async Task PluginRoundTrip()
    {
        var name="MakrivoTest-"+Guid.NewGuid().ToString("N");
        await using var pipe=new System.IO.Pipes.NamedPipeServerStream(name,System.IO.Pipes.PipeDirection.InOut,1,System.IO.Pipes.PipeTransmissionMode.Byte,System.IO.Pipes.PipeOptions.Asynchronous | System.IO.Pipes.PipeOptions.CurrentUserOnly);
        using var process=new Process { StartInfo=new(Path.Combine(AppContext.BaseDirectory,"Aivana.PluginHost.exe")) { UseShellExecute=false,CreateNoWindow=true } };
        process.StartInfo.ArgumentList.Add("--pipe");process.StartInfo.ArgumentList.Add(name);
        using var timeout=new CancellationTokenSource(10000);
        try
        {
            process.Start();await pipe.WaitForConnectionAsync(timeout.Token);var id=Guid.NewGuid();
            await PluginWire.WriteAsync(pipe,new(1,id,"hello",JsonSerializer.SerializeToElement(new {})),timeout.Token);
            Require((await PluginWire.ReadAsync(pipe,timeout.Token)).Id==id,"Plugin handshake");id=Guid.NewGuid();
            await PluginWire.WriteAsync(pipe,new(1,id,"example.countdown",JsonSerializer.SerializeToElement(new { seconds=0 })),timeout.Token);
            var result=await PluginWire.ReadAsync(pipe,timeout.Token);Require(result.Id==id && result.Parameters.GetProperty("completed").GetBoolean(),"Real plugin result");
            await process.WaitForExitAsync(timeout.Token);Require(process.ExitCode==0,"Plugin worker exit");
        }
        finally { if(!process.HasExited)process.Kill(true); }
    }
    private static void Require(bool condition,string message) { if(!condition)throw new Exception(message); }
    private static async Task UiAutomationRoundTrip()
    {
        using var fixture=new Process { StartInfo=new(Environment.ProcessPath!) { UseShellExecute=false,CreateNoWindow=true,RedirectStandardOutput=true } };
        fixture.StartInfo.ArgumentList.Add("--ui-automation-fixture");
        try
        {
            fixture.Start();Require(await fixture.StandardOutput.ReadLineAsync().WaitAsync(TimeSpan.FromSeconds(10))=="READY","UI fixture ready");
            var selector=new UiElementSelector("Aivana.Windows.Tests","invoke","Button");
            var action=new UiAutomationAction(UiOperation.Invoke,selector);
            var count=await AutomationWorkerClient.RequestAsync(new("inspect",action),CancellationToken.None);Require(count.GetInt32()==1,"Unique selector");
            await AutomationWorkerClient.RequestAsync(new("execute",action),CancellationToken.None);
            var failed=false;
            try { await AutomationWorkerClient.RequestAsync(new("execute",action with { Selector=selector with { AutomationId="duplicate" } }),CancellationToken.None); }
            catch(InvalidOperationException){failed=true;} Require(failed,"Ambiguous selector must not invoke");
            failed=false;
            try { await AutomationWorkerClient.RequestAsync(new("execute",action with { Selector=selector with { AutomationId="disabled" } }),CancellationToken.None); }
            catch(InvalidOperationException){failed=true;} Require(failed,"Disabled element must fail");
            var password=new UiAutomationAction(UiOperation.SetValue,new("Aivana.Windows.Tests","password","Edit"),"never",100);
            failed=false;try { await AutomationWorkerClient.RequestAsync(new("execute",password),CancellationToken.None); }catch(InvalidOperationException){failed=true;}Require(failed,"Password excluded");
            Console.WriteLine("PASS isolated UIA invoke, ambiguity, disabled controls and password exclusion");
        }
        finally { if(!fixture.HasExited)fixture.Kill(true); }
    }
}
