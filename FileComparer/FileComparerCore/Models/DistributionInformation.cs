using System;
using System.Collections.Generic;
using System.Text;

namespace FileComparerCore.Models
{
    public class DistributionInformation
    {
        public List<string> ColumnNames { get; set; }
        public List<SheetDistributionInformation> SheetDistributionInformations { get; set; }
    }
}
