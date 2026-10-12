<!-- generated-doc locale=sl-SI source=docs/testing/2026-10-12-six-extensions.md sha256=ff2eb755b2f18dce8c7bd7c832018ae71ee706bcca65e37c191dbb55ff999c1e -->

> Strojno podprt prevod. Struktura, primeri kode in povezave se preverjajo; to ne pomeni pregleda maternega govorca ali pravne odobritve. Zgodovinska poročila ohranijo izvirne datume in obseg. [Izvirni vir](../../testing/2026-10-12-six-extensions.md)

# Šest razširitev: izvajanje in preskus

Stanišče: 12. Oktobra 2026, Področje `codex/six-extensions`. Delovna doba, brez proizvodnih izpustov.

## Storitev

Ukazovalna plošča odpre vnose **Ustvarjanje delovnega mesta**, **Tiskalniki in SDK**, **Mobilni panel**, **Razširitve**, **UI avtomatizacija**, **Začetek UI** in **Izvedbeni pregled**.

- Delovni asistent ustvari profile za delovno mesto, srečanje, pretok, raziskave in projekt. Programi iz registracije/start menija ali izbire datoteke, URL-jev, izhoda/zaslon in scene OBS se prikažejo kot koraki pred shranjevanjem. Institucija ne izvaja ukrepov.
- V uredniku korakov **odstranite preverjanje** in preverjanje. Potovanje in mobilna plošča ločijo dejansko potrditev od nepreverljivega učinka. Relativne spremembe, skrajšanje in vnos besedila ne zagotavljajo fiksnega potrjevanja učinka. Dokaz postopka ne dokazuje strokovne pripravljenosti za uporabo.
- UI avtomatizacija uporablja ločen, časovno omejen delavec. Izbira po poziciji miške, preizkušanje izbire, Invoke/Select/Toggle/Focus/SetValue/WaitState; več golov, deaktiviranih ciljev in gesel se zavrnejo. Vpis vključuje podprte klike in odmore; besedilni vnosi niso skrajšani. Pred sprejemom se preveri vnos.
- Razširitve: ZIP preizkus, hashes, shema, deaktiviran uvoz, izrecno zaupanje pred aktiviranjem, izoliran proces z omejeno IPC, izvoz, deaktiviranje, obnovitev/odstranitev prejšnje različice. Procesna izolacija ni pesek: razdeljeni plugini delujejo z uporabniškimi pravicami. Primerni paket `makrivo.example.zip` vsebuje izračun in statusno akcijo. Podprte sheme sestavljajo dokumentiran preprost obseg dela sheme JSON v viru; ni javnega trga.
- Mobilni panel: izberite lokalno IP in razdeljene profile, odprite naslov HTTPS, preverite potrdilo na podlagi prikazanega SHA-256-Fingerprints in se zavestno zaupate, nato vnesite enostavno šestih znakov kodo. Zasebni ključi so zaščiteni z Windows-DPAPI; shranjeni so samo hashovi za dostop do strank. Potrdila se spreminjajo po obnovi ali zamenjavi IP. Uporabnik bo razkril firewall Windows po potrebi. Brez samodejne namestitve potrdil, širjenja pristanišča ali spremembe ognja. Dostop do strank se lahko prekliče. Panel prikazuje stanje delovanja in potek pregleda; še ni univerzalni živo senzorje.

## tipkovnice

| Proizvajalec | izvajanje | Omejitev |
|---|---|---|
| Corsair | Obstoječe iCUE G tipke in RGB | lokalna povezava iCUE in primeren model |
| Logitech G | G-Key SDK in ločen LED SDK | Original lokalni x64-DLL izrecno; G-Key SDK ne zagotavlja individualne identitete več tipkovnic |
| Razer | Chroma RGB in standardno zaznavanje tipk | lastniške tipke predhodno dodelite v Synapse |
| SteelSeries | GameSense-RGB in standardno zaznavanje tipk | lastniške tipke predhodno v GG |
| Wooting | Analogne vrednosti povezane z napravo, tlaka / razpona | originalna lokalna x64-SDK-DLL; normalni HID vnos ostane aktiven |
| Keychron/QMK/VIA/Ducky/ASUS/HyperX/Roccat/Še več | Fizični standardni tipki po napravi in skeniranju kode | Fn in lastniški kontrolniki odvisni od modela |

