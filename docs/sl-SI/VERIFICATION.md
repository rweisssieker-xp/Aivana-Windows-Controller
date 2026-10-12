<!-- generated-doc locale=sl-SI source=docs/VERIFICATION.md sha256=f19afb9e7b5ffac90c83167416ab3af0964a4481dd16b7357f8585480c6aae65 -->

> Strojno podprt prevod. Struktura, primeri kode in povezave se preverjajo; to ne pomeni pregleda maternega govorca ali pravne odobritve. Zgodovinska poročila ohranijo izvirne datume in obseg. [Izvirni vir](../VERIFICATION.md)

# Preizkus – 8. Oktobra 2026

[English (US)](../en-US/VERIFICATION.md) | [Deutsch](../de-DE/VERIFICATION.md) | [Français](../fr-FR/VERIFICATION.md) | [Italiano](../it-IT/VERIFICATION.md)

Aplikacija je bila zgrajena pod Windows x64 z stabilnim .NET SDK 10.0.202 in objavljena kot samostojna Windows-x64-Ausgabe.

## Avtomatizirano preverjeno

- Celotna gradnja: nobenih opozoril in napak.
- 19 Core vedenjske teste: standardna konfiguracija, validacija, nič / podvojni podatki, profilna prednost, uvoz / izvoz in obnovitev varnosti, makro zaporedje in ponovitev, prekinitev, dvakratno izločanje, serijaliziranje, nespremenjeni izvedbeni snapshot in napake.
- Windows preizkušnja: Hotkey standardizacija in alias imena, native strukturne velikosti, realna tipkovnica seznama naprav, varno registracijo in ponovitev, sintetično WM_HOTKEY za klop in razpad, deaktivirano registracijo klop med simuliranjem.
- Neodvisna EXE se je začela z `--smoke-test`: vse pet WPF pogledov se vrže, zapise se lahko namestijo/kopirajo/izklopijo, zasnove ostanejo ob razsvetljavi, konfiguracija se shrani in ponovno naloži. Ta test uporablja izoliran začasni seznam nastavitev in ne izvaja besedilnih ali medijskih dejanj.
- Dashboard, urednik in kompakten prikaz (900 × 720) so bili vizualno preizkušeni kot resnični WPF. Pregledano je shranjevanje, zavrnitev, zaporedje škatle/makro, nemška alijada, posamezne makro razpoke in napredek.

## Hardware odvisne meje

Zadnji lokalni prehod je zaznal štiri Windows tipkovnice. Ta številka opisuje vmesnike HID/Windows, ne pa nujno štiri fizične tipke.

Uradni iCUE SDK je nameščen in se nahaja ob EXE. Živ test se poveže z iCUE in zazna tipkovnico Corsair z informacijami o modelu in LED. Spremembe razsvetljave niso bile samodejno sprožene; odvisne tipke in drugi proizvajalci niso testirali strojne opreme. Generalni hotkeys se uporabljajo med tipkovnicami.

Vnosi besedila/medij niso bili injicirani v druge odprte uporabniške programe. Nativne podatkovne strukture in logika napak so bile preučene; vedenje v določenem ciljnem programu je med drugim odvisno od ravni pooblastila.

## Ponovite

```powershell
dotnet build Aivana.slnx -c Release
dotnet run --project tests/Aivana.Tests -c Release
dotnet run --project tests/Aivana.Windows.Tests -c Release
dotnet publish src/Aivana.Desktop -c Release -r win-x64 --self-contained true -o artifacts/win-x64
& ./artifacts/win-x64/MAKRIVO.exe --smoke-test
```

GUI testira rezultate in zaslon po `smoke` poleg preverjene EXE. Razmišlja se kot način diagnoze in po zaključku ne začne trajnega ozadja.


## Razširjena kontrola

Dodatno preverjeno: profilne pogoje s procesom in naslovom okna, spremenljivke brez rekursivne ekspanzije, makrovizgled brez izvedbe, nova validacija ukrepa in spodnja združljivost starih datotek JSON. Native pregledi zajemajo ukazni paleto z registracijo, konflikti, WM_HOTKEY dispatch in oddajanje ter čisto razdelitev monitorja in dostop do klipov na potrebo. Test WPF odpre in vrne dejansko paleto, preveri povratne/povratne in priljubljene, preveri čiste zaslone po vrnitvi in uspešno konča proces.

Windows blokade, spremembe v medsebojnem nastavitvah in gibanja oken se med testiranjem niso uporabljale za uporabniške programe. Akcije okna objavijo Windows zahteve asinkron; konkretni ciljni program lahko ima svoje minimalne velikosti ali omejitve. Za zaključek in statistiko izvedbe veljajo trenutni sestanki.


Razširitev pregleda 12.10.2026: 30 Core preizkušnje; Windows-Adapterji, vključno z izpustom med vnosom, zaščitenim zaznavanjem in OBS-Status Mock je bil. WPF Preverjanje sprejemanja/pregledovanja, začetek/stop zapisnika, profilna izmenjava, gesta in simbolični viri.: 27 Katalogi z 967 ključavnice; dokumentacija: 84 Strani v štirih jezikih. lastniki tipke drugih proizvajalcev kot Corsair ni testirano na strojni opremi; splošna podpora vključuje standardne dogodke F13–F24 in medija. prenos v oblaku poteka prek izbranega sinhroniziranega mape; uvoz se preveri in ostane deaktiviran.
