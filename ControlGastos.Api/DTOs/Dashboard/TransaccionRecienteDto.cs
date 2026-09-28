namespace ControlGastos.Api.DTOs.Dashboard
{
    public class TransaccionRecienteDto
    {
        public int Id { get; set; }

        public DateTime Fecha { get; set; }

        public int CategoriaId { get; set; }

        public string Categoria { get; set; } = string.Empty;

        public int TipoOperacionId { get; set; }

        public string TipoOperacion { get; set; } = string.Empty;

        public string Descripcion { get; set; } = string.Empty;

        public decimal Monto { get; set; }
    }
}
