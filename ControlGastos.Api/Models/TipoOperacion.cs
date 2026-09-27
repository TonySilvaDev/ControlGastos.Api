namespace ControlGastos.Api.Models
{
    public class TipoOperacion
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public ICollection<Gasto> Gastos { get; set; } = new List<Gasto>();
    }
}
