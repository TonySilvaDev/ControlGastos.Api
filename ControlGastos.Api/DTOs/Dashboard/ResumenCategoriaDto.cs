namespace ControlGastos.Api.DTOs.Dashboard
{
    public class ResumenCategoriaDto
    {
        public int CategoriaId { get; set; }

        public string Categoria { get; set; } = string.Empty;

        public decimal Total { get; set; }
    }
}
