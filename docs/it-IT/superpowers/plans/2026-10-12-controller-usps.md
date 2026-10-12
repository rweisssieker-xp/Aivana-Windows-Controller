# Piano di implementazione delle estensioni

Implementare monitor, gesti, flussi, registrazione, diagnostica, scambio portatile dei profili e miglioramenti dell’icona.

Estendere gli strati Core/Windows/Desktop esistenti. Macchine a stati pure e configurazione portatile rimangono in Core, integrazioni native in Windows ed editor modali di bozze in Desktop. Conservare modifiche locali e configurazione distribuita.

**Spec:** ../specs/2026-10-12-controller-usps-design.md

- [x] Servizio monitor, persistenza della destinazione e stato asincrono; verificare identificatori assenti e annullamento.
- [x] Macchina a stati dei gesti e livelli mantenuti; verificare transizioni, ripetizione, ripristino e assegnazioni immediate.
- [x] Condizioni, diramazioni in avanti e regole di errore; verificare istantanee e annullamento.
- [x] Registrazione esplicita delle scorciatoie con revisione della bozza; verificare filtri e rilascio degli hook.
- [x] Scambio tramite cartella condivisa con revisioni e anteprime; verificare conflitti e assegnazioni portatili.
- [x] Tasti standard per dispositivo e diagnostica; verificare elaborazione, duplicati e pausa.
- [x] Risorse delle icone e aspetto in pausa; verificare l’eseguibile pubblicato.
- [x] Integrare accessi dalla palette/editor e traduzioni; eseguire controlli Core, Windows e WPF.
- [x] Pubblicare in una cartella intermedia separata, preservare le impostazioni e riavviare l’app.
