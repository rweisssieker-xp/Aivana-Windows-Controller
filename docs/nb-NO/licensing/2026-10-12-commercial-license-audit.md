<!-- generated-doc locale=nb-NO source=docs/licensing/2026-10-12-commercial-license-audit.md sha256=b58c6861e0b38c4495265651b5ab10349c701d8fb4582dd7d7cae5d2d865bc5f -->

> Maskinassistert oversettelse. Struktur, kodeeksempler og lenker kontrolleres; dette innebærer ikke gjennomgang av en morsmålsbruker eller juridisk godkjenning. Historiske rapporter beholder sine opprinnelige datoer og omfang. [Originalt kilde](../../licensing/2026-10-12-commercial-license-audit.md)

# MAKRIVO: Test for kommersiell enkeltsalg

Stand: 12. Oktober 2026. produsent: Aivana.

## Resultatet

Det ble ikke oppdaget noen generell forpliktelse til å avsløre sin egen kildekode MAKRIVO. En eiendomsforsalg synes å være mulig, men er ennå ikke fullt forberedt. Særlig er ikke tillatelsen for overføring av eksisterende Corsair SDK-DLL beviset. Denne undersøkelsen erstattes ikke av juridisk frigjøring.

## Tekniske og lisensrelaterte oppdagelser

| Bestanddel | Oppdaget | Nødvendig skritt |
| --- | --- | --- |
| egen kilde | Ingen egen LICENSE, COPYING eller NOTICE funnet i GIT-innholdet og dens lokalt tilgjengelige lisensfilhistorie. | Bekreftelse av rettigheter, inkludert bidrag, overtatt kodefragmenter og mulig arbeidsgiverrettigheter; opprette egne vilkår for bruk. |
| .NET og WindowsDesktop | Den uavhengige pakken under `artifacts/makrivo-win-x64` inneholder versjon 10.0.6. De lokale runtime-pakkene inneholder MIT-lisenser som tillater kommersiell overføring og krever å legge til informasjonen. I de undersøkte utgivelsesordene ble det ikke funnet noen relevante lisens-/notasjonsfiler. | Nøyaktige lisenstekster og relevante tredjepartsinformasjoner om faktisk utstedt runtime inkluderes i salgspakken. |
| NuGet – forpliktelser | Det finnes ingen direkte pakkereferanseinnlegg i prosjektfiler som er undersøkt. Den undersøkte Desktop Assets-filen inneholder egne prosjektforpliktelser. Runtime-pakker forblir imidlertid lisensrelevant. | Ved den endelige utgivelsen gjenopprette alle prosjekter, transitive pakker og medleverte filer. |
| Corsair iCUE med SDK | `artifacts/win-x64/iCUESDK.x64_2019.dll`, filversjon 4.0.84.0 Den lokale SDK-referanse-PDF inneholder EULA på 35–38: gratis bruk, men begrensninger for overføring/utgivelse. Det ble ikke oppdaget en tydelig tillatelse til å overføre DLL til kunder. | For å få en belastbar leveringstillatelse. Alternativt, sjekk et pakke uten SDK-DLL som bruker en kundesidig installert SDK; også for dette gjelder SDK-vilkårene. |
| Forskjellige pakker | `artifacts/makrivo-win-x64` Inneholder den Corsair-DLL ikke; `artifacts/win-x64` applikasjonen søker SDK dynamisk på flere steder. | Bestill den faktiske salgsinstaller og sjekk innholdet. Den enkle eksistensen av et pakke uten DLL bekrefter ikke alle integrasjonsrettigheter. |
| OBS | Egen WebSocket-klient i C#-koden som er undersøkt; ingen OBS-klientpakke, OBS-binary eller overtatt GPL-kode identifiseres. | Den observerte protokollintegrasjonen begrenser seg ikke til en fastsatt GPL-oppklaringsforpliktelse. Opprinnelsen til eventuelle fragmenter må bekreftes. |
| App-symboler | Et lokalt Python-skript markerer geometriske symboler med Pillow. Verktøyet er en bygningsavhengighet. | Dokumentere opprinnelsen og rettighetene til alle faktiske bilder, lyder, tekster og andre eiendeler. Skriptet alene viser ikke rettighetene til alle eiendeler. |
| Lisensforvaltning | Ingen fullstendig kjøpslicens/aktiveringskontroll er identifisert i brukskoden som er undersøkt. | Hvis ønskelig, signert offline lisens filer implementere. Teknisk aktivering og juridiske bruksvilkår er separate oppgaver. |

