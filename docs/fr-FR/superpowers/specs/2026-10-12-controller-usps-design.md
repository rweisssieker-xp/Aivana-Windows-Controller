# Extensions de commande MAKRIVO

L’utilisateur a demandé les neuf améliorations proposées et une icône reconnaissable dans la zone de notification. Les configurations existantes, notamment la luminosité sur G1/G2, doivent rester compatibles. Les modifications locales de marque et de documentation sont conservées.

## Livrables et limites

1. Les actions de luminosité ciblent tous les écrans compatibles ou un identifiant stable précis. Une cible explicite absente produit une erreur visible. Le tableau de bord lit de manière asynchrone et bornée la luminosité, le volume de sortie, le microphone et OBS.
2. Les touches spéciales acceptent appui immédiat, appui court, maintien et double appui, sans répétition automatique. Une touche de couche maintenue change temporairement les affectations ; son relâchement les rétablit. Les affectations immédiates existantes restent compatibles. Pause, changement de profil, déconnexion et annulation effacent les gestes en attente.
3. L’éditeur de séquences modifie conditions, branches vers l’avant et règles d’erreur (arrêter/continuer) dans un brouillon de tuile. Les conditions portent sur fichiers, processus et titres de fenêtres. Les cycles de saut illimités sont exclus.
4. L’enregistrement explicite capture les raccourcis et attentes, ignore les entrées injectées et s’arrête avec Échap. Les actions sont examinées avant adoption ; le texte ordinaire n’est pas enregistré.
5. Les touches spéciales standard par périphérique utilisent Windows Raw Input. Les touches propriétaires nécessitent une API constructeur prise en charge ou une affectation constructeur vers une touche standard. Les limites sont indiquées honnêtement. Le diagnostic explique profil, pause, conflits et état réel de l’adaptateur.
6. L’échange de profils utilise un dossier partagé choisi, y compris un dossier synchronisé par un client cloud existant. Les révisions empêchent les écrasements silencieux. La récupération présente un aperçu et importe des actions désactivées sans affectations matérielles. L’inclusion du contenu des actions est explicite. MAKRIVO ne conserve aucun identifiant cloud.
7. Les textes rejoignent les 27 catalogues existants ; DE/EN/FR/IT restent les langues minimales de documentation révisée. Les lacunes de traduction doivent être signalées.
8. Une marque de clavier M turquoise remplace l’icône Windows générique. La pause porte un badge ambre. Exécutable et fenêtre utilisent la même marque. L’infobulle indique état et profil.

## Vérification

Les contrôles Core couvrent compatibilité, instantanés, annulation, temporisation des gestes, branches/erreurs, filtrage de l’enregistrement et conflits de synchronisation. Les contrôles Windows couvrent l’inventaire des écrans en lecture seule, les relâchements SDK et le traitement Raw Input. Les contrôles WPF vérifient les nouveaux accès et ressources sans modifier la luminosité réelle, saisir dans une autre application ni écraser les profils. Le déploiement conserve une sauvegarde et la configuration G1/G2.
