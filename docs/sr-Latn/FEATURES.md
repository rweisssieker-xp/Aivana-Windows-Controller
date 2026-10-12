<!-- generated-doc locale=sr-Latn source=docs/FEATURES.md sha256=ea712f39457ce69f772f09a1294201705a5f36a92a8bf15f356bd80132c3e723 -->

> Mašinski potpomognut prevod. Struktura, primeri koda i veze se proveravaju; to ne znači pregled izvornog govornika ili pravno odobrenje. Istorijski izveštaji zadržavaju izvorne datume i obim. [Originalni izvor](../FEATURES.md)

# MAKRIVO – Pregled funkcije

Proizvođač: **Aivana**. Stani: **12. oktobar 2026**. Ovo je stanje razvoja, bez prodaje ili hardvera.

## Windows Upravljanje

- Kacheli, globalni hotkeis, palet komande, omiljeni i aplikacije zavisni profili.
- Makros sa nekoliko koraka, ponavljanja, pauza, prekida i predviđanja.
- Otvorite programe, datoteke i veb stranice; koristite tekst, interval i ključ.
- Uređivanje prozora, blokiranje Windows, kontrola audio izlaza i mikrofona, kao i promena svetlosti ekrana preko podržanih interfejsa.
- OBS-WebSocket-v5-Anbindung za scene, snimanje, striming i audio.
- Šabloni, asistent radnog mesta, prepoznavanje uređaja i preklapanje tastature.
- Import i izvoz konfiguracija profila; ispitivanje dizajna i otkrivanje konflikta.
- Lokalno skladištene podešavanja; nije potreban račun za svakodnevnu upotrebu.

## Proširene funkcije u aktuelnom izvornom kodu

| Oblast | Opseg funkcije | Granice |
| --- | --- | --- |
| Rezultat ispitivanja | Pregled, pregled i ispitivanje | Samo provereni efekti potvrđuju; uspešan pritisak ne dokazuje ciljni efekat. |
| Pomoćnik na radnom mestu | Priprema profila i postupaka iz izabranih programa, datoteka, URL-a i drugih akcija | Potrebno je pregled i skladištenje; uređaj ne izvodi ništa. |
| Makro i Workflows | Prijem, uslovi, gestovi i lokalna sinhronizacija profila | Ponašanje i granice su dokumentirani u izveštajima o implementaciji i testiranju. |
| UI automatizacija | Izaberite ciljeve, pregledajte selektore, uključite akcije i ograničene klikove | Odvojeni radnici; višeglasni, deaktivirani i lozinke ciljevi su odbijeni; nema opšteg unosa teksta. |
| Proširenja | Proveriti, uvoziti, aktivirati, izvoziti i vratiti pakete po izričitoj poverenju | Proces izolacija nije peska; nije javni plugin tržište. |
| Mobilni panel | Lokalni HTTPS panel, povezivanje, izabrani profili i istoriju stanja / ispitivanja | Realni telefon / tablet LAN snimak još uvek otvoren; nema univerzalnog senzora. |
| Proizvođač adapter | Corsair, Logitech-G-SDK, Razer/SteelSeries-RGB i Wooting-Analizator | Model/SDK zavisno; novi adapter nije potpuno validiran sa stvarnim hardverom. Nema novog vlasničkog SDK-a. |
| Fizička tastatura | Standardni tasteri koristeći uređaj i skeniranje koda | Raw Input ne potiskuje unos normalnog Windows. |
| Lokalni asistent | Opcionalni prototip Ollama, LM Studio, llama.cpp i ONNX Runtejm GenAI CPU proizvodi ograničene dizajne iz izričito odabranih resursa | Standardno; indijanski CPU vreme provereno sa slučajnim model, ali nema dokazano kvalitet planiranja ili štednje vremena; ne sadrži težine modela. |

## Još uvek ne nudi kao gotovo prodaju

- **Ekskluzivna druga tastatura/makropada:** Nedostaje potpisani, licencirani i hardverski provereni filter. Uobičajeni tasteri mogu i dalje da dostignu Windows.
- Potpuno uklanjanje hardvera svih adaptera proizvođača, kao i stvarne testiranja mobilnih uređaja.
- Istinski mešani monitor / DPI promenljivi i profesionalni snimak svih novih prevoda. PerMonitorV2 i simuliranih 150/200-Prozent-Layouts su provereni; stvarni testovi ekrana ostaju odvojeni. Na interfejsu se nalaze 27 jezici; detaljni novi tekstovi još nisu u potpunosti preuzeti.
- Potpisan prodavni instalator, konačni EULA, provereni prava prodaje SDK i produktivna integracija kupovine / čeka.

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

Paket sadrži desktop aplikaciju, UI automatizaciju radnika, mobilni host, pretraživač datoteke, primjer plugin i radno vreme licence. Činjenice se ne pregledaju u GIT. GitHub izvorni kod još nije objavljen Windows instalator.
