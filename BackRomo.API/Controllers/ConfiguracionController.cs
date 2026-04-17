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

    [Authorize(Roles = "ADMINISTRADOR")]
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

    [Authorize(Roles = "ADMINISTRADOR")]
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
}
