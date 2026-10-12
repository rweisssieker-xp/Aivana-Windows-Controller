<!-- generated-doc locale=lv-LV source=docs/superpowers/plans/2026-10-12-controller-usps.md sha256=b7af0d36209e04b458ce64a5db78ab7e552a84c5e9b450ea0bd97179a4eb7f68 -->

> Mašīntulkošanas atbalstīts tulkojums. Struktūra, koda piemēri un saites tiek pārbaudītas; tas nenozīmē dzimtās valodas lietotāja pārbaudi vai juridisku apstiprinājumu. Vēsturiskie pārskati saglabā sākotnējos datumus un tvērumu. [Sākotnējais avots](../../../superpowers/plans/2026-10-12-controller-usps.md)

# Pārvaldītāja paplašināšanas īstenošanas plāns

**Mērķis: ** Ieviest pieprasīto monitoru, gestu, darba plūsmu, ierakstu, ierīces diagnozi, pārnesamu profilu apmaiņu un trajas uzlabojumus.

**Arhitektūra: ** Paplašiniet esošo kodolu/Windows/Desk slāņus. Tīra statusa mašīnas un pārnēsājama konfigurācijas loģika dzīvo Core, oriģinālās integrācijas Windows un modālās dizaina redaktoriem Desktop. Saglabājiet esošos netīrus darba vietas rediģējumus un ievietoto lietotāju konfigurāciju.

**Spēks: ** ../specs/2026-10-12-kontrole-usps-design.md

- [x] Uzraudzības pakalpojums, mērķa izturība un asinkrons dzīvs statuss; pārbaude pieejamiem ID un atcelšana.
- [x] Gesture stāvokļa mašīna un turēšanas slāņi; pārbaudes sloksnes, atkārtoti, atjauno un pastāvīgi tūlītējie savienojumi.
- [x] Darba plūsmas nosacījumi, tālākie rādītāji un kļūdas politika; pārbaudes snapshots un atcelšana.
- [x] Izskaidrots īsziņas ieraksts ar projektu; testa filtrēšana un hokeja izdalīšanās.
- [x] Koplietoti failu profila apmaiņa ar revīzijas pārbaudēm un iepriekšējām pārbaudēm; testēšanas konflikti un pārnēsājami mape.
- [x] Generic per-device standarta īpašie atslēgas un diagnoze; testēšanas pars, duplikācijas un pārtraukuma uzvedība.
- [x] Izveidojiet/izveidojiet ikona resursus un pārtrauktu izskatu; pārbaudiet resursus publicētajā izpildā.
- [x] Integrējiet visas paletes/redītāja ievades punktus un lokalizētu tekstu; izpildīt Core, Windows un WPF pārbaudes.
- [x] Publicējiet atsevišķā stažēšanas direktorijā, saglabājiet lietotāju iestatījumus un atjauninātā lietojumprogramma.
