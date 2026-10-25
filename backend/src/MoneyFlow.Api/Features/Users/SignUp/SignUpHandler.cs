using MoneyFlow.Api.Common;
using MoneyFlow.Api.Common.Authentication;
using MoneyFlow.Api.Database.Repositories.Users;

namespace MoneyFlow.Api.Features.Users.SignUp;

public class SignUpHandler(
    IPasswordHasher passwordHasher,
    IUserRepository userRepository) : IHandler
{
    public async Task<Result<SignUpResponse>> HandleAsync(SignUpCommand command)
    {
        var user = await userRepository.FindByEmailAsync(command.Email);

        if (user is not null)
        {
            return Result.Failure<SignUpResponse>(UserErrors.AlreadyExistsByEmail(command.Email));
        }

        var hashedPassword = passwordHasher.HashPassword(command.Password);
        var newUser = User.Create(
            Guid.CreateVersion7(),
            command.Username,
            command.Email,
            command.Password);


        var id = await userRepository.AddAsync(newUser);
        return Result.Success<SignUpResponse>(new SignUpResponse(id));
    }
}