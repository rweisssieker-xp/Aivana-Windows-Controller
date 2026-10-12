<!-- generated-doc locale=sr-Latn source=docs/superpowers/specs/2026-10-08-keyboard-mapping.md sha256=fb766a2a2b4698ae3e1c3f63b50bb4f5c605da3dd56c61c05fcc0832b5cf8aeb -->

> Mašinski potpomognut prevod. Struktura, primeri koda i veze se proveravaju; to ne znači pregled izvornog govornika ili pravno odobrenje. Istorijski izveštaji zadržavaju izvorne datume i obim. [Originalni izvor](../../../superpowers/specs/2026-10-08-keyboard-mapping.md)

# Fizička tastatura za mapiranje

[English (US)](2026-10-08-keyboard-mapping.md) | [Deutsch](2026-10-08-keyboard-mapping.md) | [Français](2026-10-08-keyboard-mapping.md) | [Italiano](2026-10-08-keyboard-mapping.md)

## Cilj

Pogled tastature treba da pokaže stvarne tastature prilikom dodeljenja akcija. Jednaka široka linija ne dovoljno: Enter, Shift, Prostor, Navigacija, Block brojeva i proizvođač dodatne kontrole se razlikuju.

## Implementacija

- Katalog sa odvojenim fizičkim identifikatorima, geometrijom u tastaturama, štampanim legendama i Windows kodovima.
- ISO-DE i ANSI-US pojedinačno kao puni format i TKL; dodatno Corsair K100 RGB, koji je priznat preko iCUE kao povezan model.
- ISO Enter sa L-Formom, Block brojeva sa visokim plus/inter tasterima i širokim nulom; Navigacija i trake na svojim pozicijama.
- Izaberite model / layout. USB VID/PID i ime modela ne dokazuju varijaciju jezika. Generalni profili i modelirani profili na osnovu proizvođača su različiti; nema garancije za kompatibilnost milimetara ili firmara.
- Skeniranje koda na aktivnom rasporedu Windows tako da fizički klik ne slučajno ubacuje drugu tačku, kao što je I/Z kada se menja između SAD i DE.
- Priprema otvara projekat u postojećem uređaju akcija. Skladištenje, ispitivanje i odbacivanje ostaju postojeći mehanizmi. Klik ne pokreće akcije.
- Vlasnički G tasteri, profil/lock tasteri i iCUE rad se prikazuju, ali bez implementiranog ulaznog adaptera ne izlaze kao normalni Windows hotkei. Konfigurisani proizvođači skraćenja ostaju mogući način.
- Pokretači ostaju globalni Windows hotkei; vizija uređaja ne znači potiskanje uređaja ili ekskluzivno potiskavanje pojedinačne fizičke tastature.

## Dokaz

[K100-Handbuch sa upravljačkim elementima](https://assets.corsair.com/image/upload/corsairmedia/sys_master/productcontent/AP_K100_RGB_Mechanical_QSG_AD.pdf), [Corsair ANSI / ISO izjava](https://www.corsair.com/au/en/explorer/gamer/keyboards/ansi-vs-iso-whats-the-best-keyboard-layout/).

## Ispitivanje

Testovi geometrije, varijante i ulaznog koda; Izgradnja i postojeći Core/Windows testovi; WPF Smake za prikazivanje i pričvršćivanje bez izvršenja ili promene stvarne korisničke konfiguracije. Istraživanje proizvođača dokumentira dodatne zahteve za model / SKU, bez obzira na aktuelni katalog rasporeda.
