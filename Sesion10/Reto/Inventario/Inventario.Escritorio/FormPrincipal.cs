using System.Globalization;
using Inventario.Core;
using Inventario.Datos;
using Inventario.Interfaces;
using Inventario.Negocio;

namespace Inventario.Escritorio;

public class FormPrincipal : Form
{
    private readonly ServicioInventario _servicio;

    private const int StockBajo = 5;                     
    private static readonly CultureInfo Cultura = new("es-CO");

    private readonly TextBox txtBuscar = new() { Dock = DockStyle.Fill, PlaceholderText = "Buscar por nombre o código..." };
    private readonly DataGridView dgvProductos = new();
    private readonly NumericUpDown nudCantidad = new() { Minimum = 1, Maximum = 100000, Value = 1, Width = 70 };
    private readonly Button btnVender = new() { Text = "Vender", AutoSize = true };
    private readonly Button btnAgregarStock = new() { Text = "Agregar stock", AutoSize = true };
    private readonly Button btnNuevo = new() { Text = "Nuevo producto", AutoSize = true };
    private readonly Button btnEliminar = new() { Text = "Eliminar", AutoSize = true };
    private readonly Label lblTotal = new() { AutoSize = true, Font = new Font("Segoe UI", 11, FontStyle.Bold), Margin = new Padding(20, 6, 3, 3) };
    private readonly ToolStripStatusLabel lblCantidad = new();
    private readonly ToolStripStatusLabel lblHora = new() { Spring = true, TextAlign = ContentAlignment.MiddleRight };
    private readonly System.Windows.Forms.Timer timerReloj = new() { Interval = 1000 };

    public FormPrincipal()
    {
        IRepositorioProducto repositorio = new RepositorioJson(Path.Combine(AppContext.BaseDirectory, "inventario.json"));
        _servicio = new ServicioInventario(repositorio);

        ConstruirInterfaz();
        ConectarEventos();
        CargarProductos();
    }

