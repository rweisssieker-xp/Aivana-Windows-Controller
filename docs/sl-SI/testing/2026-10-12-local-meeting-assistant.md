<!-- generated-doc locale=sl-SI source=docs/testing/2026-10-12-local-meeting-assistant.md sha256=56631ca85dca5f785448d12586b0741628728925a649f6124f7aebffb19bb615 -->

> Strojno podprt prevod. Struktura, primeri kode in povezave se preverjajo; to ne pomeni pregleda maternega govorca ali pravne odobritve. Zgodovinska poročila ohranijo izvirne datume in obseg. [Izvirni vir](../../testing/2026-10-12-local-meeting-assistant.md)

# Lokalni AI pomočnik – prototip

Stanišče: 12. Oktobra 2026. ForgeMind hipoteza »Outcome Agent«, omejen tok srečanja. brez dokazane konkurenčne prednosti in ne merjenih prihrankov časa brez dejanskih primerjalnih poskusov.

## Uporaba

1. Naročni paleto → **KI pomočnik**. Poskus je standardno in se izvaja pri uvozu konfiguracije ali Izvoz portfelja je prekinjen.
2. Aktivirajte lokalne pomočnike. Ollama mora biti ločeno `127.0.0.1:11434` deluje; ustrezni lokalni model mora biti že nameščen. **Lokalni modeli** Izbira modela. MAKRIVO ne namesti modelov/služb, ne kontaktira OpenAI/Cloud API in ne uporablja API ključa. Ollama-Storitve so pri uporabniku; izjavljeni modeli za daljavo/oblak se zavrnejo.
3. Vnesite cilj in dodajte program/dokumente/spletne strani. Pošiljanje cilja, ID virov, ime in tip; ciljna pot in vsebina izbranih datotek se ne pošlje dodatno; ciljni vnos je dobesedno. Ime spletnega mesta vsebuje le shemo/host, polna povezava pa ostane lokalna.
4. **Ustvarite AI projekt**. Maksimalno 90 Sekunde, 1024 Izhodne vrstice, omejena velikost odziva. spremembe cilja/modela/virusa zavračajo predogled; prekinitev/zaprtje zaprosila. model lahko predlaga le izbrane ID-je. nepoznane ID-je, brezplačne ukaze, dodatna JSON polja in izvedljive vrste dokumentov so zavrnjene.
5. Preverite in preverite odprte točke → **V uredniku**. Na začetku je to nesprejemljiv projekt. Najprej shranite v obstoječem uredniku in po potrebi izrecno izvedite. Normalna makrovalidacija, predhodno/pregledovanje, prekinitev in prekinitev ostanejo odgovorni.

Podprto: Začetek programov brez argumentov, odprtje izrecno izbranih lokalnih PDF/DOCX/XLSX/PPTX/MD, odprtja spletnih strani HTTP(S), po izrecnem zahtevu zaklop mikrofon. Brez standardnih sprememb slušalke, nobenih sporočil, datuma, prenosov, skriptov, brezplačnih klikov ali besedilnih vnosov. Neizražene želje se pojavijo kot odprte točke, če jih model pravilno prepozna; strokovno izpolnjevanje ciljev mora uporabnik preveriti. Dokumentacija ni samodejno preverjena.

## primerjalni eksperiment

**Razmerje nastavitve · Začetek / konec** merjenje celotnega ročnega poravnave. Pomočniki merijo čas od odprtja pomočnika do prvega shranjevanja v uredniku; `generated` pa vsebuje le čas načrtovanja. Ne primerjajte dveh neenakosti kot prihranek časa.

Lokalni dogodki so v nastavitvenem redu pod `Experiments/meeting-experiment.jsonl`: časovni znak, naključni poskus / ID, dogodek, trajanje, popravek da / ne. Ni ciljnih besedil, imen virov, poti, odgovorov na modele ali vsebine dokumentov. Datoteka se vrti od 2 MiB, prejšnja datoteka ostane. Napake v merjenju ne smejo prekiniti makro in konfiguracijskega shranjevanja.

Vključuje: ročno bazo, modelno ustvarjanje/pogrešitev/odstranitev, zamudne spremembe/zaprtje, prvo shranjevanje in popravke v uredniku ter dejanske končne pogoje za shranjene ključe. `run_completed` pomeni le, da je usmerjevalec končan; potrjeni cilji so ločeni v pregledu. Ponovitev istega makra ni nova poskusa neodvisnosti. Začetni poskus se lahko nadaljuje po ponovnem zagonu, saj je preizkusni ID ostal na shranjenem kletku. Duplicirane klobuke delijo ta izvor; pri ocenjevanju po poskusni ID.

Prvi poskus: 20 primerljive srečanja ali dva tedna z prostovoljnimi uporabniki; najprej notranja uporaba. Manualno primerjavo časa za polno časovno pomoč; poleg tega ocenite stopnjo popravka/izguba, napake v teku in dejanske rezultate pregleda. V primeru pomanjkanja izboljšanja ali kršitve zaščitnih meja izklopite. Ni dokazano nobene uporabe ali pripravljenosti za plačilo v arhivu. Brez samodejnega prenosa ali prilagajanja cen.

## Preizkušanje

- Core: omejitve, vrste, neznane/dvojne ID, izključitev skripta, dolge povezave, opcionalna konfiguracija, uvoz/izvoz-odstranitev, merjenje za varčevanje z podatki ter enkratno zavrnitev in pravilno prenos.
- Prevoz s ponarejenim modelom: Ollama JSON shema, Loopback cilj, nobenih vrstic v prompt, ime oblaka in oddaljene odjave, poškodovan/preveč odgovor, odpoved.
- WPF-Smoke: Dialog zmanjša; prenosi načrta ne spremeni shranjenih klobukov, pregledi/save dobi izvor in zazna korekcijo, zavrnitev shrani nič.
- Neodvisna pregleda: predogled in dolge povezave so našli in popravili; regresijski testi RED→GREEN.
- Release Build z nič opozorila/greške, celoten Core Suite in WPF Smoke uspešno. Celotna soba Windows je bila na začetku vključena tudi sprememba osredotočenja. Še en tek se je znova izkazal za neuspeh pri zavrnitvi; test je kontekstno odvisen in ostaja nespremenjen. Preostali preizkus Windows vključno s prevozom modelov poteka z izrecno izpuščenim preizkusom uspeha.
- Preverjanje paketov je odkrilo obstoječo napako pri jedrski izmenjavi kratkoročnih konfiguracijskih datotek. Čitalnik je ponovil isto dostopno zavrnjeno napako; omejena ponovitev samo jedrske izmenjave je RED→GREEN preverjen. Trajne napake ostanejo vidne.
- Trenutni samostojni paket `artifacts/release-20261012-184105`: WPF smoke po popravku pomnilnika uspešno, vključno z AI dialog in sprejetje/odbijanje načrta.
- Ni dejanskega testa modela: lokalna služba Ollama ni odgovorila na preskus. Nobenih izjav o kakovosti modela, latentnosti, semantični celovitosti ali prihranku časa. Vendar pa so meje strojne opreme/vodil/skalifikacije prejšnje različice še vedno.

Viri protokola: [Ollama Chat](https://docs.ollama.com/api/chat), [Struktura izhodov](https://docs.ollama.com/capabilities/structured-outputs), [Uradne opredelitve tipa, vključno z daljinskimi podatki o modelu](https://github.com/ollama/ollama/blob/main/api/types.go).
