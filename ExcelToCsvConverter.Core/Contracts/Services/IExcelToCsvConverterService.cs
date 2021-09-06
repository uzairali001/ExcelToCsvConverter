
using ExcelToCsvConverter.Core.Config;
using ExcelToCsvConverter.Core.Result;

namespace ExcelToCsvConverter.Core.Contracts.Services;

public interface IExcelToCsvConverterService
{
    Task<ExcelWriterResult> ConvertExcelFileToCSV(string sourceFilePath, ExcelWriterConfig? config = default);
}
