<!-- generated-doc locale=sr-Latn source=docs/VERIFICATION.md sha256=f19afb9e7b5ffac90c83167416ab3af0964a4481dd16b7357f8585480c6aae65 -->

> Mašinski potpomognut prevod. Struktura, primeri koda i veze se proveravaju; to ne znači pregled izvornog govornika ili pravno odobrenje. Istorijski izveštaji zadržavaju izvorne datume i obim. [Originalni izvor](../VERIFICATION.md)

# Ispitivanje – 8. oktobar 2026

[English (US)](../en-US/VERIFICATION.md) | [Deutsch](../de-DE/VERIFICATION.md) | [Français](../fr-FR/VERIFICATION.md) | [Italiano](../it-IT/VERIFICATION.md)

Aplikacija je izgrađena pod Windows x64 sa stabilnim .NET SDK 10.0.202 i objavljena kao nezavisna Windows-x64-Ausgabe.

## Automatski proveren

- Ukupna konstrukcija: nema upozorenja i grešaka.
- 19 Testovi ponašanja: Standardna konfiguracija, validacija, nule / duplice podataka, prioritet profila, uvoz / izvoz i povraćanje rezerve, makro poređenje i ponavljanje, prekid, dvostruko iscrpljenje, serijalizacija, nepromenljiva izvođenja i greške.
- Windows ispitivanje: Normalizacija hotkeja i alijas imena, uobičajene veličine strukture, stvarne tastature liste uređaja, sigurna registracija i reprodukcija, sintetički VM_HOTKEI za kocke i kocke, deaktivirana kockei registracija tokom simulacije.
- Nezavisni EKSE je pokrenut sa `--smoke-test`: sve pet WPF pogleda se vraćaju, kockalice se mogu postaviti / kopirati / izbrisati, dizajni ostaju prihvaćeni kada se refres, konfiguracija se čuva i ponovo preuzima. Ovaj test koristi izolovani privremeni katalog konfiguracije i ne vrši tekstualne ili medijske akcije.
- Dashbord, urednik i kompaktna prezentacija (900 × 720) su vizuelno provereni kao stvarni WPF rendering. Dizajn skladištenja, odbacivanje, kechel / makro poredak, nemački hotkei aliase, pojedinačne makro-ukidanja i korak napred su provereni.

## Hardvarne granice

Poslednji lokalni prolaz prepoznao je četiri Windows interfejsa. Ovaj broj opisuje HID/Windows interfejse, a ne obavezno četiri fizičke tastature.

Zvanični iCUE SDK je instaliran i nalazi se pored ECE. Živi test se povezuje sa iCUE i prepoznaje tastaturu Corsair sa informacijama o modelima i LED-u. Promene osvetljenja nisu izazvane automatski; uzorni tasteri i drugi proizvođači nisu testirani hardverskim uređajima. Generijski hotkei se primenjuju na internacionalnu tastaturu.

Tekstovi/medijski podaci nisu ubrizgani u druge otvorene korisničke programe. Istraživanje je sprovedeno na lokalnim strukturama podataka i logici grešaka/slobode; ponašanje u određenom ciljnom programu zavisi, između ostalog, od nivoa njegove dozvole.

## Ponavljaju

```powershell
dotnet build Aivana.slnx -c Release
dotnet run --project tests/Aivana.Tests -c Release
dotnet run --project tests/Aivana.Windows.Tests -c Release
dotnet publish src/Aivana.Desktop -c Release -r win-x64 --self-contained true -o artifacts/win-x64
& ./artifacts/win-x64/MAKRIVO.exe --smoke-test
```

GUI test piše rezultat i snimak ekrana nakon `smoke` pored proverenog EK. On je dizajniran kao dijagnostički režim i nakon završetka ne počinje trajno pozadinu.


## Proširena kontrola

Pored toga, proveren: uslove profila sa procesom i naslovima prozora, pretvarač bez rekurzivne ekspanzije, makro-pregled bez izvršenja, nova akcija validacija i kompatibilnost starih JSON datoteka. Narodno ispitivanje pokriva komandnu paletu sa registracijom, konfliktima, WM_HOTKEY distribucijom i oslobađanjem, kao i čistom rasporedom monitora i pristup klipbordu. Test WPF otvara i vraća stvarnu paletu, ispituje povratne / povratne i omiljene, ispituva čiste dizajne nakon rendera i uspešno završava proces.

Windows blokada, promene intervala i kretanja prozora nisu primenjeni na korisničke programe tokom testova. Prozorske akcije objavljuju asinhronne zahteve Windows; konkretni ciljni program može imati svoje minimalne veličine ili ograničenja. Zatvaranje profila i statistika izvršenja se primenjuju na tekuću sesiju.


Proširenje ispitivanja 12.10.2026: 30 Testiranje je prošao; Windows-Adapteri, uključujući oslobađanje tokom ubrizgavanja, pisano zaštićeni monitor i OBS-Status mock. WPF Proverite prenos / prenos, početak / zaustavljanje rekorda, pregled razmene profila, gestovi i simboli.: 27 Katalog sa 967 Ključ; Dokumentacija: 84 Stranice na četiri jezika. vlasnici tastera drugih proizvođača kao Corsair nije testiran na hardveru; generalna podrška uključuje uređajne standardne događaje F13–F24 i medijski pritisak. oblačni prevoz se vrši kroz izabrani sinhronizovani fasciklus; uvozi se proveravaju i ostaju deaktivirani.
