# Vista fisica della tastiera per l'associazione delle azioni

[English (US)](../../../en-US/superpowers/specs/2026-10-08-keyboard-mapping.md) · [Deutsch](../../../de-DE/superpowers/specs/2026-10-08-keyboard-mapping.md) · [Français](../../../fr-FR/superpowers/specs/2026-10-08-keyboard-mapping.md) · [Italiano](../../../it-IT/superpowers/specs/2026-10-08-keyboard-mapping.md)

## Obiettivo

Quando si associano le azioni, la vista della tastiera deve mostrare le posizioni effettive dei tasti. Righe tutte della stessa larghezza non bastano: Invio, Maiusc, Spazio, navigazione, tastierino numerico e controlli aggiuntivi del produttore hanno forme diverse.

## Implementazione

- Catalogo con ID fisici separati, geometria espressa in unità di tasto, legende stampate e codici di input di Windows.
- ISO-DE e ANSI-US sia nel formato completo sia TKL; inoltre Corsair K100 RGB, riconosciuta come modello collegato tramite iCUE.
- Tasto Invio ISO a forma di L, tastierino numerico con tasti Più/Invio alti e zero largo; navigazione e frecce nelle rispettive posizioni.
- Selezione esplicita del modello/layout. VID/PID USB e nome del modello non dimostrano la variante linguistica. I profili generici e quelli modellati sulla documentazione del produttore hanno etichette chiaramente diverse; non viene garantita una misura in millimetri né la compatibilità del firmware.
- Associare lo scancode al layout di input Windows attivo, così il clic su un tasto fisico non associa per errore un tasto diverso, ad esempio Y/Z passando tra US e DE.
- L'associazione apre una bozza nell'editor di azioni esistente. Salvataggio, controllo dei conflitti e scarto mantengono i meccanismi attuali. I clic non avviano azioni.
- I tasti G proprietari, i tasti Profilo/Blocco e la rotella iCUE vengono mostrati, ma non generano hotkey Windows normali senza un adattatore di input implementato. Le scorciatoie configurabili del produttore restano una possibile soluzione.
- Le associazioni restano hotkey globali di Windows; la vista del dispositivo non implica soppressione specifica per dispositivo né l'associazione esclusiva a una singola tastiera fisica.

## Riferimenti

[Manuale K100 con i controlli](https://assets.corsair.com/image/upload/corsairmedia/sys_master/productcontent/AP_K100_RGB_Mechanical_QSG_AD.pdf), [spiegazione Corsair ANSI/ISO](https://www.corsair.com/au/en/explorer/gamer/keyboards/ansi-vs-iso-whats-the-best-keyboard-layout/).

## Verifica

Test di geometria, varianti e codici di input; build e test Core/Windows esistenti; test smoke WPF per visualizzazione e associazione, senza esecuzione di azioni o modifiche alla configurazione reale dell'utente. La ricerca dei produttori documenta requisiti aggiuntivi per modelli/SKU indipendentemente dal catalogo dei layout attualmente implementato.
