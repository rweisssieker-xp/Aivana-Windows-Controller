<!-- generated-doc locale=et-EE source=docs/FEATURES.md sha256=ea712f39457ce69f772f09a1294201705a5f36a92a8bf15f356bd80132c3e723 -->

> Masintõlke abil koostatud dokumentatsioon. Struktuuri, koodinäiteid ja linke kontrollitakse; see ei tähenda emakeelse toimetaja kontrolli ega õiguslikku heakskiitu. Ajaloolised aruanded säilitavad algsed kuupäevad ja ulatuse. [Originaalne allikas](../FEATURES.md)

# MAKRIVO – funktsioonide ülevaade

Tootjad: **Aivana**. Jätkake: **12. oktoober 2026**. See on arengu seisund, mitte müük või hardware vabastamine.

## Windows juhtimine

- Kahlid, globaalsed hotkeys, juhendipalett, lemmikud ja rakenduse profiilid.
- Makro mitmete sammudega, korratused, pausi, katkestamine ja ettevaatus.
- Avatud programmid, failid ja veebilehed; teksti, vahe ja lühend.
- Korraldage aknad, blokeerige Windows, hallige heli ja mikrofonid ning muuta ekraani heledust toetatud vahendite kaudu.
- OBS-WebSocket-v5-Anbindung stseenide, salvestamise, voogude ja heli jaoks.
- Kaardid, töökoha assistent, seadmete tuvastamine ja klaviatuuri üleandmine.
- Profiilkonfiguratsioonide importimine ja eksportimine; disainikontrolli ja konfliktide tuvastamine.
- Kohapeal salvestatud seaded; igapäevaseks kasutamiseks ei ole vaja konto.

## Täiendav funktsioon praeguses allikas

| Piirkond | Funktsionaalne ulatus | Piirid |
| --- | --- | --- |
| Tulemuste kontrollimine | Eelkontrolli, jälgimise ja läbivaatamise protsess | Ainult kontrollitavad tulemused kinnitavad; edukas nuppu ei näita sihtotstarbelist mõju. |
| Töökoha assistent | Ettevalmistada profiilid ja protsessid valitud programmidest, failidest, URL-idest ja muudest tegevustest | Eesmärk ja salvestamine on vajalik; asutus ei tee midagi. |
| Makro ja töövoodid | Registreerimine, tingimused, gestid ja kohalik profiili sünkroonimine | Käitumine ja piirangud on dokumenteeritud rakendus- ja testide aruannetes. |
| UI automatiseerimine | Eesmärkide valimine, valijate kontrollimine, tegevuste ja piiratud kliki käigud | Eraldi töötajad; mitmekülgsed, deaktiivsed ja parooli eesmärgid on keeldunud; üldine teksti sisestamine ei ole. |
| Laiendused | Pakendite kontrollimine, importimine, selgesõnaliselt usaldatav aktiveerimine, eksportimine ja tagasipöördumine | Protsessisolatsioon ei ole liivakasti; ei ole avalik turg. |
| Mobiilpaneel | Kohalik HTTPS-paneel, ühendus, valitud profiilid ja staatus/testi ajalugu | Reaalne telefoni/tabletti LAN-i eemaldamine on veel avatud; universaalne sensor. |
| Tootja Adapter | Corsair, Logitech-G-SDK, Razer/SteelSeries-RGB ja Wooting-Analogadapter | mudeli/SDK sõltuvuses; uus adapter ei ole täielikult valideeritud reaalse seadmega. Ei ole uusi omandatud SDK-dLLs ühendatud. |
| Füüsiline klaviatuur | Seadme ja skaneerimiskodi abil | Raw Input ei pärsita normaalset Windows sisestamist. |
| Kohalik AI-nõustaja | Valikuline prototüüp Ollama, LM Studio, llama.cpp ja ONNX käivitusaeg GenAI CPU toodab piiratud disainid selgelt valitud ressurssidest | Standardne; kodumaine CPU tööaeg juhuslikult testitud, kuid ei ole kindlaksmääratud planeerimise kvaliteet või aja säästmine; ei sisalda mudeli kaalu. |

## Ei ole veel valmis müügi

- **Ekslusiivne teise seadme/makropood: ** allkirjastatud, litsentseeritud ja tarkvara kontrollitud filtreerijad puuduvad. Tavalised klaviatuuri sissemakseid saab jätkata Windows.
- Täielikku seadme eemaldamist kõik tootja adapterid, samuti reaalne mobiilside LAN testid.
- Tõeline segatud monitor/DPI vahetus ja kõik uued tõlked. PerMonitorV2 ja simuleeritud 150/200-Prozent-Layouts on kontrollitud; reaalne ekraani testimine jääb eraldi. Ülevaade pakub 27 keeli; üksikasjalikud uued tekstid ei ole veel täielikult keeleline.
- Allkirjastatud müügiinstalleerijad, lõplik EULA, kontrollitud SDK müügiõigused ja produktiivne ostulitsentsi/kontrolli integreerimine.

## Detailid ja tõendid

- [Kasutusdokumendid](README.md)
- [Laiendused: funktsioonid, testid ja avatud piirid](testing/2026-10-12-six-extensions.md)
- [Kohalik AI assistent: prototüüp ja testimine](testing/2026-10-12-local-meeting-assistant.md)
- [Kohalikud AI-tunnid ja mudeli nõuded](testing/2026-10-12-local-ai-runtimes.md)
- [DPI ja skaalutõend](testing/2026-10-12-dpi-scaling.md)
- [Litsentsi kontrollimine](licensing/2026-10-12-commercial-license-audit.md) – ajalooline salvestus; uus Publish Script täiendab Runtime'i juhiseid. Lõplik pakett tuleb kontrollida eraldi.
- [Müügiplatvormid](licensing/makrivo-vertriebsplattformen-2026-10-12.html) – Võrdlusalune; Stripe on lisaks võimalik makseplatvorm, vt kehtivad teenuseosutajad.

## Täielik arenduspakett

```powershell
./tools/publish-complete.ps1 -SelfContained
```

Pakett sisaldab töölaua rakendust, UI-automaatika töötajaid, mobiiltelefone, brauseri faile, näidisplugin ja käivitamise litsentsi teavet. Ehitusfakte ei kontrollita GIT-s. GitHub lähtekood ei ole veel avaldatud Windows install.
