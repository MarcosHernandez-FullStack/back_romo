using BackRomo.Application.DTOs.Configuracion;

namespace BackRomo.Application.Interfaces;

public interface IConfiguracionRepository
{
    Task<TarifaDto?>       ObtenerTarifarioGlobalAsync(CancellationToken ct = default);
    Task<ParametroDto?>    ObtenerParametroOperativoAsync(CancellationToken ct = default);
    Task<ConfigResultDto>  ActualizarReservaClienteOnAsync(bool value, int actualizadoPor, CancellationToken ct = default);
}
