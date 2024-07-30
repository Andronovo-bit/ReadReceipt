namespace ReadReceipt.Services
{
    public class ReturnReceipt
    {
        public GroupedItem[] Items { get; set; }
    }

    public class GroupedItem
    {
        public int Line { get; set; }
        public string Text { get; set; }
    }

    public class ReceiptItem
    {
        public string Description { get; set; }
        public double AvgX { get; set; }
        public double AvgY { get; set; }
    }
}