namespace MoneyFlow.Api.Common.Extensions;

public static class HandlerExtensions
{
    public static IServiceCollection AddHandlers(this IServiceCollection services)
    {
        var types = typeof(IHandler)
            .Assembly
            .GetTypes()
            .Where(type => type is { IsClass: true, IsAbstract: false }
                           && typeof(IHandler).IsAssignableFrom(type));

        foreach (var type in types)
        {
            services.AddScoped(type);
        }

        return services;
    }
}