<!-- generated-doc locale=et-EE source=docs/licensing/2026-10-12-commercial-license-audit.md sha256=b58c6861e0b38c4495265651b5ab10349c701d8fb4582dd7d7cae5d2d865bc5f -->

> Masintõlke abil koostatud dokumentatsioon. Struktuuri, koodinäiteid ja linke kontrollitakse; see ei tähenda emakeelse toimetaja kontrolli ega õiguslikku heakskiitu. Ajaloolised aruanded säilitavad algsed kuupäevad ja ulatuse. [Originaalne allikas](../../licensing/2026-10-12-commercial-license-audit.md)

# MAKRIVO: Ühekordse müügi testimine

Jätkake: 12. oktoober 2026. Tootjad: Aivana.

## Tulemused

Kontrolliks ei tuvastatud üldist kohustust avaldada oma MAKRIVO allikakoodi. Omandatud ühekordne müük tundub võimalik, kuid ei ole veel täielikult valmis. Eelkõige ei ole olemasoleva Corsair SDK-DLLi edastamisluba tõendatud. See kontroll ei asenda õiguslikku vabastamist.

## Tehnilised ja litsentsiga seotud uuringud

| Koostisosa | Leitud | Vajalik samm |
| --- | --- | --- |
| Oma lähtekoodi | Ei leitud oma LICENSE, COPYING või NOTICE uuritud GIT-i ja selle kohaliku kättesaadavuse litsentsifaili ajalugu. | Õiguste omandamine, sealhulgas panused, omandatud koodide ja võimalike tööandjate õiguste kinnitamine; luua oma kasutustingimused. |
| .NET ja WindowsDesktop | Uuritud iseseisev pakett all `artifacts/makrivo-win-x64` sisaldab versiooni 10.0.6. Kohalikud käivitamispaketid sisaldavad MITi litsentse, mis võimaldavad kaubanduslikku edastamist ja nõuavad nende teavet. Uuritud väljaandmise järjekorras ei leitud vastavaid litsentsi/teatefaile. | Täpsed litsentsiteed ja asjakohased kolmandate osapoolte teavet tegelikult väljastatud käitumisajast müügipakendis. |
| NuGet - Ülevaade | Uuritud projektifailides ei leitud otseseid pakendi viiteid. Uuritud laua varade fail sisaldab oma projekti kohustusi. Runtime pakendid jäävad siiski litsentsile. | Lõpliku vabastamise ajal kõik projektid, üleminekupakendid ja kaasas olevad failid uuesti inventeerida. |
| Corsair iCUE SDK | `artifacts/win-x64/iCUESDK.x64_2019.dll`, failiversioon 4.0.84.0 Kohaliku SDK-referentsi PDF sisaldab EULA-d 35–38 trükis: tasuta kasutamine, kuid edastamise/väljaannete piirangud. Üksiku loa DLL-i edastamiseks klientidele ei ole selles märgitud. | Enne ühendamist omandada koormatud edastamisloa. Alternatiivselt kontrollida paketi ilma SDK DLL, mis kasutab kliendi poolt paigaldatud SDK; ka SDK tingimused kehtivad. |
| Erinevad pakendid | `artifacts/makrivo-win-x64` See sisaldab Corsair-DLL ei ole; `artifacts/win-x64` Rakendus otsib SDK-d dünaamiliselt mitmes kohas. | Määrake tegelik müügisüsteem ja kontrollige selle sisu. Ainult paketi olemasolu ilma DLL-deta ei kinnita veel kõiki integratsiooniõigusi. |
| OBS | Oma kliendi WebSocket uuritud C# kood; ei tuvastata kliendi paketi OBS, OBS binaarne või omandatud GPL koodi. | Vaadatud protokollide integreerimine ei põhjusta iseenesest kindlaksmääratud GPL-i avalikustamise kohustust. Võimalik, et päritolu on kinnitatud. |
| App sümbolid | Kohalik Python skripti märgib geomeetrilised sümbolid Pillow. Tööriist on ehitatud sõltuvus. | Dokumentatsiooni päritolu ja õigused kõik tegelikult edastatud pilte, helid, kirjad ja muud vara. Üksnes salvestus ei tõenda iga vara õigusi. |
| Litsentsi haldamine | Ei tuvastata täielikku ostulitsentsi/aktiivsuse kontrollimist uuritud rakenduskodu. | Soovi korral rakendada allkirjastatud offline litsentsifaile. Tehniline aktiveerimine ja seaduslikud kasutustingimused on eraldi ülesanded. |

