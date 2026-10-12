<!-- generated-doc locale=fi-FI source=docs/superpowers/plans/2026-10-12-six-extensions.md sha256=0ac83f2f795ba08551082caee7c775af015828dd39e200dd9857aab504cdba01 -->

> Koneavusteinen käännös. Rakenne, koodiesimerkit ja linkit tarkistetaan; tämä ei tarkoita äidinkielisen tarkistajan arviota tai oikeudellista hyväksyntää. Historialliset raportit säilyttävät alkuperäiset päivämääränsä ja rajauksensa. [Alkuperäinen lähde](../../../superpowers/plans/2026-10-12-six-extensions.md)

# MAKRIVO Laajennussuunnitelma

> **Aineiston työntekijöille: ** KÄYTTÖSYVÄ: Käytä supervoimia:Käyttösuunnitelmia kotimaiseen täytäntöönpanoon tai supervoimille:subagentti-ohjattu kehittäminen käyttäjän valinnan jälkeen. Seuraa askeleita tarkastuslaatikkoilla.

**Tavoite: ** Tarjoaa kuusi integroitua laajennusta ja kapasiteettipohjaista monitoimittajan tuki.

**Arkkitehtuuri:** Säilytä ydin / Windows / WPF erottaminen. tuoda eristettyä plugin ja mobiili-palveluprosesseja tiheän todistetun IPC; kaikki suoritus kulkee olemassa olevan ohjaus ja MacroRunner quue..

**Tekniikka Stack:** C ja .NET 10, WPF, Windows UI automaatio, Win32 Raw Input, Asiakirjoitettu SDK, ASP.NET Core HTTPS/WebSocket, Järjestelmä.Json.

**Spec: ** `docs/superpowers/specs/2026-10-12-six-extensions-design.md` (käyttäjän hyväksymä)

## Globaalit rajoitukset

- Nykyisen konfiguraation on ladattava muuttumattomasti; lisätoimenpiteitä sen sijaan, että niiden pysyviä enum-arvoja.
- Säilytä olemassa olevat profiilit, G1/G2 tehtävät, paikallinen toiminta ja tili-vapaa käyttö.
- Raw Input Tarjoaa vain tarkkailua. Yksinomainen laitteen poisto vaatii todellisen näppäimistön tausta.
- Importoitujen suoritettavien plug-inien ei saa suorittaa automaattisesti.
- Ehdottomasti poistaa ja rajoittaa mobiilipalveluille määriteltyjä käyttöliittymien käyttöä; ei valitusten tunnistamisen palautumista.
- Lyhytkestoinen/tekstitoimitus ei ole todiste siitä, että kohderyhmä hyväksyi komennon.
- Älä korvaa asennettuja sovelluksia, asenna ohjaimia, muuttaa todellisia näppäimistöjä tai avata verkkoyhteyttä satunnaisena testinä.
- Suojaa ei-suhteisia muutoksia. Älä sitoudu olemassa oleviin jaettuihin muokkauksiin osana tätä ominaisuutta. Stage vain itsenäisesti tarkistettavissa olevia ominaisuuksia, jos sitoumukset pyydetään.

## Tarkastelu keskittyy

1. Kaksinkertaiset samanlaiset näppäimistön mallit: epämääräisen uudelleenyhteyden on tarpeen valita uudelleen (Tasks 10–12).
2. Peruutus UIA: n / palveluntarjoajan / laajennuksen / verkko odottamisen aikana: peruuttamisen jälkeen ei toteuteta toimintaa (Tasks 1, 4, 6, 8).
3. Käyttäjä muokkaa, kun taiteilija tai paketti esittely on avoinna: tarkistuksen väärinkäytön on estettävä hiljainen ylikuormitus (Tasks 3, 7).
4. Kieli ja fyysinen järjestelmä eroavat: valinta- ja skannauskoodi ei saa riippua paikallisista näytön nimistä (Tasks 4, 10).
5. Verkon retry ja plugin uudelleen käynnistää: komentoja ei saa hiljaa suorittaa kahdesti (Tasks 6, 8).

## Yhteiset sopimukset ja riippuvuusjärjestys

Tavoitteet 1–3 toteuttaa täytäntöönpano ja asentaa. Tavoitteet 4–5 rakentaa UIA. Työt 6–7 toimittaa plugins. Tavoitteet 8–9 toimittaa mobiililaitetta. Tehtävät 10–12 toimittaa laatikon adaptereita ja suodattimien integrointia. Task 13 vahvistaa koko tuotteen.

