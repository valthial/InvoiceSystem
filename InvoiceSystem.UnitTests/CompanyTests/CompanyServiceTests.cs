using InvoiceSystem.Application.Services;
using InvoiceSystem.Domain.Entities;
using InvoiceSystem.Domain.Interfaces;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace InvoiceSystem.UnitTests.Controllers.CompanyTests
{
    public class CompanyServiceTests
    {
        private readonly Mock<ICompanyRepository> _mockCompanyRepository;
        private readonly Mock<ILogger<CompanyService>> _mockLogger;
        private readonly CompanyService _companyService;

        public CompanyServiceTests()
        {
            _mockCompanyRepository = new Mock<ICompanyRepository>();
            _mockLogger = new Mock<ILogger<CompanyService>>();
            _companyService = new CompanyService(_mockCompanyRepository.Object, _mockLogger.Object);
        }

        [Fact]
        public async Task CreateCompanyAsync_ReturnsCompany_WhenCompanyIsValid()
        {
            var company = Company.Create("Test Company");
            _mockCompanyRepository.Setup(repo => repo.CreateCompanyAsync(It.IsAny<Company>()))
                .Returns(Task.CompletedTask);

            var result = await _companyService.CreateCompanyAsync(company);

            Assert.Equal(company.Id, result.Id);
            Assert.Equal(company.Name, result.Name);
        }

        [Fact]
        public async Task GetCompanyByIdAsync_ReturnsCompany_WhenCompanyExists()
        {
            var company = Company.Create("Test Company");
            _mockCompanyRepository.Setup(repo => repo.GetCompanyByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(company);

            var result = await _companyService.GetCompanyByIdAsync(1);

            Assert.NotNull(result);
            Assert.Equal(company.Id, result.Id);
        }

        [Fact]
        public async Task GetAllCompaniesAsync_ReturnsListOfCompanies()
        {
            var companies = new List<Company>
            {
                Company.Create("Test Company 1"),
                Company.Create("Test Company 2")
            };
            _mockCompanyRepository.Setup(repo => repo.GetAllCompaniesAsync(It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(companies);

            var result = await _companyService.GetAllCompaniesAsync(1, 10);

            Assert.Equal(2, result.Count);
        }
    }
}