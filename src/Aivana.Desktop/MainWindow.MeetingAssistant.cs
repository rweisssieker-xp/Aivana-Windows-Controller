using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;
using Aivana.Core;
using Aivana.Windows;
using Microsoft.Win32;
namespace Aivana.Desktop;
public partial class MainWindow
{
    private (Guid Tile,Guid Attempt,string Original,long Started)? _meetingDraft;
    private void RecordMeetingDraft(bool saved)
    {
        if(_meetingDraft is not {} evidence || _draftTile?.Id!=evidence.Tile)return;
        _controller.MeetingExperiment.Record(evidence.Attempt,evidence.Tile,saved?"draft_saved":"draft_discarded",Environment.TickCount64-evidence.Started,saved?SerializeTile(_draftTile)!=evidence.Original:null);
        _meetingDraft=null;
    }
    private void StageMeetingDraft(Tile candidate,Guid profileId,Guid attempt,long setupStarted)
    {
        candidate.MeetingExperimentId=attempt;
        _selectedTileId=candidate.Id;_draftTile=candidate;_draftProfileId=profileId;_selectedStep=0;
        _draftBaseline="";_draftBaselineTileId=candidate.Id;_draftBaselineProfileId=profileId;
        _meetingDraft=(candidate.Id,attempt,SerializeTile(candidate),setupStarted);
        _page="Dashboard";ResetDraftHistory();Refresh(forceRender:true);
    }
    internal void RunMeetingDraftSmokeChecks()
    {
        if(ActiveProfile is not {} profile || HasUnsavedChanges)throw new InvalidOperationException("Isolated clean editor required.");
        var before=profile.Tiles.Count;
        var resource=new MeetingResource("agenda","Agenda",MeetingResourceKind.Document,Path.Combine(Path.GetTempPath(),"MakrivoMeetingSmoke.pdf"));
        var candidate=MeetingPlanner.Compile(new("Besprechung",["agenda"],false,[]),[resource]);
        var attempt=Guid.NewGuid();
        try
        {
            StageMeetingDraft(candidate,profile.Id,attempt,Environment.TickCount64);
            if(profile.Tiles.Count!=before || !HasUnsavedChanges || _draftTile!=candidate)throw new InvalidOperationException("AI handoff must remain an unsaved draft.");
            candidate.Name="Reviewed meeting";
            if(!CommitDraft() || profile.Tiles.Single(t=>t.Id==candidate.Id).MeetingExperimentId!=attempt)throw new InvalidOperationException("Reviewed draft did not persist provenance.");
            var measurement=File.ReadLines(_controller.MeetingExperiment.PathName).Select(line=>JsonSerializer.Deserialize<MeetingMeasurement>(line)!).Last(m=>m.Attempt==attempt);
            if(measurement.Event!="draft_saved" || measurement.Corrected!=true)throw new InvalidOperationException("Reviewed correction not measured.");
            var discarded=MeetingPlanner.Compile(new("Besprechung",["agenda"],false,[]),[resource]);
            StageMeetingDraft(discarded,profile.Id,Guid.NewGuid(),Environment.TickCount64);DiscardDraft();
            if(profile.Tiles.Count!=before+1)throw new InvalidOperationException("Discarded draft was saved.");
        }
        finally { profile.Tiles.RemoveAll(t=>t.Id==candidate.Id);_controller.Save();_draftTile=null;_selectedTileId=null;_draftProfileId=null;_draftBaseline=null;_draftBaselineTileId=null;_draftBaselineProfileId=null;_meetingDraft=null;ResetDraftHistory();Refresh(forceRender:true); }
    }
    private void ShowMeetingAssistant()
    {
        if(!ResolvePendingDraft())return;
        if(ActiveProfile is not {} profile)return;
        var setupStarted=Environment.TickCount64;
        var previews=new MeetingPreviewTracker(_controller.MeetingExperiment,setupStarted);
        var baseline=JsonSerializer.Serialize(_controller.Settings.Profiles);
        var window=new Window { Owner=this,Title=L("KI-Besprechungsassistent · lokal"),Width=760,Height=830,Background=_panel,Foreground=System.Windows.Media.Brushes.White,WindowStartupLocation=WindowStartupLocation.CenterOwner };
        window.Resources.MergedDictionaries.Add(Resources);
        var body=new StackPanel { Margin=new Thickness(20) };
        body.Children.Add(Label(L("Besprechung als Ablauf entwerfen"),22,"#E8F0F1",true));
        body.Children.Add(Label(L("Lokales Experiment mit Ollama. Nur ausgewählte Ressourcen; Entwurf wird niemals automatisch ausgeführt. Kein automatischer Headsetwechsel. Keine Cloudmodelle."),13,"#AABABC"));
        var enabled=new CheckBox { Content=L("Lokalen KI-Besprechungsassistenten aktivieren"),IsChecked=_controller.Settings.MeetingAssistantEnabled,Margin=new Thickness(0,10,0,10) };body.Children.Add(enabled);
        var model=new ComboBox { MinHeight=32 };body.Children.Add(Label(L("Lokal installiertes Modell"),12));body.Children.Add(model);
        var state=Label(L("Ollama auf 127.0.0.1:11434 benötigt. Modelle werden nicht automatisch installiert."),12,"#F2B66D");body.Children.Add(state);
        var goal=new TextBox { AcceptsReturn=true,TextWrapping=TextWrapping.Wrap,Height=70,MaxLength=2000,Text=L("Bereite meine Besprechung vor: öffne Teams und die Agenda. Schalte das Mikrofon stumm.") };body.Children.Add(Label(L("Ziel"),12));body.Children.Add(goal);
        var resources=new List<MeetingResource>();
        var list=new ListBox { Height=105,DisplayMemberPath="Name" };body.Children.Add(Label(L("Freigegebene Programme, Dokumente und Webseiten"),12));body.Children.Add(list);
        Tile? candidate=null;MeetingPlan? plan=null;Guid attempt=Guid.Empty;
        var preview=new TextBox { AcceptsReturn=true,IsReadOnly=true,TextWrapping=TextWrapping.Wrap,Height=125,VerticalScrollBarVisibility=ScrollBarVisibility.Auto };
        var apply=Button(L("Im Editor prüfen"),()=>
        {
            if(candidate==null || !_controller.Settings.MeetingAssistantEnabled)return;
            if(JsonSerializer.Serialize(_controller.Settings.Profiles)!=baseline || ActiveProfile?.Id!=profile.Id || !ResolvePendingDraft()) { state.Text=L("Profil wurde geändert. Assistent erneut öffnen.");return; }
            StageMeetingDraft(candidate,profile.Id,attempt,setupStarted);
            previews.Handoff();window.Close();
        },true);apply.IsEnabled=false;
        CancellationTokenSource? request=null;var busy=false;var closed=false;var revision=0;
        void Invalidate() { previews.Discard();revision++;candidate=null;plan=null;apply.IsEnabled=false;preview.Text="";request?.Cancel(); }
        void RefreshList() { list.ItemsSource=resources.ToArray();Invalidate(); }
        void Pick(MeetingResourceKind kind)
        {
            if(busy)return;
            var picker=new OpenFileDialog { Filter=kind==MeetingResourceKind.Program?"Programme|*.exe":"Dokumente|*.pdf;*.docx;*.xlsx;*.pptx;*.txt;*.md",Multiselect=true };
            if(picker.ShowDialog(window)!=true)return;
            foreach(var file in picker.FileNames) if(resources.Count<24 && !resources.Any(r=>r.Target.Equals(file,StringComparison.OrdinalIgnoreCase))) resources.Add(new(Guid.NewGuid().ToString("N"),Path.GetFileName(file),kind,file));
            RefreshList();
        }
        var buttons=new WrapPanel();buttons.Children.Add(Button(L("Programm hinzufügen"),()=>Pick(MeetingResourceKind.Program)));buttons.Children.Add(Button(L("Dokument hinzufügen"),()=>Pick(MeetingResourceKind.Document)));
        buttons.Children.Add(Button(L("Auswahl entfernen"),()=>{if(!busy && list.SelectedItem is MeetingResource resource){resources.Remove(resource);RefreshList();}}));body.Children.Add(buttons);
        var url=new TextBox { MaxLength=4096 };body.Children.Add(url);
        body.Children.Add(Button(L("Webseite hinzufügen"),()=> { if(busy)return;try { var resource=MeetingPlanner.WebsiteResource(Guid.NewGuid().ToString("N"),url.Text);MeetingPlanner.ValidateRequest(true,"Besprechung",resources.Append(resource).ToArray());resources.Add(resource);url.Clear();RefreshList(); } catch(Exception ex){state.Text=ex.Message;} }));
        var baselineClock=new Stopwatch();
        body.Children.Add(Button(L("Manuelle Einrichtung messen · Start / Ende"),()=> { if(!baselineClock.IsRunning){baselineClock.Restart();state.Text=L("Stoppuhr läuft. Richte den Vergleichsablauf manuell ein und drücke erneut.");}else{baselineClock.Stop();_controller.MeetingExperiment.Record(Guid.NewGuid(),null,"manual_baseline",baselineClock.ElapsedMilliseconds);state.Text=$"Manuelle Einrichtung: {baselineClock.Elapsed.TotalSeconds:F1} s · lokal gespeichert";} }));
        var lifetime=new CancellationTokenSource();
        body.Children.Add(Button(L("Lokale Modelle laden"),async ()=>
        {
            if(busy || enabled.IsChecked!=true)return;
            try { using var client=new MeetingModelClient();var models=await client.ModelsAsync(lifetime.Token);if(closed)return;model.ItemsSource=models;if(models.Length>0)model.SelectedIndex=0;state.Text=models.Length==0?L("Kein lokales Modell gefunden. Ein Modell in Ollama installieren."):L("Lokale Modelle geladen."); }
            catch(OperationCanceledException){} catch(Exception){if(!closed)state.Text=L("Ollama ist nicht erreichbar. Lokalen Dienst starten; keine Installation wurde vorgenommen.");}
        }));
        body.Children.Add(Button(L("KI-Entwurf erstellen"),async ()=>
        {
            if(busy)return;
            try
            {
                MeetingPlanner.ValidateRequest(_controller.Settings.MeetingAssistantEnabled,goal.Text,resources);
                if(model.SelectedItem is not string modelName)throw new ArgumentException(L("Zuerst ein lokales Modell laden und auswählen."));
                Invalidate();busy=true;var current=revision;request=CancellationTokenSource.CreateLinkedTokenSource(lifetime.Token);attempt=Guid.NewGuid();var clock=Stopwatch.StartNew();
                state.Text=L("Lokales Modell erstellt einen Entwurf …");
                try
                {
                    using var client=new MeetingModelClient();var result=await client.PlanAsync(true,goal.Text,modelName,resources.ToArray(),request.Token);
                    if(closed || revision!=current || enabled.IsChecked!=true)return;
                    plan=result;candidate=MeetingPlanner.Compile(plan,resources);
                    previews.Set(attempt,candidate.Id);
                    preview.Text=string.Join("\n",candidate.Steps.Select((s,i)=>$"{i+1}. {s.Kind}: {s.Value}"))+"\n\n"+(plan.Unresolved.Length==0?L("Keine offenen Punkte vom Modell gemeldet. Bitte Zielerfüllung selbst prüfen."):L("Offene Punkte:")+"\n"+string.Join("\n",plan.Unresolved));
                    apply.IsEnabled=true;state.Text=L("Entwurf prüfen. Speichern und Ausführen erfolgen separat im Editor.");
                    _controller.MeetingExperiment.Record(attempt,candidate.Id,"generated",clock.ElapsedMilliseconds);
                }
                catch(OperationCanceledException) { _controller.MeetingExperiment.Record(attempt,null,"generation_cancelled",clock.ElapsedMilliseconds);if(!closed)state.Text=L("Planung abgebrochen oder Zeitlimit erreicht."); }
                catch(Exception) { _controller.MeetingExperiment.Record(attempt,null,"generation_failed",clock.ElapsedMilliseconds);throw; }
            }
            catch(Exception ex) { if(!closed)state.Text=ex is HttpRequestException?L("Lokales Modell nicht erreichbar. Ollama prüfen."):ex.Message; }
            finally { busy=false;request?.Dispose();request=null; }
        },true));
        body.Children.Add(Button(L("Planung abbrechen"),()=>request?.Cancel()));body.Children.Add(preview);body.Children.Add(apply);
        body.Children.Add(Label(L("Messungen bleiben lokal: Dauer, Korrektur und Ausführungszustand; keine Ziele, Namen oder Dateipfade."),12,"#91A3A5"));
        enabled.Click+=(_,_)=> { var previous=_controller.Settings.MeetingAssistantEnabled;_controller.Settings.MeetingAssistantEnabled=enabled.IsChecked==true;try{_controller.Save();Invalidate();}catch(Exception ex){_controller.Settings.MeetingAssistantEnabled=previous;enabled.IsChecked=previous;state.Text=ex.Message;} };
        goal.TextChanged+=(_,_)=>Invalidate();model.SelectionChanged+=(_,_)=>Invalidate();
        window.Closed+=(_,_)=> {previews.Discard();closed=true;lifetime.Cancel();lifetime.Dispose();request?.Cancel();};
        window.Content=new ScrollViewer { Content=body,VerticalScrollBarVisibility=ScrollBarVisibility.Auto };window.Show();
    }
}
