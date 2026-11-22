using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Inventario.Core;
using Inventario.Interfaces;

namespace Inventario.Datos
{
    public class RepositorioJson : IRepositorioProducto
    {
        private readonly string _rutaArchivo;

        public RepositorioJson(string rutaArchivo = "inventario.json")
        {
            _rutaArchivo = rutaArchivo;
        }

        public List<Producto> ObtenerProductos()
        {
            if (!File.Exists(_rutaArchivo))
            {
                return new List<Producto>();
            }

            try
            {
                string json = File.ReadAllText(_rutaArchivo);
                if (string.IsNullOrWhiteSpace(json))
                {
                    return new List<Producto>();
                }

                return JsonSerializer.Deserialize<List<Producto>>(json) ?? new List<Producto>();
            }
            catch (Exception)
            {
                try
                {
                    string rutaDanada = _rutaArchivo + ".danado";
                    File.Copy(_rutaArchivo, rutaDanada, true);
                    File.Delete(_rutaArchivo);
                }
                catch { }

                return new List<Producto>();
            }
        }

        public void GuardarProductos(List<Producto> productos)
        {
            var opciones = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(productos, opciones);
            File.WriteAllText(_rutaArchivo, json);
        }
    }
}