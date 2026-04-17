using BackRomo.Application.DTOs.Reserva;
using BackRomo.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Timeouts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace BackRomo.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReservasController : ControllerBase
{
    private readonly ReservaService _reservaService;

    public ReservasController(ReservaService reservaService)
    {
        _reservaService = reservaService;
    }

    [Authorize(Roles = "CLIENTE,ADMINISTRADOR")]
    [EnableRateLimiting("lectura")]
    [RequestTimeout("largo")]
    [HttpGet("horarios")]
    public async Task<IActionResult> ListarHorarios(
        [FromQuery] DateOnly  fecha,
        [FromQuery] string    rol,
        [FromQuery] short     capacidad,
        CancellationToken     ct)
    {
        var horarios = await _reservaService.ListarHorariosAsync(fecha, rol, capacidad, ct);

        if (!horarios.Any())
            return NoContent();

        return Ok(horarios);
    }

    [Authorize(Roles = "ADMINISTRADOR")]
    [EnableRateLimiting("lectura")]
    [RequestTimeout("largo")]
    [HttpGet("horarios-reprogramacion")]
    public async Task<IActionResult> ListarHorariosReprogramacion(
        [FromQuery] DateOnly  fecha,
        [FromQuery] string    rol,
        [FromQuery] short     capacidad,
        [FromQuery] int       idReserva,
        CancellationToken     ct)
    {
        var horarios = await _reservaService.ListarHorariosReprogramacionAsync(fecha, rol, capacidad, idReserva, ct);

        if (!horarios.Any())
            return NoContent();

        return Ok(horarios);
    }

    [Authorize(Roles = "CLIENTE,ADMINISTRADOR")]
    [EnableRateLimiting("escritura")]
    [RequestTimeout("largo")]
    [HttpPost("validar-horario")]
    public async Task<IActionResult> ValidarHorario([FromBody] CrearReservaDto dto, CancellationToken ct)
    {
        var result = await _reservaService.ValidarHorarioAsync(dto, ct);

        if (result.Exitoso == 0)
            return Conflict(result);

        return Ok(result);
    }

    [Authorize(Roles = "CLIENTE,ADMINISTRADOR")]
    [EnableRateLimiting("escritura")]
    [RequestTimeout("largo")]
    [HttpPost("crear-reserva")]
    public async Task<IActionResult> CrearReserva([FromBody] ConfirmarReservaDto dto, CancellationToken ct)
    {
        var result = await _reservaService.CrearReservaAsync(dto, ct);

        if (result.Exitoso == 0)
            return Conflict(result);

        return Ok(result);
    }

    [Authorize(Roles = "CLIENTE,ADMINISTRADOR")]
    [EnableRateLimiting("escritura")]
    [RequestTimeout("corto")]
    [HttpDelete("timer/{id}")]
    public async Task<IActionResult> EliminarTimer(int id, CancellationToken ct)
    {
        var result = await _reservaService.EliminarTimerAsync(id, ct);

        if (result.Exitoso == 0)
            return Conflict(result);

        return NoContent();
    }
}
