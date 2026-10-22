using System;
using System.Text;

namespace Tarea
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Nombre completo: ");
            string nombre = Console.ReadLine() ?? "";
            Console.Write("Documento: ");
            string documento = Console.ReadLine() ?? "";
            Console.WriteLine($"Usuario: {GenerarUsuario(nombre, documento)}");

            Console.Write("\nEscribe una frase: ");
            string frase = Console.ReadLine() ?? "";
            AnalizarFrase(frase);
        }

        static string GenerarUsuario(string nombreCompleto, string documento)
        {
            string[] partes = nombreCompleto.Trim()
                .Split(' ', StringSplitOptions.RemoveEmptyEntries);

            if (partes.Length < 2 || documento.Length < 2)
                return "Datos insuficientes";

            string primerApellido = partes.Length >= 4 ? partes[2] : partes[1];

            string usuario = partes[0][0] + primerApellido
                           + documento.Substring(documento.Length - 2);

            return QuitarTildes(usuario.ToLower());
        }
        static string QuitarTildes(string texto)
        {
            return texto
                .Replace("á", "a")
                .Replace("é", "e")
                .Replace("í", "i")
                .Replace("ó", "o")
                .Replace("ú", "u")
                .Replace("ü", "u")
                .Replace("ñ", "n");
        }

        static void AnalizarFrase(string frase)
        {
            string[] palabras = frase.Trim()
                .Split(' ', StringSplitOptions.RemoveEmptyEntries);

            StringBuilder invertida = new StringBuilder();
            for (int i = palabras.Length - 1; i >= 0; i--)
            {
                invertida.Append(palabras[i]);
                if (i > 0) invertida.Append(' ');
            }

            Console.WriteLine($"Cantidad de palabras: {palabras.Length}");
            Console.WriteLine($"Frase invertida: {invertida}");
        }
    }
}