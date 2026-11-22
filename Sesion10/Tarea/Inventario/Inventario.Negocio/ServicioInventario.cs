using System;
using System.Collections.Generic;
using System.Linq;
using Inventario.Core;
using Inventario.Interfaces;

namespace Inventario.Negocio
{
    public class ServicioInventario
    {
        private readonly IRepositorioProducto _repositorio;

        public ServicioInventario(IRepositorioProducto repositorio)
        {
            _repositorio = repositorio;
        }

        public List<Producto> ObtenerInventario()
        {
            return _repositorio.ObtenerProductos();
        }

        public void Agregar(string codigo, string nombre, decimal precio, int stockInicial)
        {
           
            if (string.IsNullOrWhiteSpace(codigo)) throw new ArgumentException("El código no puede estar vacío.");
            if (string.IsNullOrWhiteSpace(nombre)) throw new ArgumentException("El nombre no puede estar vacío.");
            if (precio < 0) throw new ArgumentException("El precio no puede ser negativo.");
            if (stockInicial < 0) throw new ArgumentException("El stock inicial no puede ser negativo.");

            codigo = codigo.Trim();
            var productos = _repositorio.ObtenerProductos();

            if (productos.Any(p => string.Equals(p.Codigo, codigo, StringComparison.OrdinalIgnoreCase)))
            {
                throw new InvalidOperationException("Ya existe un producto registrado con ese código.");
            }

            productos.Add(new Producto
            {
                Codigo = codigo,
                Nombre = nombre,
                Precio = precio,
                Stock = stockInicial
            });

            _repositorio.GuardarProductos(productos);
        }

        public void AgregarStock(string codigo, int cantidad)
        {
            if (cantidad <= 0) throw new ArgumentException("La cantidad debe ser mayor que cero.");

            var productos = _repositorio.ObtenerProductos();
            var producto = productos.FirstOrDefault(p => string.Equals(p.Codigo, codigo?.Trim(), StringComparison.OrdinalIgnoreCase));

            if (producto == null)
            {
                throw new KeyNotFoundException("No se encontró ningún producto con ese código.");
            }

            producto.Stock += cantidad;
            _repositorio.GuardarProductos(productos);
        }

        public void Vender(string codigo, int cantidadVenta)
        {
            if (cantidadVenta <= 0) throw new ArgumentException("La cantidad debe ser mayor que cero.");

            var productos = _repositorio.ObtenerProductos();
            var producto = productos.FirstOrDefault(p => string.Equals(p.Codigo, codigo?.Trim(), StringComparison.OrdinalIgnoreCase));

            if (producto == null)
            {
                throw new KeyNotFoundException("No se encontró ningún producto con ese código.");
            }

            if (producto.Stock < cantidadVenta)
            {
                throw new StockInsuficienteException($"Stock insuficiente. Stock actual: {producto.Stock}, solicitado: {cantidadVenta}.");
            }

            producto.Stock -= cantidadVenta;
            _repositorio.GuardarProductos(productos);
        }

        public List<Producto> ObtenerStockBajo()
        {
            var productos = _repositorio.ObtenerProductos();
            return productos.Where(p => p.Stock < 5).ToList();
        }
        public List<Producto> BuscarPorTexto(string texto)
        {
            if (string.IsNullOrWhiteSpace(texto))
                throw new ArgumentException("El texto de búsqueda no puede estar vacío.");

            texto = texto.Trim();

            return _repositorio.ObtenerProductos()
                .Where(p => (p.Nombre?.Contains(texto, StringComparison.OrdinalIgnoreCase) ?? false)
                         || (p.Codigo?.Contains(texto, StringComparison.OrdinalIgnoreCase) ?? false))
                .ToList();
        }

        public void Eliminar(string codigo)
        {
            var productos = _repositorio.ObtenerProductos();
            var producto = productos.FirstOrDefault(p =>
                string.Equals(p.Codigo, codigo?.Trim(), StringComparison.OrdinalIgnoreCase));

            if (producto == null)
                throw new KeyNotFoundException("No se encontró ningún producto con ese código.");

            productos.Remove(producto);
            _repositorio.GuardarProductos(productos);
        }

        public void Actualizar(string codigo, string nombre, decimal precio)
        {
            if (string.IsNullOrWhiteSpace(nombre)) throw new ArgumentException("El nombre no puede estar vacío.");
            if (precio < 0) throw new ArgumentException("El precio no puede ser negativo.");

            var productos = _repositorio.ObtenerProductos();
            var producto = productos.FirstOrDefault(p =>
                string.Equals(p.Codigo, codigo?.Trim(), StringComparison.OrdinalIgnoreCase));

            if (producto == null)
                throw new KeyNotFoundException("No se encontró ningún producto con ese código.");

            producto.Nombre = nombre.Trim();
            producto.Precio = precio;
            _repositorio.GuardarProductos(productos);
        }
    }
}