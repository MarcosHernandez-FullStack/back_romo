using System.Security.Claims;
using BackRomo.Application.DTOs.Agenda;
using BackRomo.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Timeouts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace BackRomo.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AgendaController : ControllerBase
{
    private readonly AgendaService _agendaService;

    public AgendaController(AgendaService agendaService)
    {
        _agendaService = agendaService;
    }

    [Authorize(Roles = "ADMINISTRADOR")]
    [EnableRateLimiting("lectura")]
    [RequestTimeout("corto")]
    [HttpGet("horarios")]
    public async Task<IActionResult> ListarConfiguracionHorario(
        [FromQuery] string? rol,
        [FromQuery] string? estado,
        CancellationToken   ct)
    {
        var horarios = await _agendaService.ListarConfiguracionHorarioAsync(rol, estado, ct);

        if (!horarios.Any())
            return NoContent();

        return Ok(horarios);
    }

    [Authorize(Roles = "ADMINISTRADOR")]
    [EnableRateLimiting("lectura")]
    [RequestTimeout("corto")]
    [HttpGet("excepciones")]
    public async Task<IActionResult> ListarExcepciones(
        [FromQuery] string? estado,
        [FromQuery] int?    id,
        CancellationToken   ct)
    {
        var excepciones = await _agendaService.ListarExcepcionesAsync(estado, id, ct);

        if (!excepciones.Any())
            return NoContent();

        return Ok(excepciones);
    }

    [Authorize(Roles = "ADMINISTRADOR")]
    [EnableRateLimiting("escritura")]
    [RequestTimeout("corto")]
    [HttpPost("excepciones")]
    public async Task<IActionResult> CreUpdExcepcion(
        [FromBody] CrearExcepcionDto dto,
        CancellationToken ct)
    {
        dto.UsuarioId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "0");

        var result = await _agendaService.CreUpdExcepcionAsync(dto, ct);

        if (result.Exitoso == 0) return Conflict(result);
        if (result.Exitoso == 2) return Accepted(result);
        return Ok(result);
    }

    [Authorize(Roles = "ADMINISTRADOR")]
    [EnableRateLimiting("escritura")]
    [RequestTimeout("corto")]
    [HttpPatch("excepciones/{id:int}/estado")]
    public async Task<IActionResult> UpdEstadoExcepcion(
        int id,
        [FromBody] UpdEstadoExcepcionDto dto,
        CancellationToken ct)
    {
        dto.ActualizadoPor = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "0");

        var result = await _agendaService.UpdEstadoExcepcionAsync(id, dto, ct);

        if (result.Exitoso == 0) return Conflict(result);
        if (result.Exitoso == 2) return Accepted(result);
        return Ok(result);
    }

    [Authorize(Roles = "ADMINISTRADOR")]
    [EnableRateLimiting("escritura")]
    [RequestTimeout("corto")]
    [HttpPut("horarios")]
    public async Task<IActionResult> ActualizarConfiguracionHorario(
        [FromBody] UpdConfiguracionHorarioDto dto,
        CancellationToken ct)
    {
        dto.ActualizadoPor = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "0");

        var result = await _agendaService.ActualizarConfiguracionHorarioAsync(dto, ct);

        if (result.Exitoso == 0) return Conflict(result);
        if (result.Exitoso == 2) return Accepted(result);
        return Ok(result);
    }
}
