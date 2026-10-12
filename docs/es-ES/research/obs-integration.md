<!-- generated-doc locale=es-ES source=docs/research/obs-integration.md sha256=a4e6ca4368b28daa73ffbdda4fb817863e3fa15d52da00d26bbd32ce1f05f9b7 -->

> Traducción asistida por máquina. Se comprueban la estructura, los ejemplos de código y los enlaces; esto no constituye una revisión por hablantes nativos ni una aprobación jurídica. Los informes históricos conservan sus fechas y alcance originales. [fuente original](../../research/obs-integration.md)

# OBS Integración de estudio

[English (US)](obs-integration.md) | [Deutsch](obs-integration.md) | [Français](obs-integration.md) | [Italiano](obs-integration.md)

Aivana utiliza el protocolo obs-websocket v5 de OBS Studio sobre ClientWebSocket; no instala una biblioteca cliente OBS de terceros. El protocolo se abre con el servidor Hola (código de operación 0), responde con Identificar (código de operación 1), espera a Identificado (código de operación 2), envía una solicitud incluida en la lista blanca (código de operación 6), luego coincide con su RequestResponse (código de operación 7) por ID de solicitud. La autenticación de contraseña sigue el cálculo de desafío SHA-256 documentado en dos etapas de OBS.

Habilite **Herramientas → WebSocket Configuración del servidor** en OBS. El punto final predeterminado es **ws://127.0.0.1:4455**. Establezca el campo **OBS-WebSocket-Adresse** para otro punto final. Un punto final remoto debe utilizar wss://; ws:// sin cifrar se acepta solo para una dirección de bucle invertido. El punto final también puede incluir una ruta o un puerto personalizado. Aivana rechaza las credenciales de URL incrustadas.

Cuando OBS tenga habilitada la autenticación, guarde su contraseña desde el editor de acciones OBS. Aivana lo almacena en el Administrador de credenciales del usuario actual Windows como una credencial genérica denominada **Aivana.OBS:<punto final normalizado>**. Nunca se escribe en un archivo de acción o configuración. Para implementaciones administradas, **AIVANA_OBS_PASSWORD** permanece disponible como respaldo cuando el punto final no tiene una contraseña guardada. OBS puede permanecer sin autenticar, en cuyo caso ninguna de las dos es necesaria. Evite los argumentos de la línea de comandos para la contraseña porque las líneas de comandos del proceso se pueden inspeccionar.

El catálogo del controlador y la configuración admiten **StartRecord**, **StopRecord**, **ToggleRecord**, **PauseRecord**, **ResumeRecord**, **StartStream**, **StopStream**, **ToggleStream**, **SetCurrentProgramScene** y **ToggleInputMute**. Los primeros ocho no aceptan argumentos. SetCurrentProgramScene usa el nombre de la escena del editor; ToggleInputMute usa su nombre de entrada. El cliente de protocolo acepta la misma lista de permitidos.

Cada acción obtiene una nueva conexión con un 10-segundo límite y sin reintento automático. Esto es importante para los comandos de alternancia: después de enviar la solicitud, un tiempo de espera o una conexión interrumpida pueden dejar el resultado desconocido, y volver a intentarlo podría alternar el estado nuevamente. OBS Los errores de solicitud se devuelven con su código de estado y (cuando se proporciona) el comentario del servidor.

Referencias del protocolo:

- [OBS WebSocket especificación de protocolo v5](https://github.com/obsproject/obs-websocket/blob/master/docs/generated/protocol.md)
- [OBS WebSocket fórmula de autenticación y códigos de mensaje](https://github.com/obsproject/obs-websocket/blob/master/docs/generated/protocol.md#creating-an-authentication-string)
- [OBS WebSocket Implementación de la solicitud de entrada de medios y valores enum de acción aceptados](https://github.com/obsproject/obs-websocket/blob/master/src/requesthandler/RequestHandler_MediaInputs.cpp)

Las notas del protocolo y los nombres de las operaciones se compararon con la documentación del proyecto OBS en 2026-10-08.
