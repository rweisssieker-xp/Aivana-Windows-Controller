<!-- generated-doc locale=es-ES source=docs/superpowers/plans/2026-10-12-six-extensions.md sha256=0ac83f2f795ba08551082caee7c775af015828dd39e200dd9857aab504cdba01 -->

> Traducción asistida por máquina. Se comprueban la estructura, los ejemplos de código y los enlaces; esto no constituye una revisión por hablantes nativos ni una aprobación jurídica. Los informes históricos conservan sus fechas y alcance originales. [fuente original](../../../superpowers/plans/2026-10-12-six-extensions.md)

# MAKRIVO Plan de implementación de extensiones

> **Para trabajadores agentes:** SUBHABILIDAD REQUERIDA: use superpowers:executing-plans para ejecución nativa o superpowers:subagent-driven-development después de que el usuario seleccione la delegación. Realice un seguimiento de los pasos con casillas de verificación.

**Objetivo:** Ofrecer seis extensiones integradas y compatibilidad con teclados de varios fabricantes basados en capacidades.

**Arquitectura:** Preservar el núcleo / Windows / WPF separación. Introducir procesos aislados de complementos y servicios móviles con IPC autenticado restringido; toda la ejecución pasa por el controlador existente y la cola MacroRunner. Los adaptadores de hardware anuncian capacidades detectadas en lugar de soporte inferido para toda la marca.

**Tech Stack:** C#, .NET 10, WPF, Windows UI Automation, Win32 Raw Input, SDK de proveedores documentados, ASP.NET Core HTTPS/WebSocket, System.Text.Json.

**Spec:** `docs/superpowers/specs/2026-10-12-six-extensions-design.md` (aprobado por el usuario).

## Restricciones globales

- La configuración existente debe cargarse sin cambios; agregue tipos de acciones en lugar de reordenar sus valores de enumeración persistentes.
- Conserve los perfiles existentes, las asignaciones G1/G2, la operación local y el uso sin cuenta.
- Raw Input proporciona únicamente observación. La supresión exclusiva por dispositivo requiere un backend de filtro de teclado real.
- Los complementos ejecutables importados no deben ejecutarse automáticamente.
- De forma predeterminada, está deshabilitado y restringido las interfaces configuradas para el servicio móvil; sin respaldo de autenticación de texto plano.
- La entrega de acceso directo/texto no es prueba de que la aplicación de destino aceptó el comando.
- No reemplace la aplicación instalada, instale controladores, altere asignaciones de teclado reales ni abra el acceso a la red como prueba incidental.
- Conservar los cambios no confirmados y no relacionados. No confirme ediciones compartidas existentes como parte de esta función. Organice únicamente fragmentos de funciones revisables de forma independiente si se solicitan confirmaciones.

## Enfoque de revisión

1. Modelos de teclado idénticos duplicados: la reconexión ambigua debe requerir una nueva selección (Tareas 10–12).
2. Cancelación durante esperas de UIA/proveedor/complemento/red: no hay acción diferida después de la cancelación (Tareas 1, 4, 6, 8).
3. Ediciones del usuario mientras el asistente o la vista previa del paquete está abierto: la discrepancia de revisión debe evitar la sobrescritura silenciosa (Tareas 3, 7).
4. El idioma y el diseño físico difieren: los selectores y los enlaces de códigos de escaneo no deben depender de los nombres para mostrar localizados (tareas 4, 10).
5. Reintento de red y reinicio del complemento: los comandos no deben ejecutarse silenciosamente dos veces (Tareas 6, 8).

## Contratos compartidos y orden de dependencia.

Las tareas 1–3 establecen la ejecución y la configuración. Tareas 4–5 construir UIA. Tareas 6–7 entregan complementos. Las tareas 8–9 brindan acceso móvil. Las tareas 10–12 entregan adaptadores de teclado e integración de filtros. La tarea 13 valida el producto completo.

Cada tarea sigue un ciclo de prueba fallida → implementación mínima → prueba aprobada. Una tarea se completa solo cuando se puede acceder a ella a través de la aplicación y se superan sus comprobaciones. Los andamios, los adaptadores simulados y la documentación por sí solos no satisfacen un entregable.

### Tarea 1: Verificación escrita y persistencia compatible

