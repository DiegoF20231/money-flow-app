using Dapper;
using MoneyFlow.Api.Features.Categories;

namespace MoneyFlow.Api.Database.Repositories.Categories;

public class CategoryDapperRepository(IDbConnectionFactory connectionFactory) : ICategoryRepository
{
    public async Task<Category?> FindByIdAsync(Guid id)
    {
        using var connection = connectionFactory.CreateConnection();
        const string sql = @"
                    SELECT 
                         category_id,
                         name,
                         color,
                         type
                    FROM categories
                    WHERE category_id = @CategoryId
                    ";

        var category = await connection
            .QueryFirstOrDefaultAsync<Category>(sql, new { CategoryId = id });
        return category;
    }

    public async Task<Category?> FindByNameAsync(string name, Guid userId)
    {
        using var connection = connectionFactory.CreateConnection();
        const string sql = @"
                    SELECT 
                         category_id,
                         name,
                         color,
                         type
                    FROM categories
                    WHERE name = @Name AND user_id = @UserId;";

        var category = await connection
            .QueryFirstOrDefaultAsync<Category>(sql,
                new { Name = name, UserId = userId });

        return category;
    }

    public async Task<Guid> AddAsync(Category category)
    {
        using var connection = connectionFactory.CreateConnection();
        const string sql = @"
                    INSERT INTO categories (CategoryId, Name, Color, Type)
                    VALUES (@CategoryId, @Name, @Color, @Type)
                    RETURNING CategoryId;
                    ";

        var id = await connection.ExecuteScalarAsync<Guid>(sql,
            new
            {
                category.CategoryId,
                category.Name,
                category.Color,
                category.Type
            });

        return id;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        using var connection = connectionFactory.CreateConnection();
        const string sql = @"
                        DELETE 
                        FROM categories
                        WHERE category_id = @CategoryId;";
        var rowsAffected = await connection
            .ExecuteAsync(sql, new { CategoryId = id });

        return rowsAffected > 0;
    }
}