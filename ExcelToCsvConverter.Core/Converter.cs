using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ExcelToCsvConverter.Core.Contracts.Services;
using ExcelToCsvConverter.Core.Services;

namespace ExcelToCsvConverter.Core;

public class Converter
{
    public static IExcelToCsvConverterService GetConverter()
    {
        return new ExcelToCsvConverterService();
    }
}
