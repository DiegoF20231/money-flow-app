using MoneyFlow.Api.Common;

namespace MoneyFlow.Api.Features.Users.SignUp;

public class SignUpEndpoint : IEndpoint
{
    public static void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("/signup", Handle);
    }

    private static async Task<IResult> Handle(SignUpRequest request, SignUpHandler handler)
    {
        var result = await handler.HandleAsync(request);

        if (!result.IsSuccess)
        {
            return TypedResults.BadRequest(result.Error);
        }

        return TypedResults.Ok(result.Value);
    }
}