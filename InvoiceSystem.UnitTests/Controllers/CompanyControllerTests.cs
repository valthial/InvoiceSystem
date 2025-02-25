// using InvoiceSystem.Api.Controllers;
// using InvoiceSystem.Application.Services;
// using InvoiceSystem.Domain.Entities;
// using InvoiceSystem.Domain.Interfaces;
// using Microsoft.AspNetCore.Mvc;
// using Microsoft.Extensions.Logging;
// using Moq;
// using Xunit;
//
// namespace InvoiceSystem.UnitTests.Controllers;
//
// public class CompanyControllerTests
// {
//     private readonly Mock<ICompanyRepository> _mockCompanyRepository;
//     private readonly Mock<ILogger<CompanyService>> _mockLogger;
//     private readonly CompanyService _companyService;
//     private readonly CompanyController _companyController;
//
//     public CompanyControllerTests()
//     {
//         _mockCompanyRepository = new Mock<ICompanyRepository>();
//         _mockLogger = new Mock<ILogger<CompanyService>>();
//         _companyService = new CompanyService(_mockCompanyRepository.Object, _mockLogger.Object);
//         _companyController = new CompanyController(_companyService);
//     }
//
//     [Fact]
//     public async Task CreateCompany_ReturnsOkResult_WhenModelStateIsValid()
//     {
//         var companyDto = IssuerCompany.Create("Test IssuerCompany");
//         var company = IssuerCompany.Create(companyDto.Name);
//
//         _mockCompanyRepository
//             .Setup(repo => repo.CompanyExistsAsync(companyDto.Id))
//             .ReturnsAsync(false);
//
//         _mockCompanyRepository
//             .Setup(repo => repo.CreateCompanyAsync(It.IsAny<IssuerCompany>()))
//             .Returns(Task.CompletedTask);
//
//         var result = await _companyController.CreateCompany(companyDto);
//
//         var okResult = Assert.IsType<OkObjectResult>(result);
//         Assert.NotNull(okResult.Value);
//     }
//
//     [Fact]
//     public async Task CreateCompany_ReturnsBadRequest_WhenModelStateIsInvalid()
//     {
//         var companyDto = IssuerCompany.Create("");
//         _companyController.ModelState.AddModelError("Name", "Name is required");
//
//         var result = await _companyController.CreateCompany(companyDto);
//
//         Assert.IsType<BadRequestObjectResult>(result);
//     }
//
//     [Fact]
//     public async Task GetCompanyById_ReturnsNotFound_WhenCompanyDoesNotExist()
//     {
//         const string companyId = "1";
//         _mockCompanyRepository
//             .Setup(repo => repo.GetCompanyByIdAsync(companyId))
//             .ReturnsAsync((IssuerCompany?)null);
//
//         var result = await _companyController.GetCompanyById(companyId);
//
//         Assert.IsType<NotFoundResult>(result);
//     }
//
//     [Fact]
//     public async Task GetCompanyById_ReturnsOkResult_WhenCompanyExists()
//     {
//         var companyId = "1";
//         var company = IssuerCompany.Create("Test IssuerCompany");
//
//         _mockCompanyRepository
//             .Setup(repo => repo.GetCompanyByIdAsync(companyId))
//             .ReturnsAsync(company);
//
//         var result = await _companyController.GetCompanyById(companyId);
//
//         var okResult = Assert.IsType<OkObjectResult>(result);
//         Assert.Equal(company, okResult.Value);
//     }
//
//     [Fact]
//     public async Task GetAllCompanies_ReturnsOkResult_WithListOfCompanies()
//     {
//         var companies = new List<IssuerCompany>
//         {
//             IssuerCompany.Create("IssuerCompany 1"),
//             IssuerCompany.Create("IssuerCompany 2")
//         };
//
//         _mockCompanyRepository
//             .Setup(repo => repo.GetAllCompaniesAsync(1, 10))
//             .ReturnsAsync(companies);
//
//         var result = await _companyController.GetAllCompanies();
//
//         var okResult = Assert.IsType<OkObjectResult>(result);
//         Assert.Equal(companies, okResult.Value);
//     }
//
//     [Fact]
//     public async Task GetAllCompanies_ReturnsOkResult_WithPagedResults()
//     {
//         var companies = new List<IssuerCompany>
//         {
//             IssuerCompany.Create("IssuerCompany 1"),
//             IssuerCompany.Create("IssuerCompany 2")
//         };
//
//         _mockCompanyRepository
//             .Setup(repo => repo.GetAllCompaniesAsync(2, 5))
//             .ReturnsAsync(companies);
//
//         var result = await _companyController.GetAllCompanies(2, 5);
//         
//         var okResult = Assert.IsType<OkObjectResult>(result);
//         Assert.Equal(companies, okResult.Value);
//     }
// }