**Archivos:** Crear `src/Aivana.Core/ActionVerification.cs`, `tests/Aivana.Tests/VerificationChecks.cs`; modificar `src/Aivana.Core/Models.cs`, `src/Aivana.Core/MacroRunner.cs`, `src/Aivana.Core/ConfigurationValidator.cs`, `tests/Aivana.Tests/Program.cs`.

**Interfaces:** Produce `VerificationState { Requested, Completed, Verified, Failed, TimedOut, Cancelled, Unverifiable }`, `ActionVerificationResult(VerificationState State, string Code, string Message)`, `IActionVerificationProvider.PreflightAsync(ActionStep, CancellationToken)` y `VerifyAsync(ActionStep, CancellationToken)`, ambos devuelven `Task<ActionVerificationResult>`. Agregue `VerificationOptions` serializable opcional con indicador habilitado y tiempo de espera (100–60000 ms), deshabilitado de forma predeterminada. Publique resultados escritos por paso, incluido el ID de ejecución, el ID de mosaico y el índice de pasos sin cargas útiles de acción.

- [] Comprobaciones de escritura: JSON heredado conserva los valores numéricos de acción existentes; las opciones ausentes desactivan la verificación; el tiempo de espera no válido falla la validación; la falla de verificación previa impide la invocación del ejecutor; la poscondición fallida nunca produce un éxito verificado; el ejecutor no admitido no es verificable; la cancelación detiene la verificación y los pasos posteriores.
- [] Ejecute `dotnet run --project tests/Aivana.Tests -c Release`; Confirme que las nuevas comprobaciones fallan antes de su implementación.
- [] Implementar contratos, validación, clonación/conservación de instantáneas e integración de MacroRunner. Respetar las políticas de parada/continuación OnError; preservar los suscriptores de comentarios de ejecución existentes.
- [] Ejecutar pruebas básicas; Requerir que todos los controles pasen.

### Tarea 2: Windows proveedores de verificación y interfaz de usuario de resultados

**Archivos:** Crear `src/Aivana.Windows/WindowsActionVerification.cs`, `src/Aivana.Desktop/MainWindow.Verification.cs`, `tests/Aivana.Windows.Tests/VerificationChecks.cs`; modificar `src/Aivana.Windows/WinActionExecutor.cs`, `src/Aivana.Desktop/DesktopController.Feedback.cs`, `src/Aivana.Desktop/MainWindow.Editor.cs`, puntos de entrada de prueba.

**Interfaces:** WindowsActionVerification implementa el proveedor de la tarea 1 utilizando clientes existentes de audio, visualización, proceso/ventana y OBS. Disponibilidad e identificadores de pruebas previas al vuelo; la verificación compara el estado deseado después de un sondeo limitado. Produzca instantáneas de resultados del controlador consumidas por la vista del historial WPF.

- [] Agregue pruebas fallidas para ID de audio/visualización explícitos no disponibles, tiempo de espera OBS, estado observado no coincidente, efectos de acceso directo desconocidos, omisión de carga útil sensible y sondeo cancelado, utilizando proveedores de lectura inyectables.
- [] Implemente una verificación real con respaldo de lectura para volumen absoluto, silencio, brillo absoluto, preparación de proceso/ventana y comandos OBS admitidos. Las acciones relativas/de alternancia requieren un estado previo capturado; de lo contrario informe No verificable.
- [] Agregue controles de verificación por paso y un historial de ejecución segura que muestre el estado, los requisitos fallidos y el texto de recuperación.
- [] Ejecute comprobaciones core/Windows y humo WPF con proveedores de estado inyectados; no hay brillo real ni escrituras de audio.

### Tarea 3: Creación guiada de espacio de trabajo

**Archivos:** Crear `src/Aivana.Core/WorkspaceSetup.cs`, `src/Aivana.Windows/WorkspaceDiscovery.cs`, `src/Aivana.Desktop/MainWindow.WorkspaceWizard.cs`, `tests/Aivana.Tests/WorkspaceSetupChecks.cs`; modificar los puntos de entrada de la paleta y el tablero.

**Interfaces:** `WorkspaceSetup.CreateProfile(WorkspaceSelection selection) -> Profile`; La selección contiene ID de plantilla, rutas ejecutables elegidas, URL, ID de audio/pantalla, escena OBS y enlaces seleccionados. Discovery devuelve candidatos nombrados con identificaciones estables y disponibilidad. Revisión de configuración de borradores de registros para detección de conflictos.

