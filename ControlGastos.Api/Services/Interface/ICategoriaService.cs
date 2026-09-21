using ControlGastos.Api.DTOs.Categorias;

namespace ControlGastos.Api.Services.Interface
{
    public interface ICategoriaService
    {
        Task<List<CategoriaDto>> ObtenerTosasAsync(string usuarioId);
    }
}
