using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Cajero_Con_Ventana
{
    public partial class FormPin : Form
    {
        private const string PinCorrecto = "1234";
        private int _intentos = 0;

        private readonly Label lblInfo = new() { Text = "Ingrese su PIN:", Location = new Point(20, 15), AutoSize = true };
        private readonly TextBox txtPin = new() { PasswordChar = '*', MaxLength = 4, Location = new Point(20, 40), Width = 200 };
        private readonly Button btnAceptar = new() { Text = "Aceptar", Location = new Point(20, 80), Width = 100 };

        public FormPin()
        {
            InitializeComponent();
            Text = "PIN";
            ClientSize = new Size(240, 125);
            StartPosition = FormStartPosition.CenterScreen;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;

            Controls.AddRange(new Control[] { lblInfo, txtPin, btnAceptar });
            AcceptButton = btnAceptar; // Enter = Aceptar
            btnAceptar.Click += btnAceptar_Click;
        }

        private void btnAceptar_Click(object? sender, EventArgs e)
        {
            if (txtPin.Text == PinCorrecto)
            {
                DialogResult = DialogResult.OK; // cierra el diálogo
                return;
            }

            _intentos++;
            int restantes = 3 - _intentos;

            if (restantes == 0)
            {
                MessageBox.Show("Superó los 3 intentos. La aplicación se cerrará.",
                    "Acceso bloqueado", MessageBoxButtons.OK, MessageBoxIcon.Error);
                DialogResult = DialogResult.Cancel;
                return;
            }

            MessageBox.Show($"PIN incorrecto. Le quedan {restantes} intento(s).",
                "PIN", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            txtPin.Clear();
            txtPin.Focus();
        }
    }
}