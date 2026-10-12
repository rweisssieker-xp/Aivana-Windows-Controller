<!-- generated-doc locale=fi-FI source=docs/licensing/2026-10-12-commercial-license-audit.md sha256=b58c6861e0b38c4495265651b5ab10349c701d8fb4582dd7d7cae5d2d865bc5f -->

> Koneavusteinen käännös. Rakenne, koodiesimerkit ja linkit tarkistetaan; tämä ei tarkoita äidinkielisen tarkistajan arviota tai oikeudellista hyväksyntää. Historialliset raportit säilyttävät alkuperäiset päivämääränsä ja rajauksensa. [Alkuperäinen lähde](../../licensing/2026-10-12-commercial-license-audit.md)

# MAKRIVO: Kaupan kerran myytävien tarkastus

Pysy: 12. Lokakuussa 2026. Valmistaja: Aivana.

## Tulokset

Tarkastetussa varastossa ei tunnustettu yleistä velvollisuutta paljastaa omaa MAKRIVO lähdekoodia. Yksittäinen myynti näyttää mahdolliselta, mutta se ei ole vielä täysin valmistunut. Erityisesti Corsair SDK-DLL: n luvan antaminen ei ole todistettu. Tämä tutkimus ei korvaa oikeudellista vapautusta.

## Tekniset ja lisenssitoiminnot

| komponentti | Löytyi | Vaadittava askel |
| --- | --- | --- |
| Oma lähteen koodi | Ei löydetty omaa LICENSE, KOPIINTA tai HUOMAUTUS tutkitussa GIT-sisällössä ja sen paikallisesti saatavilla olevassa lisenssin tiedostossa. | vahvistaa oikeuksia, mukaan lukien maksuja, hankittuja koodiosia ja mahdollisia työnantajan oikeuksia; luo omat käyttöehdot. |
| .NET ja WindowsDesktop | Tutkittu riippumaton paketti `artifacts/makrivo-win-x64` sisältää version 10.0.6. Paikalliset runtime-paketit sisältävät MIT: n lisenssejä, jotka sallivat kaupallisen lähetyksen ja vaativat niiden ilmoituksen. Tutkittujen julkaisujärjestelmien mukaisia lisenssi-/ilmoitustiedostoja ei löydetty. | Tarkka lisenssiteksti ja oikeat kolmansien osapuolten ilmoitukset tosiasiallisesti myytyä runtimea sisällytetään myyntipakettiin. |
| NuGet-Riippuvuus | Tutkittujen projektitiedostojen ei löydy suoria PackageReference-tiedostoja. Tutkittu työpöytävarat-tiedosto sisältää omia hankkeita. Runtime-paketit pysyvät kuitenkin lisenssin merkityksellisinä. | Lopullisen julkaisemisen jälkeen kaikki hankkeet, siirtymäpaketit ja mukana toimitetut tiedostot tallennetaan uudelleen. |
| Corsair iCUE SDK | `artifacts/win-x64/iCUESDK.x64_2019.dll`, tiedoston versio 4.0.84.0 Paikallinen SDK-referenssi-PDF sisältää EULA:n 35–38 tulostussivustoilla: maksuton käyttö, mutta rajoitukset lähettämiseen/muuntamiseen. Siinä ei ole yksiselitteistä lupaa luovuttaa DLL:tä asiakkaille. | Ennen yhdistämistä hanki kuormittava luvan. Vaihtoehtoisesti tarkistaa paketti ilman SDK-DLL:tä, joka käyttää asiakkaan sivulla asennettuja SDK:itä; myös SDK-ehtoja sovelletaan. |
| Erilaisia paketteja | `artifacts/makrivo-win-x64` Sisältää niitä Corsair-DLL ei; `artifacts/win-x64` sovellus etsii SDK dynaamisesti useissa paikoissa. | Määritä tosiasiallinen myyntiasentaja ja tarkista sen sisältö. Yksinkertainen olemassaolo ilman DLL:tä ei vielä vahvista kaikkia yhdentymisoikeuksia. |
| OBS | Oma WebSocket asiakas tutkitussa C#-koodissa; ei tunnistettu OBS-asiakaspaketti, OBS-binary tai hankittu GPL-koodi. | Tarkkailtu protokollin integrointi ei perustele itseään GPL:n julkistamisvelvollisuudelle. Saatujen fragmenttien alkuperä on edelleen vahvistettava. |
| App Symbolit | Paikallinen Python skripti merkitsee geometrisia symboleja Pillow. Työkalu on rakennus riippuvuus. | Alkuperä ja oikeudet kaikki tosiasiallisesti myytyjä kuvia, ääniä, kirjoituksia ja muita omaisuutta. Yksin kirjoitus ei osoita kaikkien varojen oikeuksia. |
| Lisenssin hallinta | Täydellistä ostolupaa/aktivointia ei tunnisteta tarkastetussa sovelluskoodissa. | Tarvittaessa, allekirjoitettu offline-lisenssi tiedostoja. Tekninen aktivointi ja oikeudelliset käyttöehdot ovat erillisiä tehtäviä. |