Jokainen tehtävä seuraa epäonnistumista → minimaalista täytäntöönpanoa → passing-test -kierrosta. Tehtävä on valmis vain, kun se on saavutettavissa hakemuksen ja sen tarkastusten kautta. Scaffoldit, mock-adapterit ja dokumentaatio yksin eivät tyydytä toimitettavaa.

### Tehtävä 1: Tyypillinen tarkastus ja yhteensopiva kestävyys

**tiedostoja:** Luo `src/Aivana.Core/ActionVerification.cs`, `tests/Aivana.Tests/VerificationChecks.cs`; muutos `src/Aivana.Core/Models.cs`, `src/Aivana.Core/MacroRunner.cs`, `src/Aivana.Core/ConfigurationValidator.cs`, `tests/Aivana.Tests/Program.cs`.

**Yhteydenotot:** Tuotanto `VerificationState { Requested, Completed, Verified, Failed, TimedOut, Cancelled, Unverifiable }`, `ActionVerificationResult(VerificationState State, string Code, string Message)`, `IActionVerificationProvider.PreflightAsync(ActionStep, CancellationToken)` ja `VerifyAsync(ActionStep, CancellationToken)`, Molemmat paluu `Task<ActionVerificationResult>`. Lisää valinnainen sarjakuva `VerificationOptions` Säätiö ja Timeout (100–60000 ms), oletusarvo on poissa. julkaista kirjoitetut vaiheen tulokset, mukaan lukien käynnissä oleva ID, tile ID ja vaiheen indeksi ilman toimenpiteitä.

- [ ] Kirjoita tarkastukset: perintö JSON säilyttää olemassa olevat toiminnan numeroarvot; poissaolo vaihtoehtoja estää todentaminen; epätodennäköinen aikavälin vahvistaminen; ennakkoilmoitus estää suorittajan kutsumisen; epäonnistunut jälkikäteen ei koskaan tuota vahvistettua menestystä; tukematon suorittaja on Epävarmoitu; peruutus pysähtyy todentaminen ja seuraavat vaiheet.
- [ ] Käynnistä `dotnet run --project tests/Aivana.Tests -c Release`; vahvistaa, että uudet tarkastukset epäonnistuvat ennen toteutusta.
- [ ] Täytäntöönpanosopimukset, validointi, kloonien/snapshotien säilyttäminen ja MacroRunnerin integrointi. Noudata OnError -politiikkaa / jatkaa sitä; säilytä olemassa olevat toimitusviestinnät.
- [ ] Käynnistä ydin testi; vaatii kaikkia tarkastuksia.

### Tehtävä 2: Windows tarkastuspalveluntarjoajat ja tulos UI

**tiedostoja:** Luo `src/Aivana.Windows/WindowsActionVerification.cs`, `src/Aivana.Desktop/MainWindow.Verification.cs`, `tests/Aivana.Windows.Tests/VerificationChecks.cs`; muutos `src/Aivana.Windows/WinActionExecutor.cs`, `src/Aivana.Desktop/DesktopController.Feedback.cs`, `src/Aivana.Desktop/MainWindow.Editor.cs`, Testipisteitä.

**Yhteydet: ** WindowsActionVerification toteuttaa Task 1 -palveluntarjoaja käyttämällä olemassa olevia ääntä, näyttöä, prosessia/ikkunoita ja OBS -asiakkaita. Preflight testaus saatavuutta ja tunnisteita; todentaminen vertailee toivottua tilaa rajoitetun kyselyn jälkeen. Tuottaa ohjaajan tuloksia, joita kuluttaa WPF historianäkymä.

- [ ] Lisää epäonnistuneita testejä käytettävissä oleville nimenomaisille audio- ja näytönohjaimille, OBS aikarajoitteille, virheellisesti havaittuun tilaan, tuntemattomiin lyhenteisiin vaikutuksiin, herkkä maksukyvyttömyys ja peruutettu kysely, käyttämällä injektoitavien lukupalvelujen tarjoajia.
- [ ] Käytä todellista lukemisen tukemista absoluuttiseen määrään, tyhjiöön, absoluuttiselle kirkkaudelle, prosessin/ikkunan valmiuteen ja tuettuihin komentoihin OBS. Suhteelliset/toggle-toimet edellyttävät etuoikeutta; muuten raportoidaan Epävarmattomana.
- [ ] Lisää vaiheittain tarkastus ja turvallinen toimeenpanon historia osoittaen tilan, epäonnistuneen vaatimuksen ja palauttamisen tekstin.
- [ ] Käynnistä ydin/Windows tarkastukset ja WPF savua injektoitujen valtion toimittajien kanssa; ei todellista kirkkautta tai äänikirjoituksia.

