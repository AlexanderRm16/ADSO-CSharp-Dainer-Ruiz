using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adivina_el_numero
{
    internal class Program
    {
        static void Main(string[] args)
        {
            const int MaxIntentos = 7;
            Random rnd = new Random();
            int secreto = rnd.Next(1, 101);
            int intentos = 0;
            bool acertado = false;

            Console.WriteLine("Adivina el número (entre 1 y 100). Tienes 7 intentos.");

            while (intentos < MaxIntentos && !acertado)
            {
                Console.Write($"Intento {intentos + 1}/{MaxIntentos}: ");

                if (!int.TryParse(Console.ReadLine(), out int numero))
                {
                    Console.WriteLine("Escribe un número válido (no cuenta como intento).");
                    continue;
                }

                intentos++; 

                if (numero == secreto)
                {
                    acertado = true;
                }
                else if (secreto > numero)
                {
                    Console.WriteLine("El secreto es MAYOR.");
                }
                else
                {
                    Console.WriteLine("El secreto es MENOR.");
                }
            }

            if (acertado)
            {
                Console.WriteLine($"¡Acertaste en {intentos} intento(s)!");
            }
            else
            {
                Console.WriteLine($"Se acabaron los intentos. El número era {secreto}.");
            }
        }
    }
}
