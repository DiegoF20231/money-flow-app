using MoneyFlow.Api.Features.Users.Login;

namespace MoneyFlow.Api.Features.Users;

public static class UserEndpoints
{
    public static IEndpointRouteBuilder MapUserEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app
            .MapGroup("api/auth")
            .WithSummary("User Endpoints")
            .WithTags("Users");

        LoginEndpoint.MapEndpoint(group);

        return group;
    }
}