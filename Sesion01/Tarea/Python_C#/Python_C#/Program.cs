using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Python_C_
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("====================================");
            Console.WriteLine("   ¡Bienvenido al Sistema ADSO!    ");
            Console.WriteLine("====================================");

            // Pedir datos al usuario
            Console.Write("Por favor, ingresa tu nombre: ");
            string nombre = Console.ReadLine();

            Console.Write("Ingresa tu ciudad de origen: ");
            string ciudad = Console.ReadLine();

            // Saludo personalizado mediante interpolación
            Console.WriteLine($"\n¡Hola, {nombre}! Nos alegra tenerte aquí desde {ciudad}.");
            Console.WriteLine("¡Mucho éxito en tu proceso de formación!");

            Console.WriteLine("\nPresiona cualquier tecla para finalizar...");
            Console.ReadKey();
        }
    }
}
