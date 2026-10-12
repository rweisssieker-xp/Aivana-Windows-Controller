<!-- generated-doc locale=lt-LT source=docs/licensing/2026-10-12-commercial-license-audit.md sha256=b58c6861e0b38c4495265651b5ab10349c701d8fb4582dd7d7cae5d2d865bc5f -->

> Mašininio vertimo pagalba parengtas vertimas. Tikrinama struktūra, kodo pavyzdžiai ir nuorodos; tai nereiškia gimtakalbio peržiūros ar teisinio patvirtinimo. Istorinės ataskaitos išlaiko pradines datas ir apimtį. [Originalus šaltinis](../../licensing/2026-10-12-commercial-license-audit.md)

# MAKRIVO: Komercinės vienkartinės pardavimo testas

Standas: 12. Spalio mėn. 2026. Gamintojas: Aivana.

## Rezultatas

Tikrinamoje sandėlyje nebuvo nustatyta jokios bendros pareigos atskleisti savo MAKRIVO šaltinio kodą. Vienkartinis pardavimas atrodo įmanomas, tačiau dar nėra visiškai paruoštas. Visų pirma, leidimas perduoti esamą Corsair SDK DLL nėra įrodytas. Šis tyrimas nepakeičia teisinio išleidimo.

## Techniniai ir licencijų tyrimai

| Sudėtis | Įrodymas | Reikalingas žingsnis |
| --- | --- | --- |
| Savo šaltinio kodas | Nėra jokių savų LICENCIJA, KOPIJA ar Pranešimai, kurie būtų rasti tiriamoje „Git“ turinyje ir jo vietos licencijos failo istorijoje. | Teisės, įskaitant įnašus, įsigytas kodų fragmentus ir galimas darbdavių teises patvirtinti; sukurti savo naudojimo sąlygas. |
| .NET ir WindowsDesktop | Ištirtas atskiras paketas pagal `artifacts/makrivo-win-x64` yra versija 10.0.6. Vietos paleidimo paketai apima MIT licencijas, leidžiančias komercinį perdavimą ir reikalauja jų pateikimo. Tyrimo tvarkaraščiuose nebuvo rasti atitinkamų licencijų / pranešimų failų. | Tikslūs licencijos tekstai ir atitinkami trečiųjų šalių pranešimai apie faktinį pardavimo laiką įtraukti į pardavimo paketą. |
| NuGet įsipareigojimai | Ištirtų projektų failų nerado tiesioginių paketų nuorodų. Ištirtas "Desktop" turtas turi savo projektų įsipareigojimus. Vis dėlto „Runtime“ paketai lieka licencijos reikalavimai. | Galutinio išleidimo metu visi projektai, pereinamojo laikotarpio paketai ir pristatyti failai vėl įrašo. |
| Corsair iCUE SDK | `artifacts/win-x64/iCUESDK.x64_2019.dll`, failų versija 4.0.84.0 Vietos SDK nuorodos PDF spausdintuvėse 35–38 pateikiama EULA: nemokama naudojimas, tačiau perdavimo / išdavimo apribojimai. Jame nenustatyta aiški leidimas perduoti DLL klientams. | Prieš susietą gaukite apkrovos leidimą. Alternatyviai patikrinti paketą be SDK DLL, kuris naudoja klientų pusėje įdiegtą SDK; taip pat taikomos SDK sąlygos. |
| Įvairių paketų | `artifacts/makrivo-win-x64` Jame yra Corsair-DLL ne; `artifacts/win-x64` Įtrauktas. programos ieško SDK dinamiškai keliose vietose. | Nustatykite faktinį pardavimo įrenginį ir patikrinkite jo turinį. Vienintelis paketo egzistavimas be DLL dar nepaaiškina visų integracijos teisių. |
| OBS | Savarankiškas WebSocket klientas nagrinėjamuose C# koduose; nėra identifikuotas OBS kliento paketas, OBS binaras ar įsigytas GPL kodas. | Stebėta protokolo integracija savaime nesuteikia nustatyto GPL atskleidimo pareigos. Galimų fragmentų kilmė lieka patvirtinta. |
| App simboliai | Vietinis Python scenarijus apibūdina geometrinius simbolius su Pillow. Šis įrankis yra statybos priklausomybė. | Dokumentuoti visų faktinių nuotraukų, garsų, raštų ir kitų turto kilmę ir teises. Vienas scenarijus neparodo visų turto teisių. |
| Licencijos administravimas | Nėra išsamios pirkimo licencijos / aktyvavimo patikros, nustatytos nagrinėjamuose taikymo koduose. | Jei pageidaujama, pasirašytus licencijos failus įgyvendinti. Techninė aktyvacija ir teisinės naudojimo sąlygos yra atskiri užduotys. |

