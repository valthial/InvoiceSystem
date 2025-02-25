using AutoMapper;
using InvoiceSystem.Api.Controllers;
using InvoiceSystem.Application.Dto;
using InvoiceSystem.Application.Mapper;
using InvoiceSystem.Domain.Entities;
using InvoiceSystem.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace InvoiceSystem.UnitTests.Controllers.CompanyTests
{
    public class CompanyControllerTests
    {
        private readonly Mock<ICompanyService> _mockCompanyService;
        private readonly IMapper _mapper;
        private readonly CompanyController _controller;

        public CompanyControllerTests()
        {
            _mockCompanyService = new Mock<ICompanyService>();
            var config = new MapperConfiguration(cfg => cfg.AddProfile<CompanyProfile>());
            _mapper = config.CreateMapper();
            _controller = new CompanyController(_mockCompanyService.Object, _mapper);
        }

        [Fact]
        public async Task CreateCompany_ReturnsOkResult_WhenModelStateIsValid()
        {
            var companyDto = new CompanyDto { Name = "Test Company" };
            var company = _mapper.Map<Company>(companyDto);
            _mockCompanyService.Setup(service => service.CreateCompanyAsync(It.IsAny<Company>()))
                .ReturnsAsync(company);

            var result = await _controller.CreateCompany(companyDto);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnCompany = Assert.IsType<Company>(okResult.Value);
            Assert.Equal(company.Name, returnCompany.Name);
        }

        [Fact]
        public async Task GetCompanyById_ReturnsNotFound_WhenCompanyDoesNotExist()
        {
            _mockCompanyService.Setup(service => service.GetCompanyByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((Company)null);

            var result = await _controller.GetCompanyById(1);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task GetCompanyById_ReturnsOkResult_WhenCompanyExists()
        {
            var company = Company.Create("Test Company");
            _mockCompanyService.Setup(service => service.GetCompanyByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(company);

            var result = await _controller.GetCompanyById(1);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnCompany = Assert.IsType<Company>(okResult.Value);
            Assert.Equal(company.Id, returnCompany.Id);
        }

        [Fact]
        public async Task GetAllCompanies_ReturnsOkResult_WithListOfCompanies()
        {
            var companies = new List<Company>
            {
                Company.Create("Test Company 1"),
                Company.Create("Test Company 2"),
            };
            _mockCompanyService.Setup(service => service.GetAllCompaniesAsync(It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(companies);

            var result = await _controller.GetAllCompanies(1, 10);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnCompanies = Assert.IsType<List<Company>>(okResult.Value);
            Assert.Equal(2, returnCompanies.Count);
        }
    }
}