    private void ConstruirInterfaz()
    {
        Text = "Inventario de escritorio";
        Size = new Size(820, 560);
        StartPosition = FormStartPosition.CenterScreen;

        var menu = new MenuStrip();
        var salir = new ToolStripMenuItem("Salir");
        salir.Click += (_, _) => Close();
        var acerca = new ToolStripMenuItem("Acerca de");
        acerca.Click += (_, _) => MessageBox.Show("Inventario de escritorio\nWinForms sobre la arquitectura en capas de la sesión 8.",
                                                   "Acerca de", MessageBoxButtons.OK, MessageBoxIcon.Information);
        menu.Items.Add(new ToolStripMenuItem("Archivo", null, salir));
        menu.Items.Add(new ToolStripMenuItem("Ayuda", null, acerca));
        var status = new StatusStrip();
        status.Items.AddRange(new ToolStripItem[] { lblCantidad, lblHora });
        var panelBusqueda = new Panel { Dock = DockStyle.Top, Height = 38, Padding = new Padding(8) };
        panelBusqueda.Controls.Add(txtBuscar);
        var panelAcciones = new FlowLayoutPanel { Dock = DockStyle.Bottom, AutoSize = true, Padding = new Padding(8) };
        panelAcciones.Controls.AddRange(new Control[]
        {
            new Label { Text = "Cantidad:", AutoSize = true, Margin = new Padding(3, 8, 3, 3) },
            nudCantidad, btnVender, btnAgregarStock, btnNuevo, btnEliminar, lblTotal
        });

        dgvProductos.Dock = DockStyle.Fill;
        dgvProductos.ReadOnly = true;
        dgvProductos.AllowUserToAddRows = false;
        dgvProductos.MultiSelect = false;
        dgvProductos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgvProductos.RowHeadersVisible = false;
        dgvProductos.AutoGenerateColumns = false;
        dgvProductos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        dgvProductos.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = nameof(Producto.Codigo), HeaderText = "Código" });
        dgvProductos.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = nameof(Producto.Nombre), HeaderText = "Nombre", FillWeight = 200 });
        dgvProductos.Columns.Add(new DataGridViewTextBoxColumn
        {
            DataPropertyName = nameof(Producto.Precio),
            HeaderText = "Precio",
            DefaultCellStyle = new DataGridViewCellStyle { Format = "C0", FormatProvider = Cultura, Alignment = DataGridViewContentAlignment.MiddleRight }
        });
        dgvProductos.Columns.Add(new DataGridViewTextBoxColumn
        {
            DataPropertyName = nameof(Producto.Stock),
            HeaderText = "Stock",
            DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleRight }
        });

        Controls.AddRange(new Control[] { dgvProductos, panelAcciones, panelBusqueda, status, menu });
        MainMenuStrip = menu;
    }

    private void ConectarEventos()
    {
        txtBuscar.TextChanged += (_, _) => CargarProductos();     
        btnVender.Click += (_, _) => Vender();
        btnAgregarStock.Click += (_, _) => AgregarStock();
        btnNuevo.Click += (_, _) => Nuevo();
        btnEliminar.Click += (_, _) => Eliminar();
        dgvProductos.CellFormatting += ResaltarStockBajo;

        timerReloj.Tick += (_, _) => lblHora.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
        timerReloj.Start();
        lblHora.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
    }

    private void CargarProductos()
    {
        string? codigoSeleccionado = Seleccionado()?.Codigo; 

        List<Producto> lista = string.IsNullOrWhiteSpace(txtBuscar.Text)
            ? _servicio.ObtenerInventario()
            : _servicio.BuscarPorTexto(txtBuscar.Text);

        dgvProductos.DataSource = lista;

        if (codigoSeleccionado != null)
        {
            foreach (DataGridViewRow fila in dgvProductos.Rows)
            {
                if (((Producto)fila.DataBoundItem).Codigo == codigoSeleccionado)
                {
                    fila.Selected = true;
                    dgvProductos.CurrentCell = fila.Cells[0];
                    break;
                }
            }
        }

        decimal total = _servicio.ObtenerInventario().Sum(p => p.Precio * p.Stock);
        lblTotal.Text = $"Valor total: {total.ToString("C0", Cultura)}";
        lblCantidad.Text = $"Productos: {lista.Count}";
    }

    private Producto? Seleccionado() => dgvProductos.CurrentRow?.DataBoundItem as Producto;

    private void ResaltarStockBajo(object? sender, DataGridViewCellFormattingEventArgs e)
    {
        if (e.RowIndex < 0) return;
        if (dgvProductos.Rows[e.RowIndex].DataBoundItem is Producto p && p.Stock < StockBajo)
        {
            e.CellStyle!.BackColor = Color.MistyRose;
            e.CellStyle.ForeColor = Color.DarkRed;
            e.CellStyle.Font = new Font(dgvProductos.Font, FontStyle.Bold);
        }
    }

    private void Nuevo()
    {
        using var form = new FormProducto();
        if (form.ShowDialog(this) != DialogResult.OK) return;

        try
        {
            _servicio.Agregar(form.Codigo, form.Nombre, form.Precio, form.Stock);
            CargarProductos();
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "No se pudo agregar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }

    private void Vender()
    {
        if (Seleccionado() is not Producto producto) { MessageBox.Show("Selecciona un producto."); return; }
        try
        {
            _servicio.Vender(producto.Codigo, (int)nudCantidad.Value);
            CargarProductos();
        }
        catch (StockInsuficienteException ex) 
        {
            MessageBox.Show(ex.Message, "Stock insuficiente", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }

    private void AgregarStock()
    {
        if (Seleccionado() is not Producto producto) { MessageBox.Show("Selecciona un producto."); return; }
        try
        {
            _servicio.AgregarStock(producto.Codigo, (int)nudCantidad.Value);
            CargarProductos();
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void Eliminar()
    {
        if (Seleccionado() is not Producto producto) { MessageBox.Show("Selecciona un producto."); return; }

        var r = MessageBox.Show($"¿Eliminar \"{producto.Nombre}\"?", "Confirmar eliminación",
                                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        if (r != DialogResult.Yes) return;

        try
        {
            _servicio.Eliminar(producto.Codigo);
            CargarProductos();
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
