<!-- generated-doc locale=lv-LV source=docs/superpowers/plans/2026-10-08-controller-polish.md sha256=1a076a67eea31048f6305f30a121c474d592451d1ddcd9b46bc339fd8ec068f2 -->

> Mašīntulkošanas atbalstīts tulkojums. Struktūra, koda piemēri un saites tiek pārbaudītas; tas nenozīmē dzimtās valodas lietotāja pārbaudi vai juridisku apstiprinājumu. Vēsturiskie pārskati saglabā sākotnējos datumus un tvērumu. [Sākotnējais avots](../../../superpowers/plans/2026-10-08-controller-polish.md)

# Kontrolieru izmantojamības un spēju uzlabošanas plāns

[English (US)](2026-10-08-controller-polish.md) | [Deutsch](2026-10-08-controller-polish.md) | [Français](2026-10-08-controller-polish.md) | [Italiano](2026-10-08-controller-polish.md)

Lietotāja pieprasījums: uzlabojas izskats, lietderība un funkcionalitāte; iepriekšēja atļauja prasa maksimāli noderīgus apakšuzņēmumus par nepieciešamajiem mazākajiem modeļiem un autonomu pabeigšanu.

## Apjoms
Saglabājiet esošos profilus un darbojas Corsair SDK integrācija. Izlabot esošo WPF pieteikumu, nevis radot atsevišķu prototipu.

- [x] Shell/dashboard darbinieks: reaģējošs izkārtojums, aktīva navigācija, lasāmie kontroles, kompakta aktivitāte/statuss, traukšanas un izkraušanas tīkls, noderīgi tukši apstākļi un veidnes. MainWindow.xaml, mainWindow, xaml.cs un mainWindOW.Dashboard.cs.
- [x] Editor darbinieks: MainWindow.Editor.cs un opcionāls redaktors - tikai daļēji faili. Izskaidrojiet stacionārus rediģējumus, saglabājiet/izslēdzat, validējiet inline, īpašu makro sarakstu ar drag-and-drop, duplikāciju soļus, uzticamu atslēgu un failu/URL laukumus.
- [x] Windows darbinieki: dzimtas validācija, spēcīgi ierīces nosaukumi un SDK iespējas, ierobežoti modifikatori un droša injekcija; īpašs Windows un Windows tests.
- [x] Vecāki: kodola izpildes progresa, kontroliera saskarnes, konfigurācijas saglabāšana, integrācijas testa, faktiski renderētas ekrāna attēli, publicēšana/paketēšana un darbojas lietojuma aizstāšana.

## Interfeisa līgums
Pārvaldītājs nodrošina esošos APIs plus sloksnes LastStatus, bool HasRunningActions, bool IsRunning(Guid), sloksnes GetTileStatus(Guid)、 sloksnes ForegroundProcess, bool CorsairAvailable, IReadOnlyList<CorsairKeyboardInfo> CorsairKayboards, Task TestCorsairColorAsync(string), void OpenIcue(). Redaktors izpaužas bool HasUnsavedChanges, void DiscardDraft(), bool CommitDraft(); pastāvošie lauka/metodes nosaukumi paliek pieejami visās daļās.

## Pieņemšana
Neatjauno reālo lietotāja konfigurāciju; spēkā esošās izmaiņas glabājas vienu reizi, nevēlami projekti paliek rediģējami, atgriežas glabātie dati. Native hotkey aliases apstiprināts bez injekcijas testa ievades neskartajiem logu. Tile un makro reorder pārbaudīts; visi skatījumi rādīt desktop izmērā; darbojas / nepilnīgs / pilnīgs stāvoklis redzams; parādīts faktiskais SDK ierīces modelis. Izveidojiet/pārbaudiet un publicējiet smēķēšanas pastu, un tiek uzsākta atjaunināta pieteikšanās.
