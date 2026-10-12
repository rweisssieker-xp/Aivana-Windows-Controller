<!-- generated-doc locale=sl-SI source=docs/superpowers/specs/2026-10-12-controller-usps-design.md sha256=4c3b2dbedb83af6bd48917515ae6559012afb6318ce69d1e32fb2765e0793327 -->

> Strojno podprt prevod. Struktura, primeri kode in povezave se preverjajo; to ne pomeni pregleda maternega govorca ali pravne odobritve. Zgodovinska poročila ohranijo izvirne datume in obseg. [Izvirni vir](../../../superpowers/specs/2026-10-12-controller-usps-design.md)

# MAKRIVO nadzorne razširitve

Uporabnik je zahteval vse 9 predlaganih izboljšav in prepoznavno ikono. Obstoječe konfiguracije, zlasti svetlost G1/G2, morajo ostati združljive. Obstoječe neobvezne oblike in dokumentacije spadajo v deljen delovni prostor in jih je treba ohraniti.

## Dostava in meje

1. Akcije svetlobe izberite vse podprte monitorje ali stabilno specifično ID monitorja. Izgubljeni izrecni cilji ne uspejo. Dashboard prikazuje trenutno svetlobo, izhodni volumen, stanje mikrofonov in stanje OBS prek omejenih asinkronnih bralcev.
2. Posebni ključi podpirajo takojšnjo tiskanje, držo in dvakratno tiskanje. Vztrajen sloj selektor začasno spremeni aktivno mapiranje in ga sprostite. Obstoječe neposredne vezi ohranjajo njihovo vedenje. Prekinitev, spremembe profila, razpoke in preklic jasnih žanrov.
3. Oblikovalec delovnega toka ureja pogoje, napredne podružnice in politiko stop/continue neuspeh transakcijsko v predlogu. Pogoji pokrivajo datoteke, procesne postopke in naslove oken. Brez neomejenih skokov.
4. Razloženo snemanje zajema kratke vrstice in čaka, ignorira injicirani vnos in se ustavi na Escape. Zapisane akcije se pregledajo pred sprejetjem; običajni besedilo ni zapisano.
5. Standardni posebni ključi za vsako napravo uporabljajo drugi proizvajalci Windows Raw Input. Lastniški ključi zahtevajo podprto ponudniško API ali ponudniško mapiranje na standardni ključ; nepodprta funkcionalnost je pošteno prikazana. Diagnoza pojasnjuje profil, odmore, zavezujoče konflikte in dejansko stanje adapterja.
6. Izmenjava profila uporablja uporabnikovo izbrano skupno mapo (vključno s mapami, ki jih sinhronizira obstoječi odjemalec v oblaku). Pregled preprečuje tiho prepisovanje. Pull predvideva spremembe in uvažajo ukrepe z odstranitvijo strojnih map. Vključitev vsebine je izrecna. Nobenih potrdil v oblaku ni shranjenih MAKRIVO.
7. UI vrstice spadajo v obstoječe jezikovne kataloge 27; DE/EN/FR/IT ostajajo najmanj pregledani jezikovni dokumenti. Prevajalske pomanjkljivosti morajo biti razkrite, ne pa zahtevane popolne.
8. Prepoznavna oznaka tipkovnice M nadomešča ikono Windows. Sprehodni način ima amber odmor. Izvedljiva / okensko blagovno znamko uporablja isto blagovno znamke. Vključuje stanje in profil.

## preverjanje

Osnovni preverjanja zajemajo nazaj združljivost, snapshots, preklic, časovnik gibanja, delovni pretok / napake, snemanje in sinhronizacijo konfliktov. Windows preverjanja pokrivajo samo seznam zaslona, dogodke izdaje SDK in Raw Input parsing. WPF smoke preverjanja prekrivajo nove vnose in vire brez spremembe resnične svetlosti monitorja, vpisovanje v drugo aplikacijo ali prepisovanje uporabniških profilov. Namestitev ohranja varnostno kopijo in konfiguracijo G1/G2.
