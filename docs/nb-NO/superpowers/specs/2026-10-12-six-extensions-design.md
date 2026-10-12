<!-- generated-doc locale=nb-NO source=docs/superpowers/specs/2026-10-12-six-extensions-design.md sha256=a80dda083fbbe90404023ac784b47e7f16f89f72ce0b821e3cda122dc561317f -->

> Maskinassistert oversettelse. Struktur, kodeeksempler og lenker kontrolleres; dette innebærer ikke gjennomgang av en morsmålsbruker eller juridisk godkjenning. Historiske rapporter beholder sine opprinnelige datoer og omfang. [Originalt kilde](../../../superpowers/specs/2026-10-12-six-extensions-design.md)

# MAKRIVO: seks produktutvidelser og tastaturadaptere

Datoer: 2026-10-12
Status: foreslått design for brukervurdering; implementering har ikke begynt.

## Mål
Lever alle seks forespurte utvidelser i den eksisterende Windows/WPF appen, beholde eksisterende profiler, G1/G2 oppdrag, lokal drift og kontofri bruk. Utvid tastaturintegrasjon til Razer og Logitech, med en gjenbrukbar kapasitetsbasert arkitektur for andre produsenter. Et produsent navn eller tastatur layout er aldri bevis på at proprietære kontroller støttes.

## Arkitektur avgjørelse
Utvidelse Aivana.Core med serialiserbare kontrakter og validering, Aivana.Windows Med Windows og leverandør adapter, og Aivana.Desktop med transaksjonsredaktører og statusvisninger. Bruk separate partnerprosjekter for isolert plugin utførelse og den lokale mobilpaneletjenesten. Unngå en rewrite eller en enkelt privilegert prosess hosting hver utvidelse. eksisterende konfigurasjon må lastes uendret; legge til handlingstyper i stedet for å redigere sine vedvarende enum verdier.

Alternativer vurdert: en monolitisk implementering ville innsette maskinvare, nettverk og arbeidsflyt utførelse; å erstatte WPF ville legge til uforholdsmessig migrasjon arbeid. Modulære utvidelser anbefales.

## 1. Uavhengig makropad
Lagre bånd ved enhetsidentitet plus fysisk skannekode og utvidede nøkkelflagger. Legg til eksplisitte observasjons- og eksklusive moduser, enhetsoppdagelse, nøkkelfanger, gjenta undertrykkelse, gesturer, hold lag og gjenkoble diagnoser. Oppbevar hoved/numpad Enter-forskjeller og prøv tysk/amerikansk layout. Bindinger tar sikte på et valgt fysisk tastatur; ambisiøse gjenkoblede identiteter krever re-seleksjon i stedet for å kontrollere en annen enhet.

Raw Input Exclusive per-device fjerning krever en ekte tastaturfilter bakgrunnen; aldri korrelere globale lavnivå hoke med Raw Input og hevder pålitelig enhetspecifik sletting. gi en overvåking, nødsituasjon, feil-åpen oppførsel og installatør/uninstaller. ikke fange sikker skrivebordinnføring. Driver installasjon og eksklusive modus aktivering er eksplisitt separate brukere handlinger.

Driver gate: Velg en støttet, riktig lisensiert og signert backend som er kompatibel med målet Windows 11 konfigurasjoner, eller utvikle/sign en dedikert filter. Interception er en kandidat til å vurdere, ikke en forpliktet produksjonsavhengighet: dens offisielle README beskriver testing gjennom Windows 10 og distinkte kommersielle lisens. Før denne porten er oppfylt, må eksklusiv modus vises som ikke tilgjengelig; funksjonen kan ikke erklæres fullstendig.

Aksept: Med to fysiske tastaturer kjører samme nøkkel en makro og produserer ingen normal applikasjoninnføring bare på det valgte eksklusive enheten; andre tastaturinnføringer forblir uvirket. Kontroller avkobling, kollisjon, kansellering, suspension/resume og driver fjerning på den virkelige maskinvaren.

## 2. Guidet arbeidsplass
Legg til en trollmann for arbeidsplass, møte, streaming, forskning og prosjektoverføring. Oppdag installerte apper, lydenheter, skjermer og OBS scener. Brukere velger mål og bånd; forutsi de resulterende trinnene og validerer manglende krav. Spare bare etter gransking, med ingen handlinger som kjører under installasjonen. Genererte profiler forblir redigerbare i eksisterende redaktører.

