<!-- generated-doc locale=hr-HR source=docs/licensing/2026-10-12-commercial-license-audit.md sha256=b58c6861e0b38c4495265651b5ab10349c701d8fb4582dd7d7cae5d2d865bc5f -->

> Strojno potpomognut prijevod. Struktura, primjeri koda i poveznice provjeravaju se; to ne znači pregled izvornog govornika ili pravno odobrenje. Povijesna izvješća zadržavaju izvorne datume i opseg. [Originalni izvor](../../licensing/2026-10-12-commercial-license-audit.md)

# MAKRIVO: Pregled za komercijalne prodaje

Stanište: 12. listopad 2026. Proizvođač: Aivana.

## Rezultati

U ispitivanju se nije utvrdila opća obveza objavljivanja vlastitog izvornog koda MAKRIVO. Proizvodnja se čini mogućom, ali još nije u potpunosti pripremljena. Konkretno, dozvola za prijenos za postojeće Corsair SDK-DLL nije dokazana. Ova ispitivanja ne zamjenjuju pravno oslobođenje.

## Tehnički i licencijski rezultati

| Sastavni dio | Pronađeno | Potrebni korak |
| --- | --- | --- |
| vlastiti izvorni kod | Nema vlastite LICENCE, COPYING ili NOTICE pronađen u pregledanom GIT sadržaju i lokalno dostupnom povijesti licencijskog datoteka. | Potvrditi imovinu prava, uključujući doprinose, preuzete fragmente koda i moguće prava poslodavca; stvoriti vlastite uvjete korištenja. |
| .NET i Windows Desktop | Istraženi samostalni paket pod `artifacts/makrivo-win-x64` sadrži verziju 10.0.6. Lokalni paketi za rad sadrže MIT dozvole koje omogućuju komercijalnu prijenos i zahtijevaju dodavanje njihovih informacija. U pregledanim redoslijedima nije pronađeno odgovarajuće licencijsko/notično datoteke. | Točni tekstovi licencije i relevantne informacije trećih strana o stvarno isporučenom radnom vremenu uključiti u paket prodaje. |
| NuGet – uzimanje u obzir | U pregledanim projektnim datotekama nisu pronađeni izravni paketi. Istraženi Desktop Assets datoteka sadrži vlastite projektne obveze. Ipak, paketi za vožnju ostaju relevantni za licencu. | Nakon konačnog izdanja, sve projekte, tranzitne pakete i dostavljene datoteke ponovno inventarizirati. |
| Corsair iCUE SDK | `artifacts/win-x64/iCUESDK.x64_2019.dll`, verzija datoteke 4.0.84.0 Lokalni referentni PDF sadrži EULA na tiskanim stranicama 35–38: besplatno korištenje, ali ograničenja za prijenos/odstupanje. U njemu nije utvrđeno jednoznačno dopuštenje za prijenos DLL-a kupcima. | Prethodno je potrebno dobiti dozvolu za isporuku. Alternativno, provjerite paket bez SDK-a koji koristi SDK koji je instaliran na strani klijenta; također se primjenjuju uvjeti SDK. |
| Različiti paketi | `artifacts/makrivo-win-x64` Sadrži se Corsair-DLL ne; `artifacts/win-x64` Aplikacija traži SDK dinamično na nekoliko mjesta. | Određivanje stvarnog prodajnog uređaja i provjeravanje sadržaja. Samo postojanje paketa bez DLL-a još uvijek ne potvrđuje sve prava na integraciju. |
| OBS | Sam klijenta WebSocket u ispitivanju C# koda; nema identifikacije OBS paketa klijenta, OBS binarnog ili preuzetog GPL koda. | Pronađena integracija protokola sama po sebi ne podrazumijeva utvrđenu obvezu otkrivanja GPL-a. Potrebno je potvrditi podrijetlo prikupljenih fragmenta. |
| App simboli | Lokalni Python skript označava geometrijske simbole s Pillow. To je alat koji se temelji na izgradnji. | Podrijetlo i prava svih stvarno isporučenih slika, zvuka, pisma i drugih imovine dokumentiraju. Sam scenarij ne dokazuje prava na svaki imovinu. |
| Upravljanje licencama | Nema potpune provjere kupovne dozvole/aktiviranja u ispitivanju primjene. | Ako je potrebno, provoditi potpisane offline dozvole. Tehnička aktivacija i pravni uvjeti korištenja su odvojen zadatak. |

