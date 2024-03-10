# README for ConsoleApp

ConsoleApp is a command-line application developed in C# for .NET Framework 4.8. Its primary functionality is to import data from a CSV file, process it, and then output the structured information to the console. The data is expected to represent a hierarchical structure of databases, tables, and columns.

## Features

- **CSV Data Import**: The application imports data from a specified CSV file, containing structured information about databases, tables, and columns.
- **Data Processing**: Utilizes advanced data processing techniques to clean, organize, and categorize the imported data for optimal display.
- **Hierarchical Data Display**: Presents the data in a hierarchical format, showcasing the relationship between databases, tables, and columns.
- **Error Handling**: Robust error handling mechanisms are in place to manage potential issues like file not found, format errors, and other runtime exceptions.
- **User Interaction**: Interactive command-line interface for specifying the CSV file path, either as a command-line argument or through user input during runtime.

## Prerequisites

- .NET Framework 4.8
- Visual Studio or compatible IDE

## Installation

1. Clone the repository to your local machine.
2. Open the solution in Visual Studio or compatible IDE.
3. Build the solution.

## Usage

Run the application from the command line with the following syntax:

```bash
ConsoleApp.exe [csv_file_path]
```
- `csv_file_path` - The path to the CSV file to be imported. If not provided, the application will prompt for it.
- Typing the `.csv` extension is optional.

## Structure

- `Program.cs` - The entry point of the application.
- `DataReader.cs` - Responsible for reading, validating, processing, and printing the data from the CSV file.
- `ImportedObject.cs` - Defines the structure of the objects imported from the CSV file.

## How It Works

- The `Program` class parses the command line arguments to obtain the file path.
- The `DataReader` class reads the data from the CSV file.
- Data is validated, cleaned, and organized into a structured format.
- The structured data is printed to the console.
- The application exits with an error message on failure or closes on success.

## Known Limitations

- The application only supports CSV files with a specific structure.
- Error handling is basic and may need enhancement for broader use cases.

## Note

This application is a simple example of a command-line application in C#. It is not intended for production use and may require further development to meet specific requirements.
