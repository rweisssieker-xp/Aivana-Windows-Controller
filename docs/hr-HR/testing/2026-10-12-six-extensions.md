<!-- generated-doc locale=hr-HR source=docs/testing/2026-10-12-six-extensions.md sha256=ff2eb755b2f18dce8c7bd7c832018ae71ee706bcca65e37c191dbb55ff999c1e -->

> Strojno potpomognut prijevod. Struktura, primjeri koda i poveznice provjeravaju se; to ne znači pregled izvornog govornika ili pravno odobrenje. Povijesna izvješća zadržavaju izvorne datume i opseg. [Originalni izvor](../../testing/2026-10-12-six-extensions.md)

# Šest proširenja: provedba i razina ispitivanja

Stanište: 12. listopad 2026, Područje `codex/six-extensions`. Radno stanje, bez otpuštanja proizvodnje.

## Služba

Zapovijedna ploča otvara upute **Uređivanje radnog mjesta**, **Proizvođači tipkovnica i SDK**, **Mobilni panel**, **proširenja**, **UI automatizacija**, **UI završava** i **Provjera provedbe**.

- Radni asistent stvara profile za radno mjesto, sastanke, prijenos, istraživanje i projekt. Programi iz registracije/start menija ili odabira datoteke, URL-ovi, izlazak/izgled i scena OBS prikazani su kao korake prije pohrane. Institucija ne provodi nikakve aktivnosti.
- U redatelj koraka **Rezultati se provjeravaju** i uključuju preglede i preglede. Procjenjuje i mobilni panel razlikuju stvarnu potvrdu od nepovjerljivog učinka. Relativne promjene, skraćivanje i uvod teksta ne pružaju pauznu potvrdu učinka. Procesni dokaz ne dokazuje stručnu spremnost za primjenu.
- UI automatizacija koristi odvojen, vremenski ograničen radnik. Izbor putem položaja miševa, ispitivanja selektora, Invoke/Select/Toggle/Focus/SetValue/WaitState; odbijeni su brojni golovi, deaktivirani ciljevi i lozinke. Snimka prikazuje klikove i pauze; tekstovi se ne smanjuju. Prije preuzimanja provjerava se prijem.
- Proširenja: ZIP ispitivanje, hash, shema, deaktivirani uvoz, izričito povjerenje prije aktivacije, izolirani proces s ograničenim IPC, izvoz, deaktiviranje, deinstaliranje, obnoviti/odbijati prethodnu verziju. Procesna izolacija nije pješčana kutija: objavljeni plugini rade s korisničkim pravima. Primjer paketa `makrivo.example.zip` sadrži brojanje i status akcije. Podržane sheme predstavljaju dokumentiran jednostavan raspon JSON sheme u izvornom kodu; nema javnog tržišta.
- Mobilni panel: odaberite lokalni IP i objavljeni profili, otvoriti HTTPS adresu, provjeriti certifikat na temelju prikazano SHA-256-Fingerprints i svjesno povjerovati, a zatim unijeti jedinstvenu šestičlansku kod. Privatni ključi su zaštićeni Windows-DPAPI; pohranjuju se samo hashovi klijenata. Certifikati se mijenjaju nakon obnove ili promjene IP-a. Uklanjanje Windows Firewall obavlja korisnik ako je to potrebno. Nema automatske instalacije certifikata, promjena vrata ili ognjišta. Klijenti se mogu povući. Panel prikazuje stanje djelovanja i tijek ispitivanja; još uvijek nije univerzalni live senzor.

## Tastature

| Proizvođač | Uvođenje | ograničenja |
|---|---|---|
| Corsair | postojeće iCUE G tipke i RGB | Potrebno je lokalno povezivanje iCUE i odgovarajući model |
| Logitech G | G-Key SDK i odvojeno LED SDK | Original lokalni x64-DLL izričito; G-Key SDK ne pruža pojedinačnu identitet više tipkovnica |
| Razer | Chroma RGB i standardni otkrivanje | Prethodni članakPredviđanje na Synapse |
| SteelSeries | GameSense-RGB i standardni otkrivanje tipki | Prethodni članakPredviđanje vlastitosti u GG |
| Wooting | Analogne vrijednosti povezane s uređajem, valovi tlaka/razbijanja | Originalni lokalni x64-SDK-DLL; normalni HID ulaz ostaje aktivan |
| Keychron/QMK/VIA/Ducky/ASUS/HyperX/Roccat/Ostali | Fizički standardni tipke prema uređaju i skeniranju koda | Fn i vlastiti kontrolni elementi ovisno o modelu |

