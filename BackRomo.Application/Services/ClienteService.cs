using BackRomo.Application.DTOs.Cliente;
using BackRomo.Application.Interfaces;

namespace BackRomo.Application.Services;

public class ClienteService
{
    private readonly IClienteRepository _clienteRepository;

    public ClienteService(IClienteRepository clienteRepository)
    {
        _clienteRepository = clienteRepository;
    }

    public async Task<IEnumerable<ClienteDto>> ListarClientesAsync(string? estado, int? id, CancellationToken ct = default)
        => await _clienteRepository.ListarClientesAsync(estado, id, ct);
}
