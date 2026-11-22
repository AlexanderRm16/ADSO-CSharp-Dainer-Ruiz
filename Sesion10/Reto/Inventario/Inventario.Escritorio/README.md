# Inventario.Escritorio

Reto sesión 10: interfaz WinForms sobre las capas de la sesión 8 (Core, Interfaces, Datos, Negocio). Esas capas no se modificaron.

- Tabla con precio en moneda y stock bajo (< 5) resaltado
- Búsqueda en vivo por nombre o código
- Nuevo producto (`FormProducto`, validado)
- Vender / agregar stock con `NumericUpDown`
- Eliminar con confirmación
- Valor total del inventario siempre visible
- Extra: `MenuStrip` (Archivo → Salir, Ayuda → Acerca de) y `StatusStrip` (cantidad + hora)

Los datos se guardan en `inventario.json` junto al ejecutable.

Ejecutar: establecer `Inventario.Escritorio` como proyecto de inicio y pulsar F5.
