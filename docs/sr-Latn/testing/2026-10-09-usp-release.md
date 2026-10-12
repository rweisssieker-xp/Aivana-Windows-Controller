<!-- generated-doc locale=sr-Latn source=docs/testing/2026-10-09-usp-release.md sha256=96a5b23b67032ef07f2f5dd93e9585e7bab0a522e5096ffc9f4d020823c4264e -->

> Mašinski potpomognut prevod. Struktura, primeri koda i veze se proveravaju; to ne znači pregled izvornog govornika ili pravno odobrenje. Istorijski izveštaji zadržavaju izvorne datume i obim. [Originalni izvor](../../testing/2026-10-09-usp-release.md)

# Pomoć tastature, preklapanje i povratne informacije

[English (US)](2026-10-09-usp-release.md) | [Deutsch](2026-10-09-usp-release.md) | [Français](2026-10-09-usp-release.md) | [Italiano](2026-10-09-usp-release.md)

Implementirali su tri prioritetne poboljšanja u postojećoj aplikaciji Windows:

- Specijalni ključ čarobnjak: stvarno iCUE-podržan uređaj / ključ inventar, privremeni G-chei snimanje, konfiguracija-uspeh verifikacija, validacija konflikta i nezaštićeni projekat akcije. Snimanje rollback, nemogućnost obavljanja poziva i normalno vezujuće obnavljanje pokriva greške i zatvaranje dijaloga.
- Tabela: fizička referentna geometrija, aktivni profilni vezi, živa profil / uređivanje i obična Windows ključna naglasa. Držite Ctrl+Alt+F1 ili prođete kroz paletu / tastaturu. Neaktiviranje, klik i odsustvo iz tabčaka.
- Izvršenje povratne informacije: tipovan Čekanje / trčanje / korak / završen / neuspješno / odbijena faza, korak / ponavljanje napretka i ne-aktivirajuće tost. Reakcija isključuje sadržaj akcije i izvanredne poruke. Poslednji korak otkazivanja ne može da se obaveštava o završetku. Potpunost opisuje izvršenje, a ne nezavisno državno pitanje OBS.

Verifikacija :

- 24 Ključni pregledi su prošli, uključujući fazu narudžbine, rezultate neuspjeha / otklanjanja, konačnu fazu otklanjanje, izolaciju slušaoca i isključivanje privatnih vrednosti akcije.
- Rešenje za oslobađanje je prošlo bez upozorenja / grešaka.
- Windows provera je prošao; povezan K100 izveštaj G1-G6 i indijanski konfiguracija / oslobađanje je uspeo. Audio krajnje tačke su proverene bez promene njihovog stanja.
- WPF dim prošao sa svim referentnim rasporedama, čarobnjak / katalog / sukob / trajnost, indijanski konfiguracija za snimanje, poziv nazad prenos, odbijanje poziv nazad i restauracija prekida. Narodna pozivna ulaza je simulirana; fizički ljudski G ključ presa ostaje ručni pregled.
- Aktuelna prednja promenljivost prozora je proverena za izvršenje tosta. Pročitano je interaktivne osobine i pokazivanje / sakrivanje; snimak ekrana je napravljen za čarobnjaka, sakrivanje i tost.
- 27 jezici sadrže sve 819 kanoničke nijanse; offline lokator / ključna validacija prošao.

Snapshots i puni GUI rezultat su u artifaktima/upp-vin-k64/dumak. Testovi koriste privremene profile; podešavanja proizvodnje su podržana i hash proverena tokom rasporeda.
