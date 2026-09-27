using ControlGastos.Api.DTOs.Gastos;
using ControlGastos.Api.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ControlGastos.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class GastosController : ControllerBase
    {
        private readonly IGastoService _gastoService;

        public GastosController(IGastoService gastoService)
        {
            _gastoService = gastoService;
        }

        [HttpGet]
        public async Task<ActionResult<List<GastoDto>>> ObtenerTodos()
        {
            var usuarioId = ObtenerUsuarioId();

            if (usuarioId == null)
            {
                return Unauthorized();
            }

            var gastos = await _gastoService
                .ObtenerTodosAsync(usuarioId);

            return Ok(gastos);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<GastoDto>> ObtenerPorId(int id)
        {
            var usuarioId = ObtenerUsuarioId();

            if (usuarioId == null)
            {
                return Unauthorized();
            }

            var gasto = await _gastoService
                .ObtenerPorIdAsync(id, usuarioId);

            if (gasto == null)
            {
                return NotFound();
            }

            return Ok(gasto);
        }

        [HttpPost]
        public async Task<ActionResult<GastoDto>> Crear(
            CrearGastoDto dto)
        {
            var usuarioId = ObtenerUsuarioId();

            if (usuarioId == null)
            {
                return Unauthorized();
            }

            var gasto = await _gastoService
                .CrearAsync(dto, usuarioId);

            if (gasto == null)
            {
                return BadRequest(
                    "La categoría no existe o no pertenece al usuario autenticado.");
            }

            return CreatedAtAction(
                nameof(ObtenerPorId),
                new { id = gasto.Id },
                gasto);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<GastoDto>> Actualizar(
            int id,
            ActualizarGastoDto dto)
        {
            var usuarioId = ObtenerUsuarioId();

            if (usuarioId == null)
            {
                return Unauthorized();
            }

            var gasto = await _gastoService
                .ActualizarAsync(id, dto, usuarioId);

            if (gasto == null)
            {
                return NotFound(
                    "El gasto no existe, no pertenece al usuario o la categoría no le pertenece.");
            }

            return Ok(gasto);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            var usuarioId = ObtenerUsuarioId();

            if (usuarioId == null)
            {
                return Unauthorized();
            }

            var eliminado = await _gastoService
                .EliminarAsync(id, usuarioId);

            if (!eliminado)
            {
                return NotFound();
            }

            return NoContent();
        }

        private string? ObtenerUsuarioId()
        {
            return User.FindFirstValue(ClaimTypes.NameIdentifier);
        }
    }
}
