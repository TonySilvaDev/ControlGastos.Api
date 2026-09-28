using ControlGastos.Api.DTOs.Dashboard;

namespace ControlGastos.Api.Services.Interface
{
    public interface IDashboardService
    {
        Task<DashboardDto> ObtenerDashboardAsync(
            string usuarioId,
            CancellationToken cancellationToken = default);
    }
}
