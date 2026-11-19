using System.Windows;
using System.Windows.Controls;
using Aivana.Core;
namespace Aivana.Desktop;
public partial class MainWindow
{
    internal void RunSixExtensionsSmokeChecks(Action<Window,string> snapshot)
    {
        var original=System.Text.Json.JsonSerializer.Serialize(_controller.Settings);
        var openings=new (string Name,Action Open)[] {
#if !MAKRIVO_STORE_NO_AI
("meeting-assistant",ShowMeetingAssistant),
#endif
("extension-keyboards",ShowKeyboardAdapters),("extension-plugins",ShowPlugins),("extension-mobile",ShowMobilePanel),("extension-history",ShowVerificationHistory),("extension-workspace",ShowWorkspaceWizard),("extension-uia-recorder",ShowUiRecorder) };
        foreach(var entry in openings)
        {
            var previous=Application.Current.Windows.Cast<Window>().ToHashSet();entry.Open();
            var added=Application.Current.Windows.Cast<Window>().Where(w=>!previous.Contains(w)).ToArray();
            if(added.Length!=1)throw new InvalidOperationException("Extension entry point did not open: "+entry.Name);
            added[0].UpdateLayout();snapshot(added[0],entry.Name);added[0].Close();
        }
        var body=new StackPanel();AddUiAutomationFields(body,new() { Kind=ActionKind.UiAutomation });
        if(body.Children.Count<4)throw new InvalidOperationException("UIA editor unavailable");
        body.Children.Clear();AddPluginFields(body,new() { Kind=ActionKind.Plugin });
        if(body.Children.Count<3)throw new InvalidOperationException("Plugin editor unavailable");
#if !MAKRIVO_STORE_NO_AI
        RunMeetingDraftSmokeChecks();
#endif
        if(System.Text.Json.JsonSerializer.Serialize(_controller.Settings)!=original)throw new InvalidOperationException("Opening extension views changed settings.");
    }
}
