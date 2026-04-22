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
}
