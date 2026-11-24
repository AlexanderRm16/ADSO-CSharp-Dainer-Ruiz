# Agenda de contactos (WinForms + SQLite)

Agenda de escritorio en C# con Entity Framework Core y SQLite.

## Requisitos
- .NET 8 SDK (Windows)

## Ejecutar
```
dotnet run
```
El primer arranque crea `agenda.db` junto al ejecutable; los datos persisten entre sesiones.

## Funciones
- Grilla ordenada por nombre y total en `lblTotal`
- Agregar con `ErrorProvider` (nombre obligatorio, correo con `@`)
- Eliminar con confirmación Sí/No
- Búsqueda en vivo por nombre
- Editar (extra): doble clic en la fila → modificar → *Guardar cambios*

## Estructura
| Archivo | Rol |
|---|---|
| `Contacto.cs` | Modelo |
| `AgendaContext.cs` | DbContext (SQLite) |
| `FormAgenda.cs` | Interfaz y lógica |
| `Program.cs` | Punto de entrada, crea la BD |
