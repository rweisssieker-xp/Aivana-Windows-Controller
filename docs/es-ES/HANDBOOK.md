<!-- generated-doc locale=es-ES source=docs/HANDBOOK.md sha256=8c6422e18d8939164f91da28603ed597f478f1a9b8bb41e14cbe4f6630bab3fc -->

> Traducción asistida por máquina. Se comprueban la estructura, los ejemplos de código y los enlaces; esto no constituye una revisión por hablantes nativos ni una aprobación jurídica. Los informes históricos conservan sus fechas y alcance originales. [fuente original](../HANDBOOK.md)

# MAKRIVO — Manual de usuario

Fabricante: **Aivana**. Instantánea de la documentación: **2026-10-12**. Este manual describe la implementación actual del árbol de trabajo. Las integraciones experimentales y las funciones dependientes del hardware se identifican explícitamente. Los informes históricos describen su instantánea fechada, no una garantía de una versión más nueva.

## Documentación e idiomas

Utilice el selector de idioma de documentación para abrir el manual completo, el inventario de funciones, la solución de problemas, las notas de desarrollo, la investigación y los informes de pruebas históricos en cualquiera de los idiomas de la aplicación 27. Las traducciones generadas son asistidas por máquinas y tienen controles estructurales; no todos han recibido reseñas de hablantes nativos. Los identificadores técnicos y los ejemplos ejecutables permanecen sin cambios. La fuente original permanece disponible para comparación. El análisis de licencias traducido es informativo y no reemplaza los términos legales originales de terceros ni un CLUF final.

## Instalación y primer lanzamiento.

El objetivo inicial es Windows 11 x64. Mantenga todos los archivos y subdirectorios de la distribución juntos. Inicie `MAKRIVO.exe`. Un pago de origen no es un instalador. Los paquetes autónomos incluyen su tiempo de ejecución .NET; Los paquetes dependientes del marco requieren los tiempos de ejecución de escritorio .NET 10 y ASP.NET adecuados. Se deben incluir ONNX y los hosts complementarios para sus respectivas funciones.

1. Inicie MAKRIVO y elija el idioma de la interfaz en Configuración. Automático sigue a Windows. La distribución del teclado es una configuración separada.
2. Elija un perfil y luego agregue un mosaico a través del catálogo de acciones con capacidad de búsqueda. Dale un nombre reconocible y elige sus acciones.
3. Revise los pasos y asigne un acceso directo si es necesario. Evite combinaciones ya utilizadas por Windows o su aplicación de destino.
4. Guarde el borrador. Los cambios no guardados no son la configuración guardada.
5. Pruebe con una acción inofensiva, como abrir un documento, e inspeccione los comentarios de ejecución.

Cerrar la ventana principal normalmente deja la aplicación en el área de notificación. Haga doble clic en el icono de su bandeja para volver a abrirlo o utilice el menú de la bandeja para salir. Comenzar con Windows es opcional e inicialmente está deshabilitado. El control de pausa suspende las vinculaciones; el icono muestra una insignia de pausa. Cancele la ejecución de macros a través de la interfaz de usuario, la bandeja o el acceso directo de cancelación configurado; el valor predeterminado es `Ctrl+Alt+Escape`.

## Mosaicos, acciones y la paleta de comandos.

Los mosaicos pertenecen a perfiles y pueden contener varios pasos ordenados. El editor admite guardar/descartar, obtener vista previa, deshacer/rehacer y reordenar pasos. El catálogo proporciona ajustes preestablecidos para programas, documentos, URL, texto, portapapeles, accesos directos, medios, audio, ventanas, acciones del sistema e integraciones compatibles. Un ajuste preestablecido que requiere parámetros se abre como borrador.

Abra la paleta con `Ctrl+K` mientras MAKRIVO tiene el foco. Busque acciones guardadas, ajustes preestablecidos, cambio de perfil y herramientas. Las teclas de flecha seleccionan; Enter se activa; `Ctrl+Enter` edita un elemento admitido; La fuga se cierra. Algunos comandos listos para usar se ejecutan inmediatamente, mientras que las acciones configuradas requieren revisión. Se puede configurar un acceso directo a la paleta global independiente; las nuevas configuraciones utilizan `Ctrl+Shift+Space`.

