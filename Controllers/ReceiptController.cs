using Microsoft.AspNetCore.Mvc;
using ReadReceipt.Attributes;
using ReadReceipt.Services;
using System.Text.Json;

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

    [HttpPost("upload-jsonfile")]
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

    [HttpPost("upload-jsoncontent")]
    [ValidateJsonContent]
    public IActionResult UploadReceiptJsonContent([FromBody] JsonElement jsonContent)
    {
        var stringContent = jsonContent.GetRawText();

        var receipt = _receiptReaderService.ProcessReceiptFromJson(stringContent);

        return Ok(receipt);
    }

}
