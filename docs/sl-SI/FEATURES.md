<!-- generated-doc locale=sl-SI source=docs/FEATURES.md sha256=ea712f39457ce69f772f09a1294201705a5f36a92a8bf15f356bd80132c3e723 -->

> Strojno podprt prevod. Struktura, primeri kode in povezave se preverjajo; to ne pomeni pregleda maternega govorca ali pravne odobritve. Zgodovinska poročila ohranijo izvirne datume in obseg. [Izvirni vir](../FEATURES.md)

# MAKRIVO - Pregled funkcij

Proizvajalec: **Aivana**. Stanišče: **12. Oktobra 2026**. To je razvojna stopnja, ne prodaja ali ločitev strojne opreme.

## Windows upravljanje

- Kabel, globalni hotkeys, ukazna paleta, priljubljeni in profili, ki so odvisni od uporabe.
- Makro z več koraki, ponavljanja, prekinitve in predogled.
- Odprite programe, datoteke in spletne strani; uporabite besedilo, mehanizem in kratke tipke.
- Urejanje oken, blokiranje Windows, nadzor avdio izhodov in mikrofonov ter spreminjanje svetlosti zaslona prek podprtih vmesnikov.
- OBS-WebSocket-v5-Anbindung za snemanje, snemanje in avdio.
- Šabloni, pomočnik za delovno mesto, prepoznavanje naprave in nadomestilo tipkovnice.
- Uvoz in izvoz profilnih konfiguracij; preverjanje in odkrivanje konfliktov.
- Lokalno shranjene nastavitve; račun ni potreben za vsakodnevno uporabo.

## Razširjene funkcije v trenutnem viru

| območje | Obseg funkcij | meje |
| --- | --- | --- |
| Rezultati pregleda | Preverjanje, spremljanje in preizkus | Samo preverljivi učinki potrjujejo; uspešen pritisk na tipko ne dokazuje ciljnega učinka. |
| Delovni asistent | Priprava profilov in postopkov iz izbranih programov, datotek, URL-jev in drugih ukrepov | Predogled in shranjevanje je potrebno; namestitev ne povzroča nič. |
| Makro in Workflows | Vpis, pogoji, gest in lokalna sinhronizacija | Obnašanje in omejitve so zapisane v poročilih o izvajanju in testiranju. |
| UI avtomatizacija | Izberite cilje, preverite selektorje, sprejmite ukrepe in omejene klikovne poteke | Ločeni delavci; večnamenski, deaktivirani in gesla cilji so zavrnjeni; nobena splošna vnos besedila. |
| Razširitve | Preverjanje, uvoz, aktiviranje, izvoz in vračanje paketov po izrecnem zaupanju | Procesna izolacija ni pesek; ni javnega tržnega prostora za plugine. |
| Mobilni panel | Lokalni HTTPS panel, povezava, izbrani profili in status/pregled | Realni telefon/tablet LAN oddajanje še vedno odprt; brez univerzalnega senzorja plošče. |
| Proizvajalec adapter | Corsair, Logitech-G-SDK, Razer/SteelSeries-RGB in Wooting-Analogadapter | Model / SDK odvisno; novi adapter ni popolnoma potrjen z realno strojno opremo. Ni novih lastnih SDK-DLL povezanih. |
| Fizična nastavitev | Standardni tipki za prepoznavanje s pomočjo naprave in skeniranja kode | Raw Input ne zavira normalnega vnosa Windows. |
| Lokalni pomočnik za razpravo | Opcionalni prototip za Ollama, LM Studio, llama.cpp in ONNX Runtime čas GenAI CPU ustvarja omejene zasnove iz izrecno izbranih virov | Standardno; native CPU delovni čas z naključnim modelom, vendar ni dokazano kakovost načrtovanja ali prihranek časa; ne vsebuje teže modela. |

## Še vedno ne ponujajo kot končana prodaja

- **Ekskluzivni drugi računalnik/makropad:** Ni podpisanega, licenciranega in strojno preverjenega filtra. Običajne tipke lahko še naprej dosežejo Windows.
- Popolna odstranitev strojne opreme vseh adapterjev proizvajalca, pa tudi realni mobilni napravni test.
- Pravi mešan monitor/DPI spreminjanje in strokovno prevajanje vseh novih prevodov. PerMonitorV2 in simulacije 150/200-Prozent-Layouts so bili preverjeni; realni test zaslona ostane ločeno. Površina vsebuje 27 jezike; podrobne nove besedila še niso v celoti prenesene.
- Podpisani prodajni inštalator, končni EULA, preverjene prodajne pravice SDK in produktivna integracija nakupa/pregleda.

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

Paket vsebuje namizno aplikacijo, UI avtomatizatorja, mobilni gostitelj, brskalnik datoteke, primerni plugin in obvestilo o licencah za izvajanje. Gradbeni podatki niso preverjeni v Git. Izvorni kodo GitHub še ni objavljen Windows namestnik.
