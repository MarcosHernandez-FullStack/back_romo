using BackRomo.Application.Services;
using Microsoft.AspNetCore.Mvc;

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

    [HttpGet]
    public async Task<IActionResult> ListarClientes(
        [FromQuery] string? estado,
        [FromQuery] int?    id)
    {
        var clientes = await _clienteService.ListarClientesAsync(estado, id);

        if (!clientes.Any())
            return NoContent();

        return Ok(clientes);
    }
}
