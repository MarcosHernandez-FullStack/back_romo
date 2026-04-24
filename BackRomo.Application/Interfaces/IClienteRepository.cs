using BackRomo.Application.DTOs.Cliente;

namespace BackRomo.Application.Interfaces;

public interface IClienteRepository
{
    Task<IEnumerable<ClienteDto>> ListarClientesAsync (string? estado, int? id,   CancellationToken ct = default);
    Task<ClienteResultDto>        CrearClienteAsync   (CrearClienteDto  dto,      CancellationToken ct = default);
    Task<ClienteResultDto>        EditarClienteAsync  (EditarClienteDto dto,      CancellationToken ct = default);
}
