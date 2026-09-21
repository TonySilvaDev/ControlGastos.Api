using ControlGastos.Api.Models;

namespace ControlGastos.Api.Repositories.Interfaces
{
    public interface ICategoriaRepository
    {
        Task<List<Categoria>> ObtenerTodasAsync(string usuarioId);
        Task<Categoria> ObtenerPorIdAsync(int id, string usuarioId);
        Task<Categoria> CrearAsync(Categoria categoria);
        Task ActualizarAsync(Categoria categoria);
        Task EliminarAsync(Categoria categoria);
    }
}
