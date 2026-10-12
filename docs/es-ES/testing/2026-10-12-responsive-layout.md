<!-- generated-doc locale=es-ES source=docs/testing/2026-10-12-responsive-layout.md sha256=8b3710a27f806575e88b4d9a0c4e80de4f26539ea1b5826cfe291764228b4ca6 -->

> Traducción asistida por máquina. Se comprueban la estructura, los ejemplos de código y los enlaces; esto no constituye una revisión por hablantes nativos ni una aprobación jurídica. Los informes históricos conservan sus fechas y alcance originales. [fuente original](../../testing/2026-10-12-responsive-layout.md)

# Interfaz responsiva WPF

Las páginas principales se basan en el ancho del contenido utilizable, no en un ancho de ventana fijo. Bajo el ancho del contenido de 900 DIP, el panel y el editor están uno debajo del otro. En 620 DIP, los ratios están en dos columnas. La barra lateral está colapsada bajo el ancho de ventana 820 DIP. Tamaño mínimo: 640 × 480 DIP.

Los paneles de perfil y configuración y los controles de teclado cambian sus columnas según el ancho real de la cuadrícula. Los controles existentes se reorganizan en lugar de crear nuevas entradas. Las tarjetas de acción dividen el ancho de línea disponible. Las barras de herramientas se rompen. El lado del teclado es accesible verticalmente, el teclado físico conserva sus proporciones.

Prueba automática WPF: las cinco páginas principales en 640 × 480, 900 × 620 y 1440 × 900; Capturas de pantalla por página; Cambio de columna en ambas direcciones con comprobación del texto recibido. Los DIP son unidades lógicas WPF. La verdadera escala Windows al porcentaje 150/200 y varios monitores aún no están validados.

Banco de pruebas: versión de lanzamiento sin errores ni advertencias. Se pasó la prueba de humo WPF completa con todos los tamaños, cambios de columnas, diseños de teclado y comprobaciones de idioma. git diff --check aprobado.
