using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ReadReceipt.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

public class ReceiptReaderService : IReceiptReaderService
{
    private readonly ILogger<ReceiptReaderService> _logger;
    private const double Tolerance = 15;

    public ReceiptReaderService(ILogger<ReceiptReaderService> logger)
    {
        _logger = logger;
    }

    public string ProcessReceiptFromJson(string json)
    {
        try
        {
            _logger.LogInformation("Processing receipt from JSON content");

            var items = ParseJson(json);

            var extractedItems = ExtractRelevantItems(items);

            var groupedItems = GroupItemsByCoordinates(extractedItems);

            var receipt = CreateReceiptFromGroupedItems(groupedItems);

            var jsonGroupedItems = JsonConvert.SerializeObject(receipt, Formatting.Indented);

            return jsonGroupedItems;
        }
        catch (JsonException jsonEx)
        {
            _logger.LogError(jsonEx, "Error parsing JSON");
            throw new InvalidOperationException("Invalid JSON format", jsonEx);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while processing the receipt");
            throw;
        }
        finally
        {
            _logger.LogInformation("Receipt processing completed");
        }
    }

    private JArray ParseJson(string json)
    {
        return JArray.Parse(json);
    }

    private List<ReceiptItem> ExtractRelevantItems(JArray items)
    {
        return items
            .Where(t => t["locale"] == null)
            .Select(item => new ReceiptItem
            {
                Description = item["description"].ToString(),
                AvgX = item["boundingPoly"]["vertices"].Average(v => (int)v["x"]),
                AvgY = item["boundingPoly"]["vertices"].Average(v => (int)v["y"])
            })
            .ToList();
    }

    private Dictionary<double, List<ReceiptItem>> GroupItemsByCoordinates(List<ReceiptItem> extractedItems)
    {
        var groupedItemsDictionary = new Dictionary<double, List<ReceiptItem>>();

        foreach (var item in extractedItems)
        {
            var key = groupedItemsDictionary.Keys.FirstOrDefault(k => Math.Abs(item.AvgY - k) <= Tolerance);
            if (key != 0)
            {
                groupedItemsDictionary[key].Add(item);
            }
            else
            {
                groupedItemsDictionary[item.AvgY] = new List<ReceiptItem> { item };
            }
        }

        return groupedItemsDictionary;
    }

    private ReturnReceipt CreateReceiptFromGroupedItems(Dictionary<double, List<ReceiptItem>> groupedItems)
    {
        return new ReturnReceipt
        {
            Items = groupedItems
                .Select((g, i) =>
                {
                    var sortedItems = g.Value.OrderBy(item => item.AvgX).ToList();
                    return new GroupedItem
                    {
                        Line = i + 1,
                        Text = string.Join(" ", sortedItems.Select(item => item.Description))
                    };
                })
                .ToArray()
        };
    }
}
