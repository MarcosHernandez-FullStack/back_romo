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

    public async Task<IEnumerable<ClienteDto>> ListarClientesAsync(string? estado, int? id)
    {
        using var conn = _db.CreateConnection();

        return await conn.QueryAsync<ClienteDto>(
            "SELECT * FROM fn_ListClientes(@Estado, @Id)",
            new { Estado = estado, Id = id },
            commandType: CommandType.Text
        );
    }
}
