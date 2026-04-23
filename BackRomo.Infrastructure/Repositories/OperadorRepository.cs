using System.Data;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using BackRomo.Application.DTOs.Operador;
using BackRomo.Application.Interfaces;
using BackRomo.Infrastructure.Data;
using Dapper;

namespace BackRomo.Infrastructure.Repositories;

public class OperadorRepository : IOperadorRepository
{
    private readonly DbConnectionFactory _db;

    public OperadorRepository(DbConnectionFactory db)
    {
        _db = db;
    }

    public async Task<IEnumerable<OperadorListDto>> ListarOperadoresAsync(string? estado, CancellationToken ct = default)
    {
        using var conn = _db.CreateConnection();

        var rows = await conn.QueryAsync<OperadorDapperRow>(new CommandDefinition(
            "SELECT * FROM fn_ListOperadores(@Estado)",
            new { Estado = estado },
            cancellationToken: ct
        ));

        return rows.Select(r => new OperadorListDto
        {
            Id                      = r.Id,
            Alias                   = r.Alias,
            NombresCompleto         = r.NombresCompleto,
            Nombres                 = r.Nombres,
            Apellidos               = r.Apellidos,
            Correo                  = r.Correo,
            Telefono                = r.Telefono,
            NroLicencia             = r.NroLicencia,
            FecVenLic               = DateOnly.FromDateTime(r.FecVenLic),
            Estado                  = r.Estado,
            ProximaFechaServicio    = r.ProximaFechaServicio.HasValue
                                        ? DateOnly.FromDateTime(r.ProximaFechaServicio.Value)
                                        : null,
            ProximaHoraServicio     = r.ProximaHoraServicio.HasValue
                                        ? TimeOnly.FromTimeSpan(r.ProximaHoraServicio.Value)
                                        : null,
            TotalServiciosAsignados = r.TotalServiciosAsignados,
            TotalHorasSemanales     = r.TotalHorasSemanales,
        });
    }

    public async Task<DispOperadorDto> ObtenerDispOperadorAsync(int idOperador, CancellationToken ct = default)
    {
        using var conn = _db.CreateConnection();

        var rows = await conn.QueryAsync<DispDapperRow>(new CommandDefinition(
            "SELECT * FROM fn_ListDispOperador(@IdOperador)",
            new { IdOperador = idOperador },
            commandType: CommandType.Text,
            cancellationToken: ct
        ));

        var list = rows.ToList();
        if (list.Count == 0)
            return new DispOperadorDto { Slots = new(), TotalHorasSemanales = 0, DiasActivos = 0 };

        return new DispOperadorDto
        {
            TotalHorasSemanales = list[0].TotalHorasSemanales,
            DiasActivos         = list[0].DiasActivos,
            Slots = list.Select(r => new DispSlotDto
            {
                Id         = r.Id,
                NroDia     = r.NroDia,
                NombreDia  = r.NombreDia,
                HoraInicio = TimeOnly.FromTimeSpan(r.HoraInicio).ToString("HH:mm"),
                HoraFin    = TimeOnly.FromTimeSpan(r.HoraFin).ToString("HH:mm"),
            }).ToList(),
        };
    }

