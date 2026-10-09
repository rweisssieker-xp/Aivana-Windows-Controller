# Piano di implementazione del controller Windows

[English (US)](../../../en-US/superpowers/plans/2026-10-08-windows-controller.md) · [Deutsch](../../../de-DE/superpowers/plans/2026-10-08-windows-controller.md) · [Français](../../../fr-FR/superpowers/plans/2026-10-08-windows-controller.md) · [Italiano](../../../it-IT/superpowers/plans/2026-10-08-windows-controller.md)

> Per chi lavora tramite agenti: usare ownership isolata e parallela; l'utente ha richiesto esplicitamente l'implementazione completa con il massimo numero utile di subagenti.

**Obiettivo:** distribuire il pannello hotkey nativo per Windows secondo il design approvato.
**Architettura:** configurazione Core ed esecuzione annullabile; adattatori Windows; composizione desktop WPF.
**Stack tecnologico:** .NET 10, WPF, Win32, JSON. Nessuna dipendenza a pagamento.
**Specifica:** ../specs/2026-10-08-windows-controller-design.md

## Vincoli globali
Interfaccia in tedesco; archiviazione locale; Windows x64; stato delle capacità hardware veritiero; nessuna registrazione della tastiera; avvio automatico disattivato per impostazione predefinita.

## Aspetti della revisione
Gli import non validi non devono sostituire le impostazioni salvate; i modificatori premuti non devono contaminare l'output; lo scollegamento dell'hardware non deve causare arresti anomali; la chiusura deve mantenere disponibile l'area di notifica; le macro annullate devono rilasciare le risorse.

## Attività e responsabilità
- [x] Agente Core: src/Aivana.Core e tests/Aivana.Tests. Modelli, persistenza JSON, convalida, selezione dei profili e MacroRunner. Eseguire i test comportamentali in errore, poi implementare ed eseguire tutti i test Core.
- [x] Agente Windows: src/Aivana.Windows. RegisterHotKey, inventario dispositivi, SendInput, processo in primo piano e adattatore facoltativo per l'illuminazione Corsair. Verificare le dimensioni delle strutture native e la pulizia della registrazione con test smoke sicuri.
- [x] Agente GUI: src/Aivana.Desktop/MainWindow.xaml e .cs, oltre ai soli helper UI. Dashboard scura, editor completo, profili, dispositivi, impostazioni, panoramica tastiera ed editor macro. Eseguire la build quando i contratti sono disponibili.
- [x] Responsabile principale: contratti condivisi, file di progetto, composizione DesktopController, ciclo di vita dell'app/area di notifica, finestre di dialogo per import/export, documentazione, controlli d'integrazione, pubblicazione e test smoke dell'app in esecuzione.

## Interfacce condivise
I contratti sono in Core/Models.cs. L'implementazione del controller è in Desktop/DesktopController.cs. L'interfaccia utente chiama i metodi del controller tramite questa classe concreta. Gli agenti ricevono le firme esatte nelle rispettive attività. Ognuno gestisce file distinti. Il responsabile principale risolve gli errori tra progetti dopo il completamento degli agenti.

## Verifica e consegna
- [x] dotnet build Aivana.slnx -c Release: zero errori.
- [x] dotnet run --project tests/Aivana.Tests -c Release: test comportamentali superati.
- [x] dotnet publish src/Aivana.Desktop -c Release -r win-x64 --self-contained true -o artifacts/win-x64.
- [x] Avviare l'eseguibile pubblicato, esaminare l'interfaccia Windows e la stabilità del processo, provare un flusso sicuro di profilo/modifica/persistenza.
- [x] Scrivere il README con istruzioni di build/avvio e limiti hardware; registrare i controlli e i test hardware ancora necessari.
