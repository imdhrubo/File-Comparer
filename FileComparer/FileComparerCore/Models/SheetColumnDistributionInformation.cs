using System;
using System.Collections.Generic;
using System.Text;

namespace FileComparerCore.Models
{
    public class SheetColumnDistributionInformation
    {
        public string ColumnName;
        public SortedList<string, RowCount> RowInformations { get; set; }
    }
}
