namespace Reto;

public class Producto
{
    private string _nombre = "";
    private decimal _precio;
    private int _stock;
    public string Codigo { get; private set; }

    public string Nombre
    {
        get => _nombre;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("El nombre no puede estar vacío.");
            _nombre = value.Trim();
        }
    }
    public decimal Precio
    {
        get => _precio;
        set
        {
            if (value <= 0)
                throw new ArgumentException("El precio debe ser mayor que cero.");
            _precio = value;
        }
    }

    public int Stock => _stock;

    public Producto(string codigo, string nombre, decimal precio, int stock)
    {
        if (string.IsNullOrWhiteSpace(codigo))
            throw new ArgumentException("El código es obligatorio.");
        if (stock < 0)
            throw new ArgumentException("El stock no puede ser negativo.");

        Codigo = codigo.Trim().ToUpper();
        Nombre = nombre;  
        Precio = precio;   
        _stock = stock;
    }

    public void AgregarStock(int cantidad)
    {
        if (cantidad <= 0)
            throw new ArgumentException("La cantidad a agregar debe ser mayor que cero.");
        _stock += cantidad;
    }

    public bool Vender(int cantidad)
    {
        if (cantidad <= 0 || cantidad > _stock)
            return false;
        _stock -= cantidad;
        return true;
    }
}