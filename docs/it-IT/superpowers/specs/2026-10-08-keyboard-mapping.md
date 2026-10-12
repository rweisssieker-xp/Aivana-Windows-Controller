<!-- generated-doc locale=it-IT source=docs/superpowers/specs/2026-10-08-keyboard-mapping.md sha256=fb766a2a2b4698ae3e1c3f63b50bb4f5c605da3dd56c61c05fcc0832b5cf8aeb -->

> Traduzione assistita automaticamente. Struttura, esempi di codice e collegamenti vengono verificati; ciò non costituisce una revisione madrelingua o una convalida legale. I rapporti storici conservano le date e l'ambito originali. [Fonte originale](../../../superpowers/specs/2026-10-08-keyboard-mapping.md)

# Visualizzazione della tastiera fisica per la mappatura

[English (US)](2026-10-08-keyboard-mapping.md) | [Deutsch](2026-10-08-keyboard-mapping.md) | [Français](2026-10-08-keyboard-mapping.md) | [Italiano](2026-10-08-keyboard-mapping.md)

## Obiettivo

La visualizzazione della tastiera ha lo scopo di mostrare le posizioni effettive dei tasti durante l'assegnazione delle azioni. Le linee della stessa larghezza non sono sufficienti: Invio, Maiusc, Spazio, Navigazione, tastierino numerico e i controlli aggiuntivi del produttore differiscono.

## Attuazione

- Catalogo con ID fisici separati, geometria in unità chiave, legende stampate e codici di input Windows.
- ISO-DE e ANSI-US ciascuno in formato completo e TKL; inoltre Corsair K100 RGB, che è stato riconosciuto come modello connesso tramite iCUE.
- Invio ISO con forma a L, tastierino numerico con tasti più/invio alti e zero largo; Navigazione e frecce nelle rispettive posizioni.
- Seleziona esplicitamente modello/layout. Il VID/PID USB e il nome del modello non dimostrano la variante linguistica. I profili generici e i profili modellati sulla base dei documenti del produttore sono chiaramente etichettati in modo diverso; nessuna scala millimetrica o garanzia di compatibilità del firmware.
- Mappa lo scancode sul layout di input attivo Windows in modo che il clic di un tasto fisico non occupi accidentalmente un altro tasto, come Y/Z quando si passa da US a DE.
- L'assegnazione apre una bozza nell'editor di azioni esistente. Il salvataggio, il controllo dei conflitti e l'eliminazione rimangono i meccanismi esistenti. I clic non avviano azioni.
- Sono rappresentati i tasti G proprietari, i tasti profilo/blocco e la ruota iCUE, ma non vengono emessi come normali tasti di scelta rapida Windows senza un adattatore di input implementato. Le scorciatoie configurabili del produttore rimangono una strada possibile.
- Le assegnazioni rimangono tasti di scelta rapida globali Windows; la visualizzazione del dispositivo non implica la soppressione specifica del dispositivo o l'assegnazione esclusiva di un'unica tastiera fisica.

## Prove

[K100-Handbuch con elementi di controllo](https://assets.corsair.com/image/upload/corsairmedia/sys_master/productcontent/AP_K100_RGB_Mechanical_QSG_AD.pdf), [Corsair Dichiarazione ANSI/ISO](https://www.corsair.com/au/en/explorer/gamer/keyboards/ansi-vs-iso-whats-the-best-keyboard-layout/).

## Esame

test di geometria, variante e codice di input; Build e test core/Windows esistenti; WPF fumo per visualizzazione e mappatura senza eseguire o modificare la reale configurazione dell'utente. La ricerca del produttore documenta ulteriori requisiti di modello/SKU indipendentemente dal catalogo di layout attualmente implementato.
