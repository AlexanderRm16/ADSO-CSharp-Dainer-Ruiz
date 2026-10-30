public class EmpleadoPlanta : Empleado, IBonificable
{
    public decimal SalarioMensual { get; }
    public int Antiguedad { get; } 

    public EmpleadoPlanta(string nombre, string documento, decimal salarioMensual, int antiguedad)
        : base(nombre, documento)
    {
        SalarioMensual = salarioMensual;
        Antiguedad = antiguedad;
    }

    public override string Tipo => "Planta";

    public decimal CalcularBono() => Antiguedad >= 5 ? SalarioMensual * 0.05m : 0m;

    public override decimal CalcularSalario() => SalarioMensual + CalcularBono();
}