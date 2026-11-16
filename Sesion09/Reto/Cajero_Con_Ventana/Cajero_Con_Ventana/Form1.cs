using Cajero_Con_Ventana;

namespace Reto
{
    public partial class Form1 : Form
{
    private readonly CuentaBancaria _cuenta = new("Alexander", "001-123", 500000m);
    private readonly CuentaBancaria _destino = new("Ana Gómez", "002-456", 0m); 

    public Form1()
    {
        InitializeComponent();
        ActualizarSaldo();
    }

    private void ActualizarSaldo()
    {
        lblTitular.Text = $"Titular: {_cuenta.Titular} ({_cuenta.Numero})";
        lblSaldo.Text = $"Saldo: {_cuenta.Saldo:C0}";
        lblDestino.Text = $"Cuenta destino: {_destino.Titular} ({_destino.Numero}) - Saldo: {_destino.Saldo:C0}";
    }

    private void Ejecutar(string tipo, Action<decimal> operacion)
    {
        decimal monto = nudMonto.Value;

        try
        {
            operacion(monto);
            lstMovimientos.Items.Insert(0, $"{DateTime.Now:HH:mm:ss} | {tipo} | {monto:C0}");
        }
        catch (Exception ex) when (ex is ArgumentException or InvalidOperationException)
        {
            MessageBox.Show(ex.Message, "Operación no permitida",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        ActualizarSaldo();
    }

    private void btnDepositar_Click(object? sender, EventArgs e)
        => Ejecutar("Depósito", _cuenta.Depositar);

    private void btnRetirar_Click(object? sender, EventArgs e)
        => Ejecutar("Retiro", _cuenta.Retirar);

    private void btnTransferir_Click(object? sender, EventArgs e)
        => Ejecutar("Transferencia", monto => _cuenta.Transferir(_destino, monto));
}
}