<!-- generated-doc locale=lt-LT source=docs/testing/2026-10-09-usp-release.md sha256=96a5b23b67032ef07f2f5dd93e9585e7bab0a522e5096ffc9f4d020823c4264e -->

> Mašininio vertimo pagalba parengtas vertimas. Tikrinama struktūra, kodo pavyzdžiai ir nuorodos; tai nereiškia gimtakalbio peržiūros ar teisinio patvirtinimo. Istorinės ataskaitos išlaiko pradines datas ir apimtį. [Originalus šaltinis](../../testing/2026-10-09-usp-release.md)

# Slapukų pagalba, perpildymas ir atsiliepimai

[English (US)](2026-10-09-usp-release.md) | [Deutsch](2026-10-09-usp-release.md) | [Français](2026-10-09-usp-release.md) | [Italiano](2026-10-09-usp-release.md)

Įdiegta trys prioritetiniai patobulinimai esamoje Windows programoje:

- Specialus raktas: reali iCUE palaikoma prietaisas / raktas inventorius, laikinai G raktas užfiksuotas, konfigūracijos sėkmės patikrinimas, konflikto patvirtinimas ir neslėptas veiksmų projektas. Atsisiųsti atgal, atgal skambinti negalią ir įprastai prijungti atkūrimo klaidas ir dialogas uždarymas.
- Klaviatūra: fizinė nuorodos geometrija, aktyvus profiliavimas, gyvas profilis / redagavimas ir įprastas Windows raktas. Laikykite Ctrl+Alt+F1 arba paspauskite paletę / klaviatūros puslapį. Neaktyvinimas, spustelėjimas ir nebuvimas iš užduočių juosto.
- Veiksmo atsiliepimai: laukti / paleisti / žingsnis / baigtas / atšauktas / atmestas etapas, žingsnis ir pakartotinis progresas ir neaktyvinantis taškas. Atsiliepimai išskiria veiksmų turinį ir išimtinį pranešimą. Paskutinio žingsnio atšaukimas negali būti pranešta apie pabaigą. Kompletion apibūdina vykdymą, o ne nepriklausomą OBS valstybės užklausą.

Tikrinimas →

- 24 Pagrindiniai patikrinimai, įskaitant fazės užsakymą, nesėkmės / atšaukimo rezultatus, galutinį laipsnį atšaukimą, klausytojų izoliaciją ir privačių veiksmų vertybių pašalinimą.
- Išleidimo sprendimas pastatytas be įspėjimų / klaidų.
- Windows patikrinimai praėjo; prisijungę K100 pranešė G1–G6 ir gimtoji konfigūracija / išleidimas sėkmingai. Audio galutiniai taškai buvo patikrinami, nepakeičiant jų būklės.
- WPF dūmai praėjo su visais nuorodų į viršų išdėstymais, burtininkas / katalogas / konfliktai / traukiniai, gyvas gimtojo sugrįžimo konfigūracija, kreipimasis į priekį, atsisakymas kreiptis į priekį ir atkūrimas. Native callback įvedimas buvo simuliuojamas; fizinis žmogaus G raktas lieka rankiniu būdu.
- Tikroji priekinių langų nepakitimas buvo patikrinamas vykdymo taškui. Overlay sąveikos savybės ir parodymas / slėpimas buvo patikrinamas; ekrano nuotraukos buvo pateiktos burtininkui, overlay ir toast.
- 27 kalbomis yra visų 819 kanoninių juostų; atvirkščiai turėtojas / raktas patvirtintas.

Snapshots ir visiškas GUI rezultatas yra artefaktų/usp-win-x64/smoke. Bandymai naudoja laikinus profilius; gamybos nustatymai yra palaikomi ir patikrinami paleidimo metu.
