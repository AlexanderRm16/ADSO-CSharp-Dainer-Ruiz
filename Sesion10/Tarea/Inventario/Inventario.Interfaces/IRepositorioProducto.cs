using System.Collections.Generic;
using Inventario.Core;

namespace Inventario.Interfaces
{
    public interface IRepositorioProducto
    {
        List<Producto> ObtenerProductos();
        void GuardarProductos(List<Producto> productos);
    }
}