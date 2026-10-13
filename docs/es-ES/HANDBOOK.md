<!-- generated-doc locale=es-ES source=docs/HANDBOOK.md sha256=8c6422e18d8939164f91da28603ed597f478f1a9b8bb41e14cbe4f6630bab3fc -->

> Traducción asistida por máquina. Se comprueban la estructura, los ejemplos de código y los enlaces; esto no constituye una revisión por hablantes nativos ni una aprobación jurídica. Los informes históricos conservan sus fechas y alcance originales. [fuente original](../HANDBOOK.md)

# MAKRIVO — Manual de usuario

Fabricante: **Aivana**. Instantánea de la documentación: **2026-10-12**. Este manual describe la implementación actual del árbol de trabajo. Las integraciones experimentales y las funciones dependientes del hardware se identifican explícitamente. Los informes históricos describen su instantánea fechada, no una garantía de una versión más nueva.

## Documentación e idiomas

Utilice el selector de idioma de la documentación para abrir el manual completo, el inventario de funciones, la solución de problemas, las notas de desarrollo, la investigación y los informes históricos de pruebas en cualquiera de los 27 idiomas de la aplicación. Las traducciones generadas cuentan con asistencia automática y comprobaciones estructurales; no todas han sido revisadas por hablantes nativos. Los identificadores técnicos y los ejemplos ejecutables no cambian. La fuente original está disponible para comparación. El análisis de licencias traducido es informativo y no sustituye las condiciones legales originales de terceros ni un CLUF final.

## Instalación y primer lanzamiento.

La plataforma inicial es Windows 11 x64. Mantenga juntos todos los archivos y subdirectorios de la distribución. Inicie `MAKRIVO.exe`. Una copia del repositorio fuente no es un instalador. Los paquetes autónomos incluyen su entorno .NET; los dependientes del framework requieren los entornos .NET 10 Desktop y ASP.NET adecuados. Deben incluirse ONNX y los hosts auxiliares para sus respectivas funciones.

1. Inicie MAKRIVO y elija el idioma de la interfaz en Configuración. Automático sigue a Windows. La distribución del teclado es una configuración separada.
2. Elija un perfil y añada un mosaico mediante el catálogo de acciones con búsqueda. Asígnele un nombre reconocible y elija sus acciones.
3. Revise los pasos y asigne un acceso directo si es necesario. Evite combinaciones ya utilizadas por Windows o su aplicación de destino.
4. Guarde el borrador. Los cambios no guardados no son la configuración guardada.
5. Pruebe con una acción inofensiva, como abrir un documento, e inspeccione los comentarios de ejecución.

Cerrar la ventana principal normalmente deja la aplicación en el área de notificación. Haga doble clic en su icono para volver a abrirla o use el menú del icono para salir. El inicio con Windows es opcional y está deshabilitado inicialmente. El control de pausa suspende las asignaciones; el icono muestra un indicador de pausa. Cancele las macros en curso desde la interfaz, el área de notificación o el atajo de cancelación configurado; el predeterminado es `Ctrl+Alt+Escape`.

## Mosaicos, acciones y la paleta de comandos.

Los mosaicos pertenecen a perfiles y pueden contener varios pasos ordenados. El editor permite guardar o descartar cambios, previsualizar, deshacer/rehacer y reordenar pasos. El catálogo ofrece ajustes preestablecidos para programas, documentos, URL, texto, portapapeles, atajos, medios, audio, ventanas, acciones del sistema e integraciones compatibles. Un ajuste que requiere parámetros se abre como borrador.

Abra la paleta con `Ctrl+K` cuando MAKRIVO tenga el foco. Busque acciones guardadas, ajustes preestablecidos, cambios de perfil y herramientas. Las flechas seleccionan; Intro activa; `Ctrl+Enter` edita un elemento compatible; Escape cierra. Algunos comandos listos para usar se ejecutan inmediatamente, mientras que las acciones configuradas requieren revisión. Se puede configurar un atajo global independiente para la paleta; las nuevas configuraciones usan `Ctrl+Shift+Space`.

