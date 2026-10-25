using FluentAssertions;
using MoneyFlow.Api.Common;
using MoneyFlow.Api.Common.Authentication;
using MoneyFlow.Api.Database.Repositories.Users;
using MoneyFlow.Api.Features.Users;
using MoneyFlow.Api.Features.Users.Login;
using Moq;

namespace MoneyFlow.Api.UnitTests.Users;

public class LoginHandlerTests
{
    private readonly Mock<IUserRepository> _userRepositoryMock = new();
    private readonly Mock<IPasswordHasher> _passwordHasherMock = new();
    private readonly Mock<ITokenProvider> _tokenProviderMock = new();

    [Fact]
    public async Task Handle_Should_Fail_WhenUserDoesNotExist()
    {
        var command = new LoginCommand("test@test.com", "1234567");
        _userRepositoryMock
            .Setup(x => x.FindByEmailAsync(
                command.Email
            ))
            .ReturnsAsync((User?)null);

        var handler = new LoginHandler(
            _passwordHasherMock.Object,
            _tokenProviderMock.Object,
            _userRepositoryMock.Object);

        var result = await handler.HandleAsync(command);

        _passwordHasherMock.Verify(
            x => x.Verify(
                It.IsAny<string>(),
                It.IsAny<string>()),
            Times.Never);

        _tokenProviderMock.Verify(
            x => x.GenerateToken(It.IsAny<User>()),
            Times.Never);


        result
            .IsSuccess
            .Should()
            .BeFalse();

        result
            .Error
            .Should()
            .NotBe(Error.None);

        result
            .Error
            .Should()
            .Be(UserErrors.InvalidCredentials);
    }
}