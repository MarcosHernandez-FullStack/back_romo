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

    public async Task<IEnumerable<ReservaDto>> ListarReservasAsync(string? estadoOperacion, int? id, DateTime? fechaServicio, int? idOperador, int? idGrua, CancellationToken ct = default)
        => await _operacionRepository.ListarReservasAsync(estadoOperacion, id, fechaServicio, idOperador, idGrua, ct);

    public async Task<OperacionResultDto> IniciarReservaAsync(IniciarReservaDto dto, CancellationToken ct = default)
        => await _operacionRepository.IniciarReservaAsync(dto, ct);

    public async Task<OperacionResultDto> FinalizarReservaAsync(FinalizarReservaDto dto, CancellationToken ct = default)
        => await _operacionRepository.FinalizarReservaAsync(dto, ct);

    public async Task<OperacionResultDto> CancelarReservaAsync(CancelarServicioDto dto, CancellationToken ct = default)
        => await _operacionRepository.CancelarReservaAsync(dto, ct);

    public async Task<OperacionResultDto> AsignarReservaAsync(AsignarServicioDto dto, CancellationToken ct = default)
        => await _operacionRepository.AsignarReservaAsync(dto, ct);

    public async Task<OperacionResultDto> ReprogramarReservaAsync(ReprogramarServicioDto dto, CancellationToken ct = default)
        => await _operacionRepository.ReprogramarReservaAsync(dto, ct);

    public async Task<IEnumerable<short>> ListarCapacidadesGruasAsync(CancellationToken ct = default)
        => await _operacionRepository.ListarCapacidadesGruasAsync(ct);

    public async Task<IEnumerable<DisponibilidadGruaDto>> ObtenerDisponibilidadGruasAsync(DateOnly fechaServicio, short? capacidad, CancellationToken ct = default)
        => await _operacionRepository.ObtenerDisponibilidadGruasAsync(fechaServicio, capacidad, ct);

    public async Task<SugerenciasDto> SugerirAsignacionAsync(int idReserva, CancellationToken ct = default)
    {
        var parametro = await _configuracionRepository.ObtenerParametroOperativoAsync(ct)
                        ?? new ParametroDto { MinutosCerca = 15, MinutosMedio = 35 };

        var origen = await _operacionRepository.ObtenerOrigenReservaAsync(idReserva, ct);
        var (candidatosGruas, candidatosOperadores) = await _operacionRepository.ObtenerCandidatosAsync(idReserva, ct);

        var gruas      = await ClasificarGruasAsync(candidatosGruas, origen, parametro, ct);
        var operadores = await ClasificarOperadoresAsync(candidatosOperadores, origen, parametro, ct);

        return new SugerenciasDto { Gruas = gruas, Operadores = operadores };
    }

    private async Task<List<GruaSugeridaDto>> ClasificarGruasAsync(
        IEnumerable<GruaCandidatoDto> candidatos,
        (string lat, string lon)? origen,
        ParametroDto parametro,
        CancellationToken ct)
    {
        var result = new List<GruaSugeridaDto>();

        foreach (var g in candidatos)
        {
            ct.ThrowIfCancellationRequested();

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
        ParametroDto parametro,
        CancellationToken ct)
    {
        var result = new List<OperadorSugeridoDto>();

        foreach (var o in candidatos)
        {
            ct.ThrowIfCancellationRequested();

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
