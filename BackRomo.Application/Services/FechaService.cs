using BackRomo.Application.Interfaces;

namespace BackRomo.Application.Services;

/// <summary>
/// Provee la fecha/hora local del sistema usando la ZonaHoraria
/// configurada en ParametroOperativo. Cachea el TimeZoneInfo para
/// evitar consultas repetidas a la base de datos.
/// </summary>
public class FechaService : IFechaService
{
    private readonly IConfiguracionRepository _configuracionRepository;
    private TimeZoneInfo? _zonaCache;

    public FechaService(IConfiguracionRepository configuracionRepository)
    {
        _configuracionRepository = configuracionRepository;
    }

    public async Task<DateTime> AhoraAsync()
    {
        _zonaCache ??= await CargarZonaAsync();
        return TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, _zonaCache);
    }

    private async Task<TimeZoneInfo> CargarZonaAsync()
    {
        var parametro = await _configuracionRepository.ObtenerParametroOperativoAsync();
        if (parametro is null || string.IsNullOrWhiteSpace(parametro.ZonaHoraria))
            return TimeZoneInfo.Utc;

        try
        {
            return TimeZoneInfo.FindSystemTimeZoneById(parametro.ZonaHoraria.Trim());
        }
        catch (TimeZoneNotFoundException)
        {
            return TimeZoneInfo.Utc;
        }
    }
}
