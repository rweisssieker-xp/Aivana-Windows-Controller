<!-- generated-doc locale=lt-LT source=docs/superpowers/plans/2026-10-12-controller-usps.md sha256=b7af0d36209e04b458ce64a5db78ab7e552a84c5e9b450ea0bd97179a4eb7f68 -->

> Mašininio vertimo pagalba parengtas vertimas. Tikrinama struktūra, kodo pavyzdžiai ir nuorodos; tai nereiškia gimtakalbio peržiūros ar teisinio patvirtinimo. Istorinės ataskaitos išlaiko pradines datas ir apimtį. [Originalus šaltinis](../../../superpowers/plans/2026-10-12-controller-usps.md)

# Kontrolės pratęsimo planas

**Tikslas: ** Įdiegti prašomą monitorą, gestą, darbo srautą, įrašymą, prietaiso diagnozę, nešiojamą profilio mainus ir trajaus patobulinimus.

**Architektūra: ** Išplėskite esamus pagrindinius / Windows / darbalaukio sluoksnius. Švarūs statiniai mašinos ir nešiojama konfigūracijos logika gyvena pagrindu, gimtoji integracija Windows ir modaliniai dizaino redaktoriai darbalaukėje. Išsaugokite esamas purvinas darbo vietas ir įdiegtą naudotojo konfigūraciją.

**Spec: ** ../specs/2026-10-12-kontroliatorius-usps-design.md

- [x] stebėjimo paslauga, tikslinė tęstinumas ir nesinkronus gyvenamasis būdas; bandymas neįmanoma ID ir atšaukimas.
- [x] Gestavimas būsenos mašina ir laikomi sluoksniai; bandymų ribos, kartojimas, atkūrimas ir esami nedelsiant susieti.
- [x] Darbo srauto sąlygos, išankstiniai sklypai ir klaidos politika; bandymų nuotraukos ir atšaukimas.
- [x] Išsamus trumpalaikis įrašymas su projekto peržiūra; bandymų filtravimas ir šaknis.
- [x] Pasidalijamas failas keičiasi profiliu su peržiūros patikrinimais ir išankstinėmis nuomonėmis; bandymai ir nešiojamieji žemėlapiai.
- [x] Generinis kiekvieno įrenginio specialus raktas ir diagnozė; bandymų parsinga, duplikatų žemėlapiai ir pertraukos elgesys.
- [x] Paieškos / langas / vykdomas ikono ištekliai ir pertrauktas išvaizda; patikrinti išteklius paskelbta vykdomas.
- [x] integruoti visus paletės / redaktoriaus įėjimo taškus ir lokalizuotą tekstą; paleisti Core, Windows ir WPF patikrinimus.
- [x] Paskelbkite atskirame etapo kataloge, išsaugokite naudotojo nustatymus ir atnaujinkite programą.
