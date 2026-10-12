<!-- generated-doc locale=es-ES source=docs/superpowers/specs/2026-10-12-six-extensions-design.md sha256=a80dda083fbbe90404023ac784b47e7f16f89f72ce0b821e3cda122dc561317f -->

> Traducción asistida por máquina. Se comprueban la estructura, los ejemplos de código y los enlaces; esto no constituye una revisión por hablantes nativos ni una aprobación jurídica. Los informes históricos conservan sus fechas y alcance originales. [fuente original](../../../superpowers/specs/2026-10-12-six-extensions-design.md)

# MAKRIVO: seis extensiones de producto y adaptadores de teclado

Fecha: 2026-10-12
Estado: diseño propuesto para revisión del usuario; la implementación no ha comenzado.

## Objetivo
Entregue las seis extensiones solicitadas en la aplicación Windows/WPF existente, preservando los perfiles existentes, las asignaciones G1/G2, la operación local y el uso sin cuenta. Amplíe la integración del teclado a Razer y Logitech, con una arquitectura reutilizable basada en capacidades para otros fabricantes. El nombre del fabricante o la distribución del teclado nunca son evidencia de que se admitan controles propietarios.

## Decisión de arquitectura
Amplíe Aivana.Core con validaciones y contratos serializables, Aivana.Windows con Windows y adaptadores de proveedores, y Aivana.Desktop con editores transaccionales y vistas de estado. Utilice proyectos complementarios independientes para la ejecución aislada de complementos y el servicio de panel móvil local. Evite una reescritura o un único proceso privilegiado que aloje cada extensión. La configuración existente debe cargarse sin cambios; agregue tipos de acciones en lugar de reordenar sus valores de enumeración persistentes.

Alternativas consideradas: una implementación monolítica entrelazaría la ejecución del hardware, la red y el flujo de trabajo; reemplazar WPF agregaría trabajo de migración no relacionado. Se recomiendan extensiones modulares.

## 1. Teclado macro independiente
Almacene enlaces por identidad del dispositivo más código de escaneo físico e indicadores de clave extendida. Agregue modos exclusivos y de observación explícita, descubrimiento de dispositivos, captura de claves, supresión de repeticiones, gestos, capas retenidas y diagnósticos de reconexión. Conservar principal/teclado numérico Ingrese distinciones y pruebe diseños alemanes/estadounidenses. Los enlaces apuntan a un teclado físico seleccionado; las identidades de reconexión ambiguas requieren una nueva selección en lugar de controlar otro dispositivo.

Raw Input proporciona únicamente observación. La supresión exclusiva por dispositivo requiere un backend de filtro de teclado real; nunca correlacione ganchos globales de bajo nivel con Raw Input y reclame una supresión confiable específica del dispositivo. Proporcionar un mecanismo de vigilancia, liberación de emergencia, comportamiento de apertura fallida e instalador/desinstalador. No capture entradas seguras del escritorio. La instalación del controlador y la activación del modo exclusivo son acciones explícitas e independientes del usuario.

Puerta del controlador: seleccione un backend compatible, con la licencia adecuada y firmado que sea compatible con las configuraciones Windows 11 de destino, o desarrolle/firme un filtro dedicado. Interception es un candidato a evaluar, no una dependencia de producción comprometida: su archivo README oficial describe las pruebas a través de Windows 10 y licencias comerciales distintas. Hasta que se alcance este límite, el modo exclusivo debe mostrarse como no disponible; la característica no se puede declarar completa.

Aceptación: con dos teclados físicos, la misma tecla ejecuta una macro y no produce ninguna entrada de aplicación normal solo en el dispositivo exclusivo seleccionado; otras entradas del teclado no se ven afectadas. Verifique la desconexión, falla, cancelación, suspensión/reanudación y eliminación del controlador en el hardware real.

## 2. Configuración guiada del espacio de trabajo
Agregue un asistente para el lugar de trabajo, reuniones, transmisión, investigación y cambio de proyectos. Descubra aplicaciones instaladas, dispositivos de audio, monitores y escenas OBS. Los usuarios eligen objetivos y enlaces; Obtenga una vista previa de los pasos resultantes y valide los requisitos faltantes. Guarde solo después de la revisión, sin ejecutar ninguna acción durante la configuración. Los perfiles generados siguen siendo editables en los editores existentes.

