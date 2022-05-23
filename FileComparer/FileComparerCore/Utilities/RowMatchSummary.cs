namespace FileComparerCore.Utilities
{
    public class RowMatchSummary
    {
        public string SheetName { get; set; }
        public int MatchCount { get; set; }
        public int MissingCount { get; set; }
        public int SurplusCount { get; set; }
        public int DuplicateCount { get; set; }
    }
}
