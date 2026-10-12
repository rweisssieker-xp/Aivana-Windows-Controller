<!-- generated-doc locale=it-IT source=docs/superpowers/plans/2026-10-08-windows-controller.md sha256=10a5ba49bbefc617bce40563c668e3c4628647822117087deba766713fb9b3af -->

> Traduzione assistita automaticamente. Struttura, esempi di codice e collegamenti vengono verificati; ciò non costituisce una revisione madrelingua o una convalida legale. I rapporti storici conservano le date e l'ambito originali. [Fonte originale](../../../superpowers/plans/2026-10-08-windows-controller.md)

# Windows Piano di implementazione del controllore

[English (US)](2026-10-08-windows-controller.md) | [Deutsch](2026-10-08-windows-controller.md) | [Français](2026-10-08-windows-controller.md) | [Italiano](2026-10-08-windows-controller.md)

> Per i lavoratori tramite agenti: utilizzare la proprietà isolata parallela; l'utente ha esplicitamente richiesto l'implementazione completa con il massimo degli agenti secondari utili.

**Obiettivo:** Fornire il pannello di tasti di scelta rapida nativo Windows nel design approvato.
**Architettura:** Configurazione core ed esecuzione cancellabile; Adattatori Windows; WPF composizione da scrivania.
**Stack tecnologico:** .NET 10, WPF, Win32, JSON. Nessuna dipendenza a pagamento.
**Spec:** ../specs/2026-10-08-windows-controller-design.md

## Vincoli globali
interfaccia utente tedesca; archiviazione locale; Windowsx64; stato di capacità hardware veritiero; nessuna registrazione da tastiera; nessun avvio automatico predefinito.

## Rivedi Focus
Le importazioni non valide non devono sostituire le impostazioni salvate; i modificatori trattenuti non devono contaminare l'output; l'hardware scollegato non deve bloccarsi; la chiusura deve preservare la disponibilità del vassoio; le macro annullate devono liberare risorse.

## Compiti e proprietà
- [x] Agente principale: src/Aivana.Core e tests/Aivana.Tests. Modelli, persistenza JSON, validazione, selezione del profilo e MacroRunner. Esegui test comportamentali falliti, quindi implementa ed esegui tutti i test principali.
- [x] Windows agente: src/Aivana.Windows. RegisterHotKey, inventario dispositivi, SendInput, processo in primo piano e adattatore di illuminazione Corsair opzionale. Verifica le dimensioni delle strutture native e la pulizia della registrazione con test del fumo sicuri.
- [x] Agente GUI: src/Aivana.Desktop/MainWindow.xaml e .cs più solo helper dell'interfaccia utente. Dashboard scuro, editor completo, profili, dispositivi, impostazioni, panoramica della tastiera ed editor di macro. Costruisci una volta che esistono i contratti.
- [x] Elemento principale: contratti condivisi, file di progetto, composizione di DesktopController, ciclo di vita di applicazioni/vassoi, finestre di dialogo di importazione/esportazione, documentazione, controlli di integrazione, test del fumo di pubblicazione ed esecuzione delle app.

## Interfacce condivise
I contratti risiedono in Core/Models.cs. L'implementazione del controller si trova in Desktop/DesktopController.cs. L'interfaccia utente chiama i metodi del controller tramite quella classe concreta. Gli agenti ricevono firme esatte nei loro compiti. Ciascuno possiede file separati. Il genitore risolve gli errori tra progetti al termine di tutti gli agenti.

## Verifica e consegna
- [x] dotnet build Aivana.slnx -c Versione: zero errori.
- [x] dotnet run --project tests/Aivana.Tests -c Release: controlli comportamentali superati.
- [x] dotnet pubblicare src/Aivana.Desktop -c Release -r win-x64 --self-contenuto true -o artifacts/win-x64.
- [x] Avviare l'eseguibile pubblicato, ispezionare l'interfaccia utente Windows e la stabilità del processo, esercitare un flusso sicuro di profilo/modifica/persistenza.
- [x] Scrivi README con istruzioni di compilazione/avvio e limitazioni hardware; registrare i controlli e i restanti test solo hardware.
