using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExcelToCsvConverter.Core.Config
{
    public class ExcelWriterConfig
    {
        public string ColumnSeperator { get; set; } = ",";
        public string? OptionallyEnclosedBy { get; set; }
        public int SkipRows { get; set; }

        public bool OverwiteOutputFile { get; set; } = true;

        public string OutputFile { get; set; } = null!;
        public string OutputFileExtention { get; set; } = "csv";
    }
}