Los pasos de texto y portapapeles admiten `{{date}}`, `{{time}}`, `{{datetime}}`, `{{clipboard}}`, `{{profile}}`, `{{process}}` y `{{user}}`. El formato de fecha es `yyyy-MM-dd`; El formato de hora es `HH:mm`. Las variables desconocidas permanecen sin cambios. La lectura del portapapeles se produce sólo cuando se solicita. Una acción del portapapeles escribe texto sin pegarlo automáticamente.

Las acciones iniciadas desde la interfaz pueden dirigirse a la última aplicación externa. Si el destino desaparece o Windows bloquea su activación en primer plano, actívelo manualmente y vuelva a intentarlo. La entrega correcta de las pulsaciones de teclas no demuestra que la aplicación de destino haya completado su tarea.

## Perfiles y configuración del lugar de trabajo.

Los perfiles pueden coincidir con un nombre de proceso y parte del título de una ventana. Si se especifican ambos criterios, ambos deben coincidir. El orden de los perfiles resuelve las coincidencias en competencia; el perfil predeterminado se usa como alternativa. Active explícitamente la selección automática. Bloquear perfil evita cambios automáticos durante la sesión actual.

Abra la configuración del entorno de trabajo desde Herramientas o la paleta. Seleccione programas, documentos, sitios web, un destino de audio/pantalla o una escena OBS. El asistente prepara perfiles de trabajo, reuniones, streaming, investigación o proyectos. Inspeccione los pasos generados antes de guardar. La configuración no ejecuta las acciones preparadas.

## Macros, condiciones y grabación.

Las macros ejecutan sus pasos en orden y soportan esperas, repeticiones y cancelaciones. El límite general de repeticiones es 100. Utilice la Vista previa antes de ejecutar acciones que escriban en otra aplicación o cambien el estado del sistema.

La acción Esperar a la ventana de destino acepta un proceso, una subcadena del título y un tiempo de espera de 1 a 600000 ms. Se requiere al menos un filtro. Una ventana visible coincidente debe responder y pasar a primer plano antes de continuar con los pasos siguientes. Una ventana lista no demuestra que un documento o servicio haya terminado de cargarse.

El editor de flujos de trabajo añade condiciones de existencia de archivo, proceso en ejecución y título de ventana. Una condición falsa omite el paso y puede saltar otros pasos hacia delante. Niegue la condición para expresar una rama alternativa. Elija deliberadamente detenerse o continuar en caso de error; continuar puede dejar los pasos posteriores sin las condiciones previas esperadas.

La grabadora de acciones captura combinaciones de teclas de atajo y esperas, no la escritura normal. Admite atajos con teclas modificadoras y teclas de función. Escape detiene la grabación. Revise la lista y adóptela como borrador de mosaico sin guardar; después guárdelo explícitamente. La grabación de clics en la interfaz es una herramienta distinta que se describe a continuación.

## Teclados, gestos y capas

La página del teclado enumera las interfaces detectadas y los diseños de referencia. Un solo teclado físico puede exponer varias interfaces. Seleccione el idioma y la geometría correctos; el nombre de un producto por sí solo no identifica el diseño ISO o ANSI.

Los atajos globales estándar se aplican a todos los teclados. Las asignaciones Raw Input específicas de dispositivo observan un dispositivo y un código de exploración concretos, incluidas las teclas de función y multimedia compatibles. **Raw Input no suprime la entrada normal de Windows. El uso exclusivo de un segundo teclado no está listo para producción.** Sigue siendo necesario un backend de controlador de filtro firmado, con licencia y probado; MAKRIVO no instala ninguno.

Las asignaciones compatibles de teclas especiales pueden usar pulsación inmediata, pulsación breve, pulsación mantenida o doble pulsación. Las teclas de capa mantenidas seleccionan temporalmente otra asignación y restauran la capa base al soltarlas; las teclas selectoras de capa no ejecutan su propia macro. Los umbrales de pulsación mantenida van de 150 a 5000 ms; los intervalos de doble pulsación van de 150 a 1000 ms. Estas asignaciones son distintas de los atajos globales ordinarios.

El asistente de teclas especiales detecta teclas compatibles sin ejecutar las acciones asignadas durante la captura. El diagnóstico explica dispositivos no disponibles, asignaciones en pausa, perfiles que no coinciden y conflictos. La superposición muestra la distribución y las asignaciones seleccionadas; su atajo predeterminado, que debe mantenerse pulsado, es `Ctrl+Alt+F1`.

