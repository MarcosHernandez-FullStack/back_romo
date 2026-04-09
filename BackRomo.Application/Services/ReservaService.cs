using BackRomo.Application.DTOs.Reserva;
using BackRomo.Application.Interfaces;

namespace BackRomo.Application.Services;

public class ReservaService
{
    private readonly IReservaRepository _reservaRepository;
    private readonly IFechaService      _fechaService;

    public ReservaService(IReservaRepository reservaRepository, IFechaService fechaService)
    {
        _reservaRepository = reservaRepository;
        _fechaService      = fechaService;
    }

    public async Task<IEnumerable<HorarioDto>> ListarHorariosAsync(DateOnly fecha, string rol, short capacidad)
        => await _reservaRepository.ListarHorariosAsync(fecha, rol, capacidad);

    public async Task<IEnumerable<HorarioDto>> ListarHorariosReprogramacionAsync(DateOnly fecha, string rol, short capacidad, int idReserva)
        => await _reservaRepository.ListarHorariosReprogramacionAsync(fecha, rol, capacidad, idReserva);

    public async Task<ValidarHorarioResultDto> ValidarHorarioAsync(CrearReservaDto dto)
    {
        dto.FechaCreacion = await _fechaService.AhoraAsync();
        return await _reservaRepository.ValidarHorarioAsync(dto);
    }

    public async Task<ValidarHorarioResultDto> CrearReservaAsync(ConfirmarReservaDto dto)
        => await _reservaRepository.CrearReservaAsync(dto);

    public async Task EliminarTimerAsync(int idTimer)
        => await _reservaRepository.EliminarTimerAsync(idTimer);
}
