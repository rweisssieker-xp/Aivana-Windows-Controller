<!-- generated-doc locale=es-ES source=docs/FEATURES.md sha256=ea712f39457ce69f772f09a1294201705a5f36a92a8bf15f356bd80132c3e723 -->

> Traducción asistida por máquina. Se comprueban la estructura, los ejemplos de código y los enlaces; esto no constituye una revisión por hablantes nativos ni una aprobación jurídica. Los informes históricos conservan sus fechas y alcance originales. [fuente original](../FEATURES.md)

# MAKRIVO – Descripción general de funciones

Fabricante: **Aivana**. Estado: **12. Octubre 2026**. Este es el estado de desarrollo, no un lanzamiento de ventas o de hardware.

## Windows control

- Mosaicos, teclas de acceso rápido globales, paleta de comandos, favoritos y perfiles dependientes de la aplicación.
- Macros de varios pasos, repetir, pausar, cancelar y obtener una vista previa.
- Abrir programas, archivos y sitios web; Utilice atajos de texto, portapapeles y teclado.
- Organice ventanas, bloquee Windows, controle salidas de audio y micrófonos y cambie el brillo de la pantalla a través de interfaces compatibles.
- OBS-WebSocket-v5-Anbindung para escenas, grabación, streaming y audio.
- Plantillas, Asistente de Workplace, Descubrimiento de dispositivos y Superposición de teclado.
- Importación y exportación de configuraciones de perfiles; Revisión de diseño y detección de conflictos.
- Configuraciones guardadas localmente; no se requiere cuenta para el uso diario.

## Funciones avanzadas en el código fuente actual

| área | Gama de funciones | Límites |
| --- | --- | --- |
| Verificación de resultados | Historial de pruebas previas, posteriores a la inspección y de pruebas | Confirmar únicamente efectos verificables; Una pulsación de tecla exitosa no prueba el efecto objetivo. |
| Asistente en el lugar de trabajo | Prepare perfiles y procesos a partir de programas, archivos, URL y otras acciones seleccionadas. | Es necesario obtener una vista previa y guardar; La configuración no hace nada. |
| Grabación de macros y flujos de trabajo | Grabación, condiciones, gestos y sincronización de perfiles locales. | El comportamiento y los límites están documentados en los informes de implementación y prueba. |
| Automatización de la interfaz de usuario | Seleccione objetivos, verifique selectores, registre acciones y secuencias de clics limitadas | Trabajador separado; Se rechazan los objetivos ambiguos, deshabilitados y de contraseña; sin grabación de entrada de texto general. |
| Extensiones | Inspeccionar, importar, activar basándose en confianza explícita, exportar y revertir paquetes | El aislamiento de procesos no es un entorno limitado; no es un mercado público de complementos. |
| Panel móvil | Panel HTTPS local, emparejamiento, perfiles seleccionados e historial de estado/auditoría | La aceptación de LAN real para teléfonos/tabletas aún está abierta; Sin panel de instrumentos con sensores universales. |
| Adaptador del fabricante | Adaptador analógico Corsair, Logitech-G-SDK, Razer/SteelSeries-RGB y Wooting | Depende del modelo/SDK; Nuevos adaptadores no completamente validados con hardware real. No se incluyen nuevas DLL de SDK patentadas. |
| Mapeo de teclado físico | Reconocer claves estándar según el dispositivo y el código de escaneo | Raw Input no suprime la entrada normal Windows. |
| Asistente de reuniones locales de IA | Prototipo opcional para Ollama, LM Studio, llama.cpp y ONNX Runtime GenAI La CPU produce diseños limitados a partir de recursos seleccionados explícitamente | Desactivado de forma predeterminada; tiempo de ejecución de CPU nativo probado con modelo aleatorio, pero sin calidad de planificación comprobada ni ahorro de tiempo; No se incluyen pesos de modelo. |

## Aún no está disponible como característica de ventas terminada

- **Teclado secundario/teclado de macros exclusivo:** Falta un controlador de filtro firmado, con licencia y probado por hardware. Las pulsaciones de teclas normales aún pueden llegar a Windows.
- Aceptación completa del hardware de todos los adaptadores del fabricante, así como pruebas de LAN de dispositivos móviles reales.
- Verdaderos cambios mixtos de monitor/DPI y aprobación técnica de todas las nuevas traducciones. Se comprobaron PerMonitorV2 y 150/200-Prozent-Layouts simulado; Las pruebas de visualización reales permanecen separadas. La interfaz ofrece idiomas 27; Los nuevos textos detallados aún no han sido completamente aprobados lingüísticamente.
- Instalador minorista firmado, CLUF final, derechos de distribución de SDK verificados e integración productiva de licencia de compra/pago.

## Detalles y evidencia

- [Documentación de servicio](README.md)
- [Extensiones: funciones, pruebas y límites abiertos](testing/2026-10-12-six-extensions.md)
- [Asistente de IA local: prototipo y estado de prueba](testing/2026-10-12-local-meeting-assistant.md)
- [Horarios de IA locales y requisitos de modelo](testing/2026-10-12-local-ai-runtimes.md)
- [Evaluación de DPI y escaleración](testing/2026-10-12-dpi-scaling.md)
- [Examen de licencia](licensing/2026-10-12-commercial-license-audit.md) – Recopilación histórica; el nuevo script de Publish completa las indicaciones de tiempo de trabajo. El paquete final se debe examinar por separado.
- [Plataformas de venta](licensing/makrivo-vertriebsplattformen-2026-10-12.html) – estado de comparación; Stripe es además una plataforma de pago posible, vea las condiciones actuales del proveedor.

## Generar paquete de desarrollo completo

```powershell
./tools/publish-complete.ps1 -SelfContained
```

El paquete incluye una aplicación de escritorio, un trabajador de automatización de la interfaz de usuario, un host móvil, archivos del navegador, un complemento de muestra y notas de licencia de tiempo de ejecución. Los artefactos de compilación no se registran en Git. Una versión del código fuente GitHub aún no es un instalador publicado de Windows.
