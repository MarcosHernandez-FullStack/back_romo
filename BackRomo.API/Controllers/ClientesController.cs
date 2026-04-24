using System.Security.Claims;
using BackRomo.Application.DTOs.Cliente;
using BackRomo.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Timeouts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace BackRomo.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClientesController : ControllerBase
{
    private readonly ClienteService _clienteService;

    public ClientesController(ClienteService clienteService)
    {
        _clienteService = clienteService;
    }

    [Authorize(Roles = "ADMINISTRADOR")]
    [EnableRateLimiting("lectura")]
    [RequestTimeout("corto")]
    [HttpGet]
    public async Task<IActionResult> ListarClientes(
        [FromQuery] string? estado,
        [FromQuery] int?    id,
        CancellationToken   ct)
    {
        var clientes = await _clienteService.ListarClientesAsync(estado, id, ct);

        if (!clientes.Any())
            return NoContent();

        return Ok(clientes);
    }

    [Authorize(Roles = "ADMINISTRADOR")]
    [EnableRateLimiting("escritura")]
    [RequestTimeout("corto")]
    [HttpPost]
    public async Task<IActionResult> CrearCliente(
        [FromBody] CrearClienteDto dto,
        CancellationToken ct)
    {
        dto.CreadoPor = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "0");

        var result = await _clienteService.CrearClienteAsync(dto, ct);

        if (result.Exitoso == 0) return Conflict(result);
        if (result.Exitoso == 2) return Accepted(result);
        return Ok(result);
    }

    [Authorize(Roles = "ADMINISTRADOR")]
    [EnableRateLimiting("escritura")]
    [RequestTimeout("corto")]
    [HttpPut("{idCliente:int}")]
    public async Task<IActionResult> EditarCliente(
        int idCliente,
        [FromBody] EditarClienteDto dto,
        CancellationToken ct)
    {
        dto.IdCliente      = idCliente;
        dto.ActualizadoPor = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "0");

        var result = await _clienteService.EditarClienteAsync(dto, ct);

        if (result.Exitoso == 0) return Conflict(result);
        if (result.Exitoso == 2) return Accepted(result);
        return Ok(result);
    }
}
