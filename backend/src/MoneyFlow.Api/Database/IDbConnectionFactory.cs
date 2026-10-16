using System.Data;

namespace MoneyFlow.Api.Database;

public interface IDbConnectionFactory
{
    public IDbConnection CreateConnection();
}