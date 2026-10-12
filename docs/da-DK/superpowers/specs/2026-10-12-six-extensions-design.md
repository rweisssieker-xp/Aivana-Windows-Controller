<!-- generated-doc locale=da-DK source=docs/superpowers/specs/2026-10-12-six-extensions-design.md sha256=a80dda083fbbe90404023ac784b47e7f16f89f72ce0b821e3cda122dc561317f -->

> Maskinassisteret oversættelse. Struktur, kodeeksempler og links kontrolleres; dette er ikke en gennemgang af en modersmålstalende eller en juridisk godkendelse. Historiske rapporter bevarer deres oprindelige datoer og omfang. [Oprindelig kilde](../../../superpowers/specs/2026-10-12-six-extensions-design.md)

# MAKRIVO: seks produktudvidelser og tastaturadaptere

datoer: 2026-10-12
Status: foreslået design til brugeren anmeldelse; implementering er ikke startet.

## Mål
Lever alle seks anmodede udvidelser i den eksisterende Windows/WPF ansøgning, bevarer eksisterende profiler, G1/G2 opgaver, lokal drift og kontofri brug. Udvide tastaturintegrationen til Razer og Logitech, med en genanvendelig kapacitetsbaseret arkitektur for andre producenter. Et producent navn eller tastatur layout er aldrig bevis for, at ejendomsstyringer er understøttet.

## Arkitektur beslutning
Udvidelse Aivana.Core med serialiserbare kontrakter og validering, Aivana.Windows Med Windows Adapter til leverandører og Aivana.Desktop med transaktionelle redaktører og statusvisninger. Brug separate partnerprojekter til isoleret plugin udførelse og den lokale mobile panel service. Undgå en rewrite eller en enkelt privilegeret proces hosting hver forlængelse. Den eksisterende konfiguration skal lastes uændret; tilføje handlingstyper i stedet for at redigere deres vedvarende enum værdier.

Alternativer overvejet: en monolitisk implementering ville indsætte hardware, netværk og arbejdsproces; erstattelse af WPF ville tilføje uforholdsmæssigt migration arbejde. Modulære udvidelser anbefales.

## 1. Uafhængig Macro Pad
Lagring af bånd ved enhedens identitet plus fysisk skanningskode og udvidede nøgleflag. Tilføj eksplicit observering og eksklusive mods, enhed opdagelse, nøgleoptagelse, gentagne suppressioner, gesturer, hold lag og genforbindelse diagnostik. Beskyt hoved/numpad Enter-forskelle og prøv tyske/amerikanske layouter. Bindinger målretter en udvalgt fysisk tastatur; tvetydige genforbindelser kræver genvalg i stedet for at kontrollere en anden enhed.

Raw Input Eksklusiv ophævelse pr. enhed kræver en reel tastaturfilterbackend; aldrig forbundet med globale lavt niveau hooks Raw Input og hævder pålideligt enhed-specifik fjernelse. Giv en overvågning, nødsituation frigivelse, fejl-åbent adfærd og installatør/uninstaller. Ikke fange sikker skrivebord input. Driver installation og eksklusive mode aktivering er udtrykkeligt adskilte bruger handlinger.

Driverport: Vælg en støttet, korrekt licenseret og underskrevet backend, der er kompatibel med målkonfigurationerne Windows 11, eller udvikle/sign en dedikeret filter. Interception er en kandidat til at evaluere, ikke en forpligtet produktionsafhængighed: dens officielle README beskriver test gennem Windows 10 og distinkte kommercielle licenser. Indtil denne port er opfyldt, skal eksklusiv modus vises som utilgængelig; funktionen kan ikke erklæres fuldstændig.

Acceptance: Med to fysiske tastaturer kører den samme nøgle en makro og producerer ingen normale applikationsindtægter kun på det udvalgte eksklusive enhed; andre tastaturindtægterne forbliver uændret. Kontroller opløsning, ulykke, annullering, suspension/resume og driver fjernelse på den virkelige hardware.

## 2. Guide til arbejdspladsen
Tilføj en trollkarriere til arbejdspladsen, møder, streaming, forskning og projektudskiftning. Oplev installerede applikationer, lydudstyr, monitorer og OBS scener. Brugere vælger mål og bindinger; forudse de resulterende skridt og validerer manglende krav. Spare kun efter anmeldelse, med ingen handlinger, der kører under indstillingen. De genererede profiler er redigerbare i eksisterende redaktører.

