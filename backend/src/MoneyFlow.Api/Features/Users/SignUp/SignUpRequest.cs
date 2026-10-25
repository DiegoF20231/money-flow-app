using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MoneyFlow.Api.Features.Users.SignUp;

public record SignUpRequest(
    [property: Description("Username of the user"), DefaultValue("John Doe")]
    string Username,
    [property: Description("Email of the user"), DefaultValue("test@test.com")]
    string Email,
    [property: Description("Password of the user"), DefaultValue("1234567"), MinLength(6)]
    string Password);