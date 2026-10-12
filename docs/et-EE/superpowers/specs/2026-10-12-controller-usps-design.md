<!-- generated-doc locale=et-EE source=docs/superpowers/specs/2026-10-12-controller-usps-design.md sha256=4c3b2dbedb83af6bd48917515ae6559012afb6318ce69d1e32fb2765e0793327 -->

> Masintõlke abil koostatud dokumentatsioon. Struktuuri, koodinäiteid ja linke kontrollitakse; see ei tähenda emakeelse toimetaja kontrolli ega õiguslikku heakskiitu. Ajaloolised aruanded säilitavad algsed kuupäevad ja ulatuse. [Originaalne allikas](../../../superpowers/specs/2026-10-12-controller-usps-design.md)

# MAKRIVO Kontrolli laiendamine

Kasutaja palus kõiki üheksa kavandatud parandusi ja tunnustatavat tray ikooni. Praegused konfiguratsioonid, eriti G1/G2 heledus, peavad jääma ühilduvaks. Olemasolevad brändi- ja dokumendieditused kuuluvad jagatud tööruumi ja neid tuleb säilitada.

## Toetused ja piirid

1. Valige kõik toetatud monitorid või stabiilne konkreetne monitor ID. Puuduvad selged eesmärgid ei ole nähtavad. Dashboard näitab praegust heledust, väljumismaht, mikrofonide seisundit ja OBS seisundit piiratud sünkrooniliste lugude kaudu.
2. Spetsiaalsed võtmed toetavad kohest survet, tapmist, kinnipidamist ja topelt tapmist, korduva survega. Hoitud kihti valik ajutiselt muuta aktiivset kaardistamist ja vabastada taastab seda. Olemasolevad kohesed sidemed säilitavad oma käitumist. Pausi, profiili muutused, tühistamine ja tühistamise selged käed.
3. Töövoo disainer kohandab tingimusi, edasijõudmisi ja lõpetada/pöörata ebaõnnestumise poliitikat tehinguliselt. Tingimused hõlmavad faile, käivitamisprotsesse ja akna pealkirju. Ei ole piiratud hüpata tsüklit.
4. Selge salvestamine võtab lühikeseid koore ja ootusi, ignoreerib süstetud sissemakseid ja peatub Escape'is. Registreeritud meetmed vaadatakse läbi enne vastuvõtmist; tavaline tekst ei ole registreeritud.
5. Seadme kohta kasutatakse teisi tootjaid Windows Raw Input Omandikõned vajavad toetatud pakkuja API-d või pakkuja standardse võtme kaardistamist; toetamata funktsionaalsus on ausalt näidatud. Diagnoos selgitab profiili, pausi, siduvaid konflikte ja tegeliku adapteri seisundit.
6. Profili vahetamine kasutab kasutaja poolt valitud jagatud faili (sealhulgas olemasoleva pilvkliendi sünkroniseeritud faile). Kontrolli kontrollimine takistab vaikseid ülekirjutusi. Pull ennustab muudatusi ja impordib häiritud toiminguid, kustutatud hardware mapping. Tegevuse sisu sisaldamine on selge. Ükski pilv ei ole salvestatud MAKRIVO.
7. UI stringid kuuluvad olemasolevatele 27 keele kataloogidele; DE/EN/FR/IT jäävad minimaalselt läbi vaadatud dokumendi keelteks. Tõlke puudused peavad olema avalikustatud, mitte väidetavalt täielikud.
8. Tunnustatava teal M klaviatuuri tähis asendab geneerilist Windows trai ikooni. Pause moodul on amber pause. Executable/window bränd kasutab sama märki. Tooltip hõlmab staatust ja profiili.

## Kontrollimine

Põhikatsed hõlmavad tagasipöördumist, snapshots, tühistamist, käitumise ajakava, töövoo rühmad / vead, salvestamise filtreerimine ja sünkroonimise konfliktid. Windows kontrollid hõlmavat loetavalt näidisnimekirja, SDK vabastamise sündmusi ja Raw Input parsingu. WPF suitsu kontrollid hävivad uusi sisendpunkte ja ressursse, muutmata reaalset monitori heledust, kirjutage teise rakenduse või ületada kasutajaprofiile. Kasutamine säilitab varukoopi ja G1/G2 konfigureerimist.
