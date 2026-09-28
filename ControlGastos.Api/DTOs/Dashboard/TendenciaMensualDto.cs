namespace ControlGastos.Api.DTOs.Dashboard
{
    public class TendenciaMensualDto
    {
        public int Anio { get; set; }

        public int Mes { get; set; }

        public string NombreMes { get; set; } = string.Empty;

        public decimal Ingresos { get; set; }

        public decimal Gastos { get; set; }
    }
}