- [] Agregue pruebas fallidas para las cinco familias de asistentes solicitadas, pasos generados independientes, objetivos faltantes requeridos, cancele sin mutación y guarde contra revisión modificada.
- [] Descubra aplicaciones a través de rutas de aplicaciones registradas y accesos directos del menú Inicio; dispositivos a través de proveedores existentes; escenas mediante consulta OBS de solo lectura. Mantenga la selección manual disponible para aplicaciones no descubiertas.
- [] Implementar Elegir flujo de trabajo → Seleccionar objetivos → Elegir controles → Vista previa/validar → Guardar. Genere perfiles editables y nunca los ejecute durante la configuración.
- [] Agregue comprobaciones de humo del asistente WPF reales para completar/cancelar/conflicto; ejecutar todas las pruebas.

### Tarea 4: selectores UIA y acciones limitadas

**Archivos:** Crear `src/Aivana.Core/UiAutomationAction.cs`, `src/Aivana.Windows/UiAutomationService.cs`, `tests/Aivana.Windows.Tests/UiAutomationChecks.cs`; modificar modelos, validación de configuración y WinActionExecutor.

**Interfaces:** Anexar `ActionKind.UiAutomation`; `UiElementSelector` almacena el proceso, el ID de automatización, el tipo de control y los criterios de ancestros ordenados. `UiAutomationService.ExecuteAsync(UiAutomationAction action, CancellationToken)` devuelve `Task`; operaciones Invoke, SetValue, Select, Toggle, Focus y WaitState. La resolución del selector debe devolver exactamente un elemento elegible.

- [] Agregue pruebas fallidas en una ventana de prueba dedicada para reubicación/cambio de tamaño, coincidencias duplicadas, elementos faltantes, patrones no admitidos, controles deshabilitados, valores de contraseña, etiquetas Unicode y tiempo de espera/cancelación.
- [] Implemente UIA en un trabajador dedicado con solicitudes limitadas y segmentación de procesos. Los proveedores potencialmente suspendidos se ejecutan detrás de un límite de trabajadores reiniciable; abandonar una solicitud no debe dejar efectos secundarios en cola.
- [] Excluir valores de contraseña; informar restricciones de acceso/integridad en lugar de eludirlas.
- [] Ejecute comprobaciones Windows utilizando dispositivos aislados; agregue solo valores de enumeración y vuelva a ejecutar comprobaciones de persistencia heredadas.

### Tarea 5: selector de UIA y grabación revisada

**Archivos:** Crear `src/Aivana.Desktop/MainWindow.UiAutomation.cs`, `src/Aivana.Windows/UiAutomationRecorder.cs`; modificar el catálogo de acciones, el editor y la integración del registrador; crear controles de humo UIA WPF.

**Interfaces:** El selector devuelve un `UiElementSelector`; El registrador devuelve los pasos UIA propuestos y nunca escribe directamente en un perfil guardado. El comando del selector de prueba devuelve el recuento de coincidencias sin invocación.

- [] Agregar comprobaciones fallidas: el selector excluye MAKRIVO y la captura de valor de contraseña; grabar/cancelar deja el perfil existente sin cambios; recuento de coincidencias de informes de vista previa ambiguos; aceptar los pasos registrados crea un borrador editable.
- [] Implemente resaltado/selector de elementos, parámetros específicos de la operación, vista previa del selector y ventana de revisión del registrador. Conservar el comportamiento existente de la grabadora de accesos directos.
- [] Agregar entradas de catálogo y acceso a paletas; valide los errores del editor antes de guardar.
- [] Ejecute comprobaciones de humo Windows y WPF en el dispositivo; inspeccionar capturas de pantalla del selector/editor.

### Tarea 6: protocolo y host de complemento aislado

**Archivos:** Crear `src/Aivana.Core/Plugins/PluginContracts.cs`, `src/Aivana.Windows/Plugins/PluginProcessClient.cs`, `src/Aivana.PluginHost/Aivana.PluginHost.csproj`, `src/Aivana.PluginHost/Program.cs`, `tests/Aivana.Tests/PluginProtocolChecks.cs`; modificar solución y envío de acciones.

