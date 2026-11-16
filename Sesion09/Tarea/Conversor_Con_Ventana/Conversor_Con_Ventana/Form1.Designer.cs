using static System.Net.Mime.MediaTypeNames;

namespace Conversor_Con_Ventana
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            errorProvider = new ErrorProvider(components);
            lblValor = new Label();
            txtValor = new TextBox();
            lblOrigen = new Label();
            cboOrigen = new ComboBox();
            lblDestino = new Label();
            cboDestino = new ComboBox();
            btnConvertir = new Button();
            lblResultado = new Label();
            ((System.ComponentModel.ISupportInitialize)errorProvider).BeginInit();
            SuspendLayout();

            errorProvider.ContainerControl = this;
            errorProvider.BlinkStyle = ErrorBlinkStyle.NeverBlink;

            lblValor.AutoSize = true;
            lblValor.Text = "Valor:";
            lblValor.Location = new Point(20, 20);

            txtValor.Location = new Point(20, 40);
            txtValor.Size = new Size(260, 23);

            lblOrigen.AutoSize = true;
            lblOrigen.Text = "De:";
            lblOrigen.Location = new Point(20, 80);

            cboOrigen.DropDownStyle = ComboBoxStyle.DropDownList; // solo se puede elegir de la lista
            cboOrigen.Location = new Point(20, 100);
            cboOrigen.Size = new Size(120, 23);

            lblDestino.AutoSize = true;
            lblDestino.Text = "A:";
            lblDestino.Location = new Point(160, 80);

            cboDestino.DropDownStyle = ComboBoxStyle.DropDownList;
            cboDestino.Location = new Point(160, 100);
            cboDestino.Size = new Size(120, 23);

            btnConvertir.Text = "Convertir";
            btnConvertir.Location = new Point(20, 145);
            btnConvertir.Size = new Size(260, 35);
            btnConvertir.Click += btnConvertir_Click;

            lblResultado.AutoSize = true;
            lblResultado.Font = new System.Drawing.Font("Segoe UI", 11F, FontStyle.Bold);
            lblResultado.Location = new Point(20, 195);

            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(300, 240);
            Controls.AddRange(new Control[] { lblValor, txtValor, lblOrigen, cboOrigen,
                lblDestino, cboDestino, btnConvertir, lblResultado });
            AcceptButton = btnConvertir; // Enter = Convertir
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Conversor de temperatura";
            ((System.ComponentModel.ISupportInitialize)errorProvider).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        private ErrorProvider errorProvider;
        private Label lblValor;
        private TextBox txtValor;
        private Label lblOrigen;
        private ComboBox cboOrigen;
        private Label lblDestino;
        private ComboBox cboDestino;
        private Button btnConvertir;
        private Label lblResultado;
    }
}