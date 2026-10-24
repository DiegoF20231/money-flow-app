using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace MoneyFlow.Api.Common.Extensions;

public static class ClaimsPrincipalExtensions
{
    public static Guid GetUserId(this ClaimsPrincipal principal)
    {
        var userId = principal?
            .FindFirst(JwtRegisteredClaimNames.Sub)?
            .Value;

        return Guid.TryParse(userId, out var guid)
            ? guid
            : throw new Exception("Invalid user id");
    }
}