**Interfaces:** Version-1 El manifiesto especifica el ID/versión del paquete, el punto de entrada, los esquemas de parámetros, las acciones, los proveedores de estado y las declaraciones de capacidad. Los mensajes IPC contienen la versión del protocolo, el ID de solicitud único, el método, los parámetros JSON y el resultado escrito. `PluginProcessClient.InvokeAsync(string actionId, JsonElement parameters, CancellationToken)` devuelve un resultado escrito. Agregue `ActionKind.Plugin`.

- [] Agregue pruebas fallidas para protocolo incompatible, mensaje con formato incorrecto/sobredimensionado (>1 MiB), ID de solicitud duplicada, cancelación, falla, proceso que no responde y acción faltante.
- [] Implemente IPC de canalización con nombre del mismo usuario con un protocolo de enlace de 10 de segundos y un tiempo de espera de ejecución configurable limitado a 60 segundos. Despedir al trabajador que no responde; no reproduzca automáticamente una acción en vuelo después del reinicio.
- [] Aplicar esquemas y servicios de controlador permitidos. Explicar la confianza del complemento ejecutable; El aislamiento de procesos no es un entorno limitado del sistema operativo.
- [] Ejecute pruebas principales y pruebas de integración contra procesos de complementos de prueba reales.

### Tarea 7: Paquetes de complementos y ejemplo de integración

**Archivos:** Crear `src/Aivana.Core/Plugins/PluginPackage.cs`, `src/Aivana.Desktop/MainWindow.Plugins.cs`, `samples/Aivana.ExamplePlugin/`, pruebas de paquete; modificar las entradas de paleta/configuración.

**Interfaces:** El servicio de paquete expone `InspectAsync(path)`, `InstallAsync(reviewedPackage, revision)`, `ExportAsync(packageId, destination)`, `DisableAsync(packageId)` y `UninstallAsync(packageId)`. Registros de paquetes revisados: hashes, dependencias, versión y capacidades declaradas.

- [] Agregue pruebas fallidas para recorrido zip, rutas absolutas, enlace simbólico/escape de análisis, entradas duplicadas, >100 datos expandidos MiB, falta de coincidencia de hash, degradación, revisión obsoleta e importaciones deshabilitadas.
- [] Implementar vista previa y habilitación explícita, errores de dependencia, instalación por etapas y reversión de actualización; descargar al trabajador antes de retirarlo.
- [] Entregar un complemento de ejemplo real que devuelva una acción de cuenta regresiva configurable y un estado en vivo a través de IPC, incluida la cancelación; paquete/exportar/instalar/desinstalar a través de la interfaz de usuario.
- [] Ejecute pruebas de paquete/host y comprobaciones de extremo a extremo WPF. Verifique que no se ejecute ningún ejecutable importado antes de la habilitación.

### Tarea 8: servicio móvil HTTPS y emparejamiento

**Files:** Crear `src/Aivana.MobileHost/` Proyecto principal ASP.NET, `src/Aivana.Core/MobilePanelContracts.cs`, `src/Aivana.Windows/MobilePanelClient.cs`, pruebas de integración; modificar la solución y el controlador.

**Interfaces:** El servicio móvil se comunica con el controlador a través del IPC del mismo usuario. Rutas: emparejamiento, perfiles/acciones permitidas, invocación de acción autenticada, cancelación y estado WebSocket. La invocación incluye una ID de comando única; El controlador posee deduplicación limitada. Las credenciales son revocables y se almacenan mediante la protección Windows.

- [] Agregue pruebas fallidas para enumeración/ejecución no emparejada, código caducado, código reutilizado, token revocado, origen no válido, comando duplicado, marco de gran tamaño y límites de inundación.
- [] Implementar escucha HTTPS únicamente, interfaces seleccionadas por el usuario, aprovisionamiento de certificados locales/instrucciones de confianza, códigos de emparejamiento de dígitos 6 únicos que caducan después de 120 segundos, máximo cinco intentos por código y revocación de token. Almacenar tokens aleatorios con hash; nunca registre credenciales.
- [] Implementar ID de acciones estables incluidas en la lista permitida y enrutamiento de colas existentes; rechace shells, rutas y comandos de instalación de paquetes arbitrarios. Configure límites de velocidad explícitos por cliente (10 comandos/seg, ráfaga 30) y límite de mensajes KiB 64.
- [] Verificar las pruebas de integración de loopback sin agregar reglas de firewall ni habilitar el acceso LAN; prueba de apagado y persistencia del token.

### Tarea 9: Controles de servicio de escritorio y panel móvil

