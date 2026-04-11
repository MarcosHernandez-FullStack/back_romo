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


    public async Task<TarifaDto?> ObtenerTarifarioGlobalAsync()
    {
        using var conn = _db.CreateConnection();

        return await conn.QueryFirstOrDefaultAsync<TarifaDto>(
            "SELECT * FROM fn_TarifarioGlobal()",
            commandType: CommandType.Text
        );
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

    public async Task<ParametroDto?> ObtenerParametroOperativoAsync()
{
    using var conn = _db.CreateConnection();

    return await conn.QueryFirstOrDefaultAsync<ParametroDto>(
        "SELECT * FROM fn_ParametroOperativo()",
        commandType: CommandType.Text
    );
}
}
