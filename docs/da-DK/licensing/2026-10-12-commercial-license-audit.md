<!-- generated-doc locale=da-DK source=docs/licensing/2026-10-12-commercial-license-audit.md sha256=b58c6861e0b38c4495265651b5ab10349c701d8fb4582dd7d7cae5d2d865bc5f -->

> Maskinassisteret oversættelse. Struktur, kodeeksempler og links kontrolleres; dette er ikke en gennemgang af en modersmålstalende eller en juridisk godkendelse. Historiske rapporter bevarer deres oprindelige datoer og omfang. [Oprindelig kilde](../../licensing/2026-10-12-commercial-license-audit.md)

# MAKRIVO: Prøve for kommercielle enkeltsalg

Stand: 12. Oktober 2026. Producenterne: Aivana.

## Resultatet

I det kontrollerede arkiv blev der ikke anerkendt en generel forpligtelse til at afsløre sin egen kilde kode MAKRIVO. En ejendomsforretning synes at være mulig, men er endnu ikke fuldt ud forberedt. Især er overførselstilladelsen ikke dokumenteret for den eksisterende Corsair SDK-DLL. Denne undersøgelse erstatter ikke den juridiske frigivelse.

## Tekniske og licensrelaterede resultater

| Bestanddel | Findet | Det nødvendige skridt |
| --- | --- | --- |
| Din egen kilde | Der findes ingen egen LICENSE, COPYING eller NOTICE i den undersøgte GIT-indhold og dens lokalt tilgængelige licensfil historie. | bekræftelse af ejerskab, herunder bidrag, overtagne kodefragmenter og eventuelle arbejdsgiverrettigheder; oprettelse af egne vilkår for brug. |
| .NET og WindowsDesktop | Det uafhængige pakke under `artifacts/makrivo-win-x64` indeholder version 10.0.6. De lokale runtime-pakker indeholder MIT-licenser, der tillader kommercielle overførsel og kræver, at deres meddelelser tilføjes. I de undersøgte udgivelsesordre blev der ikke fundet relevante licens-/notisfiler. | Nøjagtige licenstekster og relevante tredjepartsoplysninger om den faktiske udstedte runtime indgå i salgspakken. |
| NuGet - afhængighed | I de undersøgte projektfiler er der ikke fundet direkte pakkereference-indlæg. Den undersøgte Desktop-acts-fil indeholder egne projektforpligtelser. Runtime-pakkerne forbliver dog licensrelaterede. | Ved den endelige udgivelse geninventar alle projekter, transitive pakker og leverede filer. |
| Corsair iCUE af SDK | `artifacts/win-x64/iCUESDK.x64_2019.dll`, filversion 4.0.84.0 Den lokale SDK-reference-PDF indeholder EULA på 35–38-sider: gratis brug, men begrænsninger for overførsel/udlevering. Der blev ikke fastsat en klar tilladelse til at videregive DLL til kunder. | Inden forbindelsen opnås en belastbar leveringstilladelse. Alternativt skal du tjekke et pakke uden SDK-DLL, der bruger en kundesidig installeret SDK; det gælder også SDK-betingelserne. |
| Forskellige pakker | `artifacts/makrivo-win-x64` Inkluderer den Corsair-DLL ikke; `artifacts/win-x64` Applikationen søger SDK dynamisk på flere steder. | Find den faktiske salgsinstaller og tjek indholdet af det. Den eneste eksistens af et pakke uden DLL bekræfter endnu ikke alle integrationsrettigheder. |
| OBS | Egen WebSocket-klient i den undersøgte C#-kode; ingen OBS-klientpakke, OBS binær eller overtaget GPL-kode identificeret. | Den observerede protokolintegration begrunder ikke en fastlagt GPL-oplysningsforpligtelse. Oprindelsen af eventuelle fragmenter skal bekræftes. |
| App-symboler | Et lokalt Python-skript markerer geometriske symboler med Pillow. Værktøjet er en bygning afhængighed. | Oprindelsen og rettighederne til alle faktiske billeder, lyder, tekster og andre aktiver dokumenteres. Skriptet alene beviser ikke rettighederne til alle aktiver. |
| Licensforvaltning | Der er ingen fuldstændig købs- og aktiveringskontrol, der er identificeret i den undersøgte anvendelseskod. | Hvis det er ønskeligt, gennemføre underskrevne licensfiler. Teknisk aktivering og juridiske betingelser for brug er separate opgaver. |

