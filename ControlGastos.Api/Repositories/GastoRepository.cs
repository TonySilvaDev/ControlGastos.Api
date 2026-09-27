using ControlGastos.Api.Data;
using ControlGastos.Api.Models;
using ControlGastos.Api.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ControlGastos.Api.Repositories
{
    public class GastoRepository : IGastoRepository
    {
        private readonly AppDbContext _context;

        public GastoRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Gasto>> ObtenerTodosAsync(string usuarioId)
        {
            return await _context.Gastos
                .Include(g => g.Categoria)
                .Where(g => g.UsuarioId == usuarioId)
                .OrderByDescending(g => g.Fecha)
                .ToListAsync();
        }

        public async Task<Gasto?> ObtenerPorIdAsync(int id, string usuarioId)
        {
            return await _context.Gastos
                .Include(g => g.Categoria)
                .FirstOrDefaultAsync(g =>
                    g.Id == id &&
                    g.UsuarioId == usuarioId);
        }

        public async Task<Gasto> CrearAsync(Gasto gasto)
        {
            _context.Gastos.Add(gasto);
            await _context.SaveChangesAsync();

            return gasto;
        }

        public async Task ActualizarAsync(Gasto gasto)
        {
            _context.Gastos.Update(gasto);
            await _context.SaveChangesAsync();
        }

        public async Task EliminarAsync(Gasto gasto)
        {
            _context.Gastos.Remove(gasto);
            await _context.SaveChangesAsync();
        }
    }
}
