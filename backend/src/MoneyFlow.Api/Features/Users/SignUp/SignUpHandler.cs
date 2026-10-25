using MoneyFlow.Api.Common;
using MoneyFlow.Api.Common.Authentication;
using MoneyFlow.Api.Database.Repositories.Users;

namespace MoneyFlow.Api.Features.Users.SignUp;

public class SignUpHandler(
    IPasswordHasher passwordHasher,
    IUserRepository userRepository) : IHandler
{
    public async Task<Result<SignUpResponse>> HandleAsync(SignUpRequest request)
    {
        var user = await userRepository.FindByEmailAsync(request.Email);

        if (user is not null)
        {
            return Result.Failure<SignUpResponse>(UserErrors.AlreadyExistsByEmail(request.Email));
        }

        var hashedPassword = passwordHasher.HashPassword(request.Password);
        var newUser = User.Create(
            Guid.CreateVersion7(),
            request.Username,
            request.Email,
            request.Password);


        var id = await userRepository.AddAsync(newUser);
        return Result.Success<SignUpResponse>(new SignUpResponse(id));
    }
}