| Integración | Requisitos y limitaciones |
| --- | --- |
| Corsair | Ejecutando iCUE, acceso al SDK y un modelo compatible. G1–G6 y funciones RGB compatibles; Los controles patentados Fn, rueda, bloqueo y perfil generalmente no están expuestos por la API de la tecla G. |
| LogitechG | Las bibliotecas locales originales de SDK x64 G-key/LED requieren aprobación explícita. Los eventos de la tecla G no identifican de forma única varios teclados físicos idénticos. |
| Razer | Chroma RGB y entrada estándar; asigne controles propietarios en Synapse cuando sea necesario. |
| SteelSeries | GameSense RGB y entrada estándar; mapear controles propietarios en GG cuando sea necesario. |
| Wooting | SDK x64 local original compatible para valores y umbrales analógicos; La entrada HID normal permanece activa. |
| Otros teclados | Teclas de acceso rápido estándar o enlaces de códigos de escaneo físicos compatibles. Los controles Fn y propietarios dependen del modelo y la configuración del proveedor. |

La investigación de fabricantes describe capacidades documentadas, no compatibilidad implementada o probada con cada dispositivo listado. Los nuevos adaptadores todavía requieren validación con hardware real. No descargue binarios SDK de fuentes no fiables; los derechos de distribución del SDK son independientes de la compatibilidad técnica.

## Audio, pantallas y OBS

Los pasos de audio ajustan el volumen de 0 a 100 por ciento y silencian, reactivan o alternan el silencio de las salidas o micrófonos compatibles. Elija un dispositivo concreto o el predeterminado de Windows. Si el destino guardado no está disponible, se genera un error en lugar de controlar otro dispositivo sin avisar. Actualice los dispositivos tras volver a conectarlos. El control de dispositivos de audio no implica que se admita un mezclador independiente por aplicación.

Las acciones de visualización aumentan/disminuyen el brillo en 10 puntos porcentuales o establecen un valor de 0 a 100. Las pantallas internas utilizan interfaces WMI compatibles; Los monitores externos requieren compatibilidad con DDC/CI y, por lo general, su configuración del menú del monitor. Las bases, adaptadores y cables pueden bloquear el control. Elija todas las pantallas compatibles o un monitor. La reconexión puede cambiar las identidades disponibles; revisar el objetivo guardado. Las lecturas faltantes se muestran como no disponibles en lugar de valores medidos.

OBS requiere WebSocket v5 habilitado en OBS Tools. Configure el punto final, pruebe la conexión y cargue los nombres de escenas/entradas antes de guardar. Las acciones admitidas incluyen cambio de escena, inicio/detención/alternancia de transmisión/grabación, pausa/reanudación de grabación y silenciamiento de entrada. Las contraseñas se pueden almacenar en Windows Credential Manager y están excluidas de las exportaciones de configuración. Un comando OBS exitoso no establece todos los efectos de transmisión descendente.

## Historial de ejecución y verificación de resultados.

Active Verificar resultado en los pasos adecuados. La aplicación distingue comprobaciones previas, efectos confirmados, fallos, cancelaciones y efectos que no se pueden verificar. La existencia de un proceso no significa que la aplicación esté lista. Los cambios relativos, los atajos y la introducción de texto no pueden declararse siempre correctos. Inspeccione el registro de actividad y el historial de verificación si falla una acción. Corrija los errores de configuración antes de volver a ejecutar la macro completa.

## Automatización de la interfaz de usuario y grabación de clics

Añada un paso de automatización de la interfaz de usuario, seleccione la operación y capture el destino situado bajo el puntero tras el retraso indicado. La captura por sí sola no ejecuta nada. Compruebe el selector y exija una coincidencia única. Las operaciones compatibles incluyen `Invoke`, `Select`, `Toggle`, `Focus`, `SetValue` y `WaitState`. Se rechazan los campos de contraseña, los controles deshabilitados y los selectores ambiguos.