Nobenih novih lastnih SDK-jev ni. Novi adapterji proizvajalca še niso potrjeni brez ustrezne fizične opreme.

**Exclusive drugi makropad ni končan: ** Raw Input opazuje naprave ločeno, vendar ne zavira njihovega normalnega vnosa Windows. Proizvodnja zahteva licenco, podpisano, preverjeno filter pogon pod Windows 11/Secure ter testiranje za izredne izdaje, razpad, suspenzije in ponovne povezave. Ni bilo nameščenih voznikov. Uporaba ta meja izrecno označuje.

## Preizkušnje

- Release-Build: uspešno, nič opozorila / napake.
- Core konzole: vse skupine uspešno, vključno s konflikti pri gumbi proizvajalca, plug-in rollback in dejanske napake izvedbe v procesu testiranja.
- Integracija razširitve: pravi UIA delavec z WPF preskusno uporabo; enostaven invoke, večkratnost, deaktivirani cilji, izključitev gesla. Echter Named Pipe Plugin postopek, protokolne meje, analogna histerija in Logitech ABI dekodiranje uspešno.
- Resnični HTTPS gostitelj: vgrajen certifikat, neskončni dostop, napačen/enkratni kodo, zaščita izvora, brskalnik datoteke, shranjevanje in zaščiten ključ uspešno. Napaka, ki je bila odkrita v testu, je bila popravljena z efemiranimi ključi.
- Windows-Suite z izrecno izpuščenim testom uspeha: uspešno; Audio/OBS/iCUE/Raw Input in preverjeno stanje zaslona. v prejšnjem teku je bila polna soba neuspešna pri zamenjavi osredotočenosti na izolirano preskusno okno. v naslednjem teku je bil tudi celoten soba, vključno s spremembo osredotočenja in povezanimi iCUE-Naslednji ponavljajoči se tečaj je znova neuspešen pri zamenjavi osredotočenja; kontekstno testiranje ni stabilno zeleno. ta test ni bil oslabljen. kratkoročno pomanjkljivost bralcev DDC je omejena; svetlost ni spremenjena.
- WPF-Smoke: vse prejšnje in nove dijaloge, konfiguracijska varianta, urednik in CRUD uspešno; zasloni v gradbenem redu `smoke`.
- Izvirnik: Playwright with Edge, 390×844 in 1024×768, Povezava / UUID / enkratno pošiljanje / brez vodoravnega pretoka uspešno. API-Mock; resnični test HTTPS ločeno. `artifacts/mobile-browser`. Browser plugin ni na voljo, zato redno Playwright Fallback.
- Neodvisna pregleda kod; popravljena zamuda mobilnih naročil, pomanjkanje rezultatov preverjanja, pregleda postopka, vrnitev plugina in ponovitev RGB srčnega dela.

Še vedno odprt: ekskluzivna integracija pogonov, fizični proizvajalec/telefon/tabletni LAN testi, Windows stopnja 150/200 in popolna prevajanja vseh novih besedil. Novi dialogi so večinoma nemški; obstoječi jezikovni katalogi ostanejo ohranjeni. Zato je skupno zahtevo ** ni v celoti zmanjšano **.

## Izdelava paketa

`./tools/publish-complete.ps1 -SelfContained` ustvarja novo mape pod `artifacts/release-*`, vključno z namiznim računalnikom, UIA delavcem, HTTPS gostiteljem, brskalnikom, uvoznim dodatkom in licencami za izvajanje. Nastavljena aplikacija ni podpisan. Brez `-SelfContained` je paketa primerna .NET-10-Desktop-/ASP.NET-Runtimes.

Paket je lokalni preizkušalnik, ne podpisani prodajalec. Lastne pogoje uporabe in pravice do prodaje SDK so ločene; glej obstoječo dokumentacijo pod `docs/licensing`.
