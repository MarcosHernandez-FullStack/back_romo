using BackRomo.Application.DTOs.Operador;

namespace BackRomo.Application.Interfaces;

public interface IOperadorRepository
{
    Task<IEnumerable<OperadorListDto>> ListarOperadoresAsync(string? estado, CancellationToken ct = default);
}