    public async Task<DispResultDto> GuardarDispOperadorAsync(AsignarDispOperadorDto dto, CancellationToken ct = default)
    {
        using var conn = _db.CreateConnection();
        try
        {
            var dispJson = JsonSerializer.Serialize(dto.Disponibilidad, new JsonSerializerOptions
            {
                PropertyNamingPolicy = null
            });

            var p = new DynamicParameters();
            p.Add("_IdOperador",      dto.IdOperador,     DbType.Int32);
            p.Add("_Disponibilidad",  dispJson,            DbType.String);
            p.Add("_Confirmar",       dto.Confirmar,       DbType.Boolean);
            p.Add("_ActualizadoPor",  dto.ActualizadoPor, DbType.Int32);
            p.Add("_Exitoso",    value: 0,    dbType: DbType.Int32,  direction: ParameterDirection.InputOutput);
            p.Add("_Mensaje",    value: "",   dbType: DbType.String, direction: ParameterDirection.InputOutput, size: 500);
            p.Add("_Conflictos", value: "[]", dbType: DbType.String, direction: ParameterDirection.InputOutput, size: 4000);

            await conn.ExecuteAsync(new CommandDefinition(
                "CALL sp_AsignarDispOperador(@_IdOperador, @_Disponibilidad, @_Confirmar, @_ActualizadoPor, @_Exitoso, @_Mensaje, @_Conflictos)",
                p, commandType: CommandType.Text, cancellationToken: ct
            ));

            var exitoso    = p.Get<int>("_Exitoso");
            var mensaje    = p.Get<string>("_Mensaje");
            var conflictos = p.Get<string?>("_Conflictos");

            List<DispConflictoDto>? conflictoList = null;
            if (!string.IsNullOrWhiteSpace(conflictos) && conflictos != "[]")
            {
                var opts = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                conflictoList = JsonSerializer.Deserialize<List<DispConflictoDto>>(conflictos, opts);
            }

            return new DispResultDto { Exitoso = exitoso, Mensaje = mensaje, Conflictos = conflictoList };
        }
        catch (OperationCanceledException)
        {
            return new DispResultDto { Exitoso = 2, Mensaje = "La operación tardó demasiado. Verifique si el cambio fue aplicado." };
        }
        catch (Exception ex)
        {
            return new DispResultDto { Exitoso = 0, Mensaje = ex.Message };
        }
    }

    public async Task<OperadorResultDto> CrearOperadorAsync(CrearOperadorDto dto, CancellationToken ct = default)
    {
        using var conn = _db.CreateConnection();
        try
        {
            var p = new DynamicParameters();
            p.Add("_IdUsuario",    0,                                                DbType.Int32);
            p.Add("_Alias",        dto.Alias,                                        DbType.String);
            p.Add("_Contrasena",   HashMd5ComoGuid(dto.Contrasena),                  DbType.String);
            p.Add("_Nombres",      dto.Nombres,                                      DbType.String);
            p.Add("_Apellidos",    dto.Apellidos,                                    DbType.String);
            p.Add("_Telefono",     dto.Telefono,                                     DbType.String);
            p.Add("_Correo",       dto.Correo,                                       DbType.String);
            p.Add("_Rol",          dto.Rol,                                          DbType.String);
            p.Add("_NroLicencia",  dto.NroLicencia,                                  DbType.String);
            p.Add("_FecVenLic",    dto.FecVenLic.ToDateTime(TimeOnly.MinValue),      DbType.Date);
            p.Add("_CreadoPor",    dto.CreadoPor,                                    DbType.Int32);
            p.Add("_Exitoso", value: 0,  dbType: DbType.Int32,  direction: ParameterDirection.InputOutput);
            p.Add("_Mensaje", value: "", dbType: DbType.String, direction: ParameterDirection.InputOutput, size: 500);
            p.Add("_IdNuevo", value: 0,  dbType: DbType.Int32,  direction: ParameterDirection.InputOutput);

            await conn.ExecuteAsync(new CommandDefinition(
                "CALL sp_CreUpdUsuario(@_IdUsuario, @_Alias, @_Contrasena, @_Nombres, @_Apellidos, @_Telefono, @_Correo, @_Rol, @_NroLicencia, @_FecVenLic, @_CreadoPor, @_Exitoso, @_Mensaje, @_IdNuevo)",
                p, commandType: CommandType.Text, cancellationToken: ct
            ));

            return new OperadorResultDto
            {
                Exitoso = p.Get<int>("_Exitoso"),
                Mensaje = p.Get<string>("_Mensaje"),
                IdNuevo = p.Get<int>("_IdNuevo"),
            };
        }
        catch (OperationCanceledException)
        {
            return new OperadorResultDto { Exitoso = 2, Mensaje = "La operación tardó demasiado. Verifique si el operador fue creado." };
        }
        catch (Exception ex)
        {
            return new OperadorResultDto { Exitoso = 0, Mensaje = ex.Message };
        }
    }

