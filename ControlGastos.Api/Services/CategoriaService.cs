using ControlGastos.Api.DTOs.Categorias;
using ControlGastos.Api.Models;
using ControlGastos.Api.Repositories.Interfaces;
using ControlGastos.Api.Services.Interface;

namespace ControlGastos.Api.Services
{
    public class CategoriaService : ICategoriaService
    {
        private readonly ICategoriaRepository _repository;

        public CategoriaService(ICategoriaRepository repository)
        {
            _repository = repository;
        }

        public async Task<(bool Exito, string? Error)> ActualizarAsync(int id, CrearCategoriaDto dto, string usuarioId)
        {
            var nombre = dto.Nombre.Trim();

            if (string.IsNullOrWhiteSpace(nombre))
            {
                return (
                    false,
                    "El nombre de la categoría es obligatorio.");
            }

            var categoria = await _repository
                .ObtenerPorIdAsync(id, usuarioId);

            if (categoria == null)
            {
                return (
                    false,
                    "La categoría no existe.");
            }

            var existente = await _repository
                .ObtenerPorNombreAsync(nombre, usuarioId);

            if (existente != null &&
                existente.Id != categoria.Id)
            {
                return (
                    false,
                    "Ya existe una categoría con ese nombre.");
            }

            categoria.Nombre = nombre;

            await _repository.ActualizarAsync(categoria);

            return (true, null);
        }

        public async Task<(bool Exito, string? Error, CategoriaDto? Categoria)> CrearAsync(CrearCategoriaDto dto, string usuarioId)
        {
            var nombre = dto.Nombre.Trim();

            if (string.IsNullOrWhiteSpace(nombre))
            {
                return (
                    false,
                    "El nombre de la categoría es obligatorio.",
                    null);
            }

            var existente = await _repository
                .ObtenerPorNombreAsync(nombre, usuarioId);

            if (existente != null)
            {
                return (
                    false,
                    "Ya existe una categoría con ese nombre.",
                    null);
            }

            var categoria = new Categoria
            {
                Nombre = nombre,
                UsuarioId = usuarioId
            };

            await _repository.CrearAsync(categoria);

            var resultado = new CategoriaDto
            {
                Id = categoria.Id,
                Nombre = categoria.Nombre
            };

            return (true, null, resultado);
        }

        public async Task<(bool Exito, string? Error)> EliminarAsync(int id, string usuarioId)
        {
            var categoria = await _repository
            .ObtenerPorIdAsync(id, usuarioId);

            if (categoria == null)
            {
                return (
                    false,
                    "La categoría no existe.");
            }

            var tieneGastos = await _repository.TieneGastosAsync(id);

            if (tieneGastos)
            {
                return (
                    false,
                    "No se puede eliminar la categoría porque tiene gastos asociados");
            }

            await _repository.EliminarAsync(categoria);

            return (true, null);
        }

        public async Task<CategoriaDto?> ObtenerPorIdAsync(int id, string usuarioId)
        {
            var categoria = await _repository
            .ObtenerPorIdAsync(id, usuarioId);

            if (categoria == null)
                return null;

            return new CategoriaDto
            {
                Id = categoria.Id,
                Nombre = categoria.Nombre
            };
        }

        public async Task<List<CategoriaDto>> ObtenerTodasAsync(string usuarioId)
        {
            var categorias = await _repository.ObtenerTodasAsync(usuarioId);

            return categorias
                   .Select(x => new CategoriaDto
                   {
                       Id = x.Id,
                       Nombre = x.Nombre
                   })
                   .ToList();
        }
    }
}
