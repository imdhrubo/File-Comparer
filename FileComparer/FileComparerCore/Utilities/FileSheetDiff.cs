using System.Collections.Generic;
using FileComparerCore.Models;

namespace FileComparerCore.Utilities
{
    public class FileSheetDiff
    {
        public FileDiff FileDiff { get; set; }
        public List<SheetMatchSummary> SheetsMatchSummary { get; set; }
    }
}
