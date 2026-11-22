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

    // --- Controles ---
    private readonly TextBox txtBuscar = new() { Dock = DockStyle.Fill, PlaceholderText = "Buscar por nombre o código..." };
    private readonly DataGridView dgvProductos = new();
    private readonly NumericUpDown nudCantidad = new() { Minimum = 1, Maximum = 100000, Value = 1, Width = 70 };
    private readonly Button btnVender = new() { Text = "Vender", AutoSize = true };
    private readonly Button btnAgregarStock = new() { Text = "Agregar stock", AutoSize = true };
    private readonly Button btnNuevo = new() { Text = "Nuevo producto", AutoSize = true };
    private readonly Button btnEliminar = new() { Text = "Eliminar", AutoSize = true };
    private readonly Button btnReportes = new() { Text = "Reportes", AutoSize = true };
    private readonly Label lblTotal = new() { AutoSize = true, Font = new Font("Segoe UI", 11, FontStyle.Bold), Margin = new Padding(20, 6, 3, 3) };
    private readonly ToolStripStatusLabel lblCantidad = new();
    private readonly ToolStripStatusLabel lblHora = new() { Spring = true, TextAlign = ContentAlignment.MiddleRight };
    private readonly System.Windows.Forms.Timer timerReloj = new() { Interval = 1000 };

    public FormPrincipal()
    {
        // La ruta absoluta evita que el JSON cambie de lugar según desde dónde se ejecute
        IRepositorioProducto repositorio = new RepositorioJson(Path.Combine(AppContext.BaseDirectory, "inventario.json"));
        _servicio = new ServicioInventario(repositorio);

        ConstruirInterfaz();
        ConectarEventos();
        CargarProductos();
    }

    // ---------- Interfaz (construida por código para que sea fácil de copiar) ----------
    private void ConstruirInterfaz()
    {
        Text = "Inventario de escritorio";
        Size = new Size(820, 560);
        StartPosition = FormStartPosition.CenterScreen;

        // Menú (nivel extra): Archivo → Salir, Ayuda → Acerca de
        var menu = new MenuStrip();
        var salir = new ToolStripMenuItem("Salir");
        salir.Click += (_, _) => Close();
        var acerca = new ToolStripMenuItem("Acerca de");
        acerca.Click += (_, _) => MessageBox.Show("Inventario de escritorio\nWinForms sobre la arquitectura en capas de la sesión 8.",
                                                   "Acerca de", MessageBoxButtons.OK, MessageBoxIcon.Information);
        menu.Items.Add(new ToolStripMenuItem("Archivo", null, salir));
        menu.Items.Add(new ToolStripMenuItem("Ayuda", null, acerca));

        // Barra de estado (nivel extra): cantidad de productos + hora
        var status = new StatusStrip();
        status.Items.AddRange(new ToolStripItem[] { lblCantidad, lblHora });

        // Buscador arriba
        var panelBusqueda = new Panel { Dock = DockStyle.Top, Height = 38, Padding = new Padding(8) };
        panelBusqueda.Controls.Add(txtBuscar);

        // Acciones abajo
        var panelAcciones = new FlowLayoutPanel { Dock = DockStyle.Bottom, AutoSize = true, Padding = new Padding(8) };
        panelAcciones.Controls.AddRange(new Control[]
        {
            new Label { Text = "Cantidad:", AutoSize = true, Margin = new Padding(3, 8, 3, 3) },
            nudCantidad, btnVender, btnAgregarStock, btnNuevo, btnEliminar, btnReportes, lblTotal
        });

        // Tabla
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
            // "C0" = moneda sin decimales, con la cultura es-CO
            DefaultCellStyle = new DataGridViewCellStyle { Format = "C0", FormatProvider = Cultura, Alignment = DataGridViewContentAlignment.MiddleRight }
        });
        dgvProductos.Columns.Add(new DataGridViewTextBoxColumn
        {
            DataPropertyName = nameof(Producto.Stock),
            HeaderText = "Stock",
            DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleRight }
        });

        // El control agregado primero (Fill) ocupa lo que dejan los demás; el último agregado queda más afuera
        Controls.AddRange(new Control[] { dgvProductos, panelAcciones, panelBusqueda, status, menu });
        MainMenuStrip = menu;
    }

    private void ConectarEventos()
    {
        txtBuscar.TextChanged += (_, _) => CargarProductos();      // búsqueda en vivo
        btnVender.Click += (_, _) => Vender();
        btnAgregarStock.Click += (_, _) => AgregarStock();
        btnNuevo.Click += (_, _) => Nuevo();
        btnEliminar.Click += (_, _) => Eliminar();
        btnReportes.Click += (_, _) => AbrirReportes();
        dgvProductos.CellDoubleClick += (_, e) => { if (e.RowIndex >= 0) Editar(); }; // RowIndex -1 = encabezado
        dgvProductos.CellFormatting += ResaltarStockBajo;

        timerReloj.Tick += (_, _) => lblHora.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
        timerReloj.Start();
        lblHora.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
    }

    // ---------- Carga / refresco ----------
    // Se llama después de CADA cambio: tabla, total y contador siempre quedan al día.
    private void CargarProductos()
    {
        string? codigoSeleccionado = Seleccionado()?.Codigo; // para no perder la selección al refrescar

        // BuscarPorTexto lanza excepción con texto vacío, por eso se pregunta antes
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

        // El total es de TODO el inventario, aunque la tabla esté filtrada
        decimal total = _servicio.ObtenerInventario().Sum(p => p.Precio * p.Stock);
        lblTotal.Text = $"Valor total: {total.ToString("C0", Cultura)}";
        lblCantidad.Text = $"Productos: {lista.Count}";
    }

    private Producto? Seleccionado() => dgvProductos.CurrentRow?.DataBoundItem as Producto;

    // Fila en rojo cuando el stock es bajo (< 5)
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

    // ---------- Acciones ----------
    private void Nuevo()
    {
        using var form = new FormProducto();
        if (form.ShowDialog(this) != DialogResult.OK) return;

        try
        {
            _servicio.Agregar(form.Codigo, form.Nombre, form.Precio, form.Stock);
            CargarProductos();
        }
        catch (Exception ex) // p. ej. código duplicado (InvalidOperationException)
        {
            MessageBox.Show(ex.Message, "No se pudo agregar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }

    // Doble clic en una fila: abre FormProducto con los datos precargados
    private void Editar()
    {
        if (Seleccionado() is not Producto producto) return;

        using var form = new FormProducto(producto);
        if (form.ShowDialog(this) != DialogResult.OK) return;

        try
        {
            _servicio.Actualizar(producto.Codigo, form.Nombre, form.Precio);
            CargarProductos();
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "No se pudo actualizar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }

    private void AbrirReportes()
    {
        using var form = new FormReportes(_servicio);
        form.ShowDialog(this);
    }

    private void Vender()
    {
        if (Seleccionado() is not Producto producto) { MessageBox.Show("Selecciona un producto."); return; }
        try
        {
            _servicio.Vender(producto.Codigo, (int)nudCantidad.Value);
            CargarProductos();
        }
        catch (StockInsuficienteException ex) // el mensaje sale de tu capa de negocio
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
