using ControlGastos.Api.DTOs.Gastos;
using ControlGastos.Api.Models;
using ControlGastos.Api.Repositories.Interfaces;
using ControlGastos.Api.Services.Interface;

namespace ControlGastos.Api.Services
{
    public class GastoService : IGastoService
    {
        private readonly IGastoRepository _gastoRepository;
        private readonly ICategoriaRepository _categoriaRepository;

        public GastoService(
            IGastoRepository gastoRepository,
            ICategoriaRepository categoriaRepository)
        {
            _gastoRepository = gastoRepository;
            _categoriaRepository = categoriaRepository;
        }

        public async Task<List<GastoDto>> ObtenerTodosAsync(string usuarioId)
        {
            var gastos = await _gastoRepository
                .ObtenerTodosAsync(usuarioId);

            return gastos.Select(MapearDto).ToList();
        }

        public async Task<GastoDto?> ObtenerPorIdAsync(
            int id,
            string usuarioId)
        {
            var gasto = await _gastoRepository
                .ObtenerPorIdAsync(id, usuarioId);

            return gasto == null
                ? null
                : MapearDto(gasto);
        }

        public async Task<GastoDto?> CrearAsync(
            CrearGastoDto dto,
            string usuarioId)
        {
            // La categoría debe pertenecer al usuario autenticado
            var categoria = await _categoriaRepository
                .ObtenerPorIdAsync(dto.CategoriaId, usuarioId);

            if (categoria == null)
            {
                return null;
            }

            var gasto = new Gasto
            {
                Monto = dto.Monto,
                Descripcion = dto.Descripcion,
                Fecha = dto.Fecha,
                CategoriaId = dto.CategoriaId,
                TipoOperacionId = dto.TipoOperacionId,
                UsuarioId = usuarioId
            };

            var resultado = await _gastoRepository
                .CrearAsync(gasto);

            resultado.Categoria = categoria;

            return MapearDto(resultado);
        }

        public async Task<GastoDto?> ActualizarAsync(
            int id,
            ActualizarGastoDto dto,
            string usuarioId)
        {
            var gasto = await _gastoRepository
                .ObtenerPorIdAsync(id, usuarioId);

            if (gasto == null)
            {
                return null;
            }

            // La nueva categoría también debe pertenecer
            // al usuario autenticado
            var categoria = await _categoriaRepository
                .ObtenerPorIdAsync(dto.CategoriaId, usuarioId);

            if (categoria == null)
            {
                return null;
            }

            gasto.Monto = dto.Monto;
            gasto.Descripcion = dto.Descripcion;
            gasto.Fecha = dto.Fecha;
            gasto.CategoriaId = dto.CategoriaId;
            gasto.TipoOperacionId = dto.TipoOperacionId;
            gasto.Categoria = categoria;

            await _gastoRepository.ActualizarAsync(gasto);

            return MapearDto(gasto);
        }

        public async Task<bool> EliminarAsync(
            int id,
            string usuarioId)
        {
            var gasto = await _gastoRepository
                .ObtenerPorIdAsync(id, usuarioId);

            if (gasto == null)
            {
                return false;
            }

            await _gastoRepository.EliminarAsync(gasto);

            return true;
        }

        private static GastoDto MapearDto(Gasto gasto)
        {
            return new GastoDto
            {
                Id = gasto.Id,
                Monto = gasto.Monto,
                Descripcion = gasto.Descripcion,
                Fecha = gasto.Fecha,
                CategoriaId = gasto.CategoriaId,
                CategoriaNombre = gasto.Categoria.Nombre,
                TipoOperacionId = gasto.TipoOperacionId
            };
        }
    }
}
