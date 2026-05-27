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
        [FromQuery] string? nombre,
        [FromQuery] string? correo,
        [FromQuery] string? rol,
        [FromQuery] int?    pagina,
        [FromQuery] int?    tamano,
        CancellationToken   ct)
    {
        var result = await _usuarioService.ListarUsuariosAsync(estado, id, nombre, correo, rol, pagina, tamano, ct);

        if (result.Total == 0)
            return NoContent();

        return Ok(result);
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
        if (dto.CreadoPor == 0)
            return Unauthorized(new { exitoso = 0, mensaje = "No se pudo identificar al usuario autenticado." });

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
        if (idUsuario <= 0)
            return BadRequest(new { exitoso = 0, mensaje = "El id del usuario no es válido." });
        dto.IdUsuario      = idUsuario;
        dto.ActualizadoPor = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "0");
        if (dto.ActualizadoPor == 0)
            return Unauthorized(new { exitoso = 0, mensaje = "No se pudo identificar al usuario autenticado." });

        var result = await _usuarioService.EditarUsuarioAsync(dto, ct);

        if (result.Exitoso == 0) return Conflict(result);
        if (result.Exitoso == 2) return Accepted(result);
        return Ok(result);
    }

    [Authorize(Roles = "ADMINISTRADOR")]
    [EnableRateLimiting("escritura")]
    [RequestTimeout("corto")]
    [HttpPatch("{idUsuario:int}/estado")]
    public async Task<IActionResult> ActualizarEstadoUsuario(
        int idUsuario,
        [FromBody] UpdEstadoUsuarioDto dto,
        CancellationToken ct)
    {
        if (idUsuario <= 0)
            return BadRequest(new { exitoso = 0, mensaje = "El id del usuario no es válido." });

        dto.IdUsuario      = idUsuario;
        dto.ActualizadoPor = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "0");
        if (dto.ActualizadoPor == 0)
            return Unauthorized(new { exitoso = 0, mensaje = "No se pudo identificar al usuario autenticado." });

        var result = await _usuarioService.ActualizarEstadoUsuarioAsync(dto, ct);

        if (result.Exitoso == 0) return Conflict(result);
        return Ok(result);
    }
}
