namespace ControlGastos.Api.DTOs.Dashboard
{
    public class DashboardDto
    {
        public decimal Saldo { get; set; }

        public decimal TotalIngresos { get; set; }

        public decimal TotalGastos { get; set; }

        public List<ResumenCategoriaDto> GastosPorCategoria { get; set; }
            = new();

        public List<TendenciaMensualDto> TendenciaMensual { get; set; }
            = new();

        public List<TransaccionRecienteDto> TransaccionesRecientes { get; set; }
            = new();
    }
}
