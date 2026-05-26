using System.Data;
using System.Text.Json;
using BackRomo.Application.DTOs.Agenda;
using BackRomo.Application.Interfaces;
using BackRomo.Infrastructure.Data;
using Dapper;

namespace BackRomo.Infrastructure.Repositories;

public class AgendaRepository : IAgendaRepository
{
    private readonly DbConnectionFactory _db;

    public AgendaRepository(DbConnectionFactory db)
    {
        _db = db;
    }

    public async Task<IEnumerable<HorarioDto>> ListarConfiguracionHorarioAsync(string? rol, string? estado, CancellationToken ct = default)
    {
        using var conn = _db.CreateConnection();

        var rows = await conn.QueryAsync<HorarioDapperRow>(new CommandDefinition(
            "SELECT * FROM fn_ListConfiguracionHorario(@Rol, @Estado)",
            new { Rol = rol, Estado = estado },
            commandType: CommandType.Text,
            cancellationToken: ct
        ));

        return rows.Select(r => new HorarioDto
        {
            Id         = r.Id,
            NroDia     = r.NroDia,
            NombreDia  = r.NombreDia,
            Estado     = r.Estado,
            HoraInicio = TimeOnly.FromTimeSpan(r.HoraInicio).ToString("HH:mm"),
            HoraFinal  = TimeOnly.FromTimeSpan(r.HoraFinal).ToString("HH:mm"),
        });
    }

    public async Task<AgendaResultDto> ActualizarConfiguracionHorarioAsync(UpdConfiguracionHorarioDto dto, CancellationToken ct = default)
    {
        using var conn = _db.CreateConnection();
        try
        {
            var horariosJson = JsonSerializer.Serialize(dto.Horarios, new JsonSerializerOptions
            {
                PropertyNamingPolicy = null
            });

            var p = new DynamicParameters();
            p.Add("_Horarios",       horariosJson,       DbType.String);
            p.Add("_ActualizadoPor", dto.ActualizadoPor, DbType.Int32);
            p.Add("_Exitoso", value: 0,  dbType: DbType.Int32,  direction: ParameterDirection.InputOutput);
            p.Add("_Mensaje", value: "", dbType: DbType.String, direction: ParameterDirection.InputOutput, size: 500);

            await conn.ExecuteAsync(new CommandDefinition(
                "CALL sp_UpdConfiguracionHorario(@_Horarios, @_ActualizadoPor, @_Exitoso, @_Mensaje)",
                p, commandType: CommandType.Text, cancellationToken: ct
            ));

            return new AgendaResultDto { Exitoso = p.Get<int>("_Exitoso"), Mensaje = p.Get<string>("_Mensaje") };
        }
        catch (OperationCanceledException)
        {
            return new AgendaResultDto { Exitoso = 2, Mensaje = "La operación tardó demasiado. Verifique si el cambio fue aplicado." };
        }
        catch (Exception ex)
        {
            return new AgendaResultDto { Exitoso = 0, Mensaje = ex.Message };
        }
    }

    public async Task<AgendaResultDto> CreUpdExcepcionAsync(CrearExcepcionDto dto, CancellationToken ct = default)
    {
        using var conn = _db.CreateConnection();
        try
        {
            var p = new DynamicParameters();
            p.Add("_Id",                dto.Id,                         DbType.Int32);
            p.Add("_Fecha",             DateOnly.Parse(dto.Fecha),      DbType.Date);
            p.Add("_Motivo",            dto.Motivo,                     DbType.String);
            p.Add("_HoraInicio",        TimeSpan.Parse(dto.HoraInicio), DbType.Time);
            p.Add("_HoraFin",           TimeSpan.Parse(dto.HoraFin),    DbType.Time);
            p.Add("_DescripcionMotivo", dto.DescripcionMotivo,          DbType.String);
            p.Add("_UsuarioId",         dto.UsuarioId,                  DbType.Int32);
            p.Add("_Exitoso", value: 0,  dbType: DbType.Int32,  direction: ParameterDirection.InputOutput);
            p.Add("_Mensaje", value: "", dbType: DbType.String, direction: ParameterDirection.InputOutput, size: 500);
            p.Add("_IdNuevo", value: 0,  dbType: DbType.Int32,  direction: ParameterDirection.InputOutput);

            await conn.ExecuteAsync(new CommandDefinition(
                "CALL sp_CreUpdExcepcion(@_Id, @_Fecha, @_Motivo, @_HoraInicio, @_HoraFin, @_DescripcionMotivo, @_UsuarioId, @_Exitoso, @_Mensaje, @_IdNuevo)",
                p, commandType: CommandType.Text, cancellationToken: ct
            ));

            return new AgendaResultDto
            {
                Exitoso = p.Get<int>("_Exitoso"),
                Mensaje = p.Get<string>("_Mensaje"),
                IdNuevo = p.Get<int>("_IdNuevo"),
            };
        }
        catch (OperationCanceledException)
        {
            return new AgendaResultDto { Exitoso = 2, Mensaje = "La operación tardó demasiado. Verifique si el cambio fue aplicado." };
        }
        catch (Exception ex)
        {
            return new AgendaResultDto { Exitoso = 0, Mensaje = ex.Message };
        }
    }