## Sunkus pirkimo modelis – dar neįgyvendintas

- Vienkartinis mokėjimas; nuolatinis naudojimo teisė į įsigytą pagrindinę versiją.
- Vienas asmuo iki dviejų savų kompiuterių; profesionalus naudojimas aiškiai leidžiamas.
- Įtraukti klaidų pataisymus ir mažesnius atnaujinimus; vėliau pagrindinės versijos siūlomos nemokamai.
- Ne internete patikrinamas, skaitmeniniu būdu pasirašytas licencijos failas; privatus pasirašymo raktas niekada neperduoda su programa.
- Pagrindinė versijos taisyklė negali atskleisti teisinių teisių ir reikalingų saugos atnaujinimų.

## Prieš pirmąjį pardavimą

1. Corsair išaiškinti perdavimo teises arba paruošti pardavimą be susieto SDK DLL.
2. Licencijos ir trečiųjų šalių įsipareigojimų tekstai pridedami ir gaunami įdiegėją.
3. Patvirtinti teises į savo kodą ir turtą; sukurti EULA su aiškiu įrenginio, vartotojo ir atnaujinimo taisyklių.
4. Vartotojų pardavimų apdorojimas, įskaitant informacijos reikalavimus ir atšaukimą. Vienkartinis „Nėra grąžinimo per atsisiuntimus“ nepakanka. Teisės atšaukimo panaikinimas, kai pateikiama nedelsiant, numato teisines sąlygas.
5. Užbaigtą montuotoją, įskaitant įskiepius ir visas pereinamasis priklausomybės peržiūrėti. Darbo kopija yra išplėsta lygiagrečiai; šis įrašas nėra būsimų failų atskleidimas.

## GitHub

Apklausos katalogas yra viešas. Be atviro šaltinio licencijos kodas automatiškai nebus laisvas bet kokiam tolesnio naudojimo tikslais. Vis dėlto reikėtų atsižvelgti į GitHub viešųjų repositorių platformos teises. Vėlesnis perėjimas į privatų nepašalinti jau egzistuojančių kopijų ar forkų. Matomumas ir licencija nebuvo pakeista per šį egzaminą.

## Įrodymai ir šaltiniai

- Vietos .NET licencijos: `C:/Users/weiss/.nuget/packages/microsoft.netcore.app.runtime.win-x64/10.0.6/LICENSE.TXT` ir `C:/Users/weiss/.nuget/packages/microsoft.windowsdesktop.app.runtime.win-x64/10.0.6/LICENSE`.
- Vietinis .NET trečiųjų šalių teikėjo pranešimas: `C:/Users/weiss/.nuget/packages/microsoft.netcore.app.runtime.win-x64/10.0.6/THIRD-PARTY-NOTICES.TXT`.
- Vietinis Corsair SDK: `artifacts/dependencies/iCUESDK_4.0.84.zip`; išvestas nuorodos PDF žemiau `sdk/iCUESDK/doc/iCUE SDK - Overview and Reference.pdf`.
- [.NET 10.0.6 MIT licencija](https://raw.githubusercontent.com/dotnet/runtime/v10.0.6/LICENSE.TXT)
- [Corsair SDK dokumentacija su EULA](https://raw.githubusercontent.com/CorsairOfficial/cue-sdk/master/docs/index.html)
- [GitHub: Leidžiama registruotis](https://docs.github.com/en/repositories/managing-your-repositorys-settings-and-features/customizing-your-repository/licensing-a-repository)
- [BGB § 327f – atnaujinimai](https://www.gesetze-im-internet.de/bgb/__327f.html)
- [BGB § 356 – atšaukimo teisė](https://www.gesetze-im-internet.de/bgb/__356.html)

Nesu patikrinamas: visiškas išorės indėlių istorija ir teisių grandinės, prekių ženklų išdavimas, esamas pardavimų šekutų, būsimi papildai, visiškas teisinis veiksmingumas dar nėra EULA.
