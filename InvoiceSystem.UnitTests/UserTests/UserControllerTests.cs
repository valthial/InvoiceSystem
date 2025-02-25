using AutoMapper;
using InvoiceSystem.Api.Controllers;
using InvoiceSystem.Application.Dto;
using InvoiceSystem.Application.DTO;
using InvoiceSystem.Domain.Entities;
using InvoiceSystem.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace InvoiceSystem.UnitTests.UserTests
{
    public class UserControllerTests
    {
        private readonly Mock<IUserService> _mockUserService;
        private readonly Mock<IMapper> _mockMapper;
        private readonly UserController _controller;

        public UserControllerTests()
        {
            _mockUserService = new Mock<IUserService>();
            _mockMapper = new Mock<IMapper>();
            _controller = new UserController(_mockUserService.Object, _mockMapper.Object);
        }

        [Fact]
        public async Task CreateUser_ValidInput_ReturnsOkResult()
        {
            var userDto = new UserDto
            {
                Email = "test@example.com",
                Password = "Password123",
                CompanyId = 1
            };

            var user = User.Create(userDto.Email, userDto.Password, userDto.CompanyId);

            _mockMapper.Setup(m => m.Map<User>(userDto)).Returns(user);
            _mockUserService.Setup(s => s.CreateUserAsync(user)).ReturnsAsync(user);

            var result = await _controller.CreateUser(userDto);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedUser = Assert.IsType<User>(okResult.Value);
            Assert.Equal(user.Email, returnedUser.Email);
        }

        [Fact]
        public async Task CreateUser_InvalidInput_ReturnsBadRequest()
        {
            var userDto = new UserDto
            { };

            _controller.ModelState.AddModelError("Email", "Email is required");

            var result = await _controller.CreateUser(userDto);

            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task GetUserByEmail_ValidEmail_ReturnsOkResult()
        {
            var email = "test@example.com";
            var user = User.Create(email, "Password123", 1);

            _mockUserService.Setup(s => s.GetUserByIdAsync(email)).ReturnsAsync(user);

            var result = await _controller.GetUserByEmail(email);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedUser = Assert.IsType<User>(okResult.Value);
            Assert.Equal(email, returnedUser.Email);
        }

        [Fact]
        public async Task GetUserByEmail_InvalidEmail_ReturnsNotFound()
        {
            var email = "nonexistent@example.com";
            _mockUserService.Setup(s => s.GetUserByIdAsync(email)).ReturnsAsync((User)null);

            var result = await _controller.GetUserByEmail(email);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task GetAllUsers_ValidPagination_ReturnsOkResult()
        {
            var page = 1;
            var pageSize = 10;
            var users = new List<User>
            {
                User.Create("user1@example.com", "Password123", 1),
                User.Create("user2@example.com", "Password456", 2)
            };

            _mockUserService.Setup(s => s.GetAllUsersAsync(page, pageSize)).ReturnsAsync(users);

            var result = await _controller.GetAllUsers(page, pageSize);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedUsers = Assert.IsType<List<User>>(okResult.Value);
            Assert.Equal(2, returnedUsers.Count);
        }

        [Fact]
        public async Task ValidateCredentials_ValidCredentials_ReturnsOkResult()
        {
            var credentials = new ValidateCredentialsDto
            {
                Email = "test@example.com",
                Password = "Password123"
            };

            var user = User.Create(credentials.Email, credentials.Password, 1);
            var validationResult = (UserId: user.Id.ToString(), CompanyId: user.IssuerCompanyId);

            _mockUserService
                .Setup(s => s.ValidateUserCredentialsAsync(credentials.Email, credentials.Password))
                .ReturnsAsync(validationResult);

            var result = await _controller.ValidateCredentials(credentials);
            
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedResult = Assert.IsType<(string UserId, int CompanyId)>(okResult.Value);
            Assert.Equal(validationResult.UserId, returnedResult.UserId);
            Assert.Equal(validationResult.CompanyId, returnedResult.CompanyId);
        }

        [Fact]
        public async Task ValidateCredentials_InvalidCredentials_ReturnsUnauthorized()
        {
            var credentials = new ValidateCredentialsDto
            {
                Email = "test@example.com",
                Password = "WrongPassword"
            };

            _mockUserService
                .Setup(s => s.ValidateUserCredentialsAsync(credentials.Email, credentials.Password))
                .ReturnsAsync((null as (string UserId, int CompanyId)?)); // Explicitly cast null

            var result = await _controller.ValidateCredentials(credentials);

            Assert.IsType<UnauthorizedResult>(result);
        }
    }
}