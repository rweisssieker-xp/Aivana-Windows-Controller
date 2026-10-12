<!-- generated-doc locale=ro-RO source=docs/superpowers/plans/2026-10-08-windows-controller.md sha256=10a5ba49bbefc617bce40563c668e3c4628647822117087deba766713fb9b3af -->

> Traducere asistată automat. Structura, exemplele de cod și linkurile sunt verificate; aceasta nu reprezintă o revizuire de către un vorbitor nativ sau o aprobare juridică. Rapoartele istorice își păstrează datele și domeniul inițial. [Sursă originală](../../../superpowers/plans/2026-10-08-windows-controller.md)

# Windows Planul de punere în aplicare a controlului

[English (US)](2026-10-08-windows-controller.md) | [Deutsch](2026-10-08-windows-controller.md) | [Français](2026-10-08-windows-controller.md) | [Italiano](2026-10-08-windows-controller.md)

> Pentru lucrătorii agenți: utilizarea proprietății izolate paralele; utilizatorul a solicitat în mod explicit implementarea completă cu sub-agentele utile maxime.

**Obiectiv: ** Livrați panourile native Windows în designul aprobat.
**Arhitectura:** Configurație de bază și execuție anulată; Windows adaptoare; WPF compoziție de desktop.
**Tech Stack: ** .NET 10, WPF, Win32, JSON. Nici o dependență plătită.
**Spec: ** ../specs/2026-10-08-Windows-controller-design.md

## Constrângeri globale
UI germană; stocare locală; Windows x64; statutul de capacitate hardware adevărată; fără înregistrare la tastatură; fără start automat standard.

## Recenzie Focus
Importurile defectuoase nu trebuie să înlocuiască setările salvate; modificatorii păstrați nu trebuie să contamineze producția; hardware-ul neplugit nu trebuie să se prăbușească; închiderea trebuie să păstreze disponibilitatea tray; macro-urile anulate trebuie să elibereze resurse.

## Sarcini și proprietate
- [x] Agent de bază: src/Aivana.Core și teste/Aivana.Teste. Modele, persistența JSON, validarea, selecția de profil și MacroRunner. Începeți testele comportamentale eșuante, apoi implementați și efectuați toate testele de bază.
- [x] Windows agent: src/Aivana Windows RegisterHotKey, inventariul dispozitivului, SendInput, procesul de predare si adaptorul optional Corsair de iluminat. Verificați dimensiunile structurilor native și curățați înregistrarea cu teste de fumat sigure.
- [x] agent GUI: src/Aivana.Desktop/MainWindow.xaml și .cs plus Ajutorele de UI numai. Dark dashboard, editor complet, profiluri, setări, tastatură de ansamblu și editor macro. Construiți-vă atunci când există contracte.
- [x] Parent: contractele partajate, fișierele de proiect, compoziția DesktopController, ciclul de viață al aplicației/tray, dialogurile de import/export, documentația, verificările de integrare, publicarea și testul de fumat al aplicației.

## Interfețe partajate
Contractele live în Core/Models.cs. Implementarea controlorului trăiește în Desktop/DesktopController.cs. UI cheamă metode de control prin această clasă concretă. Agenții primesc semnături exacte în sarcinile lor. Fiecare are fișiere separate. Părinții rezolvă eșecurile de proiect după ce toți agenții au terminat.

## Verificare și livrare
- [x] dotnet build Aivana.slnx -c Release: erori zero.
- [x] dotnet run --project tests/Aivana.Tests -c Release: controlul comportamental trece.
- [x] dotnet publish src/Aivana.Desktop -c Release -r win-x64 -autocontenit real -o artefacte/win-x64.
- [x] lansarea publicată executabilă, inspectarea Windows UI și stabilitatea procesului, exercitarea unui profil sigur / editare / flux de persistență.
- [x] Scrieți README cu instrucțiuni de construcție / pornire și limitări hardware; inspecții de înregistrare și teste hardware rămase.
