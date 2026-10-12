<!-- generated-doc locale=fi-FI source=docs/VERIFICATION.md sha256=f19afb9e7b5ffac90c83167416ab3af0964a4481dd16b7357f8585480c6aae65 -->

> Koneavusteinen käännös. Rakenne, koodiesimerkit ja linkit tarkistetaan; tämä ei tarkoita äidinkielisen tarkistajan arviota tai oikeudellista hyväksyntää. Historialliset raportit säilyttävät alkuperäiset päivämääränsä ja rajauksensa. [Alkuperäinen lähde](../VERIFICATION.md)

# Tutkimus – 8. Lokakuussa 2026

[English (US)](../en-US/VERIFICATION.md) | [Deutsch](../de-DE/VERIFICATION.md) | [Français](../fr-FR/VERIFICATION.md) | [Italiano](../it-IT/VERIFICATION.md)

Sovellus on rakennettu Windows x64 vakaalla .NET-SDK:llä 10.0.202 ja julkaistu itsenäisenä Windows-x64-Ausgabe.

## Automaattisesti tarkastettu

- Koko rakenne: ei varoituksia eikä virheitä.
- 19 Ydin käyttäytymistestit: vakiomuoto, validointi, nollat / kaksinkertaiset tiedot, profiilin prioriteetti, tuonti / vienti ja varmuuskopiointi, makrojärjestys ja toistaminen, katkaisu, kaksinkertainen erottaminen, sarjakuvaus, muuttumaton suoritusnäppä ja askelvirhe.
- Windows -testi: Hotkey-normalisointi ja Alias-nimet, alkuperäinen rakenne koot, todellinen painikkeiden luettelo, turvallinen rekisteröinti ja uudelleenlaskeminen, synteettinen WM_HOTKEY kaakkoille ja romahdukselle, poistettu kaakkojen rekisteröinti sisäänsimuloinnin aikana.
- Itsenäinen EXE käynnistyi `--smoke-test`: kaikki viisi WPF-näkökulmaa voidaan tallentaa/kopioida/tyynyä, muotoja säilytetään uudelleen, konfiguraatio tallennetaan ja ladataan uudelleen. Tämä testi käyttää eristettyä tilapäistä luetteloa ja ei tee teksti- tai mediatoimia.
- Pöytä, muokkaus ja kompakti kuvaus (900 × 720) on tarkastettu visuaalisesti todellisena WPF. Suunnittelun tallentaminen, hylkääminen, makrojärjestys, saksalainen hotkey-liisaus, yksittäiset makrot ja askel edistyminen on tarkastettu.

## Hardware riippuvaiset rajat

Viimeinen paikallinen kulku tunnisti neljä Windows painikkeet. Tämä luku kuvastaa HID-/Windows-liitännät, ei välttämättä neljä fyysistä painiketta.

Virallinen iCUE SDK on asennettu ja sijaitsee EXE: n vieressä. Live-testi yhdistyy iCUE ja tunnistaa Corsair -näppäimen mallitietojen ja LED-tietojen kanssa. Valaistusmuutoksia ei ole automaattisesti tapahtunut; mallien ja muiden valmistajien ulkonäköpainikkeita ei ole testattu. Generic Hotkeys sovelletaan painopisteiden välille.

Tekstitiedostoja ei ole injektoitu muihin avoimiin käyttäjäohjelmiin. Kotimaiset tietorakenteet ja virhe/vapauslogika on tutkittu; tietyn kohdeohjelman käyttäytyminen riippuu muun muassa sen hyväksymisasteesta.

## toistaa

```powershell
dotnet build Aivana.slnx -c Release
dotnet run --project tests/Aivana.Tests -c Release
dotnet run --project tests/Aivana.Windows.Tests -c Release
dotnet publish src/Aivana.Desktop -c Release -r win-x64 --self-contained true -o artifacts/win-x64
& ./artifacts/win-x64/MAKRIVO.exe --smoke-test
```

GUI-testi kirjoittaa tuloksen ja näyttökuvan `smoke` tarkastetun EXE: n vieressä. Se on tarkoitettu diagnoosimuodoksi ja ei käynnisty pysyvää taustaa valmistumisen jälkeen.


## Laajennettu hallinta

Lisäksi tarkastettu: profiilin olosuhteet prosessin ja ikkunan otsikoita, muuttujat ilman rekurssi laajentumista, makrotutkimus ilman suorituskykyä, uusi toimenpide validointi ja vanhojen JSON-tiedostojen yhteensopivuus. Alkuperäiset testit kattavat komento-pallot, joissa on rekisteröinti, konfliktit, WM_HOTKEY -levy ja vapautus sekä puhdas monitorin jakautuminen ja tarvetta hallittu klippikäsittely. WPF -testi avaa ja palauttaa todellisen paletin, tutkii palautetta/jälleenmyyjä ja suosikkeja, tutkii puhtaita suunnitelmia palautuksen jälkeen ja päättää prosessin onnistuneesti.

Windows -tyyppisiä lukkoja, välimuotoja ja ikkunoiden liikkeitä ei sovellettu käyttäjäsovelluksiin testauksen aikana. Windows-toiminnot lähettävät Windows kyselyjä synkronoidusti; konkreettisella kohdeohjelmalla voi olla omat vähimmäiskoot tai sallittujen rajojen. Profileja ja suoritustilastoja sovelletaan käynnissä olevaan istuntoon.


Laajennustarkastukset 12.10.2026: 30 Core-testiä on tehty; Windows-Adapterit, mukaan lukien päästöt sisäänkirjautumisen aikana, kirjoitettu monitorin tunnistus ja OBS-Status-Mock pysyi. WPF tarkastelee lopputuloksen vastaanottoa/korjausta, rekisterin käynnistämistä/pysäytymistä, profiilivaihdon ennakointia, käskyjä ja symboliarvoja.: 27 Katalogi on 967 Avaimet; dokumentaatio: 84 Sivut neljällä kielellä. muiden valmistajien omistajat Corsair ei ole testattu laitteistoa; yleinen tuki sisältää laitteeseen liittyviä vakiotilaisuuksia F13–F24 ja median painikkeet. pilvipalvelu suoritetaan valitun synkronoidun tiedoston kautta; tuonti tarkistetaan ja pysyy poissa.
