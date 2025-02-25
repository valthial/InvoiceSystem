// using InvoiceSystem.Api.Controllers;
// using InvoiceSystem.Application.DTO;
// using InvoiceSystem.Domain.Entities;
// using InvoiceSystem.Domain.Interfaces.Services;
// using Microsoft.AspNetCore.Mvc;
// using Moq;
// using Xunit;
//
// namespace InvoiceSystem.UnitTests.Controllers;
//
// public class UserControllerTests
// {
//     private readonly Mock<IUserService> _mockUserService;
//     private readonly UserController _userController;
//
//     public UserControllerTests()
//     {
//         _mockUserService = new Mock<IUserService>();
//         _userController = new UserController(_mockUserService.Object);
//     }
//
//     [Fact]
//     public async Task CreateUser_ValidUser_ReturnsOkResult()
//     {
//         var user = User.Create("test@example.com", "password", null,"companyId");
//         _mockUserService.Setup(service => service.CreateUserAsync(user)).ReturnsAsync(user);
//
//         var result = await _userController.CreateUser(user);
//
//         var okResult = Assert.IsType<OkObjectResult>(result);
//         Assert.Equal(user, okResult.Value);
//     }
//
//     [Fact]
//     public async Task GetUserByEmail_UserExists_ReturnsOkResult()
//     {
//         var user = User.Create("test@example.com", "hashedpassword", null,"companyId");
//         _mockUserService.Setup(service => service.GetUserByIdAsync(user.Email)).ReturnsAsync(user);
//
//         var result = await _userController.GetUserByEmail(user.Email);
//
//         var okResult = Assert.IsType<OkObjectResult>(result);
//         Assert.Equal(user, okResult.Value);
//     }
//
//     [Fact]
//     public async Task GetUserByEmail_UserDoesNotExist_ReturnsNotFound()
//     {
//         _mockUserService.Setup(service => service.GetUserByIdAsync(It.IsAny<string>())).ReturnsAsync((User)null);
//
//         var result = await _userController.GetUserByEmail("nonexistent@example.com");
//
//         Assert.IsType<NotFoundResult>(result);
//     }
//
//     [Fact]
//     public async Task ValidateCredentials_ValidCredentials_ReturnsOkResult()
//     {
//         var credentials = new ValidateCredentialsDto
//         {
//             Email = "test@example.com",
//             Password = "password"
//         };
//         _mockUserService.Setup(service => service.ValidateUserCredentialsAsync(credentials.Email, credentials.Password))
//             .ReturnsAsync(("userId", "companyId"));
//         
//         var result = await _userController.ValidateCredentials(credentials);
//         var okResult = Assert.IsType<OkObjectResult>(result);
//         Assert.Equal(("userId", "companyId"), okResult.Value);
//     }
//
//     [Fact]
//     public async Task ValidateCredentials_InvalidCredentials_ReturnsUnauthorized()
//     {
//         var credentials = new ValidateCredentialsDto
//         {
//             Email = "test@example.com",
//             Password = "wrongpassword"
//         };
//
//         _mockUserService.Setup(service => service.ValidateUserCredentialsAsync(credentials.Email, credentials.Password))
//             .ReturnsAsync((ValueTuple<string, string>?)null);
//
//         var result = await _userController.ValidateCredentials(credentials);
//
//         Assert.IsType<UnauthorizedResult>(result);
//     }
// }