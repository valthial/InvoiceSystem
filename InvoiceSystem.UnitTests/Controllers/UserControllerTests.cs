using InvoiceSystem.Api.Controllers;
using InvoiceSystem.Application.Dto;
using InvoiceSystem.Application.Services;
using InvoiceSystem.Domain.Entities;
using InvoiceSystem.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace InvoiceSystem.UnitTests.Controllers;

public class UserControllerTests
{
    private readonly Mock<IUserRepository> _mockUserRepository;
    private readonly Mock<ILogger<UserService>> _mockLogger;
    private readonly UserService _userService;
    private readonly UserController _userController;

    public UserControllerTests()
    {
        _mockUserRepository = new Mock<IUserRepository>();
        _mockLogger = new Mock<ILogger<UserService>>();
        _userService = new UserService(_mockUserRepository.Object, _mockLogger.Object);
        _userController = new UserController(_userService);
    }

    [Fact]
    public async Task CreateUser_ReturnsOkResult_WhenModelStateIsValid()
    {
        var userDto = new UserDto
        {
            Email = "test@example.com",
            Password = "password123",
            CompanyId = "1"
        };

        var user = User.Create(userDto.Email, BCrypt.Net.BCrypt.HashPassword(userDto.Password), null, userDto.CompanyId);

        _mockUserRepository
            .Setup(repo => repo.UserExistsAsync(userDto.Email))
            .ReturnsAsync(false);

        _mockUserRepository
            .Setup(repo => repo.CreateUserAsync(It.IsAny<User>()))
            .Returns(Task.CompletedTask);

        var result = await _userController.CreateUser(userDto);

        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.NotNull(okResult.Value);
    }

    [Fact]
    public async Task CreateUser_ReturnsBadRequest_WhenModelStateIsInvalid()
    {
        var userDto = new UserDto { Email = "", Password = "", CompanyId = "" };
        _userController.ModelState.AddModelError("Email", "Email is required");

        var result = await _userController.CreateUser(userDto);

        Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact]
    public async Task GetUserByEmail_ReturnsNotFound_WhenUserDoesNotExist()
    {
        var email = "test@example.com";
        _mockUserRepository
            .Setup(repo => repo.GetUserByEmailAsync(email))
            .ReturnsAsync((User?)null);

        var result = await _userController.GetUserByEmail(email);

        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async Task GetUserByEmail_ReturnsOkResult_WhenUserExists()
    {
        var email = "test@example.com";
        var user = User.Create(email, BCrypt.Net.BCrypt.HashPassword("password123"), null, "1");

        _mockUserRepository
            .Setup(repo => repo.GetUserByEmailAsync(email))
            .ReturnsAsync(user);

        var result = await _userController.GetUserByEmail(email);

        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(user, okResult.Value);
    }

    [Fact]
    public async Task GetAllUsers_ReturnsOkResult_WithListOfUsers()
    {
        var users = new List<User>
        {
            User.Create("user1@example.com", BCrypt.Net.BCrypt.HashPassword("password1"), null, "1"),
            User.Create("user2@example.com", BCrypt.Net.BCrypt.HashPassword("password2"), null, "2")
        };

        _mockUserRepository
            .Setup(repo => repo.GetAllUsersAsync(1, 10))
            .ReturnsAsync(users);

        var result = await _userController.GetAllUsers();

        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(users, okResult.Value);
    }

    [Fact]
    public async Task ValidateCredentials_ReturnsUnauthorized_WhenCredentialsAreInvalid()
    {
        var credentials = new ValidateCredentialsDto
        {
            Email = "test@example.com",
            Password = "wrongpassword"
        };

        _mockUserRepository
            .Setup(repo => repo.GetUserByEmailAsync(credentials.Email))
            .ReturnsAsync((User?)null);

        var result = await _userController.ValidateCredentials(credentials);

        Assert.IsType<UnauthorizedResult>(result);
    }

    [Fact]
    public async Task ValidateCredentials_ReturnsOkResult_WhenCredentialsAreValid()
    {
        var credentials = new ValidateCredentialsDto
        {
            Email = "test@example.com",
            Password = "password123"
        };

        var company = Company.Create("Test Company");
        var user = User.Create(credentials.Email, BCrypt.Net.BCrypt.HashPassword(credentials.Password), company, company.Id);

        _mockUserRepository
            .Setup(repo => repo.GetUserByEmailAsync(credentials.Email))
            .ReturnsAsync(user);

        var result = await _userController.ValidateCredentials(credentials);

        var okResult = Assert.IsType<OkObjectResult>(result);
        var response = okResult.Value as (string UserId, string CompanyId)?;
        Assert.NotNull(response);
        Assert.Equal(user.Id, response.Value.UserId);
        Assert.Equal(user.Company.Id, response.Value.CompanyId);
    }
    
    [Fact]
    public async Task ValidateCredentials_ReturnsUnauthorized_WhenCompanyIsNull()
    {
        // Arrange
        var credentials = new ValidateCredentialsDto
        {
            Email = "test@example.com",
            Password = "password123"
        };

        var user = User.Create(credentials.Email, BCrypt.Net.BCrypt.HashPassword(credentials.Password), null, "1");

        _mockUserRepository
            .Setup(repo => repo.GetUserByEmailAsync(credentials.Email))
            .ReturnsAsync(user);

        var result = await _userController.ValidateCredentials(credentials);

        Assert.IsType<UnauthorizedResult>(result);
    }
}