### Tehtävä 3: Ohjattu työpiste luo

**tiedostoja:** Luo `src/Aivana.Core/WorkspaceSetup.cs`, `src/Aivana.Windows/WorkspaceDiscovery.cs`, `src/Aivana.Desktop/MainWindow.WorkspaceWizard.cs`, `tests/Aivana.Tests/WorkspaceSetupChecks.cs`; Muokkaa paletta ja dashboard-pääpisteitä.

**Yhteydet: ** `WorkspaceSetup.CreateProfile(WorkspaceSelection selection) -> Profile`; valinta sisältää mallin tunnuksen, valitun suoritettavien reittien, URL-osoitteiden, äänen/näytön tunnuksen, OBS -näytön ja valitun linkin. Discovery palauttaa nimettyjä ehdokkaita vakaalla tunnuksella ja saatavilla. Konfliktin havaitsemiseksi laaditaan konfiguraation tarkistus.

- [ ] Lisää epäonnistuneita testejä kaikille viidelle pyydetylle taiteelliselle perheelle, itsenäisesti syntyneet askeleet, puuttuvat pyydetyt tavoitteet, peruutetaan ilman mutaatiota ja säästetään muutoksilta.
- [ ] Etsi sovelluksia rekisteröityjen sovellusreitin ja Start Menu lyhennökset; laitteet olemassa olevien palveluntarjoajien kautta; kohtaukset vain lukemalla OBS kyselyä. Pidä manuaalinen valinta saatavilla löytämättömille sovelluksille.
- [ ] Toteutus Valitse työnkulku → Valitse tavoitteet → Valitsee ohjaimet → Tarkennetaan/validoidaan → Tallenna. Luo muokkaavia profiileja ja älä koskaan käynnistä asennuksen aikana.
- [ ] Lisää todellinen WPF tupakointitarkastukset loppuun / peruuttamiseen / konfliktiin; suorita kaikki testit.

### Tehtävä 4: UIA-valinta ja rajoitetut toimet

**tiedostoja:** Luo `src/Aivana.Core/UiAutomationAction.cs`, `src/Aivana.Windows/UiAutomationService.cs`, `tests/Aivana.Windows.Tests/UiAutomationChecks.cs`; Muokkaa malleja, konfiguraatiota ja WinActionExecutor.

**Yhteydet: ** Liite `ActionKind.UiAutomation`; `UiElementSelector` tallentaa prosessi, automaatio ID, ohjaus tyyppi ja tilattu esivanhemmat kriteerit. `UiAutomationService.ExecuteAsync(UiAutomationAction action, CancellationToken)` palauttaa `Task`; toiminnot Invoke, SetValue, Select, Toggle, Focus ja WaitState. Valikoiman on palautettava täsmälleen yksi hyväksyttävä elementti.

- [ ] Lisää epäonnistuneita testejä erilliseen testausikkunassa siirtämiseen/korvaukseen, kaksinkertaistamiseen, puuttuviin elementteihin, tukemattomiin malleihin, poistoon, salasanoihin, Unicode-merkintöihin ja aikarajoitteluun/peruuttamiseen.
- [ ] UIA: n täytäntöönpano omistettuun työntekijään, jolla on rajalliset pyynnöt ja prosessin kohdistus. Potentiaalisesti ripustetut palveluntarjoajat juoksevat uudelleenlaajennettavan työntekijän rajojen takana; pyynnön hylkääminen ei saa jättää kiusattuja sivuvaikutuksia.
- [ ] Poista salasanat; raportoi pääsy- / eheysrajoituksia sen sijaan, että ne ylitetään.
- [ ] Käynnistä Windows tarkastuksia käyttämällä eristettyjä kiinnityksiä; lisää vain enum-arvoja ja rerun perinnön pysyvyys tarkastuksia.

### Task 5: UIA piikki ja tarkistettu tallennus

