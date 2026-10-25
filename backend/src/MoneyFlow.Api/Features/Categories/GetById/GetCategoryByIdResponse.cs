namespace MoneyFlow.Api.Features.Categories.GetById;

public record GetCategoryByIdResponse(Guid CategoryId, string Name, string Color, string Type);