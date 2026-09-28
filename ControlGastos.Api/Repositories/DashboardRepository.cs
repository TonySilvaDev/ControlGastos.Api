using ControlGastos.Api.Data;
using ControlGastos.Api.DTOs.Dashboard;
using ControlGastos.Api.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ControlGastos.Api.Repositories
{
    public class DashboardRepository : IDashboardRepository
    {
        private readonly AppDbContext _context;

        public DashboardRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<DashboardDto> ObtenerDashboardAsync(
            string usuarioId,
            CancellationToken cancellationToken = default)
        {
            var gastos = _context.Gastos
                .AsNoTracking()
                .Where(x => x.UsuarioId == usuarioId);

            var totalIngresos = await gastos
                .Where(x => x.TipoOperacion.Nombre == "Ingreso")
                .SumAsync(x => (decimal?)x.Monto, cancellationToken)
                ?? 0;

            var totalGastos = await gastos
                .Where(x => x.TipoOperacion.Nombre == "Gasto")
                .SumAsync(x => (decimal?)x.Monto, cancellationToken)
                ?? 0;


            var gastosPorCategoria = await gastos
                .Where(x => x.TipoOperacion.Nombre == "Gasto")
                .GroupBy(x => new
                {
                    x.CategoriaId,
                    Categoria = x.Categoria.Nombre
                })
                .Select(x => new ResumenCategoriaDto
                {
                    CategoriaId = x.Key.CategoriaId,
                    Categoria = x.Key.Categoria,
                    Total = x.Sum(y => y.Monto)
                })
                .OrderByDescending(x => x.Total)
                .ToListAsync(cancellationToken);


            var anioActual = DateTime.Now.Year;

            var operacionesAnio = await gastos
                .Where(x => x.Fecha.Year == anioActual)
                .GroupBy(x => new
                {
                    x.Fecha.Year,
                    x.Fecha.Month
                })
                .Select(x => new
                {
                    Anio = x.Key.Year,
                    Mes = x.Key.Month,

                    Ingresos = x
                        .Where(y => y.TipoOperacion.Nombre == "Ingreso")
                        .Sum(y => (decimal?)y.Monto) ?? 0,

                    Gastos = x
                        .Where(y => y.TipoOperacion.Nombre == "Gasto")
                        .Sum(y => (decimal?)y.Monto) ?? 0
                })
                .OrderBy(x => x.Anio)
                .ThenBy(x => x.Mes)
                .ToListAsync(cancellationToken);


            var tendenciaMensual = operacionesAnio
                .Select(x => new TendenciaMensualDto
                {
                    Anio = x.Anio,
                    Mes = x.Mes,
                    NombreMes = ObtenerNombreMes(x.Mes),
                    Ingresos = x.Ingresos,
                    Gastos = x.Gastos
                })
                .ToList();


            var transaccionesRecientes = await gastos
                .OrderByDescending(x => x.Fecha)
                .ThenByDescending(x => x.Id)
                .Take(10)
                .Select(x => new TransaccionRecienteDto
                {
                    Id = x.Id,
                    Fecha = x.Fecha,

                    CategoriaId = x.CategoriaId,

                    Categoria = x.Categoria.Nombre,

                    TipoOperacionId = x.TipoOperacionId,

                    TipoOperacion = x.TipoOperacion.Nombre,

                    Descripcion = x.Descripcion,

                    Monto = x.Monto
                })
                .ToListAsync(cancellationToken);


            return new DashboardDto
            {
                Saldo = totalIngresos - totalGastos,

                TotalIngresos = totalIngresos,

                TotalGastos = totalGastos,

                GastosPorCategoria = gastosPorCategoria,

                TendenciaMensual = tendenciaMensual,

                TransaccionesRecientes = transaccionesRecientes
            };
        }


        private static string ObtenerNombreMes(int mes)
        {
            return mes switch
            {
                1 => "Enero",
                2 => "Febrero",
                3 => "Marzo",
                4 => "Abril",
                5 => "Mayo",
                6 => "Junio",
                7 => "Julio",
                8 => "Agosto",
                9 => "Septiembre",
                10 => "Octubre",
                11 => "Noviembre",
                12 => "Diciembre",
                _ => string.Empty
            };
        }
    }
}
