using Dapper;
using MoneyFlow.Api.Features.Users;

namespace MoneyFlow.Api.Database.Repositories.Users;

public class UserDapperRepository(IDbConnectionFactory connectionFactory) : IUserRepository
{
    public async Task<User?> FindByIdAsync(Guid id)
    {
        using var connection = connectionFactory.CreateConnection();

        string sql = @"
                SELECT 
                    user_id,
                    username,
                    email,
                    password
                FROM users
                WHERE user_id = @Id; ";

        var user = await connection.QueryFirstOrDefaultAsync<User>(sql, new { Id = id });
        return user;
    }

    public async Task<User?> FindByEmailAsync(string email)
    {
        using var connection = connectionFactory.CreateConnection();

        string sql = @"
                SELECT 
                    user_id,
                    username,
                    email,
                    password
                FROM users
                WHERE email = @Email; ";

        var user = await connection.QueryFirstOrDefaultAsync<User>(sql, new { Email = email });
        return user;
    }

    public async Task<bool> ExistsByEmailAsync(string email)
    {
        using var connection = connectionFactory.CreateConnection();
        string sql = @"
                     SELECT EXISTS
                                (
                                    SELECT 1 FROM users 
                                             WHERE email = @Email      
                                );
                                    ";

        var result = await connection.ExecuteScalarAsync<bool>(sql, new { Email = email });
        return result;
    }

    public async Task<bool> ExistsByIdAsync(Guid id)
    {
        using var connection = connectionFactory.CreateConnection();
        string sql = @"
                     SELECT EXISTS
                                (
                                    SELECT 1 FROM users 
                                             WHERE user_id = @id      
                                );
                                    ";

        var result = await connection.ExecuteScalarAsync<bool>(sql, new { Id = id });
        return result;
    }

    public async Task<Guid> AddAsync(User user)
    {
        using var connection = connectionFactory.CreateConnection();

        string sql = @"
                INSERT INTO users(user_id,username,email,password)
                VALUES(@Id, @UserName, @Email, @Password)
                RETURNING user_id;
                ";

        var id = await connection.ExecuteScalarAsync<Guid>(sql,
            new
            {
                Id = user.UserId,
                UserName = user.UserName,
                Email = user.Email,
                Password = user.Password
            });

        return id;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        using var connection = connectionFactory.CreateConnection();

        string sql = @"
                           DELETE FROM users
                           WHERE user_id = @Id;
                           ";
        var rowsAffected = await connection.ExecuteAsync(sql, new { Id = id });
        
        return rowsAffected > 0;
    }
}