<!-- generated-doc locale=fi-FI source=docs/superpowers/plans/2026-10-08-windows-controller.md sha256=10a5ba49bbefc617bce40563c668e3c4628647822117087deba766713fb9b3af -->

> Koneavusteinen käännös. Rakenne, koodiesimerkit ja linkit tarkistetaan; tämä ei tarkoita äidinkielisen tarkistajan arviota tai oikeudellista hyväksyntää. Historialliset raportit säilyttävät alkuperäiset päivämääränsä ja rajauksensa. [Alkuperäinen lähde](../../../superpowers/plans/2026-10-08-windows-controller.md)

# Windows Valvontaohjelman toteutus

[English (US)](2026-10-08-windows-controller.md) | [Deutsch](2026-10-08-windows-controller.md) | [Français](2026-10-08-windows-controller.md) | [Italiano](2026-10-08-windows-controller.md)

> Agentti työntekijöille: käytä rinnakkaista eristettyä omistusta; käyttäjä nimenomaisesti pyytää täydellistä täytäntöönpanoa maksimaalisilla hyödyllisillä alihankkijoilla.

**Tavoite: ** Toimitetaan alkuperäinen Windows hotkey paneeli hyväksytyssä suunnittelussa.
**Arkkitehtuuri:** Peruskonferenssi ja peruutettava täytäntöönpano; Windows Adapterit; WPF Desktop koostumus.
**Tech Stack: ** .NET 10, WPF ja Win32 JSON. Ei maksettuja riippuvuuksia.
**Spec:** / specs/2026-10-08 - windows-controller-design.md

## Globaalit rajoitukset
Saksalainen käyttöliittymä; paikallinen tallennus; Windows x64; todellinen laitteiston kapasiteetin tila; ei näppäimistön rekisteröinti; ei oletusarvoinen käynnistys.

## Tarkastelu keskittyy
Haitalliset tuonnit eivät saa korvata tallennettuja asetuksia; pidätetyt muunntimet eivät saa saastuttaa tuotantoa; suljettu laite ei saa katoa; sulkeminen on säilytettävä jäljen saatavuus; peruutettu makro on vapautettava resursseja.

## Tehtävät ja omistajuus
- [x] Core agent: src/Aivana.Core ja testit/Aivana.Testit. Mallit, JSON-kestävyys, validointi, profiilin valinta ja MacroRunner. Käytä epäonnistuneita käyttäytymiskokeita ja suorittaa sitten kaikki keskeiset testit.
- [x] Windows Pääosat: src/Aivana.Windows. RegisterHotKey, laitteen varasto, SendInput, ennakkoprosessi ja valinnainen Corsair valaistusadapteri. tarkistaa alkuperäiset rakenteet ja rekisteröinti puhdistus turvallisen savutesti.
- [x] GUI-agentti: src/Aivana.Desktop/MainWindow.xaml ja .cs plus UI -avustajat vain. Dark dashboard, täydellinen muokkaus, profiilit, asetukset, näppäimistö ja makro-muokkaus. Rakenna sopimukset kun ne ovat olemassa.
- [x] Vanhempi: jaettuja sopimuksia, projektitiedostoja, DesktopController koostumus, sovellus/tila elinkaari, tuonti/vienti vuoropuhelut, dokumentointi, integrointi tarkastukset, julkaisu ja käynnissä sovellus savutesti.

## Yhteiset käyttöliittymät
Sopimukset ovat elossa Core/Models.cs Ohjaajan toteutus elää Desktop/DesktopController.cs. UI kutsuu ohjausmenetelmiä tämän konkreettisen luokan kautta. Asiakkaat saavat tarkkoja allekirjoituksia tehtäviään. Jokaisella on erilliset tiedostot. Vanhempi ratkaisee projektin epäonnistumisen kaikkien agenttien päättymisen jälkeen.

## Tarkastus ja toimitus
- [x] dotnet build Aivana.slnx -c Julkaisu: nolla virheitä.
- [x] dotnet run --project tests/Aivana.Tests -c Julkaisu: käyttäytymiskokeet kulkevat.
- [x] dotnet publish src/Aivana.Desktop -c Release -r win-x64 --self-contained true -o artifacts/win-x64.
- [x] julkaistu täytäntöönpanokelpoinen, tarkastettava Windows käyttöliittymä ja prosessin vakaus, turvallinen profiili / muokkaus / jatkuvuus.
- [x] Kirjoita README rakennus- ja käynnistysohjeet ja laitteiston rajoitukset; rekisterikokeet ja jäävät vain laitteistotesti.
