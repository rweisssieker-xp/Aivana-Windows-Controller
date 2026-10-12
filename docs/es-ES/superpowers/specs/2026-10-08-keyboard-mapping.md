<!-- generated-doc locale=es-ES source=docs/superpowers/specs/2026-10-08-keyboard-mapping.md sha256=fb766a2a2b4698ae3e1c3f63b50bb4f5c605da3dd56c61c05fcc0832b5cf8aeb -->

> Traducción asistida por máquina. Se comprueban la estructura, los ejemplos de código y los enlaces; esto no constituye una revisión por hablantes nativos ni una aprobación jurídica. Los informes históricos conservan sus fechas y alcance originales. [fuente original](../../../superpowers/specs/2026-10-08-keyboard-mapping.md)

# Vista de teclado físico para mapeo

[English (US)](2026-10-08-keyboard-mapping.md) | [Deutsch](2026-10-08-keyboard-mapping.md) | [Français](2026-10-08-keyboard-mapping.md) | [Italiano](2026-10-08-keyboard-mapping.md)

## Objetivo

La vista del teclado está destinada a mostrar las posiciones reales de las teclas al asignar acciones. Las líneas del mismo ancho no son suficientes: Enter, Shift, Space, Navigation, el teclado numérico y los controles adicionales del fabricante difieren.

## Implementación

- Catálogo con ID físicos separados, geometría en unidades clave, leyendas impresas y códigos de entrada Windows.
- ISO-DE y ANSI-US cada uno como formato completo y TKL; además Corsair K100 RGB, que fue reconocido como modelo conectado a través de iCUE.
- ISO Enter con forma de L, teclado numérico con teclas altas más/entrar y cero ancho; Navegación y flechas en sus propias posiciones.
- Seleccione explícitamente el modelo/diseño. USB VID/PID y el nombre del modelo no prueban la variante de idioma. Los perfiles genéricos y los perfiles modelados según los documentos del fabricante están claramente etiquetados de forma diferente; sin escala milimétrica ni garantía de compatibilidad de firmware.
- Asigne el código de escaneo al diseño de entrada activo Windows para que un clic en una tecla física no ocupe accidentalmente otra tecla, como Y/Z al cambiar entre EE. UU. y DE.
- La tarea abre un borrador en el editor de acciones existente. Los mecanismos existentes son el ahorro, la comprobación de conflictos y el descarte. Los clics no inician acciones.
- Las teclas G patentadas, las teclas de perfil/bloqueo y la rueda iCUE están representadas, pero no se emiten como teclas de acceso rápido Windows normales sin un adaptador de entrada implementado. Los atajos configurables del fabricante siguen siendo una ruta posible.
- Las asignaciones siguen siendo teclas de acceso rápido globales Windows; una vista de dispositivo no implica la supresión específica del dispositivo ni la asignación exclusiva de un único teclado físico.

## evidencia

[K100-Handbuch con elementos de control](https://assets.corsair.com/image/upload/corsairmedia/sys_master/productcontent/AP_K100_RGB_Mechanical_QSG_AD.pdf), [Corsair Declaración de ANSI / ISO](https://www.corsair.com/au/en/explorer/gamer/keyboards/ansi-vs-iso-whats-the-best-keyboard-layout/).

## examen

pruebas de geometría, variantes y códigos de entrada; Compilaciones y pruebas core/Windows existentes; WPF humo para visualización y mapeo sin ejecutar ni cambiar la configuración del usuario real. La investigación del fabricante documenta requisitos adicionales de modelos/SKU independientemente del catálogo de diseño implementado actualmente.
