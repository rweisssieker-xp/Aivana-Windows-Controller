<!-- generated-doc locale=lt-LT source=docs/superpowers/plans/2026-10-08-controller-polish.md sha256=1a076a67eea31048f6305f30a121c474d592451d1ddcd9b46bc339fd8ec068f2 -->

> Mašininio vertimo pagalba parengtas vertimas. Tikrinama struktūra, kodo pavyzdžiai ir nuorodos; tai nereiškia gimtakalbio peržiūros ar teisinio patvirtinimo. Istorinės ataskaitos išlaiko pradines datas ir apimtį. [Originalus šaltinis](../../../superpowers/plans/2026-10-08-controller-polish.md)

# Valdymo gebėjimų ir gebėjimų tobulinimo planas

[English (US)](2026-10-08-controller-polish.md) | [Deutsch](2026-10-08-controller-polish.md) | [Français](2026-10-08-controller-polish.md) | [Italiano](2026-10-08-controller-polish.md)

Naudotojo prašymas: pagerinti išvaizdą, naudojamumą ir funkcionalumą; išankstinis leidimas reikalauja maksimaliai naudingų elementų, reikalingų mažesniems modeliams ir savarankiškai užbaigti.

## apimtis
Išsaugokite esamus profilius ir dirbkite Corsair SDK integraciją. Pagerinkite esamą WPF programą, o ne sukurkite atskirą prototipą.

- [x] Shell/dashboard darbuotojas: atsakingas išdėstymas, aktyvus navigacija, skaityti kontrolės, kompaktiškas veikla / būklė, traukimo ir nuotėkio plokštelės užsakymas, naudingos tuščios sąlygos ir šablonai. PagrindinisWindow.xaml, MainWindow, xaml.cs ir MainWindov.Dashboard.cs
- [x] Redaktorius: MainWindow.Editor.cs ir pasirinktas redaktorius - tik daliniai failai. Išsamūs laipsnių redagavimas, išsaugoti / atsisakyti, patvirtinimo sąrašas, dedikuotas makro sąrašas su traukiniu ir nukreipimu, dvigubai žingsniai, patikimas raktas ir failų / URL laukai.
- [x] Windows darbuotojai: gimtoji patvirtinimas, tvirtas prietaiso pavadinimas ir SDK pajėgumai, ribotos modifikatoriaus laidos ir saugi injekcija; savarankiškai Windows ir Windows bandymai.
- [x] Tėvas: pagrindinė vykdymo pažanga, valdytojo sąsajos, konfigūracijos išsaugojimas, integravimo bandymai, faktiniai atnaujinti ekranai, leidimas / pakuotė ir paleidimo programos pakeitimas.

## sąsajos sutartis
Kontrolierius teikia esamas APIs plius eilutę LastStatus, bull HasRunningActions, bull IsRunning(Guid), bull GetTileStatus(Guid) eilutę ForegroundProcess, bull CorsairAvailable, IReadOnlyList<CorsairKeyboardInfo> CorsairKayboards, Task TestCorsairColorAsync(string), void OpenIcue(). Redaktorius atskleidžia „HasUnsavedChanges“, „DiscardDraft“ nebuvimą, „CommitDraft“; esami lauko/metodo pavadinimai lieka prieinami visose dalinėse klasėse.

## Priėmimas
Nėra realios naudotojo konfigūracijos; galiojantys etapo pakeitimai išsaugoti vieną kartą, negaliojančių projektų likti redaguojami, atsisakyti atkurti išsaugotus duomenis. Native hotkey aliases patvirtintas be injekcijos bandymų į ne susijusius langus. Tile ir makro reorder išbandytas; visi vaizdai rodomi darbalaukio dydžiu; veikia / neveikia / visi būsenos matomos; realus SDK įrenginio modelis rodomas. Build/tests ir paskelbtas dūmų pasas, ir atnaujinta programa yra pradėta.
