namespace Reto;

public record LineaVenta(string Codigo, string Nombre, int Cantidad, decimal Subtotal);
public record Factura(int Numero, DateTime Fecha, List<LineaVenta> Lineas, decimal Total);
