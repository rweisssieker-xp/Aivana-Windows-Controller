<!-- generated-doc locale=it-IT source=docs/VERIFICATION.md sha256=f19afb9e7b5ffac90c83167416ab3af0964a4481dd16b7357f8585480c6aae65 -->

> Traduzione assistita automaticamente. Struttura, esempi di codice e collegamenti vengono verificati; ciò non costituisce una revisione madrelingua o una convalida legale. I rapporti storici conservano le date e l'ambito originali. [Fonte originale](../VERIFICATION.md)

# Esame – 8. Ottobre 2026

[English (US)](../en-US/VERIFICATION.md) | [Deutsch](../de-DE/VERIFICATION.md) | [Français](../fr-FR/VERIFICATION.md) | [Italiano](VERIFICATION.md)

L'applicazione è stata creata su Windows x64 con l'SDK .NET stabile 10.0.202 e rilasciata come Windows-x64-Ausgabe autonomo.

## Controllato automaticamente

- Costruzione complessiva: nessun avviso e nessun errore.
- 19 Test di comportamento principali: configurazione predefinita, convalida, dati nulli/duplicati, priorità del profilo, importazione/esportazione e ripristino del backup, ordine e nuovo tentativo di macro, interruzione, doppio trigger, serializzazione, istantanea di esecuzione immutabile e errore di passaggio.
- Controllo Windows: normalizzazione e alias dei tasti di scelta rapida, dimensioni della struttura nativa, elenco dei dispositivi della tastiera reale, registrazione sicura e ricondivisione, WM_HOTKEY sintetico per riquadro e annullamento, registrazione piastrella disabilitata durante la simulazione dell'input.
- L'EXE autonomo è stato avviato con `--smoke-test`: viene eseguito il rendering di tutte e cinque le visualizzazioni WPF, i riquadri possono essere creati/copiati/eliminati, le bozze vengono conservate durante l'aggiornamento, la configurazione viene salvata e ricaricata. Questo test utilizza una directory di configurazione temporanea isolata e non esegue alcuna azione di testo o multimediale.
- Dashboard, editor e rappresentazione compatta (900 × 720) sono stati ispezionati visivamente come rendering WPF reali. Vengono controllati il ​​salvataggio delle bozze, l'eliminazione, l'ordine dei riquadri/macro, gli alias dei tasti di scelta rapida tedeschi, le singole cancellazioni delle macro e l'avanzamento dei passaggi.

## Limiti dipendenti dall'hardware

L'ultima esecuzione locale ha rilevato quattro interfacce tastiera Windows. Questo numero descrive le interfacce HID/Windows, non necessariamente quattro tastiere fisiche.

L'SDK ufficiale iCUE è installato e si trova accanto all'EXE. Il test live si connette a iCUE e rileva una tastiera Corsair con informazioni sul modello e sul LED. I cambiamenti di illuminazione non venivano attivati ​​automaticamente; Le chiavi speciali dipendenti dal modello e da altri produttori non vengono testate sull'hardware. I tasti di scelta rapida generici si applicano a tutte le tastiere.

L'input di testo/media non è stato inserito in altri programmi utente aperti. Sono state verificate le strutture dati native e la logica di errore/rilascio; Il comportamento in un determinato programma di destinazione dipende tra l'altro dal suo livello di autorizzazione.

## Ripeti

```powershell
dotnet build Aivana.slnx -c Release
dotnet run --project tests/Aivana.Tests -c Release
dotnet run --project tests/Aivana.Windows.Tests -c Release
dotnet publish src/Aivana.Desktop -c Release -r win-x64 --self-contained true -o artifacts/win-x64
& ./artifacts/win-x64/MAKRIVO.exe --smoke-test
```

Il test della GUI scrive il risultato e lo screenshot in `smoke` accanto all'EXE testato. È inteso come modalità diagnostica e non avvia un'istanza in background persistente al termine.


## Controlli avanzati

Inoltre controllati: condizioni del profilo con titolo del processo e della finestra, sostituzione delle variabili senza espansione ricorsiva, anteprima macro senza esecuzione, convalida di nuove azioni e compatibilità con le versioni precedenti dei vecchi file JSON. I controlli nativi coprono la tavolozza dei comandi con registrazione, conflitti, invio e rilascio di WM_HOTKEY, nonché la pura suddivisione del monitor e l'accesso in lettura agli appunti su richiesta. Il test WPF apre ed esegue il rendering della tavolozza effettiva, controlla l'annullamento/ripristino e i preferiti, controlla le bozze pulite dopo il rendering e completa il processo con successo.

Il blocco Windows, le modifiche agli appunti e lo spostamento delle finestre non sono stati applicati agli user agent durante i test. Azioni della finestra di pubblicazione Windows richiede in modo asincrono; Il programma di destinazione specifico può avere dimensioni minime o limiti di autorizzazione propri. Il blocco del profilo e le statistiche di esecuzione si applicano alla sessione corrente.


Esame di estensione da 12.10.2026: Esami fondamentali 30 superati; Adattatore Windows incluso rilascio durante l'iniezione dell'input, rilevamento del monitor di sola lettura e simulazione dello stato OBS superato. WPF controlla il commit/annullamento del flusso, l'avvio/arresto del registratore, l'anteprima dello scambio del profilo, i gesti e le risorse dei simboli. Localizzazione: cataloghi 27 ciascuno con chiavi 967; Documentazione: 84 pagine in quattro lingue. I pulsanti proprietari diversi da Corsair non sono stati testati sull'hardware; Il supporto generico include eventi standard specifici del dispositivo F13–F24 e chiavi multimediali. Il trasporto nel cloud avviene tramite una cartella sincronizzata selezionata; Le importazioni vengono controllate e rimangono disabilitate.
