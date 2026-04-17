using BackRomo.Application.DTOs.Operacion;
using BackRomo.Application.DTOs.Reserva;

namespace BackRomo.Application.Interfaces;

public interface IOperacionRepository
{
    Task<IEnumerable<ReservaDto>> ListarReservasAsync(string? estadoOperacion, int? id, DateTime? fechaServicio, int? idOperador, CancellationToken ct = default);
    Task<OperacionResultDto> IniciarReservaAsync(IniciarReservaDto dto, CancellationToken ct = default);
    Task<OperacionResultDto> FinalizarReservaAsync(FinalizarReservaDto dto, CancellationToken ct = default);
    Task<OperacionResultDto> CancelarReservaAsync(CancelarServicioDto dto, CancellationToken ct = default);
    Task<(IEnumerable<GruaCandidatoDto> gruas, IEnumerable<OperadorCandidatoDto> operadores)> ObtenerCandidatosAsync(int idReserva, CancellationToken ct = default);
    Task<(string latOrigen, string lonOrigen)?> ObtenerOrigenReservaAsync(int idReserva, CancellationToken ct = default);
    Task<OperacionResultDto> AsignarReservaAsync(AsignarServicioDto dto, CancellationToken ct = default);
    Task<OperacionResultDto> ReprogramarReservaAsync(ReprogramarServicioDto dto, CancellationToken ct = default);
    Task<IEnumerable<short>> ListarCapacidadesGruasAsync(CancellationToken ct = default);
    Task<IEnumerable<DisponibilidadGruaDto>> ObtenerDisponibilidadGruasAsync(DateOnly fechaServicio, short? capacidad, CancellationToken ct = default);
}
