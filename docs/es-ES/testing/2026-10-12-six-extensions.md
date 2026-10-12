<!-- generated-doc locale=es-ES source=docs/testing/2026-10-12-six-extensions.md sha256=ff2eb755b2f18dce8c7bd7c832018ae71ee706bcca65e37c191dbb55ff999c1e -->

> Traducción asistida por máquina. Se comprueban la estructura, los ejemplos de código y los enlaces; esto no constituye una revisión por hablantes nativos ni una aprobación jurídica. Los informes históricos conservan sus fechas y alcance originales. [fuente original](../../testing/2026-10-12-six-extensions.md)

# Seis ampliaciones: implementación y banco de pruebas.

Estado: 12. Octubre 2026, Sucursal `codex/six-extensions`. Estado de funcionamiento, sin autorización de producción.

## Servicio

La paleta de comandos abre las entradas **Configurar estación de trabajo**, **Fabricante de teclado y SDK**, **Panel móvil**, **Extensiones**, **UI automatización**, **UI flujo de registro** y **Prueba de ejecución**.

- Workspace Assistant crea perfiles de lugar de trabajo, reuniones, transmisión, investigación y proyectos. Los programas del Registro/Menú Inicio o Selector de archivos, URL, Salida/Pantalla y Escena OBS se muestran como pasos antes de guardar. La instalación no realiza ninguna acción.
- En el editor de pasos, **Check result** activa la verificación preliminar y la verificación posterior. El historial y el panel móvil distinguen la confirmación real del efecto no verificable. Los cambios relativos, los atajos de teclado y la entrada de texto no proporcionan una confirmación general de eficacia. La prueba del proceso no prueba la preparación técnica de la solicitud.
- UI Automation utiliza un trabajador independiente por tiempo limitado. Selección mediante la posición del mouse, verificación del selector, Invocar/Seleccionar/Alternar/Focus/SetValue/WaitState; Se rechazan múltiples visitas, objetivos deshabilitados y campos de contraseña. La grabación de capturas admitió acciones de clic y pausas; Las entradas de texto no se registran. La admisión será revisada antes de la aceptación.
- Extensiones: inspección ZIP, hashes, esquema, importación deshabilitada, confianza explícita antes de la activación, proceso aislado con IPC limitado, exportación, desactivación, desinstalación, restauración/descarte de la versión anterior. El aislamiento de procesos no es una zona de pruebas: los complementos compartidos se ejecutan con derechos de usuario. El paquete de muestra `makrivo.example.zip` contiene una cuenta regresiva y una acción de estado. Los esquemas admitidos representan un subconjunto de esquemas JSON simples documentados en el código fuente; ningún mercado público.
- Panel móvil: seleccione IP local y perfiles compartidos, abra la dirección HTTPS, verifique el certificado usando el SHA-256-Fingerprints que se muestra y confíe conscientemente en él, luego ingrese un código único de seis dígitos. Las claves privadas están protegidas con Windows-DPAPI; sólo se guardan los hashes del acceso del cliente. Los certificados cambian después de la renovación o cambio de IP. El usuario libera el cortafuegos Windows si es necesario. Sin instalación automática de certificados, reenvío de puertos ni cambios de firewall. El acceso del cliente se puede revocar. El panel muestra el estado de la acción y el historial de pruebas; Todavía no es un tablero universal con sensores en vivo.

## Teclados

| Fabricante | Implementación | Limitación |
|---|---|---|
| Corsair | teclas iCUE-G existentes y RGB | Se requiere conexión local iCUE y modelo adecuado |
| LogitechG | SDK de G-Key y SDK de LED independiente | liberar explícitamente el x64-DLL local original; G-Key SDK no proporciona identidad individual de varios teclados |
| Razer | Chroma RGB y reconocimiento de clave estándar | Asignar claves propietarias previamente en Synapse |
| SteelSeries | GameSense-RGB y reconocimiento de clave estándar | asignar claves propietarias previamente en GG |
| Wooting | Valores analógicos relacionados con el dispositivo, umbrales de presión/liberación | local original x64-SDK-DLL; La entrada HID normal permanece activa |
| Keychron/QMK/VIA/Ducky/ASUS/HyperX/Roccat/otros | Botones físicos estándar por dispositivo y código de escaneo | Los controles Fn y propietarios dependen del modelo |

