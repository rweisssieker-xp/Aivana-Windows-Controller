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

En el diálogo genai_config.json. el paquete debe contener tokenizer.json y un modelo de chat. se apoyan chat_template como una cadena de caracteres, un modelo de nombre (Array o Diccionario) y un chat_template.jinja separado, si no hay una muestra en línea o no. Se rechazan los modelos multiplicados; 65536 Los archivos .onnx, los modelos de visión/embedón o las exportaciones puras de GPU/NPU no son modelos de reunión.

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

Más CPU con allama-ai/SmolLM-360M-Instruct-onnx-int4Revisión 82a3d1a8a86c8053f2a859a90ed4d344fc0725b0: Inferencia nativa. Modellgewichte-SHA256: 48e9c4ef12f7f995ca11fc7020b39990aa27bc67cc0fe1160676a000a04429.

Cuatro pruebas de planificación reales (alemán/inglés, documento, programa, micrófono explícito): **0/4 Los planes válidos**; cada JsonException en caso de examen de respuesta estricto. 63501, 6376, 72118 y 6774 ms. No se realizan acciones. Este pequeño modelo no cumple con el contrato de planificación y no se recomienda como adecuado. informe: artifacts/local-ai-evaluation/smollm360-results.json. reproducción: Windows-Test --evaluate-onnx <modelo ordenador> <caminas de informe>. la función de tiempo de trabajo no demuestra la calidad del modelo.

Navegación de herramientas y los informes de prueba se han añadido a todos los 27 Los catálogos de lenguaje traducidos. los textos largos y las grabaciones lingüísticas permanecen abiertas. la regresión de los modelos también examina los tipos de JSON falsos, los modelos vacíos/múltiples/múltiples y el nulo con el Jinja Fallback.

Paquete actualizado: Artifacts / Release-20261012-201134. envasado WPFEl azúcar en 12.10.2026 El 20:12 superado; la referencia nativa de CPU con el modelo de prueba de instrucción separado en el trabajador conectado. Windows-Testado de TI; Construye sin advertencias/error. calidad de planificación del modelo continúa 0/4.
