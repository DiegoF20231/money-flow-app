using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using MoneyFlow.Api.Common;
using MoneyFlow.Api.Common.Extensions;

namespace MoneyFlow.Api.Features.Categories.GetById;

public class GetCategoryByIdEndpoint : IEndpoint
{
    public static void MapEndpoint(IEndpointRouteBuilder app)
    {
        app
            .MapGet("/{categoryId:guid}", Handle)
            .WithDisplayName("Get category by id")
            .WithSummary("Get By Id");
    }

    private static async Task<IResult> Handle(
        [FromRoute] Guid categoryId,
        GetCategoryByIdQueryHandler handler,
        ClaimsPrincipal principal)
    {
        var query = new GetCategoryByIdQuery(categoryId, principal.GetUserId());
        var result = await handler.HandleAsync(query);

        if (!result.IsSuccess)
        {
            return TypedResults.BadRequest(result.Error);
        }

        return TypedResults.Ok(result.GetValue());
    }
}