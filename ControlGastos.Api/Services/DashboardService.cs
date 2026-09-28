using ControlGastos.Api.DTOs.Dashboard;
using ControlGastos.Api.Repositories.Interfaces;
using ControlGastos.Api.Services.Interface;

namespace ControlGastos.Api.Services
{
    public class DashboardService : IDashboardService
    {
        private readonly IDashboardRepository _dashboardRepository;

        public DashboardService(
            IDashboardRepository dashboardRepository)
        {
            _dashboardRepository = dashboardRepository;
        }

        public async Task<DashboardDto> ObtenerDashboardAsync(
            string usuarioId,
            CancellationToken cancellationToken = default)
        {
            return await _dashboardRepository
                .ObtenerDashboardAsync(
                    usuarioId,
                    cancellationToken);
        }
    }
}
