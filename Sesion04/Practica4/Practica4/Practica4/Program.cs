List<string> compras = ["Arroz","Leche","Leche","Frijol"];

Console.WriteLine($"Tienes antes de agregar {compras.Count} Elementos");
compras.Add("Café"); //Agrega a una lista
Console.WriteLine($"Tienes despues de agregar {compras.Count} Elementos");


Console.WriteLine(string.Join(", ",compras));
compras.Sort(); //ordena la lista
Console.WriteLine(string.Join(", ", compras));

// compras.Remove("Leche"); permite eliminar un elemento

Console.WriteLine("================================");
// crear un diccionario

Dictionary<string, string> agenda = new(StringComparer.OrdinalIgnoreCase);

agenda["Ana"] = "30055";
agenda["Luisa"] = "304043";

if(agenda.TryGetValue("Ana",out string ? telefono))
{
    Console.WriteLine($"Ana: {telefono}");
}

foreach(var (nombre,tel) in agenda)
{
    Console.WriteLine($"{nombre}, {tel}");
}



Console.WriteLine("==============");

//Diccionario con lista adentro 

Dictionary<string, List<double>> notas = new(StringComparer.OrdinalIgnoreCase);

notas["Usuario 1:"] = [];
notas["Usuario 1:"].Add(4.5);
notas["Usuario 1:"].Add(3.4);
notas["Usuario 1:"].Add(3);


foreach (var(nombre, nota)in notas)
{
    Console.WriteLine($"{nombre}: {string.Join("- ",nota)}");
}



Console.WriteLine("\n\n============================");

List<int> numeros = [10, 11, 15, 75, 53, 36, 45, 67, 56, 69];

// pares
var pares = numeros.Where(n => n % 2 == 0).ToList();
// top 3
var top3 = numeros.OrderByDescending(n => n).Take(3).ToList();
//promedio
Console.WriteLine($"El promedio es {numeros.Average()}");
Console.WriteLine(string.Join (", ",top3));
Console.WriteLine(string.Join(", ",pares));
Console.WriteLine($"Hay algun numero mayor a 80? {numeros.Any(n => n > 80)}");





