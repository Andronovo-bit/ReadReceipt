using Microsoft.AspNetCore.Mvc;
using ReadReceipt.Services;

[ApiController]
[Route("api/[controller]")]
public class ReceiptController : ControllerBase
{
    private readonly IReceiptReaderService _receiptReaderService;
    private readonly ILogger<ReceiptController> _logger;

    public ReceiptController(IReceiptReaderService receiptReaderService, ILogger<ReceiptController> logger)
    {
        _receiptReaderService = receiptReaderService;
        _logger = logger;
    }

    [HttpPost("upload")]
    [ValidateJsonFile]
    public async Task<IActionResult> UploadReceipt(IFormFile file)
    {

        string jsonContent;
        using (var streamReader = new StreamReader(file.OpenReadStream()))
        {
            jsonContent = await streamReader.ReadToEndAsync();
        }

        var receipt = _receiptReaderService.ProcessReceiptFromJson(jsonContent);

        return Ok(receipt);
    }
}
