using FluentValidation;

namespace MoneyFlow.Api.Common.Filters;

public class EndpointValidationFilter<TBody>(IValidator<TBody> validator) : IEndpointFilter where TBody : class
{
    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        var request = context.Arguments.OfType<TBody>().FirstOrDefault();
        
        if (request is null)
        {
            return TypedResults.Problem("The request body was missing or could not be read.",
                statusCode: StatusCodes.Status400BadRequest);
        }

        var result = await validator.ValidateAsync(request, context.HttpContext.RequestAborted);
        if (!result.IsValid)
        {
            return TypedResults.ValidationProblem(result.ToDictionary());
        }

        return await next(context);
    }
}