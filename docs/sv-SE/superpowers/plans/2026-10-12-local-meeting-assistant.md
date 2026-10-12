<!-- generated-doc locale=sv-SE source=docs/superpowers/plans/2026-10-12-local-meeting-assistant.md sha256=1f105661dc4244fce4110ddae1484b4ce819e2d4c6945ada84a27704d95db72a -->

> Maskinassisterad översättning. Struktur, kodexempel och länkar kontrolleras; detta innebär inte granskning av en modersmålstalare eller juridiskt godkännande. Historiska rapporter behåller sina ursprungliga datum och omfattning. [Originalkälla](../../../superpowers/plans/2026-10-12-local-meeting-assistant.md)

# Lokala mötesassistenter – 2026-10-12

Godkända av "OK" och befintlig beställning utan frågor. Förändringar i befintlig bransch. Korta prototyper, ingen automatisk genomförande.

1. Kärn: Välj resurser med ID; Modellplan endast resurs-ID, mikrofon stängd, öppna punkter. Strikta storlekar/typ/ID-gränser; Compiler genererar endast Launch/OpenPath/Url/Volume. Test först: okända ID, utförbara dokument, icke-HTTP-URL, duplikater, tomma planer, funktioner.
2. Windows: Ollama loopback-only HTTP utan proxy/redirect, JSON-schema, token/responsgränser, avbrott och 90s-Zeitlimit; ingen automatisk installation/ nedladdning, ingen moln/API-nyckel. Falska transporttester undersöker fråga och fel; verklig modell endast när det finns lokala.
3. WPF: kommandospalett, standardaktiverad växellåda, val av modell från den lokala tjänsten, mål och uttryckliga resurslistan, förhands/öppna punkter, avbrott, överföring i ett befintligt design utan genomförande. Förändringar efter förhandsvisningen gör dem ogiltiga; granskning av profil.
4. Experiment: manuell stopp och lokala mätningar (utan mål/vägar), antagande/korrigering/avslag samt faktiska makrotillstånd. Inga bevis på framgång endast från Completed; befintliga resultatprov separat.
5. Test: Core, Transport, WPF Smoke; inga användarinsatser. Rapporten markerar bristande live-modellprov och fortsätter att öppna gamla släppgator. Den befintliga sex procent utgåva inte omdeklareras som AI-förmåga.

Ollama-protokollkällor: https://docs.ollama.com/api/chat och https://docs.ollama.com/capabilities/structured-outputs (12.10.2026).

## Utförandestatus

- Core Compiler/Feature/Data besparing grön efter initialt Compile-RED.
- Lokal modellklient grön efter initialt kompilerad RED; fjärran Alias, molnnamn, schema, bristande väg i prompt, bruten/för stor utgång och avbrott kontrolleras med falska handlare.
- WPF-Handoff: inte sparas, inte utföras; granskning/lagring/korrigering och avslag i rök grön.
- Fresh-context reviewer /root/meeting_review (read-only): två P2-Funde, förkastade undersökningar och långa länkar. Båda korrigeras med RED→GREEN. Inga fler viktiga resultat rapporteras.
- Release-Build noll varningar/fel, alla kärngrupper och fullständig Windows-switten utan skip grön. Native fokus testning nu också grön; befintliga iCUE-tastatur ansluten. Inga verkliga användarprogram som startas med modell.
- Ingen Live-Ollama tillgänglig; ingen modell/tjänst installerad och ingen moln/nyckel används. Kvalitet/tidsbesparing är experimentell.
- Selfcontained-paket med åtföljande program och nya servicepaketer/testanvisningar under artfakter/release-20261012-183635. Erhålls befintliga artefakter och obetydliga ändringar; ingen merge/push.

- Paketet smoke visade åtkomst-avvisas på ConfigStore atomiska File.Move; ny upprepning förblev senare hängande i en befintlig feldialog och avslutades ändamålsenligt. Regression med FileShare.Read (Läsning tillåts, Delete-sharing vägras) reproducerar File.Move fel. Begränsad retry endast för Win32 5/32/33 till 500ms RÖD→GREEN; permanent saknade rättigheter förblir fel. Nya paketkontrollen granskas igen.

- Den slutgiltiga fullständiga Windows återhämtningskursen misslyckas igen med en förnekad fokusväxling; inga nya kompiler/modellfel. Övriga Suite --skip-foreground-fixture grön. Det finns ingen stabil grön för sammanhangsberoende nativt grundläggande test. Aktuell paketvägen artifacts/release-20261012-184105

- Aktuell oberoende paket av artefakter/release-20261012-184105: WPF smuts inklusive AI-dialog, olagrad redigeringsöverföring, korrigering mätning och framgångsrik avvisning (Exit 0). Ingen annan kod ändras efter detta test.