    public async Task<OperadorResultDto> EditarOperadorAsync(EditarOperadorDto dto, CancellationToken ct = default)
    {
        using var conn = _db.CreateConnection();
        try
        {
            var idUsuario = await conn.ExecuteScalarAsync<int>(new CommandDefinition(
                "SELECT \"IdUsuario\" FROM \"Operador\" WHERE \"Id\" = @Id",
                new { Id = dto.IdOperador }, cancellationToken: ct));

            if (idUsuario == 0)
                return new OperadorResultDto { Exitoso = 0, Mensaje = "El operador no existe." };

            var contrasenaParam = string.IsNullOrWhiteSpace(dto.Contrasena)
                ? string.Empty
                : HashMd5ComoGuid(dto.Contrasena);

            var p = new DynamicParameters();
            p.Add("_IdUsuario",    idUsuario,                                    DbType.Int32);
            p.Add("_Alias",        string.Empty,                                     DbType.String);
            p.Add("_Contrasena",   contrasenaParam,                                  DbType.String);
            p.Add("_Nombres",      dto.Nombres,                                      DbType.String);
            p.Add("_Apellidos",    dto.Apellidos,                                    DbType.String);
            p.Add("_Telefono",     dto.Telefono,                                     DbType.String);
            p.Add("_Correo",       dto.Correo,                                       DbType.String);
            p.Add("_Rol",          dto.Rol,                                          DbType.String);
            p.Add("_NroLicencia",  dto.NroLicencia,                                  DbType.String);
            p.Add("_FecVenLic",    dto.FecVenLic.ToDateTime(TimeOnly.MinValue),      DbType.Date);
            p.Add("_CreadoPor",    dto.ActualizadoPor,                               DbType.Int32);
            p.Add("_Exitoso", value: 0,  dbType: DbType.Int32,  direction: ParameterDirection.InputOutput);
            p.Add("_Mensaje", value: "", dbType: DbType.String, direction: ParameterDirection.InputOutput, size: 500);
            p.Add("_IdNuevo", value: 0,  dbType: DbType.Int32,  direction: ParameterDirection.InputOutput);

            await conn.ExecuteAsync(new CommandDefinition(
                "CALL sp_CreUpdUsuario(@_IdUsuario, @_Alias, @_Contrasena, @_Nombres, @_Apellidos, @_Telefono, @_Correo, @_Rol, @_NroLicencia, @_FecVenLic, @_CreadoPor, @_Exitoso, @_Mensaje, @_IdNuevo)",
                p, commandType: CommandType.Text, cancellationToken: ct));

            return new OperadorResultDto
            {
                Exitoso = p.Get<int>("_Exitoso"),
                Mensaje = p.Get<string>("_Mensaje"),
                IdNuevo = p.Get<int>("_IdNuevo"),
            };
        }
        catch (OperationCanceledException)
        {
            return new OperadorResultDto { Exitoso = 2, Mensaje = "La operación tardó demasiado. Verifique si el operador fue actualizado." };
        }
        catch (Exception ex)
        {
            return new OperadorResultDto { Exitoso = 0, Mensaje = ex.Message };
        }
    }

