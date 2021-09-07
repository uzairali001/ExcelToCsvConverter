using ExcelToCsvConverter.Core;
using ExcelToCsvConverter.Core.Config;
using ExcelToCsvConverter.Core.Contracts.Services;
using ExcelToCsvConverter.Core.Result;

namespace ExcelToCsvConverter.ConsoleApp;

public class Program
{
    public static async Task Main(string[] args)
    {
        Console.WriteLine("Excel To CSV Converter Console App");

        IExcelToCsvConverterService _excelToCsvConverter = Converter.GetConverter();

        string? filePath = AskForInput("Enter the exel file path:");
        if (string.IsNullOrWhiteSpace(filePath))
        {
            Console.WriteLine("File path is requied and it can't be empty");
            return;
        }

        string? columnSeperator = AskForInput("Set Column Seperator (,):");
        string? enclosedBy = AskForInput("Set Optinally Enclosed By ():");
        string? outfileExt = AskForInput("Set Output file Extension (csv):");
        if (int.TryParse(AskForInput("Set Skip rows (0):"), out int skipRows) is false)
        {
            skipRows = 0;
        }

        Console.WriteLine();
        ExcelWriterResult? result = await _excelToCsvConverter.ConvertExcelFileToCSV(filePath, new ExcelWriterConfig()
        {
            ColumnSeperator = columnSeperator ?? ", ",
            OptionallyEnclosedBy = enclosedBy,
            OutputFileExtention = outfileExt ?? "csv",
            SkipRows = skipRows,
        });
        Console.WriteLine("Done");
        Console.WriteLine($"Result: {result} \n");
        
    }

    private static string? AskForInput(string title)
    {
        Console.Write(title);
        return Console.ReadLine();
    }
}