Aceptación: cree un perfil de trabajo sin editar manualmente rutas ejecutables o ID de dispositivos; la cancelación deja la configuración sin cambios; El software faltante tiene una explicación procesable.

## 3. Ejecución verificada
Introduzca comprobaciones previas, resultados escritos por paso y condiciones posteriores opcionales. Distinga los estados solicitado, completado, verificado, fallido, agotado, cancelado y no verificable. Verifique el audio/micrófono, el brillo, la preparación del proceso/ventana y el estado OBS con lecturas asíncronas limitadas. La entrega de acceso directo/texto no es prueba de que la aplicación de destino aceptó el comando. Los efectos secundarios externos no son universalmente reversibles; ofrecer compensación sólo cuando se implemente.

Aceptación: los objetivos explícitos desconectados nunca retroceden silenciosamente; el estado observado incorrecto nunca informa un éxito verificado; la cancelación detiene la votación; Las fallas identifican el paso y la acción de recuperación. El historial de ejecución omite secretos y cargas útiles de texto.

## 4. Complementos y paquetes compartibles
Los manifiestos JSON versionados declaran acciones, esquemas de parámetros, proveedores de estado, dependencias y capacidades. Ejecute complementos ejecutables en un proceso separado sobre IPC local delimitado con protocolo de enlace, fechas límite y cancelación. Rechazar mensajes con formato incorrecto y versiones incompatibles; manejar fallas sin terminar el controlador. Las declaraciones de capacidad son divulgación y mediación, no una afirmación de sandboxing del sistema operativo.

Proporcione vista previa de exportación/importación de paquetes, hashes de contenido, protección de recorrido de ruta, límites de tamaño, comprobaciones de versión/dependencia, habilitación explícita y reversión de actualizaciones. Los complementos ejecutables importados no deben ejecutarse automáticamente. Incluya un complemento de ejemplo funcional con un flujo de instalación/acción/estado/desinstalación de un extremo a otro. No se requiere ningún servicio de mercado público para esta versión.

Aceptación: crear, exportar, instalar y ejecutar una integración de ejemplo real; demostrar tiempo de espera, aislamiento de fallas, falla de dependencia, rechazo transversal e importación deshabilitada.

## 5. Windows Automatización de la interfaz de usuario
Agregue un selector de elementos y acciones limitadas para invocar, valorar, seleccionar, alternar, enfocar y esperar un elemento/estado. Selectores persistentes que utilizan proceso, ID de automatización, tipo de control y contexto ancestral; Evite el registro solo de coordenadas. Muestra el recuento de coincidencias del selector y prueba los selectores como de solo lectura. Los campos de contraseña están excluidos de la grabación. Maneje controles no admitidos y restricciones de nivel de integridad Windows de forma explícita.

Amplíe la grabación con acciones UIA revisables, conservando la grabación de acceso directo actual. Los selectores ambiguos fallan en lugar de hacer clic en un control arbitrario. Un árbol de accesibilidad de navegador normal no es la promesa de un motor de automatización DOM completo.

Aceptación: automatice una aplicación de prueba dedicada con selectores estables en todos los cambios de posición/tamaño; ambigüedad de la prueba, elementos faltantes, controles deshabilitados, tiempos de espera y cancelación.

## 6. Panel de teléfono/tableta local
Ofrezca un panel responsivo desde un servicio LAN opcional. Emparéjelo explícitamente utilizando un código único de corta duración y emita credenciales revocables protegidas localmente. Utilice HTTPS con confianza/fijación de certificados locales documentados; sin respaldo para enviar credenciales en texto sin formato. El valor predeterminado es deshabilitar y restringir las interfaces configuradas. La validación del origen, los comandos autenticados, los límites de velocidad y los mensajes acotados son obligatorios.

Exponer perfiles/acciones permitidas, estado en vivo, progreso y cancelación; utilice ID estables y la misma cola de ejecución que las claves físicas y de escritorio. No exponga comandos arbitrarios, rutas ejecutables, secretos o instalación de complementos. Proporcionar pantallas de conexión, emparejamiento y revocación. No se incluye ninguna cuenta en la nube ni retransmisión de Internet.

Aceptación: operar desde un teléfono/tableta real en la misma LAN; los clientes no emparejados no pueden enumerar ni ejecutar acciones; los tokens revocados fallan; los comandos duplicados/reproducidos no se ejecutan dos veces; La desconexión no reintenta acciones silenciosamente.

