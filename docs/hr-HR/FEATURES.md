<!-- generated-doc locale=hr-HR source=docs/FEATURES.md sha256=ea712f39457ce69f772f09a1294201705a5f36a92a8bf15f356bd80132c3e723 -->

> Strojno potpomognut prijevod. Struktura, primjeri koda i poveznice provjeravaju se; to ne znači pregled izvornog govornika ili pravno odobrenje. Povijesna izvješća zadržavaju izvorne datume i opseg. [Originalni izvor](../FEATURES.md)

# MAKRIVO – Pregled sadržaja

Proizvođač: **Aivana**. Stanište: **12. listopad 2026**. To je razvojni status, bez prodaje ili hardverske distribucije.

## Windows upravljanje

- Kachel, globalni hotkeys, paleta zapovijedi, omiljeni i profili ovisni o aplikaciji.
- Makro s nekoliko koraka, ponavljanja, pauze, prekida i predviđanja.
- Otvaranje programa, datoteka i web stranica; korištenje teksta, međusobnog rasporeda i skraćivanja.
- Uređivanje prozora, blokiranje Windows, upravljanje zvukom i mikrofonom te promjena svjetlosti zaslona preko podržanih sučelja.
- OBS-WebSocket-v5-Anbindung za snimanje, prijenos i audio.
- Šabloni, asistent za radno mjesto, prepoznavanje uređaja i preklapanje tipkovnice.
- Uvoz i izvoz konfiguracija profila; provjera dizajna i otkrivanje sukoba.
- Lokalne postavke pohranjene; ne zahtijeva račun za svakodnevnu upotrebu.

## Povećane funkcije u sadašnjem izvornom kodu

| Područje | Opseg funkcija | granice |
| --- | --- | --- |
| Rezultati ispitivanja | Prethodni članakPredizbor, kontrola i ispit | Samo provjerljivi učinci potvrđuju; uspješni pritisak ne dokazuje ciljni učinak. |
| Radni asistent | Priprema profila i postupaka iz odabranih programa, datoteka, URL-a i drugih akcija | Potrebno je unaprijed vidjeti i pohraniti; uređaj ne dovodi u pitanje. |
| Makroizvješće i Workflows | Prijave, uvjeti, gestovi i lokalna sinhronizacija profila | Posao i granice su dokumentirane u izvješćima o provedbi i testiranju. |
| UI automatizacija | Izaberite ciljeve, provjerite selektor, uključite akcije i ograničene klikove | Odvojeni radnici; više-povijesni, deaktivirani i lozinke su odbijeni; nema opće upisivanje teksta. |
| proširenja | Proveriti, uvoziti, aktivirati, izvoziti i vratiti pakete po izričitoj povjerenju | Procesna izolacija nije pješčana kutija; nema javnog tržišta plugina. |
| Mobilni panel | Lokalni HTTPS panel, povezivanje, odabrani profili i povijest ispitivanja | Realni telefon/tablet LAN prijenos još uvijek otvoren; nema univerzalnog senzornog ploča. |
| Proizvođač adaptera | Corsair, Logitech-G-SDK, Razer/SteelSeries-RGB i Wooting-Analogadapter | Modeli/SDK ovisno; novi adapteri nisu u potpunosti validirani s stvarnim hardverom. Nema novih vlastitih SDK-a povezanih. |
| Fizička tasta | Prepoznajte standardne tipke pomoću uređaja i skeniranja koda | Raw Input ne potiskuje normalno ulazak Windows. |
| Lokalni asistent za razgovor | Opcionalni prototip za Ollama, LM Studio, llama.cpp i ONNX Runtime vrijeme GenAI CPU proizvodi ograničene dizajne iz izričito odabranih resursa | Standardno; native CPU radno vrijeme provjerena slučajnim modelom, ali bez dokazane kvalitete planiranja ili uštede vremena; ne sadrži težine modela. |

## Još uvijek ne ponudi kao gotova prodaja

- **Ekskluzivni drugi uređaj/makropad: ** Nedostaje potpisani, licencirani i hardverno provjereni filter. Normalne ulaznice mogu i dalje doseći Windows.
- Potpuno oduzimanje hardvera svih adaptera proizvođača, kao i stvarne testove mobilnih uređaja.
- Prava mješovita promjena monitor/DPI i profesionalni prijenos svih novih prevoda. PerMonitorV2 i simulirani 150/200-Prozent-Layouts provjereni su; stvarni testovi prikazivanja ostaju odvojeno. Površina sadrži 27 jezike; detaljni novi tekstovi još nisu u potpunosti ispisani.
- Potpisani prodajni instalator, konačni EULA, provjerene prava prodaje SDK-a i produktivna integracija kupovine/pregledanja.

## Detalji i dokazi

- [Službena dokumentacija](README.md)
- [Proširenja: funkcije, testovi i otvorene granice](testing/2026-10-12-six-extensions.md)
- [Lokalni AI asistent: Prototyp i testni stupanj](testing/2026-10-12-local-meeting-assistant.md)
- [Lokalni vremenski uvjeti i zahtjevi modela](testing/2026-10-12-local-ai-runtimes.md)
- [Ispitivanje DPI i skaliranja](testing/2026-10-12-dpi-scaling.md)
- [licencijska ispitivanja](licensing/2026-10-12-commercial-license-audit.md) – povijesni zapis; novi publikacijski skript dopunjuje upute za radno vrijeme. Konačni paket treba provjeriti odvojeno.
- [Platforme za prodaju](licensing/makrivo-vertriebsplattformen-2026-10-12.html) – usporedba; Stripe je dodatna moguća platforma, pogledajte trenutne uvjete pružatelja.

## Proizvoditi potpuni razvojni paket

```powershell
./tools/publish-complete.ps1 -SelfContained
```

Paket sadrži desktop aplikaciju, UI automatizacijski radnik, mobilni domaćin, pretraživač datoteke, primjer plugin i upute za radno vrijeme. Stvarni činjenice nisu provjerene u Git. Izvorni kod GitHub još nije objavljen Windows instalator.