## Myönteinen ostosmalli – ei vielä toteutettu

- Yksinkertainen maksu; pysyvä käyttöoikeus hankitun pääversion.
- Yksi henkilö jopa kaksi omaa PC: n; ammatillinen käyttö nimenomaisesti sallitaan.
- Sisältää virheiden korjauksia ja pienempiä päivityksiä; myöhemmät tärkeimmät versiot tarjoavat maksullisesti.
- Offline tarkastettavissa, digitaalisesti allekirjoitettu lisenssi tiedosto; yksityinen allekirjoitus ei koskaan toimittaa sovelluksen kanssa.
- Lailliset oikeudet ja vaaditut turvallisuuspäivitykset eivät saa sulkea pois pääversion säännöksestä.

## Ennen ensimmäistä myyntiä

1. Corsair -lisäoikeudet tai valmistaudu myyntiin ilman yhdistettyä SDK-DLL:tä.
2. Lisenssin ja kolmannen osapuolen tekstejä tarkkoja julkaisuvelvoitteita liitetään ja saadaan asentajalle.
3. Vahvista oikeudenmukaisuus omalla koodilla ja omaisuudella; luo EULA selkeällä laitteen, käyttäjän ja päivityksen sääntöllä.
4. Kuluttajien myyntitilanteen käsittely, mukaan lukien tietovaatimukset ja peruutus. Pysyvä ”ei palautetta latauksissa” ei riitä. Peruutusoikeuden peruuttaminen välittömästi edellyttää lakisääteisiä edellytyksiä.
5. Tarkista valmiin asentaja, mukaan lukien plugins ja kaikki siirtymävaraisuus uudelleen. Työkopio laajennetaan rinnakkain; tämä tiedosto ei ole tulevaisuuden tiedostojen julkaiseminen.

## GitHub

Tutkittu levy on julkinen. Ilman avoimen lähdekoodin käyttöoikeutta koodi ei ole automaattisesti käytettävissä. GitHub julkisten tallennuslaitosten alustojen oikeudet on kuitenkin noudatettava. Myöhemmin siirtyminen yksityiseen ei poista jo olemassa olevia kopioita tai forkkeja. Näkyvyys ja lisenssi eivät ole muuttuneet tässä tutkimuksessa.

## Todisteet ja lähteet

- Paikalliset .NET-lisenssit: `C:/Users/weiss/.nuget/packages/microsoft.netcore.app.runtime.win-x64/10.0.6/LICENSE.TXT` ja `C:/Users/weiss/.nuget/packages/microsoft.windowsdesktop.app.runtime.win-x64/10.0.6/LICENSE`
- Paikallinen .NET kolmannen osapuolen tarjoajan ilmoitus: `C:/Users/weiss/.nuget/packages/microsoft.netcore.app.runtime.win-x64/10.0.6/THIRD-PARTY-NOTICES.TXT`.
- Paikallinen Corsair SDK: `artifacts/dependencies/iCUESDK_4.0.84.zip`; viittaus PDF: n alla `sdk/iCUESDK/doc/iCUE SDK - Overview and Reference.pdf`.
- [.NET 10.0.6 MIT-lisenssi](https://raw.githubusercontent.com/dotnet/runtime/v10.0.6/LICENSE.TXT)
- [Corsair SDK-dokumentti EULA: n kanssa](https://raw.githubusercontent.com/CorsairOfficial/cue-sdk/master/docs/index.html)
- [GitHub: Lisenssi toimitiloille](https://docs.github.com/en/repositories/managing-your-repositorys-settings-and-features/customizing-your-repository/licensing-a-repository)
- [BGB § 327f – Päivitykset](https://www.gesetze-im-internet.de/bgb/__327f.html)
- [BGB § 356 – peruuttamisoikeus](https://www.gesetze-im-internet.de/bgb/__356.html)

Ei tarkastettu: täydellinen ulkoinen historia ja oikeuksien ketju, brändi, olemassa oleva myynti, tulevat plugin-paketit, täydellinen oikeudellinen tehokkuus vielä ei ole olemassa EULA.
