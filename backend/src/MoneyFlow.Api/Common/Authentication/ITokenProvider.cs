using MoneyFlow.Api.Features.Users;

namespace MoneyFlow.Api.Common.Authentication;

public interface ITokenProvider
{
    string GenerateToken(User user);
}