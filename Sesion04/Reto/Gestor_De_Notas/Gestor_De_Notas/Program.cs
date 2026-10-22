using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
class Program
{
    static Dictionary<string, List<double>> notas =
        new Dictionary<string, List<double>>(StringComparer.OrdinalIgnoreCase);

    const double Aprueba = 3.0;

    static void Main()
    {
        CultureInfo.CurrentCulture = new CultureInfo("es-CO");

        bool salir = false;
        while (!salir)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\n=== GESTOR DE NOTAS ===");
            Console.WriteLine("1. Registrar aprendiz");
            Console.WriteLine("2. Agregar nota");
            Console.WriteLine("3. Listar todos");
            Console.WriteLine("4. Ranking");
            Console.WriteLine("5. Mejor aprendiz y cuántos aprobaron");
            Console.WriteLine("6. Eliminar aprendiz");
            Console.WriteLine("7. Salir");
            Console.WriteLine("8. Reporte");
            Console.Write("Opción: ");
            Console.ResetColor();

            switch (Console.ReadLine())
            {

                case "1": RegistrarAprendiz(); break;
                case "2": AgregarNota(); break;
                case "3": ListarTodos(); break;
                case "4": Ranking(); break;
                case "5": MejorYAprobados(); break;
                case "6": EliminarAprendiz(); break;
                case "7": salir = true; break;
                case "8": Reporte(); break;
                default: Console.WriteLine("Opción inválida."); break;
            }

        }
    }

    static void RegistrarAprendiz()
    {
        Console.Write("Nombre: ");
        string nombre = (Console.ReadLine() ?? "").Trim();

        if (nombre == "") { Console.WriteLine("El nombre no puede estar vacío."); return; }

        if (notas.ContainsKey(nombre)) { Console.WriteLine("Ese aprendiz ya existe."); return; }

        notas[nombre] = new List<double>();
        Console.WriteLine($"{nombre} registrado.");
    }

    static void AgregarNota()
    {
        Console.Write("Nombre: ");
        string nombre = (Console.ReadLine() ?? "").Trim();

        if (!notas.TryGetValue(nombre, out List<double> lista))
        {
            Console.WriteLine("Ese aprendiz no existe.");
            return;
        }

        Console.Write("Nota (0 a 5): ");
        if (!double.TryParse(Console.ReadLine(), out double nota) || nota < 0 || nota > 5)
        {
            Console.WriteLine("Nota inválida. Debe ser un número entre 0 y 5.");
            return;
        }
        lista.Add(nota);
        Console.WriteLine("Nota agregada.");
    }

    static void ListarTodos()
    {
        if (notas.Count == 0) { Console.WriteLine("No hay aprendices."); return; }

        foreach (var a in notas)
            Console.WriteLine(Linea(a));
    }

    static void Ranking()
    {
        var ranking = notas
            .Where(a => a.Value.Count > 0)
            .OrderByDescending(a => a.Value.Average());

        if (!ranking.Any()) { Console.WriteLine("Nadie tiene notas todavía."); return; }

        int puesto = 1;
        foreach (var a in ranking)
            Console.WriteLine($"{puesto++}. {Linea(a)}");
    }
    static void MejorYAprobados()
    {
        var conNotas = notas.Where(a => a.Value.Count > 0).ToList();
        if (conNotas.Count == 0) { Console.WriteLine("Nadie tiene notas todavía."); return; }
        var mejor = conNotas.OrderByDescending(a => a.Value.Average()).First();

        int aprobados = conNotas.Count(a => a.Value.Average() >= Aprueba);

        Console.WriteLine($"Mejor aprendiz: {mejor.Key} (promedio {mejor.Value.Average():F1})");
        Console.WriteLine($"Aprobaron: {aprobados} de {conNotas.Count}");
    }
    static void EliminarAprendiz()
    {
        Console.Write("Nombre: ");
        string nombre = (Console.ReadLine() ?? "").Trim();

        Console.WriteLine(notas.Remove(nombre) ? "Aprendiz eliminado." : "Ese aprendiz no existe.");
    }
    static void Reporte()
    {
        string[] orden = { "Excelente", "Sobresaliente", "Aceptable", "Deficiente" };
        var grupos = notas
            .Where(a => a.Value.Count > 0)
            .GroupBy(a => Nivel(a.Value.Average()))
            .OrderBy(g => Array.IndexOf(orden, g.Key));

        if (!grupos.Any()) { Console.WriteLine("Nadie tiene notas todavía."); return; }

        foreach (var g in grupos)
        {
            Console.WriteLine($"\n{g.Key} ({g.Count()}):");
            foreach (var a in g)
                Console.WriteLine($"  - {a.Key} ({a.Value.Average():F1})");
        }
    }
    static string Linea(KeyValuePair<string, List<double>> a)
    {
        if (a.Value.Count == 0)
            return $"{a.Key,-15} 0 notas · Sin promedio";

        double prom = a.Value.Average();
        string juicio = prom >= Aprueba ? "A" : "D"; 
        return $"{a.Key,-15} {a.Value.Count} notas · Promedio {prom:F1} · Juicio {juicio}";
    }

    static string Nivel(double promedio)
    {
        if (promedio >= 4.5) return "Excelente";
        if (promedio >= 4.0) return "Sobresaliente";
        if (promedio >= Aprueba) return "Aceptable";
        return "Deficiente";
    }
}