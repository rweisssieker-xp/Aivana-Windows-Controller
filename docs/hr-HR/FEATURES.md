<!-- generated-doc locale=hr-HR source=docs/FEATURES.md sha256=ea712f39457ce69f772f09a1294201705a5f36a92a8bf15f356bd80132c3e723 -->

> Strojno potpomognut prijevod. Struktura, primjeri koda i poveznice provjeravaju se; to ne znači pregled izvornog govornika ili pravno odobrenje. Povijesna izvješća zadržavaju izvorne datume i opseg. [Originalni izvor](../FEATURES.md)

# MAKRIVO – Pregled sadržaja

Proizvođač: **Aivana**. Stanje: **12. listopada 2026**. Ovo je stanje razvoja, a ne odobrenje prodaje ili hardvera.

## Windows upravljanje

- Pločice, globalni prečaci, paleta naredbi, favoriti i profili za određene aplikacije.
- Makronaredbe s više koraka, ponavljanjima, pauzama, otkazivanjem i pretpregledom.
- Otvaranje programa, datoteka i web-mjesta; korištenje teksta, međuspremnika i tipkovničkih prečaca.
- Uređivanje prozora, blokiranje Windows, upravljanje zvukom i mikrofonom te promjena svjetlosti zaslona preko podržanih sučelja.
- Integracija OBS-WebSocket-v5 za scene, snimanje, prijenos i zvuk.
- Šabloni, asistent za radno mjesto, prepoznavanje uređaja i preklapanje tipkovnice.
- Uvoz i izvoz konfiguracija profila; provjera dizajna i otkrivanje sukoba.
- Lokalne postavke pohranjene; ne zahtijeva račun za svakodnevnu upotrebu.

## Povećane funkcije u sadašnjem izvornom kodu

| Područje | Opseg funkcija | granice |
| --- | --- | --- |
| Rezultati ispitivanja | Prethodna provjera, naknadna kontrola i povijest provjera | Samo provjerljivi učinci potvrđuju; uspješni pritisak ne dokazuje ciljni učinak. |
| Radni asistent | Priprema profila i postupaka iz odabranih programa, datoteka, URL-a i drugih akcija | Potrebni su pretpregled i spremanje; postavljanje ništa ne izvršava. |
| Makroizvješće i Workflows | Snimanje, uvjeti, geste i lokalna sinkronizacija profila | Posao i granice su dokumentirane u izvješćima o provedbi i testiranju. |
| UI automatizacija | Izaberite ciljeve, provjerite selektor, uključite akcije i ograničene klikove | Zaseban radni proces; dvosmisleni, onemogućeni ciljevi i polja lozinki odbijaju se; nema općeg snimanja unosa teksta. |
| proširenja | Proveriti, uvoziti, aktivirati, izvoziti i vratiti pakete po izričitoj povjerenju | Izolacija procesa nije sandbox; nema javnog tržišta dodataka. |
| Mobilni panel | Lokalni HTTPS panel, povezivanje, odabrani profili i povijest ispitivanja | Prihvatno testiranje stvarnog telefona/tableta putem LAN još nije dovršeno; nije univerzalna nadzorna ploča senzora. |
| Proizvođač adaptera | Corsair, Logitech-G-SDK, Razer/SteelSeries-RGB i Wooting-Analogadapter | Ovisi o modelu/SDK-u; novi adapteri nisu potpuno provjereni na stvarnom hardveru. Nove vlasničke datoteke SDK-DLL nisu priložene. |
| Fizička tasta | Prepoznajte standardne tipke pomoću uređaja i skeniranja koda | Raw Input ne potiskuje normalno ulazak Windows. |
| Lokalni asistent za razgovor | Neobvezni prototip za Ollama, LM Studio, llama.cpp i ONNX Runtime GenAI CPU stvara ograničene nacrte iz izričito odabranih resursa | Zadano isključeno; izvorno CPU izvršno okruženje provjereno nasumičnim modelom, ali bez dokazane kvalitete planiranja ili uštede vremena; težine modela nisu uključene. |

## Još uvijek ne ponudi kao gotova prodaja

- **Isključiva druga tipkovnica/makropad:** Nedostaje potpisani, licencirani i hardverski provjereni filtarski upravljački program. Uobičajeni pritisci tipki i dalje mogu doći do Windows.
- Potpuno prihvatno testiranje hardvera svih adaptera proizvođača i stvarni testovi mobilnih uređaja putem LAN.
- Stvarni prijelazi između različitih monitora/DPI i stručna provjera svih novih prijevoda. PerMonitorV2 i simulirani rasporedi sa 150/200-postotnim skaliranjem provjereni su; stvarni testovi zaslona ostaju zasebni. Sučelje nudi 27 jezika; detaljni novi tekstovi još nisu potpuno jezično odobreni.
- Potpisani prodajni instalacijski program, konačni EULA, provjerena prava distribucije SDK-a i produkcijska integracija kupovnih licenci/naplate.

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

Paket sadrži aplikaciju za stolna računala, radni proces automatizacije sučelja, mobilni host, datoteke preglednika, primjer dodatka i licencne obavijesti izvršnog okruženja. Rezultati izgradnje ne dodaju se u Git. Izvorni kod na GitHub još nije objavljen instalacijski program Windows.
