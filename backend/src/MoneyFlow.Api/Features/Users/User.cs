namespace MoneyFlow.Api.Features.Users;

public class User
{
    public Guid UserId { get; private set; }
    public string UserName { get; private set; }
    public string Email { get; private set; }
    public string Password { get; private set; }

    private User(Guid userId, string userName, string email, string password)
    {
        UserId = userId;
        UserName = userName;
        Email = email;
        Password = password;
    }

    public static User Create(Guid userId, string userName, string email, string password)
    {
        return new User(userId, userName, email, password);
    }
}