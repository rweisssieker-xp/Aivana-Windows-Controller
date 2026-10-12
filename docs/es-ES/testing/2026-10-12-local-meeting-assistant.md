<!-- generated-doc locale=es-ES source=docs/testing/2026-10-12-local-meeting-assistant.md sha256=56631ca85dca5f785448d12586b0741628728925a649f6124f7aebffb19bb615 -->

> Traducción asistida por máquina. Se comprueban la estructura, los ejemplos de código y los enlaces; esto no constituye una revisión por hablantes nativos ni una aprobación jurídica. Los informes históricos conservan sus fechas y alcance originales. [fuente original](../../testing/2026-10-12-local-meeting-assistant.md)

# Asistente de reuniones de IA local: prototipo

Estado: 12. Octubre 2026. Hipótesis del agente de resultados de ForgeMind, flujo de reuniones limitado. No hay ventaja competitiva demostrada ni ahorros de tiempo medidos sin pruebas de comparación reales.

## Uso

1. Paleta de comandos → **KI Asistente de reuniones**. El experimento está desactivado de forma predeterminada y se restablece durante la importación de configuración o la exportación portátil.
2. Habilitar asistente local. Ollama debe ejecutarse por separado en `127.0.0.1:11434`; ya debe estar instalado un modelo local adecuado. **Cargar modelos locales** y seleccionar modelo. MAKRIVO no instala modelos/servicios, no contacta con OpenAI/Cloud API ni utiliza claves API. La configuración fuera de línea y la confiabilidad del servicio local Ollama recae en el usuario; Se rechazan los modelos remotos/en la nube declarados.
3. Ingrese el destino y agregue explícitamente programas/documentos/sitios web. Se transmiten destino, ID de recurso, nombre y tipo; Las rutas de destino y el contenido de los archivos seleccionados no se transmiten adicionalmente; la entrada de destino en sí se envía palabra por palabra. Las etiquetas del sitio web incluyen solo esquema/host, el enlace completo permanece local.
4. **KI crear borrador**. Máximo de segundos 90, tokens de salida 1024, tamaño de respuesta limitado. Los cambios de destino/modelo/recurso descartan la vista previa; Cancelar/Cerrar finaliza la solicitud. El modelo solo puede sugerir identificaciones seleccionadas. Se rechazan los ID desconocidos, los comandos gratuitos, los campos JSON adicionales y los tipos de documentos ejecutables.
5. Ver vista previa y puntos abiertos → **Verificar en el editor**. Inicialmente, el resultado es un borrador no guardado. Primero guárdelo en el editor existente y ejecútelo explícitamente si es necesario. La validación normal de macros, la verificación previa y posterior, la pausa y la cancelación siguen siendo responsables.

Compatible: iniciar programas sin argumentos, abrir PDF/DOCX/XLSX/PPTX/TXT/MD local seleccionado explícitamente, abrir sitios web HTTP(S), silenciar el micrófono ante una solicitud explícita. Sin cambios predeterminados de auriculares, sin mensajes, citas, descargas, scripts, clics gratuitos o entrada de texto. Los deseos que no se pueden representar aparecen como puntos abiertos si el modelo los reconoce correctamente; El usuario debe comprobar si se han cumplido los objetivos técnicos. La apertura de documentos no se verifica técnicamente automáticamente.

## experimento de comparación

**Medir configuración manual · Inicio / Fin** mide el flujo de comparación manual completo. Las suposiciones del asistente miden el tiempo desde que se abre el asistente hasta que se guarda por primera vez en el editor; `generated`, por el contrario, sólo contiene la duración de la planificación. No compares dos duraciones diferentes para ahorrar tiempo.

Los eventos locales se encuentran en la carpeta de configuración en `Experiments/meeting-experiment.jsonl`: marca de tiempo, intento aleatorio/ID de mosaico, evento, duración, corrección sí/no. Sin textos de destino, nombres de recursos, rutas, respuestas modelo o contenido del documento. El archivo gira desde 2 MiB, se conserva un archivo anterior. Los errores de medición no deben cancelar las macros ni el guardado de la configuración.

Capturas: línea de base manual, generación/errores/abortar del modelo, descartar cambios/cierres de vista previa, guardados iniciales y correcciones en el editor, y estados finales reales del corredor para los mosaicos guardados desde ellos. `run_completed` simplemente significa que el corredor ha completado; Los efectos objetivo confirmados se enumeran por separado en el proceso de prueba. Las repeticiones de la misma macro no son nuevos intentos de configuración independientes. El experimento inicial puede continuar después del reinicio porque el ID del experimento permanece en el mosaico guardado. Los mosaicos duplicados comparten este origen; grupo por ID de prueba al evaluar.

Primer experimento: 20 instalaciones para reuniones comparables o dos semanas con usuarios de prueba voluntarios; Inicialmente uso interno. Compare el tiempo de configuración manual con el tiempo de configuración asistida completa; Además, evalúe la tasa de corrección/rechazo, los errores del corredor y los resultados de las pruebas reales. Si no hay mejora o si se violan los límites de protección, desactive el interruptor. No se ha demostrado ningún uso ni disposición a pagar en el repositorio. Sin transmisión automática de telemetría ni ajuste de precios.

## banco de pruebas

- Principales: Límites, tipos, ID desconocidos/duplicados, exclusión de secuencias de comandos, enlaces largos, configuración heredada opcional, opción de importación/exportación, medición de ahorro de datos y descarte único y confirmación correcta.
- Transporte con modelo falso: esquema JSON Ollama, destino de bucle invertido, no hay rutas de archivo en el mensaje, nombre de nube y alias remoto rechazados, respuesta corrupta/demasiado larga rechazada, cancelación.
- WPF-Humo: cuadro de diálogo representado; El envío del borrador no cambia los mosaicos guardados, revisar/guardar conserva la procedencia y la corrección de registros, descartar no guarda nada.
- Revisión independiente: vista previa descartada y enlaces largos encontrados y corregidos; Prueba de regresión ROJO → VERDE.
- Versión de lanzamiento sin advertencias ni errores, todo el conjunto principal y WPF funcionan correctamente. Inicialmente existía la suite Windows completa, incluidos cambios de enfoque. Otra ejecución volvió a fallar porque se rechazó el cambio de primer plano; la prueba depende del contexto y permanece sin cambios. Las pruebas Windows restantes, incluido el transporte del modelo, pasan con la prueba de éxito en primer plano omitida explícitamente.
- La inspección del paquete reveló un error existente en el intercambio atómico de archivos de configuración abiertos brevemente. Un bloqueo de lectura reprodujo el mismo error de acceso denegado; La repetición limitada de solo el intercambio atómico está marcada en ROJO → VERDE. Los errores de acceso permanentes siguen siendo visibles.
- Paquete independiente actual `artifacts/release-20261012-184105`: WPF fuma exitosamente después de la corrección de memoria, incluido el diálogo AI y la aceptación/rechazo de borradores.
- No es una verdadera prueba de modelo: el servicio local Ollama no respondió cuando se probó. No hay declaraciones sobre la calidad del modelo, la latencia, la integridad semántica o el ahorro de tiempo. Se mantienen las limitaciones de hardware/controlador/escalado de la versión anterior.

fuentes de protocolo: [Ollama El chat](https://docs.ollama.com/api/chat), [Estruturados resultados](https://docs.ollama.com/capabilities/structured-outputs), [Definiciones de tipo oficiales, incluidos los datos de modelo remoto](https://github.com/ollama/ollama/blob/main/api/types.go).
