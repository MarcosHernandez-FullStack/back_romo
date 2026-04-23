using System.Security.Claims;
using BackRomo.Application.DTOs.Operador;
using BackRomo.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Timeouts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace BackRomo.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OperadoresController : ControllerBase
{
    private readonly OperadorService _operadorService;

    public OperadoresController(OperadorService operadorService)
    {
        _operadorService = operadorService;
    }

    [Authorize(Roles = "ADMINISTRADOR")]
    [EnableRateLimiting("lectura")]
    [RequestTimeout("largo")]
    [HttpGet]
    public async Task<IActionResult> ListarOperadores(
        [FromQuery] string? estado,
        CancellationToken   ct)
    {
        var operadores = await _operadorService.ListarOperadoresAsync(estado, ct);
        return Ok(operadores);
    }

    [Authorize(Roles = "ADMINISTRADOR")]
    [EnableRateLimiting("lectura")]
    [RequestTimeout("largo")]
    [HttpGet("{idOperador:int}/disponibilidad")]
    public async Task<IActionResult> ObtenerDisponibilidad(int idOperador, CancellationToken ct)
    {
        var result = await _operadorService.ObtenerDispOperadorAsync(idOperador, ct);
        return Ok(result);
    }

    [Authorize(Roles = "ADMINISTRADOR")]
    [EnableRateLimiting("escritura")]
    [RequestTimeout("corto")]
    [HttpPost]
    public async Task<IActionResult> CrearOperador(
        [FromBody] CrearOperadorDto dto,
        CancellationToken ct)
    {
        dto.CreadoPor = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "0");

        var result = await _operadorService.CrearOperadorAsync(dto, ct);

        if (result.Exitoso == 0) return Conflict(result);
        if (result.Exitoso == 2) return Accepted(result);
        return Ok(result);
    }

    [Authorize(Roles = "ADMINISTRADOR")]
    [EnableRateLimiting("escritura")]
    [RequestTimeout("corto")]
    [HttpPut("{idOperador:int}")]
    public async Task<IActionResult> EditarOperador(
        int idOperador,
        [FromBody] EditarOperadorDto dto,
        CancellationToken ct)
    {
        dto.IdOperador     = idOperador;
        dto.ActualizadoPor = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "0");

        var result = await _operadorService.EditarOperadorAsync(dto, ct);

        if (result.Exitoso == 0) return Conflict(result);
        if (result.Exitoso == 2) return Accepted(result);
        return Ok(result);
    }

    [Authorize(Roles = "ADMINISTRADOR")]
    [EnableRateLimiting("escritura")]
    [RequestTimeout("corto")]
    [HttpPatch("{idOperador:int}/estado")]
    public async Task<IActionResult> ActualizarEstado(
        int idOperador,
        [FromBody] UpdEstadoOperadorDto dto,
        CancellationToken ct)
    {
        dto.ActualizadoPor = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "0");

        var result = await _operadorService.ActualizarEstadoAsync(idOperador, dto, ct);

        if (result.Exitoso == 0) return Conflict(result);
        if (result.Exitoso == 2) return Accepted(result);
        return Ok(result);
    }

    [Authorize(Roles = "ADMINISTRADOR")]
    [EnableRateLimiting("lectura")]
    [RequestTimeout("corto")]
    [HttpGet("{idOperador:int}/proximos-servicios")]
    public async Task<IActionResult> ObtenerProximosServicios(int idOperador, CancellationToken ct)
    {
        var result = await _operadorService.ObtenerProxServAsync(idOperador, ct);
        return Ok(result);
    }

    [Authorize(Roles = "ADMINISTRADOR")]
    [EnableRateLimiting("escritura")]
    [RequestTimeout("corto")]
    [HttpPost("{idOperador:int}/disponibilidad")]
    public async Task<IActionResult> GuardarDisponibilidad(
        int idOperador,
        [FromBody] AsignarDispOperadorDto dto,
        CancellationToken ct)
    {
        dto.IdOperador     = idOperador;
        dto.ActualizadoPor = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "0");

        var result = await _operadorService.GuardarDispOperadorAsync(dto, ct);

        if (result.Exitoso == 0) return Conflict(result);   // error
        if (result.Exitoso == 2) return Accepted(result);   // timeout
        return Ok(result);                                   // exitoso=1 (éxito) o exitoso=3 (requiere confirmación)
    }
}
