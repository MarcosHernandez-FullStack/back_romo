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

    public async Task<IEnumerable<HorarioDisponibleDto>> ListarHorariosDisponiblesAsync(DateOnly fecha, string rol, short capacidad)
    {
        using var conn = _db.CreateConnection();

        var results = await conn.QueryAsync<SpHorarioResult>(
            "sp_ListHorariosDisponibles",
            new { FechaSeleccionada = fecha.ToDateTime(TimeOnly.MinValue), Rol = rol, Capacidad = capacidad },
            commandType: CommandType.StoredProcedure
        );

        return results
            .Where(r => r.HoraDisponible.HasValue)
            .Select(r => new HorarioDisponibleDto { HoraDisponible = r.HoraDisponible!.Value });
    }

    public async Task<ValidarHorarioResultDto> ValidarHorarioAsync(CrearReservaDto dto)
    {
        using var conn = _db.CreateConnection();

        var result = await conn.QueryFirstOrDefaultAsync<ValidarHorarioResultDto>(
            "sp_ValidarHorario",
            new
            {
                FechaServicio     = dto.FechaServicio.ToDateTime(TimeOnly.MinValue),
                HoraInicio        = dto.HoraInicio.ToTimeSpan(),
                HoraFin           = dto.HoraFin.ToTimeSpan(),
                CantidadCarga     = dto.CantidadCarga,
                IdCliente         = dto.IdCliente,
                IdOperador        = dto.IdOperador,
                DireccionOrigen   = dto.DireccionOrigen,
                CoordLatOrigen    = dto.CoordLatOrigen,
                CoordLonOrigen    = dto.CoordLonOrigen,
                DireccionDestino  = dto.DireccionDestino,
                CoordLatDestino   = dto.CoordLatDestino,
                CoordLonDestino   = dto.CoordLonDestino,
                DistanciaKm       = dto.DistanciaKm,
                TiempoEstimado    = dto.TiempoEstimado,
                TiempoManiobra    = dto.TiempoManiobra,
                TiempoRetorno     = dto.TiempoRetorno,
                NroBloques        = dto.NroBloques,
                CostoKm           = dto.CostoKm,
                CostoBase         = dto.CostoBase,
                TimerExpiracion      = dto.TimerExpiracion,
                CreadoPor            = dto.CreadoPor,
                EstadoOperacion      = dto.EstadoOperacion,
                EstadoAdministrativo = dto.EstadoAdministrativo,
                Estado               = dto.Estado,
                FechaCreacion        = dto.FechaCreacion,
            },
            commandType: CommandType.StoredProcedure
        );

        return result ?? new ValidarHorarioResultDto { Exitoso = 0, Mensaje = "Error inesperado al validar el horario." };
    }

    public async Task<ValidarHorarioResultDto> CrearReservaAsync(ConfirmarReservaDto dto)
    {
        using var conn = _db.CreateConnection();

        var result = await conn.QueryFirstOrDefaultAsync<ValidarHorarioResultDto>(
            "sp_CreateReserva",
            new
            {
                IdTimerReserva = dto.IdTimerReserva,
                ActualizadoPor = dto.ActualizadoPor,
            },
            commandType: CommandType.StoredProcedure
        );

        return result ?? new ValidarHorarioResultDto { Exitoso = 0, Mensaje = "Error inesperado al crear la reserva." };
    }

    private class SpHorarioResult
    {
        public TimeSpan? HoraDisponible { get; set; }
        public int?      Exitoso        { get; set; }
        public string?   Mensaje        { get; set; }
    }
}
