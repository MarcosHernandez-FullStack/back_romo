using BackRomo.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace BackRomo.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReservasController : ControllerBase
{
    private readonly ReservaService _reservaService;

    public ReservasController(ReservaService reservaService)
    {
        _reservaService = reservaService;
    }

    [HttpGet("horarios-disponibles")]
    public async Task<IActionResult> ListarHorariosDisponibles([FromQuery] DateOnly fecha, [FromQuery] string rol)
    {
        var horarios = await _reservaService.ListarHorariosDisponiblesAsync(fecha, rol);

        if (!horarios.Any())
            return NoContent();

        return Ok(horarios);
    }
}
