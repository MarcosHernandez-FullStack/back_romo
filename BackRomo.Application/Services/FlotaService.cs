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

    public async Task<IEnumerable<UnidadDto>> ListarGruasAsync(string? estado, string? estadoOperacion, int? id, CancellationToken ct = default)
        => await _flotaRepository.ListarGruasAsync(estado, estadoOperacion, id, ct);

    public async Task<UnidadResultDto> CrearGruaAsync(CrearUnidadDto dto, CancellationToken ct = default)
        => await _flotaRepository.CrearGruaAsync(dto, ct);

    public async Task<UnidadResultDto> EditarGruaAsync(EditarUnidadDto dto, CancellationToken ct = default)
        => await _flotaRepository.EditarGruaAsync(dto, ct);

    public async Task<UnidadResultDto> DarDeBajaGruaAsync(DarDeBajaUnidadDto dto, CancellationToken ct = default)
        => await _flotaRepository.DarDeBajaGruaAsync(dto, ct);

    public async Task<IEnumerable<BitaMantDto>> ListarBitaMantAsync(int idGrua, CancellationToken ct = default)
        => await _flotaRepository.ListarBitaMantAsync(idGrua, ct);
}
