<!-- generated-doc locale=sv-SE source=docs/superpowers/specs/2026-10-08-windows-controller-design.md sha256=f36d496c6af5190889e9a31487a8fc6afdeba5af18facdd600639ede5ddbd169 -->

> Maskinassisterad översättning. Struktur, kodexempel och länkar kontrolleras; detta innebär inte granskning av en modersmålstalare eller juridiskt godkännande. Historiska rapporter behåller sina ursprungliga datum och omfattning. [Originalkälla](../../../superpowers/specs/2026-10-08-windows-controller-design.md)

# MAKRIVO – Projektet

[English (US)](2026-10-08-windows-controller-design.md) | [Deutsch](2026-10-08-windows-controller-design.md) | [Français](2026-10-08-windows-controller-design.md) | [Italiano](2026-10-08-windows-controller-design.md)

Stannar: 8. oktober 2026. Status: För undersökning före genomförande.

## Mål och överenskomna omfattning

En oberoende Windows-app gör en normal PC-tastatur till ett fritt konfigurerbart åtgärdspanel som liknar en strömdäck. Användaren har bekräftat alla föreslagna åtgärdstyper, makron, enhetlig identifiering, profiler och en bra GUI. Anslutna Corsair- och andra tangentbord identifieras; ytterligare tillverkningsfunktioner erbjuds endast om de faktiskt stöds.

Antaganden: tyska gränssnitt, lokal användning utan konto, Windows 11 x64 som det första målet, ingen drivrutin installation. Tastaturen kan användas för normal skrivning. Globala klistermärken använder standardt tangentkombinationer med modifierare. Enskilda bokstäver är inte standardiserade globalt.

## Tekniska beslut

Född C#/.NET-10-App med WPF MVV: Det är stabilt .NET-10-SDK Finns tillgängligt i arbetsområdet. WPF tillåter en individuell Desktop-gräns och direkt Windows-Alternativ skulle vara en webbläsare med separat Windows-En tjänst eller en plattformsövergripande skrivbordsapp; båda ger extra drifttid och kommunikationskomplexitet för denna Windows-Den ursprungliga varianten är därför den valda grunden.

Delning: Desktop-projekt för syn och trail, Core-projekt för konfiguration och genomförande logik, Windows projekt för hotkeys, inmatning och enheter, testprojekt för plattformsberoende regler. Tillverkaradaptrar hänger på ett fast kapacitetsgränssnitt och är oberoende av standardåtgärder.

## ytan

Mörk design med lugn antrazitfärgad yta, välläsbart skrivande och turkisk accent. Länkar Navigation för dashboard, profiler, enheter och inställningar. Över aktiv profil, sök och växlar för att avbryta alla hotkeys. Ytan förblir användbar vid 100–200 procent Windows-skalering.

Dashboardet innehåller en anpassningsbar skruvskruv. Varje kål visar symbol, namn, hotkey och prestationsstatus. Klicka utför samma åtgärd som Hotkey. Till höger öppnas en redaktör med åtgärdstyp, parametrar, hotkey-inspelning, symbol/färg, aktivering och testknappen. Nya, duplicera, radera och omordna är tillgängliga. En tangentbordsvisning visar inslag schematiskt för det tyska standardläget; den hävdar inte exakt geometri av den identifierade enheten.

Profiler kan skapas, kopieras, bytas namn och aktiveras manuellt. Alternativt tilldelas en processnamn till en profil. Ett lämpligt grundprogram aktiverar sin profil, annars gäller den globala standardprofilen. För flera lämpliga profiler gäller en tydlig ordning. Redaktören förklarar hotkey-konflikter direkt på platsen.

## Hotkeys och inträde

Windows RegisterHotKey bearbetar normala globala kombinationer med undertryckt knappåterställning. Endast den aktiva profilen registreras. Reserverade eller redan registrerade kombinationer visas som fel; appen rapporterar inte att de har aktiverats. Innehållet i redaktionen löser inte åtgärder.

Före simuleringar släpps de utlösande modifieringsnycklarna eller förväntas deras fysiska släpp. Unicode-text och tangentkombinationer skickas med SendInput. Varje själv tryckt knapp släpps även vid avbrott. Handlingar körs utanför UI-tråden; genomförandet och profiländringen får inte blockera ytan.

## Aktioner och makron

Åtgärdstyper: Starta programmet med argument och arbetsbok, öppna filer/filer över Windows, öppna HTTP/HTTPS-webbplatser, skriva in unicode-text, skicka en knappkombination, media Play/Pause/Nästa/Förstopp, höga/låg/stämma samt ändra profil. Parameterna valideras före lagring och igen före utförande. Programmet startas direkt; Argumenten tolkas inte som Shell-kommandon.

