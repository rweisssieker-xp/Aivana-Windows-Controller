<!-- generated-doc locale=es-ES source=README.md sha256=eb6f3fe7810ed3e10793e8938e908e151a84c4afbea2f22cba2656bfe4e8dfc3 -->

> Traducción asistida por máquina. Se comprueban la estructura, los ejemplos de código y los enlaces; esto no constituye una revisión por hablantes nativos ni una aprobación jurídica. Los informes históricos conservan sus fechas y alcance originales. [fuente original](../../README.md)

# MAKRIVO

[Documentación completa en todos los idiomas 27](README.md) · [Manual de usuario completo](HANDBOOK.md)

**por Aivana.** [El inventario actual y las limitaciones de liberación](FEATURES.md) · [Extensión del informe](testing/2026-10-12-six-extensions.md) · [El prototipo de asistente local](testing/2026-10-12-local-meeting-assistant.md)

**Inglés (EE.UU.)** | [Deutsch](../de-DE/REFERENCE.md) | [Français](../fr-FR/REFERENCE.md) | [Italiano](../it-IT/REFERENCE.md)

Un controlador de teclas de acceso rápido local Windows con mosaicos, perfiles, macros y detección de teclado estilo Stream Deck. La interfaz admite idiomas 27. No se requiere cuenta ni conexión a la nube para el uso diario.

[Documentación](README.md) · [Verificación](VERIFICATION.md) · [El informe de pruebas](testing/2026-10-09-usp-release.md)

## Empezando

Después de la publicación, la aplicación está en `artifacts/win-x64/MAKRIVO.exe`. Mantenga toda la carpeta de salida junta; puedes copiarlo en otro lugar. Windows 11 x64 es el objetivo inicial. Los artefactos de compilación están excluidos de Git; un pago de código fuente no incluye un ejecutable prediseñado.

Instale el SDK .NET 10 especificado en `global.json` para compilarlo y ejecutarlo desde el código fuente:

```powershell
dotnet build Aivana.slnx -c Release
dotnet run --project src/Aivana.Desktop -c Release
```

Publique una compilación independiente sin necesidad de instalar un entorno de ejecución .NET por separado:

```powershell
dotnet publish src/Aivana.Desktop -c Release -r win-x64 --self-contained true -o artifacts/win-x64
```

## Flujo de trabajo básico

1. Seleccione un perfil y cree un mosaico de acción.
2. Establezca su nombre, icono, color y método abreviado de teclado. Las combinaciones Ctrl/Alt/Shift reducen la activación accidental al escribir.
3. Agregue uno o más pasos: aplicaciones, archivos, sitios web, texto, accesos directos, medios, volumen, cambio de perfil o retrasos.
4. Guardar. Los cambios siguen siendo un borrador hasta que se guardan; descartar restaura la versión guardada. El botón Reproducir del mosaico y su tecla de acceso rápido utilizan la misma ruta de ejecución.
5. Reordene mosaicos y pasos de macro con arrastrar y soltar o botones de flecha. Las plantillas simplifican la configuración. Las macros pueden repetirse hasta 100 veces. Cancele a través de la interfaz, el menú de la bandeja del sistema o Ctrl+Alt+Escape de forma predeterminada.

Los perfiles específicos de la aplicación utilizan un nombre de proceso sin su ruta completa. El orden del perfil rompe lazos; el perfil predeterminado es el de reserva. La selección automática de perfil debe estar habilitada en Configuración.

Cerrar la ventana principal mantiene la aplicación en la bandeja del sistema de forma predeterminada. Haga doble clic en el icono de su bandeja para volver a abrirlo; utilice el menú de la bandeja para salir. El inicio con Windows es opcional y está deshabilitado de forma predeterminada. Las teclas de acceso rápido se pueden pausar de forma centralizada.

## Acciones y paleta de comandos

Agregar acción abre un catálogo de búsqueda con ajustes preestablecidos 90 en ocho categorías. Cada uno tiene una descripción y una vista previa. El editor de macros utiliza el mismo catálogo para agregar o reemplazar pasos. Al seleccionar un ajuste preestablecido en este flujo de trabajo del editor se crea un borrador sin ejecución.

