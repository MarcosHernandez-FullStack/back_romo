using BackRomo.Application.DTOs.Configuracion;

namespace BackRomo.Application.Interfaces;

public interface IConfiguracionRepository
{
    Task<TarifaDto?> ObtenerTarifarioGlobalAsync();
}
