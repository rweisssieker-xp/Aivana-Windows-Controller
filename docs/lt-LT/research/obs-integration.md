<!-- generated-doc locale=lt-LT source=docs/research/obs-integration.md sha256=a4e6ca4368b28daa73ffbdda4fb817863e3fa15d52da00d26bbd32ce1f05f9b7 -->

> Mašininio vertimo pagalba parengtas vertimas. Tikrinama struktūra, kodo pavyzdžiai ir nuorodos; tai nereiškia gimtakalbio peržiūros ar teisinio patvirtinimo. Istorinės ataskaitos išlaiko pradines datas ir apimtį. [Originalus šaltinis](../../research/obs-integration.md)

# OBS studijų integracija

[English (US)](obs-integration.md) | [Deutsch](obs-integration.md) | [Français](obs-integration.md) | [Italiano](obs-integration.md)

Aivana Naudokite OBS Studio obs-websocket v5 protokolas per ClientWebSocket; jis nėra trečiosios šalies OBS Klientų biblioteka. protokolas atidaromas su serveriu Hello (opcode) 0), Atsakymai su Identify (opcode) 1), Laukiame identifikuotų (opcode) 2), siunčiamas aprašymas (opcode) 6), Tada atitinka jo RequestResponse (opcode) 7) Pateikite ID. slaptažodžio autentifikavimas OBS'Dviejų etapų dokumentacija SHA-256 Iššūkis apskaičiavimas.

Galimybė **Įrankiai → WebSocket Serverio nustatymai** Į OBS. „Default Endpoint“ yra **WS://127.0.0.1:4455**. Įveskite veiksmus **OBS-WebSocket-Adresas** laukas kitam galutiniui. nuotolinis galutinis taškas turi naudoti wss://; nesifruotas ws:// yra priimamas tik pasikeitimo adresu.. Aivana Atsisakyti įterptų URL įrodymų.

Kai OBS Įjungti autentiškumą, išsaugoti savo slaptažodį iš OBS Veiksmo redaktorius. Aivana Įdėkite jį į dabartinę Windows Naudotojo autentiškumas vadinamas generiniu autentiškumu **Aivana.OBS:<Normalizuotas taškas>**. Jis niekada nėra parašytas į veiksmų ar nustatymų failą., **AIVANA_OBS_Pasakymas** lieka prieinama kaip atgal, kai galutinis taškas neturi išsaugoto slaptažodžio. OBS gali likti neautentiška, kuriuo atveju taip pat nereikia. vengti komandos eilutės argumentų dėl slaptažodžio, nes procesas komandos linijos gali būti patikrinamos.

Kontrolierių katalogas ir konfigūracija **Pradėkime**, **StopRekordas**, **ToggleRekordas**, **Paukščių rekordas**, **Rekordas**, **Pradėk**, **StopStream**, **ToggleStream**, **Atnaujinta programa**, ir **Togo įkrovimas**. Pirmasis aštuonis neturi jokių argumentų. „SetCurrentProgramScene“ naudoja redaktoriaus scenos pavadinimą; „ToggleInputMute“ naudojo jo įvesties pavadinimą..

Kiekviena akcija gauna šviežią ryšį su 10 sekundžių ribomis ir be automatinio atgalumo. Tai susiję su ištrynimo komandomis: po prašymo siunčiamas laikas arba nutrauktas ryšys gali palikti rezultatą nežinomą, o atšaukimas gali vėl ištrinti būklę. OBS prašymo klaidos grąžinamos su jų būklės kodu ir (kai pateikiama) serverio komentaras.

Protokolo nuorodos:

- [OBS WebSocket v5 protokolo specifikacija](https://github.com/obsproject/obs-websocket/blob/master/docs/generated/protocol.md)
- [OBS WebSocket autentiškumo formulė ir pranešimo kodai](https://github.com/obsproject/obs-websocket/blob/master/docs/generated/protocol.md#creating-an-authentication-string)
- [OBS WebSocket žiniasklaidos įvedimo prašymo įgyvendinimas ir priimtos veiksmų vertės](https://github.com/obsproject/obs-websocket/blob/master/src/requesthandler/RequestHandler_MediaInputs.cpp)

Protokolo įrašai ir veiklos pavadinimai buvo patikrinti prieš OBS projekto dokumentaciją 2026-10-08.