La paleta de comandos se abre desde el encabezado o con Ctrl+K dentro de la aplicación. Busca acciones guardadas en perfiles, ajustes preestablecidos de catálogo, cambio de perfil, navegación, pausa/reanudación, cancelación de macro e importación/exportación. Filtre por acciones guardadas, funciones de catálogo o comandos de aplicaciones. Las teclas de flecha seleccionan, Intro activa, Ctrl+Intro edita una entrada editable y Escape cierra. Los favoritos aparecen primero. Los comandos del catálogo listos para usar se pueden ejecutar inmediatamente; Los ajustes preestablecidos que requieren configuración abren un borrador. La configuración puede definir un acceso directo a la paleta global. Las nuevas configuraciones usan Ctrl+Shift+Espacio; Las configuraciones existentes no reciben un nuevo acceso directo global automáticamente.

Las acciones de audio establecen el volumen con precisión desde 0 a 100 por ciento y silencian, reactivan o alternan los dispositivos de salida y micrófono. Elija un punto final específico o déjelo sin configurar para el valor predeterminado Windows. Un punto final guardado no disponible produce un error en lugar de controlar otro dispositivo. Actualizar dispositivos recarga los dispositivos conectados.

OBS Studio utiliza WebSocket v5: iniciar/detener/alternar/pausar/reanudar la grabación; iniciar/detener/alternar transmisión; cambiar de escena; alternar entrada silenciada. Habilite el servidor WebSocket en Herramientas en OBS y configure su punto final en el editor. Check Connection and Load Names lee la versión, las escenas y las entradas OBS sin iniciar una grabación o transmisión. Elija nombres de la lista o ingréselos manualmente. Las contraseñas se pueden guardar en Windows Credential Manager y se excluyen de las configuraciones exportadas. Las conexiones OBS no disponibles informan un error.

Los perfiles pueden coincidir con el nombre de un proceso y parte del título de una ventana. Ambas condiciones especificadas deben coincidir. Bloquear perfil evita el cambio automático de la sesión. Capturar condiciones desde primer plano utiliza la última aplicación externa enfocada.

Las acciones de texto y portapapeles admiten `{{date}}`, `{{time}}`, `{{datetime}}`, `{{clipboard}}`, `{{profile}}`, `{{process}}` y `{{user}}`. Las fechas utilizan `yyyy-MM-dd`; veces use `HH:mm`. Los marcadores de posición desconocidos permanecen sin cambios. `{{profile}}` identifica el perfil de la acción lanzada; `{{process}}` identifica el contexto de la aplicación externa. El contenido del portapapeles se lee solo cuando se utiliza ese marcador de posición. La acción del portapapeles escribe texto pero no lo pega automáticamente.

Las acciones de ventana minimizan, maximizan, restauran u organizan una ventana en el lado izquierdo/derecho del área de trabajo de su monitor. La acción del sistema bloquea Windows. Estas acciones se ejecutan sólo cuando se invocan explícitamente.

La vista previa muestra pasos, repeticiones y variables sin ejecución. Deshacer y Rehacer conservan hasta 100 estados de borrador; los cuadros de texto mantienen la edición nativa Ctrl+Z. Marque favoritos en el editor o menú de mosaicos y filtrelos en el panel. Los recuentos de ejecución se aplican sólo a la sesión actual.

Las acciones de texto, accesos directos y ventanas iniciadas desde la interfaz activan la última ventana de destino externa. La paleta muestra ese contexto de destino. Si el objetivo se ha cerrado o Windows rechaza la activación, la ejecución se detiene con un mensaje.

## Asistencia y comentarios del teclado

El Asistente de teclas especiales se encuentra en la página Teclado y en la paleta. Enumera las teclas G informadas por iCUE. Detectar pulsación de tecla los captura temporalmente sin ejecutar las acciones asignadas. Las vinculaciones normales regresan después de la detección, cancelación o cierre. Una función de catálogo y una clave seleccionada se abren como un borrador no guardado; Se comprueban los enlaces duplicados.

La superposición del teclado muestra el diseño de referencia seleccionado y los enlaces activos. Mantenga presionada la tecla Ctrl+Alt+F1 o alterne desde la paleta o la página Teclado. No toma foco y deja pasar los clics del mouse. Los cambios de perfil y vinculantes se actualizan mientras están visibles; Las pulsaciones de teclas Windows normales se resaltan. Es posible que los controles específicos del fabricante no proporcionen una señal de tecla Windows equivalente.

Los comentarios de ejecución muestran la espera, el progreso del paso/repetición, la finalización, la cancelación o el error sin centrarse ni exponer el texto ingresado. Comandos ejecutados confirma la ejecución del comando, no un estado en vivo consultado de forma independiente en una aplicación de terceros. Los detalles del error aparecen en el registro de actividad.

## Dispositivos y Corsair

