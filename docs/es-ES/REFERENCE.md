<!-- generated-doc locale=es-ES source=README.md sha256=eb6f3fe7810ed3e10793e8938e908e151a84c4afbea2f22cba2656bfe4e8dfc3 -->

> Traducción asistida por máquina. Se comprueban la estructura, los ejemplos de código y los enlaces; esto no constituye una revisión por hablantes nativos ni una aprobación jurídica. Los informes históricos conservan sus fechas y alcance originales. [fuente original](../../README.md)

# MAKRIVO

[Documentación completa en todos los idiomas 27](README.md) · [Manual de usuario completo](HANDBOOK.md)

**por Aivana.** [Inventario actual de funciones y limitaciones de publicación](FEATURES.md) · [Extensión del informe](testing/2026-10-12-six-extensions.md) · [Prototipo de asistente de IA local](testing/2026-10-12-local-meeting-assistant.md)

**Inglés (EE.UU.)** | [Deutsch](../de-DE/REFERENCE.md) | [Français](../fr-FR/REFERENCE.md) | [Italiano](../it-IT/REFERENCE.md)

Un controlador de teclas de acceso rápido local Windows con mosaicos, perfiles, macros y detección de teclado estilo Stream Deck. La interfaz admite idiomas 27. No se requiere cuenta ni conexión a la nube para el uso diario.

[Documentación](README.md) · [Verificación](VERIFICATION.md) · [El informe de pruebas](testing/2026-10-09-usp-release.md)

## Empezando

Tras la publicación, la aplicación está en `artifacts/win-x64/MAKRIVO.exe`. Mantenga junta toda la carpeta de salida; puede copiarla a otra ubicación. Windows 11 x64 es la plataforma inicial. Los artefactos de compilación se excluyen de Git; una copia del repositorio fuente no incluye un ejecutable precompilado.

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

Los perfiles específicos de aplicación usan un nombre de proceso sin su ruta completa. El orden de los perfiles resuelve los empates; el predeterminado sirve como alternativa. La selección automática de perfiles debe activarse en Configuración.

Cerrar la ventana principal mantiene la aplicación en el área de notificación de forma predeterminada. Haga doble clic en su icono para volver a abrirla; use el menú del icono para salir. El inicio con Windows es opcional y está deshabilitado por defecto. Los atajos pueden pausarse de forma centralizada.

## Acciones y paleta de comandos

Agregar acción abre un catálogo de búsqueda con ajustes preestablecidos 90 en ocho categorías. Cada uno tiene una descripción y una vista previa. El editor de macros utiliza el mismo catálogo para agregar o reemplazar pasos. Al seleccionar un ajuste preestablecido en este flujo de trabajo del editor se crea un borrador sin ejecución.

La paleta de comandos se abre desde el encabezado o con Ctrl+K dentro de la aplicación. Busca acciones guardadas en todos los perfiles, ajustes preestablecidos del catálogo, cambios de perfil, navegación, pausa/reanudación, cancelación de macros e importación/exportación. Filtre por acciones guardadas, funciones del catálogo o comandos de la aplicación. Las flechas seleccionan, Intro activa, Ctrl+Enter edita una entrada editable y Escape cierra. Los favoritos aparecen primero. Los comandos del catálogo listos para usar pueden ejecutarse inmediatamente; los ajustes que requieren configuración abren un borrador. En Configuración se puede definir un atajo global para la paleta. Las nuevas configuraciones usan Ctrl+Shift+Space; las existentes no reciben automáticamente un nuevo atajo global.

Las acciones de audio ajustan con precisión el volumen de 0 a 100 por ciento y silencian, reactivan o alternan el silencio de los dispositivos de salida y micrófono. Elija un dispositivo concreto o deje la selección vacía para usar el predeterminado de Windows. Si el dispositivo guardado no está disponible, se genera un error en lugar de controlar otro. Actualizar dispositivos vuelve a cargar los dispositivos conectados.

