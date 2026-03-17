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

    public async Task<IEnumerable<HorarioDto>> ListarHorariosAsync(DateOnly fecha, string rol, short capacidad)
    {
        using var conn = _db.CreateConnection();

        var results = await conn.QueryAsync<SpHorarioResult>(
            "sp_ListHorariosDisponibles",
            new { FechaSeleccionada = fecha.ToDateTime(TimeOnly.MinValue), Rol = rol, Capacidad = capacidad },
            commandType: CommandType.StoredProcedure
        );

        return results
            .Where(r => r.Hora.HasValue)
            .Select(r => new HorarioDto { Hora = r.Hora!.Value, Estado = r.Estado ?? "disponible" });
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
                Rol               = dto.Rol,
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
                TipoHorario          = dto.TipoHorario,
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

        var tvp = new DataTable();
        tvp.Columns.Add("Tipo",        typeof(string));
        tvp.Columns.Add("Placa",       typeof(string));
        tvp.Columns.Add("Descripcion", typeof(string));
        tvp.Columns.Add("Observacion", typeof(string));

        foreach (var v in dto.Vehiculos)
            tvp.Rows.Add(v.Tipo, v.Placa, v.Descripcion, v.Observacion);

        var result = await conn.QueryFirstOrDefaultAsync<ValidarHorarioResultDto>(
            "sp_CreateReserva",
            new
            {
                IdTimerReserva = dto.IdTimerReserva,
                Rol            = dto.Rol,
                Vehiculos      = tvp.AsTableValuedParameter("dbo.TipoVehiculoDetalle"),
            },
            commandType: CommandType.StoredProcedure
        );

        return result ?? new ValidarHorarioResultDto { Exitoso = 0, Mensaje = "Error inesperado al crear la reserva." };
    }

    public async Task EliminarTimerAsync(int idTimer)
    {
        using var conn = _db.CreateConnection();
        await conn.ExecuteAsync(
            "sp_DeleteTimerReserva",
            new { Id = idTimer },
            commandType: CommandType.StoredProcedure
        );
    }

    private class SpHorarioResult
    {
        public TimeSpan? Hora    { get; set; }
        public string?   Estado  { get; set; }
        public int?      Exitoso { get; set; }
        public string?   Mensaje { get; set; }
    }
}