La automatización de la interfaz se ejecuta en un proceso auxiliar con límites definidos. Depende del soporte de accesibilidad Windows y de los permisos de la aplicación de destino. La grabadora de clics captura las acciones compatibles y las pausas; no registra la introducción general de texto. Revise y valide el borrador antes de adoptarlo. Los cambios de interfaz de una aplicación externa pueden invalidar los selectores. El aislamiento de procesos limita las consecuencias de los fallos y delimita la cancelación, pero no es un entorno aislado de seguridad.

## Complementos

Abra Extensiones para inspeccionar un paquete antes de importarlo. Se comprueban la estructura del paquete, el manifiesto y los hashes. El paquete importado empieza deshabilitado. Lea las acciones declaradas y otorgue explícitamente su confianza al editor antes de habilitarlo. Los complementos se ejecutan con sus permisos de usuario en un proceso separado; ese proceso **no es un entorno aislado de seguridad**. Instale solo paquetes en los que confíe.

El administrador admite exportar, deshabilitar, desinstalar y restaurar/descartar una versión anterior. Las dependencias faltantes bloquean la invocación. El paquete de ejemplo proporciona acciones de cuenta regresiva y de estado local. El esquema admitido es un subconjunto de esquema JSON documentado, no todas las características posibles de esquema JSON. No existe un mercado público de complementos.

## Panel móvil

1. Abra Mobile Panel, seleccione una IP local y elija explícitamente los perfiles a exponer. Solo se puede acceder a la dirección de loopback predeterminada en la propia PC; seleccione la dirección LAN adecuada para un teléfono o tableta.
2. Inicie el panel y lea su dirección HTTPS, la huella digital del certificado y el código de emparejamiento de seis dígitos de un solo uso.
3. En un dispositivo en la misma red confiable, abra la dirección. Compare la huella digital SHA-256 a través de un canal confiable antes de confiar en el certificado local. La aplicación no instala automáticamente un certificado confiable.
4. Emparéjelo usando el código mostrado y pruebe un mosaico inofensivo. Es posible que se necesite un permiso manual de firewall Windows para acceder a la LAN. No exponga el puerto a Internet.
5. Detenga el panel cuando haya terminado o revoque los clientes emparejados a través de la aplicación.

Las claves privadas del servidor se protegen con Windows DPAPI; las credenciales de los clientes se almacenan como hashes. Cambiar la IP o renovar el certificado puede requerir renovar la confianza. El panel muestra estados de acciones y de verificación, no todos los sensores posibles en tiempo real. Las comprobaciones de distribución en el navegador y de HTTPS local no sustituyen la validación real de teléfono/tableta en la LAN.

## Asistente de reuniones locales de IA

Este asistente experimental opcional está deshabilitado de forma predeterminada. Ábralo desde Herramientas, actívelo explícitamente, seleccione un entorno de ejecución/modelo y añada solo los recursos que deba considerar. Indique el objetivo, cree un borrador, inspeccione las solicitudes no resueltas y transfiera el resultado al editor habitual. Nada se ejecuta automáticamente; guardar y ejecutar siguen siendo acciones explícitas. No hay cambio automático a otro entorno de ejecución.

| Entorno de ejecución | Configuración |
| --- | --- |
| Ollama | Servicio local independiente y un modelo instalado localmente en `127.0.0.1:11434`. Se rechazan los modelos remotos/en la nube declarados. |
| LM Studio | Servidor local con un modelo cargado localmente y soporte de esquema JSON en `127.0.0.1:1234`. No habilite el enrutamiento remoto para el procesamiento local. |
| llama.cpp | `llama-server` local y modelo compatible con soporte de esquema JSON en `127.0.0.1:8080`. |
| ONNX Tiempo de ejecución GenAI CPU | Carpeta de modelo de texto local compatible y confiable GenAI; seleccione `genai_config.json`. La inferencia utiliza un proceso complementario incluido sin HTTP. |

Los clientes HTTP utilizan únicamente loopback, no utilizan proxies y no siguen redireccionamientos. MAKRIVO no puede establecer de manera confiable si un servidor local compatible llama internamente a un servicio remoto. Los servidores locales autenticados no se pueden configurar en esta versión. Los pesos de los modelos, Python y los servidores de inferencia locales no se descargan ni instalan automáticamente.

