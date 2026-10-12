<!-- generated-doc locale=es-ES source=docs/testing/2026-10-09-usp-release.md sha256=96a5b23b67032ef07f2f5dd93e9585e7bab0a522e5096ffc9f4d020823c4264e -->

> Traducción asistida por máquina. Se comprueban la estructura, los ejemplos de código y los enlaces; esto no constituye una revisión por hablantes nativos ni una aprobación jurídica. Los informes históricos conservan sus fechas y alcance originales. [fuente original](../../testing/2026-10-09-usp-release.md)

# Asistencia de teclado, superposición y comentarios de ejecución.

[English (US)](2026-10-09-usp-release.md) | [Deutsch](2026-10-09-usp-release.md) | [Français](2026-10-09-usp-release.md) | [Italiano](2026-10-09-usp-release.md)

Se implementaron las tres mejoras priorizadas en la aplicación Windows existente:

- Asistente de claves especiales: inventario real de dispositivos/claves compatibles con iCUE, captura temporal de claves G, verificación de configuración exitosa, validación de conflictos y borrador de acción no guardado. La reversión de captura, la invalidación de devolución de llamada en cola y la restauración de enlace normal cubren errores y cierre de diálogo.
- Superposición de teclado: geometría de referencia física, enlaces de perfil activo, actualización/edición de perfil en vivo y resaltado de teclas Windows normal. Mantenga presionada la tecla Ctrl+Alt+F1 o alternar entre la página de paleta/teclado. No activable, con clics y ausente en la barra de tareas.
- Comentarios de ejecución: fases escritas en espera/en ejecución/paso/completado/cancelado/fallido/rechazado, progreso del paso/repetición y un brindis no activado. Los comentarios excluyen el contenido de la acción y los mensajes de excepción. La cancelación del último paso no puede informar la finalización. La finalización describe la ejecución, no una consulta de estado independiente OBS.

Verificación:

- 24 Se aprobaron las comprobaciones principales, incluido el orden de fases, los resultados de falla/cancelación, la cancelación del paso final, el aislamiento de excepciones del oyente y la exclusión de valores de acciones privadas.
- La compilación de la solución de lanzamiento se aprobó sin advertencias ni errores.
- Windows comprobaciones aprobadas; el K100 conectado informó G1–G6 y la configuración/liberación nativa se realizó correctamente. Los puntos finales de audio se inspeccionaron sin cambiar su estado.
- WPF pasó humo con todos los diseños de superposición de referencia, asistente/catálogo/conflictos/persistencia de borrador, configuración de captura nativa en vivo, reenvío de devolución de llamada, rechazo de devolución de llamada obsoleta y restauración de enlace en pausa. Se simuló la entrada de devolución de llamada nativa; una pulsación física humana de la tecla G sigue siendo una comprobación manual.
- Se verificó la invariancia real de la ventana de primer plano para el brindis de ejecución. Se comprobaron las propiedades de interacción de superposición y mostrar/ocultar; Se renderizaron capturas de pantalla para el asistente, la superposición y el brindis.
- Los idiomas 27 contienen todas las cadenas canónicas 819; Se aprobó la validación de clave/marcador de posición fuera de línea.

Las instantáneas y el resultado completo de la GUI se encuentran en artefactos/usp-win-x64/smoke. Las pruebas utilizan perfiles temporales; Se realiza una copia de seguridad de la configuración de producción y se verifica el hash durante la implementación.
