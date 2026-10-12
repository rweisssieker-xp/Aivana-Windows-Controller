<!-- generated-doc locale=lv-LV source=docs/MAINTENANCE.md sha256=9bf5875b50386e698c0574eee390f790d4b1c29cb11288c856c98e03a05877f7 -->

> Mašīntulkošanas atbalstīts tulkojums. Struktūra, koda piemēri un saites tiek pārbaudītas; tas nenozīmē dzimtās valodas lietotāja pārbaudi vai juridisku apstiprinājumu. Vēsturiskie pārskati saglabā sākotnējos datumus un tvērumu. [Sākotnējais avots](../MAINTENANCE.md)

# Visu dokumentācijas valodu saglabāšana

Dokumentācija seko visiem 27 UI valodas. ģenerētie faili izmanto reģionālos kodus; Norvēģijas kartes `nb-NO` Latīņamerikāņu valodā `sr-Latn`. Katra mape satur indeksu, rokasgrāmatu, detalizētu rīcības atsauci, funkciju inventāru, verifikācijas piezīmes, pētījumus, tostarp CSV, vēsturiskos plānus un licencēšanas analīzi.

## Patiesības avots

Redzēt sakni `README.md`, `docs/HANDBOOK.md`, `docs/FEATURES.md`, `docs/VERIFICATION.md`, Šī lapa vai oriģinālās failas `docs/research`, `docs/testing`, `docs/superpowers` un `docs/licensing`. Neuzrakstīt vēsturisko ziņojumu, lai tas apliecinātu vēlāku versiju. saglabāt īstenošanu, simulāciju, reālu ierīču pieņemšanu un komerciālo apstiprinājumu atšķirīgi.

## Izveido un validē

```powershell
python tools/localize_documentation.py --refresh-sources
python tools/check_documentation.py
```

Opcionālais tiešsaistes ģenerators nosūta dokumentāciju uz Google publisko tulkojumu galamērķi, ievērojot esošo lokalizācijas rīku. Tas neslasīs lietotāju konfigurāciju, noslēpumus vai modeļu failus. Izveidojiet to tikai ar atbilstošu dokumentu šim pakalpojumam. Koda bloki, inline kodi, URL, tabulas struktūra un tehniskie identifikatori ir aizsargāti. Tulkošanas cache un iesaldēts avots tiek glabāti zem ignorēta `artifacts/documentation-translation`.

Ja tiešsaistes pakalpojumu sniedzējs ir ierobežots, `tools/translate_documentation_local.py` aizpilda to pašu cache, izmantojot skaidri piegādātu vietējo M2M100 CTranslate2 Modelis – tas 2026-10-12 Snapshot izmanto MIT licencēto facebook/m2m100_418M Modelis, kas pārveidots kā JustFrederik/m2m_100_418m_ct2_int8 Pārskatīšanas laikā `1aeed44db4dd61a486bba44acf54c76507082a2c`. CTranslate2 4.8.2 un `SentencePiece` 0.2.2 nav veidotas vienīgi rīkus. nav modeļa svaru vai tulkošanas atkarības tiek nosūtītas ar MAKRIVO. Izveidojiet ģeneratoru ar `--cache-only` pēc tam, kad ir aizpildīts cache, lai sniegtu bez tīkla pieprasījumiem. vietējais palīgs netiek lejupielādēts vai izpildīts attālināti modeļa kods.

```powershell
python -m pip install ctranslate2==4.8.2 sentencepiece==0.2.2
python tools/translate_documentation_local.py --model C:\models\m2m100-ct2 --refresh-sources
python tools/localize_documentation.py --cache-only
python tools/check_documentation.py
```

Bez `--refresh-sources`, runā atkārto savu iesaldēto snapshot. Izmantojiet atdzesēšanas opciju pēc oriģināla rediģēšanas. Atkārtoti tiek izmantotas slēptās segmenti. Daļēja braukšana var izvēlēties lokālos kodus, izmantojot `--locales`; visu lokālo pirms publicēšanas. Pārtrauktas runas nepārstē nepārliecinātu prozu ar neveiksmīgu tulkojumu.

Manifest ieraksta katru avotu un ģenerēto failu ar SHA-256 pirkstu nospiedumiem normalizētā UTF-8 teksta: LF līnijas beigām un bez bitu pasūtījuma. Āfrikas pārbaude pārbauda pilnīgu pārskatu par valodu, pirkstu nospiedumus, vietējos saites, koda paraugus, tabulu/CSV struktūru, avotu URL un neatgriezeniskos vietas turētājus. Tas noraida stūres avotus. Piegādātais strukturālais pārskats nav izcelsmes runātāja pārskats vai juridiskais apstiprinājums. Izveidotas lapas šo kvalifikāciju redzami.

## Publicēšana un iepakojumi

Publicējiet dokumentāciju, ģeneratoru un pārbaudi kopā. Saglabājiet oriģinālos trešās puses licences noteikumus; tulkotais licences analīze ir informatīva. Ēkas, tulkošanas cache un vietējās orķestras failus nepieder Git. Saraksta dokumentu publicēšana pati par sevi nepublicē Windows instalatoru.

Lietotāja aplēses ir atsevišķas JSON katalogas `src/Aivana.Core/Localization`. Dokumentācijas paaudze šos katalogus neizmanto. Programmas izveide un tā dokumentācija būtu jāatklāj versijā; nesen aprakstītais eksperimentālais darbības laiks var būt iepriekš instalēta pakete.
