<!-- generated-doc locale=it-IT source=docs/testing/2026-10-12-github-publication.md sha256=ab4d40cd12047e2cdacad359a12febe16d5689a82f4f792b484b3d4e2430e02a -->

> Traduzione assistita automaticamente. Struttura, esempi di codice e collegamenti vengono verificati; ciò non costituisce una revisione madrelingua o una convalida legale. I rapporti storici conservano le date e l'ambito originali. [Fonte originale](../../testing/2026-10-12-github-publication.md)

# Rilascio GitHub dello stato di sviluppo MAKRIVO

Stato: 12. Ottobre 2026. Rilascio del codice sorgente, non una versione commerciale firmata.

- Compilazione del rilascio della soluzione: riuscita, zero avvisi ed errori.
- Esami principali: superati.
- WPF test fumi: riuscito; nuovo rapporto sui risultati nella cartella di build locale.
- Prima suite completa Windows: superamento controlli finestra in primo piano, audio, schermo e iCUE; quindi la risposta dell'operatore di UI Automation scade.
- Nuovo tentativo mirato `--extensions-only`: riuscito, inclusi automazione dell'interfaccia utente, ambiguità, target disabilitati, esclusione della password, processo di plug-in e host mobile HTTPS reale. Il timeout originale non è stato oscurato da una modifica del timeout; la causa non è stata ancora provata in modo definitivo.
- Riesecuzione completa successiva di Windows: riuscita, inclusi i controlli della finestra in primo piano e di tutte le estensioni. Una riesecuzione riuscita non esclude timeout sporadici.
- Prima della pubblicazione, nell'inventario delle fonti controllate non sono state trovate chiavi private tipiche o chiavi produttive Stripe/GitHub/AWS. Questo controllo a campione limitato non è un controllo segreto completo.
- Gli artefatti della build e i dati di orchestrazione locale non vengono pubblicati.

Dimensioni e rimanenti sconti: [Elenco di Feature](../FEATURES.md). I rapporti di prova precedenti mantengono la loro validità temporanea; non sono una garanzia per qualsiasi hardware o contesto desktop.