Corsair G1–G6 se puede asignar directamente a través del SDK oficial iCUE. Seleccione el dispositivo y la clave en el editor de acciones y guárdelo o prepare un borrador desde la vista de teclado K100. Los enlaces se aplican al dispositivo seleccionado y al perfil activo; la pausa también los desactiva. iCUE debe estar ejecutándose y permitir el acceso. Fn, los controles de rueda, perfil y bloqueo iCUE no se pueden asignar a través de esta API de clave G.

La página Dispositivos enumera las interfaces de teclado Windows y se actualiza periódicamente y cuando los dispositivos se conectan. Un teclado físico puede exponer múltiples interfaces HID. Las teclas de acceso rápido globales estándar funcionan en todos los fabricantes.

Corsair La detección utiliza la información disponible del dispositivo. El control RGB requiere hardware compatible, en ejecución iCUE, y el funcionario iCUE SDK; la aplicación informa la disponibilidad real. Otros fabricantes reciben actualmente detección y acciones estándar. Afuera Corsair Integración de tecla G, los controles propietarios funcionan cuando Windows los recibe como entrada regular. La aplicación no instala un controlador de teclado.

## Idiomas

La configuración ofrece idiomas 27: alemán, inglés, francés, español, italiano, portugués, holandés, polaco, checo, eslovaco, húngaro, rumano, búlgaro, griego, danés, sueco, noruego, finlandés, estonio, letón, lituano, esloveno, croata, ucraniano, ruso, turco y serbio en escritura latina. Automático sigue el idioma de visualización Windows. Las traducciones se agrupan localmente. Los diseños, las teclas de acceso rápido y los nombres creados por el usuario son independientes del idioma de la interfaz.

La documentación completa del repositorio está disponible en todos los idiomas de aplicación 27 a través de la [El índice de idiomas](README.md). Las traducciones son asistidas por máquina, con enlaces de fuente original y verificaciones estructurales; la revisión del orador nativo es separada. `en-US`, `de-DE`, `fr-FR` y `it-IT` y utiliza sus formatos de número. los códigos lingüísticos existentes permanecen compatibles.

## Diseños e investigación de fabricantes.

La vista del teclado proporciona referencias ISO-DE/ANSI-US de tamaño completo y sin teclas, además de diseños RGB Corsair K100. La geometría representa posiciones, teclas anchas/altas e ISO Enter. Los diseños estándar son referencias genéricas. Los diseños K100 siguen controles documentados sin pretender dimensiones con precisión milimétrica. Elija la variante de idioma/geometría correcta: un nombre de modelo o USB VID/PID no establece una variante específica.

El mapeo resuelve códigos de escaneo físico usando el diseño de entrada activo Windows. El flujo de trabajo prepara un borrador y conserva las comprobaciones de guardado/conflicto. Los enlaces estándar son teclas de acceso rápido globales Windows, no aisladas de un teclado conectado. Corsair G1–G6 utiliza la integración iCUE específica del dispositivo. Otros controles propietarios requieren un adaptador o accesos directos configurados por el fabricante. La entrada principal y la entrada del teclado numérico comparten una identidad de clave virtual para las teclas de acceso rápido globales.

El [Investigación de fabricantes](research/tastaturhersteller-sonderfunktionen.md) y [El CSV](research/tastaturhersteller-sonderfunktionen.csv) cubrir 74 grupos de marca/productor, distinguir los configuradores, SDK de entrada y APIs de iluminación. [Layout de evidencia](research/keyboard-layout-evidence.md) Modelo de registro, SKU y diferencias de módulo. La cobertura de la investigación no significa que se realice el soporte de hardware.

## Datos

La configuración se almacena en los datos de la aplicación local. Importar/exportar utilice JSON. Importar muestra una vista previa de la configuración, reemplaza la configuración actual después de la confirmación y deshabilita los enlaces importados hasta que se habilite manualmente. El contenido de acción de texto no se recopila como un registro de pulsaciones de teclas. Las contraseñas de Credential Manager están separadas de los perfiles exportados.

## Verificación

```powershell
dotnet run --project tests/Aivana.Tests -c Release
dotnet run --project tests/Aivana.Windows.Tests -c Release
```

Las comprobaciones Windows pueden inspeccionar el hardware y configurar/liberar temporalmente enlaces iCUE compatibles. Ejecútelos sin otro controlador usando esos enlaces. Las comprobaciones de los puntos finales de audio no cambian el volumen en vivo ni la configuración de silencio.