**Tämä tiedosto: ** Luo `src/Aivana.Desktop/MainWindow.UiAutomation.cs`, `src/Aivana.Windows/UiAutomationRecorder.cs`; muokkaa toimenkatalogia, muokkaa ja tallentaa integrointia; luo UIA WPF savutarkastuksia.

**Yhteydet: ** Picker palauttaa `UiElementSelector`; rekisterin palauttaa ehdotetut UIA askeleet ja ei koskaan kirjoita suoraan tallennettuun profiiliin. Test-selektorin käsky palauttaa ottelun ilman kutsumista.

- [ ] Lisää epäonnistuneita tarkastuksia: piikeri sulkee pois MAKRIVO ja salasanan arvo; tallennus/tallennuslehdet ovat muuttumattomia; epäselvä ennakointi raportointi vastaa; hyväksyminen tallennettuja askeleita luo muokattavan suunnitelman.
- [ ] Implement elementti korostus/piikkuri, toiminnalliset parametrit, valinta etu ja tallennus tarkistusikkuna. Pidä olemassa oleva lyhytrekisterin käyttäytyminen.
- [ ] Lisää luetteloon ja palettiin pääsy; validoi muokkausvirheet ennen tallennusta.
- [ ] Käynnistä Windows ja WPF tupakointi tarkastuksia; tarkista pikkurin / toimittajan näytönkuvat.

### Tehtävä 6: Eristetty plugin-hostaja ja protokolla

**tiedostoja:** Luo `src/Aivana.Core/Plugins/PluginContracts.cs`, `src/Aivana.Windows/Plugins/PluginProcessClient.cs`, `src/Aivana.PluginHost/Aivana.PluginHost.csproj`, `src/Aivana.PluginHost/Program.cs`, `tests/Aivana.Tests/PluginProtocolChecks.cs`; Muutokset ja toimenpiteet.

**Yhteydenotot:** Version-1 manifesti määrittää paketin ID/versio, sisäänpääsypiste, parametrit järjestelmät, toimenpiteet, valtion tarjoajat ja valmiusilmoitukset. IPC viestit sisältävät protokollin versio, ainutlaatuinen pyyntö ID, menetelmä, JSON parametrit ja tulos. `PluginProcessClient.InvokeAsync(string actionId, JsonElement parameters, CancellationToken)` palauttaa tuloksen. lisäys `ActionKind.Plugin`.

- [ ] Lisää epäonnistuneita testejä yhteensopimattomalle protokollille, väärennettyyn / ylitykseen (>1 MiB) viestiin, kaksinkertaiseen pyyntöön, peruuttamiseen, onnettomuuteen, vastuuttomuuteen ja puuttuvaan toimintaan.
- [ ] Käyttö sama-käyttäjä nimeltään putki IPC kanssa 10 -sekundin käsin ja konfiguroitu suoritus aika kaappi 60 sekuntia. Lopeta vastuuttamaton työntekijä; älä automaattisesti toistaa toimintaa lennon jälkeen uudelleen.
- [ ] Sovelletaan järjestelmiä ja sallittuja ohjauspalveluja. Selitä täytettävissä oleva plugin luottamus; prosessin eristys ei ole OS: n hiekkakortti.
- [ ] Käytä ydin- ja integrointitestejä todellisten testien prosessien kanssa.

### Tehtävä 7: Plugin-paketit ja esimerkki integrointi

**tiedostoja:** Luo `src/Aivana.Core/Plugins/PluginPackage.cs`, `src/Aivana.Desktop/MainWindow.Plugins.cs`, `samples/Aivana.ExamplePlugin/`, paketin testaus; muuttaa palettia/asetuksia.

**Yhteydenotot:** Pakkauspalvelut `InspectAsync(path)`, `InstallAsync(reviewedPackage, revision)`, `ExportAsync(packageId, destination)`, `DisableAsync(packageId)` ja `UninstallAsync(packageId)`. Uudistettu paketti tallentaa hashit, riippuvuudet, versio ja ilmoitetut valmiudet.

