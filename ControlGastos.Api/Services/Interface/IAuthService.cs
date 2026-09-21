using ControlGastos.Api.DTOs.Auth;

namespace ControlGastos.Api.Services.Interface
{
    public interface IAuthService
    {
        Task<(bool Exitoso, string Mensaje, AuthResponseDto? Datos)> RegistrarAsync(RegistrarDto registrarDto);
        Task<(bool Exitoso, string Mensaje, AuthResponseDto? Datos)> LoginAsync(LoginDto loginDto);
    }
}
