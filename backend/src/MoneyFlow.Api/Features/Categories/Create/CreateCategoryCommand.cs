namespace MoneyFlow.Api.Features.Categories.Create;

public record CreateCategoryCommand(string Name, string Color, string Type, Guid UserId);