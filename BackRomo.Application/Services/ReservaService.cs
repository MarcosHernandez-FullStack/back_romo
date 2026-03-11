using BackRomo.Application.DTOs.Reserva;
using BackRomo.Application.Interfaces;

namespace BackRomo.Application.Services;

public class ReservaService
{
    private readonly IReservaRepository _reservaRepository;

    public ReservaService(IReservaRepository reservaRepository)
    {
        _reservaRepository = reservaRepository;
    }

    public async Task<IEnumerable<HorarioDisponibleDto>> ListarHorariosDisponiblesAsync(DateOnly fecha, string rol, short capacidad)
        => await _reservaRepository.ListarHorariosDisponiblesAsync(fecha, rol, capacidad);

    public async Task<ValidarHorarioResultDto> ValidarHorarioAsync(CrearReservaDto dto)
        => await _reservaRepository.ValidarHorarioAsync(dto);
}