**Archivos:** Cree el panel responsivo `src/Aivana.MobileHost/wwwroot/` y `src/Aivana.Desktop/MainWindow.MobilePanel.cs`; agregue scripts de verificación de navegador/móvil y cobertura de humo WPF.

**Interfaces:** Consumir tarea 8 solo rutas. Exponga la cuadrícula de acciones táctiles, el selector de perfiles, los valores/progreso en vivo, la cancelación, el estado de la conexión y el emparejamiento. El escritorio controla el inicio/detención del servicio, instrucciones de dirección/certificado, perfiles permitidos y revocación de clientes emparejados.

- [] Agregue comprobaciones de integración fallidas para el flujo de autenticación, ID de acción obsoletas/faltantes, reconexión sin repetición de acción, cliente revocado y parada del servicio.
- [] Implemente una interfaz de usuario receptiva accesible utilizando recursos locales, sin scripts de terceros ni dependencias de Internet. Los tokens nunca aparecen en las URL.
- [] Pruebe el diseño del navegador en 360/768/1280 CSS px; inspeccionar capturas de pantalla reales. Verifique con un teléfono/tableta real solo después de que el usuario habilite explícitamente el servicio LAN.
- [] Documente e informe la verificación del hardware móvil por separado de la emulación del navegador.

### Tarea 10: Capacidades del dispositivo y observación de clave ordinaria

**Archivos:** Crear `src/Aivana.Core/KeyboardCapabilities.cs`, `src/Aivana.Windows/KeyboardAdapterRegistry.cs`, `src/Aivana.Desktop/MainWindow.KeyboardAdapters.cs`; modificar RawKeyboardBindingService, KeyboardBinding, validador y diagnóstico de dispositivos; agregar pruebas.

**Interfaces:** `IKeyboardAdapter` expone instantáneas de capacidad, identidad estable del dispositivo y bordes de clave física (código de escaneo, indicadores extendidos, prensa/liberación). Los modos distinguen observación, entrada estándar mapeada, SDK nativo y exclusivo del controlador. Los enlaces especiales de clave virtual existentes conservan su representación y comportamiento de migración.

- [] Agregue pruebas fallidas para enlaces antiguos, ingrese distinciones, independencia de idioma/diseño, caídas repetidas, desconexión mientras se mantiene presionado, ambigüedad de modelo duplicado y rechazo de entrada inyectada.
- [] Amplíe la observación a claves físicas ordinarias sin suprimirlas. Agregue captura explícita y vuelva a conectar el manejo de identidad.
- [] Muestra las capacidades reales del adaptador y el estado del hardware no probado en la interfaz de usuario del dispositivo; nunca infiera la compatibilidad con Fn/marcación a partir de nombres de marcas.
- [] Ejecute comprobaciones core/Windows/WPF con informes simulados y enumeración de dispositivos de solo lectura.

### Tarea 11: Adaptadores del fabricante

**Files:** Cree archivos separados en `src/Aivana.Windows/KeyboardAdapters/` para LogitechG, RazerChroma, SteelSeriesGameSense, WootingAnalog y la guía genérica QmkVia; modifique el registro/UI y agregue comprobaciones del protocolo del adaptador.

**Interfaces:** Consumir tarea 10 contratos de adaptador. Las bibliotecas nativas se cargan solo desde ubicaciones validadas de aplicaciones/proveedores; La ausencia de SDK es una capacidad no disponible. Las capacidades de iluminación y entrada están separadas.

- [] Verifique los encabezados oficiales actuales del SDK, los términos de redistribución, las dependencias de software y los modelos compatibles antes de implementar cada adaptador; fuentes de registro y versiones exactas.
- [] Agregue pruebas fallidas para SDK ausente/arquitectura incorrecta, evento de prensa/liberación/repetición, desconexión, respuesta mal formada y dispositivo no compatible. Utilice abstracciones de llamadas nativas inyectables para comprobaciones deterministas.
- [] Implemente la entrada de tecla G Logitech y la iluminación admitida por separado; Razer Iluminación cromática más asistente de asignación de teclas estándar Synapse; SteelSeries GameSense estado/iluminación; Wooting sondeo analógico con umbrales/histéresis configurables; QMK/VIA guía de configuración de clave estándar. No reclame aportes de propiedad de las API de iluminación.
- [] Validar dispositivos reales compatibles cuando estén disponibles. Marcar otros implementados/hardware no probados, con evidencia de modelo/firmware; El SDK utilizable que falta es una puerta externa revelada.

