<!-- generated-doc locale=it-IT source=docs/testing/2026-10-12-dpi-scaling.md sha256=dcf6d4c1a21eec9be9f07a38cab0aff308587c78b85fb2b24c69f04d28d82864 -->

> Traduzione assistita automaticamente. Struttura, esempi di codice e collegamenti vengono verificati; ciò non costituisce una revisione madrelingua o una convalida legale. I rapporti storici conservano le date e l'ambito originali. [Fonte originale](../../testing/2026-10-12-dpi-scaling.md)

# Controllo DPI

MAKRIVO dichiara PerMonitorV2 nel manifesto dell'applicazione incorporata. Il test WPF utilizza GetWindowDpiAwarenessContext e AreDpiAwarenessContextsEqual per verificare il contesto effettivo effettivo della finestra principale.

I test aggiuntivi di layout/griglia simulano la percentuale 150 e 200 su un display 1920 × 1080 con riserva della barra delle applicazioni: dimensioni della finestra logica 1280 × 693 rispettivamente 960 × 520 DIP. Tutte e cinque le pagine principali vengono visualizzate come PNG a 144 o 192 DPI. I file sono chiamati scale-simulated-150-* o scale-simulated-200-*.

Questi test non modificano alcuna impostazione Windows. Non convalidano la vera commutazione del monitor WM_DPICHANGED, la configurazione di monitor misti e la nitidezza effettiva su un display diverso. I test di ridimensionamento fisico rimangono aperti.

L'avviso WinForms dell'analizzatore WFO0003 viene soppresso esclusivamente per questo progetto di avvio WPF: WinForms viene utilizzato per l'integrazione dell'area di notifica, WPF controlla l'avvio; Il DPI viene impostato tramite manifest prima della creazione della finestra.

Microsoft documentazione: https://learn.microsoft.com/en-us/windows/win32/hidpi/setting-the-default-dpi-awareness-for-a-process

Ispezione visiva: il dashboard passa anche alla visualizzazione impilata sotto l'altezza della finestra DIP 650 in modo che le mappe e l'editor non siano schiacciati in un'area centrale troppo piccola.

Banco di prova: rilascio build senza errori/avvisi; Superato il test completo del fumo WPF, incluso il PerMonitorV2-Fensterkontext effettivo e il 150/200-Prozent-Renderings simulato. Lo 200-Prozent-Dashboardaufnahme è stato ispezionato visivamente dopo la regolazione dell'altezza. git diff --controllo superato.