- [ ] Lisää epäonnistuneita testejä zip traversal, absoluuttisia reittejä, symlink/reparse escape, duplicate entries, >100 MiB laajennettuja tietoja, hash väärinkäytös, laskeutuminen, jyrkkä tarkastelu ja puutteellinen tuonti.
- [ ] Implement Preview ja nimenomainen käyttöönotto, riippuvuus virheitä, vaiheittainen asennus ja päivitys; poistaa työntekijän ennen poistamista.
- [ ] Tarjoaa todellinen esimerkki plugin palauttaa konfiguroitua laskentatoimintaa ja live-tila IPC: n kautta, mukaan lukien peruutus; pakkaus / vienti / asennus / poistaminen UI: n kautta.
- [ ] Käynnistä paketti / host testi ja WPF lopulliset tarkastukset. Tarkista, että ei ole maahantuotuja suorituskykyjä ennen hyväksymistä.

### Tehtävä 8: HTTPS mobiilipalvelu ja parannus

**tiedostoja:** Luo `src/Aivana.MobileHost/` ASP.NET Core -ohjelma, `src/Aivana.Core/MobilePanelContracts.cs`, `src/Aivana.Windows/MobilePanelClient.cs`, Integration testit; muokkausratkaisu ja ohjaus.

**Yhteydet: ** Matkapuhelinpalvelut hallitsevat saman käyttäjän IPC: n kautta. Reitit: parannus, sallitut profiilit/toiminnot, todistettu toimenpide, peruuttaminen ja WebSocket tilanne. Invokaatio sisältää ainutlaatuisen komento-tunnuksen; ohjaajan omistuksessa on rajoitettu deduplication. Luottamus on peruutettavissa ja tallennetaan Windows suojalla.

- [ ] Lisää epäonnistuneita testejä, jotka koskevat ennennäkemätöntä luetteloa/toimintaa, päättyneitä koodeja, uudelleenkäytetyn koodin, peruuttamattoman alkuperän, kaksinkertaisen komennon, liiallisen kehityksen ja tulvan rajat.
- [ ] HTTPS vain kuuntelija, käyttäjän valitut käyttöliittymät, paikallinen sertifikaatti / luottamusohjeet, yhden kerran 6-digit pari koodit päättyvät jälkeen 120 sekuntia, enintään viisi yritystä koodin ja token peruuttaminen. Tallenna satunnaisia tokeneja; älä koskaan kirjaudu tunnistuksiin.
- [ ] Soveltaminen mahdollistaa lueteltuja vakaita toiminnallisia tunnisteita ja olemassa olevia reitityksiä; kieltäytyä tahallisesta shellista, reiteistä ja pakkauksen asennuksesta. Määritä nimenomaiset asiakasrajat (10 komennot/sek, 30 putoaminen) ja 64 KiB-viestiraja.
- [ ] Tarkista loopback integrointi testit lisäämättä palomuurin sääntöjä tai sallimalla LAN-sovellus; testaus sulkeutuminen ja token kestävyyttä.

### Task 9: Mobiili-paneeli ja työpöytäpalvelut

**tiedostoja:** Luo `src/Aivana.MobileHost/wwwroot/` Reaktiivinen paneeli ja `src/Aivana.Desktop/MainWindow.MobilePanel.cs`; lisätä selaimen/mobiilien tarkastuskriptejä ja WPF Tupakointi kattaa.

**Yhteydet: ** Kulutusvelvollisuus 8 vain reittejä. Näytä kosketusverkko, profiilipikeri, elävät arvot / eteneminen, peruutus, yhteyden tilanne ja parannus. Työpöytä hallitsee palvelun käynnistämistä / pysäyttämistä, osoitetta / sertifiointia koskevia ohjeita, sallittuja profiileja ja peruuttamista.

- [ ] Lisää epäonnistuneita integrointitarkastuksia auth-virtaukseen, pysäyttämiseen/todistamiseen liittyviä tunnisteita, uudelleenyhteyttä ilman toistoa, peruutettuja asiakkaita ja palvelun pysäyttelyä.
- [ ] Sovelletaan käytettävissä olevaa reaktiivista käyttöliittymää käyttämällä paikallisia varoja, ei kolmannen osapuolen kirjoja tai internetin riippuvuuksia. Tekstiä ei koskaan löydy URL-osoitteista.
- [ ] Testaa selaimen asetukset 360/768/1280 CSS px; tarkistaa todelliset näytöt. Tarkista todellisella puhelimella/tabletilla vain, kun käyttäjä nimenomaisesti sallii LAN-palvelun.
- [ ] Dokumentti ja raportti mobiililaitteiston tarkistaminen erikseen selaimen emulaatiosta.

### Tehtävä 10: Laitteen kapasiteetit ja tavallinen avain tarkkailu

