<!-- generated-doc locale=lv-LV source=docs/superpowers/plans/2026-10-12-six-extensions-progress.md sha256=aaa5db1c77f8f730220146a230868b9df95d87653b48708f49aa1418db56fa5d -->

> Mašīntulkošanas atbalstīts tulkojums. Struktūra, koda piemēri un saites tiek pārbaudītas; tas nenozīmē dzimtās valodas lietotāja pārbaudi vai juridisku apstiprinājumu. Vēsturiskie pārskati saglabā sākotnējos datumus un tvērumu. [Sākotnējais avots](../../../superpowers/plans/2026-10-12-six-extensions-progress.md)

# Izpildes līderis: 2026-10-12 - seši paplašinājumi
Lietotājam atļauts ieviest bez papildu jautājumiem.
Ruling: izmantot jaunas kodeksas / sešas paplašināšanās filiāles pašreizējā pārbaudei, lai saglabātu esošās neatkarīgās funkcijas; jauns darbs izvairītos no atkarības. Testēšanas laikā nav instalācijas vai vadītāja izmaiņu.
Preflight: verifikācija baro burvju/UIA/plugin/mobile rezultātu skatījumu; ražotāja iespējas baro tastatūras mapēšanu un vadītāju backend. Pastāvošajām numeriskām vērtībām jābūt stabilām.
Darbs 1: progresē; neveiksmīgs pārbaudes tests ir ieviests.

Uzdevumi 1–3: verifikācijas līgumi / piegādātājs / vēsture un burvju īstenošana; pamata pārbaudes pabeigta; UI pieņemšana joprojām tiek paplašināta.
Uzdevumi 4–5: izolēts UIA darbinieks, pikers un pārskatītais klikšķu ieraksts īstenots; reālā fiksācijas testi pabeidza invoke / ambiguity / disabled / paroles izslēgšanu. Atradās / fiksēts process nosaukums truncation.
Uzdevumi 6–7: iepakojuma pārskats / imports / izslēgts / eksports / izvietošana un izolēts izpildāms IPC; reāls paraugs IPC pāri. Nav publiskā tirgus.
Uzdevumi 8–9: HTTPS paneļi, pārsniedz, hashed uzticamības, aizsargāts TLS atslēga un atļauj IPC īstenot; HTTP / mobilo verifikāciju.
Uzdevumi 10–11: fiziskie skenēšanas kodi, Logitech G SDK un Wooting analogs un RGB protokoli ir īstenoti; nav saderīgu piegādātāja aparatūru pārbaudīts ārpus esošajiem Corsair.
Uzdevums 12: ārējais vārts paliek: ražošanas parakstīts/licencēts Windows 11 ekskluzīvs filtrs nav pieejams. Neaizstāj nepatiesu apturēšanu.
Ruling: izveidot līdzjutējus, izmantojot MSBuild mērķus; publicēt visus hostus kopā, lai UIA/mobile būtu lietojams no lietojumprogrammas.
Ruling: nav īpašumtiesību pārdalāmās SDK DLL saistīts bez verifikācijas tiesības; skaidri vietējās SDK apstiprināšanas hashes izvēlēto bināro.

Pēdējais pārskats: jaunā pārskatītāja /root/final_review pārskatīja pašreizējo darba koku, plānu/spec un izlaišanas durvis. Neatkarīgi no redakcijas.
Pēdējais: fiksēts aizkavētais mobilo nosūtītājs, piemērojot pieprasījuma atcelšanu nosūtīšanas un atpakaļ ievadīšanā.
Galvenais: fiksēts trūkst izpildītāja neveiksmes/atceļošanas verifikācijas termināla stāvoklis; izpildes neveiksmes regresijas RED→GREEN; iepriekšējā laika režīmā saglabāts.
Galvenais: fiksēts pamata nosaukums - tikai pārbaude, salīdzinot absolūtu MainModule ceļu vai ziņo nepārbaudāmā.
Galvenais: fiksēts plugins dzīves cikls ar atjaunošanu/discard rollback kontroles; jauns API tests kompilē RED→GREEN; atcelšana aktivizē darbiniekus; trūkst atkarības noraida aicinājumu.
Galvenais: fiksēts sirdsdarbs atkārtojas nepilnībā Razer/GameSense sesijās; nav pieejams fizisks RGB ierīces tests.
Galvenais: pievienots piegādātāja spēks / slāņa konflikta validācija, testa kopija RED→GREEN.
Galvenais: reāls HTTPS RED→GREEN: Šanāls noraida ephemeral servera atslēgas; izmantojiet pagaidu lietotāju atslēgu un DPAPI aizsargātu pastāvīgu PFX, nulles pārvaldītu PFX.
Galvenais: UIA rīcības budžets ietver ierobežotu darba ņēmēju atmaksu; darba ņēmēji vienmēr nogalināti par neparastu izdošanu.
Pēdējais: DDC lasīšanas pārejas neveiksme atkārtoti (tīra snapshot tad stabilizēt to pašu monitora ID); ierobežota lasīšanas atpakaļ pāriet sākotnējo stabilu saraksta testu bez rakstīšanas spilgtumu.
Uzdevums 13: atbrīvošanas būvniecība 0 brīdinājumi / kļūdas; galvenais suite zaļā; Windows suite zaļā ar izteiktu -skip-foreground-fixture; oriģinālā pilna suite ir izcelsmes foreground aktivācijas neveiksme, saglabāta. Reāls UIA, IPC un HTTPS plugins pārbauda zaļo. WPF smēķēt zaļā. Edge/Playwright tālrunis un planšetdatoru izkārtojumi ir zaļi ar API mock, reāls HTTPS atsevišķi zaļš. Pilns pašapkalpošanas pakete, ko rada rīki/publish-complete.ps1 ar hosts/acets/example/runtime paziņojumiem.
Ruling: nav apvienot / komiteja sajaukto iepriekš esošo lietotāja rediģēšanu. Atstājiet izmaiņas un filiāles vietā. Nav pabeigšanas izvēlnes, jo lietotājs nav pieprasījis jautājumus un pilnīgi atbrīvošanas kritēriji nav izpildīti.
Ruling: uzdevums 12 nepilnīgs: parakstīts/licencēts ražošanas filtrs un atjaunojami divklavieru aparatūras testi nav pieejami. Autovadītājs nav uzstādīts. Nekādas ekskluzīvas apturēšanas nav.
Ruling: aparatūras pieņemšana, reāla mobilo LAN pieņemšana, augsts DPI 150/200 un pilnīgi jauni tulkojumi paliek neierakstīti; detalizēti atklājumi dok/testing/2026-10-12-six-extensions.md. Vispārējais pieprasījums nav pilnībā pieņemts, neskatoties uz darba programmatūras papildu.
