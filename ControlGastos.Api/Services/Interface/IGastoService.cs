using ControlGastos.Api.DTOs.Gastos;

namespace ControlGastos.Api.Services.Interface
{
    public interface IGastoService
    {
        Task<List<GastoDto>> ObtenerTodosAsync(string usuarioId);

        Task<GastoDto?> ObtenerPorIdAsync(int id, string usuarioId);

        Task<GastoDto?> CrearAsync(CrearGastoDto dto, string usuarioId);

        Task<GastoDto?> ActualizarAsync(
            int id,
            ActualizarGastoDto dto,
            string usuarioId);

        Task<bool> EliminarAsync(int id, string usuarioId);
    }
}
