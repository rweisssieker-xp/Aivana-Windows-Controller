<!-- generated-doc locale=es-ES source=docs/superpowers/specs/2026-10-08-windows-controller-design.md sha256=f36d496c6af5190889e9a31487a8fc6afdeba5af18facdd600639ede5ddbd169 -->

> Traducción asistida por máquina. Se comprueban la estructura, los ejemplos de código y los enlaces; esto no constituye una revisión por hablantes nativos ni una aprobación jurídica. Los informes históricos conservan sus fechas y alcance originales. [fuente original](../../../superpowers/specs/2026-10-08-windows-controller-design.md)

# MAKRIVO – Borrador

[English (US)](2026-10-08-windows-controller-design.md) | [Deutsch](2026-10-08-windows-controller-design.md) | [Français](2026-10-08-windows-controller-design.md) | [Italiano](2026-10-08-windows-controller-design.md)

Estado: 8. Octubre 2026. Estado: para revisión antes de su implementación.

## Objetivo y alcance acordado

Una aplicación independiente Windows convierte un teclado de PC normal en un panel de acción libremente configurable similar a una plataforma de transmisión. El usuario confirmó todos los tipos de acción sugeridos, macros, detección de dispositivos, perfiles y una buena GUI. Se reconocen el Corsair conectado y otros teclados; Las funciones adicionales del fabricante sólo se ofrecen si realmente son compatibles.

Supuestos: interfaz alemana, uso local sin cuenta, Windows 11 x64 como primer objetivo, sin instalación de controlador. El teclado sigue siendo utilizable para escribir normalmente. De forma predeterminada, las asignaciones globales utilizan atajos de teclado con modificadores. Las claves de letras individuales no están reservadas globalmente de forma predeterminada.

## Decisión técnica

C#/.NET-10-App nativo con WPF y MVVM. El estable .NET-10-SDK está disponible en el espacio de trabajo. WPF permite una interfaz de escritorio personalizada y una integración directa con Windows. Las alternativas serían una interfaz web con un servicio Windows independiente o una aplicación de escritorio multiplataforma; Ambos aportan tiempo de ejecución y complejidad de comunicación adicionales a este proyecto Windows. Por tanto, la variante nativa es la base elegida.

División: Proyecto de escritorio para vistas y bandeja, proyecto central para configuración y lógica de ejecución, proyecto Windows para teclas de acceso rápido, simulación de entrada y dispositivos, proyecto de prueba para reglas independientes de la plataforma. Los adaptadores del fabricante dependen de una interfaz de capacidad fija y son independientes de las acciones estándar.

## superficie

Diseño oscuro con superficies tranquilas en color antracita, fuente fácil de leer y detalles en turquesa. Vincula la navegación para el panel, perfiles, dispositivos y configuraciones. Perfil activo, busque y cambie para pausar todas las teclas de acceso rápido en la parte superior. La interfaz sigue siendo utilizable en 100–200 por ciento de escala Windows.

El tablero incluye una cuadrícula de mosaicos personalizable. Cada mosaico muestra icono, nombre, tecla de acceso rápido y estado de ejecución. Hacer clic realiza la misma acción que la tecla de acceso rápido. Se abre un editor a la derecha con tipo de acción, parámetros, grabación de teclas de acceso rápido, símbolo/color, activación y botón de prueba. Están disponibles Nuevo, Duplicar, Eliminar y Reordenar. Una vista de teclado muestra esquemáticamente las asignaciones para el diseño estándar alemán; no afirma la geometría exacta del dispositivo detectado.

Los perfiles se pueden crear, copiar, cambiar de nombre y activar manualmente. Opcionalmente, se asigna un nombre de proceso a un perfil. Un programa de primer plano adecuado activa su perfil; de lo contrario, se aplica el perfil estándar global. Si hay varios perfiles coincidentes, se aplica un orden explícitamente visible. El editor explica los conflictos de teclas de acceso rápido directamente en la tarea.

## Teclas de acceso rápido y entradas

Windows RegisterHotKey maneja combinaciones globales normales con la repetición de claves suprimida. Sólo se registra el perfil activo. Las combinaciones reservadas o ya ocupadas aparecen como errores; la aplicación no informa que se haya activado correctamente. La grabación en el editor no desencadena ninguna acción.

Antes de las entradas simuladas, las teclas modificadoras de activación se sueltan o se espera a que se suelten físicamente. El texto Unicode y las combinaciones de teclas se envían mediante SendInput. Cada tecla que presiones se liberará nuevamente incluso si cancelas. Las acciones se ejecutan fuera del hilo de la interfaz de usuario; La ejecución y los cambios de perfil no deben bloquear la superficie.

## Acciones y macros

Tipos de acciones: iniciar el programa con argumentos y directorio de trabajo, abrir archivo/carpeta a través de Windows, abrir el sitio web HTTP/HTTPS, ingresar texto Unicode, enviar combinación de teclas, reproducir medios/pausar/siguiente/anterior/detener, subir/bajar volumen/silenciar y cambiar de perfil. Los parámetros se validan antes de guardarlos y nuevamente antes de ejecutarlos. Los programas se inician directamente; Los argumentos no se interpretan implícitamente como comandos de shell.

