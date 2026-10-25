namespace MoneyFlow.Api.Features.Users.SignUp;

public record SignUpCommand(string Username, string Email, string Password);