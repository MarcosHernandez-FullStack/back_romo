using BackRomo.Application.Services;
using Microsoft.AspNetCore.Mvc;

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

    [HttpGet("tarifario-global")]
    public async Task<IActionResult> ObtenerTarifarioGlobal()
    {
        var tarifa = await _configuracionService.ObtenerTarifarioGlobalAsync();

        if (tarifa is null)
            return NoContent();

        return Ok(tarifa);
    }
}
