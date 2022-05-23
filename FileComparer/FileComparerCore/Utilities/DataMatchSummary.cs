namespace FileComparerCore.Utilities
{
    public class DataMatchSummary
    {
        public string SheetName { get; set; }
        public int MatchCount { get; set; }
        public int NoMatchCount { get; set; }
        public string SelectedColumns { get; set; }
    }
}
