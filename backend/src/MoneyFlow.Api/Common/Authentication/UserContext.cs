using MoneyFlow.Api.Common.Extensions;

namespace MoneyFlow.Api.Common.Authentication;

public class UserContext(IHttpContextAccessor httpContextAccessor) : IUserContext
{
    public Guid UserId => httpContextAccessor
                              .HttpContext?
                              .User
                              .GetUserId() ??
                          throw new InvalidOperationException("");
}