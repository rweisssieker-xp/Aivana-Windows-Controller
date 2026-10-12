<!-- generated-doc locale=hu-HU source=docs/superpowers/specs/2026-10-08-windows-controller-design.md sha256=f36d496c6af5190889e9a31487a8fc6afdeba5af18facdd600639ede5ddbd169 -->

> Gépi fordítással készült dokumentáció. A szerkezetet, kódpéldákat és hivatkozásokat ellenőrizzük; ez nem jelent anyanyelvi lektorálást vagy jogi jóváhagyást. A korábbi jelentések megőrzik eredeti dátumukat és hatókörüket. [eredeti forrás](../../../superpowers/specs/2026-10-08-windows-controller-design.md)

# MAKRIVO – Tervezés

[English (US)](2026-10-08-windows-controller-design.md) | [Deutsch](2026-10-08-windows-controller-design.md) | [Français](2026-10-08-windows-controller-design.md) | [Italiano](2026-10-08-windows-controller-design.md)

Állapota: 8. október 2026 Állapota: a végrehajtás előtt történő vizsgálathoz.

## Cél és megállapodás

Egy önálló Windows alkalmazás egy normál PC billentyűzet egy szabadon konfigurálható akciópanel hasonló áramlat. A felhasználó megerősítette az összes javasolt akciótípusok, makrok, eszközfelismerés, profilok és egy jó GUI. A csatlakoztatott Corsair billentyűzetek és egyéb billentyűzetek felismerik; további gyártói funkciók csak akkor kapnak, ha ténylegesen támogatják őket.

Elfogadások: német felület, helyi használata számla nélkül, Windows 11 x64 első cél, nincs vezérlő telepítése. A billentyűzet továbbra is használható a szokásos íráshoz. Globális billentyűzetek szabványos billentyűkombinációkat használnak a módosítókkal.

## Technikai döntés

Szülő C#/.NET-10-App A M WPF Az MVV stabil .NET-10-SDK A munkaterületben elérhető. WPF lehetővé teszi az egyéni asztali felületet és a közvetlen Windows-Integráció. alternatívák egy külön webes felületet Windows-Szolgáltatás vagy platformokon átnyúló asztali alkalmazás; mindkettő további működési és kommunikációs komplexitást biztosít a Windows-A native variant ezért a kiválasztott alap.

Elosztás: asztali projekt a látás és a nyomtatás, a konfiguráció és a végrehajtás logikája, a Windows projekt a hotkeys, a bejárati szimuláció és a készülékek, a tesztprojekt a platformfüggetlen szabályok. A gyártó adapterek függnek egy szilárd kapacitási felület és függetlenek a szabványos akcióktól.

## felszín

sötét design nyugodt anthrazit színű területek, jól olvasható írás és turkis színes akcentus. linkek navigáció a dashboard, profilok, eszközök és beállítások. felett aktív profil, keresés és váltó, hogy megáll a hotkeys. A felület továbbra is használható 100–200 százalékos Windows szkaláció.

A képernyő egy személyre szabható képernyővel rendelkezik. Minden képernyő jelzi a szimbólumot, a nevét, a hotkey-t és a végrehajtási állapotot. A kattintás ugyanazt a cselekedetet hajtja végre, mint a hotkey. Jobb oldalon egy szerkesztő nyílik meg a cselekvési típus, a paraméterek, a hotkey-felvétel, a szimbólum/szín, az aktiválás és a tesztfelület. Új, duplikálva, törölve és átalakítva elérhető. A billentyűzet megjelenítése rendszerint jelzi a német szabványos elrendezéshez szükséges beállításokat; nem állítja meg a felismerett eszköz pontos geometriáját.

A profilok létrehozhatók, másolhatók, átnevezhetők és manuálisan aktiválhatók. Opcionálisan egy folyamatnevet adnak a profilhoz. Egy megfelelő alapprogram aktiválja a profilját, különben a globális szabványos profil érvényes. Több megfelelő profil esetében egy kifejezetten látható sorrend érvényes. A szerkesztő a Hotkey konfliktusokat közvetlenül a helyszínen magyarázza.

## Hotkeys és bejáratok

Windows RegisterHotKey feldolgozza a normál globális kombinációkat a nyomtatott billentyű visszaállítása. Csak az aktív profil regisztrálódik. A foglalott vagy már rögzített kombinációk hibaként jelennek meg; az alkalmazás nem jelentette be őket, mint sikeresen aktivált. A szerkesztőben lévő felvétel nem oldja meg a műveleteket.

A szimulált bevitelek előtt a kiinduló módosító gombok szabadulnak meg, vagy fizikai kiadásuk várható. Az Unicode szöveg és a gombkombinációk a SendInput segítségével kerülnek elküldésre. Minden önnyomott gombot a törlés esetén is szabadulnak meg. A műveletek az UI-szalagon kívül zajlanak; a végrehajtás és a profilváltozás nem zárhatja meg a felületet.

## Akciók és makrok

Akciótípusok: Program indítása érvekkel és munkafüzet, megnyitása fájl / fájl a Windows, megnyitása HTTP / HTTPS weboldal, beírja az Unicode szöveg, küldjön gombkombinációt, média Play / Pause / Következő / Előző / Állítsa meg, hang magasabb / alacsonyabb / zaj, valamint a profil. A paraméterek megerősítik, mielőtt a tárolás és újra a futás.

