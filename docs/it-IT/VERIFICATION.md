# Verifica — 8 ottobre 2026

[English (US)](../en-US/VERIFICATION.md) · [Deutsch](../de-DE/VERIFICATION.md) · [Français](../fr-FR/VERIFICATION.md) · [Italiano](../it-IT/VERIFICATION.md)

L'applicazione è stata compilata su Windows x64 con .NET SDK stabile 10.0.202 e pubblicata come pacchetto autonomo per Windows x64.

## Controlli automatizzati

- Build completa: nessun avviso o errore.
- 19 test comportamentali Core: configurazione predefinita, convalida, dati nulli/duplicati, priorità dei profili, import/export e ripristino del backup, ordine e ripetizione delle macro, annullamento, attivazioni duplicate, serializzazione, snapshot di esecuzione immutabile ed errori nei passaggi.
- Verifica Windows: normalizzazione degli hotkey e nomi alias, dimensioni delle strutture native, elenco reale dei dispositivi tastiera, registrazione sicura e nuovo rilascio, WM_HOTKEY sintetico per riquadro e annullamento, registrazione disattivata dei riquadri durante la simulazione dell'input.
- L'EXE autonoma è stata avviata con `--smoke-test`: vengono renderizzate tutte e cinque le viste WPF; si possono creare, copiare ed eliminare riquadri; le bozze restano dopo l'aggiornamento; la configurazione viene salvata e ricaricata. Il test usa una directory temporanea isolata per la configurazione e non esegue azioni di testo o multimediali.
- Dashboard, editor e vista compatta (900 × 720) sono stati esaminati visivamente tramite rendering WPF reale. Sono stati verificati salvataggio delle bozze, scarto, ordine di riquadri/macro, alias hotkey tedeschi, annullamenti singoli di macro e avanzamento dei passaggi.

## Limiti legati all'hardware

L'ultima esecuzione locale ha rilevato quattro interfacce tastiera Windows. Questo numero descrive le interfacce HID/Windows, non necessariamente quattro tastiere fisiche.

L'SDK ufficiale iCUE è installato e si trova accanto all'EXE. Il test dal vivo si collega a iCUE e rileva una tastiera Corsair con informazioni sul modello e sui LED. Le modifiche all'illuminazione non sono state attivate automaticamente; i tasti speciali specifici dei modelli e quelli di altri produttori non sono stati testati sull'hardware. Gli hotkey generici valgono per tutte le tastiere.

Gli input di testo/multimediali non sono stati inviati ad altri programmi utente aperti. Sono state controllate le strutture dati native e la logica di errore/rilascio; il comportamento in uno specifico programma di destinazione dipende anche dal suo livello di autorizzazione.

## Ripetere i controlli

```powershell
dotnet build Aivana.slnx -c Release
dotnet run --project tests/Aivana.Tests -c Release
dotnet run --project tests/Aivana.Windows.Tests -c Release
dotnet publish src/Aivana.Desktop -c Release -r win-x64 --self-contained true -o artifacts/win-x64
& ./artifacts/win-x64/MAKRIVO.exe --smoke-test
```

Il test GUI scrive risultati e screenshot in `smoke`, accanto all'EXE verificata. È una modalità diagnostica e al termine non avvia un'istanza permanente in background.

## Controlli estesi

Inoltre sono stati verificati: condizioni del profilo con processo e titolo della finestra, sostituzione delle variabili senza espansione ricorsiva, anteprima delle macro senza esecuzione, nuova convalida delle azioni e compatibilità con i vecchi file JSON. I controlli nativi includono la tavolozza dei comandi con registrazione, conflitti, dispatch e rilascio di WM_HOTKEY, la sola suddivisione dei monitor e la lettura degli appunti su richiesta. Il test WPF apre e renderizza la tavolozza reale, verifica Annulla/Ripristina e preferiti, controlla che le bozze restino pulite dopo il rendering e termina correttamente il processo.

Durante i test non sono stati applicati a programmi dell'utente il blocco di Windows, modifiche agli appunti o spostamenti delle finestre. Le azioni finestra inviano richieste asincrone a Windows; il programma di destinazione può imporre dimensioni minime o limiti di autorizzazione. Il blocco del profilo e le statistiche di esecuzione valgono per la sessione corrente.

Verifica delle estensioni del 12 ottobre 2026: superati 30 controlli Core e i controlli Windows, inclusi rilascio durante l’iniezione, identificazione dei monitor in sola lettura e simulazioni OBS. WPF verifica adozione/annullamento dei flussi, avvio/arresto del registratore, anteprime dello scambio profili, gesti e icone. Localizzazione: 27 cataloghi da 967 chiavi; documentazione: 84 pagine in quattro lingue. I tasti proprietari oltre Corsair non sono stati verificati su hardware; il supporto generico copre F13–F24 ed eventi multimediali standard per dispositivo. Il trasporto cloud usa una cartella sincronizzata scelta; le importazioni richiedono revisione e rimangono disabilitate.
