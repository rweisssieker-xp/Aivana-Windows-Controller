# Estensioni di controllo MAKRIVO

L’utente ha richiesto tutti i nove miglioramenti proposti e un’icona riconoscibile nell’area di notifica. Le configurazioni esistenti, in particolare la luminosità su G1/G2, devono rimanere compatibili. Le modifiche locali a marchio e documentazione vengono conservate.

## Risultati e limiti

1. Le azioni di luminosità selezionano tutti i monitor supportati oppure un identificatore stabile specifico. Una destinazione esplicita assente produce un errore visibile. La dashboard legge luminosità, volume di uscita, microfono e OBS in modo asincrono e limitato.
2. I tasti speciali supportano pressione immediata, breve, prolungata e doppia, senza ripetizione automatica. Un selettore di livello mantenuto cambia temporaneamente le assegnazioni; il rilascio le ripristina. Le assegnazioni immediate esistenti restano compatibili. Pausa, cambio profilo, scollegamento e annullamento cancellano i gesti in attesa.
3. L’editor dei flussi modifica condizioni, diramazioni in avanti e regole di errore (arresta/continua) in una bozza della tessera. Le condizioni verificano file, processi e titoli delle finestre. Sono esclusi cicli illimitati di salti.
4. La registrazione avviata esplicitamente acquisisce scorciatoie e attese, ignora gli input iniettati e termina con Esc. Le azioni vengono esaminate prima dell’adozione; il testo normale non viene registrato.
5. I tasti speciali standard per dispositivo usano Windows Raw Input. I tasti proprietari richiedono un’API supportata del produttore o una sua assegnazione a un tasto standard. I limiti sono dichiarati chiaramente. La diagnostica spiega profilo, pausa, conflitti e stato effettivo dell’adattatore.
6. Lo scambio dei profili usa una cartella condivisa scelta, anche sincronizzata da un client cloud esistente. Il controllo delle revisioni impedisce sovrascritture silenziose. Il recupero mostra un’anteprima e importa azioni disattivate senza assegnazioni hardware. L’inclusione dei contenuti delle azioni è esplicita. MAKRIVO non memorizza credenziali cloud.
7. I testi appartengono ai 27 cataloghi esistenti; DE/EN/FR/IT restano le lingue minime della documentazione revisionata. Le lacune nelle traduzioni devono essere dichiarate.
8. Un simbolo di tastiera M turchese sostituisce l’icona Windows generica. La pausa mostra un indicatore ambra. Eseguibile e finestra usano lo stesso simbolo. Il suggerimento mostra stato e profilo.

## Verifica

I controlli Core coprono compatibilità, istantanee, annullamento, tempi dei gesti, diramazioni/errori, filtri di registrazione e conflitti di sincronizzazione. I controlli Windows coprono l’inventario dei monitor in sola lettura, i rilasci SDK e l’elaborazione Raw Input. I controlli WPF verificano nuovi accessi e risorse senza cambiare la luminosità reale, digitare in altre applicazioni o sovrascrivere profili. La distribuzione conserva una copia di sicurezza e la configurazione G1/G2.