**tiedostoja:** Luo `src/Aivana.Core/KeyboardCapabilities.cs`, `src/Aivana.Windows/KeyboardAdapterRegistry.cs`, `src/Aivana.Desktop/MainWindow.KeyboardAdapters.cs`; muuttaa RawKeyboardBindingService, KeyboardBinding, vahvistin ja laitteen diagnoosi; lisää testejä.

**Yhteydet: ** `IKeyboardAdapter` altistaa kapasiteettiä, vakaa laitteen identiteetti ja fyysiset avainreitit (skannikoodi, laajennetut liput, lehdistö / julkaisu). Muodit erottavat havaintoa, kartattuja vakiintumisia, alkuperäistä SDK:ta ja kuljettajaa. Nykyiset virtuaalinen avain erikoisliitännät säilyttävät edustuksensa ja muuttoliikkeen käyttäytymisen.

- [ ] Lisää epäonnistuneita testejä vanhoille sidoksille, Enter-erityksiä, kielen / asennuksen riippumattomuutta, toistuvia laskuja, sulkemattomuutta pitämällä, kaksinkertaisen mallin epätarkkuutta ja injektoituneen sisäänpääsyn hylkäämistä.
- [ ] Laajenna havaintoa tavallisille fyysisille avaimille estämättä niitä. Lisää nimenomainen pidätys ja yhdistää identiteetin käsittely.
- [ ] Näytä adapterin todelliset ominaisuudet ja laitteiston testaamaton tilanne laitteen käyttöliittymässä; älä koskaan määritä Fn/dial-tukea brändin nimistä.
- [ ] Run core/Windows/WPF tarkistaa simuloituja raportteja ja lukemattoman laitteen luetteloa.

### Task 11: Valmistaja adapteri

**File:** Luo erilliset tiedostot `src/Aivana.Windows/KeyboardAdapters/` LogitechG, RazerChroma, SteelSeriesGameSense, WootingAnalog ja yleinen QmkVia ohjaus; muokata rekisteri/UI ja lisätä adapterin protokollatarkastuksia.

**Yhteydet: ** Kulutustehtävä 10 sovitussopimukset. Kotimaiset kirjastot ladataan vain vahvistetuista sovelluksista/myyntipaikoista; SDK: n puuttuminen on käytettävissä oleva mahdollisuus. Valo- ja sisäänkäyntiominaisuudet ovat erillisiä.

- [ ] Tarkista nykyiset viralliset SDK-päälliköt, uudelleenjakelun ehdot, ohjelmiston riippuvuudet ja tuetut mallit ennen kunkin adapterin käyttöönottoa; tallennuslähteet ja tarkka versio.
- [ ] Lisää epäonnistuneita testejä SDK: n puuttuvaan/tyyppiseen arkkitehtuuriin, tapahtumien lehdistämiseen/toistamiseen, kytkemään yhteyttä, väärennettyyn vastaukseen ja tukemattomaan laitteeseen. Käytä injektiivia alkuperäisiä abstraktioita deterministisiin tarkastuksiin.
- [ ] täytäntöönpano Logitech G-näppäintä ja tuettu valaistus erikseen; Razer Chroma valaistus plus Synapse Standard-tärkeä kartta; SteelSeries GameSense Valtion / Valo; Wooting Analoginen kysely, jossa on konfiguroitava raja/hysteresia; QMK/VIA Standard-key asennusohje. Älä vaadi omistettuja tuloksia valaistus APIs.
- [ ] Valitse yhteensopivat todelliset laitteet, kun ne ovat käytettävissä. Muiden toteutetun / laitteiston testattu, malli / firmware todisteita; puuttuva käytettävissä oleva SDK on paljastettu ulkoinen portti.

### Task 12: Yksinomainen näppäimistö suodatin tausta

**tiedostoja:** Luo `src/Aivana.Windows/KeyboardFilter/`, Säätiön lähde `tools/keyboard-filter/`, backend integrointi testaus ja käyttöönotto dokumentaatio. erillinen allekirjoitettu kuljettaja hanke vaaditaan, jos ei sopiva lisensoitu backend läpäise arviointia.

**Yhteydet: ** Backend raportoi asennettu/yhteensopiva/tunnustettu tilanne; esittää laitteen erityisen otteen käynnistämisen / pysäyttämisen, avaimen ja hätätilanteen. Erillinen tarkkailija palauttaa ohjaimen menetyksen läpi. Päänäppäimistö ei koskaan ole yksinomainen.

