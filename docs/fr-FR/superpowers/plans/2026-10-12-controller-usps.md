# Plan de mise en œuvre des extensions

Réaliser les fonctions demandées : écrans, gestes, séquences, enregistrement, diagnostic, échange portable de profils et icône.

Étendre les couches Core/Windows/Desktop existantes. Les automates purs et la configuration portable restent dans Core, les intégrations natives dans Windows et les éditeurs modaux de brouillons dans Desktop. Conserver les modifications locales et la configuration déployée.

**Spec:** ../specs/2026-10-12-controller-usps-design.md

- [x] Service des écrans, persistance des cibles et état asynchrone ; vérifier identifiants absents et annulation.
- [x] Automate de gestes et couches maintenues ; vérifier transitions, répétition, réinitialisation et affectations immédiates.
- [x] Conditions, branches vers l’avant et règles d’erreur ; vérifier instantanés et annulation.
- [x] Enregistrement explicite des raccourcis avec révision du brouillon ; vérifier filtrage et libération des hooks.
- [x] Échange par dossier partagé avec révisions et aperçus ; vérifier conflits et affectations portables.
- [x] Touches standard par périphérique et diagnostic ; vérifier traitement, doublons et pause.
- [x] Ressources des icônes et apparence en pause ; vérifier l’exécutable publié.
- [x] Intégrer les accès palette/éditeur et les traductions ; exécuter les contrôles Core, Windows et WPF.
- [x] Publier dans un dossier intermédiaire séparé, préserver les paramètres et redémarrer l’application.
