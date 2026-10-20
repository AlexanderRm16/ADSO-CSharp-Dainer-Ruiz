using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metodos_Arreglos_Cadenas
{
    internal class Program
    {
        static void Main(string[] args)
        {
            MostrarTitulo("Mis métodos");
            Console.WriteLine(Sumar(5, 3));
            Console.WriteLine(EsPar(7));

            static void MostrarTitulo(string texto)
            {
                Console.WriteLine($"=== {texto.ToUpper()} ===");
            }

            static int Sumar(int a, int b)
            {
                return a + b;
            }

            static bool EsPar(int numero) => numero % 2 == 0;
        }
    }
}
