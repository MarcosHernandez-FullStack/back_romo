using BackRomo.Application.DTOs.Cliente;

namespace BackRomo.Application.Interfaces;

public interface IClienteRepository
{
    Task<IEnumerable<ClienteDto>> ListarClientesAsync(string? estado, int? id, CancellationToken ct = default);
}
