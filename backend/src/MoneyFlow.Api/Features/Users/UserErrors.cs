using MoneyFlow.Api.Common;

namespace MoneyFlow.Api.Features.Users;

public static class UserErrors
{
    public static Error NotFoundById(Guid id)
        => new("UserErrors.NotFoundById", $"The user with id {id} does not exist.");

    public static Error NotFoundByEmail(string email)
        => new("UserErrors.NotFoundByEmail", $"The user with email {email} does not exist.");

    public static Error AlreadyExistsByEmail(string email)
        => new("UserErrors.AlreadyExistsByEmail", $"The user with email {email} already exists.");

    public static Error InvalidCredentials => new("UserErrors.InvalidCredentials", "Invalid credentials.");
}