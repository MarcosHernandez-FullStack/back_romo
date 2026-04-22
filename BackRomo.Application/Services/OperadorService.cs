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
}
