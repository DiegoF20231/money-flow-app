using System.ComponentModel;

namespace MoneyFlow.Api.Features.Categories.Create;

public record CreateCategoryRequest(
    [property: Description("Name of the category"), DefaultValue("New Category")]
    string Name,
    [property: Description("Color of the category"), DefaultValue("##5F1DCC")]
    string Color,
    [property: Description("Type of the category"), DefaultValue("Income")]
    string Type);