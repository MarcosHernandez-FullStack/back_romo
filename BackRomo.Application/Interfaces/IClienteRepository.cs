using BackRomo.Application.DTOs.Cliente;

namespace BackRomo.Application.Interfaces;

public interface IClienteRepository
{
    Task<ClientePagedDto> ListarClientesAsync(string? estado, int? id, string? empresa, string? contacto, int? pagina, int? tamano, CancellationToken ct = default);
    Task<ClienteResultDto> CrearClienteAsync          (CrearClienteDto      dto, CancellationToken ct = default);
    Task<ClienteResultDto> EditarClienteAsync         (EditarClienteDto     dto, CancellationToken ct = default);
    Task<ClienteResultDto> ActualizarEstadoClienteAsync(UpdEstadoClienteDto dto, CancellationToken ct = default);
}