Godkjennelse: opprette en arbeidsprofil uten å manuelt redigere utførbare veier eller enhetsIDer; avbestillingsblader innstillinger uendret; manglende programvare har en utførbar forklaring.

## 3. Verifisert eksekusjon
Introduksjon av forhåndsvisninger, typede steg-resultater og valgfrie etterbetingelser. Forskjellen mellom forespurte, fullført, verifisert, mislykkede, utdelt, kansellert og uoppdagelige forhold. Kontroller lyd/mikrofon, lyshet, prosess/vindue klarhet og OBS tilstand med begrensede asynkrone lesinger. Kort/tekstlevering er ikke bevis på at målsøkelsen godkjente kommandoen. Eksterne bivirkninger er ikke universelt reversible; tilbyr kompensasjon bare der implementert.

Aksept: Avkoblet eksplisitte mål faller aldri stille tilbake; feil observert tilstand rapporterer aldri verifisert suksess; annullering stopper spørring; feil identifiserer trinn og gjenoppretting handling. Eksekusjonshistorien glemmer tekstbelastninger og hemmeligheter.

## 4. Plugins og delbare pakker
Versjonert JSON manifesterer handlinger, parametersystemer, statlige leverandører, avhengigheter og evner. Kjør utførbare plugins i en separat prosess over begrenset lokal IPC med håndshake, tidsfrister og kansellering. Avvise feilformede meldinger og uforenelige versjoner; håndtere ulykker uten å avslutte kontrolløren. Kapasitetserklæringer er avsløring og mellommannelse, ikke et krav om OS sandboxing.

Gi pakkeeksport/import forhåndsvisning, innhold hasher, veiledning beskyttelse, størrelsesgrenser, versjon/avhengighet sjekker, eksplisitt tillatelse og oppgradering rollback. Importerte utførbare plugins må ikke utføres automatisk. Inkludere et arbeidshemmel plugin med en end-to-end install/action/state/uninstall flow. Ingen offentlig markedsføring er nødvendig for denne frigjøringen.

Aksept: opprette, eksportere, installere og utføre en sann eksempel integrasjon; demonstrere timeout, krasjisolasjon, avhengighet mislykkes, gjennomgående avvisning og inaktiv import.

## 5. Windows Ui Automasjon
Legg til en element pike og begrensede handlinger for invoke, verdi, utvalg, toggle, fokus og vente på et element/status. Persist selektorer ved hjelp av prosess, automatisering ID, kontrolltype og forfedre kontekst; unngå koordinering. Se selektor match count og test selektorer les-only. Passordfelt er utelukket fra registrering. Behandle ustøttede kontroller og Windows integritetsgrenser uttrykkelig.

Utvid innspillingen med UIA-handlinger som kan revideres, og beholder nåværende kortskudd. Ambiguous selektorer mislykkes i stedet for å klikke på en vilkårlig kontroll. En normal nettleser tilgjengelighet er ikke et løfte om en full DOM-motor.

Aksept: Automatiser en dedikert testapplikasjon med stabile selektorer over posisjon/størrelse endringer; test ambiguitet, savnede elementer, funksjonshemmede kontroller, timing og avbestilling.

## 6. Lokal telefon/tablett
Servere et responsivt panel fra en opt-in LAN-tjeneste. Par uttrykkelig ved hjelp av en kortvarig entids-kode og utleverer tilbakekallelige bekreftelser beskyttet lokalt. Bruk HTTPS med dokumentert lokal sertifisering; ingen fallback sending bekreftelser i klage. Deaktivering og begrensning av konfigurerte grensesnitt. Validering av opprinnelse, autentiserte kommandoer, prisgrenser og begrensede meldinger er obligatoriske.

Eksponere tillatte profiler/aksjoner, live-tilstand, fremgang og avbestillinger; bruk stabile IDs og samme utføringsskruer som skrivebord og fysiske nøkler. Ikke utsette vilkårlige kommandoer, utførbare veier, hemmeligheter eller plugin installasjon. Gi tilkobling, paring og tilbakekallelse skjermer. Det er ikke inkludert noen nett- eller nettkonto.

Aksept: opererer fra en ekte telefon/tablett på samme LAN; uavhengige kunder kan ikke liste eller kjøre handlinger; tilbakekallede tokener feiler; duplikerte/reployed kommandoer ikke utfører dobbelt; diskoneksjon tyst retrer handlinger.

