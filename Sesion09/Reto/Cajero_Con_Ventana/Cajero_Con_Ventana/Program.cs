using Cajero_Con_Ventana;

namespace Reto
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();

            using (var pin = new FormPin())
            {
                if (pin.ShowDialog() != DialogResult.OK) return;
            }

            Application.Run(new Form1());
        }
    }
}