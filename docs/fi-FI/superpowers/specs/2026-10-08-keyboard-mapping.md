<!-- generated-doc locale=fi-FI source=docs/superpowers/specs/2026-10-08-keyboard-mapping.md sha256=fb766a2a2b4698ae3e1c3f63b50bb4f5c605da3dd56c61c05fcc0832b5cf8aeb -->

> Koneavusteinen käännös. Rakenne, koodiesimerkit ja linkit tarkistetaan; tämä ei tarkoita äidinkielisen tarkistajan arviota tai oikeudellista hyväksyntää. Historialliset raportit säilyttävät alkuperäiset päivämääränsä ja rajauksensa. [Alkuperäinen lähde](../../../superpowers/specs/2026-10-08-keyboard-mapping.md)

# Fyysinen näkymä karttaukseen

[English (US)](2026-10-08-keyboard-mapping.md) | [Deutsch](2026-10-08-keyboard-mapping.md) | [Français](2026-10-08-keyboard-mapping.md) | [Italiano](2026-10-08-keyboard-mapping.md)

## Tavoite

Painikkeen näkymän tarkoituksena on osoittaa toimenpiteiden määrittämisessä todelliset painikkeet. Aivan laaja rivi ei riitä: Enter, Shift, Space, Navigointi, numero Blocks ja valmistajan lisäohjauselementit eroavat toisistaan.

## täytäntöönpano

- Luettelo, jossa on erillisiä fyysisiä tunnisteita, geometriaa painikkeissa, painettuja legendoja ja Windows -käyttökoodeja.
- ISO-DE ja ANSI-US kukin täysmuodossa ja TKL; lisäksi Corsair K100 RGB, joka tunnustettiin liitetyn mallin kautta iCUE.
- ISO Enter on L-muotoinen, numero-bloki, jossa on korkeat Plus/Enter -painikkeet ja laaja nolla; navigointi ja pilat omilla paikoillaan.
- Valitse nimenomaisesti mallin/laatikko. USB VID/PID ja mallin nimi eivät osoita kielivaihtoehtoja. Yleiset profiilit ja valmistajan asiakirjojen mukaisesti mallitut profiilit on tunnistettavissa eri tavalla; ei millimetrin tai firmware-yhteensopivuuden takuu.
- Skannoi koodi aktiiviseen Windows -tilaukseen, jotta fyysinen painiketta ei satunnaisesti kiinnitä toista painiketta, kuten Y/Z, kun se vaihdetaan Yhdysvaltojen ja DE: n välillä.
- Asettaminen avaa suunnitelman olemassa olevassa toimintoeditorissa. Säilytys, konfliktin tarkastaminen ja hylkääminen pysyvät olemassa olevia mekanismeja. Click ei käynnistä toimintaa.
- Omistettuja G-painikkeita, profiili-/loki-painikkeet ja iCUE -tila esitetään, mutta ilman toteutettuja sisäänkäyntikäyttöä ei ole normaalia Windows hotkeys. Konfiguratiiviset valmistajan lyhennökset pysyvät mahdollisena.
- Asennukset pysyvät maailmanlaajuisina Windows hotkeys; laitteen näkymä ei tarkoita laitteen erityistä tukahduttamista tai yksinomaista asennusta yksittäiselle fyysiselle painikkeelle.

## Todisteita

[K100-Handbuch ohjauselementtejä](https://assets.corsair.com/image/upload/corsairmedia/sys_master/productcontent/AP_K100_RGB_Mechanical_QSG_AD.pdf), [Corsair ANSI / ISO-ilmoitus](https://www.corsair.com/au/en/explorer/gamer/keyboards/ansi-vs-iso-whats-the-best-keyboard-layout/).

## Tutkimus

Geometriset, muunnelmalliset ja sisäänkirjautumiskokeet; rakenteet ja olemassa olevat ydin-/Windows testit; WPF-tunnisteet esittämiseen ja asettamiseen ilman todellisen käyttäjän konfiguraation suorittamista tai muuttamista. Valmistajan tutkimus dokumentoi muita malli-/SKU-vaatimuksia riippumatta nykyisestä asetuksesta.
