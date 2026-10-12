<!-- generated-doc locale=es-ES source=docs/superpowers/plans/2026-10-12-local-ai-runtimes.md sha256=f24c6b3bce4d0b4dd9d42732c363941a36644f90cf8d664d61ea0c60eb950079 -->

> Traducción asistida por máquina. Se comprueban la estructura, los ejemplos de código y los enlaces; esto no constituye una revisión por hablantes nativos ni una aprobación jurídica. Los informes históricos conservan sus fechas y alcance originales. [fuente original](../../../superpowers/plans/2026-10-12-local-ai-runtimes.md)

# Tiempos de ejecución de IA locales y comprobaciones restantes

Objetivo: conectar tiempos de ejecución de IA locales adicionales, recibir revisiones de diseño y hacer visibles los lanzamientos abiertos. El usuario autoriza la implementación sin preguntas. Se mantienen los cambios existentes.

1. Transporte loopback y pruebas de inspección de paquetes previas a la implementación ONNX; Verifique direcciones de red, límites de salida, abortos y recursos desconocidos.
2. Mensajes de planificación compartidos; Ollama recibido, LM Studio (127.0.0.1:1234) y llama.cpp (127.0.0.1:8080) agregados. Sin URL en la nube ni cambio automático de proveedor. Los servidores locales pueden reenviarse a sí mismos; Su funcionamiento sigue siendo responsabilidad del operador.
3. ONNX Runtime GenAI CPU en proceso propio con tiempo de espera, protocolo JSON limitado y terminación de proceso. Seleccione explícitamente la carpeta del modelo; ningún modelo se descarga automáticamente. Solo modelos de texto compatibles con GenAI, no redes .onnx arbitrarias.
4. Selección WPF para tiempo de ejecución, modelo, carpeta ONNX; todos ofrecen el mismo diseño rigurosamente probado.
5. Banco de pruebas con límites claros para hardware, DPI, panel móvil y macro pad exclusivo. Los controles físicos y un controlador firmado/con licencia no pueden ser reemplazados por software aquí.
6. Versión de compilación, pruebas de core/Windows, humo WPF, pruebas de trabajadores/paquetes y revisión independiente. Crear nuevo paquete completo; Sólo informe la calidad del modelo como probada si el modelo realmente existe.

Resultado: cuatro tiempos de ejecución implementados, ONNX probado de forma nativa por separado, elección del proveedor y banco de pruebas en el humo empaquetado WPF aprobado. Core/Windows suites verde. Revise cómo encontrar el límite Unicode con constante común y regresión fija. Las aprobaciones de cambios reales de hardware/móvil/DPI, la liberación de controladores, las traducciones técnicas completas y la calidad de las instrucciones en vivo siguen siendo un trabajo abierto explícito; no emitido como completo.

Continuación: se implementan modelos separados de Jinja y nombrados/default; se han realizado pruebas de regresión de tipo/limite/falback. 27 Descargar el modelo de instrucción real separadamente y SHA256 comprobado: la referencia nativa de CPU funciona, pero 0/4 Los casos de planificación cumplen con el contrato de JSON; no se ejecutan acciones. Revisión independiente: nul-fallback corregido y probado, después de no se han encontrado más concretos. Windows-Testado de TI, Edición de Release 0 Alertas y errores.