## Den fornuftige købsmodel – endnu ikke implementeret

- Enkel betaling; permanent brugsret på den erhvervede hovedversion.
- En person på op til to egne pc'er; professionel brug udtrykkeligt tillader.
- Forkortelse og mindre opdateringer; senere hovedversioner til valgfri betaling.
- Offline verifierbar, digitalt underskrevet licensfil; private signaturnøgler udleveres aldrig med appen.
- De lovmæssige rettigheder og de nødvendige sikkerhedsopdateringer kan ikke udelukkes ved hovedversionsreglen.

## Før første salg åbent

1. Corsair forbereder salg uden bundet SDK-DLL.
2. Tilføj licens- og tredjepartstekster til de nøjagtige frigivelsesforpligtelser og modtage dem i installatøren.
3. Bekræft ejerskab på din egen kode og aktiver; EULA oprettes med en klar enhed, bruger- og opgraderingsregel.
4. For forbrugersalg behandle checkout, herunder informationskrav og tilbagekaldelse. En fast "ikke tilbagebetaling ved downloads" er ikke nok. Afskaffelse af tilbagekaldelsesretten i tilfælde af øjeblikkelig levering forudsætter lovbestemmelserne.
5. Gennemført installatør, herunder plugins og alle transitive afhængigheder igen. Arbejdskopien udvides parallelt; denne arkivering er ikke frigivelse af fremtidige filer.

## GitHub

Den undersøgte repository er offentlig. Uden Open Source-licens vil koden ikke automatisk blive frigivet til yderligere brug. De GitHub plattformsrettigheder for offentlige repositorier skal dog overholdes. En senere omstilling til privat fjerner ingen eksisterende kopier eller forker. Visibilitet og licens blev ikke ændret i denne undersøgelse.

## Bevis og kilder

- Lokale .NET-licenser: `C:/Users/weiss/.nuget/packages/microsoft.netcore.app.runtime.win-x64/10.0.6/LICENSE.TXT` og `C:/Users/weiss/.nuget/packages/microsoft.windowsdesktop.app.runtime.win-x64/10.0.6/LICENSE`
- Lokale .NET tredjepartsudbydere: `C:/Users/weiss/.nuget/packages/microsoft.netcore.app.runtime.win-x64/10.0.6/THIRD-PARTY-NOTICES.TXT`
- Lokale Corsair SDK: `artifacts/dependencies/iCUESDK_4.0.84.zip`; udvundet reference-PDF under `sdk/iCUESDK/doc/iCUE SDK - Overview and Reference.pdf`.
- [.NET 10.0.6 MIT licens](https://raw.githubusercontent.com/dotnet/runtime/v10.0.6/LICENSE.TXT)
- [Corsair SDK-dokumentation med EULA](https://raw.githubusercontent.com/CorsairOfficial/cue-sdk/master/docs/index.html)
- [GitHub: licens til repositorier](https://docs.github.com/en/repositories/managing-your-repositorys-settings-and-features/customizing-your-repository/licensing-a-repository)
- [BGB § 327f – opdateringer](https://www.gesetze-im-internet.de/bgb/__327f.html)
- [BGB § 356 – Ret til tilbagekaldelse](https://www.gesetze-im-internet.de/bgb/__356.html)

Ikke testet: fuldt ud ekstern bidragshistorie og rettigheder, varemærkeudgivelse, eksisterende salgshekkout, fremtidige plugins, fuld juridisk effektivitet af en endnu ikke tilgængelig EULA.