Makronerna innehåller en ordnad lista över de nämnda åtgärderna och uttryckliga steg. Steg kan läggas till, redigeras och flyttas. Upprepningar är äntligen konfigurerbara (1–100); Endless spinn hör inte till den första versionen. En löpande makro kan slutföras över ytan eller en egen avbrottshotkey. Upprepad utlösning av samma löpande beläggning startar inte en parallell gång. Olika makroner med inmatningssimulering serieras så att deras tangenttryck inte förväxlas. I händelse av ett stegfel stoppar makron och visar det berörda steget.

## utrustning och tillverkare

Enhetslista över Windows Raw Input och kompletterande enhetsinformation: Visningsnamn, enhetsvägen, tillverkare/produktidentifiering i den mån det finns tillgängliga, anslutningsstatus och identifierade färdigheter. Uppdatera/uppdatera listan utan att starta om. En fysisk enhet kan ha flera HID-inmatningar; dessa grupperas efter laddbara identifieringar, annars synliga som separata inmatningar.

Vanliga hotkeys gäller mellan tangentbord. Enhetsvisningen ger information och färdigheter; en separat tangentbord med exklusiva knappar är inte en del av det överenskomna läget.

Corsair-detektion fungerar även utan iCUE. En valfri iCUE SDK-adapter ger anslutningsstatus och stödjande belysning när SDK, iCUE och kompatibla enheter finns. UI skiljer sig upptäckt, SDK tillgänglig, ansluten och faktiskt kontrollerbar. SDK dokumenterar främst LED-kontroll; proprietära G-knappar stöds endast som vanliga hotkeys när Windows får motsvarande inmatningar. Det finns ingen fast löfte för alla spänningar.

Andra tillverkare får först generisk identifiering och standardåtgärder. Adaptergränssnittet tillåter senare integrationer; unsupported färdigheter visas ärligt. Hardware-test för icke-existerande modeller anges inte som bestående.

## Förvaring och vardag

Versionerad JSON-konfiguration under den lokala appdatalogen. Atomisk lagring med den sista giltiga säkerhetskopieringen; felaktiga filer skrivs inte över. Importen kontrollerar format, version, ID och parametrar och visar en förhandsvisning före överföring. Importerade åtgärder kan endast genomföras efter uttrycklig aktivering. Exporten innehåller profiler och beläggningar.

Tray-menyn: Öppna, Profilval, Stopp hotkeys, Avbryta makron, Avsluta. Fönstren stängs minimalt i trajan och förklarar detta vid första gången. Starta är valfritt och standard. Endast en instans hanterar hotkeys. Slutar registrera hotkeys, bryter ut utföranden och frigör resurser.

Lokala aktiviteter med begränsad kurs för framgång, misstag och avbrott. Inga protokoll över alla tangentbord. Innehållet skrivs inte ut i felmeddelanden. Ingen moln och ingen telemetri.

## Felhantering och gränser

Saknade program, ogiltiga URL-adresser, skadade profiler och otillgängliga tillverkare SDK får konkreta tyska meddelanden. En adapterfel påverkar inte standardåtgärder. Inmatningar i högre privilegierade applikationer kan misslyckas med Windows behörighetslinjer; felet förklaras, appen kräver inte standard administratörs rättigheter.

## Prövning och nedgång

Automatiserade tester för profilprioritet, konfliktdetektion, konfigurationsimport, atomlagring, makrornas ordning, upprepning, avbrott och felpropagation. WindowsSmoke-test för verkliga hotkeys, text- och tangentkombinationer, medieinsatser, enhetsventor, trail och ren slutning. Inga tester som slumpmässigt startar några program eller webbplatser.

GUI-test: Installation och redigering, inspelning utan utförande, klick/hotkey med samma effekt, makroavbrott, konfliktmeddelande, profiländring, enhetsutsikt, import/export och skala. Tillverkarespecifika hårdvaruundersökningar dokumenteras separat med den faktiska enheten.

Leverans: Källkod, reproducerbar bygg-/startlinje, tysk användarhandledning och lokal Windows-x64-Publish-Artefakt. Framgång innebär en utförbar app med alla generiska funktioner; Corsair-förmågor är endast markerade som tillgängliga med beprövad SDK/enhetstöd.

## Tekniska källor

- Microsoft Keyboard Input / RegisterHotKey: https://learn.microsoft.com/en-us/windows/win32/inputdev/about-keyboard-input
- Microsoft Raw Input: https://learn.microsoft.com/en-us/windows/win32/inputdev/about-raw-input
- Microsoft WPF Inträde: https://learn.microsoft.com/dotnet/desktop/wpf/advanced/input-overview
- Officiell Corsair iCUE SDK: https://github.com/CorsairOfficial/cue-sdk/blob/master/README.md

## Nästa steg

Efter att ha granskat detta projekt: utarbeta en konkret genomförandeplan och föreslå genomförandet. I den tomma arbetsordningen finns det ännu ingen Git-repository; detta dokument har därför inte committed.
