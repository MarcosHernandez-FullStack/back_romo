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
        var actualizadoPor = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "0");
        var result = await _configuracionService.ActualizarTarifarioGlobalAsync(dto, actualizadoPor, ct);

        if (result.Exitoso == 0) return Conflict(result);
        if (result.Exitoso == 2) return Accepted(result);
        return Ok(result);
    }

    [Authorize(Roles = "ADMINISTRADOR")]
    [EnableRateLimiting("escritura")]
    [RequestTimeout("corto")]
    [HttpPatch("reserva-cliente-on")]
    public async Task<IActionResult> ActualizarReservaClienteOn([FromBody] bool value, CancellationToken ct)
    {
        var actualizadoPor = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "0");
        var result = await _configuracionService.ActualizarReservaClienteOnAsync(value, actualizadoPor, ct);

        if (result.Exitoso == 0) return Conflict(result);
        if (result.Exitoso == 2) return Accepted(result);
        return Ok(result);
    }

    [Authorize(Roles = "ADMINISTRADOR")]
    [EnableRateLimiting("escritura")]
    [RequestTimeout("corto")]
    [HttpPut("parametro-operativo")]
    public async Task<IActionResult> ActualizarParametroOperativo([FromBody] UpdParametroDto dto, CancellationToken ct)
    {
        var actualizadoPor = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "0");
        var result = await _configuracionService.ActualizarParametroOperativoAsync(dto, actualizadoPor, ct);

        if (result.Exitoso == 0) return Conflict(result);
        if (result.Exitoso == 2) return Accepted(result);
        return Ok(result);
    }
}
