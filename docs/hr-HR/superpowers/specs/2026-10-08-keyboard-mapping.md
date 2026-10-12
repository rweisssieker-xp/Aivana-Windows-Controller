<!-- generated-doc locale=hr-HR source=docs/superpowers/specs/2026-10-08-keyboard-mapping.md sha256=fb766a2a2b4698ae3e1c3f63b50bb4f5c605da3dd56c61c05fcc0832b5cf8aeb -->

> Strojno potpomognut prijevod. Struktura, primjeri koda i poveznice provjeravaju se; to ne znači pregled izvornog govornika ili pravno odobrenje. Povijesna izvješća zadržavaju izvorne datume i opseg. [Originalni izvor](../../../superpowers/specs/2026-10-08-keyboard-mapping.md)

# Fizički pogled na tipkovnicu za mapiranje

[English (US)](2026-10-08-keyboard-mapping.md) | [Deutsch](2026-10-08-keyboard-mapping.md) | [Français](2026-10-08-keyboard-mapping.md) | [Italiano](2026-10-08-keyboard-mapping.md)

## Cilj

Prikaz tipkovnice trebao bi pokazati stvarne položaje za dodjelu akcija. U isto vrijeme, široke linije nisu dovoljne: Enter, Shift, Space, navigacija, broj blok i proizvođač dodatnih kontrolnih elemenata razlikuju se.

## provedba

- Katalog s odvojenim fizičkim ID-ovima, geometrijom u tipkovnicama, tiskanim legendama i Windows kodovima.
- ISO-DE i ANSI-US kao cjeloviti format i TKL; osim toga, Corsair K100 RGB, koji je prepoznat kao priključen model preko iCUE.
- ISO Enter s L-formom, brojevnim blokom s visokim plus/inter tipkama i širokom nulu; navigacija i trake na vlastitim pozicijama.
- Izaberite model/layout. USB VID/PID i ime modela ne dokazuju varijante jezika. Generički profili i modelirani profili na temelju dokumentacije proizvođača mogu se prepoznati različito; nema jamstva kompatibilnosti milimetra ili firmware.
- Skaniranje kodova na aktivnom rasporedu Windows, tako da fizički klik ne slučajno stavlja drugu tipku, kao što je Y/Z kada se mijenja između SAD-a i DE-a.
- Priključenje otvara projekt u postojećem uredniku akcija. Skladištenje, ispitivanje i odbijanje sukoba ostaju postojeće mehanizme. Klikovi ne pokreću akcije.
- Proizvodi G tipke, profil / blok tipke i iCUE radovi su prikazani, ali bez implementacije adaptera ne izlazi kao normalni Windows hotkeys. Konfigurirani proizvođači kratice ostaju mogući način.
- Ulaznice ostaju globalni Windows hotkeys; pogled na uređaj ne znači specifično suzbijanje ili isključivo položenje pojedine fizičke tipkovnice.

## Dokazi

[K100-Handbuch s kontrolnim elementima](https://assets.corsair.com/image/upload/corsairmedia/sys_master/productcontent/AP_K100_RGB_Mechanical_QSG_AD.pdf), [Corsair ANSI/ISO izjava](https://www.corsair.com/au/en/explorer/gamer/keyboards/ansi-vs-iso-whats-the-best-keyboard-layout/).

## ispitivanje

Geometrijski, varijanti i testovi uvođenja; izgradnja i postojeći testovi kora/Windows; WPF smoke za prikazivanje i dodjelu bez provođenja ili promjene stvarne konfiguracije korisnika. Istraživanje proizvođača dokumentira dodatne zahtjeve za model/SKU, bez obzira na trenutni katalog postavljanja.
