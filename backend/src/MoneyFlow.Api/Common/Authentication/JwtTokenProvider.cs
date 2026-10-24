using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using MoneyFlow.Api.Features.Users;

namespace MoneyFlow.Api.Common.Authentication;

public class JwtTokenProvider(IConfiguration configuration) : ITokenProvider
{
    public string GenerateToken(User user)
    {
        var secretKey = configuration["JwtOptions:SecretKey"];
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey!));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(
                [
                    new Claim(JwtRegisteredClaimNames.Sub, user.UserId.ToString()),
                    new Claim(JwtRegisteredClaimNames.Email, user.Email)
                ]
            ),
            Expires = DateTime.UtcNow.AddYears(365),
            SigningCredentials = credentials,
            Issuer = configuration["JwtOptions:Issuer"],
            Audience = configuration["JwtOptions:Audience"],
        };

        var jwtHandler = new JsonWebTokenHandler();
        var token = jwtHandler.CreateToken(tokenDescriptor);

        return token;
    }
}