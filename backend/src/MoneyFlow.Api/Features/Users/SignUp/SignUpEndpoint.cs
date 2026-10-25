using Microsoft.AspNetCore.Mvc;
using MoneyFlow.Api.Common;

namespace MoneyFlow.Api.Features.Users.SignUp;

public class SignUpEndpoint : IEndpoint
{
    public static void MapEndpoint(IEndpointRouteBuilder app)
    {
        app
            .MapPost("/signup", Handle)
            .WithSummary("Sign Up")
            .WithDescription("Sign Up Endpoint")
            .WithName("Sign Up");
    }

    private static async Task<IResult> Handle([FromBody] SignUpRequest request, SignUpCommandHandler handler)
    {
        var command = new SignUpCommand(request.Username, request.Email, request.Password);
        var result = await handler.HandleAsync(command);

        if (!result.IsSuccess)
        {
            return TypedResults.BadRequest(result.Error);
        }

        return TypedResults.Ok(result.Value);
    }
}