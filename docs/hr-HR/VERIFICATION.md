<!-- generated-doc locale=hr-HR source=docs/VERIFICATION.md sha256=f19afb9e7b5ffac90c83167416ab3af0964a4481dd16b7357f8585480c6aae65 -->

> Strojno potpomognut prijevod. Struktura, primjeri koda i poveznice provjeravaju se; to ne znači pregled izvornog govornika ili pravno odobrenje. Povijesna izvješća zadržavaju izvorne datume i opseg. [Originalni izvor](../VERIFICATION.md)

# ispitivanje – 8. listopad 2026

[English (US)](../en-US/VERIFICATION.md) | [Deutsch](../de-DE/VERIFICATION.md) | [Français](../fr-FR/VERIFICATION.md) | [Italiano](../it-IT/VERIFICATION.md)

Aplikacija je izgrađena pod Windows x64 sa stabilnim .NET SDK 10.0.202 i objavljena kao samostalna Windows-x64-Ausgabe.

## Automatizirano provjeravanje

- Sveobuhvatna konstrukcija: bez upozorenja i pogrešaka.
- 19 Core ponašanje test: standardna konfiguracija, validacija, nula/duplikat podataka, profil prioritet, uvoz/izvoz i povrat sigurnosti, makro redoslijed i ponavljanje, razbijanje, dvostruko iscrpljenje, serijaliziranje, nepromjenjiv rezultat snapshot i korak pogreške.
- Windows ispitivanje: Hotkey normaliziranje i alia imena, native veličine strukture, stvarne tipkovnice popis uređaja, sigurna registracija i ponovnog oslobađanja, sintetički WM_HOTKEY za kuhanje i razbijanje, deaktivirani kuhanje registracije tijekom simulacije ulaza.
- Neovisni EXE je pokrenut s `--smoke-test`: sve pet WPF pogleda se vraćaju, kutije se mogu postaviti/kopirati/izbrisati, dizajni se zadržavaju tijekom osvježavanja, konfiguracija se čuva i ponovno preuzima. Ovaj test koristi izolirani privremeni katalog konfiguracija i ne provodi tekst ili medijske akcije.
- Dashboard, urednik i kompaktni prikaz (900 × 720) vizualno su provjereni kao pravi WPF rendering. Proizvodi za skladištenje, odbacivanje, redoslijed kutije/makro, njemački hotkey savez, pojedinačni makro otpad i korak napredak su provjereni.

## Hardware ovisne granice

Posljednji lokalni prolaz je prepoznao četiri Windows tipkovnice. Ovaj broj opisuje HID/Windows sučelje, a ne nužno četiri fizičke tipkovnice.

Službeni iCUE SDK je instaliran i nalazi se pored EXE-a. Živi test povezuje se s iCUE i prepoznaje Corsair tipkovnicu s informacijama o modelu i LED-u. Promjene u svjetlu nisu automatski izazvane; tipke za iscjedak i drugi proizvođači nisu testirane na hardveru. Generički hotkeys primjenjuju se međusobno.

Tekst/medijski podaci nisu ulazili u druge otvorene korisničke programe. Native strukture podataka i logika pogrešaka/slobodnih podataka su testirane; ponašanje u određenom ciljanom programu ovisi, između ostalog, o razini njegove prihvatljivosti.

## Ponavljanje

```powershell
dotnet build Aivana.slnx -c Release
dotnet run --project tests/Aivana.Tests -c Release
dotnet run --project tests/Aivana.Windows.Tests -c Release
dotnet publish src/Aivana.Desktop -c Release -r win-x64 --self-contained true -o artifacts/win-x64
& ./artifacts/win-x64/MAKRIVO.exe --smoke-test
```

GUI test prikazuje rezultate i snimke zaslona nakon `smoke` uz ispitivanje EXE. On je mislio kao način dijagnoze i nakon završetka ne započinje trajno pozadinu.


## proširena kontrola

Dodatno provjereni: uvjeti profila s procesom i naslovima prozora, varijabilnost bez rekursivne proširenja, makrovidanje bez provedbe, nova validacija akcije i kompatibilnost starih JSON datoteka. Native ispitivanja pokrivaju paletu zapovijedi s registracijom, sukobima, WM_HOTKEY dispatchom i oslobađanjem, kao i čistim raspodjelom monitora i pristupom prema potrebi. Test WPF otvara i vraća stvarnu paletu, provjerava povratne/povratne i omiljene, provjeruje čiste dizajne nakon vraćanja i uspješno završava proces.

Windows blokiranje, promjene u usporedbi i pokreti prozora nisu primijenjeni na korisničke programe tijekom testiranja. Akcije prozora objavljuju Windows zahtjeve asinkronno; konkretni ciljni program može imati svoje minimalne veličine ili granice. Zabrana profila i statistike provedbe primjenjuju se na tekuću sjednicu.


Proširenje ispitivanja od 12.10.2026: 30 Core ispitivanja; Windows-Adapteri, uključujući ispuštanje tijekom ulaska, pisano zaštićeno otkrivanje i OBS-Status-Mock je izgrađen. WPF provjerava prijem/izlazak, započetak/ustavljanje rekorda, promjena profila, gesta i simboli.: 27 Katalog s 967 Ključne riječi: dokumentacija: 84 Stranice na četiri jezika. vlasnici gumbova drugih proizvođača Corsair nisu testirane na hardveru; opća podrška uključuje standardne događaje F13–F24 Cloud transport se obavlja putem odabranog sinhroniziranog dosjea; uvoz se provjerava i ostaje deaktiviran.
