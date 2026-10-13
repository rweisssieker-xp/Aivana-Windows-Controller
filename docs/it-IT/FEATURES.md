<!-- generated-doc locale=it-IT source=docs/FEATURES.md sha256=ea712f39457ce69f772f09a1294201705a5f36a92a8bf15f356bd80132c3e723 -->

> Traduzione assistita automaticamente. Struttura, esempi di codice e collegamenti vengono verificati; ciò non costituisce una revisione madrelingua o una convalida legale. I rapporti storici conservano le date e l'ambito originali. [Fonte originale](../FEATURES.md)

# MAKRIVO – Panoramica delle funzionalità

Produttore: **Aivana**. Stato al **12. ottobre 2026**. Questo è lo stato di sviluppo, non un'approvazione alla vendita o una convalida hardware.

## Controllo Windows

- Riquadri, tasti di scelta rapida globali, tavolozza dei comandi, preferiti e profili dipendenti dall'applicazione.
- Macro multi-passaggio, ripetizione, pausa, annullamento e anteprima.
- Aprire programmi, file e siti Web; Utilizza testo, appunti e scorciatoie da tastiera.
- Disponi le finestre, blocca Windows, controlla le uscite audio e i microfoni e modifica la luminosità dello schermo tramite le interfacce supportate.
- Integrazione OBS WebSocket v5 per scene, registrazione, streaming e audio.
- Modelli, Workplace Assistant, Rilevamento dispositivi e Overlay tastiera.
- Importazione ed esportazione delle configurazioni del profilo; Revisione del progetto e rilevamento dei conflitti.
- Impostazioni salvate localmente; nessun account richiesto per l'uso quotidiano.

## Funzionalità avanzate nel codice sorgente corrente

| zona | Gamma di funzioni | Confini |
| --- | --- | --- |
| Controllo dei risultati | Pre-ispezione, post-ispezione e cronologia dei test | Confermare solo gli effetti verificabili; Una pressione di tasto riuscita non dimostra l'effetto target. |
| Assistente sul posto di lavoro | Prepara profili e processi da programmi, file, URL e altre azioni selezionati | Anteprima e salvataggio richiesti; la configurazione non esegue alcuna azione. |
| Registrazione macro e flussi di lavoro | Registrazione, condizioni, gesti e sincronizzazione del profilo locale | Comportamento e limiti sono documentati nei rapporti di implementazione e di test. |
| Automazione dell'interfaccia utente | Seleziona target, controlla selettori, registra azioni e sequenze di clic limitate | Lavoratore separato; gli obiettivi ambigui, disabilitati e con password vengono rifiutati; nessuna registrazione generale dell'immissione di testo. |
| Estensioni | Ispeziona, importa, attiva in base alla fiducia esplicita, esporta e ripristina i pacchetti | L'isolamento del processo non è una sandbox; non un mercato di plugin pubblico. |
| Pannello mobile | Pannello HTTPS locale, abbinamento, profili selezionati e cronologia stato/controllo | L'accettazione della LAN reale per telefono/tablet è ancora aperta; nessun cruscotto con sensore universale. |
| Adattatore del produttore | Corsair, SDK Logitech G, RGB Razer/SteelSeries e adattatore analogico Wooting | Dipendente dal modello/SDK; nuovi adattatori non completamente convalidati con hardware reale. Nessuna nuova DLL SDK proprietaria in bundle. |
| Mappatura della tastiera fisica | Riconoscere i tasti standard in base al dispositivo e al codice di scansione | Raw Input non sopprime il normale ingresso Windows. |
| Assistente alla riunione AI locale | Prototipo facoltativo per Ollama, LM Studio, llama.cpp e ONNX Runtime GenAI CPU che genera bozze limitate a partire da risorse selezionate esplicitamente | Disattivato per impostazione predefinita; runtime nativo della CPU testato con modello casuale, ma nessuna qualità di pianificazione comprovata o risparmio di tempo; nessun peso del modello incluso. |

## Non ancora disponibile come funzionalità di vendita finita

- **Tastiera secondaria/tastiera macro esclusiva:** Manca un driver del filtro firmato, concesso in licenza e testato dall'hardware. Le normali sequenze di tasti possono comunque raggiungere Windows.
- Completa accettazione hardware di tutti gli adattatori del produttore, nonché test LAN reali sui dispositivi mobili.
- Passaggi reali tra monitor con DPI diversi e convalida tecnica di tutte le nuove traduzioni. Sono stati verificati PerMonitorV2 e layout simulati al 150/200 percento; i test su display reali rimangono separati. L'interfaccia offre 27 lingue; i nuovi testi dettagliati non sono ancora tutti convalidati linguisticamente.
- Programma di installazione al dettaglio firmato, EULA finale, diritti di distribuzione dell'SDK verificati e integrazione di licenza/checkout di acquisto produttivo.

## Dettagli e prove

- [Documentazione di servizio](README.md)
- [Estensioni: funzioni, test e limiti aperti](testing/2026-10-12-six-extensions.md)
- [Assistente locale di intelligenza artificiale: prototipo e test](testing/2026-10-12-local-meeting-assistant.md)
- [Tempo di funzionamento locale e requisiti di modello](testing/2026-10-12-local-ai-runtimes.md)
- [Esame DPI e Scalazione](testing/2026-10-12-dpi-scaling.md)
- [Esame di licenza](licensing/2026-10-12-commercial-license-audit.md) – registrazione storica; il nuovo script di pubblicazione completa le istruzioni di funzionamento. Il pacchetto finale deve essere esaminato separatamente.
- [Piattaforme di vendita](licensing/makrivo-vertriebsplattformen-2026-10-12.html) – stato di confronto; Stripe è inoltre una possibile piattaforma di pagamento, vedi le attuali condizioni del fornitore.

## Genera un pacchetto di sviluppo completo

```powershell
./tools/publish-complete.ps1 -SelfContained
```

Il pacchetto include app desktop, operatore di automazione dell'interfaccia utente, host mobile, file del browser, plug-in di esempio e note sulla licenza di runtime. Gli artefatti di compilazione non vengono archiviati in Git. Una versione del codice sorgente GitHub non è ancora un programma di installazione Windows pubblicato.
