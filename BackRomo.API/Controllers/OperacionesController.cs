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
        [FromQuery] DateTime? fechaServicio,
        [FromQuery] int?      idOperador)
    {
        var reservas = await _operacionService.ListarReservasAsync(estadoOperacion, id, fechaServicio, idOperador);
        return Ok(reservas);
    }

    [HttpPatch("iniciar")]
    public async Task<IActionResult> IniciarReserva([FromBody] IniciarReservaDto dto)
    {
        dto.ActualizadoPor = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "0");
        var result = await _operacionService.IniciarReservaAsync(dto);

        if (result.Exitoso == 0)
            return Conflict(result);

        return Ok(result);
    }

    [HttpPatch("finalizar")]
    public async Task<IActionResult> FinalizarReserva([FromBody] FinalizarReservaDto dto)
    {
        dto.ActualizadoPor = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "0");
        var result = await _operacionService.FinalizarReservaAsync(dto);

        if (result.Exitoso == 0)
            return Conflict(result);

        return Ok(result);
    }

    [HttpPatch("cancelar")]
    public async Task<IActionResult> CancelarReserva([FromBody] CancelarServicioDto dto)
    {
        dto.ActualizadoPor = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "0");
        var result = await _operacionService.CancelarReservaAsync(dto);

        if (result.Exitoso == 0)
            return Conflict(result);

        return Ok(result);
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

        return Ok(result);
    }

    [HttpPatch("reprogramar")]
    public async Task<IActionResult> ReprogramarReserva([FromBody] ReprogramarServicioDto dto)
    {
        dto.ActualizadoPor = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "0");
        dto.Rol            = User.FindFirstValue(ClaimTypes.Role) ?? "ADMINISTRADOR";

        var result = await _operacionService.ReprogramarReservaAsync(dto);

        if (result.Exitoso == 0)
            return Conflict(result);

        return Ok(result);
    }

    [HttpGet("capacidades-gruas")]
    public async Task<IActionResult> ListarCapacidadesGruas()
    {
        var data = await _operacionService.ListarCapacidadesGruasAsync();
        return Ok(data);
    }

    [HttpGet("disponibilidad-gruas")]
    public async Task<IActionResult> ObtenerDisponibilidadGruas(
        [FromQuery] DateOnly fechaServicio,
        [FromQuery] short?   capacidad)
    {
        var data = await _operacionService.ObtenerDisponibilidadGruasAsync(fechaServicio, capacidad);
        return Ok(data);
    }
}
