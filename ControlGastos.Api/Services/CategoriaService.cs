using ControlGastos.Api.DTOs.Categorias;
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

        public async Task<List<CategoriaDto>> ObtenerTosasAsync(string usuarioId)
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