### Tarea 12: backend de filtro de teclado exclusivo

**Files:** Cree `src/Aivana.Windows/KeyboardFilter/`, fuente del instalador en `tools/keyboard-filter/`, pruebas de integración de backend y documentación de implementación. Se requiere un proyecto de controlador firmado dedicado si ningún backend con licencia adecuado pasa la evaluación.

**Interfaces:** Informes de backend instalados/compatibles/estado firmado; expone el inicio/detención de captura, los bordes de las teclas y la liberación de emergencia específicos del dispositivo. Un mecanismo de vigilancia independiente restaura el paso en caso de pérdida del controlador. El teclado principal nunca pasa a ser exclusivo por defecto.

- [] Evalúe la licencia de controlador del candidato, la procedencia del binario firmado y la compatibilidad actual de arranque Windows 11/Secure. Registre una decisión de ir/no ir. No hay instalación de controladores durante las pruebas ordinarias.
- [] Escribir pruebas de contrato para selección de objetivo de filtro, apertura fallida, expiración de latido, pérdida de IPC, ambigüedad de reconexión, pausa/cancelación, liberación y eliminación de clave retenida.
- [] Implementar backend, UI de selección/activación explícita, liberación de emergencia e instalador/desinstalador. No sustituya la heurística de sincronización de gancho de bajo nivel/Raw Input.
- [ ] Validar en una máquina de prueba recuperable con dos teclados: la tecla seleccionada se suprime solo en el dispositivo seleccionado; la escritura principal no se ve afectada; crash/suspender/desinstalar restaura la entrada.
- [] Finalización de la producción de puertas en pruebas físicas y de backend firmadas/con licencia reales. Si la firma/licencia externa no está disponible, mantenga la tarea incompleta con evidencia de dependencia precisa.

### Tarea 13: Paquete integrado de verificación, traducción y liberación

**Archivos:** Agregar `docs/testing/2026-10-12-six-extensions.md`; actualizar la documentación pertinente DE/EN/FR/IT, catálogos de localización y puntos de entrada de control de humo. Conservar otras ediciones de la documentación.

- [] Integre todos los puntos de entrada e instantáneas; completar las traducciones utilizando el flujo de trabajo del catálogo de idiomas 27 existente y revelar las traducciones no revisadas.
- [] Ejecute `dotnet build Aivana.slnx -c Release`, `dotnet run --project tests/Aivana.Tests -c Release`, `dotnet run --project tests/Aivana.Windows.Tests -c Release` y `dotnet run --project src/Aivana.Desktop -c Release -- --smoke-test`.
- [] Inspeccione las capturas de pantalla de escritorio/móvil y verifique el porcentaje de escalado 100/150/200 Windows, los viajes de ida y vuelta de la configuración, el ciclo de vida del complemento, la revocación de la red y la reconexión del teclado real.
- [] Publicar en un nuevo directorio de artefactos con `dotnet publish src/Aivana.Desktop -c Release -r win-x64 --self-contained true -o artifacts/six-extensions/win-x64`; Incluya binarios de host/servicio complementario y avisos de licencia. No sobrescriba la aplicación instalada.
- [] Informe las pruebas completadas, los adaptadores de hardware no probados y las puertas de liberación externas. Los seis entregables deben cumplir con sus criterios de aceptación antes de completar toda la solicitud.

## Planificar la autoevaluación

Las seis secciones de especificaciones se asignan a Tareas 1–12; La tarea 13 cubre la localización, la verificación integrada y la liberación. Los informes de preparación, la serialización de la ejecución, la configuración heredada, los secretos, la confianza de los paquetes y la ambigüedad del dispositivo físico tienen propietarios y pruebas explícitos. Las puertas de controlador externo/SDK/hardware permanecen incompletas hasta que se demuestren, en lugar de contarse como éxito del software.

## Elección de ejecución para revisión del usuario

Recomendado: ejecución nativa en este chat, tarea por tarea, porque la mayoría de los módulos comparten contratos y el espacio de trabajo actual contiene importantes ediciones existentes. La ejecución delegada está disponible si el usuario así lo elige. Revise este plan y seleccione el método de ejecución antes de la implementación, según lo requiere el flujo de trabajo de redacción de planes.
