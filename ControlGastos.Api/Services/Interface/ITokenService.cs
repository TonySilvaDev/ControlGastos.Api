using ControlGastos.Api.Models;

namespace ControlGastos.Api.Services.Interface
{
    public interface ITokenService
    {
        Task<string> CreateTokenAsync(Usuario usuario);
    }
}
