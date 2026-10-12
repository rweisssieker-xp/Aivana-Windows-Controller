<!-- generated-doc locale=ro-RO source=docs/superpowers/specs/2026-10-08-keyboard-mapping.md sha256=fb766a2a2b4698ae3e1c3f63b50bb4f5c605da3dd56c61c05fcc0832b5cf8aeb -->

> Traducere asistată automat. Structura, exemplele de cod și linkurile sunt verificate; aceasta nu reprezintă o revizuire de către un vorbitor nativ sau o aprobare juridică. Rapoartele istorice își păstrează datele și domeniul inițial. [Sursă originală](../../../superpowers/specs/2026-10-08-keyboard-mapping.md)

# Vizualizare fizică pentru Mapping

[English (US)](2026-10-08-keyboard-mapping.md) | [Deutsch](2026-10-08-keyboard-mapping.md) | [Français](2026-10-08-keyboard-mapping.md) | [Italiano](2026-10-08-keyboard-mapping.md)

## Obiectivul

Vizualizarea tastatură ar trebui să arate pozițiile de tastatură reale atunci când atribuie acțiuni. linii de același nivel nu sunt suficiente: Enter, Shift, Space, Navigation, Număr bloc și componentele de control ale producătorului sunt diferite.

## Implementarea

- Catalog cu ID-uri fizice separate, geometrie în unități de tastatură, legende imprimate și coduri de intrare Windows.
- ISO-DE și ANSI-US, fiecare ca format complet și TKL; în plus Corsair K100 RGB, care a fost recunoscută ca model conectat prin iCUE.
- ISO Enter cu L-formă, bloc de numere cu tastele de plus/între și zero larg; navigație și șiruri în pozițiile lor.
- Selectați modelul/layout-ul în mod explicit. USB VID/PID și numele modelului nu demonstrează o varietate de limbă. Profile generice și profilurile modelate pe baza documentelor producătorului sunt identificabile diferit; nu există garanție de compatibilitate cu milimetri sau firmware.
- Scan cod pe setul activ Windows pentru a nu face un clic fizic să pună accidental un alt buton, cum ar fi Y/Z atunci când se schimbă între US și DE.
- Admiterea deschide un proiect în editorul de acțiune existent. stocarea, verificarea conflictelor și respingerea rămân mecanismele existente.
- Butonele proprietari G, butonele de profil/loc și raza iCUE sunt afișate, dar fără adaptor de intrare implementat nu sunt emise ca hotkeys normale Windows.
- Localizările rămân hotkeys globale Windows; o viziune a dispozitivului nu înseamnă oprirea specifică a dispozitivului sau localizarea exclusivă a unei tastatură fizice individuale.

## Dovezi

[K100-Handbuch cu elemente de control](https://assets.corsair.com/image/upload/corsairmedia/sys_master/productcontent/AP_K100_RGB_Mechanical_QSG_AD.pdf), [Corsair ANSI / Declarația ISO](https://www.corsair.com/au/en/explorer/gamer/keyboards/ansi-vs-iso-whats-the-best-keyboard-layout/).

## Examinare

Testele de geometrie, variante și coduri de intrare; testele de bază și de bază Windows existente; WPF pentru afișarea și atribuirea fără efectuarea sau modificarea configurării utilizatorului real. Cercetarea producătorului documenta cerințele suplimentare de model/SKU, indiferent de catalogul de layout implementat în prezent.
