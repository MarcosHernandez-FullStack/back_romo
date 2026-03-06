using Microsoft.Data.SqlClient;

namespace BackRomo.Infrastructure.Data;

public class DbConnectionFactory
{
    private readonly string _connectionString;

    public DbConnectionFactory(string connectionString)
    {
        _connectionString = connectionString;
    }

    public SqlConnection CreateConnection() => new SqlConnection(_connectionString);
}
