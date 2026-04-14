using BackRomo.Application.DTOs.Reserva;
using BackRomo.Application.Services;
using Microsoft.AspNetCore.Mvc;

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

    [HttpGet("horarios")]
    public async Task<IActionResult> ListarHorarios([FromQuery] DateOnly fecha, [FromQuery] string rol, [FromQuery] short capacidad)
    {
        var horarios = await _reservaService.ListarHorariosAsync(fecha, rol, capacidad);

        if (!horarios.Any())
            return NoContent();

        return Ok(horarios);
    }

    [HttpGet("horarios-reprogramacion")]
    public async Task<IActionResult> ListarHorariosReprogramacion([FromQuery] DateOnly fecha, [FromQuery] string rol, [FromQuery] short capacidad, [FromQuery] int idReserva)
    {
        var horarios = await _reservaService.ListarHorariosReprogramacionAsync(fecha, rol, capacidad, idReserva);

        if (!horarios.Any())
            return NoContent();

        return Ok(horarios);
    }

    [HttpPost("validar-horario")]
    public async Task<IActionResult> ValidarHorario([FromBody] CrearReservaDto dto)
    {
        var result = await _reservaService.ValidarHorarioAsync(dto);

        if (result.Exitoso == 0)
            return Conflict(result);

        return Ok(result);
    }

    [HttpPost("crear-reserva")]
    public async Task<IActionResult> CrearReserva([FromBody] ConfirmarReservaDto dto)
    {
        var result = await _reservaService.CrearReservaAsync(dto);

        if (result.Exitoso == 0)
            return Conflict(result);

        return Ok(result);
    }

    [HttpDelete("timer/{id}")]
    public async Task<IActionResult> EliminarTimer(int id)
    {
        var result = await _reservaService.EliminarTimerAsync(id);

        if (result.Exitoso == 0)
            return Conflict(result);

        return NoContent();
    }
}
