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

    public async Task<ClienteResultDto> CrearClienteAsync(CrearClienteDto dto, CancellationToken ct = default)
        => await _clienteRepository.CrearClienteAsync(dto, ct);

    public async Task<ClienteResultDto> EditarClienteAsync(EditarClienteDto dto, CancellationToken ct = default)
        => await _clienteRepository.EditarClienteAsync(dto, ct);
}