Makrook tartalmaz egy soros listát a fent említett akciók és a kifejezett lépések. lépések hozzáadható, szerkeszthető és mozgatható. ismétlések véglegesen konfigurálható (1–100); végtelen csavarok nem tartoznak az első verzió. A futó makro befejeződik a felület vagy a saját leállási kulcs. ismételt kiindítása ugyanaz a futó beállítást nem indul párhuzamos futás. Különböző makrook beállítási szimulációt soroznak, hogy a billentyűzetek ne keveredjenek össze.

## Eszközök és gyártók

A készülékek tárolása Windows Raw Input és kiegészítő készülékek információi: A megjelenítési név, a készülék útja, a gyártó/termék azonosítása, amennyiben rendelkezésre áll, a csatlakozási állapot és a felismerett képességek. A csatlakozások/felismerések frissítik a listát újbóli indítás nélkül. A fizikai készülék több HID-bejegyzéssel rendelkezik; ezek a terhelhető azonosítók szerint csoportosulnak, különben láthatóan a külön bejegyzésekkel.

A készüléknézet információkat és képességeket nyújt; egy külön billentyűzet, amely kizárólag a billentyűzeteket tartalmazza, nem része ennek a megállapodásnak.

Corsair-A felismerés is működik anélkül, hogy iCUE. Egy opcionális iCUE-Az SDK adapter csatlakozási állapotot és támogatott világítási akciókat biztosít, ha az SDK, iCUE és kompatibilis eszközök vannak. Az UI különbözik felismerhető, SDK elérhető, csatlakoztatható és valójában vezérlő. Az SDK dokumentálva elsősorban LED vezérlés; a saját G gombok csak a szokásos hotkeys támogatják, ha Windows A bejegyzést nem kapja meg. Nincs állandó ígéret minden nyomtatásra.

Más gyártók először generikus felismerés és a szabványos akciók. Az adapter felület lehetővé teszi a későbbi integrációk; nem támogatott képességek őszintén jelenik meg.

## Tárolás és mindennapi élet

A helyi alkalmazás adatbázisában a JSON konfigurációt. Az atom tárolása az utolsó érvényes biztonsági mentéssel; a hibás fájlok nem írnak le. Az import a formátumot, a verziót, az azonosítókat és a paramétereket vizsgálja, és előzetes átvételét mutatja. Az importált akciók csak kifejezett aktiválása után hajthatók végre. Az export profilokat és betűtípusokat tartalmaz.

Tray menü: Nyissa meg, profilválasztás, Hotkeys szünet, makros szünet, befejezése. ablakok zárva minimális szabványos a Tray és magyarázza ezt az első alkalommal. autostart opcionális és szabványos. csak egy eset kezeli Hotkeys. befejezése regisztrálja Hotkeys, törik a futamokat, és adja meg az erőforrásokat.

Helyi aktivitási megjelenítés korlátozott időtartamú siker, hiba és törés. Nincs protokoll minden billentyűzetbejegyzésre. A szövegre vonatkozó tartalom nem kerül kiírásra hibaüzenetekben. Nincs felhőszolgáltatás és nem telemetria.

## Hibaelhárítás és határok

Hiányzó programok, érvénytelen URL-k, letöltött hotkeys, sérült profilok és nem elérhető gyártó SDK kap konkrét német üzeneteket. A adapter hiba nem befolyásolja a szabványos akciókat. A bejegyzések a magasabb kiváltságos alkalmazások nem sikerül Windows jogosultság határait; hiba magyarázható, az alkalmazás nem igényel szabványos adminisztrációs jogokat.

## Vizsgálat és csökkenés

Automatizált tesztelés profil prioritás, konfliktus felismerése, konfigurációs import, atomerőmű tárolás, makrórend, ismétlés, törés és hibapropagáció. Windows Smooke tesztelés valós hotkeys, szöveg és billentyűkombinációk, média akciók, eszközök, nyomtatás és tiszta befejezés. Nincs tesztelés, amely véletlenül indít bármilyen programot vagy weboldalt.

GUI tesztelés: beépítés és szerkesztés, futtatás nélküli felvételek, ugyanaz hatású kattintások/hotkey, makrofelbontás, konfliktusjelentések, profilváltozás, eszközök megjelenése, import/export és skálázás. A gyártó-specifikus hardvervizsgálat külön dokumentálódik a valósan meglévő eszközzel.

Szállítási: forráskód, reprodukálható építési/indítási útmutató, német használati útmutató és helyi Windows-x64-Publish-Artefakt. A siker egy végrehajtható alkalmazás minden általános funkcióval; Corsair képességek csak bizonyított SDK/eszköz támogatás esetén jelennek meg.

## Technikai források

- Microsoft Keyboard Bevezetés / RegisterHotKey: https://learn.microsoft.com/en-us/windows/win32/inputdev/about-keyboard-input
- Microsoft Raw Input: https://learn.microsoft.com/en-us/windows/win32/inputdev/about-raw-input
- Microsoft WPF Bevezetés: https://learn.microsoft.com/dotnet/desktop/wpf/advanced/input-overview
- Hivatalos Corsair iCUE SDK: https://github.com/CorsairOfficial/cue-sdk/blob/master/README.md

## Következő lépés

A tervezet vizsgálata után: konkrét végrehajtási tervet készítsen és a végrehajtás kiválasztására. az üres munkafolyamatban még nincs Git-repozítor; ezért ez a dokumentum még nem történt meg.
