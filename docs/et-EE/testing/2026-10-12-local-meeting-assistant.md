<!-- generated-doc locale=et-EE source=docs/testing/2026-10-12-local-meeting-assistant.md sha256=56631ca85dca5f785448d12586b0741628728925a649f6124f7aebffb19bb615 -->

> Masintõlke abil koostatud dokumentatsioon. Struktuuri, koodinäiteid ja linke kontrollitakse; see ei tähenda emakeelse toimetaja kontrolli ega õiguslikku heakskiitu. Ajaloolised aruanded säilitavad algsed kuupäevad ja ulatuse. [Originaalne allikas](../../testing/2026-10-12-local-meeting-assistant.md)

# Kohaliku AI-sõna assistent – prototüüp

Jätkake: 12. oktoober 2026. ForgeMind hüpotees "Outcome Agent", piiratud läbirääkimiste käigus. ei ole tõestatud konkurentsieelis ja ei ole mõõdetud aja säästmine ilma tegelikku võrdlusproovita.

## Kasutamine

1. Komanda palett → **KI-sõnavõtja** Eksperiment on standardne ja toimub konfigureerimise ajal või Portaalne ekspordi tagasivõtmine.
2. Aktiveerige kohalikke assistente. Ollama Tuleb eraldi `127.0.0.1:11434` käib; sobiv kohalik mudel peab olema juba paigaldatud. **Kohalikud mudelid** Valige mudel. MAKRIVO ei installita mudeleid/teenuseid, ei kasuta OpenAI/Cloud API-d ja ei kasuta API-tõusid. Ollama-Teenused on kasutaja juures; deklareeritud kaug- ja pilvimudelid on keeldutud.
3. Sisestage eesmärgid ja lisage selgelt programmid / dokumendid / veebilehed. Siirdub sihtmärk, ressursside ID, nimi ja tüüp; sihtkoht ja valitud failide sisu ei edastata täiendavalt; sihtmäär ise saadetakse sõnaselgelt. Veebilehe nimed sisaldavad ainult skeemi/hosti, kogu link jääb kohalikuks.
4. **Ki-projekti loomiseks**. Maksimaalne 90 Sekundid, 1024 Väljavõtted, piiratud vastus. siht / mudel / ressurss muutused lükkavad ettevaatuse; katkestamine / sulgemine lõpetab taotluse. mudel võib esitada ainult valitud ID-d. Tuntud ID-d, vabad juhised, täiendavad JSON-väljad ja teostatavad dokumendi tüübid lükkavat.
5. Ettepanek ja avatud punktid → **Editoris **. Esialgu on see salvestamata projekt. Säilitage ainult olemasolevas redaktoris ja rakendage vajaduse korral selgelt. Normaalne makrovalideerimine, eel- ja jälgimine, paus ja katkestamine jäävad vastutusele.

Toetatakse: rakendused käivitada ilma argumentideta, avada selgelt valitud kohalikud PDF/DOCX/XLSX/PPTX/MD, avada HTTP(S) veebisaidid, lülitada mikrofoni sõnaselgelt. Mitte standardmuutused, ühtegi sõnumit, kuupäevad, allalaadimised, skriptid, tasuta klõpsud või tekstisõnumid. Mitte kujutlusvõimelised soovid ilmuvad avatud punktidena, kui mudel neid nõuetekohaselt tunneb; kasutaja peab uurima oma eesmärkide täitmist. Dokumendi avamine ei ole automaatselt tõestatud.

## Võrdluseksperiment

**Manaalne seadme mõõtmine · Start / End** mõõdab täielikku manuaalset võrdlemist. Assistentsi vastuvõtmine mõõdab ajavahemikku, alates assisti avamisest kuni esimese salvestamiseni redaktoris; `generated` sisaldab aga ainult planeerimise kestust. Mitte võrrelda kahte erinevat kestust ajasäästuna.

