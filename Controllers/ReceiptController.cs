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
    public async Task<IActionResult> UploadReceipt(IFormFile file)
    {
        if (file == null || file.Length == 0)
        {
            return BadRequest("File is empty or not provided.");
        }
        if (file.ContentType != "application/json")
        {
            return BadRequest("Invalid file type. Please provide a JSON file.");
        }

        string jsonContent;
        using (var streamReader = new StreamReader(file.OpenReadStream()))
        {
            jsonContent = await streamReader.ReadToEndAsync();
        }

        var receipt = _receiptReaderService.ProcessReceiptFromJson(jsonContent);

        return Ok(receipt);
    }
}
