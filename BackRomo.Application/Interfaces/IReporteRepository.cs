using BackRomo.Application.DTOs.Reporte;

namespace BackRomo.Application.Interfaces;

public interface IReporteRepository
{
    Task<IEnumerable<ReporteDto>> ListarReportesAsync(
        string? busqueda,
        int?    idCliente,
        string? fechaDesde,
        string? fechaHasta,
        string? estadoOperacion,
        string? estadoAdministrativo,
        CancellationToken ct = default);

    Task<ReporteResultDto> UpdEstadoAdministrativoAsync(UpdEstadoAdministrativoDto dto, CancellationToken ct = default);
}
