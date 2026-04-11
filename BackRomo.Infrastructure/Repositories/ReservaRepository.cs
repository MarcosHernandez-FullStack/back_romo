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

    /* 
    //Con sql server
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
    } */
    public async Task<IEnumerable<HorarioDto>> ListarHorariosAsync(DateOnly fecha, string rol, short capacidad)
    {
        using var conn = _db.CreateConnection();

        var results = await conn.QueryAsync<SpHorarioResult>(
            "SELECT * FROM fn_ListHorariosDisponibles(@FechaSeleccionada::date, @Rol, @Capacidad)",
            new
            {
                FechaSeleccionada = fecha.ToDateTime(TimeOnly.MinValue),
                Rol = rol,
                Capacidad = capacidad
            },
            commandType: CommandType.Text
        );

        return results
            .Where(r => r.Hora.HasValue)
            .Select(r => new HorarioDto { Hora = r.Hora!.Value, Estado = r.Estado ?? "disponible" });
    }

    public async Task<IEnumerable<HorarioDto>> ListarHorariosReprogramacionAsync(DateOnly fecha, string rol, short capacidad, int idReserva)
    {
        using var conn = _db.CreateConnection();

        var results = await conn.QueryAsync<SpHorarioResult>(
            "sp_ListHorariosReprogramacion",
            new { FechaSeleccionada = fecha.ToDateTime(TimeOnly.MinValue), Rol = rol, Capacidad = capacidad, IdReserva = idReserva },
            commandType: CommandType.StoredProcedure
        );

        return results
            .Where(r => r.Hora.HasValue)
            .Select(r => new HorarioDto { Hora = r.Hora!.Value, Estado = r.Estado ?? "disponible" });
    }

    /* 
    //Con Sql server
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
    } */

   public async Task<ValidarHorarioResultDto> ValidarHorarioAsync(CrearReservaDto dto)
    {
        using var conn = _db.CreateConnection();

        var parameters = new DynamicParameters();

        // Parámetros de entrada
        parameters.Add("_FechaServicio",        dto.FechaServicio.ToDateTime(TimeOnly.MinValue), dbType: DbType.DateTime);
        parameters.Add("_HoraInicio",           dto.HoraInicio.ToTimeSpan(),                    dbType: DbType.Time);
        parameters.Add("_HoraFin",              dto.HoraFin.ToTimeSpan(),                       dbType: DbType.Time);
        parameters.Add("_CantidadCarga",        dto.CantidadCarga,                              dbType: DbType.Int16);
        parameters.Add("_IdCliente",            dto.IdCliente,                                  dbType: DbType.Int32);
        parameters.Add("_IdOperador",           dto.IdOperador,                                 dbType: DbType.Int32);
        parameters.Add("_CreadoPor",            dto.CreadoPor,                                  dbType: DbType.Int32);
        parameters.Add("_DireccionOrigen",      dto.DireccionOrigen,                            dbType: DbType.String);
        parameters.Add("_CoordLatOrigen",       dto.CoordLatOrigen,                             dbType: DbType.String);
        parameters.Add("_CoordLonOrigen",       dto.CoordLonOrigen,                             dbType: DbType.String);
        parameters.Add("_DireccionDestino",     dto.DireccionDestino,                           dbType: DbType.String);
        parameters.Add("_CoordLatDestino",      dto.CoordLatDestino,                            dbType: DbType.String);
        parameters.Add("_CoordLonDestino",      dto.CoordLonDestino,                            dbType: DbType.String);
        parameters.Add("_DistanciaKm",          dto.DistanciaKm,                                dbType: DbType.Decimal);
        parameters.Add("_TiempoEstimado",       dto.TiempoEstimado,                             dbType: DbType.Int32);
        parameters.Add("_TiempoManiobra",       dto.TiempoManiobra,                             dbType: DbType.Int32);
        parameters.Add("_TiempoRetorno",        dto.TiempoRetorno,                              dbType: DbType.Int32);
        parameters.Add("_NroBloques",           dto.NroBloques,                                 dbType: DbType.Int32);
        parameters.Add("_CostoKm",              dto.CostoKm,                                    dbType: DbType.Decimal);
        parameters.Add("_CostoBase",            dto.CostoBase,                                  dbType: DbType.Decimal);
        parameters.Add("_TimerExpiracion",      dto.TimerExpiracion,                            dbType: DbType.Int16);
        parameters.Add("_EstadoOperacion",      dto.EstadoOperacion,                            dbType: DbType.String);
        parameters.Add("_EstadoAdministrativo", dto.EstadoAdministrativo,                       dbType: DbType.String);
        parameters.Add("_Estado",               dto.Estado,                                     dbType: DbType.String);
        parameters.Add("_FechaCreacion",        dto.FechaCreacion,                              dbType: DbType.DateTime);
        parameters.Add("_Rol",                  dto.Rol,                                        dbType: DbType.String);
        parameters.Add("_TipoHorario",          dto.TipoHorario,                                dbType: DbType.String);

        // Parámetros de salida INOUT
        parameters.Add("_Exitoso",        value: 0,    dbType: DbType.Int32,  direction: ParameterDirection.InputOutput);
        parameters.Add("_Mensaje",        value: "",   dbType: DbType.String, direction: ParameterDirection.InputOutput, size: 500);
        parameters.Add("_HorasConflicto", value: null, dbType: DbType.String, direction: ParameterDirection.InputOutput, size: 500);
        parameters.Add("_Id",             value: null, dbType: DbType.Int32,  direction: ParameterDirection.InputOutput);

        await conn.ExecuteAsync(
            @"CALL sp_ValidarHorario(
                @_FechaServicio::date,
                @_HoraInicio::time,
                @_HoraFin::time,
                @_CantidadCarga::smallint,
                @_IdCliente::integer,
                @_IdOperador::integer,
                @_CreadoPor::integer,
                @_DireccionOrigen::text,
                @_CoordLatOrigen::varchar,
                @_CoordLonOrigen::varchar,
                @_DireccionDestino::text,
                @_CoordLatDestino::varchar,
                @_CoordLonDestino::varchar,
                @_DistanciaKm::numeric,
                @_TiempoEstimado::integer,
                @_TiempoManiobra::integer,
                @_TiempoRetorno::integer,
                @_NroBloques::integer,
                @_CostoKm::numeric,
                @_CostoBase::numeric,
                @_TimerExpiracion::smallint,
                @_EstadoOperacion::varchar,
                @_EstadoAdministrativo::varchar,
                @_Estado::varchar,
                @_FechaCreacion::timestamp,
                @_Rol::varchar,
                @_TipoHorario::varchar,
                @_Exitoso, @_Mensaje, @_HorasConflicto, @_Id)",
            parameters,
            commandType: CommandType.Text
        );

        return new ValidarHorarioResultDto
        {
            Exitoso        = parameters.Get<int>("_Exitoso"),
            Mensaje        = parameters.Get<string>("_Mensaje"),
            HorasConflicto = parameters.Get<string?>("_HorasConflicto"),
            Id             = parameters.Get<int?>("_Id")
        };
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
