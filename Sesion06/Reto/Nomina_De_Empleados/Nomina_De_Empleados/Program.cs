using System.Globalization;

CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("es-CO");
CultureInfo.CurrentCulture = new CultureInfo("es-CO");

List<Empleado> nomina =
[
    new EmpleadoPlanta("Ana Gómez", "1001", 3_200_000m, 6),
    new EmpleadoPlanta("Luis Pérez", "1002", 2_800_000m, 2),
    new EmpleadoPorHoras("Marta Ruiz", "1003", 170, 18_000m),
    new EmpleadoPorHoras("Jorge Díaz", "1004", 120, 18_000m),
    new Contratista("Sofía León", "1005", 4_000_000m)
];

foreach (var e in nomina)
    Console.WriteLine(e);

Console.WriteLine($"\nTotal nómina: {nomina.Sum(e => e.CalcularSalario()):C0}");

var mayor = nomina.MaxBy(e => e.CalcularSalario())!;
Console.WriteLine($"Mayor pago: {mayor.Nombre} - {mayor.CalcularSalario():C0}");

Console.WriteLine("\nTotal por tipo:");
foreach (var g in nomina.GroupBy(e => e.Tipo))
    Console.WriteLine($"  {g.Key}: {g.Sum(e => e.CalcularSalario()):C0}");

decimal totalBonos = nomina.OfType<IBonificable>().Sum(b => b.CalcularBono());
Console.WriteLine($"\nTotal bonos: {totalBonos:C0}");

Console.ReadKey();