## Alcance del fabricante del teclado

| Adaptador/familia | Integración planificada | Límite explícito |
| --- | --- | --- |
| Corsair | Preservar la entrada de tecla G actual iCUE y la integración RGB | La detección de capacidad de modelo/software sigue siendo necesaria |
| LogitechG | Adaptador SDK de clave G con comprobaciones de disponibilidad de prensa/lanzamiento y tiempo de ejecución; iluminación a través de su SDK independiente donde sea compatible | No equipare la detección de G HUB con la API de clave G compatible en todos los modelos |
| Razer | Adaptador de iluminación cromática y mapeo guiado de Synapse a clave estándar, además de entrada HID genérica | Chroma es una API de iluminación; no lo etiquetes como entrada de clave de macro patentada nativa |
| SteelSeries | GameSense adaptador de estado/iluminación más entrada HID genérica | GameSense el envío de eventos no es una API de captura de claves patentada general |
| Wooting | Adaptador SDK analógico para entrada analógica compatible, además de claves HID normales | Las capacidades y los términos de distribución del SDK deben verificarse según el modelo admitido. |
| Keychron/QMK/VIA | Claves HID estándar e instrucciones de mapeo de firmware; diseños conocidos sólo con evidencia | VIA El soporte de configuración no es una API de clave de macro de tiempo de ejecución universal. |
| Otros teclados HID estándar | Descubrimiento y mapeo de claves físicas por dispositivo; modo exclusivo solo con el backend de filtro validado | No hay soporte para Fn, dial o tecla patentada específicos del modelo fabricado |

Cada dispositivo muestra el estado detectado, observado, mapeado a través del software del proveedor, SDK nativo, exclusivo del controlador, no disponible o no probado por el hardware, según corresponda. Conserve la evidencia de capacidad con versiones de adaptadores e identificadores de modelo/firmware probados.

## orden de entrega
1. Compatibilidad de configuración, contratos de capacidad y ejecución verificada.
2. Configuración guiada utilizando descubrimiento real y ejecutores de acciones existentes.
3. UI Automation y su grabador/dispositivo de prueba.
4. Host de complementos, flujo de paquetes e integración de ejemplo.
5. Panel LAN emparejado y verificación móvil.
6. Adaptadores de proveedores ampliados e integración exclusiva de backend de teclado, con validación de hardware real.

Los seis siguen siendo necesarios. La firma/licencia de controladores, los SDK inaccesibles o la ausencia de hardware de prueba son puertas de lanzamiento, no razones para sustituir un comportamiento simulado y dar por finalizado el trabajo.

## Criterios de verificación y liberación.
Ejecute comprobaciones del núcleo existente, Windows y WPF y agregue pruebas de integración enfocadas para cada nuevo límite. Verifique los viajes de ida y vuelta JSON y los perfiles heredados; Pruebe el contenido ejecutable importado, la autenticación de red, la cancelación, los fallos de los complementos y la reconexión del dispositivo. Inspeccione los diseños de escritorio y móviles. Valide la escala alta Windows por separado. Cree artefactos de lanzamiento solo después de pasar las comprobaciones; Distinga la funcionalidad implementada/probada, implementada/no probada por hardware y bloqueada externamente en las notas de la versión. No reemplace la aplicación instalada, instale controladores, altere asignaciones de teclado reales ni abra el acceso a la red como prueba incidental.

## evidencia
- Microsoft Raw Input semántica: https://learn.microsoft.com/en-us/windows/win32/api/winuser/ns-winuser-rawinputdevice
- SDK para desarrolladores de Logitech G: https://www.logitechg.com/en-us/programs/developer-alliance
- Preguntas frecuentes sobre el SDK de croma Razer: https://developer.razer.com/works-with-chroma/chroma-faq/
- Repositorio de interceptación y condiciones de dependencia: https://github.com/oblitum/Interception
- Inventario de fabricantes de repositorios existentes: docs/de-DE/research/tastaturhersteller-sonderfunktionen.md

## Revisar decisiones
Se solicita al usuario que apruebe este diseño escrito antes del plan de implementación. La principal dependencia externa es un backend de controlador de entrada exclusivo listo para producción. El alcance recomendado conserva esto como un entregable cerrado en lugar de reducirlo silenciosamente a la observación Raw Input.
