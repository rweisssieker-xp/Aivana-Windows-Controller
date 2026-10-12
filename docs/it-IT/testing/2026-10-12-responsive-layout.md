<!-- generated-doc locale=it-IT source=docs/testing/2026-10-12-responsive-layout.md sha256=8b3710a27f806575e88b4d9a0c4e80de4f26539ea1b5826cfe291764228b4ca6 -->

> Traduzione assistita automaticamente. Struttura, esempi di codice e collegamenti vengono verificati; ciò non costituisce una revisione madrelingua o una convalida legale. I rapporti storici conservano le date e l'ambito originali. [Fonte originale](../../testing/2026-10-12-responsive-layout.md)

# Interfaccia reattiva WPF

Le pagine principali si basano sulla larghezza del contenuto utilizzabile, non sulla larghezza fissa della finestra. Sotto la larghezza del contenuto di 900 DIP, la dashboard e l'editor sono uno sotto l'altro. Sotto 620 DIP, gli indici sono disposti su due colonne. La barra laterale è compressa sotto la larghezza della finestra DIP 820. Dimensione minima: 640 × 480 DIP.

I riquadri del profilo e delle impostazioni e i controlli da tastiera cambiano le colonne in base alla larghezza effettiva della griglia. I controlli esistenti vengono riorganizzati invece di creare nuove voci. Le carte azione dividono la larghezza della linea disponibile. Le barre degli strumenti si rompono. Il lato della tastiera è raggiungibile verticalmente, la tastiera fisica mantiene le sue proporzioni.

Test WPF automatico: tutte e cinque le pagine principali in 640 × 480, 900 × 620 e 1440 × 900; Schermate per pagina; Cambio colonna in entrambe le direzioni con controllo del testo ricevuto. I DIP sono unità logiche WPF. Il vero ridimensionamento Windows alla percentuale 150/200 e più monitor non sono ancora convalidati.

Banco di prova: rilascio della build senza errori/avvisi. Test del fumo completo WPF superato con tutte le dimensioni, modifiche alle colonne, layout della tastiera e controlli della lingua. git diff --controllo superato.
