using ControlGastos.Api.Models;

namespace ControlGastos.Api.Repositories.Interfaces
{
    public interface IGastoRepository
    {
        Task<List<Gasto>> ObtenerTodosAsync(string usuarioId);

        Task<Gasto?> ObtenerPorIdAsync(int id, string usuarioId);

        Task<Gasto> CrearAsync(Gasto gasto);

        Task ActualizarAsync(Gasto gasto);

        Task EliminarAsync(Gasto gasto);
    }
}
