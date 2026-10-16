using System.Data;
using Npgsql;

namespace MoneyFlow.Api.Database.PgSql;

public class NpgSqlConnectionFactory(IConfiguration configuration) : IDbConnectionFactory
{
    private readonly string _connectionString = configuration.GetConnectionString("PgSqlConnection")
                                               ?? throw new ArgumentNullException(nameof(configuration),
                                                   "PgSqlConnection is null");
    public IDbConnection CreateConnection()
    {
        return new NpgsqlConnection(_connectionString);
    }
}