using BackRomo.Application.DTOs.Reserva;

namespace BackRomo.Application.Interfaces;

public interface IReservaRepository
{
    Task<IEnumerable<HorarioDisponibleDto>> ListarHorariosDisponiblesAsync(DateOnly fecha, string rol);
}
