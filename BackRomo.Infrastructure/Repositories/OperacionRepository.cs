using System.Data;
using System.Text.Json;
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

    /*
    //Con sql server
    public async Task<IEnumerable<ReservaDto>> ListarReservasAsync(string? estadoOperacion, int? id, DateTime? fechaServicio)
    {
        using var conn = _db.CreateConnection();

        return await conn.QueryAsync<ReservaDto>(
            "sp_ListReservas",
            new { EstadoOperacion = estadoOperacion, Id = id, FechaServicio = fechaServicio },
            commandType: CommandType.StoredProcedure
        );
    } */
    public async Task<IEnumerable<ReservaDto>> ListarReservasAsync(string? estadoOperacion, int? id, DateTime? fechaServicio, int? idOperador)
    {
        using var conn = _db.CreateConnection();

        var rows = await conn.QueryAsync<ReservaDapperRow>(
            "SELECT * FROM fn_ListReservas(@EstadoOperacion, @Id, @FechaServicio::date, @IdOperador)",
            new { EstadoOperacion = estadoOperacion, Id = id, FechaServicio = fechaServicio, IdOperador = idOperador },
            commandType: CommandType.Text
        );

        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

        return rows.Select(r => new ReservaDto
        {
            Id               = r.Id,
            DireccionOrigen  = r.DireccionOrigen,
            CoordLatOrigen   = r.CoordLatOrigen,
            CoordLonOrigen   = r.CoordLonOrigen,
            DireccionDestino = r.DireccionDestino,
            CoordLatDestino  = r.CoordLatDestino,
            CoordLonDestino  = r.CoordLonDestino,
            CantidadCarga    = r.CantidadCarga,
            FechaServicio    = r.FechaServicio,
            HoraInicio       = r.HoraInicio,
            HoraFin          = r.HoraFin,
            NroBloques       = r.NroBloques,
            DistanciaKm      = r.DistanciaKm,
            TiempoEstimado   = r.TiempoEstimado,
            TiempoManiobra   = r.TiempoManiobra,
            TiempoRetorno    = r.TiempoRetorno,
            Estado           = r.Estado,
            EstadoOperacion  = r.EstadoOperacion,
            NombreCliente    = r.NombreCliente,
            GruaAsignada     = r.GruaAsignada,
            OperadorAsignado = r.OperadorAsignado,
            Vehiculos        = string.IsNullOrWhiteSpace(r.Vehiculos)
                ? new()
                : JsonSerializer.Deserialize<List<VehiculoItemDto>>(r.Vehiculos, options) ?? new(),
        });
    }

    /*
    //Con sql server
    public async Task CancelarReservaAsync(CancelarServicioDto dto)
    {
        using var conn = _db.CreateConnection();

        await conn.ExecuteAsync(
            "sp_CancelarReserva",
            new { dto.Id, dto.MotivoCancelacion, dto.ActualizadoPor },
            commandType: CommandType.StoredProcedure
        );
    } */
    public async Task<OperacionResultDto> IniciarReservaAsync(IniciarReservaDto dto)
    {
        using var conn = _db.CreateConnection();
        try
        {
            var p = new DynamicParameters();
            p.Add("_IdReserva",      dto.IdReserva,      DbType.Int32);
            p.Add("_ActualizadoPor", dto.ActualizadoPor, DbType.Int32);
            p.Add("_Exitoso", value: 0,  dbType: DbType.Int32,  direction: ParameterDirection.InputOutput);
            p.Add("_Mensaje",  value: "", dbType: DbType.String, direction: ParameterDirection.InputOutput, size: 500);

            await conn.ExecuteAsync(
                "CALL sp_ActualizarEnCursoReserva(@_IdReserva, @_ActualizadoPor, @_Exitoso, @_Mensaje)",
                p, commandType: CommandType.Text
            );

            return new OperacionResultDto
            {
                Exitoso = p.Get<int>("_Exitoso"),
                Mensaje = p.Get<string>("_Mensaje")
            };
        }
        catch (Exception ex)
        {
            return new OperacionResultDto { Exitoso = 0, Mensaje = ex.Message };
        }
    }

    public async Task<OperacionResultDto> FinalizarReservaAsync(FinalizarReservaDto dto)
    {
        using var conn = _db.CreateConnection();
        try
        {
            var p = new DynamicParameters();
            p.Add("_IdReserva",      dto.IdReserva,      DbType.Int32);
            p.Add("_ActualizadoPor", dto.ActualizadoPor, DbType.Int32);
            p.Add("_Exitoso", value: 0,  dbType: DbType.Int32,  direction: ParameterDirection.InputOutput);
            p.Add("_Mensaje",  value: "", dbType: DbType.String, direction: ParameterDirection.InputOutput, size: 500);

            await conn.ExecuteAsync(
                "CALL sp_FinalizarServicio(@_IdReserva, @_ActualizadoPor, @_Exitoso, @_Mensaje)",
                p, commandType: CommandType.Text
            );

            return new OperacionResultDto
            {
                Exitoso = p.Get<int>("_Exitoso"),
                Mensaje = p.Get<string>("_Mensaje")
            };
        }
        catch (Exception ex)
        {
            return new OperacionResultDto { Exitoso = 0, Mensaje = ex.Message };
        }
    }

    public async Task<OperacionResultDto> CancelarReservaAsync(CancelarServicioDto dto)
    {
        using var conn = _db.CreateConnection();
        try
        {
            var p = new DynamicParameters();
            p.Add("_Id",                dto.Id,                DbType.Int32);
            p.Add("_MotivoCancelacion", dto.MotivoCancelacion, DbType.String);
            p.Add("_ActualizadoPor",    dto.ActualizadoPor,    DbType.Int32);
            p.Add("_Exitoso", value: 0,  dbType: DbType.Int32,  direction: ParameterDirection.InputOutput);
            p.Add("_Mensaje",  value: "", dbType: DbType.String, direction: ParameterDirection.InputOutput, size: 500);

            await conn.ExecuteAsync(
                "CALL sp_CancelarReserva(@_Id, @_MotivoCancelacion, @_ActualizadoPor, @_Exitoso, @_Mensaje)",
                p, commandType: CommandType.Text
            );

            return new OperacionResultDto
            {
                Exitoso = p.Get<int>("_Exitoso"),
                Mensaje = p.Get<string>("_Mensaje")
            };
        }
        catch (Exception ex)
        {
            return new OperacionResultDto { Exitoso = 0, Mensaje = ex.Message };
        }
    }

    /*
    //Con sql server
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
    } */
    public async Task<(IEnumerable<GruaCandidatoDto> gruas, IEnumerable<OperadorCandidatoDto> operadores)> ObtenerCandidatosAsync(int idReserva)
    {
        using var conn = _db.CreateConnection();

        var gruas = await conn.QueryAsync<GruaCandidatoDto>(
            "SELECT * FROM fn_SugerirAsignacion_Gruas(@IdReserva)",
            new { IdReserva = idReserva },
            commandType: CommandType.Text
        );

        var operadores = await conn.QueryAsync<OperadorCandidatoDto>(
            "SELECT * FROM fn_SugerirAsignacion_Operadores(@IdReserva)",
            new { IdReserva = idReserva },
            commandType: CommandType.Text
        );

        return (gruas, operadores);
    }

    /*
    //Con sql server
    public async Task<OperacionResultDto> AsignarReservaAsync(AsignarServicioDto dto)
    {
        using var conn = _db.CreateConnection();

        return await conn.QueryFirstOrDefaultAsync<OperacionResultDto>(
            "sp_AsignarServicio",
            new { dto.IdReserva, dto.IdGrua, dto.IdOperador, dto.ActualizadoPor },
            commandType: CommandType.StoredProcedure
        ) ?? new OperacionResultDto { Exitoso = 0, Mensaje = "Error inesperado al asignar el servicio." };
    } */
    public async Task<OperacionResultDto> AsignarReservaAsync(AsignarServicioDto dto)
    {
        using var conn = _db.CreateConnection();
        try
        {
            var p = new DynamicParameters();
            p.Add("_IdReserva",      dto.IdReserva,      DbType.Int32);
            p.Add("_IdGrua",         dto.IdGrua,         DbType.Int32);
            p.Add("_IdOperador",     dto.IdOperador,     DbType.Int32);
            p.Add("_ActualizadoPor", dto.ActualizadoPor, DbType.Int32);
            p.Add("_Exitoso", value: 0,  dbType: DbType.Int32,  direction: ParameterDirection.InputOutput);
            p.Add("_Mensaje",  value: "", dbType: DbType.String, direction: ParameterDirection.InputOutput, size: 500);

            await conn.ExecuteAsync(
                "CALL sp_AsignarServicio(@_IdReserva, @_IdGrua, @_IdOperador, @_ActualizadoPor, @_Exitoso, @_Mensaje)",
                p, commandType: CommandType.Text
            );

            return new OperacionResultDto
            {
                Exitoso = p.Get<int>("_Exitoso"),
                Mensaje = p.Get<string>("_Mensaje")
            };
        }
        catch (Exception ex)
        {
            return new OperacionResultDto { Exitoso = 0, Mensaje = ex.Message };
        }
    }

    /*
    //Con sql server
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
                dto.Rol,
            },
            commandType: CommandType.StoredProcedure
        ) ?? new OperacionResultDto { Exitoso = 0, Mensaje = "Error inesperado al reprogramar la reserva." };
    } */
    public async Task<OperacionResultDto> ReprogramarReservaAsync(ReprogramarServicioDto dto)
    {
        using var conn = _db.CreateConnection();
        try
        {
            var horaInicio = TimeSpan.Parse(dto.NuevaHoraInicio);

            var p = new DynamicParameters();
            p.Add("_IdReserva",       dto.IdReserva,       DbType.Int32);
            p.Add("_NuevaFecha",      dto.NuevaFecha.Date, DbType.Date);
            p.Add("_NuevaHoraInicio", horaInicio,          DbType.Time);
            p.Add("_NuevoNroBloques", dto.NuevoNroBloques, DbType.Int32);
            p.Add("_ActualizadoPor",  dto.ActualizadoPor,  DbType.Int32);
            p.Add("_Rol",             dto.Rol,             DbType.String);
            p.Add("_Exitoso",        value: 0,    dbType: DbType.Int32,  direction: ParameterDirection.InputOutput);
            p.Add("_Mensaje",        value: "",   dbType: DbType.String, direction: ParameterDirection.InputOutput, size: 500);
            p.Add("_HorasConflicto", value: null, dbType: DbType.String, direction: ParameterDirection.InputOutput, size: 500);

            await conn.ExecuteAsync(
                @"CALL sp_ReprogramarReserva(
                    @_IdReserva, @_NuevaFecha::date, @_NuevaHoraInicio::time,
                    @_NuevoNroBloques, @_ActualizadoPor, @_Rol,
                    @_Exitoso, @_Mensaje, @_HorasConflicto)",
                p, commandType: CommandType.Text
            );

            return new OperacionResultDto
            {
                Exitoso        = p.Get<int>("_Exitoso"),
                Mensaje        = p.Get<string>("_Mensaje"),
                HorasConflicto = p.Get<string?>("_HorasConflicto")
            };
        }
        catch (Exception ex)
        {
            return new OperacionResultDto { Exitoso = 0, Mensaje = ex.Message };
        }
    }

    /*
    //Con sql server
    public async Task<(string latOrigen, string lonOrigen)?> ObtenerOrigenReservaAsync(int idReserva)
    {
        using var conn = _db.CreateConnection();

        var result = await conn.QueryFirstOrDefaultAsync<OrigenReserva>(
            "SELECT CoordLatOrigen, CoordLonOrigen FROM Reserva WHERE Id = @Id",
            new { Id = idReserva }
        );

        if (result is null) return null;
        return (result.CoordLatOrigen, result.CoordLonOrigen);
    } */
    public async Task<(string latOrigen, string lonOrigen)?> ObtenerOrigenReservaAsync(int idReserva)
    {
        using var conn = _db.CreateConnection();

        var result = await conn.QueryFirstOrDefaultAsync<OrigenReserva>(
            @"SELECT ""CoordLatOrigen"", ""CoordLonOrigen"" FROM ""Reserva"" WHERE ""Id"" = @Id",
            new { Id = idReserva }
        );

        if (result is null) return null;
        return (result.CoordLatOrigen, result.CoordLonOrigen);
    }

    public async Task<IEnumerable<short>> ListarCapacidadesGruasAsync()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<short>(
            "SELECT * FROM fn_ListCapacidadesGruas()",
            commandType: CommandType.Text
        );
    }

    public async Task<IEnumerable<DisponibilidadGruaDto>> ObtenerDisponibilidadGruasAsync(DateOnly fechaServicio, short? capacidad)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<DisponibilidadGruaDto>(
            "SELECT * FROM fn_DisponibilidadGruas(@FechaServicio::date, @Capacidad::smallint)",
            new { FechaServicio = fechaServicio.ToDateTime(TimeOnly.MinValue), Capacidad = (object?)capacidad ?? DBNull.Value },
            commandType: CommandType.Text
        );
    }

    private class OrigenReserva
    {
        public string CoordLatOrigen { get; set; } = string.Empty;
        public string CoordLonOrigen { get; set; } = string.Empty;
    }

    private class ReservaDapperRow
    {
        public int       Id               { get; set; }
        public string    DireccionOrigen  { get; set; } = string.Empty;
        public string    CoordLatOrigen   { get; set; } = string.Empty;
        public string    CoordLonOrigen   { get; set; } = string.Empty;
        public string    DireccionDestino { get; set; } = string.Empty;
        public string    CoordLatDestino  { get; set; } = string.Empty;
        public string    CoordLonDestino  { get; set; } = string.Empty;
        public short     CantidadCarga    { get; set; }
        public DateTime  FechaServicio    { get; set; }
        public TimeSpan  HoraInicio       { get; set; }
        public TimeSpan  HoraFin          { get; set; }
        public int       NroBloques       { get; set; }
        public decimal   DistanciaKm      { get; set; }
        public int       TiempoEstimado   { get; set; }
        public int       TiempoManiobra   { get; set; }
        public int       TiempoRetorno    { get; set; }
        public string    Estado           { get; set; } = string.Empty;
        public string    EstadoOperacion  { get; set; } = string.Empty;
        public string?   NombreCliente    { get; set; }
        public string?   GruaAsignada     { get; set; }
        public string?   OperadorAsignado { get; set; }
        public string?   Vehiculos        { get; set; }  // columna JSON → se deserializa manualmente
    }
}
