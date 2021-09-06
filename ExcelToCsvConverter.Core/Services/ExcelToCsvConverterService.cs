
using System.Diagnostics;
using System.Text;

using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;

using ExcelToCsvConverter.Core.Config;
using ExcelToCsvConverter.Core.Contracts.Services;
using ExcelToCsvConverter.Core.Exceptions;
using ExcelToCsvConverter.Core.Result;

namespace ExcelToCsvConverter.Core.Services;

public class ExcelToCsvConverterService : Base.BaseService, IExcelToCsvConverterService
{
    public async Task<ExcelWriterResult?> ConvertExcelFileToCSV(string filePath, ExcelWriterConfig? config = default)
    {
        config ??= new ExcelWriterConfig();

        try
        {
            Stopwatch stopwatch= new();
            ExcelWriterResult result = new();

            //Lets open the existing excel file and read through its content . Open the excel using openxml sdk
            using SpreadsheetDocument doc = SpreadsheetDocument.Open(filePath, false);
            //create the object for workbook part  
            WorkbookPart? workbookPart = doc.WorkbookPart;
            if (workbookPart is null)
            {
                return new();
            }

            Sheets? theSheetCollection = workbookPart.Workbook.GetFirstChild<Sheets>();
            if (theSheetCollection is null)
            {
                return new();
            }

            Sheet? theSheet = theSheetCollection.GetFirstChild<Sheet>();
            if (theSheet is null)
            {
                return new();
            }

            //statement to get the worksheet object by using the sheet id  
            Worksheet theWorksheet = ((WorksheetPart)workbookPart.GetPartById(theSheet.Id)).Worksheet;

            SheetData? theSheetData = theWorksheet.GetFirstChild<SheetData>();
            if (theSheetData is null)
            {
                return new();
            }
            
            string outputDirectory = Path.GetDirectoryName(config.OutputFile) ?? AppContext.BaseDirectory;
            string outputFileName = Path.ChangeExtension(
                Path.GetFileNameWithoutExtension(config.OutputFile ?? Path.Combine(AppContext.BaseDirectory, filePath)),
                config.OutputFileExtention);

            string fileToWriteTo = Path.Combine(outputDirectory, outputFileName);
            if (File.Exists(fileToWriteTo))
            {
                if (config.OverwiteOutputFile is false)
                {
                    throw new OutputFileAlreadyExistException(fileToWriteTo);
                }
                File.Delete(fileToWriteTo);
            }


            using TextWriter tw = new StreamWriter(fileToWriteTo, true);

            stopwatch.Start();
            foreach (Row theCurrentRow in theSheetData.Skip(config.SkipRows))
            {
                StringBuilder excelResult = new();
                foreach (Cell theCurrentCell in theCurrentRow)
                {
                    //statement to take the integer value  
                    string currentcellvalue = string.Empty;
                    if (theCurrentCell.DataType != null)
                    {
                        if (theCurrentCell.DataType == CellValues.SharedString)
                        {
                            if (int.TryParse(theCurrentCell.InnerText, out int id))
                            {
                                SharedStringItem item = workbookPart.SharedStringTablePart.SharedStringTable.Elements<SharedStringItem>().ElementAt(id);
                                if (item.Text != null)
                                {
                                    //code to take the string value  
                                    excelResult.Append(config.OptionallyEnclosedBy + item.Text.Text.Trim() + config.OptionallyEnclosedBy + config.ColumnSeperator);
                                }
                                else if (item.InnerText != null)
                                {
                                    currentcellvalue = item.InnerText;
                                }
                                else if (item.InnerXml != null)
                                {
                                    currentcellvalue = item.InnerXml;
                                }
                            }
                        }
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(theCurrentCell.InnerText))
                        {
                            excelResult.Append(' ');
                        }

                        excelResult.Append(config.OptionallyEnclosedBy + theCurrentCell.InnerText.Trim() + config.OptionallyEnclosedBy + config.ColumnSeperator);
                    }
                    result.TotalColumns++;
                }
                result.TotalRows++;
                await tw.WriteLineAsync(excelResult.ToString());
            }
            stopwatch.Stop();

            result.OutputFile = fileToWriteTo;
            result.TimeTaken = stopwatch.Elapsed;
            return result;
        }
        catch (Exception ex)
        {
            throw;
        }
    }
}
