<!-- generated-doc locale=sl-SI source=docs/testing/2026-10-09-usp-release.md sha256=96a5b23b67032ef07f2f5dd93e9585e7bab0a522e5096ffc9f4d020823c4264e -->

> Strojno podprt prevod. Struktura, primeri kode in povezave se preverjajo; to ne pomeni pregleda maternega govorca ali pravne odobritve. Zgodovinska poročila ohranijo izvirne datume in obseg. [Izvirni vir](../../testing/2026-10-09-usp-release.md)

# Ključna pomoč, prevleka in povratne informacije

[English (US)](2026-10-09-usp-release.md) | [Deutsch](2026-10-09-usp-release.md) | [Français](2026-10-09-usp-release.md) | [Italiano](2026-10-09-usp-release.md)

Izvedli so tri prednostne izboljšave v obstoječem aplikaciji Windows:

- Posebni ključni čarovnik: dejansko iCUE podprta naprava / ključna zapisa, začasno G-ključno zajemanje, preverjanje uspeha konfiguracije, validacija konfliktov in nepovratni akcijski načrt. Zaprtje, zavrnitev klicev in normalno obvladovanje pokriva napake in zaprtje dialoga.
- Nadgradnja tipkovnice: fizikalna referenčna geometrija, aktivni profilni vezi, posodobitev profila/redikacije in navadno Windows ključno poudarjanje. Držite Ctrl+Alt+F1 ali se premaknete skozi stran palete/na tipkovnici. Neaktivno, prek klikov in odsotnost iz delovnega vrstica.
- Povračilo izvedbe: tipirana Čakanje / Running / korak / dokončano / odpovedano fazo, korak / ponovitev napredek in neaktivni toast. Feedback izključuje vsebino dejanj in izjeme sporočil. Zadnji korak za preklic ni mogoče poročati o dokončanju. Popolnitev opisuje izvršbo, ne neodvisno državno poizvedbo OBS.

preverjanje :

- 24 Osnovni pregledi so bili opravljeni, vključno s fazo naročila, rezultati neuspeha / odpovedi, končni korak odpovedi, izjema slušalke in izključitev zasebnih vrednosti.
- Odstranitev rešitev je potekala brez opozoril / napake.
- Windows preverjanja je minila; povezana K100 poročila G1–G6 in domača konfiguracija/izdaja je bila uspešna. Končne točke so bile pregledane brez spremembe njihovega stanja.
- WPF dim je minil z vsemi referenčnimi postavitvami, čarovnicami/katalogi/konflikti/trdnostjo, konfiguracijo domačega ujetja, oddajanjem klicev, zavrnjenjem klicev in obnovitvijo prekinjenih obveznosti. Native callback vnos je bil simuliran; fizični človeški G ključ pritisek ostaja ročno preverjanje.
- Trenutna invarija prednjega okna je bila preverjena za izvedbo toast. Pregledano je bilo interakcijsko lastnost in prikaz/skritost; zasloni so bili narejeni za čarovnika, preklapljanje in toast.
- 27 jeziki vsebujejo vse 819 kanonske vrstice; brezžični gostitelj / ključna validacija je minila.

Snapshots in polni GUI rezultati so v artefaktih/usp-win-x64/smoke. Preizkušnje uporabljajo začasne profile; nastavitve proizvodnje so podprte in preverjene med namestitvijo.
