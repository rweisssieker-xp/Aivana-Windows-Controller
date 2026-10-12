<!-- generated-doc locale=es-ES source=docs/superpowers/plans/2026-10-12-controller-usps.md sha256=b7af0d36209e04b458ce64a5db78ab7e552a84c5e9b450ea0bd97179a4eb7f68 -->

> Traducción asistida por máquina. Se comprueban la estructura, los ejemplos de código y los enlaces; esto no constituye una revisión por hablantes nativos ni una aprobación jurídica. Los informes históricos conservan sus fechas y alcance originales. [fuente original](../../../superpowers/plans/2026-10-12-controller-usps.md)

# Plan de implementación de extensión del controlador

**Objetivo:** Implementar las mejoras solicitadas en monitor, gestos, flujo de trabajo, grabación, diagnóstico de dispositivos, intercambio de perfiles portátiles y bandeja.

**Arquitectura:** Ampliar las capas Core/Windows/Desktop existentes. Las máquinas de estado puro y la lógica de configuración portátil se encuentran en Core, integraciones nativas en Windows y editores de borrador modales en Desktop. Conserve las ediciones existentes del espacio de trabajo sucio y la configuración de usuario implementada.

**Especificación:** ../specs/2026-10-12-controller-usps-design.md

- [x] Monitorear el servicio, la persistencia del objetivo y el estado en vivo asincrónico; prueba de identificaciones no disponibles y cancelación.
- [x] Máquina de estados de gestos y capas retenidas; Probar bordes, repetir, restablecer y vinculaciones inmediatas existentes.
- [x] Condiciones del flujo de trabajo, ramas directas y política de errores; instantáneas de prueba y cancelación.
- [x] Grabación de acceso directo explícito con revisión de borrador; prueba de filtrado y eliminación de anzuelos.
- [x] Intercambio de perfiles de carpetas compartidas con comprobaciones de revisión y vistas previas; conflictos de prueba y asignaciones portátiles.
- [x] Diagnóstico y claves especiales estándar genéricas por dispositivo; análisis de pruebas, asignaciones duplicadas y comportamiento de pausa.
- [x] Recursos de bandeja/ventana/icono ejecutable y apariencia pausada; verificar los recursos en el ejecutable publicado.
- [x] Integre todos los puntos de entrada de la paleta/editor y el texto localizado; ejecute las comprobaciones Core, Windows y WPF.
- [x] Publicar en un directorio provisional separado, conservar la configuración del usuario y reiniciar la aplicación actualizada.
