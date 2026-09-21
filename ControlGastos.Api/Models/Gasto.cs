namespace ControlGastos.Api.Models
{
    public class Gasto
    {
        public int Id { get; set; }
        public decimal Monto { get; set; }
        public string Descripcion { get; set; } = string.Empty;
        public DateTime Fecha { get; set; }
        public int CategoriaId { get; set; }
        public string UsuarioId { get; set; } = string.Empty;
        public Categoria Categoria { get; set; } = null!;
        public Usuario Usuario { get; set; } = null!;
    }
}
