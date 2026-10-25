using MoneyFlow.Api.Common;
using MoneyFlow.Api.Common.Authentication;
using MoneyFlow.Api.Database.Repositories.Users;

namespace MoneyFlow.Api.Features.Users.Login;

public class LoginHandler(
    IPasswordHasher passwordHasher,
    ITokenProvider tokenProvider,
    IUserRepository userRepository)
    : IHandler
{
    public async Task<Result<LoginResponse>> HandleAsync(LoginRequest request)
    {
        var user = await userRepository.FindByEmailAsync(request.Email);
        if (user is null)
        {
            return Result.Failure<LoginResponse>(UserErrors.InvalidCredentials);
        }

        var passwordsMatch = passwordHasher.Verify(request.Password, user.Password);

        if (!passwordsMatch)
        {
            return Result.Failure<LoginResponse>(UserErrors.InvalidCredentials);
        }

        var token = tokenProvider.GenerateToken(user);
        
        return Result.Success(new LoginResponse(token));
    }
}