using System.Collections.Generic;

namespace FileComparerCore.Utilities
{
    public class FileMatchSummary
    {
        public string ExcelCompareUtility { get; set; }
        public FileMetaData StandardSheet { get; set; }
        public FileMetaData CompareSheet { get; set; }
        public List<SheetInfoSummary> StandardSheetInfoSummary { get; set; }
        public List<SheetInfoSummary> CompareSheetInfoSummary { get; set; }
        public string AreSheetsAllMatch { get; set; }
    }
}
