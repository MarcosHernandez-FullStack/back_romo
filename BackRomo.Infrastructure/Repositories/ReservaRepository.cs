using System.Data;
using BackRomo.Application.DTOs.Reserva;
using BackRomo.Application.Interfaces;
using BackRomo.Infrastructure.Data;
using Dapper;
using Npgsql;
using NpgsqlTypes;

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

    /*
    //Con sql server
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
    } */
    public async Task<IEnumerable<HorarioDto>> ListarHorariosReprogramacionAsync(DateOnly fecha, string rol, short capacidad, int idReserva)
    {
        using var conn = _db.CreateConnection();

        var results = await conn.QueryAsync<SpHorarioResult>(
            "SELECT * FROM fn_ListHorariosReprogramacion(@FechaSeleccionada::date, @Rol, @Capacidad, @IdReserva)",
            new
            {
                FechaSeleccionada = fecha.ToDateTime(TimeOnly.MinValue),
                Rol               = rol,
                Capacidad         = capacidad,
                IdReserva         = idReserva
            },
            commandType: CommandType.Text
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
        try
        {
            var p = new DynamicParameters();
            p.Add("_FechaServicio",        dto.FechaServicio.ToDateTime(TimeOnly.MinValue), DbType.Date);
            p.Add("_HoraInicio",           dto.HoraInicio.ToTimeSpan(),                    DbType.Time);
            p.Add("_HoraFin",              dto.HoraFin.ToTimeSpan(),                       DbType.Time);
            p.Add("_CantidadCarga",        dto.CantidadCarga,        DbType.Int16);
            p.Add("_IdCliente",            dto.IdCliente,            DbType.Int32);
            p.Add("_IdOperador",           dto.IdOperador,           DbType.Int32);
            p.Add("_CreadoPor",            dto.CreadoPor,            DbType.Int32);
            p.Add("_DireccionOrigen",      dto.DireccionOrigen,      DbType.String);
            p.Add("_CoordLatOrigen",       dto.CoordLatOrigen,       DbType.String);
            p.Add("_CoordLonOrigen",       dto.CoordLonOrigen,       DbType.String);
            p.Add("_DireccionDestino",     dto.DireccionDestino,     DbType.String);
            p.Add("_CoordLatDestino",      dto.CoordLatDestino,      DbType.String);
            p.Add("_CoordLonDestino",      dto.CoordLonDestino,      DbType.String);
            p.Add("_DistanciaKm",          dto.DistanciaKm,          DbType.Decimal);
            p.Add("_TiempoEstimado",       dto.TiempoEstimado,       DbType.Int32);
            p.Add("_TiempoManiobra",       dto.TiempoManiobra,       DbType.Int32);
            p.Add("_TiempoRetorno",        dto.TiempoRetorno,        DbType.Int32);
            p.Add("_NroBloques",           dto.NroBloques,           DbType.Int32);
            p.Add("_CostoKm",              dto.CostoKm,              DbType.Decimal);
            p.Add("_CostoBase",            dto.CostoBase,            DbType.Decimal);
            p.Add("_TimerExpiracion",      dto.TimerExpiracion,      DbType.Int16);
            p.Add("_EstadoOperacion",      dto.EstadoOperacion,      DbType.String);
            p.Add("_EstadoAdministrativo", dto.EstadoAdministrativo, DbType.String);
            p.Add("_Estado",               dto.Estado,               DbType.String);
            p.Add("_FechaCreacion",        dto.FechaCreacion,        DbType.DateTime);
            p.Add("_Rol",                  dto.Rol,                  DbType.String);
            p.Add("_TipoHorario",          dto.TipoHorario,          DbType.String);
            p.Add("_Exitoso",        value: 0,    dbType: DbType.Int32,  direction: ParameterDirection.InputOutput);
            p.Add("_Mensaje",        value: "",   dbType: DbType.String, direction: ParameterDirection.InputOutput, size: 500);
            p.Add("_HorasConflicto", value: null, dbType: DbType.String, direction: ParameterDirection.InputOutput, size: 500);
            p.Add("_Id",             value: null, dbType: DbType.Int32,  direction: ParameterDirection.InputOutput);

            await conn.ExecuteAsync(
                @"CALL sp_ValidarHorario(
                    @_FechaServicio::date, @_HoraInicio::time, @_HoraFin::time,
                    @_CantidadCarga::smallint, @_IdCliente::integer, @_IdOperador::integer, @_CreadoPor::integer,
                    @_DireccionOrigen::text, @_CoordLatOrigen::varchar, @_CoordLonOrigen::varchar,
                    @_DireccionDestino::text, @_CoordLatDestino::varchar, @_CoordLonDestino::varchar,
                    @_DistanciaKm::numeric, @_TiempoEstimado::integer, @_TiempoManiobra::integer,
                    @_TiempoRetorno::integer, @_NroBloques::integer, @_CostoKm::numeric, @_CostoBase::numeric,
                    @_TimerExpiracion::smallint, @_EstadoOperacion::varchar, @_EstadoAdministrativo::varchar,
                    @_Estado::varchar, @_FechaCreacion::timestamp, @_Rol::varchar, @_TipoHorario::varchar,
                    @_Exitoso, @_Mensaje, @_HorasConflicto, @_Id)",
                p, commandType: CommandType.Text);

            return new ValidarHorarioResultDto
            {
                Exitoso        = p.Get<int>("_Exitoso"),
                Mensaje        = p.Get<string>("_Mensaje"),
                HorasConflicto = p.Get<string?>("_HorasConflicto"),
                Id             = p.Get<int?>("_Id")
            };
        }
        catch (Exception ex)
        {
            return new ValidarHorarioResultDto { Exitoso = 0, Mensaje = ex.Message };
        }
    }

    /*
    //Con sql server
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
    } */
    public async Task<ValidarHorarioResultDto> CrearReservaAsync(ConfirmarReservaDto dto)
    {
        using var conn = (NpgsqlConnection)_db.CreateConnection();
        await conn.OpenAsync();

        try
        {
            var pExitoso        = new NpgsqlParameter("_Exitoso",        NpgsqlDbType.Integer) { Value = 0,            Direction = ParameterDirection.InputOutput };
            var pMensaje        = new NpgsqlParameter("_Mensaje",        NpgsqlDbType.Text)    { Value = "",           Direction = ParameterDirection.InputOutput };
            var pHorasConflicto = new NpgsqlParameter("_HorasConflicto", NpgsqlDbType.Text)    { Value = DBNull.Value, Direction = ParameterDirection.InputOutput };
            var pId             = new NpgsqlParameter("_Id",             NpgsqlDbType.Integer) { Value = DBNull.Value, Direction = ParameterDirection.InputOutput };

            using var cmd = new NpgsqlCommand("", conn);
            cmd.Parameters.AddWithValue("_IdTimerReserva", dto.IdTimerReserva);
            cmd.Parameters.AddWithValue("_Rol", dto.Rol);
            cmd.Parameters.Add(pExitoso);
            cmd.Parameters.Add(pMensaje);
            cmd.Parameters.Add(pHorasConflicto);
            cmd.Parameters.Add(pId);

            string vehiculosExpr;
            if (dto.Vehiculos.Count == 0)
            {
                vehiculosExpr = "ARRAY[]::tipo_vehiculo_detalle[]";
            }
            else
            {
                var rows = new List<string>();
                for (int i = 0; i < dto.Vehiculos.Count; i++)
                {
                    var v = dto.Vehiculos[i];
                    cmd.Parameters.AddWithValue($"_v_tipo_{i}",  (object?)v.Tipo        ?? DBNull.Value);
                    cmd.Parameters.AddWithValue($"_v_placa_{i}", (object?)v.Placa       ?? DBNull.Value);
                    cmd.Parameters.AddWithValue($"_v_desc_{i}",  (object?)v.Descripcion ?? DBNull.Value);
                    cmd.Parameters.AddWithValue($"_v_obs_{i}",   (object?)v.Observacion ?? DBNull.Value);
                    rows.Add($"ROW(@_v_tipo_{i}, @_v_placa_{i}, @_v_desc_{i}, @_v_obs_{i})::tipo_vehiculo_detalle");
                }
                vehiculosExpr = $"ARRAY[{string.Join(", ", rows)}]";
            }

            cmd.CommandText = $"CALL sp_CreateReserva(@_IdTimerReserva, {vehiculosExpr}, @_Rol, @_Exitoso, @_Mensaje, @_HorasConflicto, @_Id)";

            await cmd.ExecuteNonQueryAsync();

            return new ValidarHorarioResultDto
            {
                Exitoso        = (int)pExitoso.Value!,
                Mensaje        = (string)pMensaje.Value!,
                HorasConflicto = pHorasConflicto.Value == DBNull.Value ? null : (string)pHorasConflicto.Value,
                Id             = pId.Value == DBNull.Value ? null : (int?)pId.Value
            };
        }
        catch (Exception ex)
        {
            return new ValidarHorarioResultDto { Exitoso = 0, Mensaje = ex.Message };
        }
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