Las macros contienen una lista ordenada de acciones con nombre y pasos de pausa explícitos. Los pasos se pueden agregar, editar y mover. Las repeticiones finalmente son configurables (1–100); Los bucles infinitos no forman parte de la primera versión. Una macro en ejecución se puede finalizar a través de la interfaz o de su propia tecla de acceso rápido de cancelar. La activación repetida de la misma asignación actual no inicia una ejecución paralela. Se serializan diferentes macros con simulación de entrada para que sus pulsaciones de teclas no se mezclen. Si se produce un error de paso, la macro se detiene y muestra el paso afectado.

## Dispositivos y fabricantes

Inventario de dispositivos a través de Windows Raw Input e información adicional del dispositivo: nombre para mostrar, ruta del dispositivo, identificador de fabricante/producto si está disponible, estado de conexión y capacidades detectadas. Conectar/desconectar actualiza la lista sin reiniciar. Un dispositivo físico puede tener varias entradas HID; Estos se agrupan según identificadores fiables; de lo contrario, se mantienen visiblemente como entradas separadas.

Las teclas de acceso rápido normales se aplican en todos los teclados. La vista del dispositivo proporciona información y capacidades; un teclado separado con teclas exclusivamente interceptadas no forma parte de este modo acordado.

La detección Corsair también funciona sin iCUE. Un adaptador SDK iCUE opcional proporciona el estado de la conexión y las acciones de iluminación admitidas cuando el SDK, iCUE y un dispositivo compatible están presentes. La interfaz de usuario diferencia SDK reconocido, disponible, conectado y realmente controlable. El SDK documenta principalmente el control de LED; Las teclas G propietarias solo se admiten como teclas de acceso rápido normales si Windows recibe la entrada adecuada. No existe una promesa general para todas las claves especiales.

Otros fabricantes reciben inicialmente detección genérica y acciones estándar. La interfaz del adaptador permite integraciones posteriores; Las habilidades no respaldadas se muestran honestamente. Las pruebas de hardware para modelos inexistentes no se consideran aprobadas.

## Almacenamiento y vida cotidiana.

Configuración JSON versionada en el directorio de datos de la aplicación local. Guardado atómico con la última copia de seguridad válida; los archivos incorrectos no se sobrescriben silenciosamente. La importación verifica el formato, la versión, los ID y los parámetros y muestra una vista previa antes de aplicar. Las acciones importadas solo se pueden ejecutar después de una activación explícita. La exportación contiene perfiles y asignaciones.

Menú de bandeja: Abrir, selección de perfil, pausar teclas de acceso rápido, cancelar macros, salir. Cerrar ventana se minimiza en la bandeja de forma predeterminada y explica esto la primera vez. El inicio automático es opcional y está desactivado de forma predeterminada. Sólo una instancia gestiona las teclas de acceso rápido. Salir registra teclas de acceso rápido, aborta ejecuciones y libera recursos.

Vista de actividad local con historial limitado de éxito, fracaso y cancelación. No hay registro de todas las pulsaciones de teclas. El contenido de la acción de texto no se detalla en los mensajes de error. Sin servicio en la nube ni telemetría.

## Manejo de errores y límites

Los programas faltantes, las URL no válidas, las teclas de acceso rápido ocupadas, los perfiles dañados y el SDK del fabricante inaccesible reciben mensajes específicos en alemán. Una falla del adaptador no afecta las acciones estándar. Las entradas a aplicaciones con mayores privilegios pueden fallar debido a los límites de permisos Windows; Se explican los errores, la aplicación no requiere derechos de administrador de forma predeterminada.

## Prueba y aceptación

Comprobaciones automatizadas de prioridad de perfil, detección de conflictos, importación de configuración, almacenamiento atómico, ordenación de macros, reintento, cancelación y propagación de errores. Windows pruebas de humo para teclas de acceso rápido reales, texto y atajos de teclado, acciones multimedia, inventario de dispositivos, bandeja y salida limpia. No hay pruebas que inicien accidentalmente programas o sitios web arbitrarios.

Verificación de GUI: creación y edición de tareas, grabación sin ejecución, clic/tecla de acceso rápido con el mismo efecto, cancelación de macro, mensaje de conflicto, cambio de perfil, vista de dispositivo, importación/exportación y escalado. Las pruebas de hardware específicas del fabricante se documentan por separado con el dispositivo real.

Entrega: Código fuente, instrucciones de compilación/inicio reproducibles, manual de usuario en alemán y Windows-x64-Publish-Artefakt local. El éxito significa una aplicación ejecutable con todas las funciones genéricas; Las capacidades Corsair solo están marcadas como disponibles con compatibilidad comprobada con SDK/dispositivo.

## Fuentes técnicas

- Microsoft Entrada de teclado/RegistroHotKey: https://learn.microsoft.com/en-us/windows/win32/inputdev/about-keyboard-input
- Microsoft Raw Input: https://learn.microsoft.com/en-us/windows/win32/inputdev/about-raw-input
- Microsoft WPF Entrada: https://learn.microsoft.com/dotnet/desktop/wpf/advanced/input-overview
- Oficial Corsair iCUE SDK: https://github.com/CorsairOfficial/cue-sdk/blob/master/README.md

## Siguiente paso

Después de revisar este borrador: cree un plan de implementación concreto y envíelo para la selección de implementación. Todavía no hay ningún repositorio Git en la carpeta de trabajo vacía; Por lo tanto, este documento aún no se ha comprometido.
