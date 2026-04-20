using System.Security.Claims;
using BackRomo.Application.DTOs.Operacion;
using BackRomo.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Timeouts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace BackRomo.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OperacionesController : ControllerBase
{
    private readonly OperacionService _operacionService;

    public OperacionesController(OperacionService operacionService)
    {
        _operacionService = operacionService;
    }

    [Authorize(Roles = "CLIENTE,ADMINISTRADOR,OPERADOR")]
    [EnableRateLimiting("lectura")]
    [RequestTimeout("largo")]
    [HttpGet]
    public async Task<IActionResult> ListarReservas(
        [FromQuery] string?   estadoOperacion,
        [FromQuery] int?      id,
        [FromQuery] DateTime? fechaServicio,
        [FromQuery] int?      idOperador,
        CancellationToken     ct)
    {
        var reservas = await _operacionService.ListarReservasAsync(estadoOperacion, id, fechaServicio, idOperador, ct);
        return Ok(reservas);
    }

    [Authorize(Roles = "OPERADOR")]
    [EnableRateLimiting("escritura")]
    [RequestTimeout("corto")]
    [HttpPatch("iniciar")]
    public async Task<IActionResult> IniciarReserva([FromBody] IniciarReservaDto dto, CancellationToken ct)
    {
        dto.ActualizadoPor = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "0");
        var result = await _operacionService.IniciarReservaAsync(dto, ct);

        if (result.Exitoso == 0) return Conflict(result);
        if (result.Exitoso == 2) return Accepted(result);

        return Ok(result);
    }

    [Authorize(Roles = "OPERADOR")]
    [EnableRateLimiting("escritura")]
    [RequestTimeout("corto")]
    [HttpPatch("finalizar")]
    public async Task<IActionResult> FinalizarReserva([FromBody] FinalizarReservaDto dto, CancellationToken ct)
    {
        dto.ActualizadoPor = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "0");
        var result = await _operacionService.FinalizarReservaAsync(dto, ct);

        if (result.Exitoso == 0) return Conflict(result);
        if (result.Exitoso == 2) return Accepted(result);

        return Ok(result);
    }

    [Authorize(Roles = "ADMINISTRADOR")]
    [EnableRateLimiting("escritura")]
    [RequestTimeout("corto")]
    [HttpPatch("cancelar")]
    public async Task<IActionResult> CancelarReserva([FromBody] CancelarServicioDto dto, CancellationToken ct)
    {
        dto.ActualizadoPor = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "0");
        var result = await _operacionService.CancelarReservaAsync(dto, ct);

        if (result.Exitoso == 0) return Conflict(result);
        if (result.Exitoso == 2) return Accepted(result);

        return Ok(result);
    }

    [Authorize(Roles = "ADMINISTRADOR")]
    [EnableRateLimiting("lectura")]
    [RequestTimeout("largo")]
    [HttpGet("sugerencias")]
    public async Task<IActionResult> SugerirAsignacion([FromQuery] int idReserva, CancellationToken ct)
    {
        var sugerencias = await _operacionService.SugerirAsignacionAsync(idReserva, ct);
        return Ok(sugerencias);
    }

    [Authorize(Roles = "ADMINISTRADOR")]
    [EnableRateLimiting("escritura")]
    [RequestTimeout("corto")]
    [HttpPatch("asignar")]
    public async Task<IActionResult> AsignarServicio([FromBody] AsignarServicioDto dto, CancellationToken ct)
    {
        dto.ActualizadoPor = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "0");

        var result = await _operacionService.AsignarReservaAsync(dto, ct);

        if (result.Exitoso == 0) return Conflict(result);
        if (result.Exitoso == 2) return Accepted(result);

        return Ok(result);
    }

    [Authorize(Roles = "ADMINISTRADOR")]
    [EnableRateLimiting("escritura")]
    [RequestTimeout("corto")]
    [HttpPatch("reprogramar")]
    public async Task<IActionResult> ReprogramarReserva([FromBody] ReprogramarServicioDto dto, CancellationToken ct)
    {
        dto.ActualizadoPor = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "0");
        dto.Rol            = User.FindFirstValue(ClaimTypes.Role) ?? "ADMINISTRADOR";

        var result = await _operacionService.ReprogramarReservaAsync(dto, ct);

        if (result.Exitoso == 0) return Conflict(result);
        if (result.Exitoso == 2) return Accepted(result);

        return Ok(result);
    }

    [Authorize(Roles = "ADMINISTRADOR")]
    [EnableRateLimiting("lectura")]
    [RequestTimeout("corto")]
    [HttpGet("capacidades-gruas")]
    public async Task<IActionResult> ListarCapacidadesGruas(CancellationToken ct)
    {
        var data = await _operacionService.ListarCapacidadesGruasAsync(ct);
        return Ok(data);
    }

    [Authorize(Roles = "ADMINISTRADOR")]
    [EnableRateLimiting("lectura")]
    [RequestTimeout("corto")]
    [HttpGet("disponibilidad-gruas")]
    public async Task<IActionResult> ObtenerDisponibilidadGruas(
        [FromQuery] DateOnly  fechaServicio,
        [FromQuery] short?    capacidad,
        CancellationToken     ct)
    {
        var data = await _operacionService.ObtenerDisponibilidadGruasAsync(fechaServicio, capacidad, ct);
        return Ok(data);
    }
}
