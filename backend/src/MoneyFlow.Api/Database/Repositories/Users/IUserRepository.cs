using MoneyFlow.Api.Features.Users;

namespace MoneyFlow.Api.Database.Repositories.Users;

public interface IUserRepository
{
    Task<User?> FindByIdAsync(Guid id);
    Task<User?> FindByEmailAsync(string email);

    Task<bool> ExistsByEmailAsync(string email);
    
    Task<bool> ExistsByIdAsync(Guid id);

    Task<Guid> AddAsync(User user);
    
    Task<bool> DeleteAsync(Guid id);
}