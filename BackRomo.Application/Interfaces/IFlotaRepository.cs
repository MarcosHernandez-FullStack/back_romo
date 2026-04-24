using BackRomo.Application.DTOs.Flota;

namespace BackRomo.Application.Interfaces;

public interface IFlotaRepository
{
    Task<IEnumerable<UnidadDto>>  ListarGruasAsync(string? estado, string? estadoOperacion, int? id, CancellationToken ct = default);
    Task<UnidadResultDto>         CrearGruaAsync  (CrearUnidadDto dto, CancellationToken ct = default);
    Task<UnidadResultDto>         EditarGruaAsync   (EditarUnidadDto dto,    CancellationToken ct = default);
    Task<UnidadResultDto>         DarDeBajaGruaAsync  (DarDeBajaUnidadDto dto, CancellationToken ct = default);
    Task<IEnumerable<BitaMantDto>> ListarBitaMantAsync (int idGrua,            CancellationToken ct = default);
}
