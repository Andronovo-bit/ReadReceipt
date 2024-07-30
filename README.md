
# ReceiptReaderApp

ReceiptReaderApp is an ASP.NET Core application designed to process receipt JSON files or content uploaded by users. The application parses the JSON content, extracts relevant items, groups them by coordinates, and returns a formatted receipt as JSON.

## Features

- Upload JSON files or content containing receipt data.
- Validate uploaded files to ensure they are not empty and are in JSON format.
- Parse and process receipt data, extracting relevant information.
- Group items by coordinates and generate a formatted receipt.
- Handle exceptions and log errors for better debugging and monitoring.

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