Los pasos de texto y portapapeles admiten `{{date}}`, `{{time}}`, `{{datetime}}`, `{{clipboard}}`, `{{profile}}`, `{{process}}` y `{{user}}`. El formato de fecha es `yyyy-MM-dd`; El formato de hora es `HH:mm`. Las variables desconocidas permanecen sin cambios. La lectura del portapapeles se produce sólo cuando se solicita. Una acción del portapapeles escribe texto sin pegarlo automáticamente.

Las acciones iniciadas desde la interfaz de usuario pueden apuntar a la última aplicación externa. Si el objetivo desaparece o Windows bloquea la activación en primer plano, active manualmente el objetivo y vuelva a intentarlo. La entrega exitosa de la clave no prueba que el objetivo haya completado su tarea.

## Perfiles y configuración del lugar de trabajo.

Los perfiles pueden coincidir con el nombre de un proceso y parte del título de una ventana. Cuando ambos están configurados, ambos deben coincidir. El orden del perfil resuelve los partidos en competencia; el perfil predeterminado es el de reserva. Habilite la selección automática explícitamente. Bloquear perfil evita cambios automáticos para la sesión actual.

Abra Configuración del lugar de trabajo desde Herramientas o la paleta. Seleccione programas, documentos, sitios web, un destino de audio/visualización o una escena OBS. El asistente prepara perfiles de trabajo, reuniones, streaming, investigaciones o proyectos. Inspeccione los pasos generados antes de guardar. El programa de instalación no ejecuta las acciones preparadas.

## Macros, condiciones y grabación.

Las macros ejecutan sus pasos en orden y soportan esperas, repeticiones y cancelaciones. El límite general de repeticiones es 100. Utilice la Vista previa antes de ejecutar acciones que escriban en otra aplicación o cambien el estado del sistema.

Esperar a que la ventana de destino acepte un proceso, una subcadena de título y un tiempo de espera de 1 a 600,000 ms. Se requiere al menos un filtro. Una ventana visible coincidente debe responder y pasar a primer plano antes de continuar con los pasos siguientes. Una ventana lista no prueba que un documento o servicio haya terminado de cargarse.

El editor de flujo de trabajo agrega condiciones de existencia de archivos, ejecución de procesos y títulos de ventanas. Una condición falsa omite el paso y puede omitir pasos adicionales. Negar la condición para expresar una rama alternativa. Seleccione detenerse en caso de error o continuar en caso de error deliberadamente; continuar puede dejar pasos posteriores sin las condiciones previas esperadas.

La grabadora de acciones captura acordes de atajos y espera, no una escritura normal. Admite atajos de modificación y teclas de función. Escape detiene la grabación. Revise la lista y adóptela como un borrador de mosaico no guardado, luego guárdela explícitamente. La grabación de clics en la interfaz de usuario es una herramienta independiente que se describe a continuación.

## Teclados, gestos y capas.

La página del teclado enumera las interfaces detectadas y los diseños de referencia. Un solo teclado físico puede exponer varias interfaces. Seleccione el idioma y la geometría correctos; el nombre de un producto por sí solo no identifica el diseño ISO o ANSI.

Las teclas de acceso rápido globales estándar se aplican en todos los teclados. Los enlaces Raw Input específicos del dispositivo observan un dispositivo particular y un código de escaneo, incluidas las funciones admitidas y las teclas multimedia. **Raw Input no suprime la entrada ordinaria Windows. Un segundo teclado exclusivo no está listo para producción.** Aún se requiere un backend de controlador de filtro firmado, autorizado y probado; ninguno está instalado por MAKRIVO.

Las combinaciones de teclas especiales admitidas pueden presionar, tocar, mantener presionado o tocar dos veces inmediatamente. Las claves de capa retenida seleccionan temporalmente otra asignación y restauran la capa base al liberarla; Las teclas selectoras de capa no ejecutan su propia macro. Los umbrales de retención varían de 150 a 5,000 ms; toque dos veces las ventanas de 150 a 1,000 ms. Estas asignaciones son distintas de las teclas de acceso rápido globales ordinarias.

