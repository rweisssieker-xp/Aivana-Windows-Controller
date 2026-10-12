<!-- generated-doc locale=it-IT source=docs/testing/2026-10-12-gui-polish.md sha256=ac39188358e51c68964421d3e855161d607b02a41da2b1a1fad70244f9feff97 -->

> Traduzione assistita automaticamente. Struttura, esempi di codice e collegamenti vengono verificati; ciò non costituisce una revisione madrelingua o una convalida legale. I rapporti storici conservano le date e l'ambito originali. [Fonte originale](../../testing/2026-10-12-gui-polish.md)

# Revisione GUI da 12.10.2026

- Accesso diretto a “Strumenti” nella navigazione; tre gruppi con azioni dichiarate per l'installazione, la creazione e il test.
- Quattordici aree di dialogo esistenti utilizzano una classe di dialogo WPF comune con le risorse della finestra principale, carattere coerente e posizionamento presso il proprietario.
- La dimensione della finestra di dialogo è limitata all'area di lavoro Windows; i contenuti lunghi ricevono barre di scorrimento. Le impostazioni e le pagine del profilo rimangono scorrevoli anche a basse altezze.
- Il filtro delle azioni viene visualizzato solo nella dashboard. La selezione e la ricerca del profilo hanno nomi espliciti per le tecnologie assistive. Le etichette dei campi sono più contrastanti.
- Il test del fumo delle finestre di dialogo include la panoramica degli strumenti e le finestre di dialogo di estensione in 560 × 440 e dimensioni normali; Il registratore esistente e i test linguistici sono stati adattati alla struttura a scorrimento.

Limitazioni: le finestre compatte non sostituiscono i test DPI reali Windows con percentuale 150/200. I nuovi testi tedeschi non sono ancora stati tradotti completamente per tutte le lingue 27. Nessuna nuova convalida hardware e nessuna dichiarazione sull'usabilità misurata.

Banco di prova: rilascio build senza avvisi/errori; Superato il test completo del fumo WPF, inclusi strumenti compatti, finestre di dialogo delle estensioni e controlli del cambio di lingua. git diff --controllo superato.
