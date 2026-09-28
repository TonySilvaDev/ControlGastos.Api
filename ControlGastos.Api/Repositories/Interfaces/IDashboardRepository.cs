using ControlGastos.Api.DTOs.Dashboard;

namespace ControlGastos.Api.Repositories.Interfaces
{
    public interface IDashboardRepository
    {
        Task<DashboardDto> ObtenerDashboardAsync(
            string usuarioId,
            CancellationToken cancellationToken = default);
    }
}
