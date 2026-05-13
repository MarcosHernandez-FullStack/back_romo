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

    public async Task<IEnumerable<ReporteDto>> ListarReportesAsync(
        string? busqueda,
        int?    idCliente,
        string? fechaDesde,
        string? fechaHasta,
        string? estadoOperacion,
        string? estadoAdministrativo,
        CancellationToken ct = default)
        => await _reporteRepository.ListarReportesAsync(
            busqueda, idCliente, fechaDesde, fechaHasta, estadoOperacion, estadoAdministrativo, ct);

    public async Task<ReporteResultDto> UpdEstadoAdministrativoAsync(UpdEstadoAdministrativoDto dto, CancellationToken ct = default)
        => await _reporteRepository.UpdEstadoAdministrativoAsync(dto, ct);
}
