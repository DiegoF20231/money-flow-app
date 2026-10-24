using MoneyFlow.Api.Common.Filters;
using MoneyFlow.Api.Features.Users;

namespace MoneyFlow.Api.Common.Extensions;

public static class EndpointsExtensions
{
    public static void WithValidation<TBody>(this RouteHandlerBuilder app) where TBody : class
    {
        app.AddEndpointFilter<EndpointValidationFilter<TBody>>();
    }

    public static void MapEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapUserEndpoints();
    }
}