Acceptance: oprette en arbejdsprofil uden manuelt at redigere udførlige ruter eller enhed ID'er; annullering af indstillingerne uændret; manglende software har en aktiverbar forklaring.

## 3. Bekræftede udførelser
Introduktion af præflightchecks, typede resultat per-step og valgfrie efterbetingelser. Forskellen mellem anmodede, afsluttede, verifiserede, mislykkede, aflystede og uafklarbare stater. Kontroller lyd/mikrofon, lysstyrke, proces/vindue klarhed og OBS tilstand med begrænsede asynkron læsninger. Shortcut/tekstlevering er ikke et bevis på, at den pågældende ansøgning har accepteret kommandoen. Eksterne bivirkninger er ikke universelt reversible; tilbyder kompensation kun, hvor implementeret.

Acceptance: Disconnected explicit mål falder aldrig stille tilbage; fejl observeret tilstand rapporterer aldrig verifiseret succes; annullering stopper spørgsmålet; fejl identificerer skridt og genopretning handling. Eksekutionshistorien glemmer tekstbelastninger og hemmeligheder.

## 4. Plugins og delbare pakker
Versionerede JSON manifesterer handlinger, parametresystemer, statsleverandører, afhængigheder og evner. Run executable plugins i en separat proces over begrænset lokal IPC med handshake, deadlines og annullering. Afvise forstyrrede meddelelser og uforenelige versioner; håndtere ulykker uden at ophøre med den kontrol. Kapacitetserklæringer er afsløring og mellemliggender, ikke krav om sandboxing.

Giv pakkeeksport/import forudsigelse, indholdshasker, vej-traversal beskyttelse, størrelsesgrænser, version/afhængighedskontrol, udtrykkelig tilladelse og opgradering. Importerede implementerbare plugins skal ikke køre automatisk. Inkludere et arbejdsprojekt med en end-to-end install/action/uninstall flow. Der kræves ikke offentlig markedsføring for denne frigivelse.

Acceptance: oprette, eksportere, installere og udføre en reel eksempel integration; demonstrere timeout, crash isolation, afhængighed fejlagtighed, crossal afvisning og disabled import.

## 5. Windows UI Automation
Tilføj et element picker og begrænsede handlinger for opfordring, værdi, udvælgelse, fokus og vente på et element/state. Persist selektorer ved hjælp af proces, automatisering ID, kontrol type og forfædre kontekst; undgå koordinering-kun optagelse. Se udvalget match count og test selektorer læs-only. Passordfelt er udelukket fra optagelse. Behandle ustøttede kontroller og Windows integritetsgrænser udtrykkeligt.

Udvide optagelsen med UIA-aktioner, der beholder nuværende kortspil. Ambigue selektorer mislykkes i stedet for at klikke på en vilkårlig kontrol. En normal browser tilgængelighed træ er ikke et løfte om en fuld DOM automatisering motor.

Acceptance: Automatiser en dedikeret testapplikation med stabile selektorer over position/size ændringer; test ambiguitet, manglende elementer, funktionshindringer, timeouts og annullering.

## 6. Lokal telefon/tablet
Tjen et responsivt panel fra en opt-in LAN-service. Par udtrykkeligt ved hjælp af en kortvarig enkelttidskode og udstede tilbagekaldelige bekræftelser beskyttet lokalt. Brug HTTPS med dokumenteret lokal certifikat tillid/pinning; ingen fald tilbage sending bekræftelser i klage. Deaktivering og begrænsning af konfigurerede grænser. Oprindelig validering, autentiserede kommandoer, prisgrænser og begrænsede meddelelser er obligatoriske.

Eksponere tilladte profiler/aktioner, live status, fremskridt og annullering; brug stabile ID'er og den samme udførelseskanal som desktop og fysiske nøgler. Undgå at udsætte vilkårlige kommandoer, udførelige veje, hemmeligheder eller plugin installation. Giv forbindelse, parring og tilbagekaldelse af skærme. Der er ingen cloud-konto eller internetrelay.