OBS Studio usa WebSocket v5: iniciar/detener/alternar/pausar/reanudar la grabación; iniciar/detener/alternar el streaming; cambiar escenas; alternar el silencio de una entrada. Active el servidor WebSocket en Herramientas de OBS y defina su punto de conexión en el editor. Comprobar conexión y cargar nombres lee la versión de OBS, las escenas y las entradas sin iniciar una grabación ni una transmisión. Elija nombres de la lista o introdúzcalos manualmente. Las contraseñas pueden guardarse en Windows Credential Manager y se excluyen de las configuraciones exportadas. Las conexiones OBS no disponibles notifican un error.

Los perfiles pueden coincidir con un nombre de proceso y parte del título de una ventana. Ambas condiciones especificadas deben coincidir. Bloquear perfil impide el cambio automático de perfil durante la sesión. Capturar condiciones de la ventana en primer plano usa la última aplicación externa que tuvo el foco.

Las acciones de texto y portapapeles admiten `{{date}}`, `{{time}}`, `{{datetime}}`, `{{clipboard}}`, `{{profile}}`, `{{process}}` y `{{user}}`. Las fechas usan `yyyy-MM-dd`; las horas usan `HH:mm`. Los marcadores desconocidos no cambian. `{{profile}}` identifica el perfil de la acción iniciada; `{{process}}` identifica el contexto de la aplicación externa. El portapapeles se lee solo cuando se usa ese marcador. La acción de portapapeles escribe texto, pero no lo pega automáticamente.

Las acciones de ventana minimizan, maximizan, restauran u organizan una ventana en el lado izquierdo/derecho del área de trabajo de su monitor. La acción del sistema bloquea Windows. Estas acciones se ejecutan sólo cuando se invocan explícitamente.

La vista previa muestra pasos, repeticiones y variables sin ejecución. Deshacer y Rehacer conservan hasta 100 estados de borrador; los cuadros de texto mantienen la edición nativa Ctrl+Z. Marque favoritos en el editor o menú de mosaicos y filtrelos en el panel. Los recuentos de ejecución se aplican sólo a la sesión actual.

Las acciones de texto, accesos directos y ventanas iniciadas desde la interfaz activan la última ventana de destino externa. La paleta muestra ese contexto de destino. Si el objetivo se ha cerrado o Windows rechaza la activación, la ejecución se detiene con un mensaje.

## Asistencia y comentarios del teclado

El asistente de teclas especiales está en la página Teclado y en la paleta. Enumera las teclas G notificadas por iCUE. Detectar pulsación de tecla las captura temporalmente sin ejecutar las acciones asignadas. Las asignaciones normales se restauran tras la detección, la cancelación o el cierre. Una función del catálogo y una tecla seleccionada se abren como borrador sin guardar; se comprueban las asignaciones duplicadas.

La superposición del teclado muestra la distribución de referencia seleccionada y las asignaciones activas. Mantenga Ctrl+Alt+F1 o actívela/desactívela desde la paleta o la página Teclado. No toma el foco y permite que pasen los clics del ratón. Los cambios de perfil y asignaciones se actualizan mientras está visible; se resaltan las pulsaciones normales de Windows. Los controles específicos del fabricante pueden no proporcionar una señal de tecla Windows equivalente.

Los comentarios de ejecución muestran la espera, el progreso del paso/repetición, la finalización, la cancelación o el error sin centrarse ni exponer el texto ingresado. Comandos ejecutados confirma la ejecución del comando, no un estado en vivo consultado de forma independiente en una aplicación de terceros. Los detalles del error aparecen en el registro de actividad.

## Dispositivos y Corsair

Corsair G1–G6 se pueden asignar directamente mediante el SDK oficial de iCUE. Seleccione el dispositivo y la tecla en el editor de acciones y guarde, o prepare un borrador desde la vista del teclado K100. Las asignaciones se aplican al dispositivo seleccionado y al perfil activo; la pausa también las deshabilita. iCUE debe estar en ejecución y permitir el acceso. Fn, la rueda iCUE y los controles de perfil y bloqueo no se pueden asignar mediante esta API G-key.

La página Dispositivos enumera las interfaces de teclado Windows y se actualiza periódicamente y cuando los dispositivos se conectan. Un teclado físico puede exponer múltiples interfaces HID. Las teclas de acceso rápido globales estándar funcionan en todos los fabricantes.

