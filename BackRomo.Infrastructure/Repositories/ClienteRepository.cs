using System.Data;
using BackRomo.Application.DTOs.Cliente;
using BackRomo.Application.Interfaces;
using BackRomo.Infrastructure.Data;
using Dapper;

namespace BackRomo.Infrastructure.Repositories;

public class ClienteRepository : IClienteRepository
{
    private readonly DbConnectionFactory _db;

    public ClienteRepository(DbConnectionFactory db)
    {
        _db = db;
    }

    /*
    //Con sql server
     public async Task<IEnumerable<ClienteDto>> ListarClientesAsync(string? estado, int? id)
    {
        using var conn = _db.CreateConnection();

        return await conn.QueryAsync<ClienteDto>(
            "sp_ListClientes",
            new { Estado = estado, Id = id },
            commandType: CommandType.StoredProcedure
        );
    } */

    public async Task<ClientePagedDto> ListarClientesAsync(string? estado, int? id, string? empresa, string? contacto, int? pagina, int? tamano, CancellationToken ct = default)
    {
        using var conn = _db.CreateConnection();
        var param = new { Estado = estado, Id = id, Empresa = empresa, Contacto = contacto, Pagina = pagina, Tamano = tamano };

        using var multi = await conn.QueryMultipleAsync(new CommandDefinition(
            """
            SELECT * FROM fn_ListClientes(@Estado, @Id, @Empresa, @Contacto, @Pagina, @Tamano);
            SELECT
                COUNT(*)                                        AS Total,
                COUNT(*) FILTER (WHERE "Estado" = 'ACTIVO')    AS TotalActivos,
                COUNT(*) FILTER (WHERE "Estado" = 'INACTIVO')  AS TotalInactivos
            FROM fn_ListClientes(@Estado, @Id, @Empresa, @Contacto, NULL, NULL);
            """,
            param,
            commandType: CommandType.Text,
            cancellationToken: ct
        ));

        var datos = (await multi.ReadAsync<ClienteDto>()).ToList();
        var (total, activos, inactivos) = await multi.ReadFirstOrDefaultAsync<(long, long, long)>();

        return new ClientePagedDto
        {
            Total          = total,
            TotalActivos   = activos,
            TotalInactivos = inactivos,
            Datos          = datos,
        };
    }

    public async Task<ClienteResultDto> CrearClienteAsync(CrearClienteDto dto, CancellationToken ct = default)
    {
        using var conn = _db.CreateConnection();
        try
        {
            var p = new DynamicParameters();
            p.Add("_IdCliente",      0,                  DbType.Int32);
            p.Add("_Alias",          dto.Alias,          DbType.String);
            p.Add("_Contrasena",     dto.Contrasena,     DbType.String);
            p.Add("_Empresa",        dto.Empresa,        DbType.String);
            p.Add("_NomContacto",    dto.NomContacto,    DbType.String);
            p.Add("_NroContacto",    dto.NroContacto,    DbType.String);
            p.Add("_CorreoContacto", dto.CorreoContacto, DbType.String);
            p.Add("_TarifaBase",     dto.TarifaBase,     DbType.Decimal);
            p.Add("_TarifaKm",       dto.TarifaKm,       DbType.Decimal);
            p.Add("_CreadoPor",      dto.CreadoPor,      DbType.Int32);
            p.Add("_Exitoso", value: 0,  dbType: DbType.Int32,  direction: ParameterDirection.InputOutput);
            p.Add("_Mensaje", value: "", dbType: DbType.String, direction: ParameterDirection.InputOutput, size: 500);
            p.Add("_IdNuevo", value: 0,  dbType: DbType.Int32,  direction: ParameterDirection.InputOutput);

            await conn.ExecuteAsync(new CommandDefinition(
                "CALL sp_CreUpdCliente(@_IdCliente, @_Alias, @_Contrasena, @_Empresa, @_NomContacto, @_NroContacto, @_CorreoContacto, @_TarifaBase, @_TarifaKm, @_CreadoPor, @_Exitoso, @_Mensaje, @_IdNuevo)",
                p, commandType: CommandType.Text, cancellationToken: ct
            ));

            return new ClienteResultDto
            {
                Exitoso = p.Get<int>("_Exitoso"),
                Mensaje = p.Get<string>("_Mensaje"),
                IdNuevo = p.Get<int>("_IdNuevo"),
            };
        }
        catch (OperationCanceledException)
        {
            return new ClienteResultDto { Exitoso = 2, Mensaje = "La operación tardó demasiado. Verifique si el cliente fue creado." };
        }
        catch (Exception ex)
        {
            return new ClienteResultDto { Exitoso = 0, Mensaje = ex.Message };
        }
    }

