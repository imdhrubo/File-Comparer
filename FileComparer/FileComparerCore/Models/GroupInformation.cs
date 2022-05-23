using System;
using System.Collections.Generic;
using System.Text;

namespace FileComparerCore.Models
{
    public class GroupInformation
    {
        public string SheetName { get; set; }
        public List<string> ColumnNames { get; set; }
        public SortedList<string, RowCount> GroupRowInformations { get; set; }
    }
}
