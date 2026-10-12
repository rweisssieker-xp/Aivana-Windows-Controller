using System.Text.Json;
using System.Threading.Channels;
using System.Windows;
using System.Windows.Controls;
using Aivana.Core;
using Aivana.Windows;
namespace Aivana.Desktop;
public partial class MainWindow
{
    private void ShowUiRecorder()
    {
        if(!ResolvePendingDraft())return;
        if(ActiveProfile==null)return;
        var profileId=ActiveProfile.Id;
        var revision=JsonSerializer.Serialize(_controller.Settings);
        var window=new Window { Owner=this,Title=L("UI-Ablauf aufnehmen"),Width=750,Height=570,Background=_panel,Foreground=System.Windows.Media.Brushes.White };
        window.Resources.MergedDictionaries.Add(Resources);
        var body=new StackPanel { Margin=new Thickness(20) };
        body.Children.Add(Label(L("Aufnahme erfasst angeklickte UI-Elemente und Wartezeiten. Kein normaler Text und keine Passwortfelder. Ergebnis vor der Übernahme prüfen."),14));
        var preview=new TextBox { IsReadOnly=true,AcceptsReturn=true,TextWrapping=TextWrapping.Wrap,Height=280,VerticalScrollBarVisibility=ScrollBarVisibility.Auto };body.Children.Add(preview);
        var state=Label("",12,"#F2B66D");body.Children.Add(state);
        UiAutomationRecorder? recorder=null;CancellationTokenSource? cancellation=null;Task? consumer=null;var steps=new List<ActionStep>();var previousPause=false;
        var accept=Button(L("Als Entwurf übernehmen"),()=>
        {
            if(recorder!=null || consumer is { IsCompleted:false } || steps.Count==0)return;
            if(JsonSerializer.Serialize(_controller.Settings)!=revision || ActiveProfile.Id!=profileId){state.Text=L("Konfiguration wurde geändert. Aufnahme erneut starten.");return;}
            AddTile(ActiveProfile);if(_draftTile==null)return;_draftTile.Name=L("UI-Ablauf");_draftTile.Steps=steps;_selectedStep=0;window.Close();Refresh(forceRender:true);
        },true);accept.IsEnabled=false;
        async Task Stop()
        {
            if(recorder==null && cancellation==null)return;
            recorder?.Dispose();recorder=null;cancellation?.Cancel();if(consumer!=null)try { await consumer; }catch(OperationCanceledException){} cancellation?.Dispose();cancellation=null;_controller.SetPaused(previousPause);accept.IsEnabled=steps.Count>0;
        }
        body.Children.Add(Button(L("Aufnahme starten"),()=>
        {
            if(recorder!=null)return;
            steps.Clear();preview.Text="";accept.IsEnabled=false;previousPause=_controller.Paused;_controller.SetPaused(true);cancellation=new();
            var token=cancellation.Token;var queue=Channel.CreateBounded<(int X,int Y,long Time)>(100);long? last=null;
            try { recorder=new(); }catch(Exception ex){state.Text=ex.Message;_controller.SetPaused(previousPause);return;}
            recorder.Clicked+=(x,y,time)=> { if(!queue.Writer.TryWrite((x,y,time))) { recorder?.Dispose();recorder=null;cancellation?.Cancel();state.Text=L("Aufnahme voll; gestoppt."); } };
            consumer=Consume();
            async Task Consume()
            {
                try
                {
                    await foreach(var click in queue.Reader.ReadAllAsync(token))
                    {
                        try
                        {
                            var data=await AutomationWorkerClient.RequestAsync(new("capture",X:click.X,Y:click.Y),token);
                            var selector=data.Deserialize<UiElementSelector>()!;
                            var op=selector.ControlType switch { "Button" or "Hyperlink" or "MenuItem"=>UiOperation.Invoke,"CheckBox"=>UiOperation.Toggle,"ListItem" or "TabItem" or "RadioButton"=>UiOperation.Select,_=>(UiOperation?)null };
                            if(op==null){state.Text=L("Element unterstützt keine aufnehmbare Klickaktion.");continue;}
                            if(last is long at && click.Time-at>100)steps.Add(new() { Kind=ActionKind.Delay,DelayMs=(int)Math.Min(60000,click.Time-at) });last=click.Time;
                            steps.Add(new() { Kind=ActionKind.UiAutomation,Value=op.ToString()!,UiAutomation=new(op.Value,selector) });
                            preview.Text=string.Join("\n",steps.Select((s,i)=>$"{i+1}. {s.Kind} · {s.UiAutomation?.Selector.Name ?? s.DelayMs.ToString()}"));
                            if(steps.Count>=200){state.Text=L("200 Schritte erreicht. Aufnahme stoppen.");recorder?.Dispose();recorder=null;break;}
                        }
                        catch(OperationCanceledException)when(token.IsCancellationRequested){throw;}
                        catch(Exception ex){state.Text=ex.Message;}
                    }
                }catch(OperationCanceledException)when(token.IsCancellationRequested){}
            }
        }));
        body.Children.Add(Button(L("Aufnahme stoppen"),async ()=>await Stop()));body.Children.Add(accept);
        window.Closed+=async(_,_)=>await Stop();window.Content=body;window.Show();
    }
}
