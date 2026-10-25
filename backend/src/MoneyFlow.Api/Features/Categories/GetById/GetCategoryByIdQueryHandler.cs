using MoneyFlow.Api.Common;
using MoneyFlow.Api.Database.Repositories.Categories;

namespace MoneyFlow.Api.Features.Categories.GetById;

public class GetCategoryByIdQueryHandler(ICategoryRepository categoryRepository) : IHandler
{
    public async Task<Result<GetCategoryByIdResponse>> HandleAsync(GetCategoryByIdQuery query)
    {
        var category = await categoryRepository.FindByIdAsync(query.CategoryId, query.UserId);

        if (category is null)
        {
            return Result.Failure<GetCategoryByIdResponse>(CategoryErrors.NotFoundById(query.CategoryId));
        }

        var response = new GetCategoryByIdResponse(
            category.CategoryId,
            category.Name,
            category.Color,
            category.Type);

        return Result.Success(response);
    }
}