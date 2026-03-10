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

    public async Task<TarifaDto?> ObtenerTarifarioGlobalAsync()
        => await _configuracionRepository.ObtenerTarifarioGlobalAsync();
}
