<!-- generated-doc locale=lt-LT source=docs/testing/2026-10-12-github-publication.md sha256=ab4d40cd12047e2cdacad359a12febe16d5689a82f4f792b484b3d4e2430e02a -->

> Mašininio vertimo pagalba parengtas vertimas. Tikrinama struktūra, kodo pavyzdžiai ir nuorodos; tai nereiškia gimtakalbio peržiūros ar teisinio patvirtinimo. Istorinės ataskaitos išlaiko pradines datas ir apimtį. [Originalus šaltinis](../../testing/2026-10-12-github-publication.md)

# GitHub-Paskelbimas MAKRIVO-vystymosi lygis

Standas: 12. Spalio mėn. 2026. šaltinio kodas, ne pasirašytas pardavimo leidimas.

- Išleidimo struktūra: sėkmingas, nulinis įspėjimai ir klaidos.
- Pagrindiniai bandymai: sėkmingai
- WPF smoke testas: sėkmingas; nauja rezultato ataskaita vietiniame statybininkas.
- Pirmasis pilnas Windows rinkinys: priekiniai langai, garso, ekrano ir iCUE bandymai sėkmingai; tada „Timeout“ su „UI“ automatizavimo darbuotojais.
- Tikslus atkūrimas `--extensions-only`: sėkmingai, įskaitant UI automatizavimą, daugybę tikslų, slaptažodžio išjungimą, „plugin“ procesą ir tikrą HTTPS mobilųjį hostą. Pradinis laikas nebuvo paslėptas laiko apribojimo pakeitimu; priežastis dar nėra galutinai įrodyta.
- Vėliau visiškas Windows atkūrimo kursas: sėkmingai, įskaitant priekinius langus ir visus išplėtimo bandymus. Sėkmingas kartojimas neapima sporadinių laiko.
- Prieš publikavimą patikrintoje šaltinio sąraše nebuvo rastų tipiškų privatų arba produktyvių Stripe/GitHub/AWS raktų. Šis ribotas patikrinimas nėra visiškas paslaptis.
- Pastatų faktų ir vietos orkestravimo duomenys nėra paskelbti.

Veiksmų apimtis ir likusios nuolaidos: [Featurų sąrašas](../FEATURES.md). ir Ankstesni patikrinimai išlaiko savo galiojimą; jie nėra garantija bet kokiam aparatinei įrangai ar kiekvienam darbalaukio kontekste.
