<!-- generated-doc locale=sl-SI source=docs/FEATURES.md sha256=ea712f39457ce69f772f09a1294201705a5f36a92a8bf15f356bd80132c3e723 -->

> Strojno podprt prevod. Struktura, primeri kode in povezave se preverjajo; to ne pomeni pregleda maternega govorca ali pravne odobritve. Zgodovinska poročila ohranijo izvirne datume in obseg. [Izvirni vir](../FEATURES.md)

# MAKRIVO - Pregled funkcij

Proizvajalec: **Aivana**. Stanje: **12. oktober 2026**. To je stanje razvoja, ne odobritev prodaje ali strojne opreme.

## Windows upravljanje

- Ploščice, globalne bližnjice, ukazna paleta, priljubljene postavke in profili za posamezne aplikacije.
- Makri z več koraki, ponovitvami, premori, preklicem in predogledom.
- Odpiranje programov, datotek in spletnih mest; uporaba besedila, odložišča in bližnjic na tipkovnici.
- Urejanje oken, blokiranje Windows, nadzor avdio izhodov in mikrofonov ter spreminjanje svetlosti zaslona prek podprtih vmesnikov.
- Integracija OBS-WebSocket-v5 za scene, snemanje, pretakanje in zvok.
- Šabloni, pomočnik za delovno mesto, prepoznavanje naprave in nadomestilo tipkovnice.
- Uvoz in izvoz profilnih konfiguracij; preverjanje in odkrivanje konfliktov.
- Lokalno shranjene nastavitve; račun ni potreben za vsakodnevno uporabo.

## Razširjene funkcije v trenutnem viru

| območje | Obseg funkcij | meje |
| --- | --- | --- |
| Rezultati pregleda | Predhodno preverjanje, naknadna kontrola in zgodovina preverjanja | Samo preverljivi učinki potrjujejo; uspešen pritisk na tipko ne dokazuje ciljnega učinka. |
| Delovni asistent | Priprava profilov in postopkov iz izbranih programov, datotek, URL-jev in drugih ukrepov | Potreben je predogled in shranjevanje; nastavitev ničesar ne izvede. |
| Makro in Workflows | Snemanje, pogoji, geste in lokalna sinhronizacija profilov | Obnašanje in omejitve so zapisane v poročilih o izvajanju in testiranju. |
| UI avtomatizacija | Izberite cilje, preverite selektorje, sprejmite ukrepe in omejene klikovne poteke | Ločen delovni proces; dvoumni, onemogočeni cilji in polja za gesla se zavrnejo; splošno snemanje vnosa besedila ni podprto. |
| Razširitve | Preverjanje, uvoz, aktiviranje, izvoz in vračanje paketov po izrecnem zaupanju | Izolacija procesa ni peskovnik; javne tržnice vtičnikov ni. |
| Mobilni panel | Lokalni HTTPS panel, povezava, izbrani profili in status/pregled | Sprejemno preverjanje s pravim telefonom/tablico prek LAN ostaja odprto; to ni univerzalna nadzorna plošča senzorjev. |
| Proizvajalec adapter | Corsair, Logitech-G-SDK, Razer/SteelSeries-RGB in Wooting-Analogadapter | Odvisno od modela/SDK; novi adapterji niso v celoti preverjeni s pravo strojno opremo. Nove lastniške DLL-je SDK niso priložene. |
| Fizična nastavitev | Standardni tipki za prepoznavanje s pomočjo naprave in skeniranja kode | Raw Input ne zavira normalnega vnosa Windows. |
| Lokalni pomočnik za razpravo | Izbirni prototip za Ollama, LM Studio, llama.cpp in ONNX Runtime GenAI CPU ustvarja omejene osnutke iz izrecno izbranih virov | Privzeto izklopljeno; nativno izvajanje CPU preverjeno z naključnim modelom, vendar brez dokazane kakovosti načrtovanja ali prihranka časa; uteži modelov niso vključene. |

## Še vedno ne ponujajo kot končana prodaja

- **Izključna druga tipkovnica/makropad:** Manjka podpisan, licenciran in strojno preizkušen filtrirni gonilnik. Običajni pritiski tipk lahko še vedno dosežejo Windows.
- Popolno sprejemno preverjanje strojne opreme vseh adapterjev proizvajalcev ter pravi preizkusi mobilnih naprav prek LAN.
- Pravi prehodi med različnimi monitorji/DPI in strokovna odobritev vseh novih prevodov. Preverjeni so bili PerMonitorV2 in simulirane postavitve s 150/200-odstotno povečavo; pravi preizkusi zaslonov ostajajo ločeni. Vmesnik ponuja 27 jezikov; podrobna nova besedila še niso v celoti jezikovno odobrena.
- Podpisan prodajni namestitveni program, končna EULA, preverjene pravice do distribucije SDK in produkcijska integracija nakupnih licenc/blagajne.

## Podrobnosti in dokazi

- [Uporabna dokumentacija](README.md)
- [Razširitve: funkcije, testi in odprte meje](testing/2026-10-12-six-extensions.md)
- [Lokalni AI asistent: prototip in preizkus](testing/2026-10-12-local-meeting-assistant.md)
- [Lokalne časovne razmere in zahteve za modeliranje](testing/2026-10-12-local-ai-runtimes.md)
- [DPI in merjenje](testing/2026-10-12-dpi-scaling.md)
- [Pregled licence](licensing/2026-10-12-commercial-license-audit.md) – zgodovinski zapis; novi publish skripti dopolnjujejo Runtime navodila. Končni paket je treba pregledati ločeno.
- [Trgovinske platforme](licensing/makrivo-vertriebsplattformen-2026-10-12.html) – primerjava; Stripe je poleg tega možna platforma za plačilo, glej trenutne pogoje ponudnika.

## Ustvarite celoten razvojni paket

```powershell
./tools/publish-complete.ps1 -SelfContained
```

Paket vsebuje namizno aplikacijo, delovni proces za avtomatizacijo vmesnika, mobilni gostitelj, datoteke brskalnika, vzorčni vtičnik in licenčna obvestila izvajalnega okolja. Rezultati gradnje se ne dodajajo v Git. Izvorna koda na GitHub še ni objavljen namestitveni program Windows.
