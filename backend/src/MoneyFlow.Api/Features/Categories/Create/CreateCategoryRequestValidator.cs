using System.Data;
using FluentValidation;

namespace MoneyFlow.Api.Features.Categories.Create;

public class CreateCategoryRequestValidator : AbstractValidator<CreateCategoryRequest>
{
    public CreateCategoryRequestValidator()
    {
        RuleFor(r => r.Name)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithMessage("Name is required");

        RuleFor(r => r.Color)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithMessage("Color is required")
            .Must(color => color.Length == 7)
            .WithMessage("Color length must be 7 characters");

        RuleFor(r => r.Type)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithMessage("Type is required")
            .Must(type => type == CategoryType.Income || type == CategoryType.Expense)
            .WithMessage("Type should be income or expense");
    }
}