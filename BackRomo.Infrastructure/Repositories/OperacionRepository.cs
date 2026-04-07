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

    public async Task<IEnumerable<ReservaDto>> ListarReservasAsync(string? estadoOperacion, int? id, DateTime? fechaServicio)
    {
        using var conn = _db.CreateConnection();

        return await conn.QueryAsync<ReservaDto>(
            "sp_ListReservas",
            new { EstadoOperacion = estadoOperacion, Id = id, FechaServicio = fechaServicio },
            commandType: CommandType.StoredProcedure
        );
    }

    public async Task CancelarReservaAsync(CancelarServicioDto dto)
    {
        using var conn = _db.CreateConnection();

        await conn.ExecuteAsync(
            "sp_CancelarReserva",
            new { dto.Id, dto.MotivoCancelacion, dto.ActualizadoPor },
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

    public async Task<OperacionResultDto> AsignarReservaAsync(AsignarServicioDto dto)
    {
        using var conn = _db.CreateConnection();

        return await conn.QueryFirstOrDefaultAsync<OperacionResultDto>(
            "sp_AsignarServicio",
            new { dto.IdReserva, dto.IdGrua, dto.IdOperador, dto.ActualizadoPor },
            commandType: CommandType.StoredProcedure
        ) ?? new OperacionResultDto { Exitoso = 0, Mensaje = "Error inesperado al asignar el servicio." };
    }

    public async Task<OperacionResultDto> ReprogramarReservaAsync(ReprogramarServicioDto dto)
    {
        using var conn = _db.CreateConnection();

        var horaInicio = TimeSpan.Parse(dto.NuevaHoraInicio);

        return await conn.QueryFirstOrDefaultAsync<OperacionResultDto>(
            "sp_ReprogramarReserva",
            new
            {
                dto.IdReserva,
                NuevaFecha      = dto.NuevaFecha.Date,
                NuevaHoraInicio = horaInicio,
                dto.NuevoNroBloques,
                dto.ActualizadoPor,
            },
            commandType: CommandType.StoredProcedure
        ) ?? new OperacionResultDto { Exitoso = 0, Mensaje = "Error inesperado al reprogramar la reserva." };
    }

    public async Task<(string latOrigen, string lonOrigen)?> ObtenerOrigenReservaAsync(int idReserva)
    {
        using var conn = _db.CreateConnection();

        var result = await conn.QueryFirstOrDefaultAsync<OrigenReserva>(
            "SELECT CoordLatOrigen, CoordLonOrigen FROM Reserva WHERE Id = @Id",
            new { Id = idReserva }
        );

        if (result is null) return null;
        return (result.CoordLatOrigen, result.CoordLonOrigen);
    }

    private class OrigenReserva
    {
        public string CoordLatOrigen { get; set; } = string.Empty;
        public string CoordLonOrigen { get; set; } = string.Empty;
    }
}
