using BackRomo.Application.DTOs.Operador;
using BackRomo.Application.Interfaces;

namespace BackRomo.Application.Services;

public class OperadorService
{
    private readonly IOperadorRepository _repo;

    public OperadorService(IOperadorRepository repo)
    {
        _repo = repo;
    }

    public Task<IEnumerable<OperadorListDto>> ListarOperadoresAsync(string? estado, CancellationToken ct = default)
        => _repo.ListarOperadoresAsync(estado, ct);

    public Task<DispOperadorDto> ObtenerDispOperadorAsync(int idOperador, CancellationToken ct = default)
        => _repo.ObtenerDispOperadorAsync(idOperador, ct);

    public Task<DispResultDto> GuardarDispOperadorAsync(AsignarDispOperadorDto dto, CancellationToken ct = default)
        => _repo.GuardarDispOperadorAsync(dto, ct);

    public Task<OperadorResultDto> CrearOperadorAsync(CrearOperadorDto dto, CancellationToken ct = default)
        => _repo.CrearOperadorAsync(dto, ct);

    public Task<OperadorResultDto> ActualizarEstadoAsync(int idOperador, UpdEstadoOperadorDto dto, CancellationToken ct = default)
        => _repo.ActualizarEstadoAsync(idOperador, dto, ct);

    public Task<OperadorResultDto> EditarOperadorAsync(EditarOperadorDto dto, CancellationToken ct = default)
        => _repo.EditarOperadorAsync(dto, ct);

    public Task<IEnumerable<ProxServOperadorDto>> ObtenerProxServAsync(int idOperador, CancellationToken ct = default)
        => _repo.ObtenerProxServAsync(idOperador, ct);
}
