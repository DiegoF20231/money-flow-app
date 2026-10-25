using MoneyFlow.Api.Common.Filters;
using MoneyFlow.Api.Features.Categories;
using MoneyFlow.Api.Features.Users;

namespace MoneyFlow.Api.Common.Extensions;

public static class EndpointsExtensions
{
    public static RouteHandlerBuilder WithValidation<TBody>(this RouteHandlerBuilder app) where TBody : class
    {
        app.AddEndpointFilter<EndpointValidationFilter<TBody>>();
        return app;
    }

    public static void MapEndpoints(this IEndpointRouteBuilder app)
    {
        app
            .MapUserEndpoints()
            .MapCategoryEndpoints();
    }
}