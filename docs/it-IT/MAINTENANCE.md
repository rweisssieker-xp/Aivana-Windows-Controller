<!-- generated-doc locale=it-IT source=docs/MAINTENANCE.md sha256=9bf5875b50386e698c0574eee390f790d4b1c29cb11288c856c98e03a05877f7 -->

> Traduzione assistita automaticamente. Struttura, esempi di codice e collegamenti vengono verificati; ciò non costituisce una revisione madrelingua o una convalida legale. I rapporti storici conservano le date e l'ambito originali. [Fonte originale](../MAINTENANCE.md)

# Mantenere tutte le lingue di documentazione

La documentazione segue tutti 27 I file generati utilizzano i codici regionali; mappe norvegesi per `nb-NO` Il latino serbo per `sr-Latn`Ogni cartella contiene un indice, un manuale, un'indicazione dettagliata di azione, un inventario di caratteristiche, note di verifica, la ricerca compreso il suo CSV, piani storici e analisi di licenza.

## La fonte della verità

Editare la radice `README.md`, `docs/HANDBOOK.md`, `docs/FEATURES.md`, `docs/VERIFICATION.md`, questa pagina, o i file originali sotto `docs/research`, `docs/testing`, `docs/superpowers` e `docs/licensing`Non ristrutturare un rapporto storico per implicare la validazione di una versione successiva. mantenere la realizzazione, la simulazione, l'accettazione di dispositivi reali e l'approvazione commerciale distinte.

## Generare e valutare

```powershell
python tools/localize_documentation.py --refresh-sources
python tools/check_documentation.py
```

Il generatore online opzionale invia la documentazione al punto finale della traduzione pubblica di Google, seguendo l'output di localizzazione esistente. non legge la configurazione dell'utente, i segreti o i file di modello. esegue solo la documentazione adatta a tale servizio. Blocco di codice, codice inline, URL, struttura della tabella e identificatori tecnici sono protetti. `artifacts/documentation-translation`.

Quando il fornitore online è limitato, `tools/translate_documentation_local.py` riempie lo stesso cache utilizzando un locale esplicitamente fornito M2M100 CTranslate2 Il modello, il 2026-10-12 Snapshot utilizza la licenza MIT facebook/m2m100_418M Modello, convertito come JustFrederik/m2m_100_418m_ct2_int8 alla revisione `1aeed44db4dd61a486bba44acf54c76507082a2c`. CTranslate2 4.8.2 e `SentencePiece` 0.2.2 non sono costruiti solo strumenti. nessun peso di modello o dipendenze di traduzione sono inviati con MAKRIVO. Con il generatore `--cache-only` dopo aver riempito il cache per renderlo senza richieste di rete. l'assistente locale non scarica o esegue il codice modello remoto.

```powershell
python -m pip install ctranslate2==4.8.2 sentencepiece==0.2.2
python tools/translate_documentation_local.py --model C:\models\m2m100-ct2 --refresh-sources
python tools/localize_documentation.py --cache-only
python tools/check_documentation.py
```

Senza `--refresh-sources`, un esercizio riprende il suo snapshot congelato. utilizzare l'opzione di ristrutturazione dopo l'editing originali. segmenti nascosti sono riutilizzati. un esercizio parziale può selezionare i codici locali utilizzando `--locales`• finisce tutti i locali prima di pubblicare. corsi interrotti non sostituiscono silenziosamente la prosa non tradotta per una traduzione fallita.

Il manifesto registra ogni fonte e file generati con SHA-256 impronte digitali di testo normalizzato UTF-8: finiture di linea LF e nessun segno di ordine byte. Il check-in offline verifica la copertura completa per lingua, le impronte digitali, i collegamenti locali, gli esempi di codice, la struttura tabella/CSV, le URL sorgente e i siti non sostituiti. Rifiuta le fonti in acciaio. Un controllo strutturale passante non è un esame o approvazione legale del parlante. Le pagine generate portano visibilmente questa qualifica.

## Pubblicazione e pacchetti

Pubblicare la documentazione, il generatore e il check-in insieme. Preservare i termini di licenza di terze parti originali; l'analisi della licenza tradotta è informativa. Archivi di costruzione, cache di traduzione e file di orchestrazione locali non appartengono a Git. Windows di installazione.

Le traduzioni app facciate dall'utente sono cataloghi JSON separati sotto `src/Aivana.Core/Localization`La generazione di documentazione non modifica questi cataloghi. un'applicazione costruita e la sua documentazione dovrebbe condividere un snapshot versato; il supporto sperimentale recentemente descritto può precederne un pacchetto installato.
