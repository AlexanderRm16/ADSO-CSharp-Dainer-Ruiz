using System.Globalization;
using Inventario.Negocio;

namespace Inventario.Escritorio;

// Dos tablas: productos más valiosos (precio × stock) y productos con stock bajo.
public class FormReportes : Form
{
    private static readonly CultureInfo Cultura = new("es-CO");

    public FormReportes(ServicioInventario servicio)
    {
        Text = "Reportes";
        Size = new Size(640, 520);
        StartPosition = FormStartPosition.CenterParent;

        // Reporte 1: valor en inventario = Precio * Stock, de mayor a menor (top 5)
        var masValiosos = servicio.ObtenerInventario()
            .Select(p => new { p.Codigo, p.Nombre, p.Stock, Valor = p.Precio * p.Stock })
            .OrderByDescending(x => x.Valor)
            .Take(5)
            .ToList();

        // Reporte 2: reutiliza el método que ya existía en el servicio (stock < 5)
        var stockBajo = servicio.ObtenerStockBajo()
            .Select(p => new { p.Codigo, p.Nombre, p.Stock })
            .ToList();

        var dgvValiosos = CrearTabla(masValiosos);
        dgvValiosos.Columns["Valor"]!.DefaultCellStyle = new DataGridViewCellStyle
        {
            Format = "C0", FormatProvider = Cultura, Alignment = DataGridViewContentAlignment.MiddleRight
        };
        var dgvBajo = CrearTabla(stockBajo);

        // 2 filas: título + tabla, título + tabla
        var layout = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 1, RowCount = 4, Padding = new Padding(8) };
        layout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        layout.RowStyles.Add(new RowStyle(SizeType.Percent, 50));
        layout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        layout.RowStyles.Add(new RowStyle(SizeType.Percent, 50));
        layout.Controls.Add(Titulo("Productos más valiosos (precio × stock)"));
        layout.Controls.Add(dgvValiosos);
        layout.Controls.Add(Titulo("Productos con stock bajo (< 5)"));
        layout.Controls.Add(dgvBajo);

        Controls.Add(layout);
    }

    private static Label Titulo(string texto) => new()
    {
        Text = texto, AutoSize = true, Font = new Font("Segoe UI", 10, FontStyle.Bold), Margin = new Padding(3, 8, 3, 4)
    };

    // Tabla de solo lectura que genera sus columnas desde la lista recibida
    private static DataGridView CrearTabla(object datos) => new()
    {
        Dock = DockStyle.Fill,
        ReadOnly = true,
        AllowUserToAddRows = false,
        RowHeadersVisible = false,
        SelectionMode = DataGridViewSelectionMode.FullRowSelect,
        AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
        DataSource = datos
    };
}
