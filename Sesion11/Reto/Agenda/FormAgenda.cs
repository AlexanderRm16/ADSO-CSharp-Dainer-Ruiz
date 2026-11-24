namespace Agenda;

public class FormAgenda : Form
{
    private readonly TextBox txtNombre = new() { PlaceholderText = "Nombre *" };
    private readonly TextBox txtCorreo = new() { PlaceholderText = "Correo" };
    private readonly TextBox txtTelefono = new() { PlaceholderText = "Teléfono" };
    private readonly Button btnAgregar = new() { Text = "Agregar" };
    private readonly Button btnGuardar = new() { Text = "Guardar cambios", Enabled = false };
    private readonly Button btnEliminar = new() { Text = "Eliminar" };
    private readonly TextBox txtBuscar = new() { PlaceholderText = "Buscar por nombre..." };
    private readonly DataGridView dgvContactos = new();
    private readonly Label lblTotal = new() { AutoSize = true };
    private readonly ErrorProvider errorProvider = new();

    private int? _idEditando; 

    public FormAgenda()
    {
        Text = "Agenda de contactos";
        Width = 760; Height = 520;
        StartPosition = FormStartPosition.CenterScreen;

        var panelDatos = new FlowLayoutPanel { Dock = DockStyle.Top, Height = 40, Padding = new Padding(8, 8, 0, 0) };
        txtNombre.Width = 170; txtCorreo.Width = 170; txtTelefono.Width = 110;
        btnAgregar.AutoSize = true; btnGuardar.AutoSize = true;
        panelDatos.Controls.AddRange([txtNombre, txtCorreo, txtTelefono, btnAgregar, btnGuardar]);

        var panelBusqueda = new FlowLayoutPanel { Dock = DockStyle.Top, Height = 40, Padding = new Padding(8, 8, 0, 0) };
        txtBuscar.Width = 250; btnEliminar.AutoSize = true;
        lblTotal.Margin = new Padding(20, 6, 0, 0);
        panelBusqueda.Controls.AddRange([txtBuscar, btnEliminar, lblTotal]);

        dgvContactos.Dock = DockStyle.Fill;
        dgvContactos.ReadOnly = true;
        dgvContactos.AllowUserToAddRows = false;
        dgvContactos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgvContactos.MultiSelect = false;
        dgvContactos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

        Controls.Add(dgvContactos);
        Controls.Add(panelBusqueda);
        Controls.Add(panelDatos);

        btnAgregar.Click += btnAgregar_Click;
        btnGuardar.Click += btnGuardar_Click;
        btnEliminar.Click += btnEliminar_Click;
        txtBuscar.TextChanged += (_, _) => CargarContactos(txtBuscar.Text);
        dgvContactos.CellDoubleClick += dgvContactos_CellDoubleClick;

        CargarContactos();
    }

    private void CargarContactos(string filtro = "")
    {
        using var db = new AgendaContext();
        var lista = db.Contactos
            .Where(c => c.Nombre.Contains(filtro))
            .OrderBy(c => c.Nombre)
            .ToList();

        dgvContactos.DataSource = lista;
        if (dgvContactos.Columns["Id"] is { } colId) colId.Visible = false;
        lblTotal.Text = $"Total: {lista.Count}";
    }

    private bool Validar()
    {
        errorProvider.Clear();
        bool ok = true;

        if (string.IsNullOrWhiteSpace(txtNombre.Text))
        {
            errorProvider.SetError(txtNombre, "El nombre es obligatorio");
            ok = false;
        }
        if (!txtCorreo.Text.Contains('@'))
        {
            errorProvider.SetError(txtCorreo, "El correo debe contener @");
            ok = false;
        }
        return ok;
    }

    private void LimpiarFormulario()
    {
        txtNombre.Clear(); txtCorreo.Clear(); txtTelefono.Clear();
        errorProvider.Clear();
        _idEditando = null;
        btnGuardar.Enabled = false;
        btnAgregar.Enabled = true;
    }

    private void btnAgregar_Click(object? sender, EventArgs e)
    {
        if (!Validar()) return;

        using var db = new AgendaContext();
        db.Contactos.Add(new Contacto
        {
            Nombre = txtNombre.Text.Trim(),
            Correo = txtCorreo.Text.Trim(),
            Telefono = txtTelefono.Text.Trim()
        });
        db.SaveChanges();

        LimpiarFormulario();
        CargarContactos(txtBuscar.Text);
    }

    private void btnEliminar_Click(object? sender, EventArgs e)
    {
        if (dgvContactos.CurrentRow?.DataBoundItem is not Contacto contacto) return;
        if (MessageBox.Show($"¿Eliminar a {contacto.Nombre}?", "Confirmar",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;

        using var db = new AgendaContext();
        db.Contactos.Remove(contacto);
        db.SaveChanges();
        CargarContactos(txtBuscar.Text);
    }

    private void dgvContactos_CellDoubleClick(object? sender, DataGridViewCellEventArgs e)
    {
        if (e.RowIndex < 0 || dgvContactos.Rows[e.RowIndex].DataBoundItem is not Contacto c) return;

        _idEditando = c.Id;
        txtNombre.Text = c.Nombre;
        txtCorreo.Text = c.Correo;
        txtTelefono.Text = c.Telefono;
        btnGuardar.Enabled = true;
        btnAgregar.Enabled = false;
    }

    private void btnGuardar_Click(object? sender, EventArgs e)
    {
        if (_idEditando is not int id || !Validar()) return;

        using var db = new AgendaContext();
        var contacto = db.Contactos.Find(id);
        if (contacto is null) return;

        contacto.Nombre = txtNombre.Text.Trim();
        contacto.Correo = txtCorreo.Text.Trim();
        contacto.Telefono = txtTelefono.Text.Trim();
        db.SaveChanges();

        LimpiarFormulario();
        CargarContactos(txtBuscar.Text);
    }
}
