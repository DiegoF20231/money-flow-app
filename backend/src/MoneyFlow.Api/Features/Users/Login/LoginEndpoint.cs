using Microsoft.AspNetCore.Mvc;
using MoneyFlow.Api.Common;
using MoneyFlow.Api.Common.Extensions;

namespace MoneyFlow.Api.Features.Users.Login;

public class LoginEndpoint : IEndpoint
{
    public static void MapEndpoint(IEndpointRouteBuilder app)
    {
        app
            .MapPost("/login", Handle)
            .WithSummary("Login")
            .WithDescription("Login Endpoint")
            .WithName("Login")
            .WithValidation<LoginRequest>();
    }


    private static async Task<IResult> Handle([FromBody] LoginRequest request, LoginHandler handler)
    {
        var command = new LoginCommand(request.Email, request.Password);
        
        var result = await handler.HandleAsync(command);

        if (!result.IsSuccess)
        {
            return TypedResults.BadRequest(result.Error);
        }

        return TypedResults.Ok(result.Value);
    }
}