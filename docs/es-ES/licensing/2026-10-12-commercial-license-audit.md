<!-- generated-doc locale=es-ES source=docs/licensing/2026-10-12-commercial-license-audit.md sha256=b58c6861e0b38c4495265651b5ab10349c701d8fb4582dd7d7cae5d2d865bc5f -->

> Traducción asistida por máquina. Se comprueban la estructura, los ejemplos de código y los enlaces; esto no constituye una revisión por hablantes nativos ni una aprobación jurídica. Los informes históricos conservan sus fechas y alcance originales. [fuente original](../../licensing/2026-10-12-commercial-license-audit.md)

# MAKRIVO: Consultar por venta única comercial

Estado: 12. Octubre 2026. Fabricante: Aivana.

## Resultado

En el inventario auditado no se identificó ninguna obligación general de revelar el propio código fuente MAKRIVO. Parece posible una venta única por propiedad, pero aún no está completamente preparada. En particular, el permiso de redistribución para el Corsair-SDK-DLL existente no está documentado. Esta revisión no reemplaza la aprobación legal.

## Hallazgos técnicos y de licencia

| componente | Hallazgos | Paso requerido |
| --- | --- | --- |
| Código fuente propio | No se encontró ninguna LICENCIA, COPIA o AVISO propio en el inventario de Git examinado y en su historial de archivos de licencia disponibles localmente. | Confirmar la propiedad de los derechos, incluidas las contribuciones, los fragmentos del código adoptado y los posibles derechos del empleador; cree sus propios términos de uso. |
| Escritorio .NET/Windows | El paquete independiente examinado en `artifacts/makrivo-win-x64` contiene la versión 10.0.6. Los paquetes de ejecución local contienen licencias MIT que permiten la distribución comercial y requieren que se incluyan avisos. No se encontraron archivos de licencia/aviso correspondientes en las carpetas de salida examinadas. | Incluya textos de licencia exactos e información de terceros aplicable para el tiempo de ejecución realmente entregado en el paquete de ventas. |
| NuGet dependencias | No se encontraron entradas directas de PackageReference en los archivos del proyecto examinados. El archivo de recursos de escritorio que se examina contiene sus propias dependencias del proyecto. Los paquetes Runtime siguen siendo relevantes para la licencia. | Vuelva a inventariar todos los proyectos, paquetes transitivos y archivos empaquetados tras el lanzamiento final. |
| Corsair iCUE SDK | `artifacts/win-x64/iCUESDK.x64_2019.dll`, versión del archivo 4.0.84.0. El PDF de referencia del SDK local contiene en las páginas impresas 35–38 el EULA: uso libre de regalías, pero restricciones en transferencia/asignación. No había un permiso claro para pasar la DLL a los clientes. | Obtenga un permiso de redistribución sólido antes de agrupar. Alternativamente, marque un paquete sin una DLL de SDK que utilice un SDK instalado por el cliente; Las condiciones del SDK también se aplican a esto. |
| Diferentes paquetes | `artifacts/makrivo-win-x64` no contiene la DLL Corsair; `artifacts/win-x64` los contiene. La aplicación busca dinámicamente el SDK en varias ubicaciones. | Configure el instalador minorista real y verifique su contenido. La mera existencia de un paquete sin DLL no confirma todos los derechos de integración. |
| OBS | Propio cliente WebSocket en el código C# examinado; no se ha identificado ningún paquete de cliente OBS, binario OBS o código GPL adoptado. | La integración del protocolo observada no justifica en sí misma ninguna obligación de divulgación de la GPL establecida. Queda por confirmar el origen de los fragmentos que puedan haber sido adoptados. |
| Iconos de aplicaciones | Un script local Python dibuja símbolos geométricos usando Pillow. La herramienta es una dependencia de compilación. | Documente el origen y los derechos de todas las imágenes, sonidos, fuentes y otros activos realmente entregados. El guión por sí solo no prueba los derechos de cada activo. |
| Gestión de licencias | No se identifica ninguna verificación de activación/licencia de compra completa en el código de solicitud examinado. | Si lo desea, implemente archivos de licencia sin conexión firmados. La activación técnica y los términos de uso legales son tareas separadas. |

## Modelo de compra sensato – aún no implementado

- pago único; Derecho permanente de uso de la versión principal adquirida.
- Una persona en hasta dos de sus propias PC; Permitir expresamente el uso profesional.
- Incluya correcciones de errores y actualizaciones menores; Opcionalmente, ofrezca versiones principales posteriores mediante una tarifa.
- Archivo de licencia firmado digitalmente que se puede consultar sin conexión; Nunca entregue claves de firma privadas con la aplicación.
- Los derechos legales y las actualizaciones de seguridad requeridas no pueden quedar excluidos en todos los ámbitos por la regla de la versión principal.

## Abierto antes de la primera venta.

1. Corsair Aclare los derechos de distribución o prepárese para vender sin el SDK DLL incluido.
2. Incluya textos de licencia y de terceros para las dependencias de lanzamiento exactas y recíbalos en el instalador.
3. Confirmar la propiedad de los derechos sobre su propio código y activos; Cree EULA con reglas claras de dispositivo, usuario y actualización.
4. Para ventas al consumidor, gestionar el proceso de pago, incluidos los requisitos de información y la cancelación. Una declaración general de “no reembolsos por descargas” no es suficiente. La extinción del derecho de desistimiento si los bienes se ponen a disposición inmediatamente requiere los requisitos legales.
5. Vuelva a verificar el instalador terminado, incluidos los complementos y todas las dependencias transitivas. La copia de trabajo se amplía en paralelo; Este inventario no es una publicación de archivos futuros.

## GitHub

El repositorio examinado es público. Sin una licencia de código abierto, el código no queda automáticamente libre para ningún uso posterior. Aún se deben respetar los derechos de plataforma GitHub para repositorios públicos. Un cambio posterior a privado no elimina las copias ni las bifurcaciones existentes. La visibilidad y la licencia no cambiaron durante esta revisión.

## Evidencia y fuentes

- Licencias .NET locales: `C:/Users/weiss/.nuget/packages/microsoft.netcore.app.runtime.win-x64/10.0.6/LICENSE.TXT` y `C:/Users/weiss/.nuget/packages/microsoft.windowsdesktop.app.runtime.win-x64/10.0.6/LICENSE`.
- Avisos locales de terceros sobre .NET: `C:/Users/weiss/.nuget/packages/microsoft.netcore.app.runtime.win-x64/10.0.6/THIRD-PARTY-NOTICES.TXT`.
- SDK local Corsair: `artifacts/dependencies/iCUESDK_4.0.84.zip`; PDF de referencia extraído en `sdk/iCUESDK/doc/iCUE SDK - Overview and Reference.pdf`.
- [.NET 10.0.6 Licencia MIT](https://raw.githubusercontent.com/dotnet/runtime/v10.0.6/LICENSE.TXT)
- [Corsair Documentación SDK con EULA](https://raw.githubusercontent.com/CorsairOfficial/cue-sdk/master/docs/index.html)
- [GitHub: Licenciar los repositorios](https://docs.github.com/en/repositories/managing-your-repositorys-settings-and-features/customizing-your-repository/licensing-a-repository)
- [BGB § 327f – Actualizaciones](https://www.gesetze-im-internet.de/bgb/__327f.html)
- [BGB § 356 – Derecho de revocación](https://www.gesetze-im-internet.de/bgb/__356.html)

No marcado: historial completo de contribuciones externas y cadenas de derechos, liberación de marca registrada, pago de ventas existente, paquetes de complementos futuros, plena efectividad legal de un EULA aún no disponible.
