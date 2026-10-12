<!-- generated-doc locale=sv-SE source=docs/licensing/2026-10-12-commercial-license-audit.md sha256=b58c6861e0b38c4495265651b5ab10349c701d8fb4582dd7d7cae5d2d865bc5f -->

> Maskinassisterad översättning. Struktur, kodexempel och länkar kontrolleras; detta innebär inte granskning av en modersmålstalare eller juridiskt godkännande. Historiska rapporter behåller sina ursprungliga datum och omfattning. [Originalkälla](../../licensing/2026-10-12-commercial-license-audit.md)

# MAKRIVO: Prövning för kommersiell återförsäljning

Stannar: 12. oktober 2026. Tillverkare: Aivana.

## Resultatet

I den kontrollerade beställningen har ingen allmän skyldighet att avslöja sin egen källa MAKRIVO inte erkänts. En egen försäljning verkar möjlig, men är ännu inte helt förberedd. I synnerhet är tillståndet för överföring inte bevisat för den befintliga Corsair SDK-DLL. Denna prövning ersätter inte rättslig frigörelse.

## Tekniska och licensrelaterade resultat

| Beståndsdel | Upptäck | Ett nödvändigt steg |
| --- | --- | --- |
| egen källkod | Inga egna licenser, kopior eller anteckningar finns i den undersökta Git-innehållet och dess lokala tillgängliga licensfil historia. | bekräfta rättigheter, inklusive bidrag, övertagna kodfragment och eventuella arbetsgivares rättigheter; skapa egna villkor. |
| .NET och WindowsDesktop | Den oberoende paketet som studerats under `artifacts/makrivo-win-x64` innehåller versionen 10.0.6. De lokala löptidspaketet innehåller MIT-licenser som tillåter kommersiell överföring och kräver att deras anvisningar läggs till. I de undersökta utgåvorna hittades inga relevanta licens-/notiseringsfiler. | Exakta licenstexter och relevanta tredjepartsinformationer om den faktiska leveranstiden ingår i försäljningspaketet. |
| NuGet-beroende | I de undersökta projektfilerna hittades inga direkta paketreferenser. Den undersökta Desktop Assets-filen innehåller egna projektbindelser. Runtime-paket är fortfarande relevant för licensen. | Vid den slutliga utgåvan återinventerar alla projekt, transitiva paket och levererade filer. |
| Corsair iCUE för SDK | `artifacts/win-x64/iCUESDK.x64_2019.dll`, Dateversion 4.0.84.0. Den lokala SDK-referensen innehåller på trycksidor 35–38 EULA: kostnadsfri användning, men begränsningar för överföring/överföring. En tydlig tillåtelse att överföra DLL till kunder har inte fastställts i den. | Innan du samlar in en belastbar leveranslicens. Alternativt, kontrollera ett paket utan SDK-DLL som använder en kundbaserad SDK; SDK-villkoren gäller också för detta. |
| Olika paket | `artifacts/makrivo-win-x64` Innehåller den Corsair-DLL inte; `artifacts/win-x64` Applikationen söker SDK dynamiskt på flera platser. | Bestäm den faktiska försäljningsinstaller och kontrollera dess innehåll. Att det bara finns ett paket utan DLL bekräftar inte alla integrationsrättigheter. |
| OBS | En egen WebSocket kund i den undersökta C#-koden; ingen OBS kundpaket, OBS binär eller övertagna GPL-koden identifieras. | Den observerade protokollintegrationen motiverar inte en fastställd skyldighet att avslöja GPL. Förekomsten av eventuella fragment måste bekräftas. |
| App Symboler | Ett lokalt Python-skript markerar geometriska symboler med Pillow. Verktyget är en byggberoende. | Dokumentera ursprung och rättigheter till alla faktiska bilder, ljud, texter och andra tillgångar. Skrivet ensamt bevisar inte rättigheterna till varje tillgång. |
| Licensförvaltning | Ingen fullständig inköpslicens/aktiveringskontroll identifieras i den undersökta applikationskoden. | Om så önskas, genomföra undertecknade licensfiler. Teknisk aktivering och juridiska användarvillkor är separata uppgifter. |

## Mycket bra köp – inte implementerat

- Enstaka betalningar; permanent användningsrätt på den förvärvade huvudversionen.
- En person på upp till två egna datorer; professionell användning uttryckligen tillåts.
- Inkludera felkorrigeringar och mindre uppdateringar; senare huvudversioner erbjuds kostnadsfritt.
- Offline verifierbar, digitalt undertecknad licensfil; privata signaturnycklar aldrig utfärdas med appen.
- Lagliga rättigheter och nödvändiga säkerhetsuppdateringar får inte uteslutas i huvudversionsregeln.

## Före första försäljningen

1. Corsair förbereda försäljningen utan bundna SDK-DLL.
2. Licens- och tredjepartstexter av exakta utgivningsförpliktelser bifogas och mottagits i installatorn.
3. Bekräfta rättigheterna på din egen kod och tillgångar; skapa EULA med tydliga enheter, användar- och uppgraderingsregler.
4. För konsumentförsäljning behandla checkout inklusive informationskrav och återkallande. En fast "ingen återbetalning vid nedladdningar" är inte tillräcklig. Avskaffande av återkallelserätten vid omedelbar tillhandahållande förutsätter de lagliga villkoren.
5. Kontrollera den färdiga installatorn, inklusive plugins och alla övergående beroende. Verksamhetskopieringen utvidgas parallellt; den här filen är inte en del av framtida filer.

## GitHub

Repositoryn är offentlig. Utan öppen källlicens blir koden inte automatiskt fri för vidare användning. De GitHub plattformsrättigheterna för offentliga repositorier måste dock beaktas. En senare omvandling till privat tar inte bort befintliga kopior eller forkar. Synlighet och licens har inte ändrats under denna undersökning.

## Bevis och källor

- Lokala .NET-licenser: `C:/Users/weiss/.nuget/packages/microsoft.netcore.app.runtime.win-x64/10.0.6/LICENSE.TXT` och `C:/Users/weiss/.nuget/packages/microsoft.windowsdesktop.app.runtime.win-x64/10.0.6/LICENSE`
- Lokala .NET tredjepartsleverantörer: `C:/Users/weiss/.nuget/packages/microsoft.netcore.app.runtime.win-x64/10.0.6/THIRD-PARTY-NOTICES.TXT`
- Lokala Corsair SDK: `artifacts/dependencies/iCUESDK_4.0.84.zip`; extraherad referens PDF under `sdk/iCUESDK/doc/iCUE SDK - Overview and Reference.pdf`.
- [.NET 10.0.6 MIT licens](https://raw.githubusercontent.com/dotnet/runtime/v10.0.6/LICENSE.TXT)
- [Corsair SDK-dokumentation med EULA](https://raw.githubusercontent.com/CorsairOfficial/cue-sdk/master/docs/index.html)
- [GitHub: licensiera restauranger](https://docs.github.com/en/repositories/managing-your-repositorys-settings-and-features/customizing-your-repository/licensing-a-repository)
- [BGB § 327f – Uppdateringar](https://www.gesetze-im-internet.de/bgb/__327f.html)
- [BGB § 356 – Rätt till återkallelse](https://www.gesetze-im-internet.de/bgb/__356.html)

Inte granskad: fullständig externa bidragshistoria och rättighetskedjor, varumärkesutlåtande, befintliga försäljningscheckout, framtida plugin-paket, fullständig rättslig verkan av en ännu inte befintlig EULA.
