using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tarjeta_Consola
{
    internal class Program
    {
        static void Main(string[] args)
        {

            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Console.WriteLine("=== REGISTRO DE DATOS SENA ===");

            Console.Write("Ingresa tu nombre completo: ");
            string nombre = Console.ReadLine();

            Console.Write("Ingresa tu programa de formación: ");
            string programa = Console.ReadLine();

            Console.Write("Ingresa tu número de ficha: ");
            string ficha = Console.ReadLine();

            Console.Write("Ingresa una frase que te represente: ");
            string frase = Console.ReadLine();

            // Limpiamos la pantalla para mostrar la tarjeta de forma limpia
            Console.Clear();

            Console.BackgroundColor = ConsoleColor.DarkBlue;
            Console.ForegroundColor = ConsoleColor.White;
            Console.Clear(); // Aplica el fondo a toda la ventana de la consola

            Console.WriteLine("\n");
            Console.WriteLine("  ╔════════════════════════════════════════════════════════════╗");
            Console.WriteLine("  ║                  TARJETA DE IDENTIFICACIÓN                 ║");
            Console.WriteLine("  ╠════════════════════════════════════════════════════════════╣");
            // Usamos interpolación de cadenas ($) para insertar las variables dentro de la tarjeta
            Console.WriteLine($"  ║  Nombre:   {nombre,-47} ║");
            Console.WriteLine($"  ║  Programa: {programa,-47} ║");
            Console.WriteLine($"  ║  Ficha:    {ficha,-47} ║");
            Console.WriteLine("  ╠════════════════════════════════════════════════════════════╣");
            Console.WriteLine($"  ║  Frase: \"{frase,-45}  ║");
            Console.WriteLine("  ╚════════════════════════════════════════════════════════════╝");
            Console.WriteLine("\n");

            // Pausa para que la consola no se cierre inmediatamente al ejecutar en Visual Studio
            Console.WriteLine("Presiona cualquier tecla para salir...");
            Console.ReadKey();
        }
    }
}
