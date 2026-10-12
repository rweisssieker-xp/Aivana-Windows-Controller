<!-- generated-doc locale=fr-FR source=docs/testing/2026-10-12-dpi-scaling.md sha256=dcf6d4c1a21eec9be9f07a38cab0aff308587c78b85fb2b24c69f04d28d82864 -->

> Traduction assistée par machine. La structure, les exemples de code et les liens sont vérifiés ; cela ne constitue ni une révision par un locuteur natif ni une validation juridique. Les rapports historiques conservent leur date et leur portée d'origine. [Source originale](../../testing/2026-10-12-dpi-scaling.md)

# Vérification DPI

MAKRIVO déclare PerMonitorV2 dans le manifeste de l'application intégrée. Le test WPF utilise GetWindowDpiAwarenessContext et AreDpiAwarenessContextsEqual pour vérifier le contexte effectif réel de la fenêtre principale.

Les tests de disposition/grille supplémentaires simulent les pourcentages 150 et 200 sur un affichage 1920 × 1080 avec réserve de barre des tâches : dimensions de fenêtre logique 1280 × 693 respectivement 960 × 520 DIP. Les cinq pages principales sont rendues au format PNG à 144 ou 192 DPI. Les fichiers sont appelés scale-simulated-150-* ou scale-simulated-200-*.

Ces tests ne modifient aucun paramètre de Windows. Ils ne valident pas la véritable commutation de moniteur WM_DPICHANGED, la configuration de moniteur mixte et la netteté réelle sur un écran différent. Les tests de mise à l'échelle physique restent ouverts.

L'avertissement de l'analyseur WinForms WFO0003 est supprimé exclusivement pour ce projet de démarrage WPF : WinForms est utilisé pour l'intégration de la zone de notification, WPF contrôle le démarrage ; Le DPI est défini via le manifeste avant la création de la fenêtre.

Documentation Microsoft : https://learn.microsoft.com/en-us/windows/win32/hidpi/setting-the-default-dpi-awareness-for-a-process

Inspection visuelle : le tableau de bord passe également à l'affichage empilé sous la hauteur de la fenêtre DIP 650 afin que les cartes et l'éditeur ne soient pas coincés dans une zone médiane trop petite.

Banc de test : version de version sans erreurs/avertissements ; A réussi le test de fumée complet WPF, y compris le PerMonitorV2-Fensterkontext réel et le 150/200-Prozent-Renderings simulé. Le 200-Prozent-Dashboardaufnahme a été inspecté visuellement après le réglage de la hauteur. git diff --check réussi.
