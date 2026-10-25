using MoneyFlow.Api.Features.Categories.Create;
using MoneyFlow.Api.Features.Categories.GetById;

namespace MoneyFlow.Api.Features.Categories;

public static class CategoryEndpoints
{
    public static IEndpointRouteBuilder MapCategoryEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app
            .MapGroup("api/categories")
            .WithTags("Categories")
            .WithDescription("Category endpoints")
            .WithName("Category endpoints name")
            .RequireAuthorization();

        CreateCategoryEndpoint.MapEndpoint(group);
        GetCategoryByIdEndpoint.MapEndpoint(group);

        return app;
    }
}