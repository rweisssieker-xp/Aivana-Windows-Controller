<!-- generated-doc locale=sv-SE source=docs/superpowers/specs/2026-10-12-controller-usps-design.md sha256=4c3b2dbedb83af6bd48917515ae6559012afb6318ce69d1e32fb2765e0793327 -->

> Maskinassisterad översättning. Struktur, kodexempel och länkar kontrolleras; detta innebär inte granskning av en modersmålstalare eller juridiskt godkännande. Historiska rapporter behåller sina ursprungliga datum och omfattning. [Originalkälla](../../../superpowers/specs/2026-10-12-controller-usps-design.md)

# MAKRIVO Kontrollförlängningar

Användaren begärde alla de nio föreslagna förbättringarna och en upptäckbar tray-ikon. De befintliga konfigurationerna, särskilt G1/G2 ljusstyrka, måste förbli kompatibla. De befintliga icke beställda märkning och dokumentationsredigeringar tillhör den delade arbetsytan och måste bevaras.

## Leveranser och gränser

1. Brightness-åtgärder väljer alla stödda monitörer eller en stabil specifik monitor ID. Det saknas tydliga mål som misslyckas. Dashboard visar aktuell ljusstyrka, utgångsvolym, mikrofonstatus och OBS status genom begränsade asynkron läsningar.
2. Särskilda nycklar stöder omedelbar tryck, tryck, håll och dubbel tryck, med upprepade tryck. En behållande lagerselektor ändrar tillfälligt den aktiva mappningen och släpper den tillbaka. Existerande omedelbara bindningar behåller sitt beteende. Pause, profilförändringar, avkopplingar och annullering tydliga hängande gester.
3. Arbetsflödesdesignern redigerar villkor, framåtskedjor och stop/continue misslyckande policy transaktionellt i ett tygprojekt. Villkoren täcker filer, körprocesser och fönstertitlar. Inga obegränsade språngcyklar.
4. Explicit inspelning fångar kortskärningar och väntar, ignorerar injektionsinmatning och stoppar på Escape. De registrerade åtgärderna granskas före antagandet; den vanliga texten är inte registrerad.
5. Standard specialnycklar per enhet från andra tillverkare används Windows Raw Input. Äganderättsnycklar kräver en stödjande leverantör API eller leverantör mappning till en standardnyckel; okänd funktionalitet visas ärligt. Diagnosen förklarar profil, paus, bindande konflikter och den faktiska adapternas tillstånd.
6. Profilutbyte använder en användarvalad delad mapp (inklusive mappar synkroniserade av en befintlig molnklient). Revisionskontrollen förhindrar tyst överskott. Pull förutser ändringar och importerar inaktiverade åtgärder med maskinvarukorttagning bort. Inkludering av innehåll är uttrycklig. Ingen molncertifiering lagras av MAKRIVO.
7. UI strängar tillhör de befintliga 27-språkkatalogerna; DE/EN/FR/IT förblir de minsta granskade dokumentationsspråken. Översättningsfrekvenser måste avslöjas snarare än anses vara fullständiga.
8. Ett erkänbart teal M-tangenter ersätter den generiska Windows tray-ikonen. Pause mode har en amber paus. Exekutable/window branding använder samma märke. Tooltip innehåller status och profil.

## Verifikation

Kärnkontroll täcker bakåtkompatibilitet, snapshots, avbokning, gesturtidning, arbetsflödesbranscher/fel, inspelning och synkroniseringskonflikter. Windows kontroller täcker läs-enbart displayenumeration, SDK release händelser och Raw Input parsing. WPF rökkontroll täcka nya ingångspunkter och resurser utan att ändra verklig skärm ljusstyrka, skriva in i en annan applikation eller överskrida användarprofiler. Installationen behåller en säkerhetskopia och G1/G2 konfiguration.
