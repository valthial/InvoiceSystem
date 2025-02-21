using System.Net;
using Microsoft.AspNetCore.Mvc;

namespace InvoiceSystem.Api.Controllers;

[ApiController]
[Route("api/invoice")]
public class InvoiceController : ControllerBase
{
    [HttpPost("CreateInvoice")]
    public async Task<IActionResult> CreateInvoice([FromBody] HttpWebRequest request)
    {
        //TODO
        return Ok();
    }
}