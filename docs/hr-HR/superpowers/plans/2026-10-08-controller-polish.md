<!-- generated-doc locale=hr-HR source=docs/superpowers/plans/2026-10-08-controller-polish.md sha256=1a076a67eea31048f6305f30a121c474d592451d1ddcd9b46bc339fd8ec068f2 -->

> Strojno potpomognut prijevod. Struktura, primjeri koda i poveznice provjeravaju se; to ne znači pregled izvornog govornika ili pravno odobrenje. Povijesna izvješća zadržavaju izvorne datume i opseg. [Originalni izvor](../../../superpowers/plans/2026-10-08-controller-polish.md)

# Plan upravljanja korisnošću i kapacitetom

[English (US)](2026-10-08-controller-polish.md) | [Deutsch](2026-10-08-controller-polish.md) | [Français](2026-10-08-controller-polish.md) | [Italiano](2026-10-08-controller-polish.md)

Zahtjev korisnika: poboljšati izgled, upotrebljivost i funkcionalnost; prethodno odobrenje zahtijeva maksimalno korisne podakente na potrebnim manjim modelima i autonomnom završetku.

## Obrazac
Održavanje postojećih profila i radna integracija Corsair SDK. Poboljšanje postojeće aplikacije WPF umjesto stvaranja odvojenog prototipa.

- [x] Shell/dashboard radnik: odgovarajuće raspored, aktivna navigacija, čitljiva kontrola, kompaktna aktivnost/stanje, povlačenje, korisni prazni uvjeti i predmeti. Svojim MainWindow.xaml, MainWindov.xam.cs i MainWind.Dashboard.cs.
- [x] Editor radnik: MainWindow.Editor.cs i opcionalni urednik samo djelomične datoteke. Objasniti redigiranje, sačuvati/izbrisati, validaciju u liniji, posvećeni makro popis s drag-and-drop, duplikirati korake, pouzdan hotkey snimanje i datoteke/URL polja.
- [x] Windows radnik: domaća validacija, robustna imena uređaja i SDK sposobnosti, ograničeni modifikator čekanja i sigurna injekcija; vlastiti Windows i Windows testovi.
- [x] Roditelj: napredak u izvršenju, kontrolorske sučelje, očuvanje konfiguracije, testiranje integracije, stvarne snimke zaslona, objavljivanje/pakiranje i zamjena pokretne aplikacije.

## Interfejs ugovor
Kontrolor pruža postojeće APIs plus string LastStatus, bool HasRunningActions, bool IsRunning(Guid), string GetTileStatus(Guid) i string ForegroundProcess, bool CorsairAvailable, iReadOnlyList<CorsairKeyboardInfo> CorsairKejboards, Task TestCorsairColorAsync(string), void OpenIcue(). Redatelj prikazuje bool HasUnsavedChanges, void DiscardDraft(), bool CommitDraft(); postojeće polje/metoda imena ostaju dostupne u svim dijelovima razreda.

## Prihvaćanje
Nema ponovnog postavljanja stvarne korisničke konfiguracije; valjane fazne promjene sačuvaju jednom, nevažeći nacrt ostaje uređiv, diskard vraćaju sačuvane podatke. Native hotkey aliase validirani bez ubrizgavanja testnih ulaza u nepovezane prozore. Tile i makro reorder testiran; sve poglede render u desktop veličini; u tijeku/neuspješno/potpuno stanje vidljivo; prikazani su stvarni modeli uređaja SDK. Izgradnja/testiranje i objavljivanje dima, a ažurirana aplikacija je pokrenuta.
