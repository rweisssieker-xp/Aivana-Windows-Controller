<!-- generated-doc locale=sl-SI source=docs/superpowers/plans/2026-10-08-controller-polish.md sha256=1a076a67eea31048f6305f30a121c474d592451d1ddcd9b46bc339fd8ec068f2 -->

> Strojno podprt prevod. Struktura, primeri kode in povezave se preverjajo; to ne pomeni pregleda maternega govorca ali pravne odobritve. Zgodovinska poročila ohranijo izvirne datume in obseg. [Izvirni vir](../../../superpowers/plans/2026-10-08-controller-polish.md)

# načrt za izboljšanje uporabnosti in zmogljivosti

[English (US)](2026-10-08-controller-polish.md) | [Deutsch](2026-10-08-controller-polish.md) | [Français](2026-10-08-controller-polish.md) | [Italiano](2026-10-08-controller-polish.md)

Uporabniška zahteva: izboljša videz, uporabnost in funkcionalnost; predhodna odobritev zahteva največ uporabnih podrejencev na potrebnih manjših modelih in samostojnem dokončanju.

## Obseg
Ohranite obstoječe profile in delajte z integracijo Corsair SDK. Izboljšajte obstoječo aplikacijo WPF namesto ustvarjanja ločenega prototipa.

- [x] Shell/dashboard delavec: odzivna postavitev, aktivna navigacija, bralne kontrole, kompaktna aktivnost/stanje, povračilo trak-and-drop, koristne prazne stanja in predloge. Lastni MainWindow.xaml, MainWindov.xam.cs in MainWind.Dashboard.cs.
- [x] Urejanje delavec: MainWindow.Editor.cs in opcijski urednik-samo delne datoteke. Razložite urejene ureditve, Save/Discard, validacija inline, posvečeni makro seznami s drag-and-drop, podvajanje korakov, zanesljivo hotkey shranjevanje in polja datoteke/URL.
- [x] Windows delavec: domača validacija, trdna imena naprave in zmogljivosti SDK, omejena modifikatorja in varno injiciranje; lastni Windows in Windows testi.
- [x] Starši: napredek v izvedbi, vmesniki upravljavca, ohranjanje konfiguracije, testiranje integracije, dejanski renderirani zasloni, publikacija/pakiranje in zamenjava delovne aplikacije.

## Interfejs pogodba
Nadzornik zagotavlja obstoječe APIs plus vrstico LastStatus, bool HasRunningActions, bool IsRunning(Guid), vrstico GetTileStatus(Guid) in vrstico ForegroundProcess, bool CorsairAvailable, IReadOnlyList<CorsairKeyboardInfo> CorsairKayboards, Task TestCorsairColorAsync(string), void OpenIcue(). Upravljavec razkriva bool HasUnsavedChanges, void DiscardDraft(), bool CommitDraft(); obstoječe polje/metodične imena ostanejo na voljo v vseh delnih razredih.

## sprejemanje
Ni ponovnega nastavitve dejanske uporabniške konfiguracije; veljavne spremembe shranite enkrat, neupravičeno oblikovanje ostane urejljivo, odstranite obnovite shranjene podatke. Native hotkey aliase potrjujejo brez injiciranja testnih vnosov v nezdružene okna. Tile in makro reorder testiran; vse poglede render na namiznih velikostih; zagon / neuspeh / popolno stanje vidna; dejanski model naprave SDK prikazano. Izdelava/testiranje in objavljen smok pas, in posodobljena aplikacija se začne.