El asistente de claves especiales detecta claves compatibles sin ejecutar las acciones asignadas durante la captura. El diagnóstico explica los dispositivos no disponibles, los enlaces pausados, las discrepancias de perfiles y los conflictos. La superposición muestra el diseño y los enlaces seleccionados; su acceso directo de retención predeterminado es `Ctrl+Alt+F1`.

| Integración | Requisitos y limitaciones |
| --- | --- |
| Corsair | Ejecutando iCUE, acceso al SDK y un modelo compatible. G1–G6 y funciones RGB compatibles; Los controles patentados Fn, rueda, bloqueo y perfil generalmente no están expuestos por la API de la tecla G. |
| LogitechG | Las bibliotecas locales originales de SDK x64 G-key/LED requieren aprobación explícita. Los eventos de la tecla G no identifican de forma única varios teclados físicos idénticos. |
| Razer | Chroma RGB y entrada estándar; asigne controles propietarios en Synapse cuando sea necesario. |
| SteelSeries | GameSense RGB y entrada estándar; mapear controles propietarios en GG cuando sea necesario. |
| Wooting | SDK x64 local original compatible para valores y umbrales analógicos; La entrada HID normal permanece activa. |
| Otros teclados | Teclas de acceso rápido estándar o enlaces de códigos de escaneo físicos compatibles. Los controles Fn y propietarios dependen del modelo y la configuración del proveedor. |

La investigación del fabricante describe capacidades documentadas, no implementadas ni probadas, con cada dispositivo listado. Los nuevos adaptadores todavía necesitan una aceptación real del hardware. No descargue archivos binarios del SDK de fuentes que no sean de confianza; Los derechos de distribución del SDK son independientes de la compatibilidad técnica.

## Audio, pantallas y OBS

Los pasos sonoros de la 0 para 100 porcentaje y salida mute/unmute/toggle soportada o puntos finales de micrófono. elegir un punto final específico o utilizar el Windows un objetivo salvo perdido produce un error en lugar de controlar silenciosamente otro dispositivo. Revisar los dispositivos después de la reconectividad. control de punto final no es una reclamación de soporte de mixer independiente por aplicación.

Las acciones de visualización aumentan/disminuyen el brillo en 10 puntos porcentuales o establecen un valor de 0 a 100. Las pantallas internas utilizan interfaces WMI compatibles; Los monitores externos requieren compatibilidad con DDC/CI y, por lo general, su configuración del menú del monitor. Las bases, adaptadores y cables pueden bloquear el control. Elija todas las pantallas compatibles o un monitor. La reconexión puede cambiar las identidades disponibles; revisar el objetivo guardado. Las lecturas faltantes se muestran como no disponibles en lugar de valores medidos.

OBS requiere WebSocket v5 habilitado en OBS Tools. Configure el punto final, pruebe la conexión y cargue los nombres de escenas/entradas antes de guardar. Las acciones admitidas incluyen cambio de escena, inicio/detención/alternancia de transmisión/grabación, pausa/reanudación de grabación y silenciamiento de entrada. Las contraseñas se pueden almacenar en Windows Credential Manager y están excluidas de las exportaciones de configuración. Un comando OBS exitoso no establece todos los efectos de transmisión descendente.

## Historial de ejecución y verificación de resultados.

Habilite Verificar resultado para ver los pasos adecuados. La aplicación distingue comprobaciones previas al vuelo, efectos confirmados, fallos, cancelaciones y efectos que no se pueden verificar. La existencia del proceso no es la preparación de la aplicación. Los cambios relativos, los atajos y la entrada de texto no pueden recibir un reclamo de éxito general. Inspeccione el registro de actividad y el historial de verificación cuando falle una acción. Los errores de configuración deben corregirse antes de volver a ejecutar la macro completa.

## Automatización de la interfaz de usuario y grabación de clics

Añade un paso de automatización de UI, seleccione la operación y captura el objetivo debajo del indicador después del retraso mostrado. Capturas solas no ejecutan nada. `Invoke`, `Select`, `Toggle`, `Focus`, `SetValue` y `WaitState`. Los campos de contraseña, los controles desactivados y los selectores ambiguos se rechazan.

