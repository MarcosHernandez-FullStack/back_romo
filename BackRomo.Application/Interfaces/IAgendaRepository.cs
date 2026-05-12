using BackRomo.Application.DTOs.Agenda;

namespace BackRomo.Application.Interfaces;

public interface IAgendaRepository
{
    Task<IEnumerable<HorarioDto>> ListarConfiguracionHorarioAsync(string? rol, string? estado, CancellationToken ct = default);
    Task<AgendaResultDto>         ActualizarConfiguracionHorarioAsync(UpdConfiguracionHorarioDto dto, CancellationToken ct = default);
}
