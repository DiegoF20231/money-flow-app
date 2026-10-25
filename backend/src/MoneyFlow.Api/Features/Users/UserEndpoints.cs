using MoneyFlow.Api.Features.Users.Login;

namespace MoneyFlow.Api.Features.Users;

public static class UserEndpoints
{
    public static IEndpointRouteBuilder MapUserEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app
            .MapGroup("api/auth")
            .WithSummary("User Endpoints Summary")
            .WithDescription("User Endpoints Description")
            .WithName("User Endpoints Name")
            .WithTags("Users Tag");

        LoginEndpoint.MapEndpoint(group);

        return group;
    }
}