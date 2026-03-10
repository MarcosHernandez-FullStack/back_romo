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

    public async Task<TarifaDto?> ObtenerTarifarioGlobalAsync()
    {
        using var conn = _db.CreateConnection();

        return await conn.QueryFirstOrDefaultAsync<TarifaDto>(
            "sp_TarifarioGlobal",
            commandType: CommandType.StoredProcedure
        );
    }
}
