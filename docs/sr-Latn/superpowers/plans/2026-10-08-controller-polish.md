<!-- generated-doc locale=sr-Latn source=docs/superpowers/plans/2026-10-08-controller-polish.md sha256=1a076a67eea31048f6305f30a121c474d592451d1ddcd9b46bc339fd8ec068f2 -->

> Mašinski potpomognut prevod. Struktura, primeri koda i veze se proveravaju; to ne znači pregled izvornog govornika ili pravno odobrenje. Istorijski izveštaji zadržavaju izvorne datume i obim. [Originalni izvor](../../../superpowers/plans/2026-10-08-controller-polish.md)

# Plan kontrolera korisnosti i sposobnosti poboljšanja

[English (US)](2026-10-08-controller-polish.md) | [Deutsch](2026-10-08-controller-polish.md) | [Français](2026-10-08-controller-polish.md) | [Italiano](2026-10-08-controller-polish.md)

Zahtev korisnika: poboljšanje izgleda, korisnosti i funkcionalnosti; prethodno odobrenje zahteva maksimalne korisne sub-agente na potrebnim manjim modelima i autonomnom završetku.

## Oblast
Sačuvajte postojeće profile i rade Corsair SDK integraciju. Poboljšati postojeću aplikaciju WPF umesto da kreirate odvojeni prototip.

- [x] Šell/dašbord radnik: reaktivni raspored, aktivna navigacija, čitajuće kontrole, kompaktna aktivnost/status, reorganizacija trake, korisni prazni stanja i šabloni. Sadržaj: MainWindow.xaml, MainWindov.xam.cs i MainWind.Dashboard.cs
- [x] Redaktor radnik: MainWindow.Editor.cs i opcionalni uređaj-samo parcijalne datoteke. Ekskluzivne stadijumske uređaje, Sačuvaj/Diskard, validacija u liniji, posvećeni makro lista sa izvlačenjem i spuštanjem, duplirani koraci, pouzdani hotchei snimak i datoteke / URL polje.
- [x] Windows radnik: rodna validacija, čvrsti imena uređaja i SDK kapaciteti, ograničeni modifikator čeka i sigurne injekcije; sopstveni Windows i Windows testovi.
- [x] Roditelj: progres u izvršenju, interfejsi kontrolera, konfiguracija održavanja, testiranje integracije, stvarni snimak ekrana, objavljivanje / pakovanje i zamena tekuće aplikacije.

## Kontrakt interfejsa
Kontroler pruža postojeće API-a plus trake LastStatus, bool HasRunningActions, bool IsRunning(Putevi), trake GetTileStatus(Puti), trake ForegroundProcess, bool CorsairAvailable, IReadOnlyList<CorsairKeyboardInfo> CorsairKejboards, Task TestCorsairColorAsync(string), void OpenIcue(). Redaktor prikazuje Bool HasUnsavedChanges, void DiscardDraft(), Bool CommitDraft(); postojeće imena polja/metoda ostaju dostupne u svim delimičnim časovima.

## Prihvatanje
Nema restauracije stvarne korisničke konfiguracije; važeće promene sačuvate jednom, nevažeći dizajni ostaju uređujuće, odbacuje se obnavljanje sačuvanih podataka. Narodi su validisani bez injekcije test ulaza u nerelevantne prozore. Testirani su tabela i makro reorganizacija; svi pregledi se prenose u desktop veličinama; vidljiv je radni / neuspešni / potpuni status; prikazan je stvarni model SDK uređaja. Izgradite / testirajte i objavljujete pasoš za dim, a ažurirana aplikacija je pokrenuta.