No se incluyen nuevas DLL SDK patentadas. Los adaptadores del nuevo fabricante aún no están validados por hardware sin los dispositivos físicos adecuados.

**El segundo macropad exclusivo no está listo:** Raw Input observa los dispositivos por separado, pero no suprime su entrada normal Windows. Una versión de producción requiere un controlador de filtro firmado y con licencia verificado según Windows 11/Secure Pruebas de arranque y liberación de emergencia, bloqueo, suspensión y reconexión. No se instaló ningún controlador. La solicitud marca explícitamente este límite.

## Exámenes

- Versión de lanzamiento: exitosa, cero advertencias/errores.
- Core Console: todos los grupos de pruebas pasan, incluidos los conflictos de claves de proveedores, la reversión de complementos y los errores de ejecución reales en el historial de pruebas.
- Integración de extensiones: trabajador real de la UIA con aplicación de prueba WPF; invocación única, ambigüedad, objetivos deshabilitados, exclusión de contraseña. Proceso de complemento de tubería con nombre real, límites de protocolo, histéresis analógica y decodificación ABI Logitech exitosa.
- Host HTTPS real: certificado fijado, acceso no emparejado, código falso/único, protección de origen, archivos del navegador, almacenamiento hash y clave protegida exitosa. Se ha solucionado un error de Schannel con claves efímeras descubierto durante las pruebas.
- Windows suite con prueba de éxito en primer plano omitida explícitamente: exitosa; Audio/OBS/iCUE/Raw Input y estado de la pantalla verificados. En la ejecución anterior, el conjunto completo falló porque se rechazó el cambio de enfoque a la ventana de prueba aislada. La ejecución posterior del prototipo de IA también incluyó el conjunto completo que incluye cambio de enfoque y un teclado iCUE conectado. Otra repetición falló nuevamente debido al cambio de enfoque; Por lo tanto, la prueba dependiente del contexto no es verde estable. Esta prueba no fue atenuada. Los accesos de lectura DDC que fallan por un corto tiempo se repiten por un tiempo limitado; no se cambió ningún brillo.
- WPF-Smoke: todos los diálogos anteriores y nuevos renderizados, invariancia de configuración, editor y CRUD exitosos; Capturas de pantalla en la carpeta de compilación `smoke`.
- Navegador: Dramaturgo con Edge, 390×844 y 1024×768, flujo de acoplamiento/UUID/envío único/sin desbordamiento horizontal exitoso. Simulacro de API; prueba HTTPS real por separado. Fotos en `artifacts/mobile-browser`. El complemento del navegador no estaba disponible, por lo que Playwright recurrió regularmente.
- Revisión independiente del código realizada; Se corrigieron comandos móviles retrasados, resultados de verificación de terminal faltantes, verificación de ruta de proceso, reversión de complementos y reinicio de latidos RGB.

Aún abierto: integración exclusiva del controlador, pruebas físicas de LAN del fabricante/teléfono/tableta, escalado Windows por ciento 150/200 y verificación completa de la traducción de todos los textos de diálogo nuevos. Los nuevos textos de diálogo son predominantemente alemanes; Se conservan los catálogos de idiomas existentes. Por lo tanto, el requisito general ** no se ha aceptado completamente**.

## Generar paquete

`./tools/publish-complete.ps1 -SelfContained` crea una nueva carpeta en `artifacts/release-*`, que incluye escritorio, trabajador UIA, host HTTPS, archivos del navegador, complemento de muestra importable y notas de licencia de tiempo de ejecución. La aplicación instalada no se sobrescribe. Sin `-SelfContained`, el paquete debe coincidir con .NET-10-Desktop-/ASP.NET-Runtimes.

El paquete es un banco de pruebas local, no un instalador minorista firmado. Los propios términos de uso y los derechos de distribución del SDK son versiones independientes; consulte la documentación existente en `docs/licensing`.
