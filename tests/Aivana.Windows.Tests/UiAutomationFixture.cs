using System.Windows;
using System.Windows.Controls;
using System.Windows.Automation;
namespace Aivana.Windows.Tests;
internal static class UiAutomationFixture
{
    public static void Run()
    {
        var body=new StackPanel();var text=new TextBox { Text="before" };AutomationProperties.SetAutomationId(text,"value");body.Children.Add(text);
        var button=new Button { Content="Invoke test" };AutomationProperties.SetAutomationId(button,"invoke");button.Click+=(_,_)=>text.Text="after";body.Children.Add(button);
        foreach(var name in new[] { "first","second" }) { var duplicate=new Button { Content=name };AutomationProperties.SetAutomationId(duplicate,"duplicate");body.Children.Add(duplicate); }
        var password=new PasswordBox();AutomationProperties.SetAutomationId(password,"password");body.Children.Add(password);
        var disabled=new Button { Content="Disabled",IsEnabled=false };AutomationProperties.SetAutomationId(disabled,"disabled");body.Children.Add(disabled);
        var window=new Window { Title="MAKRIVO isolated UI Automation fixture",Width=420,Height=300,Content=body };
        window.Loaded+=(_,_)=> { Console.WriteLine("READY");Console.Out.Flush(); };
        new Application().Run(window);
    }
}
