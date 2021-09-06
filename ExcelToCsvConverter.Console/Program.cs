// See https://aka.ms/new-console-template for more information

using ExcelToCsvConverter.Core;
using ExcelToCsvConverter.Core.Config;
using ExcelToCsvConverter.Core.Contracts.Services;

Console.WriteLine("Excel To CSV Converter Console App");


IExcelToCsvConverterService _excelToCsvConverter = Converter.GetConverter();

while (true)
{
    Console.Write("Enter the exel file path:");
    string? filePath = Console.ReadLine();
    if (filePath is not null)
    {
        Console.WriteLine();
        var result = await _excelToCsvConverter.ConvertExcelFileToCSV(filePath, new ExcelWriterConfig()
        {
            ColumnSeperator = "$",
            OptionallyEnclosedBy = "#",
            OutputFileExtention = "txt",
            SkipRows = 5,
        });
        Console.WriteLine("Done");
        Console.WriteLine($"Result: {result} \n");
    }
}