using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Login
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Ingrese su nombre: ");
            string user = Console.ReadLine().ToLower().Trim();

            Console.WriteLine("Ingrese su contraseña: ");
            string Pass = Console.ReadLine().ToLower().Trim();

            if (user == "admin" && Pass == "12345") {
                Console.WriteLine("Bienvenido al programa");
                Console.WriteLine("Calculadora con switch");
                Console.WriteLine("Digite un primer numero: ");
                int num1 = Convert.ToInt32(Console.ReadLine().Trim());
                Console.WriteLine("Digite un segundo numero: ");
                float num2 = Convert.ToInt32(Console.ReadLine().Trim());

                Console.WriteLine("Digite la opción deseada: ");
                Console.WriteLine("1. Sumar \n2. Restar \n3: Multiplicar \n4: Dividir \n5: Notas");

                int opcion = Convert.ToInt32(Console.ReadLine().Trim());

                switch (opcion)
                {
                    case 1:
                        Console.WriteLine($"La suma de {num1} + {num2} Es {num1 + num2}");
                        break;
                    case 2:
                        Console.WriteLine($"La resta de {num1} - {num2} Es {num1 - num2}");
                        break;
                    case 3:
                        Console.WriteLine($"La multiplicación de {num1} * {num2} Es {num1 * num2}");
                        break;
                    case 4:
                        if (num2 == 0)
                            Console.WriteLine("No es posible dividir entre 0");
                        else
                            Console.WriteLine($"La división de {num1} / {num2} Es {num1 / num2}");
                        break;

                    case 5:
                        double nota = Convert.ToDouble(Console.ReadLine().Trim());
                        if (nota < 0 || nota > 5)
                            Console.WriteLine("La nota debe ser entre 0.0 y 5.0");
                        else
                        {
                            if (nota < 3)
                                Console.WriteLine("Nota: Deficiente");

                            else if (nota < 4)

                                Console.WriteLine("Nota Aceptable");

                            else if (nota < 4.6)
                                Console.WriteLine("Nota sobresaliente");
                            else
                                Console.WriteLine("Nota Excelente");
                        }
                        break;

                    default :
                        Console.WriteLine("Opción no encontrada");
                        break;
                }

            }
            else
            {
                Console.WriteLine("Credenciales incorrectas");
            }
        }
    }
}
