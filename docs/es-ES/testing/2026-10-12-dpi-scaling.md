<!-- generated-doc locale=es-ES source=docs/testing/2026-10-12-dpi-scaling.md sha256=dcf6d4c1a21eec9be9f07a38cab0aff308587c78b85fb2b24c69f04d28d82864 -->

> Traducción asistida por máquina. Se comprueban la estructura, los ejemplos de código y los enlaces; esto no constituye una revisión por hablantes nativos ni una aprobación jurídica. Los informes históricos conservan sus fechas y alcance originales. [fuente original](../../testing/2026-10-12-dpi-scaling.md)

# verificación de ppp

MAKRIVO declara PerMonitorV2 en el manifiesto de la aplicación integrada. La prueba WPF utiliza GetWindowDpiAwarenessContext y AreDpiAwarenessContextsEqual para comprobar el contexto efectivo real de la ventana principal.

Las pruebas de diseño/cuadrícula adicionales simulan el porcentaje 150 y 200 en una pantalla 1920 × 1080 con reserva de barra de tareas: dimensiones lógicas de ventana 1280 × 693 respectivamente 960 × 520 DIP. Las cinco páginas principales se representan como PNG en 144 o 192 DPI. Los archivos se llaman escala-simulada-150-* o escala-simulada-200-*.

Estas pruebas no cambian ninguna configuración de Windows. No validan el verdadero cambio de monitor WM_DPICHANGED, la configuración de monitor mixta y la nitidez real en una pantalla diferente. Las pruebas de escalado físico permanecen abiertas.

La advertencia del analizador WinForms WFO0003 se suprime exclusivamente para este proyecto de inicio WPF: WinForms se utiliza para la integración del área de notificación, WPF controla el inicio; El DPI se establece mediante manifiesto antes de la creación de la ventana.

Microsoft documentación: https://learn.microsoft.com/en-us/windows/win32/hidpi/setting-the-default-dpi-awareness-for-a-process

Inspección visual: el tablero también cambia a la pantalla apilada bajo la altura de la ventana 650 DIP para que los mapas y el editor no queden comprimidos en un área intermedia que sea demasiado pequeña.

Banco de pruebas: versión de lanzamiento sin errores ni advertencias; Pasó la prueba de humo WPF completa, incluido el PerMonitorV2-Fensterkontext real y el 150/200-Prozent-Renderings simulado. El 200-Prozent-Dashboardaufnahme fue inspeccionado visualmente después del ajuste de altura. git diff --check aprobado.
