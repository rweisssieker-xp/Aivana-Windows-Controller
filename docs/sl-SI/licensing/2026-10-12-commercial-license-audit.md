<!-- generated-doc locale=sl-SI source=docs/licensing/2026-10-12-commercial-license-audit.md sha256=b58c6861e0b38c4495265651b5ab10349c701d8fb4582dd7d7cae5d2d865bc5f -->

> Strojno podprt prevod. Struktura, primeri kode in povezave se preverjajo; to ne pomeni pregleda maternega govorca ali pravne odobritve. Zgodovinska poročila ohranijo izvirne datume in obseg. [Izvirni vir](../../licensing/2026-10-12-commercial-license-audit.md)

# MAKRIVO: Pregled za enkratno prodajo

Stanišče: 12. Oktobra 2026. Proizvajalec: Aivana.

## Rezultati

V preverjenem arhivu ni bilo ugotovljeno nobeno obveznost razkritja lastnega izvornega koda MAKRIVO. Proizvodnja se zdi mogoča, vendar še ni popolnoma pripravljena. Posebno dovoljenje za prenos obstoječega Corsair SDK-DLL ni dokazano. Ta preiskava ne nadomešča pravnega izpusta.

## Tehnični in licencirani rezultati

| sestavni del | odkriti | Potrebni korak |
| --- | --- | --- |
| lastni kodo | Nobenega lastnega licence, kopiranja ali obvestila ni bilo mogoče najti v pregledani vsebini Git in zgodovini lokalno na voljo licenco. | Potrditev pravic, vključno s prispevki, prejetimi kodnimi deli in morebitnimi pravicami delodajalca; ustvarjanje lastnih pogojev uporabe. |
| .NET in Windows Desktop | Raziskovalni samostojni paket pod `artifacts/makrivo-win-x64` vsebuje različico 10.0.6. Lokalni paketi za vožnjo vsebujejo licence MIT, ki omogočajo komercialno dostavo in zahtevajo dodajanje njihovih obvestil. V pregledanih vrstnem redu ni bilo najdenih ustreznih licenc/notičnih datotek. | Natančne licence in ustrezne tretje stranke obvestila o dejansko dodeljenem urniku vključite v prodajni paket. |
| NuGet - obremenitev | V pregledanih projektnih datotek ni bilo neposrednih vnosov paketov. Raziskovani namizni arhiv vsebuje lastne projektne obveznosti. Runtime paketi pa ostanejo licencno pomembni. | Ob končnem oddajanju znova vnos vseh projektov, prehodnih paketov in dostavljenih datotek. |
| Corsair iCUE Številka | `artifacts/win-x64/iCUESDK.x64_2019.dll`, različica datoteke 4.0.84.0 V lokalnem referenčnem PDF-ju je na tiskanih straneh 35–38 vključena EULA: brezplačna uporaba, vendar omejitve za prenos/izdaj. V njej ni bilo določeno enostavno dovoljenje za posredovanje DLL strankam. | Preden se združite, pridobite obremenljivo dovoljenje za dostavo. Alternativno preverite paket brez DLL, ki uporablja stransko nameščen SDK; tudi za to veljajo pogoji SDK. |
| Različne pakete | `artifacts/makrivo-win-x64` Vsebuje jih Corsair-DLL ne; `artifacts/win-x64` Aplikacija dinamično išče SDK na več mestih. | Ugotovite dejanski prodajalec in preverite njegovo vsebino. Samo obstoj paketa brez DLL še ne potrjuje vseh pravic do integracije. |
| OBS | Lastni WebSocket stranka v pregledani C# kodo; ni OBS stranski paket, OBS binarno ali prevzeto GPL kodo. | Spremljana integracija protokola sama po sebi ne določa obveznosti razkritja GPL. Izvir morebitnih fragmentov je treba potrditi. |
| Aplikacijski simbol | Lokalni skript Python označuje geometrijske simbole z Pillow. To orodje je gradnja zavest. | Dokumentiranje porekla in pravic vseh dejansko predanih slik, zvokov, pisem in drugih sredstev. Sam scenarij ne dokazuje pravic vsakega premoženja. |
| Upravljanje licenc | Ni popolne preverjanja nakupa/aktiviranja v pregledani kodo uporabe. | Če je potrebno, izvajanje podpisanih offline licenc. Tehnična aktivacija in pravni pogoji uporabe so ločeni nalogi. |

