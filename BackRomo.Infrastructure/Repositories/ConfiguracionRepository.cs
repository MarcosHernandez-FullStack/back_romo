using System.Data;
using BackRomo.Application.DTOs.Configuracion;
using BackRomo.Application.Interfaces;
using BackRomo.Infrastructure.Data;
using Dapper;

namespace BackRomo.Infrastructure.Repositories;

public class ConfiguracionRepository : IConfiguracionRepository
{
    private readonly DbConnectionFactory _db;

    public ConfiguracionRepository(DbConnectionFactory db)
    {
        _db = db;
    }
/* 
    //Con sql server
    public async Task<TarifaDto?> ObtenerTarifarioGlobalAsync()
    {
        using var conn = _db.CreateConnection();

        return await conn.QueryFirstOrDefaultAsync<TarifaDto>(
            "sp_TarifarioGlobal",
            commandType: CommandType.StoredProcedure
        );
    } */


    public async Task<TarifaDto?> ObtenerTarifarioGlobalAsync(CancellationToken ct = default)
    {
        using var conn = _db.CreateConnection();

        return await conn.QueryFirstOrDefaultAsync<TarifaDto>(new CommandDefinition(
            "SELECT * FROM fn_TarifarioGlobal()",
            commandType: CommandType.Text,
            cancellationToken: ct
        ));
    }

    /* 
    // Con sql server
    public async Task<ParametroDto?> ObtenerParametroOperativoAsync()
    {
        using var conn = _db.CreateConnection();

        return await conn.QueryFirstOrDefaultAsync<ParametroDto>(
            "sp_ParametroOperativo",
            commandType: CommandType.StoredProcedure
        );
    } */

    public async Task<ParametroDto?> ObtenerParametroOperativoAsync(CancellationToken ct = default)
    {
        using var conn = _db.CreateConnection();

        return await conn.QueryFirstOrDefaultAsync<ParametroDto>(new CommandDefinition(
            "SELECT * FROM fn_ParametroOperativo()",
            commandType: CommandType.Text,
            cancellationToken: ct
        ));
    }

    public async Task<ConfigResultDto> ActualizarReservaClienteOnAsync(bool value, int actualizadoPor, CancellationToken ct = default)
    {
        using var conn = _db.CreateConnection();
        try
        {
            var p = new DynamicParameters();
            p.Add("_Activo",         value,          DbType.Boolean);
            p.Add("_ActualizadoPor", actualizadoPor, DbType.Int32);
            p.Add("_Exitoso", value: 0,  dbType: DbType.Int32,  direction: ParameterDirection.InputOutput);
            p.Add("_Mensaje", value: "", dbType: DbType.String, direction: ParameterDirection.InputOutput, size: 500);

            await conn.ExecuteAsync(new CommandDefinition(
                "CALL sp_UpdReservaClienteOn(@_Activo, @_ActualizadoPor, @_Exitoso, @_Mensaje)",
                p, commandType: CommandType.Text, cancellationToken: ct
            ));

            return new ConfigResultDto { Exitoso = p.Get<int>("_Exitoso"), Mensaje = p.Get<string>("_Mensaje") };
        }
        catch (OperationCanceledException)
        {
            return new ConfigResultDto { Exitoso = 2, Mensaje = "La operación tardó demasiado. Verifique si el cambio fue aplicado." };
        }
        catch (Exception ex)
        {
            return new ConfigResultDto { Exitoso = 0, Mensaje = ex.Message };
        }
    }
}
