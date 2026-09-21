using ControlGastos.Api.DTOs.Categorias;
using ControlGastos.Api.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ControlGastos.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriasController : ControllerBase
    {
        private readonly ICategoriaService _service;

        public CategoriasController(ICategoriaService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<CategoriaDto>>> ObtenerTodas()
        {
            var usuarioId = Guid.NewGuid().ToString();

            var categorias = await _service.ObtenerTosasAsync(usuarioId);

            return Ok(categorias);
        }
    }
}
