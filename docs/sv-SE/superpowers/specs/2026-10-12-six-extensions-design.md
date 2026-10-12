<!-- generated-doc locale=sv-SE source=docs/superpowers/specs/2026-10-12-six-extensions-design.md sha256=a80dda083fbbe90404023ac784b47e7f16f89f72ce0b821e3cda122dc561317f -->

> Maskinassisterad översättning. Struktur, kodexempel och länkar kontrolleras; detta innebär inte granskning av en modersmålstalare eller juridiskt godkännande. Historiska rapporter behåller sina ursprungliga datum och omfattning. [Originalkälla](../../../superpowers/specs/2026-10-12-six-extensions-design.md)

# MAKRIVO: sex produktutvidgningar och tangentbordadapter

Datum: 2026-10-12
Status: föreslagit design för användarrecension; genomförandet har inte börjat.

## Mål
Leverera alla sex begärda tillägg i den befintliga Windows/WPF ansökan, bevarar befintliga profiler, G1/G2 uppdrag, lokal drift och kontonfri användning. Utöka tangentbordets integration till Razer och Logitech, med en återanvändbar kapacitetsbaserad arkitektur för andra tillverkare. Ett tillverkarnamn eller tangentbordslayout är aldrig ett bevis på att äganderättskontroller stöds.

## Arkitekturbeslut
utöka Aivana.Kärn med serialiserbara kontrakt och validering, Aivana.Windows med Windows och leverantör adapter, och Aivana.Desktop med transaktionell redigerare och statusvyn. Använd separata medarbetarprojekt för isolerad plugin-exektion och den lokala mobila panelservice. Undvik en rewrite eller en enda privilegierad process värd varje förlängning. Den befintliga konfigurationen måste ladda oförändrad; lägga till åtgärder i stället för att ordna sina bestående enum värden.

Alternativen betraktas: en monolitisk genomförande skulle inriktas på maskinvaru, nätverk och arbetsflöde; ersättning WPF skulle lägga till icke-relaterad migration arbete. Modulära förlängningar rekommenderas.

## 1. oberoende makropad
Förvara länkar med enhetens identitet plus fysisk skanningskod och förlängda nyckelflaggor. Lägg till uttryckliga observerings- och exklusiva lägen, enhets upptäckt, nyckelfaktur, upprepade suppressioner, gesturer, håll lager och återkoppling av diagnostik. Förvara huvud/numpad Enter-avvikelser och testa tyska/amerikansk layout. Bindningar syftar till en vald fysisk tangentbord; tvivelaktiga återkopplingsidentiteter kräver omval i stället för att styra en annan enhet.

Raw Input Exklusiv avstängning per enhet kräver en riktig tangentbordfilter bakgrundsnivå; aldrig korrelera globala låga nivåer med Raw Input och kräver tillförlitlig enhetspecifik avstängning. ge en övervakning, nödsituation, felaktigt beteende och installatör/uninstaller. inte fånga säker skrivbordsinsatser. Driver installation och exklusiv mode aktivering är uttryckligen separata användaråtgärder.

Driverport: Välj en stödd, korrekt licensierad och undertecknad backend som är kompatibel med målkonfigurationerna Windows 11 eller utveckla/signera ett dedikerat filter. Interception är en kandidat för att utvärdera, inte en engagerad produktionsberoende: dess officiella README beskriver testet genom Windows 10 och distinkta kommersiella licenser. Innan denna port är uppfylld måste exklusivläge visas som oförgänglig; funktionen kan inte förklaras fullständig.

Acceptans: Med två fysiska tangentbord utför samma nyckel en makro och producerar ingen normal applikationsinsats endast på den valda exklusiva enheten; andra tangentbordinsatser förblir oförstörda. Kontrollera avkoppling, krasch, avbokning, suspension/resumtion och driver avlägsnande på den faktiska hårdvaran.

## 2. Guide arbetsplats inställning
Lägg till en trollkarl för arbetsplatser, möten, strömning, forskning och projektväxling. Upptäck installerade appar, ljudenheter, skärmar och OBS scener. Användare väljer mål och bindningar; förhandsgranska de resulterande stegen och validera saknade krav. Spara endast efter granskning, utan åtgärder som körs under installationen. De genererade profilerna kan redigeras i befintliga redaktörer.

