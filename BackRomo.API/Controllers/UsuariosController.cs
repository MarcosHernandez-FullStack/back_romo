using System.Security.Claims;
using BackRomo.Application.DTOs.Usuario;
using BackRomo.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Timeouts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace BackRomo.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsuariosController : ControllerBase
{
    private readonly UsuarioService _usuarioService;

    public UsuariosController(UsuarioService usuarioService)
    {
        _usuarioService = usuarioService;
    }

    [Authorize(Roles = "ADMINISTRADOR")]
    [EnableRateLimiting("lectura")]
    [RequestTimeout("corto")]
    [HttpGet]
    public async Task<IActionResult> ListarUsuarios(
        [FromQuery] string? estado,
        [FromQuery] int?    id,
        [FromQuery] string? rol,
        CancellationToken   ct)
    {
        var usuarios = await _usuarioService.ListarUsuariosAsync(estado, id, rol, ct);

        if (!usuarios.Any())
            return NoContent();

        return Ok(usuarios);
    }

    [Authorize(Roles = "ADMINISTRADOR")]
    [EnableRateLimiting("escritura")]
    [RequestTimeout("corto")]
    [HttpPost]
    public async Task<IActionResult> CrearUsuario(
        [FromBody] CrearUsuarioDto dto,
        CancellationToken ct)
    {
        dto.CreadoPor = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "0");

        var result = await _usuarioService.CrearUsuarioAsync(dto, ct);

        if (result.Exitoso == 0) return Conflict(result);
        if (result.Exitoso == 2) return Accepted(result);
        return Ok(result);
    }

    [Authorize(Roles = "ADMINISTRADOR")]
    [EnableRateLimiting("escritura")]
    [RequestTimeout("corto")]
    [HttpPut("{idUsuario:int}")]
    public async Task<IActionResult> EditarUsuario(
        int idUsuario,
        [FromBody] EditarUsuarioDto dto,
        CancellationToken ct)
    {
        dto.IdUsuario      = idUsuario;
        dto.ActualizadoPor = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "0");

        var result = await _usuarioService.EditarUsuarioAsync(dto, ct);

        if (result.Exitoso == 0) return Conflict(result);
        if (result.Exitoso == 2) return Accepted(result);
        return Ok(result);
    }
}