Corsair La detección utiliza la información disponible del dispositivo. El control RGB requiere hardware compatible, en ejecución iCUE, y el funcionario iCUE SDK; la aplicación informa la disponibilidad real. Otros fabricantes reciben actualmente detección y acciones estándar. Afuera Corsair Integración de tecla G, los controles propietarios funcionan cuando Windows los recibe como entrada regular. La aplicación no instala un controlador de teclado.

## Idiomas

La configuración ofrece idiomas 27: alemán, inglés, francés, español, italiano, portugués, holandés, polaco, checo, eslovaco, húngaro, rumano, búlgaro, griego, danés, sueco, noruego, finlandés, estonio, letón, lituano, esloveno, croata, ucraniano, ruso, turco y serbio en escritura latina. Automático sigue el idioma de visualización Windows. Las traducciones se agrupan localmente. Los diseños, las teclas de acceso rápido y los nombres creados por el usuario son independientes del idioma de la interfaz.

La documentación completa del repositorio está disponible en todos los idiomas de aplicación 27 a través de la [El índice de idiomas](README.md). Las traducciones cuentan con asistencia automática, enlaces a las fuentes originales y comprobaciones estructurales; la revisión por hablantes nativos es independiente. La aplicación también acepta los códigos regionales `en-US`, `de-DE`, `fr-FR` e `it-IT` y usa sus formatos numéricos. Los códigos de idioma existentes siguen siendo compatibles.

## Diseños e investigación de fabricantes.

La vista del teclado ofrece referencias ISO-DE/ANSI-US completas y sin teclado numérico, además de distribuciones Corsair K100 RGB. La geometría representa posiciones, teclas anchas/altas y la tecla Intro ISO. Las distribuciones estándar son referencias genéricas. Las distribuciones K100 siguen los controles documentados sin afirmar dimensiones precisas al milímetro. Elija la variante correcta de idioma/geometría: un nombre de modelo o un VID/PID USB no determina una variante específica.

Las asignaciones resuelven los códigos de exploración físicos mediante la distribución de entrada Windows activa. El flujo prepara un borrador y conserva las comprobaciones de guardado y conflictos. Las asignaciones estándar son atajos globales Windows, no asignaciones aisladas a un solo teclado conectado. Corsair G1–G6 usan la integración iCUE específica del dispositivo. Los demás controles propietarios requieren un adaptador o atajos configurados por el fabricante. La tecla Intro principal y la del teclado numérico comparten una identidad de tecla virtual para los atajos globales.

El [Investigación de fabricantes](research/tastaturhersteller-sonderfunktionen.md) y [CSV](research/tastaturhersteller-sonderfunktionen.csv) abarcan 74 grupos de marcas/fabricantes y distinguen configuradores, SDK de entrada y API de iluminación. [Documentación de distribuciones](research/keyboard-layout-evidence.md) documenta las diferencias entre modelos, referencias SKU y módulos. La cobertura de investigación no significa que se haya implementado compatibilidad de hardware.

## Datos

La configuración se almacena en los datos de la aplicación local. Importar/exportar utilice JSON. Importar muestra una vista previa de la configuración, reemplaza la configuración actual después de la confirmación y deshabilita los enlaces importados hasta que se habilite manualmente. El contenido de acción de texto no se recopila como un registro de pulsaciones de teclas. Las contraseñas de Credential Manager están separadas de los perfiles exportados.

## Verificación

```powershell
dotnet run --project tests/Aivana.Tests -c Release
dotnet run --project tests/Aivana.Windows.Tests -c Release
```

Las comprobaciones Windows pueden inspeccionar el hardware y configurar/liberar temporalmente enlaces iCUE compatibles. Ejecútelos sin otro controlador usando esos enlaces. Las comprobaciones de los puntos finales de audio no cambian el volumen en vivo ni la configuración de silencio.

La entrada nativa no puede eludir los privilegios Windows. Es posible que un controlador iniciado normalmente no pueda enviar entradas a una aplicación ejecutada como administrador. Los atajos reservados u ocupados aparecen como conflictos.

El [El índice de documentación](README.md) enlaza diseños históricos, planes, investigaciones e informes de pruebas fechados. Los planes históricos describen su alcance registrado, no una garantía actual de compatibilidad de funciones.

