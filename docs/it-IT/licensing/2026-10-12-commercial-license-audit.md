<!-- generated-doc locale=it-IT source=docs/licensing/2026-10-12-commercial-license-audit.md sha256=b58c6861e0b38c4495265651b5ab10349c701d8fb4582dd7d7cae5d2d865bc5f -->

> Traduzione assistita automaticamente. Struttura, esempi di codice e collegamenti vengono verificati; ciò non costituisce una revisione madrelingua o una convalida legale. I rapporti storici conservano le date e l'ambito originali. [Fonte originale](../../licensing/2026-10-12-commercial-license-audit.md)

# MAKRIVO: verificare la vendita commerciale una tantum

Stato: 12. Ottobre 2026. Produttore: Aivana.

## Risultato

Nell'inventario controllato non è stato identificato alcun obbligo generale di rivelare il proprio codice sorgente MAKRIVO. Una vendita proprietaria una tantum sembra possibile, ma non è ancora del tutto pronta. In particolare, l'autorizzazione alla ridistribuzione per la Corsair-SDK-DLL esistente non è documentata. Questa revisione non sostituisce l'approvazione legale.

## Risultati tecnici e di licenza

| componente | Risultati | Passaggio obbligatorio |
| --- | --- | --- |
| Proprio codice sorgente | Nessuna LICENZA, COPIA o AVVISO propria trovata nell'inventario Git esaminato e nella cronologia dei file di licenza disponibili localmente. | Confermare la titolarità dei diritti compresi i contributi, i frammenti di codice adottati e i possibili diritti del datore di lavoro; crea le tue condizioni d'uso. |
| .NET/desktop di Windows | Il pacchetto autonomo esaminato sotto `artifacts/makrivo-win-x64` contiene la versione 10.0.6. I pacchetti runtime locali contengono licenze MIT che consentono la distribuzione commerciale e richiedono l'inclusione di avvisi. Nelle cartelle di output esaminate non sono stati trovati file di licenza/avviso corrispondenti. | Includere i testi esatti della licenza e le informazioni di terze parti applicabili per il runtime effettivamente consegnato nella confezione di vendita. |
| Dipendenze NuGet | Nessuna voce diretta PackageReference trovata nei file di progetto esaminati. Il file delle risorse desktop esaminato contiene le proprie dipendenze di progetto. I pacchetti runtime continuano ad essere rilevanti per la licenza. | Reinventare tutti i progetti, i pacchetti transitivi e i file in bundle al momento del rilascio finale. |
| Corsair iCUE SDK | `artifacts/win-x64/iCUESDK.x64_2019.dll`, versione del file 4.0.84.0. Il PDF di riferimento dell'SDK locale contiene sulle pagine stampate 35–38 l'EULA: utilizzo esente da royalty, ma restrizioni sul trasferimento/assegnazione. Non esisteva un'autorizzazione chiara per trasmettere la DLL ai clienti. | Ottieni una solida autorizzazione alla ridistribuzione prima del raggruppamento. In alternativa, seleziona un pacchetto senza una DLL SDK che utilizza un SDK installato dal cliente; Anche a questo si applicano le condizioni dell'SDK. |
| Pacchetti diversi | `artifacts/makrivo-win-x64` non contiene la DLL Corsair; `artifacts/win-x64` li contiene. L'applicazione cerca dinamicamente l'SDK in più posizioni. | Imposta l'effettivo programma di installazione al dettaglio e controllane il contenuto. La semplice esistenza di un pacchetto senza DLL non conferma tutti i diritti di integrazione. |
| OBS | Possedere il client WebSocket nel codice C# esaminato; nessun pacchetto client OBS, binario OBS o codice GPL adottato identificato. | L'integrazione del protocollo osservata non giustifica di per sé alcun obbligo di divulgazione stabilito dalla GPL. Resta da confermare l'origine degli eventuali frammenti adottati. |
| Icone dell'app | Uno script locale Python disegna simboli geometrici utilizzando Pillow. Lo strumento è una dipendenza di compilazione. | Documentare l'origine e i diritti di tutte le immagini, suoni, caratteri e altre risorse effettivamente consegnate. La sceneggiatura da sola non dimostra i diritti su ciascuna risorsa. |
| Gestione delle licenze | Nessun controllo completo della licenza/attivazione dell'acquisto identificato nel codice dell'applicazione esaminata. | Se lo si desidera, implementare file di licenza offline firmati. L'attivazione tecnica e le condizioni legali di utilizzo sono compiti separati. |

