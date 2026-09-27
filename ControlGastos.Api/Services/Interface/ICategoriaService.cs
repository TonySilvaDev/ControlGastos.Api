using ControlGastos.Api.DTOs.Categorias;

namespace ControlGastos.Api.Services.Interface
{
    public interface ICategoriaService
    {
        Task<List<CategoriaDto>> ObtenerTodasAsync(
        string usuarioId);

        Task<CategoriaDto?> ObtenerPorIdAsync(
            int id,
            string usuarioId);

        Task<(bool Exito, string? Error, CategoriaDto? Categoria)> CrearAsync(
            CrearCategoriaDto dto,
            string usuarioId);

        Task<(bool Exito, string? Error)> ActualizarAsync(
            int id,
            CrearCategoriaDto dto,
            string usuarioId);

        Task<(bool Exito, string? Error)> EliminarAsync(
            int id,
            string usuarioId);
    }
}