Los paquetes ONNX necesitan `tokenizer.json` y una plantilla de chat válida. Las formas admitidas en `tokenizer_config.json` son una cadena `chat_template`, un diccionario con una cadena `default` o una lista inequívoca de plantillas con nombre/predeterminadas. Si la plantilla falta o es nula, se utiliza `chat_template.jinja` como alternativa. Se rechazan las plantillas vacías, ambiguas, demasiado grandes o de tipo incorrecto. Los archivos ONNX arbitrarios, los modelos de embeddings/visión y los paquetes exclusivos para GPU no son intercambiables con modelos de texto GenAI compatibles. La versión del entorno GenAI incluido es 0.17.1, CPU. Utilice solo modelos de confianza; la separación de procesos no es un entorno aislado de seguridad.

El asistente admite los programas seleccionados sin argumentos, los documentos y sitios web seleccionados y el silencio del micrófono cuando se solicita expresamente. No crea mensajes, citas, scripts arbitrarios, clics libres, descargas ni cambios de auriculares. El objetivo y los nombres de los recursos se envían al entorno local seleccionado; las rutas de los recursos y el contenido de los archivos no se proporcionan por separado. Un objetivo escrito por el usuario puede contener datos personales. El comportamiento de red propio del entorno local sigue siendo responsabilidad del usuario.

El presupuesto de una solicitud ONNX es de 90 segundos, incluida la carga del modelo, con un máximo de 3072 tokens de entrada y 1024 tokens generados, también limitado por el contexto del modelo. Se rechazan JSON malformado/truncado e identificadores de recursos desconocidos. Cancele o cierre para terminar una solicitud. Una prueba del entorno con un modelo aleatorio demuestra la ejecución, no una calidad de planificación útil ni ahorro de tiempo. Los registros de experimentos locales contienen identificadores de eventos y duraciones, no el texto de los objetivos ni el contenido de documentos; no se suben automáticamente.

La evaluación posterior del pequeño modelo Instruct no produjo ningún plan válido en cuatro pruebas en alemán/inglés. La inferencia nativa funcionó, pero el modelo incumplió el contrato estricto de planificación. Es un resultado negativo de calidad, no un modelo recomendado ni un asistente de utilidad demostrada. Consulte el informe fechado de pruebas de entornos de ejecución locales para conocer la revisión exacta del modelo y sus límites.

## Importación, exportación, sincronización y copia de seguridad

Revise las vistas previas de importación antes de reemplazar una configuración. Las acciones importadas comienzan deshabilitadas y la sincronización portátil elimina los enlaces específicos del hardware. Reasigne dispositivos y habilite acciones deliberadamente después de la inspección. Revise el contenido del texto/acción antes de compartir una exportación; excluir contraseñas no significa que todos los contenidos de acciones creados por el usuario no sean confidenciales.

La sincronización de perfiles intercambia archivos en una carpeta seleccionada, opcionalmente gestionada por el cliente de nube que ya utiliza. MAKRIVO no proporciona ninguna cuenta en la nube ni almacena credenciales de nube. Las revisiones evitan sobrescrituras silenciosas. Incluir el contenido de las acciones es una elección explícita. Haga una copia de seguridad de la configuración antes de una importación que la sustituya. La configuración predeterminada es `%LOCALAPPDATA%\AivanaWindowsController\settings.json`; se conserva el nombre histórico del directorio por compatibilidad. Cierre la aplicación antes de copiarla o restaurarla manualmente. Puede ser necesario volver a crear las aprobaciones de SDK guardadas y las credenciales protegidas por el sistema operativo en otro PC.

## Solución de problemas

