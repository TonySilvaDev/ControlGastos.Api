namespace ControlGastos.Api.Models
{
    public class Categoria
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string UsuarioId { get; set; } = string.Empty;
        public Usuario Usuario { get; set; } = null!;
        public ICollection<Gasto> Gastos { get; set; } = new List<Gasto>();
    }
}
