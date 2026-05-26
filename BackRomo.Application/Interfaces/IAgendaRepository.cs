using BackRomo.Application.DTOs.Agenda;

namespace BackRomo.Application.Interfaces;

public interface IAgendaRepository
{
    Task<IEnumerable<HorarioDto>>    ListarConfiguracionHorarioAsync(string? rol, string? estado, CancellationToken ct = default);
    Task<AgendaResultDto>            ActualizarConfiguracionHorarioAsync(UpdConfiguracionHorarioDto dto, CancellationToken ct = default);
    Task<ExcepcionPagedDto>          ListarExcepcionesAsync(string? estado, int? id, string? motivo, DateOnly? fechaInicio, DateOnly? fechaFin, int? pagina, int? tamano, CancellationToken ct = default);
    Task<AgendaResultDto>            CreUpdExcepcionAsync(CrearExcepcionDto dto, CancellationToken ct = default);
    Task<AgendaResultDto>            UpdEstadoExcepcionAsync(int id, UpdEstadoExcepcionDto dto, CancellationToken ct = default);
}
