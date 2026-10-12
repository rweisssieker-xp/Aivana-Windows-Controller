<!-- generated-doc locale=sl-SI source=docs/superpowers/specs/2026-10-08-keyboard-mapping.md sha256=fb766a2a2b4698ae3e1c3f63b50bb4f5c605da3dd56c61c05fcc0832b5cf8aeb -->

> Strojno podprt prevod. Struktura, primeri kode in povezave se preverjajo; to ne pomeni pregleda maternega govorca ali pravne odobritve. Zgodovinska poročila ohranijo izvirne datume in obseg. [Izvirni vir](../../../superpowers/specs/2026-10-08-keyboard-mapping.md)

# Fizični pogled na tipko za mapiranje

[English (US)](2026-10-08-keyboard-mapping.md) | [Deutsch](2026-10-08-keyboard-mapping.md) | [Français](2026-10-08-keyboard-mapping.md) | [Italiano](2026-10-08-keyboard-mapping.md)

## Cilj

Prikaz tipkovnice mora prikazati dejanske točke pri dodelitvi ukrepov. Enako široke vrstice ne zadostujejo: Enter, Shift, Space, navigacija, blok številk in dodatni kontrolniki proizvajalca se razlikujejo.

## izvajanje

- Katalog z ločenimi fizičnimi ID-ji, geometrijo v tipkovnicah, tiskanimi legendi in Windows vnosnimi kodami.
- ISO-DE in ANSI-US kot polni format in TKL; poleg tega Corsair K100 RGB, ki je bil priznan kot priključen model preko iCUE.
- ISO Enter z L-oblikovanjem, številčnim blokom z visokimi tipkami Plus/Enter in širšim ničlom; navigacijo in traku na lastnih lokacijah.
- Izberite model / postavitev. USB VID/PID in ime modela ne dokazujejo jezikovne različice. Splošni profili in profili, oblikovani na podlagi dokumentov proizvajalca, se lahko razlikujejo; ni jamstva za združljivost milimetra ali firmware.
- Skeniranje kodo na aktivno nastavitev Windows tako, da fizični tipko ne pride naključno drug tipko, kot je Y/Z, ko se premakne med US in DE.
- Prilagoditev odpre projekt v obstoječem uredniku. Shranjevanje, preverjanje in zavrnitev konfliktov ostanejo obstoječi mehanizmi. Kliki ne začnejo ukrepov.
- Lastniki G tipke, profil / ključavnice in iCUE vrstice so prikazani, vendar brez uvedene vhodne adapterja ne izhaja kot običajni Windows hotkeys. Konfigurabilni proizvajalci kratki ostanejo možen način.
- Nastavitve ostanejo globalni Windows hotkeys; pogled na napravo ne pomeni posebnega zatiranja naprave ali ekskluzivnega položaja posamezne fizične tipkovnice.

## Dokazati

[K100-Handbuch z nadzornimi elementi](https://assets.corsair.com/image/upload/corsairmedia/sys_master/productcontent/AP_K100_RGB_Mechanical_QSG_AD.pdf), [Corsair ANSI/ISO izjava](https://www.corsair.com/au/en/explorer/gamer/keyboards/ansi-vs-iso-whats-the-best-keyboard-layout/).

## Preizkus

Geometrijski, variantni in vnosni testi; gradnje in obstoječi testi/Windows; WPF šminke za prikaz in prilagajanje brez izvajanja ali spremembe dejanske konfiguracije uporabnika. Raziskave proizvajalcev dokumentirajo dodatne zahteve za model/SKU, ne glede na trenutno izvajani katalog postavitve.
