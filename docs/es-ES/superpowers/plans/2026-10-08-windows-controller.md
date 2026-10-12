<!-- generated-doc locale=es-ES source=docs/superpowers/plans/2026-10-08-windows-controller.md sha256=10a5ba49bbefc617bce40563c668e3c4628647822117087deba766713fb9b3af -->

> Traducción asistida por máquina. Se comprueban la estructura, los ejemplos de código y los enlaces; esto no constituye una revisión por hablantes nativos ni una aprobación jurídica. Los informes históricos conservan sus fechas y alcance originales. [fuente original](../../../superpowers/plans/2026-10-08-windows-controller.md)

# Windows Plan de implementación del controlador

[English (US)](2026-10-08-windows-controller.md) | [Deutsch](2026-10-08-windows-controller.md) | [Français](2026-10-08-windows-controller.md) | [Italiano](2026-10-08-windows-controller.md)

> Para trabajadores agentes: utilice propiedad aislada paralela; el usuario solicitó explícitamente una implementación completa con el máximo de subagentes útiles.

**Objetivo:** Entregar el panel de teclas de acceso rápido nativo Windows en el diseño aprobado.
**Arquitectura:** Configuración central y ejecución cancelable; Adaptadores Windows; WPF composición de escritorio.
**Pila técnica:** .NET 10, WPF, Win32, JSON. Sin dependencias pagadas.
**Especificación:** ../specs/2026-10-08-windows-controller-design.md

## Restricciones globales
interfaz de usuario alemana; almacenamiento local; Windows x64; estado veraz de la capacidad del hardware; sin registro de teclado; sin inicio automático predeterminado.

## Enfoque de revisión
Las importaciones con formato incorrecto no deben reemplazar las configuraciones guardadas; los modificadores retenidos no deben contaminar la salida; el hardware desconectado no debe fallar; el cierre debe preservar la disponibilidad de las bandejas; las macros canceladas deben liberar recursos.

## Tareas y propiedad
- [x] Agente principal: src/Aivana.Core y tests/Aivana.Tests. Modelos, persistencia JSON, validación, selección de perfiles y MacroRunner. Ejecute pruebas de comportamiento fallidas y luego implemente y ejecute todas las pruebas principales.
- [x] Agente Windows: src/Aivana.Windows. RegisterHotKey, inventario de dispositivos, SendInput, proceso de primer plano y adaptador de iluminación Corsair opcional. Verifique los tamaños de las estructuras nativas y la limpieza del registro con pruebas de humo seguras.
- [x] Agente GUI: src/Aivana.Desktop/MainWindow.xaml y .cs más ayudantes de UI solamente. Panel oscuro, editor completo, perfiles, dispositivos, configuraciones, descripción general del teclado y editor de macros. Construir una vez que existan los contratos.
- [x] Principal: contratos compartidos, archivos de proyecto, composición de DesktopController, ciclo de vida de aplicación/bandeja, diálogos de importación/exportación, documentación, comprobaciones de integración, publicación y prueba de humo de ejecución de aplicaciones.

## Interfaces compartidas
Los contratos se encuentran en Core/Models.cs. La implementación del controlador se encuentra en Desktop/DesktopController.cs. La interfaz de usuario llama a los métodos del controlador a través de esa clase concreta. Los agentes reciben firmas exactas en sus tareas. Cada uno posee archivos separados. El padre resuelve los errores entre proyectos una vez que todos los agentes finalizan.

## Verificación y entrega
- [x] dotnet build Aivana.slnx -c Lanzamiento: cero errores.
- [x] dotnet run --project tests/Aivana.Tests -c Lanzamiento: se pasan las comprobaciones de comportamiento.
- [x] dotnet publicar src/Aivana.Desktop -c Lanzamiento -r win-x64 --self-contained true -o artefactos/win-x64.
- [x] Inicie el ejecutable publicado, inspeccione la interfaz de usuario Windows y la estabilidad del proceso, ejerza un perfil seguro/edición/flujo de persistencia.
- [x] Escriba README con instrucciones de compilación/inicio y limitaciones de hardware; verificaciones de registros y pruebas restantes solo de hardware.
