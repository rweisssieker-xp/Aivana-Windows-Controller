<!-- generated-doc locale=hu-HU source=docs/superpowers/plans/2026-10-08-windows-controller.md sha256=10a5ba49bbefc617bce40563c668e3c4628647822117087deba766713fb9b3af -->

> Gépi fordítással készült dokumentáció. A szerkezetet, kódpéldákat és hivatkozásokat ellenőrizzük; ez nem jelent anyanyelvi lektorálást vagy jogi jóváhagyást. A korábbi jelentések megőrzik eredeti dátumukat és hatókörüket. [eredeti forrás](../../../superpowers/plans/2026-10-08-windows-controller.md)

# Windows A vezérlő végrehajtási terv

[English (US)](2026-10-08-windows-controller.md) | [Deutsch](2026-10-08-windows-controller.md) | [Français](2026-10-08-windows-controller.md) | [Italiano](2026-10-08-windows-controller.md)

> Ügynökség: használja a párhuzamos elszigetelt tulajdon; a felhasználó kifejezetten kért a teljes végrehajtás a maximális hasznos alvállalkozások.

**Cél:** A jóváhagyott tervezésben a Windows hotkey panel szállítása.
**Architektúra:** Alapkonfiguráció és törölhető végrehajtás; Windows Adapterek; WPF Desktop összetétel.
**Tech Stack: ** .NET 10, WPF, Win32, JSON. Nincs fizetett függőség.
**Spec: ** ../specs/ 2026-10-08-Windows-kontroll-design.md

## Globális korlátok
Német UI; helyi tárolás; Windows x64; igaz hardver kapacitási állapot; nincs billentyűzet logging; nincs alapértelmezett autostart.

## Fókusz áttekintése
A rosszindulatú import nem helyettesíti a mentett beállításokat; a tartott módosítók nem szennyezhetik a kimenetet; a nem csatlakoztatott hardver nem ütközik össze; a zárásnak meg kell őriznie a rendelkezésre álló nyomokat; a törölt makroknak szabadon kell bocsátaniuk az erőforrásokat.

## Feladatok és tulajdon
- [x] Core agent: src/Aivana.Core and tests/Aivana.Test. modellek, JSON állóképesség, validáció, profilválasztás és MacroRunner.
- [x] Windows ügynök: src/Aivana.Windows RegisterHotKey, készülék inventory, SendInput, előzményes folyamat és opcionális Corsair világítási adapter.
- [x] GUI ügynök: src/Aivana.Desktop/MainWindow.xaml és .cs plusz UI segédprogramok csak. sötét dashboard, teljes szerkesztő, profilok, eszközök, beállítások, billentyűzet áttekintése és macro szerkesztő.
- [x] Szülő: megosztott szerződések, projektfájlok, DesktopController összetétel, alkalmazás/távolítási életciklus, import/export párbeszédek, dokumentáció, integrációs ellenőrzések, kiadása és futtatása alkalmazás füstvizsgálat.

## Megosztott interfészek
Szerződések élő Core/Models.cs. Controller végrehajtása élő Desktop/DesktopController.cs. UI hívja a Controller módszerek keresztül a konkrét osztály. Ügynökök kapnak pontos aláírásokat a feladatok. Minden tulajdonában külön fájlokat. Szülő megoldja a projekt-csúcsokat után minden ügynök befejeződött.

## Ellenőrzés és szállítás
- [x] dotnet build Aivana.slnx -c Kiadás: null hibák.
- [x] dotnet fut -projekt tesztek/Aivana.Test -c Kiadás: viselkedési ellenőrzések át.
- [x] dotnet publikálja src/Aivana.Desktop -c Release -r win-x64 --self-containing true -o artifacts/win-x64.
- [x] Megjelent végrehajtható, ellenőrizze Windows UI és folyamat stabilitása, gyakorolja a biztonságos profil / szerkesztés / tartós áramlást.
- [x] Írja meg a README-t építési/indítási utasításokkal és hardverkorlátozásokkal; felvételi ellenőrzések és a maradék hardver-csak tesztelés.
