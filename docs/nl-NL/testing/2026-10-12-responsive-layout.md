<!-- generated-doc locale=nl-NL source=docs/testing/2026-10-12-responsive-layout.md sha256=8b3710a27f806575e88b4d9a0c4e80de4f26539ea1b5826cfe291764228b4ca6 -->

> Machinaal ondersteunde vertaling. Structuur, codevoorbeelden en links worden gecontroleerd; dit is geen taalkundige controle door een moedertaalspreker of juridische goedkeuring. Historische rapporten behouden hun oorspronkelijke datum en reikwijdte. [Originele bron](../../testing/2026-10-12-responsive-layout.md)

# Responsieve WPF-interface

De hoofdpagina's zijn gebaseerd op de bruikbare inhoudsbreedte, niet op een vaste vensterbreedte. Onder de inhoudsbreedte 900 DIP's bevinden het dashboard en de editor zich onder elkaar. Onder 620 DIP's staan ​​de kerncijfers in twee kolommen. De zijbalk is samengevouwen onder de vensterbreedte van 820 DIP. Minimumgrootte: 640 × 480 DIP's.

Profiel- en instellingenvensters en toetsenbordbedieningen wisselen hun kolommen op basis van de werkelijke rasterbreedte. Bestaande bedieningselementen worden opnieuw ingedeeld in plaats van dat er nieuwe items worden gemaakt. Actiekaarten verdelen de beschikbare lijnbreedte. Werkbalken breken. De toetsenbordzijde is verticaal bereikbaar, het fysieke toetsenbord behoudt zijn proporties.

Automatische WPF-test: alle vijf de hoofdpagina's op 640 × 480, 900 × 620 en 1440 × 900; Screenshots per pagina; Kolomverandering in beide richtingen met controle van de ontvangen tekst. DIP's zijn logische WPF-eenheden. Echte Windows-schaling op 150/200 procent en meerdere monitoren zijn nog niet gevalideerd.

Testbank: release build zonder fouten/waarschuwingen. Volledige WPF-rooktest geslaagd met alle formaten, kolomwijzigingen, toetsenbordindelingen en taalcontroles. git diff --check geslaagd.