Kohalikud sündmused on konfigureerimise järjekorras all `Experiments/meeting-experiment.jsonl`: ajakiri, juhusliku katsetuse/kahtluse ID, sündmus, kestus, korrigeerimine jah/ei. Ei ole sihtmärke, ressursside nimetusi, teed, mudeli vastuseid või dokumendi sisu. Fail pööratakse alates 2 MiB, eelmine faili jääb. Mõõtmisvigu ei tohi katkestada makro- ja konfigureerimise salvestamine.

See sisaldab: manuaalset baasiliini, mudeli genereerimist/virke/lülitamist, eelarve muutusi/lülitused, esimest korda salvestatud ja parandatud kohandusi ning sellest salvestatud kotidega tegelikku jooksja lõppu. `run_completed` tähendab ainult seda, et jooksja on lõpetanud; kinnitatud sihtmõju on katse käigus eraldi. Sama makro kordused ei ole uued sõltumatute seadmete katsed. Esialgset katset saab jätkata pärast uuesti käivitamist, sest katsetunnistus jääb salvestatud koodile. Duplitsed koerad jagavad selle päritolu; hindamise ajal rühmitada proovide ID.

Esimene eksperiment: 20 võrreldav kohtumiskoht või kaks nädalat vabatahtlike kasutajatega; esimene sisemine kasutamine. Võrdle manuaalset paigaldamise aega täielikuks assisteeritud paigaldamise ajaks; lisaks hinnatakse korrigeerimiskursust, jooksja vigu ja tegelikku testi tulemusi. Puuduvad parandused või kaitsepiirangute rikkumine. Kasutamise või makse valmisoleku ei ole registreeritud. Mitte automaatne telemetriaülekanded või hinnakujundus.

## Testimine

- Põhilised piirangud, tüübid, tundmatu/dupleeritud ID, skripti välistamine, pikad lingid, valikuline alt-konfiguratsioon, importimine/ekspord-opt-in, andmete säästlik mõõtmine ning ühekordne tagasilükkamine ja õige üleandmine.
- Transpord võltsitud mudeliga: Ollama JSON skeem, loopback sihtmärk, ei ole failipõhiseid, pilvimeid ja kaugmaterjale, kahjustatud/üleajalt vastust keeldunud, katkestamine.
- WPF-Smoke: dialoog vähendatud; disain edastamine ei muuda salvestatud kotid, ülevaade / salvestamine saab päritolu ja korrigeerib, tagasi lükata salvestada midagi.
- Sõltumatu ülevaade: eelarvamuse ja pikad lingid leitud ja parandatud; Regressiooni testid RED→GREEN.
- Release Build null hoiatusi/vead, kogu Core Suite ja WPF Smoke edukalt. Kogu komplekt Windows sisaldas esialgu keskendumisvahetust. Järgmine läbikukkumine ebaõnnestus tagasi lükatud taustamuutusega; test sõltub kontekstidest ja jääb muutumatuks. Ülejäänud Windows katsed, sealhulgas mudelite vedamine, on selgesõnaliselt välistatud eduanalüüs.
- Pakendi kontroll tuvastas olemasoleva vea lühikese aja jooksul avatud konfigureerimisfailide tuuma vahetamisel. Lugeja reprodutseerib sama juurdepääsu keeldunud viga; piiratud kordamine ainult tuumavahetus on kontrollitud RED→GREEN. Pidevad juurdepääsuvigad jäävad nähtavaks.
- Praegune iseseisev pakett `artifacts/release-20261012-184105`: WPF salvestuskorrektsiooni järel edukas, sealhulgas AI dialoog ja projekti vastuvõtmine/tagasi.
- Ei ole tõelist mudelit: kohalik Ollama teenus ei vastanud eksamile. Ei ole väiteid mudeli kvaliteedi, latentsuse, semantilise terviklikkuse või aja säästmise kohta. Varasemate versioonide seadmete/juhtimise/skalaerimise piirid jäävad kehtima.

Protokollide allikad : [Ollama Chat](https://docs.ollama.com/api/chat), [struktureeritud väljavõtted](https://docs.ollama.com/capabilities/structured-outputs), [Ametlikud tüübide määratlused, sealhulgas kaugmudeli andmed](https://github.com/ollama/ollama/blob/main/api/types.go).