- [ ] Arvioi kandidaatin ajokortti, allekirjoitettu binary alkuperä ja nykyinen Windows 11/Secure Boot yhteensopivuus. Päätös on Go/No. Ei kuljettajaa tavanomaisten testien aikana.
- [ ] Kirjoita sopimustestejä suodatin kohteen valintaan, epäonnistumiseen, sydänkohtauksen päättymiseen, IPC: n menettämiseen, uudelleenyhteyden epämääräisyyksiin, tauon/peruuttamiseen, avaimen vapauttamiseen ja poistamiseen.
- [ ] Implement backend, nimenomainen valinta/aktivointi UI, hätätilanteessa ja asentaja/uninstaller. Älä korvaa alhaisen tason heikkoa/Raw Input aikajärjestelmää.
- [ ] Valitse palautettavissa oleva testilaite kahdella näppäimistöllä: valittu avain poistetaan vain valitulla laitteella; tärkein kirjoitus ei vaikuta; vahingoittuminen / keskeytyminen / asennus palauttaa.
- [ ] Portti tuotannon loppuun tosiasiallisesti allekirjoitettu / lisensoitu tausta ja fyysiset testit. Jos ulkoinen allekirjoitus/lisenssi ei ole saatavilla, tehtävä on täyttymätön ja riippuvuus todistetaan tarkasti.

### Tehtävä 13: Integroitu tarkastus, käännökset ja vapautuspaketti

**tiedostoja: ** Lisää `docs/testing/2026-10-12-six-extensions.md`; päivittää asiaankuuluvia DE/EN/FR/IT-asiakirjoja, sijaintikataloja ja savun tarkastuspisteitä. Muita dokumentteja säilytetään.

- [ ] Integroida kaikki sisäänpääsypisteet ja kuvakuvat; täydellinen käännökset käyttämällä olemassa olevaa 27-kielen luetteloa ja paljastaa tarkastelemattomia käännöksiä.
- [ ] juokseminen `dotnet build Aivana.slnx -c Release`, `dotnet run --project tests/Aivana.Tests -c Release`, `dotnet run --project tests/Aivana.Windows.Tests -c Release`, ja `dotnet run --project src/Aivana.Desktop -c Release -- --smoke-test`.
- [ ] Tarkista työpöydän/mobiilikuvaukset ja tarkista 100/150/200 prosentti Windows mittakaavassa, konfigurointi ympäri matkoja, plugin elinkaari, verkko peruuttaminen ja todellinen näppäimistön uudelleenyhteys.
- [ ] Julkaise uusi artifakti luetteloon `dotnet publish src/Aivana.Desktop -c Release -r win-x64 --self-contained true -o artifacts/six-extensions/win-x64`; sisältää kumppanipalvelut / isäntä binareja ja lisenssitilanteita. Älä liioittele asennettua sovellusta.
- [ ] Raportoitu testi, laitteiston testaamaton adapteri ja ulkoinen päästöt. Kaikkien kuuden toimittajan on täytettävä hyväksymiskriteerit ennen koko pyynnön täyttämistä.

## Itsearviointi suunnitelma

Kaikki kuusi erikohtaa kartta tehtäviä 1–12; tehtävä 13 kattaa sijainti, integroitu todentaminen ja vapauttaminen. Valmistuneisuus raportointi, toteutuksen sarjakuvaus, perintökohdat, salaisuudet, paketin luottamus ja fyysinen laite epäselvyys ovat nimenomaisia omistajia ja testejä. Ulkoinen ohjain / SDK / laitteiston portit pysyvät epätäydellisinä, kunnes se on todistettu, eikä laskettu ohjelmiston menestykseksi.

## Käyttäjän tarkastelun valinta

Suositeltu: alkuperäinen toimeenpano tässä chatissa, tehtävästä tehtävästä, koska useimmat moduulit jakavat sopimuksia ja nykyinen työpaikka sisältää merkittäviä olemassa olevia muokkauksia. Delegoitu täytäntöönpano on käytettävissä, jos käyttäjä valitsee sen. Tarkista tämä suunnitelma ja valitse täytäntöönpanomenetelmä ennen toteuttamista, kuten kirjoitus-suunnitelmien työnkulku edellyttää.
