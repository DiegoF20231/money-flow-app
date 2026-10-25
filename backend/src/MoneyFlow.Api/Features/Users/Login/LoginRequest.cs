using System.ComponentModel;

namespace MoneyFlow.Api.Features.Users.Login;

public record LoginRequest(
    [property: Description("Email of the user"), DefaultValue("test@test.com")]
    string Email,
    [property: Description("Password of the user"), DefaultValue("1234567")]
    string Password);