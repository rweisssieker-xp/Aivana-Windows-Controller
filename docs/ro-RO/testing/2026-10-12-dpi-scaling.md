<!-- generated-doc locale=ro-RO source=docs/testing/2026-10-12-dpi-scaling.md sha256=dcf6d4c1a21eec9be9f07a38cab0aff308587c78b85fb2b24c69f04d28d82864 -->

> Traducere asistată automat. Structura, exemplele de cod și linkurile sunt verificate; aceasta nu reprezintă o revizuire de către un vorbitor nativ sau o aprobare juridică. Rapoartele istorice își păstrează datele și domeniul inițial. [Sursă originală](../../testing/2026-10-12-dpi-scaling.md)

# Examenul DPI

MAKRIVO declarată PerMonitorV2 în manifestul de aplicare încorporat. Testul WPF utilizează GetWindowDpiAwarenessContext și AreDpiAwarenessContextsEqual pentru a verifica contextul efectiv al ferestrului principal.

Testele suplimentare de layout/raster simulează 150 și 200 procente pe un display 1920 × 1080 cu rezerve de fereastră: dimensiunea logică a ferestrelor 1280 × 693 sau 960 × 520 DIPs. Toate cele cinci pagini principale sunt reduse la 144 sau 192 DPI ca PNG. Fișierele sunt denumite scale-simulate-150-* sau scale-simulate-200-*.

Aceste teste nu modifică setările Windows. nu validă schimbarea reală a monitorului WM_DPICHANGED, nu configurează un monitor mixt și nu are o rezistență reală pe un alt ecran.

Această WinForms-Avertismentul analitic WFO0003 Se face exclusiv pentru acest WPF-Proiectul iniţial este oprit: WinForms Se utilizează pentru integrarea informațiilor, WPF controlul de pornire; DPI este stabilit prin manifest înainte de a crea fereastră.

Microsoft - Documentare în număr: https://learn.microsoft.com/en-us/windows/win32/hidpi/setting-the-default-dpi-awareness-for-a-process

Verificarea vizuală: Dashboard-ul se întoarce înălțimea fereastră 650 la afișarea încorporată, astfel încât hărțile și editorul să nu fie strânse într-o zonă prea mică.

Testarea: Release-build fără erori/avertări; completă WPF testare de smoke, inclusiv PerMonitorV2-Fensterkontext și simulate 150/200-Prozent-Renderings. 200-Prozent-Dashboardaufnahme a fost verificat vizual după adaptarea înălțimii. git diff --check a trecut.
