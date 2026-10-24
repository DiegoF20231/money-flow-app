namespace MoneyFlow.Api.Common;

public interface IEndpoint
{
    static abstract void MapEndpoint(IEndpointRouteBuilder app);
}