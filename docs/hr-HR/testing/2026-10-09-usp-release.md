<!-- generated-doc locale=hr-HR source=docs/testing/2026-10-09-usp-release.md sha256=96a5b23b67032ef07f2f5dd93e9585e7bab0a522e5096ffc9f4d020823c4264e -->

> Strojno potpomognut prijevod. Struktura, primjeri koda i poveznice provjeravaju se; to ne znači pregled izvornog govornika ili pravno odobrenje. Povijesna izvješća zadržavaju izvorne datume i opseg. [Originalni izvor](../../testing/2026-10-09-usp-release.md)

# Pomoć na tipkovnici, povratne informacije o preuzimanju

[English (US)](2026-10-09-usp-release.md) | [Deutsch](2026-10-09-usp-release.md) | [Français](2026-10-09-usp-release.md) | [Italiano](2026-10-09-usp-release.md)

Implementiraju se tri prioritetna poboljšanja u postojećoj Windows aplikaciji:

- Specijalni ključ čarobnjak: stvarno iCUE podržan uređaj / ključ inventari, privremeni G ključ ulazak, konfiguracija-uspjeh provjera, validacija sukoba i neispravan nacrt djelovanja. Uklanjanje, invaliditet poziva i normalno obvezujuća obnova pokrivaju pogreške i zatvaranje dijaloga.
- Ključna ploča: fizička referentna geometrija, aktivni profili vezanosti, živ profil / urediti osvježavanje i običan Windows ključni naglasak. Držite Ctrl+Alt+F1 ili pritisnite kroz paletu/klavir stranicu. Neaktiviranje, klikom i odsutnost s radnog lista.
- Izvješće o izvršenju: tipiran čekanje / trčanje / korak / završen / neuspjeh / odbijena faza, korak / ponavljanje napretka i neaktivirajući toast. Feedback isključuje sadržaj djelovanja i iznimke poruke. Posljednji korak otkazivanja ne može se prijaviti. Kompletion opisuje izvršenje, a ne nezavisnu državnu prijavu OBS.

Provjera je:

- 24 Ključne provjere prošli, uključujući fazu narudžbe, neuspjeh/odbijanje rezultata, konačni korak otkazivanje, izuzetak slušatelja i isključivanje privatnih vrijednosti djelovanja.
- Rješenje oslobađanja prošlo bez upozorenja / pogrešaka.
- Windows provjere su prošli; povezani K100 izvješćuje G1 – G6 i domaća konfiguracija/izlazak uspio. Audio završne točke su provjerene bez promjene stanja.
- WPF dim je prošao sa svim referentnim rasporedima, čarobnjakom/katalogom/konfliktima/draktom, konfiguracijom domaćeg ulaska, pozivom za povratak, odbijanjem poziva za povratak i ponovnom obnavljanjem. Native callback ulaz je simuliran; fizički ljudski G-key pritisak ostaje ručni pregled.
- Trenutačna invarija prednjeg prozora provjerena je za izvršavanje toasta. Proizvodi interakcije i prikaza / skrivanja provjereni su; snimke su snimljene za čarobnjaka, preklapanje i toast.
- 27 jezike sadrže sve 819 kanonske trake; offline lokacija / ključna validacija prošla.

Snapshots i cjelokupni GUI rezultati su u artefaktima/usp-win-x64/smoke. Testovi koriste privremene profile; postavke proizvodnje se podržavaju i provjeravaju tijekom implementacije.