## En god kjøpsmodell – ennå ikke implementert

- Enkelbetaling; permanent bruk av den kjøpte hovedversjonen.
- En person på opptil to egne PCer; profesjonell bruk uttrykkelig tillater.
- Inkluderer feil korrigering og mindre oppdateringer; senere hovedversjoner tilbys valgfritt.
- Offline verifiserbar, digitalt signert lisensfil; private signaturnøkler utleverer aldri med appen.
- Lagsrettslige rettigheter og nødvendige sikkerhetsoppdateringer kan ikke utelukkes ved hovedversjonsregelen.

## Før første salg

1. Corsair forberede salg uten bundet SDK-DLL.
2. Lisens- og tredjepartstekster av nøyaktige utgivelsesforpliktelser legges til og mottas i installatøren.
3. Bekreft rettighetene på egen kode og eiendeler; opprette EULA med klare enheter, brukere og oppgraderingsregler.
4. For forbruker salg behandle sjekk, inkludert informasjonsforpliktelser og tilbakekalling. En fast «ingen tilbakebetaling ved nedlasting» er ikke nok. Avskaffelse av tilbakekallingsretten i tilfelle umiddelbar levering forutsetter lovbestemmelsene.
5. Fortsett å sjekke installatøren, inkludert plugins og alle transitive avhengigheter. Arbeidskopien blir utvidet parallelt; denne arkivet er ikke frigjøring av fremtidige filer.

## GitHub

Repositorien er offentlig. Uten åpen kilde lisens blir koden ikke automatisk fri for videre bruk. De GitHub plattformenes rettigheter for offentlige repositorier må imidlertid være oppmerksomme. En senere overføring til privat fjerner ingen eksisterende kopier eller forker. Synlighet og lisens ble ikke endret i denne testen.

## Bevis og kilder

- Lokale .NET-lisenser: `C:/Users/weiss/.nuget/packages/microsoft.netcore.app.runtime.win-x64/10.0.6/LICENSE.TXT` og `C:/Users/weiss/.nuget/packages/microsoft.windowsdesktop.app.runtime.win-x64/10.0.6/LICENSE`
- Lokale .NET tredjepartsleverandører: `C:/Users/weiss/.nuget/packages/microsoft.netcore.app.runtime.win-x64/10.0.6/THIRD-PARTY-NOTICES.TXT`
- Lokale Corsair SDK: `artifacts/dependencies/iCUESDK_4.0.84.zip`; utvunnet referanse-PDF under `sdk/iCUESDK/doc/iCUE SDK - Overview and Reference.pdf`.
- [.NET 10.0.6 MIT lisens](https://raw.githubusercontent.com/dotnet/runtime/v10.0.6/LICENSE.TXT)
- [Corsair SDK-dokumentasjon med EULA](https://raw.githubusercontent.com/CorsairOfficial/cue-sdk/master/docs/index.html)
- [GitHub: Lisensiering av repositorier](https://docs.github.com/en/repositories/managing-your-repositorys-settings-and-features/customizing-your-repository/licensing-a-repository)
- [BGB § 327f – Oppdateringer](https://www.gesetze-im-internet.de/bgb/__327f.html)
- [BGB § 356 – Rett til tilbakekalling](https://www.gesetze-im-internet.de/bgb/__356.html)

Ikke kontrollert: fullstendig ekstern bidragshistorie og rettigheter, varemerkeavdeling, eksisterende salgskjøp, fremtidige plugins, fullstendig juridisk effektivitet av en ikke-tilgjengelig EULA.
