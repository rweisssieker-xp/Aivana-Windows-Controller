<!-- generated-doc locale=es-ES source=docs/testing/2026-10-12-local-ai-runtimes.md sha256=3dfbb80761aad758ea6a9307999ff530dfdd4a0cf685804e15052508cf107fd7 -->

> Traducción asistida por máquina. Se comprueban la estructura, los ejemplos de código y los enlaces; esto no constituye una revisión por hablantes nativos ni una aprobación jurídica. Los informes históricos conservan sus fechas y alcance originales. [fuente original](../../testing/2026-10-12-local-ai-runtimes.md)

# Tiempos de ejecución de IA locales

## Servicio

Herramientas → Abra el asistente de reuniones AI, actívelo explícitamente y seleccione el tiempo de ejecución de AI. Libera recursos, formula un objetivo, crea un borrador y compruébalo en el editor. Ningún proveedor ejecuta el diseño automáticamente. No hay cambio automático de proveedor.

| Duración | Requisito | conexión |
|---|---|---|
| Ollama | modelo instalado localmente | 127.0.0.1:11434 |
| LM Studio | ejecutando un servidor local con modelo cargado localmente y soporte de esquema JSON | 127.0.0.1:1234 |
| llama.cpp | ejecutando el servidor llama con soporte para modelo local y esquema JSON | 127.0.0.1:8080 |
| ONNX Tiempo de ejecución GenAI CPU | carpeta de modelo de texto local compatible y confiable GenAI | proceso incluido por separado, sin HTTP |

Los clientes HTTP no utilizan proxies y no siguen redireccionamientos. Los propios servidores locales pueden abordar servicios adicionales; En particular, LM Link/Remote Routing no se puede utilizar para el procesamiento local. MAKRIVO no puede detectar esto de manera confiable a través de la interfaz compatible. Ollama Los metadatos remotos y los nombres de nubes también se rechazan. Los servidores locales autenticados no se pueden configurar en esta versión.

## ONNX

En el diálogo, seleccione genai_config.json. La carpeta debe contener tokenizer.json y una plantilla de chat. Se admiten chat_template como cadena, una plantilla llamada default (matriz o diccionario) y un archivo chat_template.jinja independiente si no existe una plantilla integrada o si esta es null. Se rechazan las plantillas ambiguas; máximo 65536 caracteres. Los archivos .onnx arbitrarios, los modelos de visión/embeddings o las exportaciones exclusivas para GPU/NPU no son modelos para el asistente de reuniones.

Incluye: Microsoft.ML.OnnxRuntimeGenAI 0.17.1, CPU. La configuración del proveedor se restablece a la CPU. Sin descargas ni pesos de modelo en el paquete. No se requiere instalación de Python. Utilice únicamente paquetes de modelos confiables; el proceso separado es un límite de bloqueo/aborto, no un entorno limitado de seguridad.

Límites: 90 segundos, incluida la carga del modelo, tokens de aviso máximos 3072 y tokens generados 1024, límite de contexto de modelo adicional; JSON verificado rigurosamente y los ID de recursos conocidos. Se descartará la respuesta incorrecta/truncada. Cerrar/Cancelar finaliza el proceso de trabajo iniciado. No hay comandos de modelo gratuitos ni programas/rutas de archivos en la lista de recursos separada del mensaje; el texto de destino escrito por el usuario puede contener dichos datos.

## Verificación

- Suite principal, pruebas de transporte de loopback con respuestas simuladas de LM-Studio/llama.cpp/Ollama, verificación de esquema/aborto/recursos.
- Regresión del protocolo Unicode con objetivos largos máximos y nombres de recursos 24.
- Inferencia de CPU nativa real con tiny-random-gpt2-fp32-Testgraph oficial de microsoft/onnxruntime-genai, confirmación 5c19002edce50beaa3a5c7eeefd8fe5269ee8d19. Plantilla de prueba agregada y tokenizador de bytes adaptado al pequeño gráfico aleatorio. Este modelo aleatorio no verifica la calidad y no está disponible.
- Prueba de tiempo de ejecución nativo reproducible: herramientas/check-onnx-runtime.ps1 -ModelDirectory <carpeta de modelo GenAI>.
- WPF-Smoke comprueba la selección de los cuatro proveedores y el banco de pruebas; La selección por sí sola no genera una solicitud de modelo.

