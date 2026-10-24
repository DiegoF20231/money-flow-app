namespace MoneyFlow.Api.Common.Authentication;

public interface IPasswordHasher
{
    string HashPassword(string password);
    bool Verify(string password, string hashedPassword);
}