using System.Collections.Generic;

namespace FileComparerCore.Models
{
    public class SelectedColumnDiff
    {
        public string TabName { get; set; }
        public string SheetName { get; set; }
        public int UniqueKeyEndIndex { get; set; }
        public int SelectedColumnIndex { get; set; }
        public List<string> ColumnNames { get; set; }
        public List<string> CommonRowsStandard { get; set; }
        public List<string> CommonRowsComparer { get; set; }
        public List<string> SelectedColumns { get; set; }
        public List<string> UniqueKeys { get; set; }
    }
}