    public async Task<OperadorResultDto> ActualizarEstadoAsync(int idOperador, UpdEstadoOperadorDto dto, CancellationToken ct = default)
    {
        using var conn = _db.CreateConnection();
        try
        {
            var p = new DynamicParameters();
            p.Add("_IdOperador",     idOperador,         DbType.Int32);
            p.Add("_NuevoEstado",    dto.NuevoEstado,    DbType.String);
            p.Add("_ActualizadoPor", dto.ActualizadoPor, DbType.Int32);
            p.Add("_Exitoso", value: 0,  dbType: DbType.Int32,  direction: ParameterDirection.InputOutput);
            p.Add("_Mensaje", value: "", dbType: DbType.String, direction: ParameterDirection.InputOutput, size: 500);

            await conn.ExecuteAsync(new CommandDefinition(
                "CALL sp_UpdEstadoOperador(@_IdOperador, @_NuevoEstado, @_ActualizadoPor, @_Exitoso, @_Mensaje)",
                p, commandType: CommandType.Text, cancellationToken: ct
            ));

            return new OperadorResultDto
            {
                Exitoso = p.Get<int>("_Exitoso"),
                Mensaje = p.Get<string>("_Mensaje"),
            };
        }
        catch (OperationCanceledException)
        {
            return new OperadorResultDto { Exitoso = 2, Mensaje = "La operación tardó demasiado. Verifique si el cambio fue aplicado." };
        }
        catch (Exception ex)
        {
            return new OperadorResultDto { Exitoso = 0, Mensaje = ex.Message };
        }
    }

    public async Task<IEnumerable<ProxServOperadorDto>> ObtenerProxServAsync(int idOperador, CancellationToken ct = default)
    {
        using var conn = _db.CreateConnection();

        var rows = await conn.QueryAsync<ProxServDapperRow>(new CommandDefinition(
            "SELECT * FROM fn_ProxServOperador(@IdOperador)",
            new { IdOperador = idOperador },
            commandType: CommandType.Text,
            cancellationToken: ct
        ));

        return rows.Select(r => new ProxServOperadorDto
        {
            Id             = r.Id,
            FechaServicio  = r.FechaServicio.ToString("yyyy-MM-dd"),
            HoraInicio     = r.HoraInicio,
            HoraFin        = r.HoraFin,
            NomCliente     = r.NomCliente,
            FechaAbreviada = r.FechaAbreviada,
        });
    }

    private static string HashMd5ComoGuid(string input)
    {
        var hashBytes = MD5.HashData(Encoding.UTF8.GetBytes(input));
        return new Guid(hashBytes).ToString().ToUpper();
    }

    private class OperadorDapperRow
    {
        public int      Id                      { get; set; }
        public string   Alias                   { get; set; } = string.Empty;
        public string   NombresCompleto         { get; set; } = string.Empty;
        public string   Nombres                 { get; set; } = string.Empty;
        public string   Apellidos               { get; set; } = string.Empty;
        public string   Correo                  { get; set; } = string.Empty;
        public string?  Telefono                { get; set; }
        public string   NroLicencia             { get; set; } = string.Empty;
        public DateTime FecVenLic               { get; set; }
        public string   Estado                  { get; set; } = string.Empty;
        public DateTime? ProximaFechaServicio   { get; set; }
        public TimeSpan? ProximaHoraServicio    { get; set; }
        public int      TotalServiciosAsignados { get; set; }
        public int      TotalHorasSemanales     { get; set; }
    }

    private class DispDapperRow
    {
        public int      Id                  { get; set; }
        public short    NroDia              { get; set; }
        public string   NombreDia           { get; set; } = string.Empty;
        public TimeSpan HoraInicio          { get; set; }
        public TimeSpan HoraFin             { get; set; }
        public int      TotalHorasSemanales { get; set; }
        public int      DiasActivos         { get; set; }
    }

    private class ProxServDapperRow
    {
        public int      Id             { get; set; }
        public DateTime FechaServicio  { get; set; }
        public string   HoraInicio     { get; set; } = string.Empty;
        public string   HoraFin        { get; set; } = string.Empty;
        public string   NomCliente     { get; set; } = string.Empty;
        public string   FechaAbreviada { get; set; } = string.Empty;
    }
}