La automatización de la interfaz de usuario se ejecuta en un proceso complementario limitado. Depende del soporte de accesibilidad y los permisos Windows de la aplicación de destino. La grabadora de clics captura acciones admitidas y pausas; no registra la entrada de texto general. Revisar y validar su borrador antes de adoptarlo. Los cambios en la interfaz de usuario en una aplicación externa pueden invalidar los selectores. El aislamiento de procesos limita los bloqueos y las cancelaciones, pero no es un entorno limitado de seguridad.

## Complementos

Abra Extensiones para inspeccionar un paquete antes de importarlo. Se verifican la estructura del paquete, el manifiesto y los hashes. Una importación comienza deshabilitada. Lea las acciones declaradas y confíe explícitamente en el editor antes de habilitarlas. Los complementos se ejecutan con sus permisos de usuario en un proceso separado; ese proceso es **, no es un sandbox**. Instale únicamente paquetes en los que confíe.

El administrador admite exportar, deshabilitar, desinstalar y restaurar/descartar una versión anterior. Las dependencias faltantes bloquean la invocación. El paquete de ejemplo proporciona acciones de cuenta regresiva y de estado local. El esquema admitido es un subconjunto de esquema JSON documentado, no todas las características posibles de esquema JSON. No existe un mercado público de complementos.

## Panel móvil

1. Abra Mobile Panel, seleccione una IP local y elija explícitamente los perfiles a exponer. Solo se puede acceder a la dirección de loopback predeterminada en la propia PC; seleccione la dirección LAN adecuada para un teléfono o tableta.
2. Inicie el panel y lea su dirección HTTPS, la huella digital del certificado y el código de emparejamiento de seis dígitos de un solo uso.
3. En un dispositivo en la misma red confiable, abra la dirección. Compare la huella digital SHA-256 a través de un canal confiable antes de confiar en el certificado local. La aplicación no instala automáticamente un certificado confiable.
4. Emparéjelo usando el código mostrado y pruebe un mosaico inofensivo. Es posible que se necesite un permiso manual de firewall Windows para acceder a la LAN. No exponga el puerto a Internet.
5. Detenga el panel cuando haya terminado o revoque los clientes emparejados a través de la aplicación.

Las claves del servidor privado están protegidas con Windows DPAPI; Las credenciales del cliente se almacenan como hashes. Cambiar la IP o renovar el certificado puede requerir una confianza renovada. El panel muestra estados de acción y verificación, no todos los sensores en vivo posibles. Las comprobaciones de diseño del navegador y HTTPS locales no reemplazan la aceptación real de la LAN del teléfono/tableta.

## Asistente de reuniones locales de IA

Este asistente experimental opcional está deshabilitado de forma predeterminada. Ábralo desde Herramientas, habilítelo explícitamente, seleccione un tiempo de ejecución/modelo y agregue solo los recursos que desea que considere. Indique el objetivo, cree un borrador, inspeccione las solicitudes no resueltas y transfiera el resultado al editor normal. Nada se ejecuta automáticamente; guardar y ejecutar siguen siendo acciones explícitas. No hay un respaldo automático del tiempo de ejecución.

| Tiempo de ejecución | Configuración |
| --- | --- |
| Ollama | Servicio local independiente y un modelo instalado localmente en `127.0.0.1:11434`. Se rechazan los modelos remotos/en la nube declarados. |
| LM Studio | Servidor local con un modelo cargado localmente y soporte de esquema JSON en `127.0.0.1:1234`. No habilite el enrutamiento remoto para el procesamiento local. |
| llama.cpp | `llama-server` local y modelo compatible con soporte de esquema JSON en `127.0.0.1:8080`. |
| ONNX Tiempo de ejecución GenAI CPU | Carpeta de modelo de texto local compatible y confiable GenAI; seleccione `genai_config.json`. La inferencia utiliza un proceso complementario incluido sin HTTP. |

Los clientes HTTP utilizan únicamente loopback, no utilizan proxies y no siguen redireccionamientos. MAKRIVO no puede establecer de manera confiable si un servidor local compatible llama internamente a un servicio remoto. Los servidores locales autenticados no se pueden configurar en esta versión. Los pesos de los modelos, Python y los servidores de inferencia locales no se descargan ni instalan automáticamente.

