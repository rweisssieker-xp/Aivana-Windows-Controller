<!-- generated-doc locale=fi-FI source=docs/superpowers/plans/2026-10-12-controller-usps.md sha256=b7af0d36209e04b458ce64a5db78ab7e552a84c5e9b450ea0bd97179a4eb7f68 -->

> Koneavusteinen käännös. Rakenne, koodiesimerkit ja linkit tarkistetaan; tämä ei tarkoita äidinkielisen tarkistajan arviota tai oikeudellista hyväksyntää. Historialliset raportit säilyttävät alkuperäiset päivämääränsä ja rajauksensa. [Alkuperäinen lähde](../../../superpowers/plans/2026-10-12-controller-usps.md)

# Valvonnan laajennusohjelma

**Tavoite:** Sovelletaan pyydettyä monitoria, työvirtaa, tallennusta, laitteen diagnoosia, kannettavia profiilivaihtoja ja parannuksia.

**Arkkitehtuuri:** Laajenna olemassa olevia Core/Windows/Desktop-laajoja. Puhdasta tilan koneita ja kannettavia konfiguraatiota live Core, alkuperäiset integroinnit Windows ja modaalinen muotoilun muokkaajat Desktop. Säilytä olemassa olevat likaiset työpaikat ja käytössä oleva käyttäjäkonfiguratio.

**Spec:** ../specs/2026-10-12-kontrollit-usps-design.md

- [x] Valvontapalvelut, kohderyhmän jatkuvuus ja synkronaalinen elävä tilanne; testata käytettävissä olevia tunnisteita ja peruuttaa.
- [x] Gesture tilan kone ja pidätetyt kerrokset; testit, toistaminen, uudelleen asettaminen ja olemassa olevat välittömät sidokset.
- [x] Työnkulun ehto, etu- ja virhepolitiikka; testausnäytöt ja peruutus.
- [x] Yksityinen lyhytkestoinen tallennus esityksen tarkistuksella; testaus suodattimen ja hook-järjestelyn kanssa.
- [x] Jaettu lomakkeen profiilin vaihto tarkistusten ja ennusteiden kanssa; testit ja kannettava karttaukset.
- [x] Yleiset laitteen mukaan erityiset avaimet ja diagnoosi; testin parsintaminen, duploidut karttaukset ja tauon käyttäytyminen.
- [x] Tray/window/executable ikonin resursseja ja keskeytettyä ulkonäköä; tarkistaa resursseja julkaistuissa executable.
- [x] Integroida kaikki paletti/editori tulospisteet ja paikalliset tekstit; käynnistää Core, Windows ja WPF tarkastukset.
- [x] Julkaise erilliseen reittikirjaan, säilytä käyttäjän asetukset ja käynnistä päivitetyn sovelluksen uudelleen.
