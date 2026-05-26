using BackRomo.Application.DTOs.Agenda;
using BackRomo.Application.Interfaces;

namespace BackRomo.Application.Services;

public class AgendaService
{
    private readonly IAgendaRepository _agendaRepository;

    public AgendaService(IAgendaRepository agendaRepository)
    {
        _agendaRepository = agendaRepository;
    }

    public async Task<IEnumerable<HorarioDto>> ListarConfiguracionHorarioAsync(string? rol, string? estado, CancellationToken ct = default)
        => await _agendaRepository.ListarConfiguracionHorarioAsync(rol, estado, ct);

    public async Task<AgendaResultDto> ActualizarConfiguracionHorarioAsync(UpdConfiguracionHorarioDto dto, CancellationToken ct = default)
        => await _agendaRepository.ActualizarConfiguracionHorarioAsync(dto, ct);

    public async Task<ExcepcionPagedDto> ListarExcepcionesAsync(string? estado, int? id, string? motivo, DateOnly? fechaInicio, DateOnly? fechaFin, int? pagina, int? tamano, CancellationToken ct = default)
        => await _agendaRepository.ListarExcepcionesAsync(estado, id, motivo, fechaInicio, fechaFin, pagina, tamano, ct);

    public async Task<AgendaResultDto> CreUpdExcepcionAsync(CrearExcepcionDto dto, CancellationToken ct = default)
        => await _agendaRepository.CreUpdExcepcionAsync(dto, ct);

    public async Task<AgendaResultDto> UpdEstadoExcepcionAsync(int id, UpdEstadoExcepcionDto dto, CancellationToken ct = default)
        => await _agendaRepository.UpdEstadoExcepcionAsync(id, dto, ct);
}