La entrada nativa no puede eludir los privilegios Windows. Es posible que un controlador iniciado normalmente no envíe información a una aplicación de administrador. Las teclas de acceso rápido reservadas u ocupadas aparecen como conflictos.

El [El índice de documentación](README.md) Enlaces de diseños históricos, planes, investigaciones y informes de pruebas datadas. Los planes históricos describen su alcance registrado, no una garantía actual de soporte.

### Plantillas de flujo de trabajo y esperas de ventana de destino

El catálogo incluye seis flujos de trabajo editables: espacio de trabajo, reunión, streaming, investigación, cambio de proyecto y fragmento de texto. Busque `workflow.` en el catálogo o en la paleta de comandos. Al seleccionar un flujo de trabajo se abre un borrador no guardado; nunca se ejecuta inmediatamente. Todos los pasos se conservan al agregar un flujo de trabajo a una macro existente o asignarlo a una tecla Corsair. Configure URL de ejemplo, rutas de programas, perfiles y escenas OBS antes de guardar.

El paso **Esperar ventana de destino** acepta un nombre de proceso (con o sin `.exe`), una subcadena de título que no distingue entre mayúsculas y minúsculas y un tiempo de espera de 1–600,000 ms. Se requiere al menos un filtro; Ambos filtros deben coincidir cuando se suministran. La macro continúa solo después de que una ventana visible coincidente responde y llega al primer plano. Utilice un título específico cuando varias ventanas compartan un proceso. La cancelación o el tiempo de espera detiene los pasos siguientes. La disponibilidad de Windows no garantiza que se haya cargado un documento o que un servicio externo como OBS WebSocket esté conectado.

Brillo de la pantalla: busque **Aumentar el brillo de la pantalla**, **Disminuir el brillo de la pantalla** o **Brillo de la pantalla en 50 por ciento** en el catálogo de acciones. Las acciones relativas cambian el brillo en 10 puntos porcentuales; los valores fijos varían de 0 a 100. Las pantallas internas compatibles utilizan WMI; Los monitores externos compatibles requieren DDC/CI habilitado en su menú. De forma predeterminada, la acción afecta a todas las pantallas compatibles; Se puede seleccionar un monitor explícito en el editor. Las conexiones y los muelles pueden restringir el soporte.

### Controles extendidos

Abra la paleta de comandos con **Ctrl+K mientras la ventana de la aplicación tiene focus**. Busque **Diseñador de flujo de trabajo**, **Grabar macro**, **Sincronizar perfiles** o **Diagnósticos clave** (el texto sigue el idioma seleccionado). El diseñador del flujo de trabajo edita el borrador del mosaico seleccionado: condiciones de archivo/proceso/ventana, recuentos de saltos directos y manejo de errores para detener/continuar. La grabadora captura acordes de acceso directo y espera, no una escritura normal; Escape detiene la grabación. Revisar el resultado antes de adoptarlo.

En un paso de brillo, seleccione todas las pantallas compatibles o un monitor específico. Un monitor seleccionado no disponible produce un error en lugar de cambiar silenciosamente otra pantalla. El tablero muestra lecturas de brillo, audio y OBS en vivo cuando sus respectivas integraciones están disponibles; una lectura no disponible no se presenta como un valor medido.

En el editor de mosaicos, asigne una tecla especial física compatible y elija presionar, tocar, mantener presionado o tocar dos veces inmediatamente. Los selectores de capas retenidos activan temporalmente otra asignación y restauran la capa anterior al liberarse. Los gestos y las capas se aplican a las teclas G del SDK Corsair y a las teclas especiales estándar específicas del dispositivo, no a las teclas de acceso rápido globales ordinarias. La captura de teclas estándar admite F13–F24 y teclas multimedia del teclado. Los controles de marcación, perfil y Fn patentados aún requieren un adaptador de proveedor compatible o una asignación de proveedor a una tecla estándar. Raw Input observa la clave y no suprime su comportamiento normal Windows. Los diagnósticos clave explican los problemas de perfil, pausa, conexión y vinculación.

La sincronización de perfiles intercambia archivos a través de una carpeta que usted elija, incluida una carpeta administrada por su cliente en la nube existente. Revise la vista previa antes de importar; las importaciones comienzan deshabilitadas y eliminan asignaciones específicas del hardware. Las revisiones evitan sobrescrituras silenciosas e incluir contenidos de acciones es una opción explícita. MAKRIVO no proporciona una cuenta en la nube ni conserva las credenciales de la nube. La bandeja ahora usa el ícono M verde azulado de MAKRIVO y muestra una insignia de pausa de color ámbar cuando está en pausa.
