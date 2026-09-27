using System.ComponentModel.DataAnnotations;

namespace ControlGastos.Api.DTOs.Gastos
{
    public class CrearGastoDto
    {
        [Required]
        [Range(0.01, double.MaxValue)]
        public decimal Monto { get; set; }

        [Required]
        [StringLength(250)]
        public string Descripcion { get; set; } = string.Empty;

        [Required]
        public DateTime Fecha { get; set; }

        [Required]
        public int CategoriaId { get; set; }

        [Required]
        public int TipoOperacionId { get; set; }
    }
}
