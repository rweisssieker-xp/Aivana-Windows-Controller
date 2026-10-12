<!-- generated-doc locale=ro-RO source=docs/testing/2026-10-12-github-publication.md sha256=ab4d40cd12047e2cdacad359a12febe16d5689a82f4f792b484b3d4e2430e02a -->

> Traducere asistată automat. Structura, exemplele de cod și linkurile sunt verificate; aceasta nu reprezintă o revizuire de către un vorbitor nativ sau o aprobare juridică. Rapoartele istorice își păstrează datele și domeniul inițial. [Sursă originală](../../testing/2026-10-12-github-publication.md)

# GitHub - Publicarea stării de dezvoltare MAKRIVO

Stat: 12. octombrie 2026. publicarea codului de sursă, nu a fost semnată.

- Release-Construcția soluției: de succes, zero avertismentele și erorile.
- Testele de bază: cu succes.
- WPF - Testul de smoke: cu succes; raportul de rezultate nou în ordinea de construcție locală.
- Prima completă Windows Suite: Fereastră de bază, audio, ecran și iCUE teste cu succes; apoi Timeout la răspunsul de lucru automat UI.
- Repetarea țintă `--extensions-only`: cu succes, inclusiv automatizarea UI, multidimensionalitatea, obiectivele dezactivate, excluderea parolelor, procesul de plugin și găzduirea mobilă reală HTTPS. Timeout-ul inițial nu a fost ascuns de o modificare a termenului; cauza nu a fost dovedită în mod definitiv.
- Următorul curs complet de recuperare Windows: cu succes, inclusiv ferestrele de bază și toate testele de extindere.
- Înainte de publicare, nu au fost găsite chei private tipice sau chei productive Stripe/GitHub/AWS în arhiva de sursă verificată.
- Fapte de construcție și date locale de orchestrare nu sunt publicate.

Amploarea funcțiilor și reducerile rămase: [Lista Featurilor](../FEATURES.md). Raporturile de testare anterioare își păstrează valabilitatea în timp; acestea nu sunt o garanție pentru orice hardware sau context desktop.
