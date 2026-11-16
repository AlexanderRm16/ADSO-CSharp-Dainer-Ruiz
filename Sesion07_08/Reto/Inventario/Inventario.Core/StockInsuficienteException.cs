using System;

namespace Inventario.Core
{
    public class StockInsuficienteException : Exception
    {
        public StockInsuficienteException(string mensaje) : base(mensaje)
        {
        }
    }
}