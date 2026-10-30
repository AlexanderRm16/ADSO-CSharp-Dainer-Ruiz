public abstract class Empleado
{
    public string Nombre { get; }
    public string Documento { get; }

    protected Empleado(string nombre, string documento)
    {
        Nombre = nombre;
        Documento = documento;
    }

    public abstract decimal CalcularSalario();
    public virtual string Tipo => "Empleado";

    public override string ToString() =>
        $"{Nombre} ({Documento}) - {Tipo}: {CalcularSalario():C0}";
}
