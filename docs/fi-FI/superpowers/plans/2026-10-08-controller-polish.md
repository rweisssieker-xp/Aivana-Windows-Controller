<!-- generated-doc locale=fi-FI source=docs/superpowers/plans/2026-10-08-controller-polish.md sha256=1a076a67eea31048f6305f30a121c474d592451d1ddcd9b46bc339fd8ec068f2 -->

> Koneavusteinen käännös. Rakenne, koodiesimerkit ja linkit tarkistetaan; tämä ei tarkoita äidinkielisen tarkistajan arviota tai oikeudellista hyväksyntää. Historialliset raportit säilyttävät alkuperäiset päivämääränsä ja rajauksensa. [Alkuperäinen lähde](../../../superpowers/plans/2026-10-08-controller-polish.md)

# Käytettävyyden ja valmiuksien parantussuunnitelma

[English (US)](2026-10-08-controller-polish.md) | [Deutsch](2026-10-08-controller-polish.md) | [Français](2026-10-08-controller-polish.md) | [Italiano](2026-10-08-controller-polish.md)

Käyttäjän pyyntö: parannetaan ulkonäköä, käyttökelpoisuutta ja toimivuutta; ennalta hyväksytty edellyttää mahdollisimman hyödyllisiä alihankkijoita tarvittavista pienemmistä malleista ja itsenäisestä valmistuksesta.

## Laajuus
Säilytä olemassa olevat profiilit ja toimiva Corsair SDK: n integrointi. Parannetaan olemassa olevaa sovellusta WPF sen sijaan, että luodaan erillinen prototyyppi.

- [x] Shell/dashboard työntekijä: reaktiivinen asettelu, aktiivinen navigointi, luettavissa olevat ohjaimet, kompakti toiminta/tila, vetäminen ja pudottaminen tiili, hyödyllisiä tyhjiöjä ja malleja. MainWindow.xaml, MainWindov.xam.cs ja MainWind.Dashboard.cs.
- [x] Editor työskentelee: MainWindow.Editor.cs ja valinnainen muokkaus - vain osittaiset tiedostot. Selkeät vaiheittaiset muokkaukset, Save/Discard, validointi inline, omistautunut makro-luettelo drag-and-drop, kaksinkertaiset vaiheet, luotettava hotkey capture ja tiedosto/URL kentät.
- [x] Windows työntekijä: alkuperäinen validointi, vahvat laitteen nimet ja SDK-ominaisuudet, rajoitetut muuttujat ja turvallinen injektio; omat Windows ja Windows testit.
- [x] Vanhempi: ydin suorituskyky, ohjausliittymät, konfiguraation säilyttäminen, integrointi testaus, todelliset renderisoidut näytöt, julkaisu/pakkaus ja käynnissä olevan sovelluksen korvaaminen.

## Yhteydenottosopimus
Ohjain tarjoaa olemassa olevia APIs plus sarja LastStatus, bool HasRunningActions, bool IsRunning(Guid), sarja GetTileStatus(Guide), sarja ForegroundProcess, bool CorsairAvailable, IReadOnlyList<CorsairKeyboardInfo> CorsairKejäret, Task TestCorsairColorAsync(string), void OpenIcue(). Toimittaja esittää Bool HasUnsavedChanges, void DiscardDraft(), Bool CommitDraft(); olemassa olevat kenttätunnukset/menetelmät pysyvät saatavilla eri luokissa.

## Hyväksyminen
Ei todellista käyttäjäkokonaisuutta; voimassa olevia vaiheittaisia muutoksia tallennetaan kerran, epätäydelliset luonnokset pysyvät muokkaammin, tallennettujen tietojen palauttaminen poistetaan. Kotimainen hotkey alias validoitu ilman injektiota testitulokset epäyhteisiä ikkunoita. Tile ja macro reorder testattu; kaikki näkymät näytetään työpöydän koot; käynnissä / epäonnistunut / täydellinen tilanne näkyvä; todellinen SDK-laitteen malli näkyy. Rakenna / testaa ja julkaista tupakointi pass, ja päivitetty sovellus käynnistetään.
