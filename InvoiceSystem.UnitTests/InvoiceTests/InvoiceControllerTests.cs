using AutoMapper;
using FluentValidation;
using InvoiceSystem.Api.Controllers;
using InvoiceSystem.Application.Dto;
using InvoiceSystem.Domain.Entities;
using InvoiceSystem.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace InvoiceSystem.UnitTests.InvoiceTests
{
    public class InvoiceControllerTests
    {
        private readonly Mock<IInvoiceService> _mockInvoiceService;
        private readonly Mock<IMapper> _mockMapper;
        private readonly InvoiceController _controller;

        public InvoiceControllerTests()
        {
            _mockInvoiceService = new Mock<IInvoiceService>();
            _mockMapper = new Mock<IMapper>();
            _controller = new InvoiceController(_mockInvoiceService.Object, _mockMapper.Object);
        }

        [Fact]
        public async Task CreateInvoice_ValidInput_ReturnsOkResult()
        {
            var invoiceDto = new InvoiceDto
            {
                DateIssued = DateTimeOffset.UtcNow,
                NetAmount = 100,
                VatAmount = 20,
                TotalAmount = 120,
                Description = "Test Invoice",
                IssuerCompanyId = 1,
                CounterPartyCompanyId = 2
            };

            var invoice = Invoice.Create(
                invoiceDto.DateIssued,
                invoiceDto.IssuerCompanyId,
                invoiceDto.CounterPartyCompanyId,
                invoiceDto.NetAmount,
                invoiceDto.VatAmount,
                invoiceDto.TotalAmount,
                invoiceDto.Description
            );

            _mockMapper.Setup(m => m.Map<Invoice>(invoiceDto)).Returns(invoice);
            _mockInvoiceService.Setup(s => s.CreateInvoiceAsync(invoice)).ReturnsAsync(invoice);

            var result = await _controller.CreateInvoice(invoiceDto);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedInvoice = Assert.IsType<Invoice>(okResult.Value);
            Assert.Equal(invoice.Id, returnedInvoice.Id);
        }

        [Fact]
        public async Task CreateInvoice_InvalidInput_ReturnsBadRequest()
        {
            var invoiceDto = new InvoiceDto { };

            _controller.ModelState.AddModelError("Description", "Description is required");

            var result = await _controller.CreateInvoice(invoiceDto);

            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task CreateInvoice_ValidationException_ReturnsBadRequest()
        {
            var invoiceDto = new InvoiceDto
            {
                DateIssued = DateTimeOffset.UtcNow,
                NetAmount = 100,
                VatAmount = 20,
                TotalAmount = 120,
                Description = "Test Invoice",
                IssuerCompanyId = 1,
                CounterPartyCompanyId = 2
            };
            
            var invoice = Invoice.Create(
                invoiceDto.DateIssued,
                invoiceDto.IssuerCompanyId,
                invoiceDto.CounterPartyCompanyId,
                invoiceDto.NetAmount,
                invoiceDto.VatAmount,
                invoiceDto.TotalAmount,
                invoiceDto.Description
            );

            _mockMapper.Setup(m => m.Map<Invoice>(invoiceDto)).Returns(invoice);
            _mockInvoiceService.Setup(s => s.CreateInvoiceAsync(invoice))
                .ThrowsAsync(new ValidationException("Validation failed"));

            var result = await _controller.CreateInvoice(invoiceDto);

            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.NotNull(badRequestResult.Value);
        }

        [Fact]
        public async Task GetInvoiceByIdAsync_ValidId_ReturnsOkResult()
        {
            var invoice = Invoice.Create(
                DateTimeOffset.UtcNow,
                1, 
                2, 
                100, 
                20, 
                120,
                "Test Invoice"
                );

            _mockInvoiceService.Setup(s => s.GetInvoiceByIdAsync(0)).ReturnsAsync(invoice);

            var result = await _controller.GetInvoiceByIdAsync(0);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedInvoice = Assert.IsType<Invoice>(okResult.Value);
            Assert.Equal(0, returnedInvoice.Id);
        }

        [Fact]
        public async Task GetInvoiceByIdAsync_InvalidId_ReturnsNotFound()
        {
            _mockInvoiceService.Setup(s => s.GetInvoiceByIdAsync(1)).ReturnsAsync((Invoice)null);
            var result = await _controller.GetInvoiceByIdAsync(1);
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task GetSentInvoices_ValidCompanyId_ReturnsOkResult()
        {
            var companyId = 1;
            var invoices = new List<Invoice>
            {
                Invoice.Create(
                    DateTimeOffset.UtcNow,
                    companyId,
                    2,
                    100,
                    20,
                    120,
                    "Test Invoice"
                ),
                Invoice.Create(
                    DateTimeOffset.UtcNow,
                    companyId,
                    2,
                    100,
                    20,
                    120,
                    "Test Invoice"
                )
            };

            _mockInvoiceService.Setup(s => s.GetAllInvoicesAsync()).ReturnsAsync(invoices);

            var result = await _controller.GetSentInvoices(companyId);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedInvoices = Assert.IsType<List<Invoice>>(okResult.Value);
            Assert.Equal(2, returnedInvoices.Count);
        }

        [Fact]
        public async Task GetReceivedInvoices_ValidCompanyId_ReturnsOkResult()
        {
            var companyId = 2;
            var invoices = new List<Invoice>
            {
                Invoice.Create(
                    DateTimeOffset.UtcNow,
                    companyId,
                    2,
                    100,
                    20,
                    120,
                    "Test Invoice"
                ),
                Invoice.Create(
                    DateTimeOffset.UtcNow,
                    companyId,
                    2,
                    100,
                    20,
                    120,
                    "Test Invoice"
                )
            };

            _mockInvoiceService.Setup(s => s.GetAllInvoicesAsync()).ReturnsAsync(invoices);

            var result = await _controller.GetReceivedInvoices(companyId);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedInvoices = Assert.IsType<List<Invoice>>(okResult.Value);
            Assert.Equal(2, returnedInvoices.Count);
        }
    }
}