## Keyboard produsent omfang

| Adapter/familie | Planlagt integrasjon | Uttrykkelig grense |
| --- | --- | --- |
| Corsair | Oppbevar strøm iCUE G-nøkkelinnføring og RGB integrasjon | Modell/programvarefunksjon er fortsatt nødvendig |
| Logitech G | G-key SDK adapter med press/utgivelse og driftstids tilgjengelighet sjekker; belysning gjennom sin separate SDK der støttet | Ikke ligge G HUB-deteksjon med støttet G-key API på hver modell |
| Razer | Chroma lysadapter og guidet Synapse-til-standard-nøkkel kartlegging, pluss generisk HID inngang | Chroma er en belysning API; ikke merke den innfødte makro-nøkkelinngang |
| SteelSeries | GameSense tilstand/lysadapter plus generisk HID-inngang | GameSense hendelsesopptak er ikke en generell proprietær nøkkelfanger API |
| Wooting | Analog SDK-adapter for støttet analoginngang, plus normale HID-nøkler | Kapasitet og SDK distribusjonsbetingelser må verifiseres per støttet modell |
| Keychron/QMK/VIA | Standard HID-nøkler og firmware-mapping instruksjoner; kjente layouter bare med bevis | VIA konfigurasjonsstøtte er ikke en universell kjøre-key API |
| Andre standard HID tastaturer | Per-device fysiske nøkkeloppdagelse og kartlegging; eksklusiv modus bare med validerte filter bakgrunnen | Ingen produsert modell-specifik Fn, dial eller proprietær nøkkelstøtte |

Hver enhet viser oppdaget, observert, kartlagt gjennom leverandør programvare, innfødte SDK, driver-eksklusiv, utilgjengelig, eller maskinvarestet status som gjeldende. Oppbevar evne bevis med adapter versjoner og testet modell / firmware identifiserer.

## Leveringsordre
1. Konfigurasjonskompatibilitet, kapasitetskontrakter og verifisert gjennomføring.
2. Guided setup ved hjelp av ekte oppdagelser og eksisterende handling utførere.
3. UI Automation og dens rekord/test fix.
4. Plugin-host, pakkefløy og eksempelintegrasjon.
5. Payed LAN panel og mobil verifisering.
6. Utvidet leverandøradapter og eksklusiv tastaturbakgrunnsintegrasjon, med ekte maskinvarekvalidering.

Alle seks er nødvendig. Driver signering/licensing, utilgjengelige SDK-er eller fravær av testmaskiner er frigjøringsdører, ikke grunner til å erstatte mock oppførsel og kalle arbeidet komplett.

## Verifikasjon og frigjøringskriterier
Kjør eksisterende kjerne, Windows og WPF sjekker og legger til fokuserte integrasjonstester for hver ny grense. Sjekk JSON runde reiser og legacy profiler; test importert utførlig innhold, nettverks autentisering, annullering, plugins krasjer og enhet gjenkobling. Sjekk ut desktop og mobile layout. Validerer høy Windows skala separat. Bygg utgivelsesartikler bare etter å ha gjennomgått kontroller; skille implementert/testet, implementert/hardware-testerte og eksternt blokkerte funksjoner i utgivelsesnoter. Ikke erstatte den installerte appen, installere drivere, endre virkelige tastaturmappinger eller åpne nettverkstilgang som en tilfeldig test.

## Bevisene
- Microsoft Raw Input Semantikk: https://learn.microsoft.com/en-us/windows/win32/api/winuser/ns-winuser-rawinputdevice
- Logitech G utvikler SDK: https://www.logitechg.com/en-us/programs/developer-alliance
- Razer Chroma SDK FAQ: https://developer.razer.com/works-with-chroma/chroma-faq/
- Interseksjonsrepositor og avhengighet: https://github.com/oblitum/Interception
- Eksisterende lagerprodusenter: docs/de-DE/research/tastaturhersteller-sonderfunktionen.md

## Revisjon av beslutninger
Brukeren blir bedt om å godta denne skriftlige utformingen før implementeringsplanen. Den viktigste eksterne avhengighet er en produksjon-ready eksklusive-input driver backend. Rekommenderet omfang beholder dette som en gaten tilgjengelig i stedet for å stille ned til Raw Input observasjon.
