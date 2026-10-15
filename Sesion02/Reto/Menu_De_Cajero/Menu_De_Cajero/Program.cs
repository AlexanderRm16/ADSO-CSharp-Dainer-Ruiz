using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Menu_De_Cajero
{
    internal class Program
    {
        static void Main(string[] args)
        {
            double saldo_inicial = 500000;
            string pin = "1234";

            int i = 1;

            while (i <= 3)
            {
                Console.WriteLine("Digite el pin");
                string entrada = Console.ReadLine().Trim();

                if (entrada == pin)
                {
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.WriteLine("Acceso Concedido\n");
                    Console.ResetColor();
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine("CAJERO AUTOMATICO \n1. Consultar saldo \n2. Depositar \n3. Retirar \n4. Salir\n");

                    int opcion;
                    do
                    {
                        Console.WriteLine("Digite una opción: ");                   

                    } while (!int.TryParse(Console.ReadLine(), out opcion));
                    int movimientos = 0;
                    while (opcion != 4)
                    {
                        switch (opcion)
                        {
                            
                            case 1:
                                Console.WriteLine($"Su saldo es de {saldo_inicial}");
                                break;
                            case 2:
                                double deposito;
                                do
                                {
                                    Console.WriteLine("Ingrese cuanto desea depositar ");

                                } while (!double.TryParse(Console.ReadLine(), out deposito));
                                Console.WriteLine($"Su saldo anterior era de: {saldo_inicial}\n Ahora es de {saldo_inicial += deposito}");
                                movimientos += 1;
                                break;
                            case 3:
                                double retiro;
                                do
                                {
                                    Console.WriteLine("Ingrese cuanto desea retirar ");
                                } while (!double.TryParse(Console.ReadLine(), out retiro));

                                if (retiro <= 0)
                                {
                                    Console.WriteLine("El monto debe ser mayor a 0");
                                }
                                else if (retiro > saldo_inicial)
                                {
                                    Console.WriteLine("Saldo insuficiente");
                                }
                                else if (retiro % 10000 != 0)
                                {
                                    Console.WriteLine("Solo se permiten retiros múltiplos de 10000");
                                }
                                else
                                {
                                    saldo_inicial -= retiro;      
                                    movimientos++;                    
                                    Console.WriteLine($"¡Retiro exitoso!, retiraste {retiro}");
                                }
                                Console.WriteLine($"Saldo actual {saldo_inicial}\n");
                                break;



                                break;                      

                            default :
                                Console.WriteLine("Opcion no disponible");
                                break;

                        }

                        Console.WriteLine("CAJERO AUTOMATICO \n1. Consultar saldo \n2. Depositar \n3. Retirar \n4. Salir\n");

                 
                        do
                        {
                            Console.WriteLine("Digite una opción: ");

                        } while (!int.TryParse(Console.ReadLine(), out opcion));

                    }
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("sesión cerrada");
                    Console.ResetColor();
                    Console.WriteLine($"Movimientos totales: {movimientos}");



                    break;
                }

                i++;
                Console.WriteLine("PIN incorrecto");
            }
            if (i > 3)
                Console.WriteLine("Cuenta bloqueada");

        }
    }
}
