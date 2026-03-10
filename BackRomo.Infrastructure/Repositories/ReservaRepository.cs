using System.Data;
using BackRomo.Application.DTOs.Reserva;
using BackRomo.Application.Interfaces;
using BackRomo.Infrastructure.Data;
using Dapper;

namespace BackRomo.Infrastructure.Repositories;

public class ReservaRepository : IReservaRepository
{
    private readonly DbConnectionFactory _db;

    public ReservaRepository(DbConnectionFactory db)
    {
        _db = db;
    }

    public async Task<IEnumerable<HorarioDisponibleDto>> ListarHorariosDisponiblesAsync(DateOnly fecha, string rol)
    {
        using var conn = _db.CreateConnection();

        var results = await conn.QueryAsync<SpHorarioResult>(
            "sp_ListHorariosDisponibles",
            new { FechaSeleccionada = fecha.ToDateTime(TimeOnly.MinValue), Rol = rol },
            commandType: CommandType.StoredProcedure
        );

        return results
            .Where(r => r.HoraDisponible.HasValue)
            .Select(r => new HorarioDisponibleDto { HoraDisponible = r.HoraDisponible!.Value });
    }

    private class SpHorarioResult
    {
        public TimeSpan? HoraDisponible { get; set; }
        public int?      Exitoso        { get; set; }
        public string?   Mensaje        { get; set; }
    }
}
