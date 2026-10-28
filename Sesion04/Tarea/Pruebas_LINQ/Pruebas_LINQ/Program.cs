using System;
using System.Collections.Generic;
using System.Linq;

namespace Sesion04
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Venta> ventas =
            [
                new("Portátil", "Medellín", 2, 2_800_000m),
                new("Mouse", "Bogotá", 10, 45_000m),
                new("Teclado", "Cali", 5, 120_000m),
                new("Monitor", "Medellín", 3, 750_000m),
                new("Mouse", "Medellín", 8, 45_000m),
                new("Portátil", "Bogotá", 1, 2_800_000m),
                new("Diadema", "Cali", 6, 180_000m),
                new("Monitor", "Bogotá", 2, 750_000m),
                new("Teclado", "Medellín", 4, 120_000m),
                new("Diadema", "Bogotá", 3, 180_000m),
                new("Portátil", "Cali", 2, 2_800_000m),
                new("Mouse", "Cali", 12, 45_000m)
            ];

            decimal totalVendido = ventas.Sum(v => v.Cantidad * v.PrecioUnitario);
            Console.WriteLine($"=== TOTAL VENDIDO ===");
            Console.WriteLine($"${totalVendido:N0}\n");

            decimal promedioVenta = ventas.Average(v => v.Cantidad * v.PrecioUnitario);
            Console.WriteLine($"=== PROMEDIO POR VENTA ===");
            Console.WriteLine($"${promedioVenta:N2}\n");
            var ventasPorCiudad = ventas
                .GroupBy(v => v.Ciudad)
                .Select(g => new
                {
                    Ciudad = g.Key,
                    Total = g.Sum(v => v.Cantidad * v.PrecioUnitario)
                })
                .OrderByDescending(x => x.Total);

            Console.WriteLine($"=== VENTAS POR CIUDAD (MAYOR A MENOR) ===");
            foreach (var item in ventasPorCiudad)
            {
                Console.WriteLine($"- {item.Ciudad}: ${item.Total:N0}");
            }
            Console.WriteLine();
            var top3Productos = ventas
                .GroupBy(v => v.Producto)
                .Select(g => new
                {
                    Producto = g.Key,
                    Ingresos = g.Sum(v => v.Cantidad * v.PrecioUnitario)
                })
                .OrderByDescending(x => x.Ingresos)
                .Take(3);

            Console.WriteLine($"=== TOP 3 DE PRODUCTOS POR INGRESOS ===");
            int puesto = 1;
            foreach (var item in top3Productos)
            {
                Console.WriteLine($"{puesto}. {item.Producto}: ${item.Ingresos:N0}");
                puesto++;
            }
            Console.WriteLine();
            var productoMasVendido = ventas
                .GroupBy(v => v.Producto)
                .Select(g => new
                {
                    Producto = g.Key,
                    TotalUnidades = g.Sum(v => v.Cantidad)
                })
                .OrderByDescending(x => x.TotalUnidades)
                .First();

            Console.WriteLine($"=== PRODUCTO MÁS VENDIDO EN UNIDADES ===");
            Console.WriteLine($"- {productoMasVendido.Producto} con {productoMasVendido.TotalUnidades} unidades.");
        }
    }

    record Venta(string Producto, string Ciudad, int Cantidad, decimal PrecioUnitario);
}