### Plantillas de flujo de trabajo y esperas de ventana de destino

El catálogo incluye seis flujos de trabajo editables: espacio de trabajo, reunión, streaming, investigación, cambio de proyecto y fragmento de texto. Busque `workflow.` en el catálogo o en la paleta de comandos. Al seleccionar un flujo de trabajo se abre un borrador no guardado; nunca se ejecuta inmediatamente. Todos los pasos se conservan al agregar un flujo de trabajo a una macro existente o asignarlo a una tecla Corsair. Configure URL de ejemplo, rutas de programas, perfiles y escenas OBS antes de guardar.

El paso **Esperar a la ventana de destino** acepta un nombre de proceso (con o sin `.exe`), una subcadena del título sin distinguir mayúsculas y minúsculas y un tiempo de espera de 1–600000 ms. Se requiere al menos un filtro; ambos deben coincidir si se especifican. La macro continúa solo cuando una ventana visible coincidente responde y pasa a primer plano. Use un título específico si varias ventanas comparten un proceso. La cancelación o el vencimiento del plazo detiene los pasos posteriores. Una ventana lista no garantiza que se haya cargado un documento ni que esté conectado un servicio externo como OBS WebSocket.

Brillo de la pantalla: busque **Aumentar el brillo de la pantalla**, **Disminuir el brillo de la pantalla** o **Brillo de la pantalla en 50 por ciento** en el catálogo de acciones. Las acciones relativas cambian el brillo en 10 puntos porcentuales; los valores fijos varían de 0 a 100. Las pantallas internas compatibles utilizan WMI; Los monitores externos compatibles requieren DDC/CI habilitado en su menú. De forma predeterminada, la acción afecta a todas las pantallas compatibles; Se puede seleccionar un monitor explícito en el editor. Las conexiones y los muelles pueden restringir el soporte.

### Controles extendidos

Abra la paleta de comandos con **Ctrl+K cuando la ventana de la aplicación tenga el foco**. Busque **Diseñador de flujos de trabajo**, **Grabar macro**, **Sincronizar perfiles** o **Diagnóstico de teclas** (el texto depende del idioma seleccionado). El diseñador edita el borrador del mosaico seleccionado: condiciones de archivo/proceso/ventana, número de pasos que se saltan hacia delante y detención/continuación en caso de error. La grabadora captura combinaciones de teclas de atajo y esperas, no la escritura normal; Escape detiene la grabación. Revise el resultado antes de adoptarlo.

En un paso de brillo, seleccione todas las pantallas compatibles o un monitor específico. Un monitor seleccionado no disponible produce un error en lugar de cambiar silenciosamente otra pantalla. El tablero muestra lecturas de brillo, audio y OBS en vivo cuando sus respectivas integraciones están disponibles; una lectura no disponible no se presenta como un valor medido.

En el editor de mosaicos, asigne una tecla especial física compatible y elija pulsación inmediata, pulsación breve, pulsación mantenida o doble pulsación. Los selectores de capa mantenidos activan temporalmente otra asignación y restauran la capa anterior al soltarlos. Los gestos y las capas se aplican a las teclas G del SDK Corsair y a las teclas especiales estándar específicas del dispositivo, no a los atajos globales ordinarios. La captura de teclas estándar admite F13–F24 y teclas multimedia. Los controles propietarios Fn, perfil y rueda siguen requiriendo un adaptador compatible del fabricante o una asignación del fabricante a una tecla estándar. Raw Input observa la tecla y no suprime su comportamiento normal en Windows. El diagnóstico de teclas explica problemas de perfil, pausa, conexión y asignación.

La sincronización de perfiles intercambia archivos a través de una carpeta que usted elija, incluida una carpeta administrada por su cliente en la nube existente. Revise la vista previa antes de importar; las importaciones comienzan deshabilitadas y eliminan asignaciones específicas del hardware. Las revisiones evitan sobrescrituras silenciosas e incluir contenidos de acciones es una opción explícita. MAKRIVO no proporciona una cuenta en la nube ni conserva las credenciales de la nube. La bandeja ahora usa el ícono M verde azulado de MAKRIVO y muestra una insignia de pausa de color ámbar cuando está en pausa.
