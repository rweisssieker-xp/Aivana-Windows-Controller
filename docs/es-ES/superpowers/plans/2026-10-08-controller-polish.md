<!-- generated-doc locale=es-ES source=docs/superpowers/plans/2026-10-08-controller-polish.md sha256=1a076a67eea31048f6305f30a121c474d592451d1ddcd9b46bc339fd8ec068f2 -->

> Traducción asistida por máquina. Se comprueban la estructura, los ejemplos de código y los enlaces; esto no constituye una revisión por hablantes nativos ni una aprobación jurídica. Los informes históricos conservan sus fechas y alcance originales. [fuente original](../../../superpowers/plans/2026-10-08-controller-polish.md)

# Plan de mejora de capacidad y usabilidad del controlador

[English (US)](2026-10-08-controller-polish.md) | [Deutsch](2026-10-08-controller-polish.md) | [Français](2026-10-08-controller-polish.md) | [Italiano](2026-10-08-controller-polish.md)

Solicitud del usuario: mejorar la apariencia, usabilidad y funcionalidad; La autorización previa solicita el máximo de subagentes útiles en modelos más pequeños necesarios y finalización autónoma.

## Alcance
Preservar los perfiles existentes y la integración funcional del SDK Corsair. Mejore la aplicación WPF existente en lugar de crear un prototipo independiente.

- [x] Trabajador de shell/panel: diseño responsivo, navegación activa, controles legibles, actividad/estado compacto, reordenamiento de mosaicos con arrastrar y soltar, estados vacíos y plantillas útiles. Propio MainWindow.xaml, MainWindow.xaml.cs y MainWindow.Dashboard.cs.
- [x] Trabajador del editor: MainWindow.Editor.cs y archivos parciales opcionales solo para el editor. Ediciones por etapas explícitas, Guardar/Descartar, validación en línea, lista de macros dedicada con arrastrar y soltar, pasos duplicados, captura confiable de teclas de acceso rápido y campos de archivo/URL.
- [x] Trabajador Windows: validación nativa, nombres de dispositivos sólidos y capacidades de SDK, esperas de modificadores limitadas e inyección segura; pruebas propias Windows y Windows.
- [x] Principal: progreso de ejecución principal, interfaces del controlador, preservación de la configuración, pruebas de integración, capturas de pantalla reales renderizadas, publicación/empaquetado y reemplazo de la aplicación en ejecución.

## Contrato de interfaz
El controlador proporciona las API existentes más la cadena LastStatus, bool HasRunningActions, bool IsRunning(Guid), cadena GetTileStatus(Guid), cadena ForegroundProcess, bool CorsairAvailable, IReadOnlyList<CorsairKeyboardInfo> CorsairKeyboards, Task TestCorsairColorAsync(cadena), void OpenIcue(). El editor expone bool HasUnsavedChanges, void DiscardDraft(), bool CommitDraft(); Los nombres de campos/métodos existentes permanecen disponibles en las clases parciales.

## Aceptación
Sin reinicio de la configuración del usuario real; los cambios preparados válidos se guardan una vez, los borradores no válidos permanecen editables, descartar restaura los datos guardados. Alias ​​de teclas de acceso rápido nativos validados sin inyectar entradas de prueba en ventanas no relacionadas. Reordenamiento de macros y mosaicos probado; todas las vistas se representan en tamaños de escritorio; estado en ejecución/fallido/completo visible; Se muestra el modelo de dispositivo SDK real. Compilación/pruebas y aprobación de humo publicada, y se lanza la aplicación actualizada.
