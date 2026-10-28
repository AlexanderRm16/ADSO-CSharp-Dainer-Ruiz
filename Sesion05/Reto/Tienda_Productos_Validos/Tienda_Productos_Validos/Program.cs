using Reto;

var inventario = new List<Producto>
{
    new("P001", "Camiseta", 45000m, 10),
    new("P002", "Gorra", 25000m, 5),
    new("P003", "Zapatillas", 180000m, 3)
};
var carrito = new List<LineaVenta>();
int consecutivo = 1;

bool salir = false;
while (!salir)
{
    Console.WriteLine("\n=== TIENDA ===");
    Console.WriteLine("1. Registrar producto");
    Console.WriteLine("2. Agregar stock");
    Console.WriteLine("3. Vender");
    Console.WriteLine("4. Ver carrito");
    Console.WriteLine("5. Ver inventario");
    Console.WriteLine("6. Confirmar compra (extra)");
    Console.WriteLine("0. Salir");
    Console.Write("Opción: ");

    try
    {
        switch (Console.ReadLine())
        {
            case "1": RegistrarProducto(); break;
            case "2": AgregarStock(); break;
            case "3": Vender(); break;
            case "4": VerCarrito(); break;
            case "5": VerInventario(); break;
            case "6": ConfirmarCompra(); break;
            case "0": salir = true; break;
            default: Console.WriteLine("Opción no válida."); break;
        }
    }
    catch (ArgumentException ex)
    {
        Console.WriteLine($"Error: {ex.Message}");
    }
}


void RegistrarProducto()
{
    string codigo = Pedir("Código: ").ToUpper();
    if (inventario.Any(p => p.Codigo == codigo))
    {
        Console.WriteLine("Ya existe un producto con ese código.");
        return;
    }
    string nombre = Pedir("Nombre: ");
    decimal precio = PedirDecimal("Precio: ");
    int stock = PedirEntero("Stock inicial: ");
    inventario.Add(new Producto(codigo, nombre, precio, stock));
    Console.WriteLine("Producto registrado.");
}

void AgregarStock()
{
    var producto = BuscarProducto();
    if (producto is null) return;
    producto.AgregarStock(PedirEntero("Cantidad a agregar: "));
    Console.WriteLine($"Nuevo stock de {producto.Nombre}: {producto.Stock}");
}

void Vender()
{
    var producto = BuscarProducto();
    if (producto is null) return;
    int cantidad = PedirEntero("Cantidad a vender: ");

    if (!producto.Vender(cantidad))
    {
        Console.WriteLine("No se pudo vender: cantidad inválida o stock insuficiente.");
        return;
    }
    carrito.Add(new LineaVenta(producto.Codigo, producto.Nombre, cantidad, producto.Precio * cantidad));
    Console.WriteLine("Agregado al carrito.");
}

void VerCarrito()
{
    if (carrito.Count == 0) { Console.WriteLine("El carrito está vacío."); return; }
    foreach (var l in carrito)
        Console.WriteLine($"{l.Codigo} | {l.Nombre} | x{l.Cantidad} | {l.Subtotal:C}");
    Console.WriteLine($"TOTAL: {carrito.Sum(l => l.Subtotal):C}");
}

void VerInventario()
{
    foreach (var p in inventario)
        Console.WriteLine($"{p.Codigo} | {p.Nombre} | {p.Precio:C} | Stock: {p.Stock}");
}
void ConfirmarCompra()
{
    if (carrito.Count == 0) { Console.WriteLine("El carrito está vacío."); return; }

    var factura = new Factura(consecutivo++, DateTime.Now, new List<LineaVenta>(carrito), carrito.Sum(l => l.Subtotal));

    Console.WriteLine("\n========== RECIBO ==========");
    Console.WriteLine($"Factura N° {factura.Numero:D4}");
    Console.WriteLine($"Fecha: {factura.Fecha:dd/MM/yyyy HH:mm}");
    Console.WriteLine("----------------------------");
    foreach (var l in factura.Lineas)
        Console.WriteLine($"{l.Nombre} x{l.Cantidad}  {l.Subtotal:C}");
    Console.WriteLine("----------------------------");
    Console.WriteLine($"TOTAL: {factura.Total:C}");
    Console.WriteLine("============================");

    carrito.Clear();
}


Producto? BuscarProducto()
{
    string codigo = Pedir("Código del producto: ").ToUpper();
    var producto = inventario.FirstOrDefault(p => p.Codigo == codigo);
    if (producto is null) Console.WriteLine("Producto no encontrado.");
    return producto;
}

string Pedir(string mensaje)
{
    Console.Write(mensaje);
    return Console.ReadLine() ?? "";
}

int PedirEntero(string mensaje)
{
    return int.TryParse(Pedir(mensaje), out int n) ? n : -1;
}

decimal PedirDecimal(string mensaje)
{
    return decimal.TryParse(Pedir(mensaje), out decimal d) ? d : 0;
}
