using BackRomo.Application.DTOs.Reserva;

namespace BackRomo.Application.Interfaces;

public interface IReservaRepository
{
    Task<IEnumerable<HorarioDto>> ListarHorariosAsync(DateOnly fecha, string rol, short capacidad, CancellationToken ct = default);
    Task<IEnumerable<HorarioDto>> ListarHorariosReprogramacionAsync(DateOnly fecha, string rol, short capacidad, int idReserva, CancellationToken ct = default);
    Task<ValidarHorarioResultDto> ValidarHorarioAsync(CrearReservaDto dto, CancellationToken ct = default);
    Task<ValidarHorarioResultDto> CrearReservaAsync(ConfirmarReservaDto dto, CancellationToken ct = default);
    Task<ValidarHorarioResultDto> EliminarTimerAsync(int idTimer, CancellationToken ct = default);
}
