using BackRomo.Application.DTOs.Reserva;

namespace BackRomo.Application.Interfaces;

public interface IReservaRepository
{
    Task<IEnumerable<HorarioDto>>  ListarHorariosAsync(DateOnly fecha, string rol, short capacidad);
    Task<IEnumerable<HorarioDto>>  ListarHorariosReprogramacionAsync(DateOnly fecha, string rol, short capacidad, int idReserva);
    Task<ValidarHorarioResultDto>            ValidarHorarioAsync(CrearReservaDto dto);
    Task<ValidarHorarioResultDto>            CrearReservaAsync(ConfirmarReservaDto dto);
    Task<ValidarHorarioResultDto>            EliminarTimerAsync(int idTimer);
}
