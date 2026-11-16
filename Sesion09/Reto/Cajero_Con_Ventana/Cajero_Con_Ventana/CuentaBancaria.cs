namespace Cajero_Con_Ventana
{
    public class CuentaBancaria
    {
        public string Titular { get; }
        public string Numero { get; }
        public decimal Saldo { get; private set; }

        public CuentaBancaria(string titular, string numero, decimal saldoInicial)
        {
            Titular = titular;
            Numero = numero;
            Saldo = saldoInicial;
        }

        public void Depositar(decimal monto)
        {
            if (monto <= 0)
                throw new ArgumentException("El monto debe ser mayor que cero.");

            Saldo += monto;
        }

        public void Retirar(decimal monto)
        {
            ValidarMonto(monto);

            if (monto > Saldo)
                throw new InvalidOperationException("Saldo insuficiente para realizar el retiro.");

            Saldo -= monto;
        }

        public void Transferir(CuentaBancaria destino, decimal monto)
        {
            if (destino == this)
                throw new ArgumentException("No puede transferir a la misma cuenta.");

            Retirar(monto);       
            destino.Depositar(monto);
        }

        private static void ValidarMonto(decimal monto)
        {
            if (monto <= 0)
                throw new ArgumentException("El monto debe ser mayor que cero.");

            if (monto % 10000 != 0)
                throw new ArgumentException("El monto debe ser múltiplo de $10.000.");
        }
    }
}