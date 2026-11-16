namespace Conversor_Con_Ventana
{
    public static class ConversorTemperatura
    {
        public static readonly string[] Unidades = { "Celsius", "Fahrenheit", "Kelvin" };

        public static double Convertir(double valor, string origen, string destino)
        {
      
            double celsius = origen switch
            {
                "Celsius" => valor,
                "Fahrenheit" => (valor - 32) * 5 / 9,
                "Kelvin" => valor - 273.15,
                _ => throw new ArgumentException("Unidad de origen no válida.")
            };
            if (celsius < -273.15 - 1e-9)
                throw new ArgumentException("La temperatura no puede estar por debajo del cero absoluto.");

            return destino switch
            {
                "Celsius" => celsius,
                "Fahrenheit" => celsius * 9 / 5 + 32,
                "Kelvin" => celsius + 273.15,
                _ => throw new ArgumentException("Unidad de destino no válida.")
            };
        }
    }
}