using System.Security.Claims;
using BackRomo.Application.DTOs.Configuracion;
using BackRomo.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Timeouts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace BackRomo.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ConfiguracionController : ControllerBase
{
    private readonly ConfiguracionService _configuracionService;

    public ConfiguracionController(ConfiguracionService configuracionService)
    {
        _configuracionService = configuracionService;
    }

    [Authorize(Roles = "ADMINISTRADOR,CLIENTE")]
    [EnableRateLimiting("lectura")]
    [RequestTimeout("corto")]
    [HttpGet("tarifario-global")]
    public async Task<IActionResult> ObtenerTarifarioGlobal(CancellationToken ct)
    {
        var tarifa = await _configuracionService.ObtenerTarifarioGlobalAsync(ct);

        if (tarifa is null)
            return NoContent();

        return Ok(tarifa);
    }

    [Authorize(Roles = "ADMINISTRADOR,OPERADOR,CLIENTE")]
    [EnableRateLimiting("lectura")]
    [RequestTimeout("corto")]
    [HttpGet("parametro-operativo")]
    public async Task<IActionResult> ObtenerParametroOperativo(CancellationToken ct)
    {
        var parametro = await _configuracionService.ObtenerParametroOperativoAsync(ct);

        if (parametro is null)
            return NoContent();

        return Ok(parametro);
    }

    [Authorize(Roles = "ADMINISTRADOR,CLIENTE")]
    [EnableRateLimiting("lectura")]
    [RequestTimeout("corto")]
    [HttpGet("publica")]
    public async Task<IActionResult> ObtenerConfigPublica(CancellationToken ct)
    {
        var parametro = await _configuracionService.ObtenerParametroOperativoAsync(ct);

        if (parametro is null)
            return NoContent();

        return Ok(new { reservaClienteOn = parametro.ReservaClienteOn });
    }

    [Authorize(Roles = "ADMINISTRADOR")]
    [EnableRateLimiting("escritura")]
    [RequestTimeout("corto")]
    [HttpPut("tarifario-global")]
    public async Task<IActionResult> ActualizarTarifarioGlobal([FromBody] UpdTarifarioDto dto, CancellationToken ct)
    {
        if (dto.Id <= 0)
            return BadRequest(new { exitoso = 0, mensaje = "El id del tarifario no es válido." });
        var actualizadoPor = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "0");
        if (actualizadoPor == 0)
            return Unauthorized(new { exitoso = 0, mensaje = "No se pudo identificar al usuario autenticado." });

        var result = await _configuracionService.ActualizarTarifarioGlobalAsync(dto, actualizadoPor, ct);

        if (result.Exitoso == 0) return Conflict(result);
        if (result.Exitoso == 2) return Accepted(result);
        return Ok(result);
    }

    [Authorize(Roles = "ADMINISTRADOR")]
    [EnableRateLimiting("escritura")]
    [RequestTimeout("corto")]
    [HttpPatch("{id:int}/reserva-cliente-on")]
    public async Task<IActionResult> ActualizarReservaClienteOn(int id, [FromBody] bool value, CancellationToken ct)
    {
        if (id <= 0)
            return BadRequest(new { exitoso = 0, mensaje = "El id del parámetro no es válido." });
        var actualizadoPor = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "0");
        if (actualizadoPor == 0)
            return Unauthorized(new { exitoso = 0, mensaje = "No se pudo identificar al usuario autenticado." });

        var result = await _configuracionService.ActualizarReservaClienteOnAsync(id, value, actualizadoPor, ct);

        if (result.Exitoso == 0) return Conflict(result);
        if (result.Exitoso == 2) return Accepted(result);
        return Ok(result);
    }

    [Authorize(Roles = "ADMINISTRADOR")]
    [EnableRateLimiting("escritura")]
    [RequestTimeout("corto")]
    [HttpPut("{id:int}/parametro-operativo")]
    public async Task<IActionResult> ActualizarParametroOperativo(int id, [FromBody] UpdParametroDto dto, CancellationToken ct)
    {
        if (id <= 0)
            return BadRequest(new { exitoso = 0, mensaje = "El id del parámetro no es válido." });
        dto.Id = id;
        var actualizadoPor = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "0");
        if (actualizadoPor == 0)
            return Unauthorized(new { exitoso = 0, mensaje = "No se pudo identificar al usuario autenticado." });

        var result = await _configuracionService.ActualizarParametroOperativoAsync(dto, actualizadoPor, ct);

        if (result.Exitoso == 0) return Conflict(result);
        if (result.Exitoso == 2) return Accepted(result);
        return Ok(result);
    }
}
