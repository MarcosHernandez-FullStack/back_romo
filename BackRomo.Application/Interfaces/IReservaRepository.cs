using BackRomo.Application.DTOs.Reserva;

namespace BackRomo.Application.Interfaces;

public interface IReservaRepository
{
    Task<IEnumerable<HorarioDisponibleDto>>  ListarHorariosDisponiblesAsync(DateOnly fecha, string rol, short capacidad);
    Task<ValidarHorarioResultDto>            ValidarHorarioAsync(CrearReservaDto dto);
    Task<ValidarHorarioResultDto>            CrearReservaAsync(ConfirmarReservaDto dto);
    Task                                     EliminarTimerAsync(int idTimer);
}
