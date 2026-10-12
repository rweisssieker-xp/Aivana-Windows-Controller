<!-- generated-doc locale=ro-RO source=docs/superpowers/plans/2026-10-12-controller-usps.md sha256=b7af0d36209e04b458ce64a5db78ab7e552a84c5e9b450ea0bd97179a4eb7f68 -->

> Traducere asistată automat. Structura, exemplele de cod și linkurile sunt verificate; aceasta nu reprezintă o revizuire de către un vorbitor nativ sau o aprobare juridică. Rapoartele istorice își păstrează datele și domeniul inițial. [Sursă originală](../../../superpowers/plans/2026-10-12-controller-usps.md)

# Planul de extindere a controlului

**Obiectiv:** Implementarea monitorului solicitat, a gestului, a fluxului de lucru, a înregistrării, a diagnosticului dispozitivului, a schimbului de profil portabil și a îmbunătățirilor în urmărire.

**Arhitectura: ** Extinde straturile de bază/Windows/Desktop existente. Mașinile de stat și logica de configurare portabilă trăiesc în Core, integrările native în Windows și editorii de proiect modale în Desktop. Conservă editările de spațiu de lucru murdar existente și configurarea utilizatorului implementat.

**Spec:** ../specs/2026-10-12-controler-usps-design.md

- [x] Serviciul de monitorizare, persistența țintită și starea de viață asincronă; testarea ID-urilor inaccesibile și anularea.
- [x] Mașina de stat și straturile de ținut; borduri de testare, repetare, reîncărcare și legături imediate existente.
- [x] Condițiile fluxului de lucru, ramurile avansate și politica de eroare; snapshots de testare și anulare.
- [x] Înregistrare explicită cu revizuire a proiectului; filtrare de testare și dezvăluire.
- [x] Schimbarea profilului de dosar partajat cu verificări de revizuire și previziuni; conflicte de testare și hărți portabile.
- [x] Chei speciale generice pe dispozitiv standard și diagnostic; testare, duplicare și comportament pauză.
- [x] Resursele de tip Tray/window/executable și apariția suspendată; verificați resursele publicate în executabil.
- [x] Integrați toate punctele de intrare de paletă / editor și textul localizat; executați controalele Core, Windows și WPF.
- [x] Publicați într-un director separat, păstrați setările utilizatorului și reîncepeți aplicația actualizată.
