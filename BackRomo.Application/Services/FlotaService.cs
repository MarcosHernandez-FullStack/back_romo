using BackRomo.Application.DTOs.Flota;
using BackRomo.Application.Interfaces;

namespace BackRomo.Application.Services;

public class FlotaService
{
    private readonly IFlotaRepository _flotaRepository;

    public FlotaService(IFlotaRepository flotaRepository)
    {
        _flotaRepository = flotaRepository;
    }

    public Task<GruaPagedDto> ListarGruasAsync(string? estado, string? estadoOperacion, int? id, string? placa, string? marca, string? modelo, int? pagina, int? tamano, CancellationToken ct = default)
        => _flotaRepository.ListarGruasAsync(estado, estadoOperacion, id, placa, marca, modelo, pagina, tamano, ct);

    public async Task<UnidadResultDto> CrearGruaAsync(CrearUnidadDto dto, CancellationToken ct = default)
        => await _flotaRepository.CrearGruaAsync(dto, ct);

    public async Task<UnidadResultDto> EditarGruaAsync(EditarUnidadDto dto, CancellationToken ct = default)
        => await _flotaRepository.EditarGruaAsync(dto, ct);

    public async Task<UnidadResultDto> ActualizarEstadoAsync(UpdEstadoGruaDto dto, CancellationToken ct = default)
        => await _flotaRepository.ActualizarEstadoAsync(dto, ct);

    public async Task<UnidadResultDto> IngresoTallerAsync(IngresoTallerDto dto, CancellationToken ct = default)
        => await _flotaRepository.IngresoTallerAsync(dto, ct);

    public async Task<UnidadResultDto> RetornoOperativaAsync(RetornoOperativaDto dto, CancellationToken ct = default)
        => await _flotaRepository.RetornoOperativaAsync(dto, ct);

    public async Task<IEnumerable<ReservaALiberarDto>> ListarReservasALiberarAsync(int idGrua, CancellationToken ct = default)
        => await _flotaRepository.ListarReservasALiberarAsync(idGrua, ct);

    public async Task<IEnumerable<BitaMantDto>> ListarBitaMantAsync(int idGrua, CancellationToken ct = default)
        => await _flotaRepository.ListarBitaMantAsync(idGrua, ct);
}
