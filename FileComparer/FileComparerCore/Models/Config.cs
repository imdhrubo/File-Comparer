using System;
using System.Collections.Generic;

namespace FileComparerCore.Models
{
    public class Config
    {
        public bool CheckColumns { get; set; }
        public bool CheckRows { get; set; }
        public bool CheckOnSelectedColumns { get; set; }
        public bool CheckGroups { get; set; }
        public bool CheckDistribution { get; set; }
        public List<ColumnGroup> UniqueKeys { get; set; }
        public List<ColumnGroup> SelectedColumns { get; set; }
        public List<ColumnGroup> SelectedGroups { get; set; }
        public List<ColumnGroup> SelectedDistributions { get; set; }
        public string StandardFilePath { get; set; }
        public string CompareFilePath { get; set; }
        public string ReportFilePath { get; set; }
    }
}
