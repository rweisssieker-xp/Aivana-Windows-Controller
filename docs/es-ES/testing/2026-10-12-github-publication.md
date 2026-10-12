<!-- generated-doc locale=es-ES source=docs/testing/2026-10-12-github-publication.md sha256=ab4d40cd12047e2cdacad359a12febe16d5689a82f4f792b484b3d4e2430e02a -->

> Traducción asistida por máquina. Se comprueban la estructura, los ejemplos de código y los enlaces; esto no constituye una revisión por hablantes nativos ni una aprobación jurídica. Los informes históricos conservan sus fechas y alcance originales. [fuente original](../../testing/2026-10-12-github-publication.md)

# GitHub lanzamiento del estado de desarrollo MAKRIVO

Estado: 12. Octubre 2026. Lanzamiento del código fuente, no un lanzamiento minorista firmado.

- Compilación de lanzamiento de la solución: exitosa, sin advertencias ni errores.
- Exámenes básicos: exitosos.
- WPF prueba de humo: exitosa; Informe de resultados nuevos en la carpeta de compilación local.
- Primera suite Windows completa: ventana de primer plano, audio, pantalla y controles iCUE aprobados; luego, se agota el tiempo de respuesta del trabajador de Automatización de la interfaz de usuario.
- Reintento dirigido `--extensions-only`: exitoso, incluida la automatización de la interfaz de usuario, la ambigüedad, los objetivos deshabilitados, la exclusión de contraseña, el proceso de complemento y el host móvil HTTPS real. El tiempo de espera original no quedó oculto por un cambio de tiempo de espera; la causa aún no se ha demostrado de manera concluyente.
- Posterior ejecución completa de Windows: exitosa, incluida la ventana de primer plano y todas las comprobaciones de extensión. Una repetición exitosa no excluye tiempos de espera esporádicos.
- Antes de la publicación, no se encontraron claves privadas típicas ni claves productivas Stripe/GitHub/AWS en el inventario de fuentes auditadas. Esta verificación de muestra limitada no es una verificación secreta completa.
- Los artefactos de compilación y los datos de orquestación local no se publican.

Tamaño y descuentos restantes: [Featuristas](../FEATURES.md). Los informes de prueba anteriores mantienen su validez temporal; no son una garantía para cualquier hardware o contexto de escritorio.
