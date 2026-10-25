using MoneyFlow.Api.Common;
using MoneyFlow.Api.Database.Repositories.Categories;

namespace MoneyFlow.Api.Features.Categories.Create;

public class CreateCategoryCommandHandler(ICategoryRepository categoryRepository) : IHandler
{
    public async Task<Result<CreateCategoryResponse>> HandleAsync(CreateCategoryCommand command)
    {
        var category = await categoryRepository.FindByNameAsync(command.Name, command.UserId);

        if (category is not null)
        {
            return Result.Failure<CreateCategoryResponse>(CategoryErrors.AlreadyExistsByName(command.Name));
        }

        var newCategory = Category.Create(
            Guid.CreateVersion7(),
            command.Name,
            command.Color,
            command.Type
        );
        var id = await categoryRepository.AddAsync(newCategory);

        return Result.Success(new CreateCategoryResponse(id));
    }
}