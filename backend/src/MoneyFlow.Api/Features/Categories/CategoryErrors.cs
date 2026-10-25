using MoneyFlow.Api.Common;

namespace MoneyFlow.Api.Features.Categories;

public static class CategoryErrors
{
    public static Error NotFoundById(Guid id) =>
        new("CategoryErrors.NotFoundById",
            $"The category with id {id} does not exist.");

    public static Error AlreadyExistsByName(string name)
        => new("CategoryErrors.AlreadyExistsByName",
            $"The category with name {name} already exists.");
}