using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using MoneyFlow.Api.Common;
using MoneyFlow.Api.Common.Extensions;

namespace MoneyFlow.Api.Features.Categories.Create;

public class CreateCategoryEndpoint : IEndpoint
{
    public static void MapEndpoint(IEndpointRouteBuilder app)
    {
        app
            .MapPost("/", Handle)
            .WithSummary("Create")
            .WithDescription("Create Category Endpoint")
            .WithValidation<CreateCategoryRequest>();
        
    }

    private static async Task<IResult> Handle([FromBody] CreateCategoryRequest request,
        CreateCategoryCommandHandler handler,
        ClaimsPrincipal principal)
    {
        var command = new CreateCategoryCommand(
            request.Name,
            request.Color,
            request.Type,
            principal.GetUserId());

        var result = await handler.HandleAsync(command);

        if (!result.IsSuccess)
        {
            return TypedResults.BadRequest(result.Error);
        }

        return TypedResults.Created();
    }
}