using System.Data;
using BackRomo.Application.DTOs.Operacion;
using BackRomo.Application.DTOs.Reserva;
using BackRomo.Application.Interfaces;
using BackRomo.Infrastructure.Data;
using Dapper;

namespace BackRomo.Infrastructure.Repositories;

public class OperacionRepository : IOperacionRepository
{
    private readonly DbConnectionFactory _db;

    public OperacionRepository(DbConnectionFactory db)
    {
        _db = db;
    }

    public async Task<IEnumerable<ReservaDto>> ListarReservasAsync(string? estadoOperacion, int? id)
    {
        using var conn = _db.CreateConnection();

        return await conn.QueryAsync<ReservaDto>(
            "sp_ListReservas",
            new { EstadoOperacion = estadoOperacion, Id = id },
            commandType: CommandType.StoredProcedure
        );
    }

    public async Task CancelarReservaAsync(CancelarServicioDto dto)
    {
        using var conn = _db.CreateConnection();

        await conn.ExecuteAsync(
            "sp_CancelarReserva",
            new { dto.Id, dto.MotivoCancelacion },
            commandType: CommandType.StoredProcedure
        );
    }

    public async Task<(IEnumerable<GruaCandidatoDto> gruas, IEnumerable<OperadorCandidatoDto> operadores)> ObtenerCandidatosAsync(int idReserva)
    {
        using var conn  = _db.CreateConnection();
        using var multi = await conn.QueryMultipleAsync(
            "sp_SugerirAsignacion",
            new { IdReserva = idReserva },
            commandType: CommandType.StoredProcedure
        );

        var gruas      = await multi.ReadAsync<GruaCandidatoDto>();
        var operadores = await multi.ReadAsync<OperadorCandidatoDto>();

        return (gruas, operadores);
    }

    public async Task<(string latOrigen, string lonOrigen)?> ObtenerOrigenReservaAsync(int idReserva)
    {
        using var conn = _db.CreateConnection();

        var result = await conn.QueryFirstOrDefaultAsync<(string, string)?>(
            "SELECT CoordLatOrigen, CoordLonOrigen FROM Reserva WHERE Id = @Id",
            new { Id = idReserva }
        );

        return result;
    }
}