    public async Task<AgendaResultDto> UpdEstadoExcepcionAsync(int id, UpdEstadoExcepcionDto dto, CancellationToken ct = default)
    {
        using var conn = _db.CreateConnection();
        try
        {
            var p = new DynamicParameters();
            p.Add("_Id",             id,                 DbType.Int32);
            p.Add("_NuevoEstado",    dto.NuevoEstado,    DbType.String);
            p.Add("_ActualizadoPor", dto.ActualizadoPor, DbType.Int32);
            p.Add("_Exitoso", value: 0,  dbType: DbType.Int32,  direction: ParameterDirection.InputOutput);
            p.Add("_Mensaje", value: "", dbType: DbType.String, direction: ParameterDirection.InputOutput, size: 500);

            await conn.ExecuteAsync(new CommandDefinition(
                "CALL sp_UpdEstadoExcepcion(@_Id, @_NuevoEstado, @_ActualizadoPor, @_Exitoso, @_Mensaje)",
                p, commandType: CommandType.Text, cancellationToken: ct
            ));

            return new AgendaResultDto { Exitoso = p.Get<int>("_Exitoso"), Mensaje = p.Get<string>("_Mensaje") };
        }
        catch (OperationCanceledException)
        {
            return new AgendaResultDto { Exitoso = 2, Mensaje = "La operación tardó demasiado. Verifique si el cambio fue aplicado." };
        }
        catch (Exception ex)
        {
            return new AgendaResultDto { Exitoso = 0, Mensaje = ex.Message };
        }
    }

    public async Task<ExcepcionPagedDto> ListarExcepcionesAsync(string? estado, int? id, string? motivo, DateOnly? fechaInicio, DateOnly? fechaFin, int? pagina, int? tamano, CancellationToken ct = default)
    {
        using var conn = _db.CreateConnection();

        var param = new DynamicParameters();
        param.Add("Estado",      estado);
        param.Add("Id",          id);
        param.Add("Motivo",      motivo);
        param.Add("FechaInicio", fechaInicio?.ToDateTime(TimeOnly.MinValue), DbType.Date);
        param.Add("FechaFin",    fechaFin?.ToDateTime(TimeOnly.MinValue),    DbType.Date);
        param.Add("Pagina",      pagina);
        param.Add("Tamano",      tamano);

        using var multi = await conn.QueryMultipleAsync(new CommandDefinition(
            """
            SELECT * FROM fn_ListExcepciones(@Estado, @Id, @Motivo, @FechaInicio, @FechaFin, @Pagina, @Tamano);
            SELECT COUNT(*) FROM fn_ListExcepciones(@Estado, @Id, @Motivo, @FechaInicio, @FechaFin, NULL, NULL);
            """,
            param,
            commandType: CommandType.Text,
            cancellationToken: ct
        ));

        var datos = (await multi.ReadAsync<ExcepcionDto>()).ToList();
        var total = await multi.ReadFirstOrDefaultAsync<long>();

        return new ExcepcionPagedDto
        {
            Total = total,
            Datos = datos
        };
    }

    private class HorarioDapperRow
    {
        public int      Id         { get; set; }
        public short    NroDia     { get; set; }
        public string   NombreDia  { get; set; } = string.Empty;
        public string   Estado     { get; set; } = string.Empty;
        public TimeSpan HoraInicio { get; set; }
        public TimeSpan HoraFinal  { get; set; }
    }

}
