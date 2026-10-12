<!-- generated-doc locale=hu-HU source=docs/licensing/2026-10-12-commercial-license-audit.md sha256=b58c6861e0b38c4495265651b5ab10349c701d8fb4582dd7d7cae5d2d865bc5f -->

> Gépi fordítással készült dokumentáció. A szerkezetet, kódpéldákat és hivatkozásokat ellenőrizzük; ez nem jelent anyanyelvi lektorálást vagy jogi jóváhagyást. A korábbi jelentések megőrzik eredeti dátumukat és hatókörüket. [eredeti forrás](../../licensing/2026-10-12-commercial-license-audit.md)

# MAKRIVO: A kereskedelmi egyszeri értékesítés vizsgálata

Állás: 12. október 2026. gyártó: Aivana.

## Az eredmény

A vizsgálatban nem ismert általános kötelezettség a saját MAKRIVO forráskód megnyitása. A tulajdonos egyszeri értékesítése lehetséges, de még nem készült teljesen. Különösen a meglévő Corsair SDK-DLL-hez való átadás engedélyét nem bizonyították. Ez a vizsgálat nem helyettesíti a jogi kiadásokat.

## Technikai és engedélyezési eredmények

| összetevők | Találatok | Szükséges lépés |
| --- | --- | --- |
| Saját forráskód | Nincs saját LICENCE, COPYING vagy NOTICE megtalálható a vizsgált GIT tartalom és a helyi elérhető licenc fájl története. | Jogok megerősítése, beleértve a hozzájárulásokat, a felvett kódfragmentákat és a munkáltató lehetséges jogait; saját használati feltételek létrehozása. |
| .NET és WindowsDesktop | A vizsgálat során a `artifacts/makrivo-win-x64` oldalon található önálló csomag tartalmazza a 10.0.6 verziót. A helyi futóidő csomagok MIT-licenceket tartalmaznak, amelyek engedélyezik a kereskedelmi átadásokat és megkövetelik az értesítések hozzáadását. | Pontos licenc szövegek és a tényleges harmadik felek értesítéseit a tényleges forgalmazási időt tartalmazza az értékesítési csomagban. |
| NuGet - A kötelezettségvállalás | A vizsgált projektfájlokban nem találtak közvetlen PackageReference bejegyzéseket. A vizsgált asztali eszközök fájl saját projektfüggőségeket tartalmaz. A futási idő csomagjai azonban licenciálisan relevánsak maradnak. | A végleges kiadásakor az összes projektet, átmeneti csomagot és a szállított fájlokat újra feljegyezzük. |
| Corsair iCUE Az SDK | `artifacts/win-x64/iCUESDK.x64_2019.dll`, fájlverzió 4.0.84.0. A helyi SDK-referencia PDF tartalmazza az EULA nyomtatott oldalakon 35–38: díjmentesen használható, de a továbbításra/átadásra vonatkozó korlátozások. | Alternatív módon, ellenőrizze az SDK-mentes csomagot, amely az ügyfél oldalán telepített SDK-t használja; az SDK feltételei is érvényesek. |
| Különböző csomag | `artifacts/makrivo-win-x64` nem tartalmazza a Corsair DLL-t; `artifacts/win-x64` tartalmazza azt. Az alkalmazás több helyen dinamikusan keres az SDK-t. | Határozza meg a tényleges értékesítési telepítőt és ellenőrizze annak tartalmát. A DLL-mentes csomag kizárólagos létezése még nem igazolja az összes integrációs jogot. |
| OBS | Saját WebSocket ügyfél a vizsgált C# kód; nincs OBS ügyfélcsomag, OBS bináris vagy felvett GPL kódot azonosított. | A megfigyelt protokollintegráció nem indokolja a GPL nyilvánosságra hozatali kötelezettségét. a felvett töredékek eredetét meg kell erősíteni. |
| App szimbólumok | A helyi Python szkript a geometriai szimbólumokat Pillow jelzi. | A származás és a jogok minden ténylegesen eladott képek, hangok, írások és egyéb eszközök dokumentálása. a forgatókönyv egyedül nem bizonyítja a jogokat minden eszközön. |
| Licenckezelés | Nincs teljes vásárlási engedély/aktiválási ellenőrzés azonosítva a vizsgált alkalmazási kódban. | Amennyiben kívánatos, aláírt offline licencfájlok végrehajtása. technikai aktiválás és a jogi használati feltételek külön feladatokat. |

