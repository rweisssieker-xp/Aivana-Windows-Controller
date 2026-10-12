<!-- generated-doc locale=sr-Latn source=docs/testing/2026-10-12-dpi-scaling.md sha256=dcf6d4c1a21eec9be9f07a38cab0aff308587c78b85fb2b24c69f04d28d82864 -->

> Mašinski potpomognut prevod. Struktura, primeri koda i veze se proveravaju; to ne znači pregled izvornog govornika ili pravno odobrenje. Istorijski izveštaji zadržavaju izvorne datume i obim. [Originalni izvor](../../testing/2026-10-12-dpi-scaling.md)

# DPI ispitivanje

MAKRIVO deklarisano PerMonitorV2 u ugrađenom manifestu za upotrebu. Test WPF ispituje sa GetWindowDpiAwarenessContext i AreDpiAwaressContextsEqual za stvarno efikasan kontekst glavnog prozora.

Simulirajte dodatne testove rasporeda / rastera 150 i 200 Procenta za jedan 1920 × 1080 Prijava sa rezervom zadataka: logična veličina prozora 1280 × 693 u odnosu na 960 × 520 PSNUMKS.Svaka peta glavna stranica 144 u odnosu na 192 DPI smanjena kao PNG. datoteke toplo skale-simulirani-150-* ili skale-simulirani-200-*.

Ovi testovi ne menjaju podešavanja Windows. Oni ne validiraju stvarnu WM_DPICHANGED promenu monitora, nema mešane konfiguracije monitora i nemaju stvarnu jačinu na drugom ekranu. Fizički testovi ostaju otvoreni.

Taj WinForms-Analitička upozorenja WFO0003 To je isključivo za WPF-Početna Projekat je potisnut: WinForms Koristi se za integraciju informacija, WPF Upravljanje pokretanjem; DPI se određuje putem manifesta pre kreiranja prozora.

Microsoft Dokumentacija: https://learn.microsoft.com/en-us/windows/win32/hidpi/setting-the-default-dpi-awareness-for-a-process

Provera: Tašbor se dodatno prebacuje na visinu 650 DIP-a, tako da se kartice i uređaji ne kreću u previše malu srednju površinu.

Test stanje: Izgradnja izlaza bez grešaka / upozorenja; potpuni test WPF Smuke, uključujući i stvarni PerMonitorV2-Fensterkontext i simuliranih 150/200-Prozent-Renderings. 200-Prozent-Dashboardaufnahme je vizuelno proveren nakon prilagođavanja visine. Git dif - proverite.
