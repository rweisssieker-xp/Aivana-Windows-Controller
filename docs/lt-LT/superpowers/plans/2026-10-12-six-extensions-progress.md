<!-- generated-doc locale=lt-LT source=docs/superpowers/plans/2026-10-12-six-extensions-progress.md sha256=aaa5db1c77f8f730220146a230868b9df95d87653b48708f49aa1418db56fa5d -->

> Mašininio vertimo pagalba parengtas vertimas. Tikrinama struktūra, kodo pavyzdžiai ir nuorodos; tai nereiškia gimtakalbio peržiūros ar teisinio patvirtinimo. Istorinės ataskaitos išlaiko pradines datas ir apimtį. [Originalus šaltinis](../../../superpowers/plans/2026-10-12-six-extensions-progress.md)

# Įgyvendinimo ledgeris: 2026-10-12 - šešios išplėstys
Naudotojas įgaliotas gimtojo įgyvendinimo be papildomų klausimų.
Rinkimas: naudoti naują kodeksą / šešių išplėtimų filialą dabartiniame patikrinime, kad būtų išsaugotos esamos nekontroliuojamos funkcijos; šviežias darbas nebūtų priklausomybės. Nėra jokių įrengimo ar vairuotojo pokyčių bandymų metu.
Išankstinis skrydis: patikrinimas maitinamas burtininkas / UIA / papildas / mobiliųjų rezultatų peržiūros; gamintojas gali maitinant klaviatūrą ir vairuotojo tašką. Esamos numerinės enum vertės turi likti stabilios.
Užduotis 1: progresuojantis; patvirtinimo testas buvo įvestas.

Užduotys 1–3: patikrinimo sutartys / teikėjas / redaktorius / istorija ir burtininkas įgyvendintas; pagrindiniai patikrinimai praeina; UI priėmimas vis dar plečiasi.
Užduotys 4–5: izoliuotas UIA darbuotojas, pikeris ir peržiūrėtas spustelėjimo įrašas įgyvendintas; realios fiksacijos bandymai praėjo invoke / ambiguity / disabled / slaptažodžio pašalinimas. Rasti / fiksuotas procesinis pavadinimas truncation.
Užduotys 6–7: pakuotės peržiūra / importas / neįgalus / eksportuoti / išjungti ir izoliuotas vykdomas IPC; tikrasis pavyzdys IPC praeina. Jokių viešųjų rinkų.
Užduotys 8–9: HTTPS plokštė, susieti, paslėptas autentiškumas, apsaugotas TLS raktas ir leidžiama IPC įgyvendinti; HTTP / mobilus patikrinimas laukiamas.
Užduotys 10–11: fiziniai skenavimo kodai, Logitech G SDK ir Wooting analoginiai adapteriai ir RGB protokolai įgyvendinti; joks suderinamas tiekėjas aparatūra išbandyta už esamą Corsair.
Užduotis 12: išorinis vartas lieka: gamybos pasirašytas/licencingas Windows 11 išskirtinis filtro nėra. Nereikia pakeisti klaidingos slėpties.
Veiksmai: sukurkite kompanijas naudojant MSBuild tikslus; paskelbkite visus hostus kartu, kad UIA / mobiliųjų įrenginių naudojimas būtų prieinamas iš programos.
Valdymas: nėra savarankiškai paskirstytos SDK DLL sujungtos be patvirtintų teisių; aiškiai vietos SDK patvirtinimo hashas pasirinktas binarus.

Galutinė peržiūra: nauja peržiūra /root/final_review peržiūrėjo dabartinį darbo medį, planą/specą ir išleidimo duris. Nėra revizijos.
Galutinis: fiksuotas vėlyvas mobilusis siuntėjas, taikydamas prašymo atšaukimą siuntimo ir grįžimo įvesties.
Galutinis: fiksuotas trūkstamas vykdytojo neveikimas / atšaukimas patikrinimo terminalo būklė; vykdymo neveikimas regresijos RED→GREEN; išsaugotas išankstinis laikas.
Galutinis: fiksuotas bazinis tikrinimas, palyginus absoliučią pagrindinį modulį ar pranešimą neišspręstą.
Galutinis: fiksuotas įkroviklis su atkūrimo / diskardas valdymo; naujas API bandymas kompiliuoti RED→GREEN; panaikinamas aktyvių darbuotojų; trūkstamos priklausomybės atsisakyti skambinti.
Galutinis: fiksuotas širdies smūgis atnaujinamas nesėkmingoje Razer / GameSense sesijoje; nėra fizinio RGB prietaiso.
Galutinis: pridėtas pardavėjo elgesys / sluoksnio konflikto patvirtinimas, bandymas kompiliuoti RED→GREEN.
Galutinis: tikras HTTPS RED→GREEN: Šanelis atmeta ephemeral serverio raktus; naudojant laikiną vartotojo raktą ir DPAPI apsaugotą nuolatinį PFX, nulinio skundo valdomą PFX.
Galutinis: UIA veiksmų biudžetas apima ribotą darbuotojų pradžios išmoką; darbuotojas visada mirė dėl nenormalaus rezultato.
Galutinis: DDC skaityti pereinamojo nesėkmės atkūrimas (švarus vaizdas tada stabilus tas pats monitorius ID); ribotas skaityti atgal praeina originalią stabilų sąrašo testą be rašymo ryškumą.
Užduotis 13: išleidimas pastatas 0 įspėjimai / klaidos; pagrindinė suite žalia; Windows suite žalia su aiškiu -skip-foreground-fixture; originalus pilnas suite turi gimtojo foreground aktyvacijos gedimą, išsaugotas. Tikrasis UIA, IPC ir HTTPS įtaisas tikrina žalią. WPF dūmų žalias. Edge/Playwright telefonai ir planšetiniai įrenginiai yra žalios su API, tikroji HTTPS atskirai žalia. Pilnas savarankiškas paketas, kurį sukuria įrankiai / publish-complete.ps1 su svetainėmis / turtais / pavyzdžiais / paleidimo pranešimais.
Veiksmas: jokių mišrių, esamų naudotojo leidinių suderinimo/komiteto. Palikite pokyčius ir sklypą vietoje. Nėra užbaigimo meniu, nes vartotojas paprašė jokių klausimų ir visi išleidimo kriterijai neatitinka.
Veiksmas 12 nepilnas: pasirašytas / licencijuotas gamybos filtras ir atkurti dviejų klaviatūrų aparatūros bandymai nėra prieinami. Nėra vairuotojo. Nėra reikalaujamo išskirtinio sustabdymo.
Valdymas: aparatūros priėmimas, realus mobilusis LAN priėmimas 150/200 ir visi nauji vertimai lieka nepaaiškinami; išsami atskleidimo dokumentai / bandymai/2026-10-12-6-extensions.md. Apskritai prašymas nėra visiškai priimtas, nepaisant darbo programinės įrangos priedų.
