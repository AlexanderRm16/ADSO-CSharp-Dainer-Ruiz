namespace Inventario.Escritorio;

public class FormProducto : Form
{
    private readonly TextBox txtCodigo = new() { Width = 200 };
    private readonly TextBox txtNombre = new() { Width = 200 };
    private readonly NumericUpDown nudPrecio = new() { Width = 200, Maximum = 1_000_000_000, DecimalPlaces = 2, ThousandsSeparator = true };
    private readonly NumericUpDown nudStock = new() { Width = 200, Maximum = 1_000_000 };
    private readonly ErrorProvider errores = new();

    public string Codigo => txtCodigo.Text.Trim();
    public string Nombre => txtNombre.Text.Trim();
    public decimal Precio => nudPrecio.Value;
    public int Stock => (int)nudStock.Value;

    public FormProducto()
    {
        Text = "Nuevo producto";
        FormBorderStyle = FormBorderStyle.FixedDialog;
        StartPosition = FormStartPosition.CenterParent;
        MaximizeBox = MinimizeBox = false;
        AutoSize = true;
        AutoSizeMode = AutoSizeMode.GrowAndShrink;

        var btnGuardar = new Button { Text = "Guardar", AutoSize = true };
        var btnCancelar = new Button { Text = "Cancelar", AutoSize = true, DialogResult = DialogResult.Cancel };
        btnGuardar.Click += Guardar;
        AcceptButton = btnGuardar;   
        CancelButton = btnCancelar;  

        var tabla = new TableLayoutPanel { AutoSize = true, ColumnCount = 2, Padding = new Padding(12) };
        void Fila(string etiqueta, Control c)
        {
            tabla.Controls.Add(new Label { Text = etiqueta, AutoSize = true, Anchor = AnchorStyles.Left, Margin = new Padding(3, 8, 8, 8) });
            tabla.Controls.Add(c);
        }
        Fila("Código:", txtCodigo);
        Fila("Nombre:", txtNombre);
        Fila("Precio:", nudPrecio);
        Fila("Stock inicial:", nudStock);

        var botones = new FlowLayoutPanel { AutoSize = true, FlowDirection = FlowDirection.RightToLeft };
        botones.Controls.AddRange(new Control[] { btnCancelar, btnGuardar });
        tabla.Controls.Add(new Label());
        tabla.Controls.Add(botones);

        Controls.Add(tabla);
    }

    private void Guardar(object? sender, EventArgs e)
    {
        errores.Clear();
        bool ok = true;

        if (string.IsNullOrWhiteSpace(txtCodigo.Text)) { errores.SetError(txtCodigo, "El código es obligatorio."); ok = false; }
        if (string.IsNullOrWhiteSpace(txtNombre.Text)) { errores.SetError(txtNombre, "El nombre es obligatorio."); ok = false; }
        if (nudPrecio.Value <= 0) { errores.SetError(nudPrecio, "El precio debe ser mayor que 0."); ok = false; }

        if (ok) DialogResult = DialogResult.OK;
    }
}
