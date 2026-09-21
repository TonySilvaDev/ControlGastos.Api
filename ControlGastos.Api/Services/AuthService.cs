using ControlGastos.Api.DTOs.Auth;
using ControlGastos.Api.Models;
using ControlGastos.Api.Services.Interface;
using Microsoft.AspNetCore.Identity;

namespace ControlGastos.Api.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<Usuario> _userManager;
        private readonly SignInManager<Usuario> _signInManager;

        public AuthService(UserManager<Usuario> userManager, SignInManager<Usuario> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }        

        public async Task<(bool Exitoso, string Mensaje, AuthResponseDto? Datos)> RegistrarAsync(RegistrarDto registrarDto)
        {
            var usuarioExistente = await _userManager.FindByEmailAsync(registrarDto.Email);

            if (usuarioExistente != null)
            {
                return (false, "El correo electrónico ya esta registrado", null);
            }

            var usuario = new Usuario
            {
                UserName = registrarDto.Email,
                Email = registrarDto.Email
            };

            var resultado = await _userManager.CreateAsync(usuario, registrarDto.Password);

            if (!resultado.Succeeded)
            {
                var errores = string.Join(" ", resultado.Errors.Select(x => x.Description));
                return (false, errores, null);
            }

            var datos = new AuthResponseDto
            {
                UsuarioId = usuario.Id,
                Email = usuario.Email
            };

            return (true, "Usuario registrado correctamente", datos);
        }

        public async Task<(bool Exitoso, string Mensaje, AuthResponseDto? Datos)> LoginAsync(LoginDto loginDto)
        {
            var usuario = await _userManager.FindByEmailAsync(loginDto.Email);

            if (usuario == null)
            {
                return (false, "Correo o contraseña incorrecta", null);
            }

            var resultado = await _signInManager.CheckPasswordSignInAsync(usuario, loginDto.Password, lockoutOnFailure: false);

            if (!resultado.Succeeded)
            {
                return (false, "Correo o contraseña incorrectos", null);
            }

            var datos = new AuthResponseDto
            {
                UsuarioId = usuario.Id,
                Email = usuario.Email
            };

            return (true, "LoginCOrrecto", datos);
        }
    }
}
