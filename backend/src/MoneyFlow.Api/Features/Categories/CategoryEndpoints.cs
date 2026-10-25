using MoneyFlow.Api.Features.Categories.Create;
using MoneyFlow.Api.Features.Categories.GetById;

namespace MoneyFlow.Api.Features.Categories;

public static class CategoryEndpoints
{
    public static IEndpointRouteBuilder MapCategoryEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app
            .MapGroup("api/categories")
            .WithSummary("Category Endpoints")
            .WithTags("Categories")
            .RequireAuthorization();

        CreateCategoryEndpoint.MapEndpoint(group);
        GetCategoryByIdEndpoint.MapEndpoint(group);

        return app;
    }
}