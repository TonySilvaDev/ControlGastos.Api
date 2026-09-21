using ControlGastos.Api.Data;
using ControlGastos.Api.Models;
using ControlGastos.Api.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ControlGastos.Api.Repositories
{
    public class CategoriaRepository : ICategoriaRepository
    {
        private readonly AppDbContext _context;

        public CategoriaRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Categoria>> ObtenerTodasAsync(string usuarioId)
        {
            return await _context.Categorias
                        .Where(x => x.UsuarioId == usuarioId)
                        .OrderBy(x => x.Nombre)
                        .ToListAsync();
        }

        public Task ActualizarAsync(Categoria categoria)
        {
            throw new NotImplementedException();
        }

        public Task<Categoria> CrearAsync(Categoria categoria)
        {
            throw new NotImplementedException();
        }

        public Task EliminarAsync(Categoria categoria)
        {
            throw new NotImplementedException();
        }

        public Task<Categoria> ObtenerPorIdAsync(int id, string usuarioId)
        {
            throw new NotImplementedException();
        }

        
    }
}
