using System.Collections.Generic;

namespace FileComparerCore.Utilities
{
    public class Summary
    {
        public List<SheetMatchSummary> SheetsMatchSummary { get; set; }
        public List<RowMatchSummary> RowsMatchSummary { get; set; }
        public List<DataMatchSummary> DatasMatchSummary { get; set; }
        public FileMatchSummary FileMatchSummary { get; set; }
    }
}
