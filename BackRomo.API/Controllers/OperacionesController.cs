using System.Security.Claims;
using BackRomo.Application.DTOs.Operacion;
using BackRomo.Application.Services;
using Microsoft.AspNetCore.Mvc;

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

    [HttpGet]
    public async Task<IActionResult> ListarReservas(
        [FromQuery] string?   estadoOperacion,
        [FromQuery] int?      id,
        [FromQuery] DateTime? fechaServicio)
    {
        var reservas = await _operacionService.ListarReservasAsync(estadoOperacion, id, fechaServicio);
        return Ok(reservas);
    }

    [HttpPatch("cancelar")]
    public async Task<IActionResult> CancelarReserva([FromBody] CancelarServicioDto dto)
    {
        dto.ActualizadoPor = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "0");
        await _operacionService.CancelarReservaAsync(dto);
        return NoContent();
    }

    [HttpGet("sugerencias")]
    public async Task<IActionResult> SugerirAsignacion([FromQuery] int idReserva)
    {
        var sugerencias = await _operacionService.SugerirAsignacionAsync(idReserva);
        return Ok(sugerencias);
    }

    [HttpPatch("asignar")]
    public async Task<IActionResult> AsignarServicio([FromBody] AsignarServicioDto dto)
    {
        dto.ActualizadoPor = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "0");

        var result = await _operacionService.AsignarReservaAsync(dto);

        if (result.Exitoso == 0)
            return Conflict(result);

        return NoContent();
    }
}