Nema novih vlastitih SDK-a. Novi adapteri proizvođača još nisu potvrđeni bez odgovarajućeg fizičkog uređaja.

**Ekskluzivni drugi makropad nije završen:** Raw Input Održavanje uređaja odvojeno, ali suzbijanje njihove normalne Windows-Proizvodnja zahtijeva licenciran, potpisan, pod Windows 11/Secure Boot provjerene filtre, kao i testovi za hitno oslobađanje, pad, suspenzija i ponovnog povezivanja. nema pogona instaliran..

## ispitivanja

- Izgradnja izdanja: uspješna, nula upozorenja/greške.
- Core konzole: sve skupine testiranja uspješno, uključujući sukobe s tipkama proizvođača, plugina i stvarne pogreške u testiranju.
- Integracija proširenja: pravi UIA radnik s WPF testnom primjenom; jedinstvena invoke, višestruka, deaktivirani ciljevi, isključivanje lozinke. Uspješno nazvan proces plugina, granice protokola, analogna histerija i Logitech ABI dekodiranje.
- Istinski HTTPS host: štampani certifikat, nepriključan pristup, lažan/jednom kod, zaštita izvora, datoteke pretraživača, hash skladištenje i zaštitni ključ uspješno. U testiranju je ispravljena pogreška s efemiranim ključima.
- Windows-Suite s izričito isključenim testom uspjeha: uspješno; Audio/OBS/iCUE/Raw Input i stanje zaslona provjerena. u prethodnom tijeku, cijela suita nije uspjela promijeniti fokus na izolirano prozorsko okno. iCUE-Tastator. još jedan ponavljanje ponovno nije uspjelo promijeniti fokus; kontekstni test nije stabilan zeleni. ovaj test nije oslabljen. kratko vrijeme nedostajuće DDC čitanja su ograničene ponavljanja; svjetlost se nije promijenila.
- WPF-Smoke: sve prethodne i nove dijaloge, konfiguracijske varijante, urednik i CRUD uspješno; snimke zaslona u rasporedu `smoke`.
- Browser: Playwright s Edge, 390×844 i 1024×768, Uspješno prijenos / UUID/jednom prijenosom / bez horizontalnog prijenosa. API-Mock; stvarni HTTPS test odvojeno. `artifacts/mobile-browser`. Browser plugin nije dostupan, stoga redoviti Playwright Fallback.
- Neovisno preispitivanje kodova; kasni mobilni zapovijedi, nedostatak rezultata provjere, provjera procesa, plugin-rollback i ponovnog pokretanja RGB-Heartbeat ispravljeno.

Još uvijek otvoreno: ekskluzivna integracija pogona, fizički test proizvođača/telefon/tableta LAN-a, Windows stupanj 150/200 i potpuni pregled prijevoda svih novih dijaloga. Novi dijalogi su uglavnom njemački; postojeći jezični katalogi ostaju. Upravo zbog toga se ukupni zahtjev ** nije potpuno smanjio**.

## Proizvodnja paketa

`./tools/publish-complete.ps1 -SelfContained` stvara novu mapu pod `artifacts/release-*`, uključujući Desktop, UIA radnika, HTTPS host, pretraživač datoteke, uvozni primjer plugin i licenciranje putovanja. Instalirana aplikacija se ne prepisuje. Bez `-SelfContained` paketi trebaju odgovarajuće .NET-10-Desktop-/ASP.NET-Runtimes.

Paket je lokalni test, a ne potpisani prodajni instalator. Osobni uvjeti korištenja i prava na prodaju SDK-a su odvojeno izdanje; pogledajte postojeće dokumentacije na `docs/licensing`.
