<!-- generated-doc locale=lv-LV source=docs/testing/2026-10-09-usp-release.md sha256=96a5b23b67032ef07f2f5dd93e9585e7bab0a522e5096ffc9f4d020823c4264e -->

> Mašīntulkošanas atbalstīts tulkojums. Struktūra, koda piemēri un saites tiek pārbaudītas; tas nenozīmē dzimtās valodas lietotāja pārbaudi vai juridisku apstiprinājumu. Vēsturiskie pārskati saglabā sākotnējos datumus un tvērumu. [Sākotnējais avots](../../testing/2026-10-09-usp-release.md)

# Keyboard atbalsts, overlay un izpildes atsauksmes

[English (US)](2026-10-09-usp-release.md) | [Deutsch](2026-10-09-usp-release.md) | [Français](2026-10-09-usp-release.md) | [Italiano](2026-10-09-usp-release.md)

Izveidoti trīs prioritātes uzlabojumi esošā Windows lietojumā:

- Speciālā atslēga varonis: reāls iCUE atbalstīts ierīces/atslēgas inventārs, pagaidu G atslēgas uzņemšana, konfigurācijas panākumu verifikācija, konflikta validācija un neizglabāts rīcības projekts. Iegūtie rollback, queued callback neesošanās un normāli saistītās atjaunošanas seguma kļūdas un dialoga slēgšana.
- Keyboard overlay: fiziskā atsauces ģeometrija, aktīvās profila saites, dzīvo profilu/edit atjaunošana un parastais Windows atslēga. Ievadiet Ctrl+Alt+F1 vai noklikšķiniet uz paletes/slēgvārda lapu. Neaktivizēt, klikšķinot un trūkst no uzdevumu rindas.
- Izpildes atsauksmes: rakstīts Gaida / Running / soli / pabeigti / neveiksmīgi / noraidīti posmi, soli / atkārtošanās progresija un neaktivizējošs toasts. Atsauksmes izslēdz rīcības saturu un izņēmuma ziņojumus. Pēdējā posma atcelšana nevar ziņot par pabeigšanu. Kompletion apraksta izpildi, nevis neatkarīgu OBS valsts pieprasījumu.

Pārbaude ir:

- 24 Galvenās pārbaudes ir pabeigtas, ieskaitot fāzes pasūtījumu, neveiksmes/atceļošanas rezultātus, galīgo posmu atcelšanu, klausītāja izņēmuma izolāciju un privātās darbības vērtību izslēgšanu.
- Atbrīvošanas risinājums ir pabeigts bez brīdinājumiem / kļūdām.
- Windows pārbaudes pabeidza; savienota K100 ziņoja G1–G6 un dzimtas konfigurācija / izlaišana veiksmīgi. Audio galamērķi tika pārbaudīti, nemainot savu stāvokli.
- WPF smēķēšana pabeigta ar visām atsauces pārklājuma izkārtojumiem, burvju/katalogu/konfliktu izturību, dzīvo dzimto uzņemšanas konfigurāciju, tālruņa pārsūtīšanu, stacionārās tālruņa noraidīšanu un pārtraukta saistīšanās atjaunošanu. Native callback ieejas tika simulētas; fiziska cilvēka G-slēga pārbaude paliek manuāla pārbaude.
- Faktiskā priekšējā loga nevienlīdzība tika pārbaudīta, lai izpildītu toast. Overlay mijiedarbības īpašības un show/hide tika pārbaudītas; ekrāna attēli tika sniegti burvim, overlay un toast.
- 27 valodas ietver visas 819 kanoniskās līnijas; ārpus vietas / atslēga validācija ir pabeigta.

Snapshots un pilns GUI rezultāts ir artefaktos/usp-win-x64/smoke. Tests izmanto pagaidu profilu; ražošanas iestatījumi tiek atbalstīti un pārbaudīti uzstādīšanas laikā.