## Modeli nakupa – še niso izvedeni

- Enkratna plačila; trajno pravico do uporabe pridobljene glavne različice.
- Oseba na do dveh lastnih računalnikih; izrecno dovoljuje poklicno uporabo.
- Vključujejo popravke napak in manjše posodobitve; poznejše glavne različice ponudijo brezplačno.
- Offline preverljiva, digitalno podpisana licenca datoteka; zasebni ključi nikoli ne izdajate z aplikacijo.
- Zakonske pravice in potrebne varnostne posodobitve ne smejo biti izključene s pravilom o glavnih različicah.

## Pred prvo prodajo odprto

1. Corsair preklic ali pripraviti prodajo brez povezanih SDK DLL.
2. Licenčni in tretji ponudnik besedila natančnih obveznosti za izdajo priložite in dobite v namestitvi.
3. Potrdite lastništvo na lastni kodo in sredstva; ustvarite EULA z jasnimi pravili za naprave, uporabnike in nadgradnjo.
4. Za potrošniške prodaje obdelujejo Checkout, vključno z informacijskimi obveznostmi in povračilom. Vendar pa ni dovolj, da se "ni vrnitve za prenos". Za razveljavitev pravice do odpovedi v primeru takojšnje dostave veljajo zakonske pogoje.
5. Preverite končni namestnik, vključno z vtičniki in vse tranzitne odvisnosti. Delovna kopija se vzporedno razširi; ta datoteka ni oddajanje prihodnjih datotek.

## GitHub

Raziskava je javna. Brez odprtega vira se kodo ne bo samodejno sprostilo za kakršno koli nadaljnjo uporabo. Vendar pa je treba upoštevati pravice platforme GitHub za javne repozitorije. Kasnejša preusmeritev v zasebno ne odstrani že obstoječih kopij ali fork. Vidljivost in licenca ni bila spremenjena v tem pregledu.

## Dokazi in viri

- Lokalne licence .NET: `C:/Users/weiss/.nuget/packages/microsoft.netcore.app.runtime.win-x64/10.0.6/LICENSE.TXT` in `C:/Users/weiss/.nuget/packages/microsoft.windowsdesktop.app.runtime.win-x64/10.0.6/LICENSE`
- Lokalni .NET tretji ponudnik obvestilo: `C:/Users/weiss/.nuget/packages/microsoft.netcore.app.runtime.win-x64/10.0.6/THIRD-PARTY-NOTICES.TXT`.
- Lokalni Corsair SDK: `artifacts/dependencies/iCUESDK_4.0.84.zip`; izvedeno referenčno PDF pod `sdk/iCUESDK/doc/iCUE SDK - Overview and Reference.pdf`.
- [.NET 10.0.6 MIT licenca](https://raw.githubusercontent.com/dotnet/runtime/v10.0.6/LICENSE.TXT)
- [Corsair SDK dokumentacija z EULA](https://raw.githubusercontent.com/CorsairOfficial/cue-sdk/master/docs/index.html)
- [GitHub: licenciranje repozitorij](https://docs.github.com/en/repositories/managing-your-repositorys-settings-and-features/customizing-your-repository/licensing-a-repository)
- [BGB § 327f – posodobitve](https://www.gesetze-im-internet.de/bgb/__327f.html)
- [BGB § 356 – pravica do odpovedi](https://www.gesetze-im-internet.de/bgb/__356.html)

Ne preverjeno: popolna zgodovina zunanjih prispevkov in pravnih verig, dodelitev blagovne znamke, obstoječe prodaje, prihodnje pakete, popolna pravna učinkovitost še ni na voljo.
