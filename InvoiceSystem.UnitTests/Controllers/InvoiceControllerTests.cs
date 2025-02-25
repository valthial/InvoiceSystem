// using System.Security.Claims;
// using InvoiceSystem.Api.Controllers;
// using InvoiceSystem.Application.Services;
// using InvoiceSystem.Domain.Entities;
// using InvoiceSystem.Domain.Interfaces;
// using Microsoft.AspNetCore.Http;
// using Microsoft.AspNetCore.Mvc;
// using Microsoft.Extensions.Logging;
// using Moq;
// using Xunit;
//
// namespace InvoiceSystem.UnitTests.Controllers;
//
// public class InvoiceControllerTests
// {
//     private readonly Mock<IInvoiceRepository> _mockInvoiceRepository;
//     private readonly Mock<ILogger<InvoiceService>> _mockLogger;
//     private readonly InvoiceService _invoiceService;
//     private readonly InvoiceController _invoiceController;
//
//     public InvoiceControllerTests()
//     {
//         _mockInvoiceRepository = new Mock<IInvoiceRepository>();
//         _mockLogger = new Mock<ILogger<InvoiceService>>();
//         _invoiceService = new InvoiceService(_mockInvoiceRepository.Object, _mockLogger.Object);
//         _invoiceController = new InvoiceController(_invoiceService);
//
//         var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
//         {
//             new Claim("IssuerCompanyId", "issuer-company-id")
//         }));
//         _invoiceController.ControllerContext = new ControllerContext
//         {
//             HttpContext = new DefaultHttpContext { User = user }
//         };
//     }
//
//     [Fact]
//     public async Task CreateInvoice_ReturnsOkResult_WhenInvoiceIsValid()
//     {
//         var invoice = Invoice.Create(
//             DateTimeOffset.UtcNow,
//             "issuer-company-id",
//             "counterparty-company-id",
//             100,
//             20,
//             120,
//             "Test Invoice");
//
//         var generatedId = Guid.NewGuid().ToString();
//         _mockInvoiceRepository
//             .Setup(repo => repo.CreateInvoiceAsync(It.IsAny<Invoice>()))
//             .Callback<Invoice>(i =>
//             {
//                 var property = typeof(Invoice).GetProperty("Id");
//                 property?.SetValue(i, generatedId);
//             })
//             .Returns(Task.CompletedTask);
//
//         var result = await _invoiceController.CreateInvoice(invoice);
//
//         var okResult = Assert.IsType<OkObjectResult>(result);
//         var returnedInvoice = Assert.IsType<Invoice>(okResult.Value);
//         Assert.Equal(generatedId, returnedInvoice.Id);
//     }
//     
//     [Fact]
//     public async Task GetSentInvoices_ReturnsOkResult_WithListOfInvoices()
//     {
//         var companyId = "issuer-company-id";
//         var invoices = new List<Invoice>
//         {
//             Invoice.Create(
//                 DateTimeOffset.UtcNow,
//                 companyId,
//                 "counterparty-company-id",
//                 100,
//                 20,
//                 120,
//                 "Invoice 1"),
//             Invoice.Create(
//                 DateTimeOffset.UtcNow,
//                 companyId,
//                 "counterparty-company-id",
//                 200,
//                 40,
//                 240,
//                 "Invoice 2")
//         };
//
//         _mockInvoiceRepository
//             .Setup(repo => repo.GetSentInvoicesAsync(companyId, null, null, null))
//             .ReturnsAsync(invoices);
//
//         var result = await _invoiceController.GetSentInvoices(null, null, null);
//
//         var okResult = Assert.IsType<OkObjectResult>(result);
//         Assert.Equal(invoices, okResult.Value);
//     }
//
//     [Fact]
//     public async Task GetSentInvoices_ReturnsUnauthorized_WhenIssuerCompanyIdIsMissing()
//     {
//         var user = new ClaimsPrincipal(new ClaimsIdentity()); // No claims
//         _invoiceController.ControllerContext = new ControllerContext
//         {
//             HttpContext = new DefaultHttpContext { User = user }
//         };
//
//         var result = await _invoiceController.GetSentInvoices(null, null, null);
//
//         Assert.IsType<UnauthorizedObjectResult>(result);
//     }
//
//     [Fact]
//     public async Task GetReceivedInvoices_ReturnsUnauthorized_WhenIssuerCompanyIdIsMissing()
//     {
//         var user = new ClaimsPrincipal(new ClaimsIdentity());
//         _invoiceController.ControllerContext = new ControllerContext
//         {
//             HttpContext = new DefaultHttpContext { User = user }
//         };
//
//         var result = await _invoiceController.GetReceivedInvoices(null, null, null);
//
//         Assert.IsType<UnauthorizedObjectResult>(result);
//     }
// }