## Értelmes vásárlási modell – még nem megvalósult

- Egyszeri fizetés; a megvásárolt fő verzió tartós használati joga.
- Egy személy legfeljebb két saját PC-k; szakmai használata kifejezetten megengedi.
- A hibák kijavítása és a kisebb frissítések; a későbbi fő verziók opcionálisan díjmentesek.
- Offline ellenőrizhető, digitálisan aláírt licencfájl; a magán aláírás kulcsok soha nem adnak ki az alkalmazással.
- A törvényes jogok és a szükséges biztonsági frissítések nem zárhatók ki a fő verzió szabályzatban.

## Az első értékesítés előtt

1. A Corsair értékesítési jogok tisztázása vagy az eladás előkészítése az összefüggésben lévő SDK-DLL nélkül.
2. Licenc és harmadik fél szövegek a pontos kibocsátási kötelezettségek hozzáadása és a telepítő.
3. Megerősíti a jogok birtoklását a saját kódja és az eszközök; az EULA egyértelmű eszközökkel, felhasználói és frissítési szabályokkal.
4. A fogyasztói értékesítések esetében a csekket, beleértve az információs kötelezettségeket és a visszavonást. Egy állandó „nem visszatérítés a letöltések során” nem elegendő. A visszavonási jog visszavonása azonnali szállítás esetén a törvényi feltételekkel rendelkezik.
5. A befejezett telepítő, beleértve a plug-ins és az összes átmeneti függőségek újra. A munka másolatot párhuzamosan bővül; ez a fájl nem a jövőbeli fájlok megosztása.

## GitHub

A vizsgálatban lévő tároló nyilvános. nyílt forráskód engedély nélkül a kód nem lesz automatikusan szabad bármely további használathoz. A GitHub platform jogai a nyilvános tárolóhelyek számára azonban tiszteletben kell tartani. A későbbi áthelyezés a magán nem távolítja el a már meglévő másolatokat vagy forkokat. A láthatóság és a licenc nem változott ebben a vizsgálatban.

## Bizonyítékok és források

- Helyi .NET engedélyek: `C:/Users/weiss/.nuget/packages/microsoft.netcore.app.runtime.win-x64/10.0.6/LICENSE.TXT` és `C:/Users/weiss/.nuget/packages/microsoft.windowsdesktop.app.runtime.win-x64/10.0.6/LICENSE`.
- Helyi .NET harmadik fél szolgáltató értesítése: `C:/Users/weiss/.nuget/packages/microsoft.netcore.app.runtime.win-x64/10.0.6/THIRD-PARTY-NOTICES.TXT`.
- Helyi Corsair SDK: `artifacts/dependencies/iCUESDK_4.0.84.zip`; kivonatú referenciamutató PDF a `sdk/iCUESDK/doc/iCUE SDK - Overview and Reference.pdf` alatt.
- [.NET 10.0.6 MIT engedély](https://raw.githubusercontent.com/dotnet/runtime/v10.0.6/LICENSE.TXT)
- [Corsair SDK dokumentáció EULA](https://raw.githubusercontent.com/CorsairOfficial/cue-sdk/master/docs/index.html)
- [GitHub: Repositoriumok engedélyezése](https://docs.github.com/en/repositories/managing-your-repositorys-settings-and-features/customizing-your-repository/licensing-a-repository)
- [BGB § 327f – frissítések](https://www.gesetze-im-internet.de/bgb/__327f.html)
- [BGB § 356 – Visszavonási jog](https://www.gesetze-im-internet.de/bgb/__356.html)

Nem ellenőrzött: teljes külső hozzájárulás története és jogláncok, márka kibocsátása, meglévő értékesítési csekket, jövőbeli plug-in csomagok, teljes jogi hatékonyságát egy még nem jelen EULA.
