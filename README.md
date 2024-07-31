
# ReceiptReaderApp

ReceiptReaderApp is an ASP.NET Core application designed to process receipt JSON files or content uploaded by users. The application parses the JSON content, extracts relevant items, groups them by coordinates, and returns a formatted receipt as JSON.

 ### You can try this
 - Click this link -> https://readreceiptapp.azurewebsites.net/swagger (maybe open slowly you should wait a little first open)
 - Use [example.json](https://github.com/Andronovo-bit/ReadReceipt/blob/master/example.json) file or content in project files.

## Features

- Upload JSON files or content containing receipt data.
- Validate uploaded files to ensure they are not empty and are in JSON format.
- Parse and process receipt data, extracting relevant information.
- Group items by coordinates and generate a formatted receipt.
- Handle exceptions and log errors for better debugging and monitoring.

## Key Features:

1. **Logging and Error Handling**: 
    - Uses `ILogger` to log informational messages, errors, and the processing steps.
    - Handles JSON parsing errors and general exceptions gracefully, providing informative error messages.

2. **Processing Method**:
    - `ProcessReceiptFromJson(string json)`: Main method that orchestrates the receipt processing, from JSON parsing to returning a structured JSON output of the receipt items.

3. **Core Functional Steps**:
    - **Parse JSON**:
        - `ParseJson(string json)`: Parses the input JSON string into a `JArray`.
    - **Extract Relevant Items**:
        - `ExtractRelevantItems(JArray items)`: Filters and extracts necessary information (description and coordinates) from each item in the JSON array.
    - **Calculate Threshold**:
        - `CalculateThreshold(JArray items)`: Calculates a dynamic threshold based on the average height of bounding polygons in the receipt items, used for grouping items.
    - **Group Items by Coordinates**:
        - `GroupItemsByCoordinates(List<ReceiptItem> extractedItems)`: Groups items by their Y-coordinates based on the calculated threshold, ensuring items on the same line are grouped together.
    - **Create Receipt from Grouped Items**:
        - `CreateReceiptFromGroupedItems(Dictionary<double, List<ReceiptItem>> groupedItems)`: Converts the grouped items into a structured receipt format suitable for JSON serialization.

## Getting Started

### Prerequisites

- [.NET 8.0 SDK or later](https://dotnet.microsoft.com/download/dotnet/8.0)
- Visual Studio 2022 or Visual Studio Code

### Installing

1. Clone the repository:
   ```bash
   git clone https://github.com/Andronovo-bit/ReadReceipt.git
   ```
2. Navigate to the project directory:
   ```bash
   cd ReadReceipt
   ```
3. Restore the dependencies:
   ```bash
   dotnet restore
   ```

### Running the Application

1. Build the application:
   ```bash
   dotnet build
   ```
2. Run the application:
   ```bash
   dotnet run
   ```
3. Open your browser and navigate to `https://localhost:5284/swagger` to explorer the endpoints.

## Usage

### Uploading a Receipt JSON File

- Use a tool like Postman or cURL to send a POST request to `https://localhost:5284/api/receipt/upload-jsonfile` with a JSON file.

- Use a tool like Postman or cURL to send a POST request to `https://localhost:5284/api/receipt/upload-jsoncontent` with a JSON content
.

#### Example using cURL

```bash
curl -X POST "https://localhost:5284/api/receipt/upload-jsonfile" -H "accept: */*" -H "Content-Type: multipart/form-data" -F "file=@path/to/your/receipt.json"
```

### Example JSON Input

```json
[
    {
        "description": "Item 1",
        "boundingPoly": {
            "vertices": [
                { "x": 10, "y": 20 },
                { "x": 20, "y": 20 },
                { "x": 20, "y": 30 },
                { "x": 10, "y": 30 }
            ]
        }
    },
    {
        "description": "Item 2",
        "boundingPoly": {
            "vertices": [
                { "x": 15, "y": 25 },
                { "x": 25, "y": 25 },
                { "x": 25, "y": 35 },
                { "x": 15, "y": 35 }
            ]
        }
    }
]
```

### Example JSON Output

```json
{
    "items": [
        {
            "line": 1,
            "text": "Item 1 Item 2"
        }
    ]
}
```
## Azure Web App Setup and GitHub Secrets

### Creating an Azure Web App

1. Sign in to the [Azure Portal](https://portal.azure.com/).
2. From the Azure portal menu, select "Create a resource".
3. In the "Search the Marketplace" field, type 'Web App' and press enter.
4. Select "Web App" from the results, then click "Create".
5. Fill in the details for your web app, such as Subscription, Resource Group, Name, Publish (Code), and Runtime Stack (.NET Core).
6. Click "Review + create" and then "Create" after verifying your details.

### Configuring GitHub Secrets for Azure Deployment

1. Navigate to your GitHub repository.
2. Go to "Settings" > "Secrets" > "Actions".
3. Click on "New repository secret".
4. Add the following secrets required for Azure deployment:
   - `AZURE_PUBLISH_PROFILE`: The publish profile XML content. (You can download this from your Azure Web App's "Deployment Center".)
5. Use these secrets in your `publish.yml` GitHub Action workflow to deploy your application.

## Contributing

    1. Fork the repository.
    2. Create your feature branch (`git checkout -b feature/AmazingFeature`).
    3. Commit your changes (`git commit -m 'Add some amazing feature'`).
    4. Push to the branch (`git push origin feature/AmazingFeature`).
    5. Open a Pull Request.

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## Acknowledgments

- [ASP.NET Core Documentation](https://docs.microsoft.com/en-us/aspnet/core/?view=aspnetcore-6.0)
- [Newtonsoft.Json Documentation](https://www.newtonsoft.com/json/help/html/Introduction.htm)


### Explanation

- **Project Overview**: Describes the purpose and functionality of the application.
- **Project Structure**: Provides a visual representation of the project's directory structure.
- **Getting Started**: Instructions on how to set up and run the application locally.
- **Usage**: Guides on how to upload a JSON file and an example of input and output JSON.
- **Contributing**: Steps for contributing to the project.
- **License**: Specifies the project's license.
- **Acknowledgments**: References useful documentation.

