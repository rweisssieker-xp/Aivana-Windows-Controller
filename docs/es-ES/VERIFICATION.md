<!-- generated-doc locale=es-ES source=docs/VERIFICATION.md sha256=f19afb9e7b5ffac90c83167416ab3af0964a4481dd16b7357f8585480c6aae65 -->

> Traducción asistida por máquina. Se comprueban la estructura, los ejemplos de código y los enlaces; esto no constituye una revisión por hablantes nativos ni una aprobación jurídica. Los informes históricos conservan sus fechas y alcance originales. [fuente original](../VERIFICATION.md)

# Examen – 8. Octubre 2026

[English (US)](../en-US/VERIFICATION.md) | [Deutsch](../de-DE/VERIFICATION.md) | [Français](../fr-FR/VERIFICATION.md) | [Italiano](../it-IT/VERIFICATION.md)

La aplicación se creó en Windows x64 con el SDK de .NET estable 10.0.202 y se lanzó como Windows-x64-Ausgabe independiente.

## Comprobado automáticamente

- Construcción general: sin advertencias ni errores.
- 19 Pruebas de comportamiento principales: configuración predeterminada, validación, datos nulos/duplicados, prioridad de perfil, importación/exportación y restauración de copia de seguridad, orden y reintento de macros, cancelación, doble activación, serialización, instantánea de ejecución inmutable y falla de paso.
- Verificación Windows: normalización y alias de teclas de acceso rápido, tamaños de estructura nativa, lista de dispositivos de teclado reales, registro seguro y volver a compartir, WM_HOTKEY sintético para mosaico y cancelación, registro de mosaico deshabilitado durante la simulación de entrada.
- El EXE independiente se inició con `--smoke-test`: se representan las cinco vistas WPF, los mosaicos se pueden crear/copiar/eliminar, los borradores se conservan durante la actualización, la configuración se guarda y se vuelve a cargar. Esta prueba utiliza un directorio de configuración temporal aislado y no realiza ninguna acción de texto o multimedia.
- El tablero, el editor y la representación compacta (900 × 720) se inspeccionaron visualmente como representaciones WPF reales. Se verifican el guardado de borradores, el descarte, el orden de mosaicos/macro, los alias de teclas de acceso rápido alemanas, las cancelaciones de macros individuales y el progreso de los pasos.

## Límites dependientes del hardware

La última ejecución local detectó cuatro interfaces de teclado Windows. Este número describe las interfaces HID/Windows, no necesariamente cuatro teclados físicos.

El SDK oficial iCUE está instalado y se encuentra junto al EXE. La prueba en vivo se conecta a iCUE y detecta un teclado Corsair con modelo e información LED. Los cambios de iluminación no se activaron automáticamente; Las llaves especiales según el modelo y de otros fabricantes no se prueban por hardware. Las teclas de acceso rápido genéricas se aplican en todos los teclados.

La entrada de texto/medios no se inyectó en otros agentes de usuario abiertos. Se verificaron las estructuras de datos nativas y la lógica de error/liberación; El comportamiento en un determinado programa de destino depende, entre otras cosas, de su nivel de autorización.

## repetir

```powershell
dotnet build Aivana.slnx -c Release
dotnet run --project tests/Aivana.Tests -c Release
dotnet run --project tests/Aivana.Windows.Tests -c Release
dotnet publish src/Aivana.Desktop -c Release -r win-x64 --self-contained true -o artifacts/win-x64
& ./artifacts/win-x64/MAKRIVO.exe --smoke-test
```

La prueba GUI escribe el resultado y la captura de pantalla en `smoke` junto al EXE probado. Está pensado como un modo de diagnóstico y no inicia una instancia en segundo plano persistente al finalizar.


## Controles avanzados

Comprobado adicionalmente: condiciones de perfil con proceso y título de ventana, reemplazo de variables sin expansión recursiva, vista previa de macro sin ejecución, validación de nuevas acciones y compatibilidad con versiones anteriores de archivos JSON antiguos. Las comprobaciones nativas cubren la paleta de comandos con registro, conflictos, envío y liberación de WM_HOTKEY, así como división pura del monitor y acceso de lectura al portapapeles bajo demanda. La prueba WPF abre y renderiza la paleta real, verifica deshacer/rehacer y favoritos, verifica borradores limpios después de renderizar y completa el proceso exitosamente.

Windows el bloqueo, los cambios en el portapapeles y el movimiento de ventanas no se aplicaron a los agentes de usuario durante las pruebas. Publicar acciones de ventana Windows solicitudes de forma asincrónica; El programa de destino específico puede tener sus propios tamaños mínimos o límites de autorización. El bloqueo del perfil y las estadísticas de ejecución se aplican a la sesión actual.


Examen de extensión de 12.10.2026: 30 exámenes básicos aprobados; Adaptador Windows que incluye liberación durante la inyección de entrada, detección de monitor de solo lectura y simulacro de estado OBS aprobado. WPF comprueba la confirmación/cancelación del flujo, el inicio/detención de la grabadora, la vista previa del intercambio de perfiles, los gestos y los recursos de símbolos. Localización: catálogos 27 cada uno con claves 967; Documentación: 84 páginas en cuatro idiomas. Los botones propietarios distintos de Corsair no se han probado en hardware; El soporte genérico incluye eventos estándar específicos del dispositivo F13–F24 y claves multimedia. El transporte en la nube se produce a través de una carpeta sincronizada seleccionada; Las importaciones se verifican y permanecen deshabilitadas.
