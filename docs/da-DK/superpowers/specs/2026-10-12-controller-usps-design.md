<!-- generated-doc locale=da-DK source=docs/superpowers/specs/2026-10-12-controller-usps-design.md sha256=4c3b2dbedb83af6bd48917515ae6559012afb6318ce69d1e32fb2765e0793327 -->

> Maskinassisteret oversættelse. Struktur, kodeeksempler og links kontrolleres; dette er ikke en gennemgang af en modersmålstalende eller en juridisk godkendelse. Historiske rapporter bevarer deres oprindelige datoer og omfang. [Oprindelig kilde](../../../superpowers/specs/2026-10-12-controller-usps-design.md)

# MAKRIVO Kontrolforlængelser

Brugeren bad om alle de ni foreslåede forbedringer og en genkendelig tray ikon. De eksisterende konfigurationer, især G1/G2 lys, skal forblive kompatible. De eksisterende ikke-kommanderede mærkning og dokumentationsaftaler tilhører det fælles arbejdsområde og skal bevares.

## Leveringsmuligheder og grænser

1. Valg alle de understøttede monitorer eller en stabil specifik monitor ID. Det er tydeligt, at de eksplicite mål mislykkes. Dashboardet viser nuværende lys, udgangsvolumen, mikrofonstatus og OBS status gennem begrænsede asynkrone læsninger.
2. Specielle nøgler understøtter øjeblikkelig tryk, tryk, hold og dobbelt tryk, med gentagne tryk. En holdet lag selektor midlertidigt ændrer den aktive mapping og frigiver det. Der er umiddelbare bindinger, der bevarer deres adfærd. Pause, profilændringer, afbrydelser og annullering tydelige hænder.
3. Arbejdsfløjets designer redigerer betingelser, fremadskridt og stop/continue fejlpolitik transaktionelt i et tylesæt. Betingelserne dækker filer, køreprocesser og vindu titler. Ingen ubegrænsede springer.
4. Eksplicit optagelse fanger kortskærme og venter, ignorerer injicerede indtægter og stopper på Escape. De registrerede handlinger gennemgås før vedtagelse; den almindelige tekst er ikke registreret.
5. Hver enhed anvendes specielle nøgler fra andre producenter Windows Raw Input Ejendomsnøgler kræver en støttet leverandør API eller leverandør mapping til en standardnøgle; ustøttet funktionalitet vises ærligt. Diagnosen forklarer profil, pause, bindende konflikter og den faktiske tilstand af adapteren.
6. Profileudveksling bruger en brugeren valgte delte mappe (herunder mapper, der er synkroniseret af en eksisterende cloud-klient). Revisionskontrol forhindrer stille overskrivninger. Pull forudser ændringer og importerer deaktiverede handlinger med hardware mappinger fjernet. Inklusionen af indholdet er udtrykkelig. Der er ingen cloud-krediter opbevaret af MAKRIVO.
7. UI-strenge tilhører de eksisterende 27 sprogkataloger; DE/EN/FR/IT forbliver de mindste dokumentationsspråk. Oversættelsesfrekvenser skal afsløres i stedet for at være fuldstændige.
8. En genkendelig teal M-tastatur erstatter den generiske Windows tray ikon. Det har en amber pause. Executable/window branding bruger det samme mærke. Tooltip omfatter status og profil.

## Verifikation

Core checks dækker backward kompatibilitet, snapshots, annullering, gesture timing, arbejdsprocessen brancher/fejl, optagelsesfiltrering og synkronisering konflikter. Windows checks dække læsning-only display enumeration, SDK release begivenheder og Raw Input parsing. WPF røgen checks dæk nye indgangspunkter og ressourcer uden at ændre den virkelige monitor lys, skrive i en anden applikation eller overskrive brugerprofiler. Opførelsen bevarer en backup og G1/G2 konfiguration.
