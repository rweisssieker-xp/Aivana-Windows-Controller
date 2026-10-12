<!-- generated-doc locale=lv-LV source=docs/superpowers/plans/2026-10-08-windows-controller.md sha256=10a5ba49bbefc617bce40563c668e3c4628647822117087deba766713fb9b3af -->

> Mašīntulkošanas atbalstīts tulkojums. Struktūra, koda piemēri un saites tiek pārbaudītas; tas nenozīmē dzimtās valodas lietotāja pārbaudi vai juridisku apstiprinājumu. Vēsturiskie pārskati saglabā sākotnējos datumus un tvērumu. [Sākotnējais avots](../../../superpowers/plans/2026-10-08-windows-controller.md)

# Windows Kontroliera īstenošanas plāns

[English (US)](2026-10-08-windows-controller.md) | [Deutsch](2026-10-08-windows-controller.md) | [Français](2026-10-08-windows-controller.md) | [Italiano](2026-10-08-windows-controller.md)

> Darbiniekiem: izmantojiet paralēli izolētu īpašumu; lietotājs skaidri pieprasīja pilnīgu īstenošanu ar maksimāli noderīgiem apakšsistēmu.

**Mērķis: ** Iekšzemes Windows atslēgas paneļa piegāde apstiprinātajā dizainā.
**Arhitektūra:** Galvenā konfigurācija un atcelta izpilde; Windows Adapteris; WPF Desktop kompozīcija.
**Tehniskā stacija:** .tīrs 10, WPF, Win32, JSON. bez maksas atkarības.
**Spec:** ../specs/2026-10-08-Windows kontrolieris dizains.md

## Globālie ierobežojumi
Vācijas lietotāja; vietējā uzglabāšana; Windows x64; patiesa aparatūras spējas statuss; nav tastatūras ierakstīšanas; nav privātās automātisko startu.

## Pārskats Focus
Nepareizs imports nedrīkst aizstāt saglabātos iestatījumus; turētos modifikatorus nedrīkst piesārņot izrakstu; nepluggts aparatūra nedrīkst sabojāt; slēgšana nedrīkst glabāt pieejamību; atceltie makro resursi ir jāatbrīvo.

## Uzdevumi un īpašumtiesības
- [x] Core aģents: src/Aivana.Core un testi/Aivana.Testi Modeli, JSON izturība, validācija, profila izvēle un MacroRunner. Veikt neveiksmīgas uzvedības testus un pēc tam īstenot un veikt visus pamattestus.
- [X] Windows Aģentūra : src/Aivana.Windows. RegisterHotKey, ierīces inventārs, SendInput, priekšējā process un opcionāls Corsair apgaismojuma adapteris. pārbaudiet dzimto struktūru izmēru un reģistrācijas tīrīšanu ar drošu smēķēšanas testu.
- [x] GUI aģents: src/Aivana.Desktop/MainWindow.xaml un .cs plus UI palīgs tikai. Dark dashboard, pilns redaktors, profili, iestatījumi, tastatūras pārskats un makro redaktors. Izveidojiet līgumus, kad pastāv.
- [x] Vecāki: kopīgie līgumi, projekta faili, DesktopController sastāvs, pieteikumu/tray dzīves cikls, imports/eksports dialogs, dokumentācija, integrācijas pārbaudes, publicēšana un darbība-applikācijas smēķēšanas tests.

## Kopīgie saskarnes
Līgumi dzīvo Core/Models.cs. Pārvaldītāja ieviešana dzīvo Desktop/DesktopController.cs. UI sauc kontrolieru metodes caur šo konkrēto klases. Aģenti saņem precīzus parakstus savās uzdevumos. Katram ir atsevišķas filmas. Vecāki atrisina pārrobežu projektu neveiksmes pēc visiem aģentiem.

## Pārbaude un piegāde
- [x] dotnet build Aivana.slnx -c Izlaišana: nulles kļūdas.
- [x] dotnet run --project tests/Aivana.Tests -c Izlaišana: uzvedības pārbaudes.
- [x] dotnet publika src/Aivana.Desktop -c Release -r win-x64 --self-contained true -o artifacts/win-x64.
- [x] Publicēts izpildāms, pārbauda Windows UI un procesa stabilitāti, īsteno drošu profilu / rediģēšanas / pastāvības plūsmu.
- [x] Rakstīt README ar būvniecības/start instrukcijām un aparatūras ierobežojumiem; ierakstu pārbaudes un atlikušās aparatūras testus.