| Síntoma | Que comprobar |
| --- | --- |
| El atajo no hace nada | Aplicación en ejecución, asignaciones sin pausa, perfil activo correcto, mosaico guardado/habilitado, conflictos de atajos y destino disponible. |
| La ventana de destino no se puede activar | Actívalo manualmente. Windows las restricciones de primer plano y las diferencias de privilegios pueden bloquear la automatización. |
| No se detecta la tecla especial | Servicio del fabricante/SDK/compatibilidad del modelo; si es posible, configure los controles propietarios para que emitan una tecla estándar compatible. |
| El segundo teclado también escribe normalmente | Se esperaba con Raw Input. La supresión exclusiva no se implementa para producción. |
| Audio o brillo no disponibles | Actualizar dispositivos; Inspeccione la identidad seleccionada, las sesiones de terminales, la compatibilidad con WMI/DDC/CI y las restricciones de conexión/cable. |
| OBS desconectado | WebSocket v5 habilitado, punto final/contraseña correcta, servicio accesible y nombres de escena/entrada válidos. |
| La automatización de la interfaz de usuario falla | Host complementario incluido, selector válido único, destino habilitado accesible, control y permisos admitidos. Vuelva a intentar un tiempo de espera transitorio solo después de revisar el objetivo. |
| Panel móvil inalcanzable | LAN en lugar de dirección de bucle invertido, misma red, servidor en ejecución, perfiles aprobados, huella digital/confianza del certificado y permisos manuales de firewall. |
| El complemento falla | Integridad del paquete, confianza explícita/estado habilitado, dependencias, compatibilidad de protocolo y registro de ejecución. |
| La solicitud de IA falla | Corrija el tiempo de ejecución local seleccionado, servicio/modelo listo, esquema JSON compatible, carpeta de modelo compatible confiable y límites de solicitudes. |
| El diseño o el texto parecen incorrectos | Separe el idioma de la interfaz de usuario de la geometría del teclado; cambie el tamaño de la ventana, verifique la escala de visualización e informe la página/idioma/compilación exacta. |

Para un informe de error útil, incluya la versión de Windows, la compilación/el commit de la aplicación, el idioma, la escala de pantalla, el modelo de hardware, una secuencia mínima y el registro pertinente con los datos sensibles ocultos. Elimine rutas personales, contenidos de acciones y credenciales antes de publicarlo.

## Construcción, pruebas y embalaje.

Utilice la versión del SDK fijada en `global.json`. Desde la raíz del repositorio:

```powershell
dotnet build Aivana.slnx -c Release
dotnet run --project tests/Aivana.Tests -c Release
dotnet run --project tests/Aivana.Windows.Tests -c Release
dotnet run --project src/Aivana.Desktop -c Release -- --smoke-test
./tools/publish-complete.ps1 -SelfContained
python tools/check_documentation.py
```

Windows las pruebas de integración pueden abrir ventanas aisladas y configurar/liberar temporalmente enlaces de proveedores compatibles. Ejecútelos sin que otro controlador sea propietario de esos enlaces. Las sondas de brillo/audio predeterminadas son de sólo lectura; Las pruebas explícitas de ida y vuelta de hardware están separadas. Los protocolos simulados, las capturas de pantalla renderizadas y los gráficos de inferencia aleatoria no deben representarse como una aceptación real de la calidad del modelo o del hardware.

El script de publicación completa reúne los hosts auxiliares, los recursos móviles, el plugin de ejemplo, el entorno ONNX y los avisos de dependencias disponibles. Conserve sus subdirectorios. Una compilación correcta no constituye un instalador firmado ni una aprobación legal de publicación. El código fuente y las compilaciones pueden evolucionar por separado; compare la instantánea de la documentación y el commit con la versión instalada.

## Licencia, privacidad y estado de publicación

El modelo comercial previsto es una compra única con uso perpetuo de la versión principal adquirida; el precio final, el número de dispositivos permitidos y las condiciones de actualización deben definirse en las condiciones de venta reales. Este manual no acredita un sistema de compra/activación operativo en producción. No se requiere una cuenta en la nube para el control cotidiano.

No distribuya archivos binarios propietarios del SDK sin permiso comprobado. Conserve los avisos de licencia y de terceros para cada dependencia enviada, incluidos los paquetes .NET y ONNX. Los pesos de los modelos tienen sus propios términos. La auditoría de licencia fechada es anterior a algunas dependencias nuevas y no es una autorización para esas adiciones. El hecho de que el repositorio sea público no otorga por sí mismo una licencia general de código abierto.

La validación con hardware real de los fabricantes, el funcionamiento de teléfonos/tabletas en LAN, las transiciones entre monitores con DPI diferentes, el uso prolongado, la revisión lingüística completa, el soporte de un controlador para uso exclusivo del teclado y una calidad útil de planificación de IA siguen siendo tareas de validación independientes. Nunca deduzca una aprobación solo por la presencia de una función.
