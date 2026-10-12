<!-- generated-doc locale=sr-Latn source=docs/licensing/2026-10-12-commercial-license-audit.md sha256=b58c6861e0b38c4495265651b5ab10349c701d8fb4582dd7d7cae5d2d865bc5f -->

> Mašinski potpomognut prevod. Struktura, primeri koda i veze se proveravaju; to ne znači pregled izvornog govornika ili pravno odobrenje. Istorijski izveštaji zadržavaju izvorne datume i obim. [Originalni izvor](../../licensing/2026-10-12-commercial-license-audit.md)

# MAKRIVO: Ispitivanje za komercijalnu prodaju

Stani: 12. oktobar 2026. Proizvođač: Aivana.

## Rezultat

U proverenom skladištu nije prepoznata opšta obaveza da se otkrije svoj izvorni kod MAKRIVO. Proizvodnja je moguća, ali još uvek nije u potpunosti pripremljena. Posebno, dozvola za isporuku za postojeće Corsair DLL nije dokazana. Ovaj pregled ne zamenjuje pravno oslobođenje.

## Tehnički i licencirani rezultati

| Sastojak | Pronađite | Neophodni korak |
| --- | --- | --- |
| sopstveni izvorni kod | Nije pronađen svoj LICENS, COPING ili NOVOSTI u ispitanom Git sadržaju i istoriji lokalno dostupnog licenciranog datoteke. | Potvrditi imovinu prava, uključujući doprinose, preuzete fragmente koda i moguće prava poslodavca; stvoriti svoje uslove korišćenja. |
| .NET / Vindovs Desktop | Istražena nezavisna paketa pod `artifacts/makrivo-win-x64` sadrži verziju 10.0.6. Lokalni radni paketi sadrže MIT licence koje omogućavaju komercijalnu isporuku i zahtevaju da se dodaju njihove informacije. U pregledanim redosledima nije pronađeno odgovarajuće licencijske/notične datoteke. | Točni tekstovi licence i relevantne informacije treće strane o stvarno isporučenom radnom vremenu uključuju u paket prodaje. |
| NuGet - Uspostavljanje | U istraženim projektnim datotekama nisu pronađeni direktni paketni referenci. Istraženi datoteku desktop-aktiva sadrži sopstvene projektne obaveze. Ipak, radni paketi ostaju relevantni za licence. | Kada je konačno objavljen, sve projekte, tranzitivne pakete i isporučene datoteke ponovo inventarizuju. |
| Corsair iCUE SDK | `artifacts/win-x64/iCUESDK.x64_2019.dll`, verzija datoteke 4.0.84.0 Na lokalnom SDK referentnom PDF-u na sajtovima 35–38 se nalazi EULA: besplatna upotreba, ali ograničenja za prenos / isporuku. U tome nije utvrđeno jednostavno dozvolu za prenos DLL-a kupcima. | Pre nego što se uklonite, dobijte dozvolu za isporuku. Alternativno, proverite paket bez SDK-a koji koristi SDK instaliran na strani klijenta; takođe se primenjuju uslovi SDK. |
| Različiti paketi | `artifacts/makrivo-win-x64` Sadrži one Corsair-DLL ne; `artifacts/win-x64` Aplikacija dinamično traži SDK na nekoliko lokacija. | Odrediti stvarni prodavac i proveriti njegov sadržaj. Samo postojanje paketa bez DLL-a još uvek ne potvrđuje sve prava integracije. |
| OBS | Svoj WebSocket klijent u ispitanom C# kodu; nije identifikovan OBS klijentski paket, OBS binarni ili preuzeti GPL kod. | Primećena integracija protokola ne opravdava određenu obavezu o otkrivanju GPL-a. Poreklo mogućih fragmenata ostaje da se potvrdi. |
| App simboli | Lokalni Python skript označuje geometrijske simbole sa Pillow. Instrument je izgrađena zavisnost. | Poreklo i prava svih stvarno isporučenih slika, zvuka, pisma i drugih imovine. Samo scenario ne dokazuje prava na svaki imovinu. |
| Upravljanje licencama | Nije identifikovan kompletni pregled kupovine/aktivacije u ispitanom kodu aplikacije. | Ako je potrebno, implementirati ofline licence datoteke. Tehnička aktivacija i zakonski uslovi korišćenja su odvojene zadatke. |

