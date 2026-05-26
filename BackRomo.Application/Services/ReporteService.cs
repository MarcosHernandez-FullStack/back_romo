using BackRomo.Application.DTOs.Reporte;
using BackRomo.Application.Interfaces;

namespace BackRomo.Application.Services;

public class ReporteService
{
    private readonly IReporteRepository _reporteRepository;

    public ReporteService(IReporteRepository reporteRepository)
    {
        _reporteRepository = reporteRepository;
    }

    public async Task<ReportePagedDto> ListarReportesAsync(
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
        CancellationToken ct = default)
        => await _reporteRepository.ListarReportesAsync(
            id, idCliente, fechaDesde, fechaHasta, estadoOperacion, estadoAdministrativo, placa, empresa, pagina, tamano, ct);

    public async Task<ReporteResultDto> UpdEstadoAdministrativoAsync(UpdEstadoAdministrativoDto dto, CancellationToken ct = default)
        => await _reporteRepository.UpdEstadoAdministrativoAsync(dto, ct);
}