## Modello di acquisto sensato – non ancora implementato

- pagamento una tantum; Diritto d'uso permanente sulla versione principale acquistata.
- Una persona su un massimo di due PC; Consentire espressamente l'uso professionale.
- Include correzioni di bug e aggiornamenti minori; Facoltativamente, offri versioni principali successive a pagamento.
- File di licenza firmato digitalmente che può essere controllato offline; Non fornire mai chiavi di firma private con l'app.
- I diritti legali e gli aggiornamenti di sicurezza richiesti non possono essere esclusi a tutti i livelli dalla regola della versione principale.

## Aperto prima della prima vendita

1. Corsair Chiarisci i diritti di distribuzione o preparati a vendere senza DLL SDK in bundle.
2. Includi testi di licenza e di terze parti per le esatte dipendenze della versione e ricevili nel programma di installazione.
3. Conferma la proprietà dei diritti sul tuo codice e sulle tue risorse; Crea un EULA con regole chiare per dispositivo, utente e aggiornamento.
4. Per le vendite al consumo, gestisci il checkout compresi i requisiti di informazione e l'annullamento. Una dichiarazione generale di “nessun rimborso per i download” non è sufficiente. La scadenza del diritto di recesso nel caso in cui la merce venga messa a disposizione immediatamente richiede gli adempimenti di legge.
5. Ricontrolla il programma di installazione finito inclusi i plugin e tutte le dipendenze transitive. La copia di lavoro viene espansa parallelamente; Questo inventario non è un rilascio di file futuri.

## GitHub

Il repository esaminato è pubblico. Senza una licenza open source, il codice non diventa automaticamente gratuito per qualsiasi ulteriore utilizzo. Devono comunque essere rispettati i diritti della piattaforma GitHub per gli archivi pubblici. Un successivo passaggio a privato non rimuove le copie o i fork esistenti. Visibilità e licenza non sono state modificate durante questa revisione.

## Prove e fonti

- Licenze .NET locali: `C:/Users/weiss/.nuget/packages/microsoft.netcore.app.runtime.win-x64/10.0.6/LICENSE.TXT` e `C:/Users/weiss/.nuget/packages/microsoft.windowsdesktop.app.runtime.win-x64/10.0.6/LICENSE`.
- Avvisi di terze parti .NET locali: `C:/Users/weiss/.nuget/packages/microsoft.netcore.app.runtime.win-x64/10.0.6/THIRD-PARTY-NOTICES.TXT`.
- Locale Corsair SDK: `artifacts/dependencies/iCUESDK_4.0.84.zip`; PDF di riferimento estratto su `sdk/iCUESDK/doc/iCUE SDK - Overview and Reference.pdf`.
- [.NET 10.0.6 Licenza MIT](https://raw.githubusercontent.com/dotnet/runtime/v10.0.6/LICENSE.TXT)
- [Corsair Documentazione SDK con EULA](https://raw.githubusercontent.com/CorsairOfficial/cue-sdk/master/docs/index.html)
- [GitHub: Licenziare i repositori](https://docs.github.com/en/repositories/managing-your-repositorys-settings-and-features/customizing-your-repository/licensing-a-repository)
- [BGB § 327f – Aggiornamenti](https://www.gesetze-im-internet.de/bgb/__327f.html)
- [BGB § 356 – Diritto di revoca](https://www.gesetze-im-internet.de/bgb/__356.html)

Non selezionato: cronologia completa dei contributi esterni e catene di diritti, rilascio del marchio, verifica delle vendite esistenti, futuri pacchetti di plug-in, piena efficacia legale di un EULA non ancora disponibile.