ONNX Los paquetes necesitan `tokenizer.json` y un modelo de chat válido. formularios de modelo apoyados en `tokenizer_config.json` Es una string `chat_template`Un diccionario con una línea `default`, o una lista de templados nombrados/default. si el templado está ausente o nulo, `chat_template.jinja` Se utilizan como un fallback. se rechazan los modelos vacíos, ambiguos, excesivos o incorrectamente tipados. ONNX Los archivos, los modelos de inserción/visualidad y los paquetes de GPU únicos no son intercambiables con compatibles GenAI Modelo de texto: El conjunto GenAI La versión runtime es 0.17.1Utilice sólo modelos confiables; la separación del proceso no es una caja de arena.

El asistente admite programas seleccionados sin argumentos, documentos y sitios web seleccionados y silencia el micrófono cuando se solicita expresamente. No crea mensajes, citas, secuencias de comandos arbitrarias, clics gratuitos, descargas ni cambios de auriculares. Los nombres de objetivos y recursos se envían al tiempo de ejecución local seleccionado; Las rutas de recursos y el contenido de los archivos no se proporcionan por separado. Un objetivo introducido por el usuario puede contener datos personales. El comportamiento de la red propia de un tiempo de ejecución local sigue siendo responsabilidad del usuario.

El presupuesto de solicitud ONNX es 90 segundos, incluida la carga del modelo, hasta 3,072 tokens de aviso y 1,024 tokens generados, también limitados por el contexto del modelo. Se rechazan los JSON con formato incorrecto o truncado y los ID de recursos desconocidos. Cancelar o cerrar para finalizar una solicitud. Una prueba de tiempo de ejecución de modelo aleatorio establece la ejecución, no la calidad de la planificación útil ni el ahorro de tiempo. Los registros de experimentos locales contienen identificadores de eventos y tiempos, no texto de objetivos ni contenido de documentos; no se cargan automáticamente.

La evaluación posterior del pequeño modelo de Instruct produjo planes nulos válidos en cuatro pruebas alemanas / inglesas. la inferencia nativa funcionó, pero el modelo fracasó el contrato de planificación estricto. Esto es un resultado de calidad negativo, no un modelo recomendado o un asistente útil probado. Ver el informe de prueba de tiempo de conducción local para la revisión exacta del modelo y los límites.

## Importación, exportación, sincronización y copia de seguridad

Revise las vistas previas de importación antes de reemplazar una configuración. Las acciones importadas comienzan deshabilitadas y la sincronización portátil elimina los enlaces específicos del hardware. Reasigne dispositivos y habilite acciones deliberadamente después de la inspección. Revise el contenido del texto/acción antes de compartir una exportación; excluir contraseñas no significa que todos los contenidos de acciones creados por el usuario no sean confidenciales.

La sincronización de perfiles intercambiará archivos en una carpeta seleccionada, opcionalmente una gestionada por su cliente en la nube existente. MAKRIVO no proporciona una cuenta en la nube y no almacena credenciales en la nube. Revisiones evitan los sobreescritos silenciosos. Incluir contenidos de acción es una elección explícita. Backup su configuración antes de una importación de reemplazo. `%LOCALAPPDATA%\AivanaWindowsController\settings.json`El nombre de directorio histórico se mantiene para la compatibilidad.Clarar la aplicación antes de copiar/restorarla manualmente.Los certificados de SDK almacenados y los credenciales protegidos por el sistema operativo pueden necesitar ser recreados en otro PC.

## Solución de problemas

