<!-- generated-doc locale=sv-SE source=docs/FEATURES.md sha256=ea712f39457ce69f772f09a1294201705a5f36a92a8bf15f356bd80132c3e723 -->

> Maskinassisterad översättning. Struktur, kodexempel och länkar kontrolleras; detta innebär inte granskning av en modersmålstalare eller juridiskt godkännande. Historiska rapporter behåller sina ursprungliga datum och omfattning. [Originalkälla](../FEATURES.md)

# MAKRIVO – Feature översikt

Tillverkare: **Aivana**. Stannar: **12. oktober 2026**. Detta är utvecklingsstadiet, ingen försäljning eller hårdvara.

## Windows-Styrning

- Kachel, globala hotkeys, kommandospalett, favoriter och applikationsberoende profiler.
- Makroner med flera steg, upprepningar, pauser, avbrott och förutseende.
- Öppna program, filer och webbplatser; Använd text, mellanrum och knappkort.
- Anordna fönster, låsa Windows, styra ljudutgångar och mikrofoner samt ändra skärmljus via stödda gränssnitt.
- OBS-WebSocket-v5-Anbindung för scener, inspelning, streaming och ljud.
- Formulär, arbetsplatsassistent, enhetssökning och tangentbord överlappning.
- Import och export av profilkonfigurationer; utformningskontroll och konfliktdetektion.
- Lokalt lagrade inställningar; inget konto krävs för daglig användning.

## Utökade funktioner i den aktuella källkoden

| Område | Funktionsområde | Gränserna |
| --- | --- | --- |
| Resultatkontroll | Förundersökning, kontroll och provning | Endast verifierbara effekter bekräftar; ett framgångsrikt knapptryck visar inte målet. |
| Arbetsplatsassistent | Förbereda profiler och processer från valda program, filer, URL-adresser och andra åtgärder | Förhandsgranska och lagra nödvändigt; anläggningen utlöser ingenting. |
| Makroupptagning och Workflows | Registrering, villkor, gest och lokal profil synkronisering | Beteende och gränser är dokumenterade i genomförandeboken och testrapporterna. |
| UI Automation | Välj mål, kolla ut valörer, ta emot åtgärder och begränsade klickflöden | Separat worker; tvetydiga, inaktiverade och lösenordsmål avvisas; ingen inspelning av allmän textinmatning. |
| Utvidgningar | Kontrollera, importera, aktivera, exportera och rulla tillbaka paket efter uttryckligt förtroende | Processisolering är inte en sandbox; ingen offentlig pluginsmarknad. |
| Mobilpanel | Lokalt HTTPS-panel, koppling, valda profiler och status/testförteckning | Verklig telefon/tablett-LAN-avtagning är fortfarande öppen; ingen universell sensor-dashboard. |
| Tillverkareadapter | Corsair, Logitech-G-SDK, Razer/SteelSeries-RGB och Wooting-Analogadapter | Modell-/SDK-beroende; nya adapter inte helt validerad med verklig hårdvara. Inga nya proprietära SDK-DLLs bundna. |
| Fysiska tangentbord | Standardknappar med hjälp av enhet och skanningskod | Raw Input undertrycker inte den normala Windows ingången. |
| Lokala AI-assistent | Optionell prototyp för Ollama, LM Studio, llama.cpp och ONNX Runtime GenAI CPU genererar begränsade design från uttryckligen valda resurser | Inaktiverad som standard; native CPU-körtid testad med en slumpmodell, men ingen bevisad planeringskvalitet eller tidsbesparing; inga modellvikter medföljer. |

## Inte att erbjuda som färdig försäljning

- **Exklusiv sekundär/makropad: ** En undertecknad, licensierad och hårdvaru-kontrollerad filterdrivrutiner saknas. Normala tangentbord kan fortsätta att nå Windows.
- Fullständig hårdvaruavtagning av alla tillverkare adapter samt verkliga mobila enheter LAN-test.
- Verkliga blandade monitor-/DPI-växlingar och professionell avtagning av alla nya översättningar. PerMonitorV2 och simulerade 150/200-Prozent-Layouts har kontrollerats; verkliga displaytest förblir separata. Ytan innehåller 27 språk; detaljerade nya texter är ännu inte helt språkliga.
- Signat försäljningsinstaller, slutgiltig EULA, verifierade SDK-försäljningsrättigheter och produktiv inköpslicens/checkutintegration.

## Detaljer och bevis

- [Användningsdokumentation](README.md)
- [Utökningar: funktioner, tester och öppna gränser](testing/2026-10-12-six-extensions.md)
- [Lokala AI-assistenter: Prototyp och teststand](testing/2026-10-12-local-meeting-assistant.md)
- [Lokala AI-tider och modellkrav](testing/2026-10-12-local-ai-runtimes.md)
- [DPI- och skaleringstest](testing/2026-10-12-dpi-scaling.md)
- [Licensprovning](licensing/2026-10-12-commercial-license-audit.md) – historisk inspelning; det nya publiceringsskriptet kompletterar Runtime-instruktioner. Det slutliga paketet ska granskas separat.
- [Försäljningsplattformar](licensing/makrivo-vertriebsplattformen-2026-10-12.html) – Jämförelse; Stripe är dessutom en möjlig betalningsplattform, se aktuella leverantörsvillkor.

## Tillverka ett komplett utvecklingspaket

```powershell
./tools/publish-complete.ps1 -SelfContained
```

Paketet innehåller skrivbordsapp, UI-automatiseringsarbetare, mobilhost, webbläsarfiler, exempelplugin och körtidslicensanvisningar. Konstruktionsfakta kontrolleras inte i Git. En GitHub källkod är ännu inte publicerad Windows installatör.