## Acciones reales restantes

Aún se requieren pruebas de reemplazo de hardware/teléfono inteligente/tableta/monitor. La supresión exclusiva del teclado permanece bloqueada sin un controlador de filtro firmado, autorizado y verificado. No reemplazado por controladores ficticios o Raw Input. El cambio de enfoque aún puede ser bloqueado por Windows; el error ahora menciona la activación manual de la ventana de destino. Se han agregado traducciones a los controles centrales de IA en todos los catálogos 27; Los nuevos textos detallados y la aprobación de la traducción técnica aún están abiertos. La calidad en vivo con un modelo de instrucción adecuado, operación continua y ahorros de tiempo medidos no se prueban mediante pruebas HTTP falsas o de modelos aleatorios.

## Fuentes

- https://onnxruntime.ai/docs/genai/api/csharp.html
- https://www.nuget.org/packages/Microsoft.ML.OnnxRuntimeGenAI/0.17.1
- https://lmstudio.ai/docs/developer/openai-compat/chat-completions
- https://github.com/ggml-org/llama.cpp/blob/master/tools/server/README.md

Prueba final: versión de lanzamiento sin advertencias ni errores; Pasó el núcleo completo y la suite Windows, incluido el dispositivo de enfoque nativo, HTTPS, UIA, complemento IPC y pruebas de transporte de IA compatibles. Lanzamiento del nuevo paquete 20261012-190618: humo WPF aprobado (reloj 19:07), inferencia de CPU ONNX incluida con gráfico de prueba aprobado. Revisión independiente: límite del protocolo Unicode corregido, no hay otros hallazgos concretos. De esto no se deriva ninguna aceptación física del fabricante/teléfono/monitor.

## Seguimiento: Real calidad y modelos

Ejecución adicional en CPU con allama-ai/SmolLM-360M-Instruct-onnx-int4, revisión 82a3d1a8a86c8053f2a859a90ed4d344fc0725b0: inferencia nativa superada. SHA256 de los pesos del modelo: 48e9c4ef12f7f995ca11fc7020b39990aa27bc67cc0fe116067676a000a04429. Exportación de la comunidad utilizada solo en el entorno de pruebas, no incluida en la versión distribuida.

Cuatro pruebas de planificación reales (alemán/inglés, documento, programa, silencio del micrófono solicitado explícitamente): **0/4 planes válidos**; una JsonException en cada validación estricta de la respuesta. Duraciones: 63501, 6376, 72118 y 6774 ms. No se ejecutó ninguna acción. Este pequeño modelo no cumple el contrato de planificación y no se recomienda como adecuado. Informe: artifacts/local-ai-evaluation/smollm360-results.json. Reproducción: pruebas de Windows --evaluate-onnx <carpeta-del-modelo> <ruta-del-informe>. El funcionamiento del entorno de ejecución no demuestra la calidad del modelo.

La navegación de herramientas y el informe de verificación también se tradujeron en los 27 catálogos de idiomas. Los textos largos y la revisión lingüística siguen pendientes. Las pruebas de regresión de plantillas también comprueban tipos JSON incorrectos, plantillas vacías/ambiguas/demasiado largas y null con alternativa Jinja.

Paquete actualizado: artifacts/release-20261012-201134. Prueba de arranque WPF del paquete superada el 12.10.2026 a las 20:12; inferencia nativa en CPU superada con el modelo Instruct de prueba independiente en el proceso de trabajo incluido. Pruebas Core y Windows de IA superadas; compilación sin advertencias ni errores. Calidad de planificación del modelo todavía en 0/4.
