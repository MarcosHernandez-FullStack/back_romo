using BackRomo.Application.DTOs.Reporte;

namespace BackRomo.Application.Interfaces;

public interface IReporteRepository
{
    Task<ReportePagedDto> ListarReportesAsync(
        int?    id,
        int?    idCliente,
        string? fechaDesde,
        string? fechaHasta,
        string? estadoOperacion,
        string? estadoAdministrativo,
        string? placa,
        string? empresa,
        int?    pagina,
        int?    tamano,
        CancellationToken ct = default);

    Task<ReporteResultDto> UpdEstadoAdministrativoAsync(UpdEstadoAdministrativoDto dto, CancellationToken ct = default);
}
