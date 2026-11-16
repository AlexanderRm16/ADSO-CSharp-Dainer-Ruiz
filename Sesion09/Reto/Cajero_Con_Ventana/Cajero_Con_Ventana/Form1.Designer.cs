namespace Reto
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
            lblTitular = new Label();
            lblSaldo = new Label();
            lblDestino = new Label();
            lblMonto = new Label();
            nudMonto = new NumericUpDown();
            btnDepositar = new Button();
            btnRetirar = new Button();
            btnTransferir = new Button();
            lblMovimientos = new Label();
            lstMovimientos = new ListBox();
            ((System.ComponentModel.ISupportInitialize)nudMonto).BeginInit();
            SuspendLayout();

            lblTitular.AutoSize = true;
            lblTitular.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblTitular.Location = new Point(20, 15);

            lblSaldo.AutoSize = true;
            lblSaldo.Font = new Font("Segoe UI", 11F);
            lblSaldo.Location = new Point(20, 42);

            lblDestino.AutoSize = true;
            lblDestino.ForeColor = Color.DimGray;
            lblDestino.Location = new Point(20, 72);

            lblMonto.AutoSize = true;
            lblMonto.Text = "Monto:";
            lblMonto.Location = new Point(20, 108);

            nudMonto.Location = new Point(80, 105);
            nudMonto.Size = new Size(170, 23);
            nudMonto.Maximum = 100000000;
            nudMonto.Increment = 10000;
            nudMonto.ThousandsSeparator = true;

            btnDepositar.Text = "Depositar";
            btnDepositar.Location = new Point(20, 145);
            btnDepositar.Size = new Size(120, 35);
            btnDepositar.Click += btnDepositar_Click;

            btnRetirar.Text = "Retirar";
            btnRetirar.Location = new Point(150, 145);
            btnRetirar.Size = new Size(120, 35);
            btnRetirar.Click += btnRetirar_Click;

            btnTransferir.Text = "Transferir";
            btnTransferir.Location = new Point(280, 145);
            btnTransferir.Size = new Size(120, 35);
            btnTransferir.Click += btnTransferir_Click;

            lblMovimientos.AutoSize = true;
            lblMovimientos.Text = "Movimientos (hora | tipo | valor):";
            lblMovimientos.Location = new Point(20, 195);

            lstMovimientos.Location = new Point(20, 217);
            lstMovimientos.Size = new Size(380, 220);

            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(420, 455);
            Controls.AddRange(new Control[] { lblTitular, lblSaldo, lblDestino, lblMonto,
                nudMonto, btnDepositar, btnRetirar, btnTransferir, lblMovimientos, lstMovimientos });
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Cajero";
            ((System.ComponentModel.ISupportInitialize)nudMonto).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        private Label lblTitular;
        private Label lblSaldo;
        private Label lblDestino;
        private Label lblMonto;
        private NumericUpDown nudMonto;
        private Button btnDepositar;
        private Button btnRetirar;
        private Button btnTransferir;
        private Label lblMovimientos;
        private ListBox lstMovimientos;
    }
}