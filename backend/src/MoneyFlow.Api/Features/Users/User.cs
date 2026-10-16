namespace MoneyFlow.Api.Features.Users;

public class User
{
    public Guid UserId { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }

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