Acceptans: Skapa en arbetsprofil utan att manuellt redigera utförbara vägar eller enhetsID; Annullering lämnar inställningar oförändrade; saknad programvara har en aktiverbar förklaring.

## 3. Verifierad verkställighet
Introduktion av förskjutningskontroller, typade steg-resultat och valfria postvillkor. Skillnaden mellan begärda, slutförda, verifierade, misslyckade, tidsbegränsade, upphävda och oåterkalleliga stater. Kontrollera ljud/mikrofon, ljusstyrka, process/fönsterberedskap och OBS tillstånd med begränsade asynkrona läsningar. Shortcut/text leverans är inte ett bevis på att målinriktad ansökan accepterade kommandot. Externa biverkningar är inte universellt reversibla; erbjuder kompensation endast där de genomförs.

Acceptans: avkopplade uttryckliga mål faller aldrig tyst tillbaka; felaktigt observerat tillstånd rapporterar aldrig verifierad framgång; annullering stoppar undersökning; misslyckanden identifierar steget och återhämtning åtgärd. Exekutionshistoria glömmer textbelastningar och hemligheter.

## 4. Plugins och delbara paket
Versionerade JSON-mönster deklarerar åtgärder, parametrar, statliga leverantörer, beroende och kapacitet. Kör utförliga plugins i en separat process över begränsad lokal IPC med handshake, tidsfrister och avbokning. Avvisar missformade meddelanden och inkompatibla versioner; hantera olyckor utan att avsluta kontrollerna. Kapacitetsdeklarationer är avslöjande och medling, inte ett krav på OS sandboxning.

Förse paketet export/import, innehållshasor, vägsskydd, storleksgränser, version/avhängighetskontroll, uttrycklig tillåtelse och uppgradering. Importerade utförbara plugins ska inte utföras automatiskt. Inkludera ett fungerande exempelplugin med en end-to-end install/action/state/uninstall flöde. Ingen offentlig marknadstjänst krävs för denna release.

Acceptans: skapa, exportera, installera och genomföra en verklig exempelintegration; demonstrera tidsläge, isoleringen av olyckor, misslyckanden med beroende, genomgående avvisning och inaktiverad import.

## 5. Windows UI Automation
Lägg till en element pick och begränsade åtgärder för inbjudan, värdet, urvalet, fokus och väntan på ett element/stat. Persistera med processer, automatisering ID, kontrolltyp och förfäder sammanhang; undvika samordnings-enbart inspelning. Visa matcher räkna och testa matcher läs-enbart. Passordfält är uteslutna från inspelning. Hantera okontrollerade kontroller och Windows integritetsgränser uttryckligen.

Utöka inspelningen med granskbara UIA-åtgärder, behålla aktuell kortspårning. Ambiguous selektorer misslyckas istället för att klicka på en vilseledande kontroll. Ett normalt webbläsaråtkomlighets träd är inte ett löfte om en fullständig DOM-motor.

Acceptance: Automatisera en dedikerad testansökan med stabila markeringar över position/storlek förändringar; test ambiguitet, saknade element, inaktiverade kontroller, timing och avbokning.

## 6. Lokal telefon/tablett
Servera ett responsivt panel från en opt-in LAN-tjänst. Parar uttryckligen med en kortvarig entidskod och utfärdar återkallbara certifikat skyddade lokalt. Använd HTTPS med dokumenterad lokal certifiering trust/pinning; ingen fallback skickar bekräftelser i klagomål. Defekt till inaktiverad och begränsa konfigurerade gränssnitt. Ursprungsvalidering, autentiserade kommandon, prisgränser och begränsade meddelanden är obligatoriska.

Exponera tillåtna profiler/åtgärder, levande tillstånd, framsteg och avbokning; använd stabila ID och samma utföranden som skrivbord och fysiska nycklar. Exponera inte vilseledande kommandon, verkställbara vägar, hemligheter eller plugin installation. Förse anslutning, parering och återkallande skärmar. Ingen molnkonto eller internetrelay ingår.

Acceptans: kör från en verklig telefon/tablett på samma LAN; obetydliga kunder kan inte lista eller köra åtgärder; återkallade tokens misslyckas; dubbelkommandon utförs inte; avkoppling stänger inte tyst åtgärder.

