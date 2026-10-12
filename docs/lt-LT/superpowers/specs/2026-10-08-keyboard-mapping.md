<!-- generated-doc locale=lt-LT source=docs/superpowers/specs/2026-10-08-keyboard-mapping.md sha256=fb766a2a2b4698ae3e1c3f63b50bb4f5c605da3dd56c61c05fcc0832b5cf8aeb -->

> Mašininio vertimo pagalba parengtas vertimas. Tikrinama struktūra, kodo pavyzdžiai ir nuorodos; tai nereiškia gimtakalbio peržiūros ar teisinio patvirtinimo. Istorinės ataskaitos išlaiko pradines datas ir apimtį. [Originalus šaltinis](../../../superpowers/specs/2026-10-08-keyboard-mapping.md)

# Fizinis klaviatūros vaizdas žemėlapyje

[English (US)](2026-10-08-keyboard-mapping.md) | [Deutsch](2026-10-08-keyboard-mapping.md) | [Français](2026-10-08-keyboard-mapping.md) | [Italiano](2026-10-08-keyboard-mapping.md)

## Tikslas

Klaviatūros vaizdas, skiriant veiksmus, turi parodyti faktines klaviatūros pozicijas. Vienodai platų eilutės nepakanka: Enter, Shift, Space, navigacija, numerių blokas ir gamintojo papildomų valdymo elementų skiriasi.

## įgyvendinimas

- Katalogas su atskirais fiziniais ID, geometrija klaviatūroje, spausdintos legendos ir Windows įrašų kodai.
- ISO-DE ir ANSI-US yra visiškas ir TKL; be to, Corsair K100 RGB, kuris buvo pripažintas per iCUE kaip prijungtas modelis.
- ISO Enter su L forma, skaičių blokas su aukštais plus / įvadas mygtukais ir platesnė nulio; navigacija ir laidai savo vietose.
- Išsamiai pasirinkti modelį / aprašymą. USB VID / PID ir modelio pavadinimas nėra kalbos variantas. Generiniai profiliai ir pagal gamintojo dokumentus modeliuojami profiliai yra žinomi skirtingai; nėra milimetrų ar firmware suderinamumo garantijos.
- Skaitmeninis kodas vaizduojamas aktyviame Windows įraše, kad fizinis mygtukas netyčia neperkeltų kito mygtuko, pavyzdžiui, Y/Z keitimo tarp JAV ir DE.
- Priskyrimas atveria projektą esamo veiksmų redaktoriuje. Savo saugojimo, konflikto patikrinimo ir atmetimo mechanizmai išlieka. Klikai nedaro veiksmų.
- Savarankiški G mygtukai, profilio / užrakto mygtukas ir iCUE ratai pateikiami, tačiau be įdiegtos įvesties adapteris nėra išleidžiami kaip įprastas Windows hotkeys. Konfiguruojami gamintojo trumpai išlieka galimas būdas.
- Įrenginiai išlieka pasaulinės Windows klaviatūros; įrangos matymas nereiškia, kad įrenginys yra slopintas arba išskirtinis vienos fizinės klaviatūros įkrovimas.

## Įrodymai

[K100-Handbuch su valdymo elementais](https://assets.corsair.com/image/upload/corsairmedia/sys_master/productcontent/AP_K100_RGB_Mechanical_QSG_AD.pdf), [Corsair ANSI / ISO deklaracija](https://www.corsair.com/au/en/explorer/gamer/keyboards/ansi-vs-iso-whats-the-best-keyboard-layout/).

## Tyrimas

Geometrijos, variantų ir įvedimo kodo testai; statiniai ir esami pagrindiniai / Windows testai; WPF atspindys ir priskyrimas be realaus vartotojo konfigūracijos vykdymo ar pakeitimo. Gamintojo tyrimai įrašo papildomus modelio / SKU reikalavimus, nepriklausomai nuo dabartinio išdėstymo katalogo.
