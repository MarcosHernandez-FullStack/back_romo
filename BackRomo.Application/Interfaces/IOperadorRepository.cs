using BackRomo.Application.DTOs.Operador;

namespace BackRomo.Application.Interfaces;

public interface IOperadorRepository
{
    Task<IEnumerable<OperadorListDto>> ListarOperadoresAsync(string? estado, CancellationToken ct = default);
    Task<DispOperadorDto>              ObtenerDispOperadorAsync(int idOperador, CancellationToken ct = default);
    Task<DispResultDto>                GuardarDispOperadorAsync(AsignarDispOperadorDto dto, CancellationToken ct = default);
    Task<OperadorResultDto>            CrearOperadorAsync(CrearOperadorDto dto, CancellationToken ct = default);
    Task<OperadorResultDto>            ActualizarEstadoAsync(int idOperador, UpdEstadoOperadorDto dto, CancellationToken ct = default);
    Task<OperadorResultDto>                 EditarOperadorAsync(EditarOperadorDto dto, CancellationToken ct = default);
    Task<IEnumerable<ProxServOperadorDto>> ObtenerProxServAsync(int idOperador, CancellationToken ct = default);
}