## Mõistlik ostumudel – veel rakendamata

- Ühekordne maksmine; püsiv kasutamisõigus omandatud põhiversioonile.
- Üks isik kuni kaks oma PC; professionaalne kasutamine selgelt lubada.
- Sisaldab vigu parandusi ja väiksemaid värskendusi; edasised peamised versioonid pakutakse tasuta.
- Offline kontrollitav, digitaalselt allkirjastatud litsentsifail; privaatne allkirja ei anna kunagi rakendusega.
- Seaduslikud õigused ja vajalikud turvamuutused ei tohi peamise versiooni reeglites eraldada.

## Enne esimest müüki

1. Corsair edasimüügiõigused või müügi ettevalmistamine ilma ühendatud SDK-DLL.
2. Litsentsi ja kolmandate osapoolte tekstid täpse vabastamise kohustused lisada ja saada installeri.
3. kinnitada oma koodi ja varade õigusi; luua EULA selge seadmete, kasutajate ja uuenduste reeglid.
4. Tarbijakaubanduse puhul käsitletakse tagasimakset, sealhulgas teavitamiskohustusi ja tagasivõtmist. Paindlik „ei allalaadimisega tagastamine” ei piisa. Tagasivõtmise õiguse tühistamine viivitamata eeldab seaduslikke tingimusi.
5. Valmistatud installeri, sealhulgas plugins ja kõik ülemineku sõltuvused uuesti kontrollida. Töökopia laiendatakse paralleelselt; see salvestus ei ole tulevaste failide avaldamine.

## GitHub

Uuritud salong on avalik. Ilma avatud lähteaineta ei vabastata koodi automaatselt mis tahes uuesti kasutamiseks. Siiski tuleb arvestada GitHub platvormi õigusi avalikele salongidele. Hiljutine üleminek privaatsele ei eemaldata olemasolevaid koopiaid või vorke. Nähtavus ja litsents ei muudetud selle uuringu käigus.

## Tõendid ja allikad

- Kohalikud .NET litsentsid: `C:/Users/weiss/.nuget/packages/microsoft.netcore.app.runtime.win-x64/10.0.6/LICENSE.TXT` ja `C:/Users/weiss/.nuget/packages/microsoft.windowsdesktop.app.runtime.win-x64/10.0.6/LICENSE`.
- Kohalik .NET kolmandate osapoolte teave: `C:/Users/weiss/.nuget/packages/microsoft.netcore.app.runtime.win-x64/10.0.6/THIRD-PARTY-NOTICES.TXT`.
- Kohaliku Corsair SDK: `artifacts/dependencies/iCUESDK_4.0.84.zip`; ekstraktitud viide PDF all `sdk/iCUESDK/doc/iCUE SDK - Overview and Reference.pdf`.
- [.NET 10.0.6 MIT litsents](https://raw.githubusercontent.com/dotnet/runtime/v10.0.6/LICENSE.TXT)
- [Corsair SDK dokumentatsioon koos EULA](https://raw.githubusercontent.com/CorsairOfficial/cue-sdk/master/docs/index.html)
- [GitHub: Litsentseerimiskeskused](https://docs.github.com/en/repositories/managing-your-repositorys-settings-and-features/customizing-your-repository/licensing-a-repository)
- [BGB § 327f – ajakohastused](https://www.gesetze-im-internet.de/bgb/__327f.html)
- [BGB § 356 – taganemisõigus](https://www.gesetze-im-internet.de/bgb/__356.html)

Ei ole kontrollitud: täieliku välise panuse ajalugu ja õiguste ahel, kaubamärgi vabastamine, olemasolev müük, tulevased plug-in pakendid, täielik õiguslik tõhusus veel olemasoleva EULA.
