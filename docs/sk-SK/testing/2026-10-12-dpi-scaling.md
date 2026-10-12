<!-- generated-doc locale=sk-SK source=docs/testing/2026-10-12-dpi-scaling.md sha256=dcf6d4c1a21eec9be9f07a38cab0aff308587c78b85fb2b24c69f04d28d82864 -->

> Strojovo podporovaný preklad. Štruktúra, príklady kódu a odkazy sa kontrolujú; nejde o kontrolu rodeným hovoriacim ani o právne schválenie. Historické správy si zachovávajú pôvodné dátumy a rozsah. [Pôvodný zdroj](../../testing/2026-10-12-dpi-scaling.md)

# Kontrola DPI

MAKRIVO deklaruje PerMonitorV2 v manifeste vloženej aplikácie. Test WPF používa GetWindowDpiAwarenessContext a AreDpiAwarenessContextsEqual na kontrolu skutočného efektívneho kontextu hlavného okna.

Simulujte dodatočné testy rozloženia/mriežky 150 a 200 percent na jednu 1920 × 1080 Displej s rezervou na paneli úloh: logické rozmery okna 1280 × 693 resp 960 × 520 DIPy. Zahrnutých je všetkých päť hlavných strán 144 resp 192 DPI vykreslené ako PNG. Súbory sa nazývajú simulované v mierke150-* alebo simulované v mierke-200-*.

Tieto testy nemenia žiadne nastavenia Windows. Neoverujú skutočné prepínanie monitora WM_DPICHANGED, zmiešanú konfiguráciu monitora a skutočnú ostrosť na inom displeji. Testy fyzického škálovania zostávajú otvorené.

Varovanie analyzátora WinForms WFO0003 je potlačené výhradne pre tento projekt spustenia WPF: WinForms sa používa na integráciu oblasti oznámení, WPF riadi spustenie; DPI sa nastavuje cez manifest pred vytvorením okna.

Dokumentácia Microsoft: https://learn.microsoft.com/en-us/windows/win32/hidpi/setting-the-default-dpi-awareness-for-a-process

Vizuálna kontrola: Prístrojová doska sa tiež prepne na skladaný displej pod výškou okna 650 DIPs, takže mapy a editor nie sú stlačené do príliš malej strednej oblasti.

Testovacia stolica: Uvoľnite zostavu bez chýb/upozornení; Prešiel úplným dymovým testom WPF vrátane skutočného PerMonitorV2-Fensterkontext a simulovaného 150/200-Prozent-Renderings. 200-Prozent-Dashboardaufnahme bol po nastavení výšky vizuálne skontrolovaný. git diff --kontrola prebehla.
