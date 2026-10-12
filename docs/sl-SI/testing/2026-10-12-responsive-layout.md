<!-- generated-doc locale=sl-SI source=docs/testing/2026-10-12-responsive-layout.md sha256=8b3710a27f806575e88b4d9a0c4e80de4f26539ea1b5826cfe291764228b4ca6 -->

> Strojno podprt prevod. Struktura, primeri kode in povezave se preverjajo; to ne pomeni pregleda maternega govorca ali pravne odobritve. Zgodovinska poročila ohranijo izvirne datume in obseg. [Izvirni vir](../../testing/2026-10-12-responsive-layout.md)

# Odgovorna površina WPF

Glavne strani so usmerjene na uporabno širino vsebine, ne pa na določeno širino oken. Pod vsebino 900 DIPs sta Dashboard in Editor med seboj. Pod 620 DIPs postanejo značilnosti dvojno. Pod 820 razdaljo okna DIP je stranski trak vklopljen. Najmanjša velikost: 640 × 480 DIPs.

Profili in nastavitve ter kontrolniki tipkovnice spremenijo svoje stolpce glede na dejansko širino omrežja. Obstoječi kontrolniki se preoblikujejo, namesto da bi jih ponovno prilagodili. Akcijske kartice delijo razširjenost razpoložljivih linij. Orodje se razbije. Stran tipkovnice je vertikalno dostopna, fizična tipkovnica ohranja svoje razmerje.

Avtomatski WPF-Preizkus: vseh petih glavnih strani 640 × 480, 900 × 620 in 1440 × 900; Slike na strani; sprememba stolpcev v obeh smereh s preverjanjem prejema besedila. WPF-enote - resnično Windows-Skala za 150/200 Nekateri odstotek in več monitorjev še ni potrjen..

Preizkušeni položaj: Release-Build brez napake / opozoril. Popolno WPF preizkusite smoke z vsemi velikostmi, kolumnami, postavitvami tipkovnice in jezikovnimi testi. Git diff - preverjanje je bilo.
