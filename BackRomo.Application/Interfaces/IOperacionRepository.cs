using BackRomo.Application.DTOs.Operacion;
using BackRomo.Application.DTOs.Reserva;

namespace BackRomo.Application.Interfaces;

public interface IOperacionRepository
{
    Task<IEnumerable<ReservaDto>> ListarReservasAsync(string? estadoOperacion, int? id, DateTime? fechaServicio);
    Task CancelarReservaAsync(CancelarServicioDto dto);
    Task<(IEnumerable<GruaCandidatoDto> gruas, IEnumerable<OperadorCandidatoDto> operadores)> ObtenerCandidatosAsync(int idReserva);
    Task<(string latOrigen, string lonOrigen)?> ObtenerOrigenReservaAsync(int idReserva);
    Task<OperacionResultDto> AsignarReservaAsync(AsignarServicioDto dto);
}
