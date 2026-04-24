using System.Security.Claims;
using BackRomo.Application.DTOs.Flota;
using BackRomo.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Timeouts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace BackRomo.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FlotaController : ControllerBase
{
    private readonly FlotaService _flotaService;

    public FlotaController(FlotaService flotaService)
    {
        _flotaService = flotaService;
    }

    [Authorize(Roles = "ADMINISTRADOR")]
    [EnableRateLimiting("lectura")]
    [RequestTimeout("corto")]
    [HttpGet]
    public async Task<IActionResult> ListarGruas(
        [FromQuery] string? estado,
        [FromQuery] string? estadoOperacion,
        [FromQuery] int?    id,
        CancellationToken   ct)
    {
        var gruas = await _flotaService.ListarGruasAsync(estado, estadoOperacion, id, ct);
        return Ok(gruas);
    }

    [Authorize(Roles = "ADMINISTRADOR")]
    [EnableRateLimiting("escritura")]
    [RequestTimeout("corto")]
    [HttpPost]
    public async Task<IActionResult> CrearGrua(
        [FromBody] CrearUnidadDto dto,
        CancellationToken ct)
    {
        dto.CreadoPor = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "0");

        var result = await _flotaService.CrearGruaAsync(dto, ct);

        if (result.Exitoso == 0) return Conflict(result);
        if (result.Exitoso == 2) return Accepted(result);
        return Ok(result);
    }

    [Authorize(Roles = "ADMINISTRADOR")]
    [EnableRateLimiting("escritura")]
    [RequestTimeout("corto")]
    [HttpPut("{idGrua:int}")]
    public async Task<IActionResult> EditarGrua(
        int idGrua,
        [FromBody] EditarUnidadDto dto,
        CancellationToken ct)
    {
        dto.IdGrua         = idGrua;
        dto.ActualizadoPor = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "0");

        var result = await _flotaService.EditarGruaAsync(dto, ct);

        if (result.Exitoso == 0) return Conflict(result);
        if (result.Exitoso == 2) return Accepted(result);
        return Ok(result);
    }

    [Authorize(Roles = "ADMINISTRADOR")]
    [EnableRateLimiting("lectura")]
    [RequestTimeout("corto")]
    [HttpGet("{idGrua:int}/bitacora")]
    public async Task<IActionResult> ListarBitaMant(int idGrua, CancellationToken ct)
    {
        var registros = await _flotaService.ListarBitaMantAsync(idGrua, ct);
        return Ok(registros);
    }

    [Authorize(Roles = "ADMINISTRADOR")]
    [EnableRateLimiting("escritura")]
    [RequestTimeout("corto")]
    [HttpDelete("{idGrua:int}")]
    public async Task<IActionResult> DarDeBajaGrua(
        int idGrua,
        CancellationToken ct)
    {
        var dto = new DarDeBajaUnidadDto
        {
            IdGrua         = idGrua,
            ActualizadoPor = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "0"),
        };

        var result = await _flotaService.DarDeBajaGruaAsync(dto, ct);

        if (result.Exitoso == 0) return Conflict(result);
        if (result.Exitoso == 2) return Accepted(result);
        return Ok(result);
    }
}
