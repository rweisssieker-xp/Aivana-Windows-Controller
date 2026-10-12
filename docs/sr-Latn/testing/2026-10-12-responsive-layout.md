<!-- generated-doc locale=sr-Latn source=docs/testing/2026-10-12-responsive-layout.md sha256=8b3710a27f806575e88b4d9a0c4e80de4f26539ea1b5826cfe291764228b4ca6 -->

> Mašinski potpomognut prevod. Struktura, primeri koda i veze se proveravaju; to ne znači pregled izvornog govornika ili pravno odobrenje. Istorijski izveštaji zadržavaju izvorne datume i obim. [Originalni izvor](../../testing/2026-10-12-responsive-layout.md)

# Odgovarajući WPF površina

Glavne stranice su usmerene na korisnu širinu sadržaja, a ne na čvrstu širinu prozora. Pod 900 širinom sadržaja DIP-a su Dashbord i Redaktor među sobom. Pod 620 DIPs su karakteristike dvostruke. Pod 820 širinom prozora DIP se ubacuje stranična linija. Minimalna veličina: 640 × 480 DIPs.

Profil i podešavanja, kao i tastatura kontrole, menjaju svoje kolone u zavisnosti od stvarne širine mreže. Postojeće kontrole se prerade umesto da se ponovo pričvršćuju. Akcione kartice dele dostupne širine linija. Instrumenti su prekinuti. Tastatura je vertikalno dostupna, fizička tastatura zadržava svoje proporcije.

Automatski WPF-Test: sve pet glavnih stranica 640 × 480, 900 × 620 i 1440 × 900; Screenshots po stranici; Promena kolona u oba pravca sa ispitivanjem primljenog teksta. WPF-Jedinstva - istina Windows-Skaliranje 150/200 Procent i više monitora još nisu validisani.

Test stanje: Izgradnja izdanja bez grešaka / upozorenja. Potpuni WPF test za mršavljenje sa svim veličinama, promenama kolona, rasporedom tastature i testiranjem jezika. Git dif - proverite.
