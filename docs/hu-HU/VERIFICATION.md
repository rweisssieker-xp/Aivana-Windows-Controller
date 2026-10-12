<!-- generated-doc locale=hu-HU source=docs/VERIFICATION.md sha256=f19afb9e7b5ffac90c83167416ab3af0964a4481dd16b7357f8585480c6aae65 -->

> Gépi fordítással készült dokumentáció. A szerkezetet, kódpéldákat és hivatkozásokat ellenőrizzük; ez nem jelent anyanyelvi lektorálást vagy jogi jóváhagyást. A korábbi jelentések megőrzik eredeti dátumukat és hatókörüket. [eredeti forrás](../VERIFICATION.md)

# A vizsgálat – 8. október 2026

[English (US)](../en-US/VERIFICATION.md) | [Deutsch](../de-DE/VERIFICATION.md) | [Français](../fr-FR/VERIFICATION.md) | [Italiano](../it-IT/VERIFICATION.md)

Az alkalmazás Windows x64 alatt épült, stabil .NET SDK 10.0.202 és önálló Windows-x64-Ausgabe.

## Automatizált ellenőrzés

- Összes építés: nincs figyelmeztetés és nincs hiba.
- 19 Core viselkedési tesztek: szabványos konfiguráció, validáció, nulla / duplikációs adatok, profil prioritás, import / export és biztonsági visszaállás, makrórend és ismétlés, törés, kettős kimerültség, sorozás, változatlan végrehajtási snapshot és lépés hiba.
- Windows teszt: Hotkey normalizáció és Alias nevek, szülői szerkezeti méretek, valós billentyűzet eszközök listája, biztonságos regisztráció és újrafeladás, szintetikus WM_HOTKEY csavar és törés, kikapcsolva csavar regisztráció során beállítási szimuláció.
- Az önálló EXE-t `--smoke-test` segítségével indították: az összes öt WPF nézetet visszavonják, a zárakat beállíthatják/másolhatják/törhetik, a tervezéseket a frissítés során megtartják, a konfigurációt tárolják és újra tölthetik.
- Dashboard, szerkesztő és kompakt megjelenítés (900 × 720) vizuálisan ellenőrizte, mint a valódi WPF renderek. tervezési tárolás, elutasítás, kábel/makro sorrend, német hotkey szövetség, egyedi makro szakadék és lépés előrehaladás ellenőrizte.

## Hardverfüggő határok

Az utolsó helyi áramlás négy Windows billentyűzetet észlel. Ez a szám a HID/Windows billentyűzetet írja le, nem feltétlenül négy fizikai billentyűzetet.

A hivatalos iCUE SDK telepítve az EXE mellett található. Az élő teszt csatlakozik a iCUE billentyűzethez és azonosítja a Corsair billentyűzet modellekkel és LED-információkkal. A világítás változása nem történik automatikusan; a modell-alapú nyomtatók és más gyártók nem tesztelhetők hardverrel.

A szöveges / média bejegyzéseit nem adták be más nyitott felhasználói programokba. A nemzeti adatstruktúrák és a hibák / kiadás logikája ellenőrizték; egy adott célprogram viselkedése többek között a jogosultság szintjétől függ.

## Ismételje meg

```powershell
dotnet build Aivana.slnx -c Release
dotnet run --project tests/Aivana.Tests -c Release
dotnet run --project tests/Aivana.Windows.Tests -c Release
dotnet publish src/Aivana.Desktop -c Release -r win-x64 --self-contained true -o artifacts/win-x64
& ./artifacts/win-x64/MAKRIVO.exe --smoke-test
```

A GUI teszt az eredményt és a képernyőfelvételt írja le `smoke` a vizsgált EXE mellett. A diagnózis módjaként tervezték, és a befejezést követően nem kezdődik állandó háttérkép.


## Továbbfejlesztett irányítás

Továbbá ellenőrzött: profilfeltételek folyamat és ablak címek, változó átalakítása nélkül rekurszív bővítés, makrofelügyelet végrehajtása nélkül, új akció validáció és visszaállítható régi JSON fájlok. A szülői vizsgálatok fedik a parancscscsot regisztráció, konfliktusok, WM_HOTKEY szállítás és kiadás, valamint a tiszta monitor elosztás és a szükséglet-vezérelt clipboard olvasás hozzáférés. A WPF teszt megnyitja és visszahívja a tényleges palettát, ellenőrizze a visszahívást / visszahívást és a kedvenceket, teszteli a tiszta tervezéseket a rendelő után és sikeresen befejezi a folyamatot.

Windows zár, interim változások és ablak mozgások nem alkalmazzák a felhasználói programok során a tesztelés. ablakok közzététele Windows kérések aszinkron; a konkrét célprogram lehet saját minimális méret vagy engedélyezési korlátok. profil zár és végrehajtási statisztikák vonatkoznak a folyamatos ülés.


kiterjesztési vizsgálat a 12.10.2026: 30 Core tesztelés; Windows-Adapterek, beleértve a beviteli beviteli időben történő felszabadulást, írásbeli monitorfelismerést és OBS-Status-Mock megmaradt. WPF vizsgálja a beérkezés/felvétel, a rekord indítása/megállása, profilcsere előrejelzése, gesztusok és szimbólumforrások.: 27 Katalógusok a 967 Kulcsszavak; dokumentáció: 84 oldalak négy nyelven. tulajdonos gombok más gyártók, mint Corsair nem tesztelték a hardvereket; a generikus támogatás magában foglalja a készülék specifikus eseményeket F13–F24 és médiaszög. felhő szállítás történik a kiválasztott szinkronizált mappán keresztül; Import ellenőrzik és letiltják.
