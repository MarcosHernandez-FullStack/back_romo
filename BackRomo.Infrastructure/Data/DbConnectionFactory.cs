using System.Data;
using Microsoft.Data.SqlClient;
using Npgsql;

namespace BackRomo.Infrastructure.Data;

public class DbConnectionFactory
{
    private readonly string _connectionString;
    private readonly string _provider;

    public DbConnectionFactory(string connectionString, string provider)
    {
        _connectionString = connectionString;
        _provider         = provider;
    }

    public IDbConnection CreateConnection() => _provider == "PostgreSQL"
        ? new NpgsqlConnection(_connectionString)
        : new SqlConnection(_connectionString);
}