## Keyboard tillverkare

| Adapter / Familj | Planerad integration | Förklarade gränser |
| --- | --- | --- |
| Corsair | Håll nu iCUE G-nyckel och RGB-integration | Modell/programvarukapacitet upptäckt är nödvändig |
| Logitech G | G-key SDK-adapter med pressen/utgivningen och körtiden tillgänglighet kontrolleras; belysning genom dess separata SDK där stöds | Jämför inte G HUB-detektionen med den stödda G-nyckeln på varje modell |
| Razer | Chroma-belysning adapter och guidad Synapse- till standardnyckel mappning, plus generisk HID input | Chroma är en belysning API; inte märka det inhemska makronyckel ingång |
| SteelSeries | GameSense läge/belysning adapter plus generisk HID input | GameSense händelseanmälan är inte en allmän ägande nyckeltagning API |
| Wooting | Analog SDK-adapter för stödjande analog inmatning, plus normala HID-nycklar | Kapacitet och SDK-distributionskrav måste verifieras per stödd modell |
| Keychron/QMK/VIA | Standard HID-nycklar och firmware-mappningsanvisningar; kända layouter endast med bevis | VIA konfigurationsstöd är inte en universell körtid macro-key API |
| Övriga HID-tangenter | Fysisk nyckel upptäckt och mappning per enhet; exklusiv läge endast med validerad filter bakgrundsnivå | Ingen tillverkad modell-specifik Fn, dial eller proprietär nyckelstöd |

Varje enhet visar upptäckt, observerad, mappad via leverantörsprogramvara, inhemska SDK, drivrutiner exklusiv, otillgänglig eller hårdvara testad status som tillämpligt. Bevara kapacitetsbevis med adapterversioner och testade modell/firmware-identifierare.

## Leveransorder
1. Konfigurationskompatibilitet, kapacitetskontrakt och verifierad genomförande.
2. Guidad installation med verkliga upptäckter och befintliga åtgärdsutövare.
3. UI Automation och dess rekord/test fixering.
4. Plugin värd, paketflöde och exempel integration.
5. Payed LAN panel och mobil verifiering.
6. Utökad leverantörsadapter och exklusiv tangentbordsbakgrundsintegration, med verklig hårdvaruvalidering.

Alla sex är nödvändiga. Driver signatur/licens, otillgängliga SDK eller frånvarande testhårdvara är släppdörrar, inte skäl att ersätta mock beteende och kalla arbetet komplett.

## Verifikation och frigivningskriterier
Kör befintliga kärnor, Windows och WPF kontrollerar och lägger till fokuserade integrationstester för varje ny gräns. Kontrollera JSON-runda resor och arvsprofiler; testa importerat verkställbart innehåll, nätverksautentisering, annullering, plugin-olyckor och enhet återanslutning. Kontrollera skrivbord och mobila layouter. Validerar hög Windows skala separat. Bygg utgivningsartiklar först efter att du har genomgått kontroller; skilja implementerad/testad, implementerad/hardware-testad och extern blockerad funktionalitet i utgivningsnoter. Ersätt inte den installerade appen, installera drivrutiner, ändra verkliga tangentbord mappningar eller öppna nätverksåtkomst som en incidentell testning.

## Beviset
- Microsoft Raw Input Semantiska: https://learn.microsoft.com/en-us/windows/win32/api/winuser/ns-winuser-rawinputdevice
- Logitech G utvecklare SDK: https://www.logitechg.com/en-us/programs/developer-alliance
- Razer Chroma SDK FAQ: https://developer.razer.com/works-with-chroma/chroma-faq/
- Interception och beroendeförhållanden: https://github.com/oblitum/Interception
- Existerande lagertillverkare: docs/de-DE/research/tastaturhersteller-sonderfunktionen.md

## Översikt Beslut
Användaren uppmanas att godkänna denna skriftliga design före genomförandet av planen. Den främsta yttre beroendet är en produktionsbered exklusiv inmatningsdrivrutiner. Den rekommenderade räckvidden behåller detta som en gatan leverans snarare än tyst att minska det till Raw Input observation.