    public async Task<ClienteResultDto> EditarClienteAsync(EditarClienteDto dto, CancellationToken ct = default)
    {
        using var conn = _db.CreateConnection();
        try
        {
            var p = new DynamicParameters();
            p.Add("_IdCliente",      dto.IdCliente,      DbType.Int32);
            p.Add("_Alias",          "",                 DbType.String);
            p.Add("_Contrasena",     dto.Contrasena,     DbType.String);
            p.Add("_Empresa",        dto.Empresa,        DbType.String);
            p.Add("_NomContacto",    dto.NomContacto,    DbType.String);
            p.Add("_NroContacto",    dto.NroContacto,    DbType.String);
            p.Add("_CorreoContacto", dto.CorreoContacto, DbType.String);
            p.Add("_TarifaBase",     dto.TarifaBase,     DbType.Decimal);
            p.Add("_TarifaKm",       dto.TarifaKm,       DbType.Decimal);
            p.Add("_CreadoPor",      dto.ActualizadoPor, DbType.Int32);
            p.Add("_Exitoso", value: 0,  dbType: DbType.Int32,  direction: ParameterDirection.InputOutput);
            p.Add("_Mensaje", value: "", dbType: DbType.String, direction: ParameterDirection.InputOutput, size: 500);
            p.Add("_IdNuevo", value: 0,  dbType: DbType.Int32,  direction: ParameterDirection.InputOutput);

            await conn.ExecuteAsync(new CommandDefinition(
                "CALL sp_CreUpdCliente(@_IdCliente, @_Alias, @_Contrasena, @_Empresa, @_NomContacto, @_NroContacto, @_CorreoContacto, @_TarifaBase, @_TarifaKm, @_CreadoPor, @_Exitoso, @_Mensaje, @_IdNuevo)",
                p, commandType: CommandType.Text, cancellationToken: ct
            ));

            return new ClienteResultDto
            {
                Exitoso = p.Get<int>("_Exitoso"),
                Mensaje = p.Get<string>("_Mensaje"),
                IdNuevo = p.Get<int>("_IdNuevo"),
            };
        }
        catch (OperationCanceledException)
        {
            return new ClienteResultDto { Exitoso = 2, Mensaje = "La operación tardó demasiado. Verifique si el cliente fue actualizado." };
        }
        catch (Exception ex)
        {
            return new ClienteResultDto { Exitoso = 0, Mensaje = ex.Message };
        }
    }

    public async Task<ClienteResultDto> ActualizarEstadoClienteAsync(UpdEstadoClienteDto dto, CancellationToken ct = default)
    {
        using var conn = _db.CreateConnection();
        try
        {
            var p = new DynamicParameters();
            p.Add("_IdCliente",      dto.IdCliente,      DbType.Int32);
            p.Add("_NuevoEstado",    dto.NuevoEstado,    DbType.String);
            p.Add("_ActualizadoPor", dto.ActualizadoPor, DbType.Int32);
            p.Add("_Exitoso", value: 0,  dbType: DbType.Int32,  direction: ParameterDirection.InputOutput);
            p.Add("_Mensaje", value: "", dbType: DbType.String, direction: ParameterDirection.InputOutput, size: 500);

            await conn.ExecuteAsync(new CommandDefinition(
                "CALL sp_UpdEstadoCliente(@_IdCliente, @_NuevoEstado, @_ActualizadoPor, @_Exitoso, @_Mensaje)",
                p, commandType: CommandType.Text, cancellationToken: ct
            ));

            return new ClienteResultDto
            {
                Exitoso = p.Get<int>("_Exitoso"),
                Mensaje = p.Get<string>("_Mensaje"),
            };
        }
        catch (Exception ex)
        {
            return new ClienteResultDto { Exitoso = 0, Mensaje = ex.Message };
        }
    }
}
