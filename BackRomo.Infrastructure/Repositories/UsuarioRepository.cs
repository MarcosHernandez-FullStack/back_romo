using System.Data;
using BackRomo.Application.DTOs.Usuario;
using BackRomo.Application.Interfaces;
using BackRomo.Infrastructure.Data;
using Dapper;

namespace BackRomo.Infrastructure.Repositories;

public class UsuarioRepository : IUsuarioRepository
{
    private readonly DbConnectionFactory _db;

    public UsuarioRepository(DbConnectionFactory db)
    {
        _db = db;
    }

    public async Task<IEnumerable<UsuarioDto>> ListarUsuariosAsync(string? estado, int? id, string? rol, CancellationToken ct = default)
    {
        using var conn = _db.CreateConnection();

        return await conn.QueryAsync<UsuarioDto>(new CommandDefinition(
            "SELECT * FROM fn_ListUsuarios(@Estado, @Id, @Rol)",
            new { Estado = estado, Id = id, Rol = rol },
            commandType: CommandType.Text,
            cancellationToken: ct
        ));
    }

    public async Task<UsuarioResultDto> CrearUsuarioAsync(CrearUsuarioDto dto, CancellationToken ct = default)
    {
        using var conn = _db.CreateConnection();
        try
        {
            var alias = GenerarAlias(dto.Correo);

            var p = new DynamicParameters();
            p.Add("_IdUsuario",   0,              DbType.Int32);
            p.Add("_Alias",       alias,          DbType.String);
            p.Add("_Contrasena",  dto.Contrasena, DbType.String);
            p.Add("_Nombres",     dto.Nombres,    DbType.String);
            p.Add("_Apellidos",   dto.Apellidos,  DbType.String);
            p.Add("_Telefono",    dto.Telefono,   DbType.String);
            p.Add("_Correo",      dto.Correo,     DbType.String);
            p.Add("_Rol",         dto.Rol,        DbType.String);
            p.Add("_NroLicencia", null,           DbType.String);
            p.Add("_FecVenLic",   null,           DbType.Date);
            p.Add("_CreadoPor",   dto.CreadoPor,  DbType.Int32);
            p.Add("_Exitoso", value: 0,  dbType: DbType.Int32,  direction: ParameterDirection.InputOutput);
            p.Add("_Mensaje", value: "", dbType: DbType.String, direction: ParameterDirection.InputOutput, size: 500);
            p.Add("_IdNuevo", value: 0,  dbType: DbType.Int32,  direction: ParameterDirection.InputOutput);

            await conn.ExecuteAsync(new CommandDefinition(
                "CALL sp_CreUpdUsuario(@_IdUsuario, @_Alias, @_Contrasena, @_Nombres, @_Apellidos, @_Telefono, @_Correo, @_Rol, @_NroLicencia, @_FecVenLic, @_CreadoPor, @_Exitoso, @_Mensaje, @_IdNuevo)",
                p, commandType: CommandType.Text, cancellationToken: ct
            ));

            return new UsuarioResultDto
            {
                Exitoso = p.Get<int>("_Exitoso"),
                Mensaje = p.Get<string>("_Mensaje"),
                IdNuevo = p.Get<int>("_IdNuevo"),
            };
        }
        catch (OperationCanceledException)
        {
            return new UsuarioResultDto { Exitoso = 2, Mensaje = "La operación tardó demasiado. Verifique si el usuario fue creado." };
        }
        catch (Exception ex)
        {
            return new UsuarioResultDto { Exitoso = 0, Mensaje = ex.Message };
        }
    }

    public async Task<UsuarioResultDto> EditarUsuarioAsync(EditarUsuarioDto dto, CancellationToken ct = default)
    {
        using var conn = _db.CreateConnection();
        try
        {
            var p = new DynamicParameters();
            p.Add("_IdUsuario",   dto.IdUsuario,      DbType.Int32);
            p.Add("_Alias",       "",                 DbType.String);   // ignorado en actualización
            p.Add("_Contrasena",  dto.Contrasena,     DbType.String);
            p.Add("_Nombres",     dto.Nombres,        DbType.String);
            p.Add("_Apellidos",   dto.Apellidos,      DbType.String);
            p.Add("_Telefono",    dto.Telefono,       DbType.String);
            p.Add("_Correo",      dto.Correo,         DbType.String);
            p.Add("_Rol",         dto.Rol,            DbType.String);
            p.Add("_NroLicencia", null,               DbType.String);
            p.Add("_FecVenLic",   null,               DbType.Date);
            p.Add("_CreadoPor",   dto.ActualizadoPor, DbType.Int32);
            p.Add("_Exitoso", value: 0,  dbType: DbType.Int32,  direction: ParameterDirection.InputOutput);
            p.Add("_Mensaje", value: "", dbType: DbType.String, direction: ParameterDirection.InputOutput, size: 500);
            p.Add("_IdNuevo", value: 0,  dbType: DbType.Int32,  direction: ParameterDirection.InputOutput);

            await conn.ExecuteAsync(new CommandDefinition(
                "CALL sp_CreUpdUsuario(@_IdUsuario, @_Alias, @_Contrasena, @_Nombres, @_Apellidos, @_Telefono, @_Correo, @_Rol, @_NroLicencia, @_FecVenLic, @_CreadoPor, @_Exitoso, @_Mensaje, @_IdNuevo)",
                p, commandType: CommandType.Text, cancellationToken: ct
            ));

            return new UsuarioResultDto
            {
                Exitoso = p.Get<int>("_Exitoso"),
                Mensaje = p.Get<string>("_Mensaje"),
                IdNuevo = p.Get<int>("_IdNuevo"),
            };
        }
        catch (OperationCanceledException)
        {
            return new UsuarioResultDto { Exitoso = 2, Mensaje = "La operación tardó demasiado. Verifique si el usuario fue actualizado." };
        }
        catch (Exception ex)
        {
            return new UsuarioResultDto { Exitoso = 0, Mensaje = ex.Message };
        }
    }

    // Genera el alias de login a partir del prefijo del correo (antes del @), en mayúsculas.
    // Garantiza unicidad indirecta: si el correo es único, el alias derivado también lo es.
    private static string GenerarAlias(string correo)
        => correo.Split('@')[0].ToUpperInvariant();
}
