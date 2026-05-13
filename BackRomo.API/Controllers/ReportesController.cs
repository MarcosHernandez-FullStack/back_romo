using System.Security.Claims;
using BackRomo.Application.DTOs.Reporte;
using BackRomo.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Timeouts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace BackRomo.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReportesController : ControllerBase
{
    private readonly ReporteService _reporteService;

    public ReportesController(ReporteService reporteService)
    {
        _reporteService = reporteService;
    }

    [Authorize(Roles = "ADMINISTRADOR")]
    [EnableRateLimiting("lectura")]
    [RequestTimeout("corto")]
    [HttpGet]
    public async Task<IActionResult> ListarReportes(
        [FromQuery] string? busqueda,
        [FromQuery] int?    idCliente,
        [FromQuery] string? fechaDesde,
        [FromQuery] string? fechaHasta,
        [FromQuery] string? estadoOperacion,
        [FromQuery] string? estadoAdministrativo,
        CancellationToken   ct)
    {
        var reportes = await _reporteService.ListarReportesAsync(
            busqueda, idCliente, fechaDesde, fechaHasta, estadoOperacion, estadoAdministrativo, ct);

        if (!reportes.Any())
            return NoContent();

        return Ok(reportes);
    }

    [Authorize(Roles = "ADMINISTRADOR")]
    [EnableRateLimiting("escritura")]
    [RequestTimeout("corto")]
    [HttpPut("{idReserva:int}/estado-administrativo")]
    public async Task<IActionResult> UpdEstadoAdministrativo(
        int idReserva,
        [FromBody] UpdEstadoAdministrativoDto dto,
        CancellationToken ct)
    {
        dto.IdReserva      = idReserva;
        dto.ActualizadoPor = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "0");

        var result = await _reporteService.UpdEstadoAdministrativoAsync(dto, ct);

        if (result.Exitoso == 0) return Conflict(result);
        if (result.Exitoso == 2) return Accepted(result);
        return Ok(result);
    }
}
