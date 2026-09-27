namespace ControlGastos.Api.DTOs.Gastos
{
    public class GastoDto
    {
        public int Id { get; set; }
        public decimal Monto { get; set; }
        public string Descripcion { get; set; } = string.Empty;
        public DateTime Fecha { get; set; }
        public int CategoriaId { get; set; }
        public string CategoriaNombre { get; set; } = string.Empty;
    }
}
