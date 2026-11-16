using Conversor_Con_Ventana;

namespace Conversor_Con_Ventana
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            cboOrigen.Items.AddRange(ConversorTemperatura.Unidades);
            cboDestino.Items.AddRange(ConversorTemperatura.Unidades);

            txtValor.TextChanged += (s, e) => errorProvider.SetError(txtValor, "");
            cboOrigen.SelectedIndexChanged += (s, e) => errorProvider.SetError(cboOrigen, "");
            cboDestino.SelectedIndexChanged += (s, e) => errorProvider.SetError(cboDestino, "");
        }

        private bool ValidarCampos(out double valor)
        {
            bool ok = true;

            if (!double.TryParse(txtValor.Text, out valor) || !double.IsFinite(valor))
            {
                errorProvider.SetError(txtValor, "Ingrese un valor numérico.");
                ok = false;
            }

            if (cboOrigen.SelectedIndex < 0)
            {
                errorProvider.SetError(cboOrigen, "Elija la unidad de origen.");
                ok = false;
            }

            if (cboDestino.SelectedIndex < 0)
            {
                errorProvider.SetError(cboDestino, "Elija la unidad de destino.");
                ok = false;
            }

            return ok;
        }

        private void btnConvertir_Click(object? sender, EventArgs e)
        {
            lblResultado.Text = "";

            if (!ValidarCampos(out double valor)) return;

            string origen = cboOrigen.SelectedItem!.ToString()!;
            string destino = cboDestino.SelectedItem!.ToString()!;

            try
            {
                double resultado = ConversorTemperatura.Convertir(valor, origen, destino);
                lblResultado.Text = $"{valor:0.##} {origen} = {resultado:0.##} {destino}";
            }
            catch (ArgumentException ex)
            { 
                errorProvider.SetError(txtValor, ex.Message);
            }
        }
    }
}