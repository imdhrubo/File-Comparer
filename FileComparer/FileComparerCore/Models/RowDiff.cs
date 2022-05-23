using System;
using System.Collections.Generic;

namespace FileComparerCore.Models
{
    public class RowDiff
    {
        public string SheetName { get; set; }
        public List<string> ColumnNames { get; set; }
        public List<string> MissingRows { get; set; }
        public List<string> SurplusRows { get; set; }
        public List<string> CommonRows { get; set; }
        public List<Tuple<string, int>> DuplicateRows { get; set; }
    }
}
