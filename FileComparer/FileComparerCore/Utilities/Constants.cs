namespace FileComparerCore.Utilities
{
    public static class Constants
    {
        public static readonly string SummaryResultTabName = "Summary";
        public static readonly string ReportColumnResultTabName = "ColumnDifference";
        public static readonly string ReportColumnTabName = "Columns";
        public static readonly string ReportRowResultTabName = "RowDifference";
        public static readonly string ReportGroupResultTabName = "GroupInformation";
        public static readonly string ReportDistributionResultTabName = "DistributionInformation";
        public static readonly string DefaultReportFolderPath = "C:\\temp";
        public static readonly string Missing = "Missing";
        public static readonly string Surplus = "Surplus";
        public static readonly string Match = "Match";
        public static readonly string Duplicate = "Duplicate";
        public static readonly string NoMatch = "No Match";
        public static readonly string ConfigJsonFileDefaultName = "config.json";
        public static readonly string DateTimeFormat = "yyyy-MM-dd hh:mm:ss tt";
        public static readonly string BackgroundColorDeleted = "#FFC864";
        public static readonly string BackgroundColorImaginary = "#C8C8C8";
        public static readonly string BackgroundColorInserted = "#FFFF00";
        public static readonly string BackgroundColorModified = "#DCDCFF";
        public static readonly string BackgroundColorUnchanged = "#FFFFFF";
        public static readonly string InvalidFileMessage = "One or more file/folder path is invalid";
        public static readonly string SuccessMessage = "Files compared and results are exported";
        public static readonly string ErrorMessage = "Error occured while comparing files";
        public static readonly string SelectionInvalidMessage = "You must select atleast one configuration";
        public static readonly string UniqueKeyInvalidMessage = "You must select atleast one unqiue key in Row Settings tab";
        public static readonly string GroupColumnsInvalidMessage = "You must select atleast one column in Group Settings tab";
        public static readonly string DistributionColumnsInvalidMessage = "You must select atleast one column in Distribution Settings tab";
        public static readonly string UniqueKeyAndSelectedColumnInvalidMessage = "You must select atleast one unqiue key & one column from matching sheet in Row Settings & Column Settings tab";
        public static readonly string UniqueKeyAndSelectedColumnNotUniqueMessage = "Unique Key and Selected Columns cannot have common element";
        public static readonly string Parser = "|*?!";
        public static readonly string ExcelExtension = ".xlsx";
        public static readonly string BlankCellValue = "(blank)";
        public static readonly string FileExplorerProcessName = "explorer.exe";
        public static readonly string StandardValue = "StandardValue";
        public static readonly string ComparerValue = "ComparerValue";
        public static readonly string Status = "Status";
        public static readonly string ColumnMatchStatus = "Yes";
        public static readonly string ColumnNoMatchStatus = "No";
        public static readonly string ColumnMatchNotAvailableStatus = "NA";
        public static readonly string StandardFile = "Standard File";
        public static readonly string CompareFile = "Compare File";
        public static readonly string Difference = "Difference";
        public static readonly string Total = "Total";
        public static readonly string ColumnName = "Column Name";
        public static readonly string Value = "Value";
    }
}
