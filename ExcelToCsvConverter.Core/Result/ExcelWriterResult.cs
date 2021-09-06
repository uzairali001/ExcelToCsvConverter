using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExcelToCsvConverter.Core.Result;

public record ExcelWriterResult
{
    public int TotalRows { get; set; }
    public int TotalColumns { get; set; }
    public string OutputFile { get; set; } = null!;
    public TimeSpan TimeTaken { get; set; }
}
