namespace ControlGastos.Api.DTOs.Auth
{
    public class AuthResponseDto
    {
        public string UsuarioId { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;
    }
}
