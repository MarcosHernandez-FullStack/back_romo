using BackRomo.Application.DTOs.Configuracion;
using BackRomo.Application.Interfaces;

namespace BackRomo.Application.Services;

public class ConfiguracionService
{
    private readonly IConfiguracionRepository _configuracionRepository;

    public ConfiguracionService(IConfiguracionRepository configuracionRepository)
    {
        _configuracionRepository = configuracionRepository;
    }

    public async Task<TarifaDto?> ObtenerTarifarioGlobalAsync(CancellationToken ct = default)
        => await _configuracionRepository.ObtenerTarifarioGlobalAsync(ct);

    public async Task<ParametroDto?> ObtenerParametroOperativoAsync(CancellationToken ct = default)
        => await _configuracionRepository.ObtenerParametroOperativoAsync(ct);
}
