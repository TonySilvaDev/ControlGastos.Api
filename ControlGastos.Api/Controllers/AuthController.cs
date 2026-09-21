using ControlGastos.Api.DTOs.Auth;
using ControlGastos.Api.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ControlGastos.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("registrar")]
        public async Task<IActionResult> Registrar(RegistrarDto registrarDto)
        {
            var resultado = await _authService.RegistrarAsync(registrarDto);

            if (!resultado.Exitoso)
            {
                return BadRequest(new { mensaje = resultado.Mensaje });
            }

            return Ok(resultado.Datos);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            var resultado = await _authService.LoginAsync(loginDto);

            if (!resultado.Exitoso)
            {
                return Unauthorized(new { mensaje = resultado.Mensaje });
            }

            return Ok(resultado.Datos);
        }
    }
}
