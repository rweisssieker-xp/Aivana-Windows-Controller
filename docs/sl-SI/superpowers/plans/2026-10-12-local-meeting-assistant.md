<!-- generated-doc locale=sl-SI source=docs/superpowers/plans/2026-10-12-local-meeting-assistant.md sha256=1f105661dc4244fce4110ddae1484b4ce819e2d4c6945ada84a27704d95db72a -->

> Strojno podprt prevod. Struktura, primeri kode in povezave se preverjajo; to ne pomeni pregleda maternega govorca ali pravne odobritve. Zgodovinska poročila ohranijo izvirne datume in obseg. [Izvirni vir](../../../superpowers/plans/2026-10-12-local-meeting-assistant.md)

# Lokalni pomočnik za srečanje – 2026-10-12

Pooblaščena z "ok" in obstoječo naročilo brez povpraševanja. Obstajajoče industrije / spremembe. Kratek prototip, brez samodejnega izvajanja.

1. Core: izbrane vire z ID-ji; načrt samo vire ID, mikrofon, odprte točke. Stroge meje velikosti / tipa / ID; kompilator ustvarja le Launch / OpenPath / URL / volumen. Preizkusite najprej: neznane ID, izvedljive dokumente, ne-HTTP URL, duplikate, prazne načrte, funkcije.
2. Windows: Ollama loopback-only HTTP brez proxy/redirect, JSON shema, token/odgovor omejitve, prekinitev in 90s-Zeitlimit; ni samodejno namestiti/preklicati, ni ključa za oblak/API. Fake prevoz testov preveriti poizvedbo in napake; pravi model le, če je lokalno.
3. WPF: ukazna paleta, privzeto deaktivirani preklopnik, izbira modela iz lokalne storitve, cilj in izrecni seznam virov, predvideni/odprti točki, prekinitev, prenos v obstoječem načrtu brez izvajanja. Spremembe po predhodnem pregledu jih onemogočajo; pregled profilov.
4. Eksperiment: ročno ustavitev in lokalne dogodke (brez cilja/povečanja), sprejemanje/korekcija/odbijanje in dejanske makro stanje. Ni dokazov o uspehu samo iz dokončane; obstoječi pregled rezultatov ločeno.
5. Preizkušnje: Core, Transport, WPF Smoke; brez uporabniških akcij. Poročilo označuje pomanjkanje živega testa in še naprej odprto stare Release vrata. Obstoječe šestičlanska izdaja ne preoblikuje kot AI sposoben.

Ollama-Viri protokola: https://docs.ollama.com/api/chat in https://docs.ollama.com/capabilities/structured-outputs (12.10.2026).

## Izvedbeni status

- Core Compiler/Feature/Data varčevanje zeleno po začetnem RED.
- Lokalni modelni stranka je zelena po začetnem kompilacijskem RED; daljinski Alias, ime oblaka, shema, pomanjkanje poti v prompt, prekomerno razporejeno in prekinjeno s ponarejenim trgovcem.
- WPF-Handoff: ne shranjeno, ne izvajanje; pregled/shranjevanje/korekcija in zavrnitev v smoke zelena.
- Fresh-context reviewer /root/meeting_review (read-only): dve P2-Funde, zavrnjeni pregled in dolge povezave. Obe so popravili z regresije RED→GREEN. Ni več pomembnih odkritj.
- Release-Build null opozorila/greške, vse jedrske skupine in polno Windows suite brez Skip zelena. Native Focus preverjanje zdaj tudi zelena; obstoječa iCUE tipkovnica povezana. Nobenih dejanskih uporabniških programov, ki se začnejo s modelom.
- Nič živega-Ollama Na voljo; ni nameščen model/služba in ni uporabljen oblak/ključ. kakovost/časovno varčevanje ostane eksperimentalno.
- Selfcontained paket s spremljevalnimi programi in novimi navodili za uporabo/izkušnje pod artifakti/release-20261012-183635. Obstajajoči predmeti in nepovratne spremembe; brez pomirjanja/push.

- Paket Smoke je pokazal dostop zavrnjen na ConfigStore atomskim File.Move; sveže ponovitev je kasneje ostala vezana v obstoječem pogovoru o napaki in je bila namerno končana. Regresija z FileShare.Read (preberite, ne delite) ponovi napako File.Move. Omejeno povračilo samo za Win32 5/32/33 do 500ms RED→GREEN; trajno pomanjkanje pravic ostane napake. Novi paket se ponovno pregleda.

- Končni polni Windows ponovni tečaj znova ne uspe z zavrnjeno spremembo osredotočenosti; ni novega napake v kompilatorju/modelu. Preostala soba --skip-foreground-fixture zelena. Ni stabilnega zelena za kontekstno temeljni test trdijo. Trenutni paketni pot Artifacts/release-20261012-184105

- Trenutni samostojni paket artifakta/release-20261012-184105: WPF smoke vključno z AI dialog, nezahtevno prenosa urednika, popravek merjenje in uspešno zavrnitev (izhod 0). Po tem testu ni bilo nobenih sprememb.
