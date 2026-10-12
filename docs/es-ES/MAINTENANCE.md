<!-- generated-doc locale=es-ES source=docs/MAINTENANCE.md sha256=9bf5875b50386e698c0574eee390f790d4b1c29cb11288c856c98e03a05877f7 -->

> Traducción asistida por máquina. Se comprueban la estructura, los ejemplos de código y los enlaces; esto no constituye una revisión por hablantes nativos ni una aprobación jurídica. Los informes históricos conservan sus fechas y alcance originales. [fuente original](../MAINTENANCE.md)

# Mantener todos los idiomas de documentación

El inventario de la documentación sigue todos los 27 Los mapas generados utilizan códigos regionales; mapas noruegos para `nb-NO` El latín serbio para `sr-Latn`Cada pasta contiene un índice, manual, referencia de acción detallada, inventario de características, notas de verificación, investigación incluyendo su CSV, planes históricos y análisis de licencia.

## Fuente de la verdad

Edit la raíz `README.md`, `docs/HANDBOOK.md`, `docs/FEATURES.md`, `docs/VERIFICATION.md`, esta página, o los archivos originales en `docs/research`, `docs/testing`, `docs/superpowers` y `docs/licensing`No reescriba un informe histórico para que se suponga que validue una versión posterior. Mantenga la implementación, la simulación, la aceptación de dispositivos reales y la aprobación comercial distinta.

## Generar y validar

```powershell
python tools/localize_documentation.py --refresh-sources
python tools/check_documentation.py
```

El generador en línea opcional envía la documentación al punto final de la traducción pública de Google, siguiendo la herramienta de localización existente. No lee la configuración del usuario, los secretos o los archivos de modelo. Sólo se ejecuta en la documentación adecuada para ese servicio. Los bloques de código, el código de línea, las URL, la estructura de la tabla y los identificadores técnicos son protegidos. Los cajeros de traducción y una imagen de fuente congelada se almacenan bajo ignorados `artifacts/documentation-translation`.

Cuando el proveedor de Internet está limitado, `tools/translate_documentation_local.py` rellenar el mismo cache utilizando un local explicitamente suministrado M2M100 CTranslate2 El modelo. el 2026-10-12 Snapshot utiliza la licencia MIT facebook/m2m100_418M El modelo, convertido en JustFrederik/m2m_100_418m_ct2_int8 En la revisión `1aeed44db4dd61a486bba44acf54c76507082a2c`. CTranslate2 4.8.2 y `SentencePiece` 0.2.2 son herramientas construidas. no se envían pesos de modelo o dependencias de traducción con MAKRIVO. Con el generador `--cache-only` Después de llenar el cache para render sin solicitudes de red. El asistente local no descarga o ejecuta el código de modelo remoto.

```powershell
python -m pip install ctranslate2==4.8.2 sentencepiece==0.2.2
python tools/translate_documentation_local.py --model C:\models\m2m100-ct2 --refresh-sources
python tools/localize_documentation.py --cache-only
python tools/check_documentation.py
```

sin `--refresh-sources`, un curso reutiliza su snapshot congelado. Utilice la opción de refresco después de editar los originales. Se reutilizan los segmentos ocultos. Un curso parcial puede seleccionar los códigos locales utilizando `--locales`Los runs interrumpidos no sustituyen silenciosamente la prosa no traducida por una traducción fallida.

El registro manifiesto de cada fuente y archivo generado con SHA-256 impresiones de texto normalizado UTF-8: terminaciones de la línea LF y sin marca de orden de byte. El checker fuera de línea verifica la cobertura completa por lenguaje, las impresiones digitales, los enlaces locales, los ejemplos de código, la estructura de la tabla/CSV, las URL de fuente y los lugares no reemplazados. Se rechaza las fuentes de acero. Un cheque estructural que pasa no es una revisión del orador nativo o la aprobación legal. Las páginas generadas llevan esta calificación visiblemente.

## Publicación y paquetes

Publica la documentación, el generador y el checker juntos. Preserva los términos de licencia de terceros originales; la análise de licencia traducida es informativa. Los archivos de artefactos, cache de traducción y orquestación local no pertenecen a Git. Windows El instalar.

Las traducciones de aplicaciones de uso son catalogados separados por JSON `src/Aivana.Core/Localization`La generación de documentación no modifica esos catálogos.Una aplicación construida y su documentación debe compartir una imagen de pantalla de versión; el soporte de tiempo de trabajo experimental recientemente descrito puede preceder a un paquete instalado.
