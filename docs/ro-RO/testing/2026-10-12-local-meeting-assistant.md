<!-- generated-doc locale=ro-RO source=docs/testing/2026-10-12-local-meeting-assistant.md sha256=56631ca85dca5f785448d12586b0741628728925a649f6124f7aebffb19bb615 -->

> Traducere asistată automat. Structura, exemplele de cod și linkurile sunt verificate; aceasta nu reprezintă o revizuire de către un vorbitor nativ sau o aprobare juridică. Rapoartele istorice își păstrează datele și domeniul inițial. [Sursă originală](../../testing/2026-10-12-local-meeting-assistant.md)

# Asistentul de informare locală – prototip

Starea: 12. octombrie 2026. hipoteza ForgeMind „Outcome Agent”, cursul de întâlnire limitat. nu există nici un avantaj competitiv dovedit și nici o economie de timp măsurată fără încercări reale de comparație.

## Utilizarea

1. Comanda → **ASIST de control al AI**. experimentul este standard și este retras la importul de configurare sau la exportul de portofel.
2. Activarea asistenților locali. Ollama trebuie să funcționeze separat pe `127.0.0.1:11434`; un model local corespunzător trebuie să fie deja instalat. ** Încărcați modelele locale** și selectați modelul. MAKRIVO nu instalează modele/servicii, nu contactează API OpenAI/Cloud și nu utilizează cheia API. Configurația offline și încrederea serviciului local Ollama sunt la dispoziția utilizatorului; modelele de distanță / cloud declarate sunt respinse.
3. Introduceți și adăugați în mod expres programe / documente / site-uri web. Se transmit obiectivul, ID-ul de resurse, denumirea și tipul; pasul de destinație și conținutul fișierelor selectate nu sunt transmise în plus; însuși înregistrarea de destinație este trimisă literalmente. Denumirile site-ului conțin doar schema/host, link-ul complet rămâne local.
4. ** Proiectarea lui AI** Maxim 90 secunde, 1024 puncte de ieșire, dimensiune limitată a răspunsului. Modificările de țintă/model/resurs resping previziunea; interzicerea/închiderea se termină. Modelul poate propune numai ID-uri selectate. Identificările necunoscute, comandele libere, câmpurile JSON suplimentare și tipurile de documente executabile sunt respinse.
5. Verificați punctele deschise → **În Editor**. Rezultatul este, în primul rând, un proiect neînregistrat. În primul rând, să se păstreze în editorul existent și să se desfășoare în mod explicit, dacă este necesar. Macrovalidarea normală, pre-/revizuirea, pauza și anularea rămân responsabile.

Sprijin: Programele sunt lansate fără argumente, se deschide PDF/DOCX/XLSX/PPTX/MD, se deschide site-urile HTTP(S), se oprește microfonul la dorința expresă. Nu există schimbări standard de headset, nu există mesaje, date, descărcări, scripturi, clicuri gratuite sau intrări text. Dorințele neimaginoase apar ca puncte deschise, cu condiția ca modelul să le recunoască corect; realizarea obiectivelor profesionale trebuie să fie examinată de utilizator. Documentele nu sunt verificate automat.

## Experimentul comparativ

**Măsura manuală · Start / Fin** măsoară cursul manual complet. Admiterea asistenților măsoară timpul de la deschiderea asistenței până la prima stocare în editor; `generated`, în schimb, conține doar durata de planificare. Nu comparați cele două durate diferite ca economii de timp.

Evenimentele locale se află în ordine de configurare în `Experiments/meeting-experiment.jsonl`: ștampă de timp, ID-ul de încercare aleatorie / ștampă, eveniment, durată, corectare da / nu. Nu există texte de țintă, denumiri de resurse, căi, răspunsuri la modele sau conținut de documente. Fișierul se rotește de la 2 MiB, un fișier anterior rămâne. Erorile de măsurare nu pot fi întrerupte de macro și de stocare a configurării.

Adăugați: bază manuală, modelare / eroare / dezactivare, modificări / închidere de prealabil, stocarea și corectarea pentru prima dată în editor, precum și condițiile reale de finalizare ale curentelor stocate din acesta. `run_completed` înseamnă doar că curentul a fost finalizat; efectele țintă confirmate sunt separate în cursul testului. Repetările aceluiași macro nu sunt noi încercări de instalare independentă. Experimentul inițial poate fi continuat după reînființare, deoarece ID-ul de încercare rămâne pe capcană stocată. Câinele duplicat împărtășește această origine; atunci când se evaluează după ID-ul de încercare.

Primul experiment: 20 facilități de întâlnire comparabile sau două săptămâni cu utilizatori voluntari; în primul rând, utilizarea internă. Comparați timpul manual de instalare cu timpul complet de instalare asistat; în plus, rata de corecție / defecțiune, erorile de runner și rezultatele de testare reale. În cazul în care nu există îmbunătățiri sau încălcări a frontierelor de protecție de a dezactiva switch-ul. Nu s-a demonstrat niciun utilizare sau disponibilitate de plată în repository. Nu există transmisie automată sau ajustare a prețurilor.

## Testarea

- Core: limitări, tipuri, ID-uri necunoscute/duplicate, excluderea scriptului, legături lungi, configurația opțională a altului, opțiunea de import/export, măsurarea economică a datelor, precum și refuzul unic și transferul corect.
- Transport cu model falsificat: schema JSON Ollama, scopul de întoarcere, nu există un pas de fișier în prompt, numele cloud și remotelii respins, răspunsul deteriorat / prelungit respins, abstracție.
- WPF-Smoke: Dialog redus; Transferul de proiect nu modifică capetele stocate, recenzii/speararea primește originea și înregistrează corectarea, respingerea nu stocă nimic.
- Independent Review: Refuz și link-uri lungi găsite și corecte; Teste de regresie RED→GREEN.
- Creați cu succes cu alerte / erori zero, întreaga suită de bază și WPF smoke. Suita completă Windows a fost inițial conținută, inclusiv schimbul de focalizare. Un nou curs a eșuat din nou în schimbarea refuzată; testul este contextual și rămâne nemodificat. Restul testelor Windows, inclusiv transportul de modele, sunt efectuate cu un test de succes explicit.
- Verificarea pachetului a detectat o eroare existentă în schimbul de date de configurare deschise pe termen scurt. Un blocaj de citire a reprodus aceleași erori de acces refuzat; repetarea limitată a schimbului nuclear este RED→GREEN verificat. Erorile permanente de acces rămân vizibile.
- Pachet independent actual `artifacts/release-20261012-184105`: WPF-Smoke după corectarea memoriei cu succes, inclusiv dialogul cu AI și acceptarea/refuzirea proiectului.
- Nu există un test real de model: serviciul local Ollama nu a răspuns la test. nu există declarații privind calitatea modelului, latenta, integritatea semantă sau economisirea timpului. limitele hardware/driver/scalarizare ale lansării anterioare rămân.

Sursele de protocol: [Ollama Chat](https://docs.ollama.com/api/chat), [Structura de ieșire](https://docs.ollama.com/capabilities/structured-outputs), [Definiții oficiale de tip, inclusiv date de modelare la distanță](https://github.com/ollama/ollama/blob/main/api/types.go).
