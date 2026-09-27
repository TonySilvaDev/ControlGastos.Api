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

        public async Task ActualizarAsync(Categoria categoria)
        {
            _context.Categorias.Update(categoria);

            await _context.SaveChangesAsync();
        }

        public async Task<Categoria> CrearAsync(Categoria categoria)
        {
            _context.Categorias.Add(categoria);

            await _context.SaveChangesAsync();

            return categoria;
        }

        public async Task EliminarAsync(Categoria categoria)
        {
            _context.Categorias.Remove(categoria);

            await _context.SaveChangesAsync();
        }

        public async Task<Categoria?> ObtenerPorIdAsync(int id, string usuarioId)
        {
            return await _context.Categorias
                        .FirstOrDefaultAsync(x =>
                            x.Id == id &&
                            x.UsuarioId == usuarioId
                        );
        }

        public async Task<Categoria?> ObtenerPorNombreAsync(string nombre, string usuarioId)
        {
            return await _context.Categorias
                        .FirstOrDefaultAsync(x =>
                            x.UsuarioId == usuarioId &&
                            x.Nombre.ToLower() == nombre.ToLower()
                        );
        }

        public async Task<bool> TieneGastosAsync(int categoriaId)
        {
            return await _context.Gastos
                        .AnyAsync(x => x.CategoriaId == categoriaId);
        }
    }
}
