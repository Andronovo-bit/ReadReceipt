namespace ReadReceipt.Services
{
    public interface IReceiptReaderService
    {
        string ProcessReceiptFromJson(string json);
    }
}
