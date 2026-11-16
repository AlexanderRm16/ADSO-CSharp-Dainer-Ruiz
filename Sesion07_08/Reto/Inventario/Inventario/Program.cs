using Inventario.Core;
using Inventario.Datos;
using Inventario.Interfaces;
using Inventario.Negocio;
using System;

class Program
{
    static void Main(string[] args)
    {
        IRepositorioProducto repositorio = new RepositorioJson("inventario.json");
        ServicioInventario servicio = new ServicioInventario(repositorio);

        bool salir = false;

        while (!salir)
        {
            Console.Clear();
            Console.WriteLine("=== MENÚ INVENTARIO EN CAPAS ===");
            Console.WriteLine("1. Registrar producto");
            Console.WriteLine("2. Agregar stock");
            Console.WriteLine("3. Vender producto");
            Console.WriteLine("4. Ver inventario");
            Console.WriteLine("5. Ver stock bajo (< 5 unidades)");
            Console.WriteLine("6. Buscar producto");
            Console.WriteLine("7. Eliminar producto");
            Console.WriteLine("8. Salir");
            Console.Write("\nSeleccione una opción: ");

            try
            {
                string opcion = Console.ReadLine();
                Console.WriteLine();

                switch (opcion)
                {
                    case "1":
                        Console.Write("Ingrese código: ");
                        string codigoReg = Console.ReadLine();
                        Console.Write("Ingrese nombre: ");
                        string nombre = Console.ReadLine();
                        Console.Write("Ingrese precio: ");
                        decimal precio = decimal.Parse(Console.ReadLine());
                        Console.Write("Ingrese stock inicial: ");
                        int stock = int.Parse(Console.ReadLine());

                        servicio.Agregar(codigoReg, nombre, precio, stock);
                        Console.WriteLine("\n¡Producto registrado con éxito!");
                        break;

                    case "2":
                        Console.Write("Ingrese código del producto: ");
                        string codigoStock = Console.ReadLine();
                        Console.Write("Ingrese cantidad a sumar: ");
                        int cantidadStock = int.Parse(Console.ReadLine());

                        servicio.AgregarStock(codigoStock, cantidadStock);
                        Console.WriteLine("\n¡Stock actualizado con éxito!");
                        break;

                    case "3":
                        Console.Write("Ingrese código del producto a vender: ");
                        string codigoVenta = Console.ReadLine();
                        Console.Write("Ingrese cantidad a vender: ");
                        int cantidadVenta = int.Parse(Console.ReadLine());

                        servicio.Vender(codigoVenta, cantidadVenta);
                        Console.WriteLine("\n¡Venta realizada con éxito!");
                        break;

                    case "4":
                        var productos = servicio.ObtenerInventario();
                        Console.WriteLine("--- LISTA DE INVENTARIO ---");
                        if (productos.Count == 0)
                        {
                            Console.WriteLine("El inventario está vacío.");
                        }
                        else
                        {
                            foreach (var p in productos)
                            {
                                Console.WriteLine($"Código: {p.Codigo} | Nombre: {p.Nombre} | Precio: ${p.Precio} | Stock: {p.Stock}");
                            }
                        }
                        break;

                    case "5":
                        var stockBajo = servicio.ObtenerStockBajo();
                        Console.WriteLine("--- PRODUCTOS CON STOCK BAJO (< 5) ---");
                        if (stockBajo.Count == 0)
                        {
                            Console.WriteLine("No hay productos con stock bajo.");
                        }
                        else
                        {
                            foreach (var p in stockBajo)
                            {
                                Console.WriteLine($"Código: {p.Codigo} | Nombre: {p.Nombre} | Stock: {p.Stock}");
                            }
                        }
                        break;

                    case "6":
                        Console.Write("Ingrese texto a buscar (nombre o código): ");
                        string texto = Console.ReadLine();

                        var resultados = servicio.BuscarPorTexto(texto);
                        Console.WriteLine("\n--- RESULTADOS DE LA BÚSQUEDA ---");
                        if (resultados.Count == 0)
                        {
                            Console.WriteLine("No se encontraron productos.");
                        }
                        else
                        {
                            foreach (var r in resultados)
                            {
                                Console.WriteLine($"Código: {r.Codigo} | Nombre: {r.Nombre} | Precio: ${r.Precio} | Stock: {r.Stock}");
                            }
                        }
                        break;

                    case "7":
                        Console.Write("Ingrese código del producto a eliminar: ");
                        string codigoEliminar = Console.ReadLine();
                        var aEliminar = servicio.ObtenerInventario().FirstOrDefault(x =>
                            string.Equals(x.Codigo, codigoEliminar?.Trim(), StringComparison.OrdinalIgnoreCase));

                        if (aEliminar == null)
                        {
                            Console.WriteLine("\nNo se encontró ningún producto con ese código.");
                            break;
                        }

                        Console.Write($"¿Eliminar \"{aEliminar.Nombre}\"? (s/n): ");
                        string confirmacion = Console.ReadLine();

                        if (confirmacion?.Trim().ToLower() == "s")
                        {
                            servicio.Eliminar(codigoEliminar);
                            Console.WriteLine("\n¡Producto eliminado con éxito!");
                        }
                        else
                        {
                            Console.WriteLine("\nEliminación cancelada.");
                        }
                        break;

                    case "8":
                        salir = true;
                        Console.WriteLine("Saliendo del programa...");
                        break;

                    default:
                        Console.WriteLine("Opción no válida. Intente de nuevo.");
                        break;
                }
            }
            catch (StockInsuficienteException ex)
            {
                Console.WriteLine($"\n[AVISO DE STOCK]: {ex.Message}");
            }
            catch (Exception ex) when (ex is FormatException or OverflowException)
            {
                // int.Parse / decimal.Parse con texto o números demasiado grandes
                Console.WriteLine("\n[ERROR]: Ingrese un número válido.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n[ERROR]: {ex.Message}");
            }

            if (!salir)
            {
                Console.WriteLine("\nPresione cualquier tecla para continuar...");
                Console.ReadKey();
            }
        }
    }
}