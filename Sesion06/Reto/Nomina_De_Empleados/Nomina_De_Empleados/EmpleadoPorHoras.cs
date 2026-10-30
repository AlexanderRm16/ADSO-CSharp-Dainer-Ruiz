public class EmpleadoPorHoras : Empleado
{
    private const int HorasNormales = 160;
    private const decimal RecargoExtra = 1.5m;

    public int Horas { get; }
    public decimal ValorHora { get; }

    public EmpleadoPorHoras(string nombre, string documento, int horas, decimal valorHora)
        : base(nombre, documento)
    {
        Horas = horas;
        ValorHora = valorHora;
    }

    public override string Tipo => "Por horas";

    public override decimal CalcularSalario()
    {
        int normales = Math.Min(Horas, HorasNormales);
        int extras = Math.Max(0, Horas - HorasNormales);
        return normales * ValorHora + extras * ValorHora * RecargoExtra;
    }
}