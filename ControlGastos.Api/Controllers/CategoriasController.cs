using ControlGastos.Api.DTOs.Categorias;
using ControlGastos.Api.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<List<CategoriaDto>>> ObtenerTodas()
        {
            var usuarioId = ObtenerUsuarioId();

            var categorias = await _service.ObtenerTodasAsync(usuarioId);

            return Ok(categorias);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<CategoriaDto>> ObtenerPorId(
        int id)
        {
            var usuarioId = ObtenerUsuarioId();

            var categoria = await _service
                .ObtenerPorIdAsync(id, usuarioId);

            if (categoria == null)
                return NotFound();

            return Ok(categoria);
        }

        [HttpPost]
        public async Task<ActionResult<CategoriaDto>> Crear(
        CrearCategoriaDto dto)
        {
            var usuarioId = ObtenerUsuarioId();

            var resultado = await _service
                .CrearAsync(dto, usuarioId);

            if (!resultado.Exito)
                return BadRequest(new
                {
                    mensaje = resultado.Error
                });

            return CreatedAtAction(
                nameof(ObtenerPorId),
                new { id = resultado.Categoria!.Id },
                resultado.Categoria);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Actualizar(
        int id,
        CrearCategoriaDto dto)
        {
            var usuarioId = ObtenerUsuarioId();

            var resultado = await _service
                .ActualizarAsync(id, dto, usuarioId);

            if (!resultado.Exito)
            {
                if (resultado.Error == "La categoría no existe.")
                    return NotFound(new
                    {
                        mensaje = resultado.Error
                    });

                return BadRequest(new
                {
                    mensaje = resultado.Error
                });
            }

            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Eliminar(
        int id)
        {
            var usuarioId = ObtenerUsuarioId();

            var resultado = await _service
                .EliminarAsync(id, usuarioId);

            if (!resultado.Exito)
            {
                return NotFound(new
                {
                    mensaje = resultado.Error
                });
            }

            return NoContent();
        }

        private string ObtenerUsuarioId()
        {
            return User.FindFirstValue(
                ClaimTypes.NameIdentifier)!;
        }
    }
}