Acceptance: opererer fra en reel telefon/tablet på samme LAN; uafhængige kunder kan ikke liste eller køre handlinger; tilbagekaldte tokener fejler; duplikerede / genspilte kommandoer udfører ikke dobbelt; diskonektion tyst returer handlinger.

## Keyboard fabrikant omfang

| Adapter og familie | Planlagt integration | Ekspliceret grænse |
| --- | --- | --- |
| Corsair | Beskytt strøm iCUE G-key input og RGB integration | Modellen/software kapacitet opdagelse er fortsat nødvendig |
| Logitech G | G-key SDK-adapter med pres/udgivelse og driftstids tilgængelighedskontrol; belysning gennem dets separate SDK, hvor den støttes | Ikke ligge G HUB-detektionen med den støttede G-key API på hver model |
| Razer | Chroma-belysning adapter og guidet Synapse-til-nøgle mapping, plus generisk HID input | Chroma er en belysning API; ikke mærke det indfødte makro-nøgle input |
| SteelSeries | GameSense tilstand / belysning adapter plus generisk HID input | GameSense begivenhed indgivelse er ikke en generel proprietær nøgle optagelse API |
| Wooting | Analog SDK-adapter til støttet analog indtastning, plus normale HID-nøgler | Kapaciteter og SDK-fordelingskrav skal verificeres efter støttet model |
| Keychron/QMK/VIA | Standard HID-nøgler og firmware-mappingsanvisninger; kendte layouter kun med bevis | VIA konfigurationsstøtte er ikke en universel runtime macro-key API |
| Andre standard HID tastaturer | Per-device fysisk nøgleopdagelse og mapping; eksklusivt tilstand kun med den validerede filterbackend | Ingen fremstillet model-specifik Fn, dial eller proprietær nøglestøtte |

Hver enhed viser detekteret, observeret, mappet gennem leverandørsoftware, indfødt SDK, driver-eksklusiv, utilgængelig eller hardware-testeret status som relevant. Bevar kapacitetsbevis med adapterversioner og testede model/firmware-identifikatorer.

## Leveringsordre
1. Konfigurationskompatibilitet, kapacitetskontrakter og verifiseret udførelse.
2. Givet setup ved hjælp af virkelige opdagelser og eksisterende handlingsudførere.
3. UI Automation og dens rekord/testfiksur.
4. Plugin hosting, pakkefløj og eksempelintegration.
5. Læger og mobil verifikation.
6. Udvidet adapter og eksklusiv tastaturbackend integration med virkeligt hardware validering.

Alle seks er nødvendige. Driver signatur/licens, utilgængelige SDK'er eller fraværet af testhardware er frigivelsesdøre, ikke grunde til at erstatte mock adfærd og kalde arbejdet fuldstændig.

## Verifikation og frigivelse kriterier
Udfør eksisterende kerner, Windows og WPF kontrollerer og tilføjer fokuserede integrationstester for hver ny grænse. Kontroller JSON rundrejser og arvprofiler; test importeret eksekuterbar indhold, netværksautentifikation, annullering, plugins ulykker og enhed genforbindelse. Kontroller desktop og mobile layouter. Validerer høj Windows skalaen separat. Bygg udgivelsesartikler kun efter at du har gennemgået kontroller; skelne implementeret/testet, implementeret/hardware-testeret og eksternt blokeret funktionalitet i udgivelsesnoter. Du skal ikke erstatte den installerede applikation, installere drivere, ændre virkelige tastaturmappinger eller åbne netværkstilgang som en tilfældig test.

## Bevis
- Microsoft Raw Input Semantik: https://learn.microsoft.com/en-us/windows/win32/api/winuser/ns-winuser-rawinputdevice
- Logitech G udvikler SDK'er: https://www.logitechg.com/en-us/programs/developer-alliance
- Razer Chroma SDK FAQ: https://developer.razer.com/works-with-chroma/chroma-faq/
- Interception repository og afhængighed betingelser: https://github.com/oblitum/Interception
- Eksisterende lagerproducentindlæg: docs/de-DE/research/tastaturhersteller-sonderfunktionen.md

## Revision af beslutninger
Brugeren bliver bedt om at godkende denne skriftlige design før implementering. Den vigtigste eksterne afhængighed er en produktionsbered eksklusiv indtastning. Den anbefalede rækkevidde bevarer dette som en glat leverbar i stedet for stille at reducere det til Raw Input observationer.
