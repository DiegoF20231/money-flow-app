namespace MoneyFlow.Api.Common.Authentication;

public interface IUserContext
{
    Guid UserId { get; }
}