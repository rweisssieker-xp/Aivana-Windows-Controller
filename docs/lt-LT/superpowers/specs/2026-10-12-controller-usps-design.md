<!-- generated-doc locale=lt-LT source=docs/superpowers/specs/2026-10-12-controller-usps-design.md sha256=4c3b2dbedb83af6bd48917515ae6559012afb6318ce69d1e32fb2765e0793327 -->

> Mašininio vertimo pagalba parengtas vertimas. Tikrinama struktūra, kodo pavyzdžiai ir nuorodos; tai nereiškia gimtakalbio peržiūros ar teisinio patvirtinimo. Istorinės ataskaitos išlaiko pradines datas ir apimtį. [Originalus šaltinis](../../../superpowers/specs/2026-10-12-controller-usps-design.md)

# MAKRIVO kontrolės pratęsimai

Vartotojas paprašė visų devynių pasiūlytų patobulinimų ir pripažįstamo tray ikono. Esamos konfigūracijos, ypač G1/G2 ryškumas, turi likti suderinamos. Esantys nekontroliuojami prekės ženklai ir dokumentacijos leidiniai priklauso bendram darbo erdvei ir turi būti išsaugoti.

## Siūlymai ir ribos

1. Ryškumo veiksmai pasirinkti visus palaikomus monitorius arba stabilų konkrečių monitorių ID. Aiškūs tikslai akivaizdžiai nesibaigia. Duomenų plokštė rodo dabartinį ryškumą, išleidimo tūrį, mikrofoną ir OBS būklę per apribotas asimetrines skaitytuves.
2. Specialūs raktai palaiko nedelsiant spausdinti, įtempti, laikyti ir dvigubai, su pakartotiniu slėpimu. Laikui bėgant laikomas sluoksnis keičia aktyvią žemėlapį ir išleidžia jį atkurti. Dabartiniai ryšiai išlaiko savo elgesį. Pertrauka, profilio pokyčiai, išjungimas ir atšaukimas aiškiai laukiančių gestų.
3. Darbo srauto dizaineris redaguoja sąlygas, išankstines šakas ir sustabdyti / tęsti nesėkmę politiką sandoriškai lentelėje. Sąlygos apima failus, paleidimo procesus ir langų pavadinimus. Nėra neribotų šuolių.
4. Aiškus įrašymas užfiksuoja trumpalaikius kodus ir laukti, ignoruoja įvedimą ir sustoja pabėgti. Įregistruoti veiksmai persvarstomi prieš priimant; įprastas tekstas nėra įrašytas.
5. Įrenginio standartiniai specialūs raktai naudojami kitiems gamintojams Windows Raw Input. Savininkų raktams reikalingas palaikomas tiekėjo API arba tiekėjo žemėlapis į standartinį raktą; ne palaikoma funkcionalumas yra sąžiningai parodyta. Diagnozė paaiškina profilį, pertrauką, privalomus konfliktus ir tikrą adapterio būklę.
6. Profilų mainai naudoja naudotojo pasirinktą bendrą aplanką (įskaitant aplankus, kuriuos sinchronizuoja esamas debesys). Audito patikrinimai užkerta kelią tyliai perrašyti. Pull prognozuoja pokyčius ir importo neįgalius veiksmus, kai pašalinami aparatūros žemėlapiai. Veiksmo turinys yra aiškus. Nėra debesų pažymėjimų, kurie būtų saugomi MAKRIVO.
7. UI juostelės priklauso esamiems 27 kalbų katalogams; DE/EN/FR/IT lieka mažiausiomis peržiūrėtos dokumentacijos kalbomis. Vertimo trūkumas turi būti atskleistas, o ne reikalaujamas.
8. Pripažįstamas M klaviatūros ženklas pakeičia generinę Windows tray ikoną. Paukščių režimas turi amber pauzę. „Executable/window branding“ naudoja tą patį ženklą. Įrankis apima statusą ir profilį.

## Tikrinimas

Pagrindiniai patikrinimai apima atgalinę suderinamumą, atspaudus, atšaukimą, elgesio laiką, darbo srauto sklypus / klaidas, įrašų filtravimo ir sinchronizavimo konfliktus. Windows patikrinimai aprėpia skaityti tik ekrano sąrašą, SDK išleidimo įvykius ir Raw Input pars. WPF dūmų patikrinimai padengia naujus įėjimo taškus ir išteklius be realaus monitoriaus ryškumo keitimo, spausdinti į kitą programą ar perrašyti naudotojo profilius. Įdiegimas išsaugo atsarginę kopiją ir G1/G2 konfigūraciją.