| Síntoma | Que comprobar |
| --- | --- |
| El atajo no hace nada | Aplicación en ejecución, enlaces no pausados, perfil activo correcto, mosaico guardado/habilitado, conflictos de accesos directos y destino disponible. |
| La ventana de destino no se puede activar | Actívalo manualmente. Windows las restricciones de primer plano y las diferencias de privilegios pueden bloquear la automatización. |
| No se detecta la clave especial | Servicio de proveedor/SDK/soporte de modelo; configure controles propietarios para emitir una clave estándar compatible si es posible. |
| El segundo teclado también escribe normalmente | Se esperaba con Raw Input. La supresión exclusiva no se implementa para producción. |
| Audio o brillo no disponibles | Actualizar dispositivos; Inspeccione la identidad seleccionada, las sesiones de terminales, la compatibilidad con WMI/DDC/CI y las restricciones de conexión/cable. |
| OBS desconectado | WebSocket v5 habilitado, punto final/contraseña correcta, servicio accesible y nombres de escena/entrada válidos. |
| La automatización de la interfaz de usuario falla | Host complementario incluido, selector válido único, destino habilitado accesible, control y permisos admitidos. Vuelva a intentar un tiempo de espera transitorio solo después de revisar el objetivo. |
| Panel móvil inalcanzable | LAN en lugar de dirección de bucle invertido, misma red, servidor en ejecución, perfiles aprobados, huella digital/confianza del certificado y permisos manuales de firewall. |
| El complemento falla | Integridad del paquete, confianza explícita/estado habilitado, dependencias, compatibilidad de protocolo y registro de ejecución. |
| La solicitud de IA falla | Corrija el tiempo de ejecución local seleccionado, servicio/modelo listo, esquema JSON compatible, carpeta de modelo compatible confiable y límites de solicitudes. |
| El diseño o el texto parecen incorrectos | Separe el idioma de la interfaz de usuario de la geometría del teclado; cambie el tamaño de la ventana, verifique la escala de visualización e informe la página/idioma/compilación exacta. |

Para obtener un informe de error útil, incluya la versión Windows, la compilación/confirmación de la aplicación, el idioma, la escala de visualización, el modelo de hardware, una secuencia mínima y el registro redactado relevante. Elimine rutas personales, contenidos de acciones y credenciales antes de publicar.

## Construcción, pruebas y embalaje.

Utilice el SDK anclado en `global.json`. Desde la raíz del repositorio:

```powershell
dotnet build Aivana.slnx -c Release
dotnet run --project tests/Aivana.Tests -c Release
dotnet run --project tests/Aivana.Windows.Tests -c Release
dotnet run --project src/Aivana.Desktop -c Release -- --smoke-test
./tools/publish-complete.ps1 -SelfContained
python tools/check_documentation.py
```

Windows las pruebas de integración pueden abrir ventanas aisladas y configurar/liberar temporalmente enlaces de proveedores compatibles. Ejecútelos sin que otro controlador sea propietario de esos enlaces. Las sondas de brillo/audio predeterminadas son de sólo lectura; Las pruebas explícitas de ida y vuelta de hardware están separadas. Los protocolos simulados, las capturas de pantalla renderizadas y los gráficos de inferencia aleatoria no deben representarse como una aceptación real de la calidad del modelo o del hardware.

El editor completo reúne hosts complementarios, activos móviles, el complemento de ejemplo, el tiempo de ejecución ONNX y avisos de dependencia disponibles. Mantenga sus subdirectorios. Una compilación exitosa no es un instalador firmado ni una aprobación de lanzamiento legal. La fuente y las compilaciones pueden evolucionar por separado; compare la instantánea de la documentación y confirme con la versión instalada.

## Licencia, privacidad y estado de publicación

El modelo comercial previsto es una compra única con uso perpetuo de la versión principal adquirida; El precio final, la asignación de dispositivos y los términos de actualización deben establecerse en los términos de venta reales. Este manual no establece un sistema de compra/activación de producción. No se requiere una cuenta en la nube para el control diario.

No distribuya archivos binarios propietarios del SDK sin permiso comprobado. Conserve los avisos de licencia y de terceros para cada dependencia enviada, incluidos los paquetes .NET y ONNX. Los pesos de los modelos tienen sus propios términos. La auditoría de licencia fechada es anterior a algunas dependencias nuevas y no es una autorización para esas adiciones. El hecho de que el repositorio sea público no otorga por sí mismo una licencia general de código abierto.

El hardware de proveedores reales, el funcionamiento de LAN de teléfono/tableta, las transiciones mixtas de monitor/DPI, el uso prolongado, la revisión lingüística completa, el soporte exclusivo de controladores y la útil calidad de la planificación de la IA siguen siendo trabajos de aceptación separados. Nunca infieras aprobación únicamente por la presencia de una función.
