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

    public async Task<ClientePagedDto> ListarClientesAsync(string? estado, int? id, string? empresa, string? contacto, int? pagina, int? tamano, CancellationToken ct = default)
        => await _clienteRepository.ListarClientesAsync(estado, id, empresa, contacto, pagina, tamano, ct);

    public async Task<ClienteResultDto> CrearClienteAsync(CrearClienteDto dto, CancellationToken ct = default)
        => await _clienteRepository.CrearClienteAsync(dto, ct);

    public async Task<ClienteResultDto> EditarClienteAsync(EditarClienteDto dto, CancellationToken ct = default)
        => await _clienteRepository.EditarClienteAsync(dto, ct);
}
