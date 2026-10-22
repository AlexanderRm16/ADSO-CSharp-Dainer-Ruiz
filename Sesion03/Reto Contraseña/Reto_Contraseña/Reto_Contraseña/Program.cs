using System;

namespace ValidadorContrasena
{
    internal class Program
    {
        static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.InputEncoding = System.Text.Encoding.UTF8;
            Contraseña();
            Console.ReadKey(); 
        }

        static void Contraseña()
        {
            string contraseña;
            bool esFuerte = false;

            do
            {
                Console.Write("Contraseña: ");
                contraseña = Console.ReadLine() ?? ""; 
                bool[] estado = Validar(contraseña);

                string[] nombreReglas =
                {
                    "Mínimo 8 caracteres",
                    "Una mayúscula",
                    "Una minúscula",
                    "Un número",
                    "Un carácter especial",
                    "Sin espacios"
                };

                int cumplidas = 0;

                for (int i = 0; i < estado.Length; i++)
                {
                    char icono = estado[i] ? '✓' : '✗';
                    Console.WriteLine($"{icono} {nombreReglas[i]}");
                    if (estado[i]) cumplidas++;
                }

                string nivel;
                if (cumplidas == 6)
                {
                    nivel = "Fuerte (6/6)"; 
                    esFuerte = true;
                }
                else if (cumplidas >= 4) nivel = $"Media ({cumplidas}/6)";
                else nivel = $"Débil ({cumplidas}/6)";

                Console.WriteLine($"Nivel: {nivel}");

                if (!esFuerte)
                    Console.WriteLine("La contraseña no es válida/segura\n");

            } while (!esFuerte);

            Console.WriteLine("\n¡Contraseña segura!");
        }

        static bool[] Validar(string contraseña)
        {
            bool min8Chars = contraseña.Length >= 8;
            bool mayuscula = false, minuscula = false, numero = false;
            bool caracterEspecial = false, sinEspacios = true;

            foreach (char c in contraseña)
            {
                if (char.IsUpper(c)) mayuscula = true;
                if (char.IsLower(c)) minuscula = true;
                if (char.IsDigit(c)) numero = true;
                if (c == ' ') sinEspacios = false;
                if (!char.IsLetterOrDigit(c) && c != ' ') caracterEspecial = true;
            }

            return new bool[] { min8Chars, mayuscula, minuscula, numero, caracterEspecial, sinEspacios };
        }
    }
}