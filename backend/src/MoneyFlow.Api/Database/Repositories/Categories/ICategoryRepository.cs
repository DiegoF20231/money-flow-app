using MoneyFlow.Api.Features.Categories;

namespace MoneyFlow.Api.Database.Repositories.Categories;

public interface ICategoryRepository
{
    Task<Category?> FindByIdAsync(Guid id);
    Task<Category?> FindByNameAsync(string name, Guid userId);
    Task<Guid> AddAsync(Category category);

    Task<bool> DeleteAsync(Guid id);
}