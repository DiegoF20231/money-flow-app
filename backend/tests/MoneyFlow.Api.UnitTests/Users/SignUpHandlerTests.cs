using FluentAssertions;
using MoneyFlow.Api.Common;
using MoneyFlow.Api.Common.Authentication;
using MoneyFlow.Api.Database.Repositories.Users;
using MoneyFlow.Api.Features.Users;
using MoneyFlow.Api.Features.Users.SignUp;
using Moq;

namespace MoneyFlow.Api.UnitTests.Users;

public class SignUpHandlerTests
{
    private readonly Mock<IUserRepository> _userRepositoryMock = new();
    private readonly Mock<IPasswordHasher> _passwordHasherMock = new();

    [Fact]
    public async Task Handle_Should_Fail_WhenUserAlreadyExists()
    {
        // Arrange
        var command = new SignUpCommand("testuser", "test@test.com", "Password123!");
        var existingUser = User.Create(
            Guid.NewGuid(),
            command.Username,
            command.Email,
            "hashed_password");

        _userRepositoryMock
            .Setup(x => x.FindByEmailAsync(command.Email))
            .ReturnsAsync(existingUser);

        var handler = new SignUpHandler(
            _passwordHasherMock.Object,
            _userRepositoryMock.Object);

        // Act
        var result = await handler.HandleAsync(command);

        // Assert
        _passwordHasherMock.Verify(
            x => x.HashPassword(It.IsAny<string>()),
            Times.Never);

        _userRepositoryMock.Verify(
            x => x.AddAsync(It.IsAny<User>()),
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
            .Be(UserErrors.AlreadyExistsByEmail(command.Email));
    }

    [Fact]
    public async Task Handle_Should_Succeed_WhenUserDoesNotExist()
    {
        // Arrange
        var command = new SignUpCommand("testuser", "test@test.com", "Password123!");
        var expectedUserId = Guid.NewGuid();

        _userRepositoryMock
            .Setup(x => x.FindByEmailAsync(command.Email))
            .ReturnsAsync((User?)null);

        _passwordHasherMock
            .Setup(x => x.HashPassword(command.Password))
            .Returns("hashed_password");

        _userRepositoryMock
            .Setup(x => x.AddAsync(It.Is<User>(u =>
                u.Email == command.Email &&
                u.UserName == command.Username)))
            .ReturnsAsync(expectedUserId);

        var handler = new SignUpHandler(
            _passwordHasherMock.Object,
            _userRepositoryMock.Object);

        // Act
        var result = await handler.HandleAsync(command);

        // Assert
        _passwordHasherMock.Verify(
            x => x.HashPassword(command.Password),
            Times.Once);

        _userRepositoryMock.Verify(
            x => x.AddAsync(It.Is<User>(u =>
                u.Email == command.Email &&
                u.UserName == command.Username)),
            Times.Once);

        result
            .IsSuccess
            .Should()
            .BeTrue();

        result
            .Error
            .Should()
            .Be(Error.None);

        result
            .Value
            .Should()
            .NotBeNull();

        result
            .Value
            .UserId
            .Should()
            .Be(expectedUserId);
    }
}
