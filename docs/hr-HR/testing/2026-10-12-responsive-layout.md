<!-- generated-doc locale=hr-HR source=docs/testing/2026-10-12-responsive-layout.md sha256=8b3710a27f806575e88b4d9a0c4e80de4f26539ea1b5826cfe291764228b4ca6 -->

> Strojno potpomognut prijevod. Struktura, primjeri koda i poveznice provjeravaju se; to ne znači pregled izvornog govornika ili pravno odobrenje. Povijesna izvješća zadržavaju izvorne datume i opseg. [Originalni izvor](../../testing/2026-10-12-responsive-layout.md)

# Odgovarajuća WPF površina

Glavne stranice usmjerene su prema korisnoj širini sadržaja, a ne prema fiksnoj širini prozora. Pod 900 DIP-ovim sadržajima, Dashboard i Editor su međusobno povezani. Pod 620 DIP-ovi su znakovi dvostruki. Pod 820 DIP širine prozora, stranica je uvrštena. Minimalna veličina: 640 × 480 DIPs.

Profili i postavke, kao i kontrolni elementi tipkovnice mijenjaju svoje kolone na temelju stvarne širine mreže. Stoga se postojeće kontrolne elemente preusmjeravaju umjesto da se njihove ulaznice ponavljaju. Akcijske kartice dijele raspon dostupnih linija. Uređaji se razbijaju. Stranica tipkovnice je vertikalno dostupna, a fizička tipkovnica zadržava svoje proporcije.

automatski WPF-Test: sve pet glavnih stranica 640 × 480, 900 × 620 i 1440 × 900; Screenshots po stranici; promjena kolona u oba smjera s pregledom dobivenog teksta. WPF-Jedinstvo – istina Windows-Skala u 150/200 Procjenjuje se da još uvijek nema više monitora..

Pregledno stanje: Release-Build bez pogrešaka / upozorenja. Potpuno ispitivanje smoke WPF sa svim veličinama, promjenama kolona, rasporedom tipkovnice i testiranjem jezika. Git diff - provjerite.
