using MoneyFlow.Api.Common.Filters;

namespace MoneyFlow.Api.Common.Extensions;

public static class EndpointsExtensions
{
    public static void WithValidation<TBody>(this RouteHandlerBuilder app) where TBody : class
    {
        app.AddEndpointFilter<EndpointValidationFilter<TBody>>();
    }
}