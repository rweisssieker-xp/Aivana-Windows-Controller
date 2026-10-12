<!-- generated-doc locale=es-ES source=docs/superpowers/specs/2026-10-12-controller-usps-design.md sha256=4c3b2dbedb83af6bd48917515ae6559012afb6318ce69d1e32fb2765e0793327 -->

> Traducción asistida por máquina. Se comprueban la estructura, los ejemplos de código y los enlaces; esto no constituye una revisión por hablantes nativos ni una aprobación jurídica. Los informes históricos conservan sus fechas y alcance originales. [fuente original](../../../superpowers/specs/2026-10-12-controller-usps-design.md)

# MAKRIVO extensiones de control

El usuario solicitó las nueve mejoras propuestas y un ícono de bandeja reconocible. Las configuraciones existentes, en particular el brillo G1/G2, deben seguir siendo compatibles. Las ediciones de marca y documentación existentes no confirmadas pertenecen al espacio de trabajo compartido y deben conservarse.

## Entregables y límites

1. Las acciones de brillo seleccionan todos los monitores compatibles o una ID de monitor específica estable. Los objetivos explícitos que faltan fracasan visiblemente. El panel muestra el brillo actual, el volumen de salida, el estado del micrófono y el estado OBS a través de lecturas asíncronas limitadas.
2. Las teclas especiales admiten presionar, tocar, mantener presionado y tocar dos veces inmediatamente, con supresión de repetición. Un selector de capa retenido cambia temporalmente el mapeo activo y la liberación lo restaura. Los enlaces inmediatos existentes conservan su comportamiento. Pausa, cambios de perfil, desconexiones y cancelación de gestos claros pendientes.
3. El diseñador del flujo de trabajo edita las condiciones, reenvía bifurcaciones y la política de detener/continuar fallas de forma transaccional en un borrador de mosaico. Las condiciones cubren archivos, procesos en ejecución y títulos de ventanas. No hay ciclos de salto ilimitados.
4. La grabación explícita captura acordes de acceso directo y espera, ignora la entrada inyectada y se detiene en Escape. Las acciones registradas se revisan antes de su adopción; El texto normal no se graba.
5. Las claves especiales estándar por dispositivo de otros fabricantes utilizan Windows Raw Input. Las claves propietarias requieren una API de proveedor compatible o una asignación de proveedor a una clave estándar; La funcionalidad no compatible se muestra honestamente. El diagnóstico explica el perfil, la pausa, los conflictos de enlace y el estado real del adaptador.
6. El intercambio de perfiles utiliza una carpeta compartida seleccionada por el usuario (incluidas las carpetas sincronizadas por un cliente en la nube existente). Las comprobaciones de revisión evitan sobrescrituras silenciosas. Extraiga vistas previas de cambios e importe acciones deshabilitadas con asignaciones de hardware eliminadas. La inclusión del contenido de la acción es explícita. MAKRIVO no almacena credenciales en la nube.
7. Las cadenas de UI pertenecen a los catálogos de idiomas 27 existentes; DE/EN/FR/IT siguen siendo los idiomas mínimos de documentación revisados. Las lagunas en la traducción deben revelarse en lugar de afirmarse que están completas.
8. Una marca de teclado M verde azulado reconocible reemplaza el ícono genérico de la bandeja Windows. El modo en pausa tiene una insignia de pausa de color ámbar. La marca del ejecutable/ventana utiliza la misma marca. La información sobre herramientas incluye estado y perfil.

## Verificación

Las comprobaciones principales cubren compatibilidad con versiones anteriores, instantáneas, cancelación, sincronización de gestos, bifurcaciones/errores del flujo de trabajo, filtrado de grabadoras y conflictos de sincronización. Las comprobaciones Windows cubren la enumeración de visualización de solo lectura, los eventos de lanzamiento del SDK y el análisis Raw Input. WPF Las comprobaciones de humo cubren nuevos puntos de entrada y recursos sin cambiar el brillo real del monitor, escribir en otra aplicación o sobrescribir perfiles de usuario. La implementación conserva una copia de seguridad y la configuración G1/G2.