## Model kupovine - još uvek nije realizovan

- Jednokratno plaćanje; trajno korišćenje na kupljenom glavnom izdanju.
- Jedna osoba na do dva sopstvenog računara; profesionalno korišćenje izričito dozvoljava.
- Uključuju popravke grešaka i manje ažuriranja; kasnije glavne verzije se nude opcionalno.
- Online pregled, digitalno potpisan licencijski datoteku; privatni potpis nikada ne izdaju sa aplikacijom.
- Zakonske prava i neophodne ažuriranja bezbednosti ne mogu biti pojedinačno isključene u glavnoj verziji.

## Pre prve prodaje

1. Corsair-očistite prava na isporuku ili pripremite prodaju bez povezanih SDK DLL.
2. Dodajte licence i tekstove treće strane tačnih obaveza za izdavanje i dobijte ih u instalatoru.
3. Potvrditi vlasništvo na sopstvenom kodu i na imovini; kreirati EULA sa jasnim pravilom uređaja, korisnika i ažuriranja.
4. Za potrošačke prodaje, obrađivanje čeka, uključujući obaveze za informacije i povlačenje. Nedostatak "nedostatak" za povraćanje nije dovoljan. Ukidanje prava na povlačenje u slučaju neposrednog isporuke podleže zakonskim uslovima.
5. Ponovo proverite završenu instalaciju, uključujući plugine i sve tranzitivne zavisnosti. Radna kopija se proširuje paralelno; ovaj snimak nije objavljivanje budućih datoteka.

## GitHub

Istražena repozitorija je javna. Bez licence otvorenog izvora, kod se ne može automatski koristiti. Međutim, treba uzeti u obzir prava GitHub platforme za javne repozitorije. Kasnije prebacivanje na privatne ne uklanja već postojeće kopije ili forke. Vidimost i licenca nisu promenjeni tokom ovog ispitivanja.

## Dokazi i izvori

- Lokalni .NET licence: `C:/Users/weiss/.nuget/packages/microsoft.netcore.app.runtime.win-x64/10.0.6/LICENSE.TXT` i `C:/Users/weiss/.nuget/packages/microsoft.windowsdesktop.app.runtime.win-x64/10.0.6/LICENSE`
- Lokalni .NET treći provajder: `C:/Users/weiss/.nuget/packages/microsoft.netcore.app.runtime.win-x64/10.0.6/THIRD-PARTY-NOTICES.TXT`
- Lokalni Corsair SDK: `artifacts/dependencies/iCUESDK_4.0.84.zip`; izvučen referentni PDF pod `sdk/iCUESDK/doc/iCUE SDK - Overview and Reference.pdf`.
- [.NET 10.0.6 Mit licenca](https://raw.githubusercontent.com/dotnet/runtime/v10.0.6/LICENSE.TXT)
- [Corsair SDK dokumentacija sa EULA](https://raw.githubusercontent.com/CorsairOfficial/cue-sdk/master/docs/index.html)
- [GitHub: Licenziranje repozitorija](https://docs.github.com/en/repositories/managing-your-repositorys-settings-and-features/customizing-your-repository/licensing-a-repository)
- [BGB § 327f – Ažuriranja](https://www.gesetze-im-internet.de/bgb/__327f.html)
- [BGB § 356 – Pravo na povlačenje](https://www.gesetze-im-internet.de/bgb/__356.html)

Neprovereno: puna istorija spoljnih doprinosa i prava, izdavanje brendova, postojeći prodajni čekut, budući Plugin paketi, potpuna pravna efikasnost još ne postojeće EULA.
