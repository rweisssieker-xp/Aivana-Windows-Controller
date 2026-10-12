<!-- generated-doc locale=hr-HR source=docs/testing/2026-10-12-dpi-scaling.md sha256=dcf6d4c1a21eec9be9f07a38cab0aff308587c78b85fb2b24c69f04d28d82864 -->

> Strojno potpomognut prijevod. Struktura, primjeri koda i poveznice provjeravaju se; to ne znači pregled izvornog govornika ili pravno odobrenje. Povijesna izvješća zadržavaju izvorne datume i opseg. [Originalni izvor](../../testing/2026-10-12-dpi-scaling.md)

# DPI ispitivanje

MAKRIVO izjavljeno PerMonitorV2 u ugrađenom manifestu primjene. Test WPF provjerava učinkovit kontekst glavnog prozora uz pomoć GetWindowDpiAwarenessContext i AreDpiAwaressContextsEqual.

Simulacija dodatnih Layout/Raster testova 150 i 200 Procjenjuje se da je jedan 1920 × 1080 Prikaz s rezervom radnih mjesta: logička veličina prozora 1280 × 693 ili u odnosu 960 × 520 Sljedeći Članak Svi pet glavnih stranica 144 ili u odnosu 192 DPI smanjen kao PNG. datoteke su topli-simulirani-150-* ili Scale-simulated-200-*.

Ove testove ne mijenjaju postavke Windows. Oni ne validiraju stvarnu WM_DPICHANGED promjenu monitora, ne miješanu konfiguraciju monitora i nema stvarne oštrine na drugom zaslonu. Fizički testovi su otvoreni.

Tko je WinForms-Analitička upozorenja WFO0003 To je isključivo za WPF-Početni projekt potisnut: WinForms Koristi se za integraciju, WPF upravlja pokretom; DPI se određuje manifestom prije stvaranja prozora.

Microsoft dokumentacija: https://learn.microsoft.com/en-us/windows/win32/hidpi/setting-the-default-dpi-awareness-for-a-process

Vizija: Dashboard se dodatno prebacuje na visinu prozora 650 u prikazan prikaz kako bi se karte i urednici ne skidali u previše malom srednjem području.

Ispitivanje: Izgradnja izdanja bez pogrešaka / upozorenja; provedeno je punu ispitivanje WPF uključujući stvarnu PerMonitorV2-Fensterkontext i simuliranu 150/200-Prozent-Renderings. 200-Prozent-Dashboardaufnahme je vizualno provjerena nakon prilagodbe visine. Git diff - provjerite.
