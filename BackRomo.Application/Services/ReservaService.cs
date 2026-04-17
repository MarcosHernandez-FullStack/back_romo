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

    public async Task<IEnumerable<HorarioDto>> ListarHorariosAsync(DateOnly fecha, string rol, short capacidad, CancellationToken ct = default)
        => await _reservaRepository.ListarHorariosAsync(fecha, rol, capacidad, ct);

    public async Task<IEnumerable<HorarioDto>> ListarHorariosReprogramacionAsync(DateOnly fecha, string rol, short capacidad, int idReserva, CancellationToken ct = default)
        => await _reservaRepository.ListarHorariosReprogramacionAsync(fecha, rol, capacidad, idReserva, ct);

    public async Task<ValidarHorarioResultDto> ValidarHorarioAsync(CrearReservaDto dto, CancellationToken ct = default)
    {
        dto.FechaCreacion = await _fechaService.AhoraAsync();
        return await _reservaRepository.ValidarHorarioAsync(dto, ct);
    }

    public async Task<ValidarHorarioResultDto> CrearReservaAsync(ConfirmarReservaDto dto, CancellationToken ct = default)
        => await _reservaRepository.CrearReservaAsync(dto, ct);

    public async Task<ValidarHorarioResultDto> EliminarTimerAsync(int idTimer, CancellationToken ct = default)
        => await _reservaRepository.EliminarTimerAsync(idTimer, ct);
}