## Smatrajući model kupnje – još nije proveden

- Jednom plaćanju; trajno korištenje na kupljenoj glavnoj verziji.
- Jedna osoba na do dva vlastita PC-a; izričito dopušta profesionalnu upotrebu.
- Uključite pogrešne korekcije i manje ažuriranja; kasnije glavne verzije ponude opcionalno plaćeno.
- Offline provjerljiva, digitalno potpisana licencijska datoteka; privatni ključi nikada ne isporučuju s aplikacijom.
- Zakonske prava i potrebne sigurnosne ažuriranja ne smiju biti izuzete u skladu s glavnim pravilom o verziji.

## Prije prvog prodaje

1. Corsair-preuzimanje prava ili pripremaju prodaju bez vezanog SDK-DLL.
2. Priključite i primite tekstove licencije i trećih strana točnih obveza za izdavanje.
3. Potvrditi vlasništvo vlastitim kodovima i imovinom; stvoriti EULA s jasnim pravilima o uređaju, korisniku i nadogradnji.
4. Za potrošačke prodaje obrađivanje provjere, uključujući obaveze o informacijama i povlačenje. Jedan fiksni "bez povratnih naknada za preuzimanje" nije dovoljno. Uklanjanje prava na povlačenje u slučaju neposredne dostave predviđa zakonske uvjete.
5. Preispitivanje završenog instalera, uključujući plugine i sve transitivne ovisnosti. Radna kopija se proširuje paralelno; ovaj arhiv nije objavljivanje budućih datoteka.

## GitHub

Istraživački ured je javni. Bez dozvole otvorenog izvora, kod se ne otvara za bilo kakvu ponovnu upotrebu. Međutim, prava platforme GitHub za javne urednice trebaju se poštovati. Kasnije prebacivanje na privatno ne uklanja već postojeće kopije ili forke. Vidljivost i dozvola nisu izmijenjeni tijekom ovog ispitivanja.

## Dokazi i izvori

- Lokalne licencije .NET: `C:/Users/weiss/.nuget/packages/microsoft.netcore.app.runtime.win-x64/10.0.6/LICENSE.TXT` i `C:/Users/weiss/.nuget/packages/microsoft.windowsdesktop.app.runtime.win-x64/10.0.6/LICENSE`
- Lokalni .NET treći pružatelj obavijesti: `C:/Users/weiss/.nuget/packages/microsoft.netcore.app.runtime.win-x64/10.0.6/THIRD-PARTY-NOTICES.TXT`.
- Lokalni Corsair SDK: `artifacts/dependencies/iCUESDK_4.0.84.zip`; izvedeno referentni PDF pod `sdk/iCUESDK/doc/iCUE SDK - Overview and Reference.pdf`.
- [.NET 10.0.6 MIT licencija](https://raw.githubusercontent.com/dotnet/runtime/v10.0.6/LICENSE.TXT)
- [Corsair SDK dokumentacija s EULA-om](https://raw.githubusercontent.com/CorsairOfficial/cue-sdk/master/docs/index.html)
- [GitHub: Licentirati repozitorije](https://docs.github.com/en/repositories/managing-your-repositorys-settings-and-features/customizing-your-repository/licensing-a-repository)
- [BGB § 327f – ažuriranje](https://www.gesetze-im-internet.de/bgb/__327f.html)
- [BGB § 356 – pravo na povlačenje](https://www.gesetze-im-internet.de/bgb/__356.html)

Ne procijenjeni: potpuna povijest vanjskih doprinosa i pravnih lanca, dodjeljivanje marke, postojeći prodajni šekkout, budući plugin paketi, potpuna pravna učinkovitost još ne postoji EULA.
