<!-- generated-doc locale=sr-Latn source=docs/FEATURES.md sha256=ea712f39457ce69f772f09a1294201705a5f36a92a8bf15f356bd80132c3e723 -->

> Mašinski potpomognut prevod. Struktura, primeri koda i veze se proveravaju; to ne znači pregled izvornog govornika ili pravno odobrenje. Istorijski izveštaji zadržavaju izvorne datume i obim. [Originalni izvor](../FEATURES.md)

# MAKRIVO – Pregled funkcije

Proizvođač: **Aivana**. Stanje: **12. oktobar 2026**. Ovo je stanje razvoja, a ne odobrenje prodaje ili hardvera.

## Windows Upravljanje

- Pločice, globalne prečice, paleta komandi, omiljene stavke i profili za određene aplikacije.
- Makroi sa više koraka, ponavljanjima, pauzama, otkazivanjem i pregledom.
- Otvaranje programa, datoteka i veb-sajtova; korišćenje teksta, ostave i prečica na tastaturi.
- Uređivanje prozora, blokiranje Windows, kontrola audio izlaza i mikrofona, kao i promena svetlosti ekrana preko podržanih interfejsa.
- Integracija OBS-WebSocket-v5 za scene, snimanje, striming i zvuk.
- Šabloni, asistent radnog mesta, prepoznavanje uređaja i preklapanje tastature.
- Import i izvoz konfiguracija profila; ispitivanje dizajna i otkrivanje konflikta.
- Lokalno skladištene podešavanja; nije potreban račun za svakodnevnu upotrebu.

## Proširene funkcije u aktuelnom izvornom kodu

| Oblast | Opseg funkcije | Granice |
| --- | --- | --- |
| Rezultat ispitivanja | Prethodna provera, naknadna kontrola i istorija provera | Samo provereni efekti potvrđuju; uspešan pritisak ne dokazuje ciljni efekat. |
| Pomoćnik na radnom mestu | Priprema profila i postupaka iz izabranih programa, datoteka, URL-a i drugih akcija | Potrebni su pregled i čuvanje; podešavanje ništa ne izvršava. |
| Makro i Workflows | Snimanje, uslovi, gestovi i lokalna sinhronizacija profila | Ponašanje i granice su dokumentirani u izveštajima o implementaciji i testiranju. |
| UI automatizacija | Izaberite ciljeve, pregledajte selektore, uključite akcije i ograničene klikove | Zaseban radni proces; dvosmisleni, onemogućeni ciljevi i polja lozinki se odbijaju; nema opšteg snimanja unosa teksta. |
| Proširenja | Proveriti, uvoziti, aktivirati, izvoziti i vratiti pakete po izričitoj poverenju | Izolacija procesa nije sandbox; nema javnog tržišta dodataka. |
| Mobilni panel | Lokalni HTTPS panel, povezivanje, izabrani profili i istoriju stanja / ispitivanja | Prihvatno testiranje stvarnog telefona/tableta preko LAN još nije dovršeno; nije univerzalna kontrolna tabla senzora. |
| Proizvođač adapter | Corsair, Logitech-G-SDK, Razer/SteelSeries-RGB i Wooting-Analizator | Zavisi od modela/SDK-a; novi adapteri nisu potpuno provereni na stvarnom hardveru. Nove vlasničke datoteke SDK-DLL nisu priložene. |
| Fizička tastatura | Standardni tasteri koristeći uređaj i skeniranje koda | Raw Input ne potiskuje unos normalnog Windows. |
| Lokalni asistent | Neobavezni prototip za Ollama, LM Studio, llama.cpp i ONNX Runtime GenAI CPU pravi ograničene nacrte iz izričito izabranih resursa | Podrazumevano isključeno; nativno CPU izvršno okruženje provereno nasumičnim modelom, ali bez dokazanog kvaliteta planiranja ili uštede vremena; težine modela nisu uključene. |

## Još uvek ne nudi kao gotovo prodaju

- **Isključiva druga tastatura/makropad:** Nedostaje potpisani, licencirani i hardverski provereni filterski drajver. Uobičajeni pritisci tastera i dalje mogu da stignu do Windows.
- Potpuno prihvatno testiranje hardvera svih adaptera proizvođača i stvarni testovi mobilnih uređaja preko LAN.
- Stvarni prelazi između različitih monitora/DPI i stručna provera svih novih prevoda. PerMonitorV2 i simulirani rasporedi sa 150/200-procentnim skaliranjem su provereni; stvarni testovi ekrana ostaju zasebni. Interfejs nudi 27 jezika; detaljni novi tekstovi još nisu potpuno jezički odobreni.
- Potpisani prodajni instalacioni program, konačni EULA, proverena prava distribucije SDK-a i produkciona integracija kupovnih licenci/naplate.

## Detalji i dokazi

- [Usluge dokumentacije](README.md)
- [Proširenja: funkcije, testovi i otvorene granice](testing/2026-10-12-six-extensions.md)
- [Lokalni II asistent: prototip i test stanje](testing/2026-10-12-local-meeting-assistant.md)
- [Lokalni II vremenski uslovi i modeli](testing/2026-10-12-local-ai-runtimes.md)
- [Ispitivanje DPI i skalacije](testing/2026-10-12-dpi-scaling.md)
- [Licenca ispitivanja](licensing/2026-10-12-commercial-license-audit.md) – istorijski snimak; novi Publiš skript dopunjuje Runtejm indikacije. Konačni paket treba proveriti odvojeno.
- [Platforme za prodaju](licensing/makrivo-vertriebsplattformen-2026-10-12.html) – Poređenje; Stripe je dodatno moguća platforma za plaćanje, pogledajte trenutne uslove ponuđača.

## Uspostavljanje kompletnog paketa

```powershell
./tools/publish-complete.ps1 -SelfContained
```

Paket sadrži desktop aplikaciju, radni proces automatizacije interfejsa, mobilni host, datoteke pregledača, primer dodatka i licencna obaveštenja izvršnog okruženja. Rezultati izgradnje ne dodaju se u Git. Izvorni kod na GitHub još nije objavljen instalacioni program Windows.
