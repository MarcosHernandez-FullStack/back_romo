using BackRomo.Application.DTOs.Configuracion;
using BackRomo.Application.DTOs.Operacion;
using BackRomo.Application.DTOs.Reserva;
using BackRomo.Application.Interfaces;

namespace BackRomo.Application.Services;

public class OperacionService
{
    private readonly IOperacionRepository  _operacionRepository;
    private readonly IGoogleMapsService    _googleMaps;
    private readonly IConfiguracionRepository _configuracionRepository;

    public OperacionService(
        IOperacionRepository     operacionRepository,
        IGoogleMapsService       googleMaps,
        IConfiguracionRepository configuracionRepository)
    {
        _operacionRepository     = operacionRepository;
        _googleMaps              = googleMaps;
        _configuracionRepository = configuracionRepository;
    }

    public async Task<IEnumerable<ReservaDto>> ListarReservasAsync(string? estadoOperacion, int? id, DateTime? fechaServicio)
        => await _operacionRepository.ListarReservasAsync(estadoOperacion, id, fechaServicio);

    public async Task CancelarReservaAsync(CancelarServicioDto dto)
        => await _operacionRepository.CancelarReservaAsync(dto);

    public async Task<OperacionResultDto> AsignarReservaAsync(AsignarServicioDto dto)
        => await _operacionRepository.AsignarReservaAsync(dto);

    public async Task<SugerenciasDto> SugerirAsignacionAsync(int idReserva)
    {
        var parametro = await _configuracionRepository.ObtenerParametroOperativoAsync()
                        ?? new ParametroDto { MinutosCerca = 15, MinutosMedio = 35 };

        var origen = await _operacionRepository.ObtenerOrigenReservaAsync(idReserva);
        var (candidatosGruas, candidatosOperadores) = await _operacionRepository.ObtenerCandidatosAsync(idReserva);

        var gruas      = await ClasificarGruasAsync(candidatosGruas, origen, parametro);
        var operadores = await ClasificarOperadoresAsync(candidatosOperadores, origen, parametro);

        return new SugerenciasDto { Gruas = gruas, Operadores = operadores };
    }

    private async Task<List<GruaSugeridaDto>> ClasificarGruasAsync(
        IEnumerable<GruaCandidatoDto> candidatos,
        (string lat, string lon)? origen,
        ParametroDto parametro)
    {
        var result = new List<GruaSugeridaDto>();

        foreach (var g in candidatos)
        {
            var dto = new GruaSugeridaDto
            {
                Id        = g.Id,
                Placa     = g.Placa,
                Marca     = g.Marca,
                Modelo    = g.Modelo,
                Capacidad = g.Capacidad
            };

            if (origen is not null && g.UltLatitud is not null && g.UltLongitud is not null)
            {
                var distancia = await _googleMaps.ObtenerDistanciaAsync(
                    g.UltLatitud, g.UltLongitud,
                    origen.Value.lat, origen.Value.lon);

                if (distancia is not null)
                {
                    dto.DistanciaKm   = distancia.Value.distanciaKm;
                    dto.TiempoMin     = distancia.Value.tiempoMin;
                    dto.Clasificacion = Clasificar(distancia.Value.tiempoMin, parametro.MinutosCerca, parametro.MinutosMedio);
                }
            }

            if (string.IsNullOrEmpty(dto.Clasificacion))
                dto.Clasificacion = "SIN UBICACION";

            result.Add(dto);
        }

        return result
            .OrderBy(g => OrdenClasificacion(g.Clasificacion))
            .ThenBy(g => g.TiempoMin ?? int.MaxValue)
            .ToList();
    }

    private async Task<List<OperadorSugeridoDto>> ClasificarOperadoresAsync(
        IEnumerable<OperadorCandidatoDto> candidatos,
        (string lat, string lon)? origen,
        ParametroDto parametro)
    {
        var result = new List<OperadorSugeridoDto>();

        foreach (var o in candidatos)
        {
            var dto = new OperadorSugeridoDto
            {
                Id       = o.Id,
                Nombres  = o.Nombres,
                Apellidos= o.Apellidos
            };

            if (origen is not null && o.UltLatitud is not null && o.UltLongitud is not null)
            {
                var distancia = await _googleMaps.ObtenerDistanciaAsync(
                    o.UltLatitud, o.UltLongitud,
                    origen.Value.lat, origen.Value.lon);

                if (distancia is not null)
                {
                    dto.DistanciaKm   = distancia.Value.distanciaKm;
                    dto.TiempoMin     = distancia.Value.tiempoMin;
                    dto.Clasificacion = Clasificar(distancia.Value.tiempoMin, parametro.MinutosCerca, parametro.MinutosMedio);
                }
            }

            if (string.IsNullOrEmpty(dto.Clasificacion))
                dto.Clasificacion = "SIN UBICACION";

            result.Add(dto);
        }

        return result
            .OrderBy(o => OrdenClasificacion(o.Clasificacion))
            .ThenBy(o => o.TiempoMin ?? int.MaxValue)
            .ToList();
    }

    private static string Clasificar(int tiempoMin, int minutosCerca, int minutosMedio)
    {
        if (tiempoMin <= minutosCerca) return "CERCA";
        if (tiempoMin <= minutosMedio) return "MEDIO";
        return "LEJOS";
    }

    private static int OrdenClasificacion(string clasificacion) => clasificacion switch
    {
        "CERCA"        => 0,
        "MEDIO"        => 1,
        "LEJOS"        => 2,
        "SIN UBICACION"=> 3,
        _              => 4
    };
}
