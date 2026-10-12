<!-- generated-doc locale=es-ES source=docs/testing/2026-10-12-gui-polish.md sha256=ac39188358e51c68964421d3e855161d607b02a41da2b1a1fad70244f9feff97 -->

> Traducción asistida por máquina. Se comprueban la estructura, los ejemplos de código y los enlaces; esto no constituye una revisión por hablantes nativos ni una aprobación jurídica. Los informes históricos conservan sus fechas y alcance originales. [fuente original](../../testing/2026-10-12-gui-polish.md)

# Revisión de GUI de 12.10.2026

- Acceso directo a “Herramientas” en la navegación; tres grupos con acciones declaradas para configuración, creación y prueba.
- Catorce áreas de diálogo existentes utilizan una clase de diálogo común WPF con los recursos de la ventana principal, fuente consistente y posicionamiento en el propietario.
- El tamaño del cuadro de diálogo está limitado al espacio de trabajo Windows; El contenido largo tiene barras de desplazamiento. Las configuraciones y las páginas de perfil siguen siendo desplazables incluso a bajas alturas.
- El filtro de acción solo aparece en el panel. La selección y búsqueda de perfiles tienen nombres explícitos para tecnologías de asistencia. Las etiquetas de los campos son más contrastantes.
- La prueba de humo de diálogo incluye descripción general de la herramienta y diálogos de extensión en 560 × 440 y tamaño normal; Las pruebas de grabación y de idioma existentes se han adaptado a la estructura de desplazamiento.

Limitaciones: Las ventanas compactas no sustituyen a las pruebas de DPI reales Windows con porcentaje 150/200. Los nuevos textos en alemán aún no se han traducido completamente para todos los idiomas 27. No hay validación de hardware nuevo ni declaración sobre la usabilidad medida.

Banco de pruebas: versión de lanzamiento sin advertencias ni errores; Pasó la prueba de humo completa WPF, incluida la herramienta compacta y los diálogos de extensión y las comprobaciones de cambio de idioma. git diff --check aprobado.
