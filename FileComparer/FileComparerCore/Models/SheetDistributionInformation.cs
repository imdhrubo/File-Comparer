using System;
using System.Collections.Generic;
using System.Text;

namespace FileComparerCore.Models
{
    public class SheetDistributionInformation
    {
        public string SheetName { get; set; }
        public List<SheetColumnDistributionInformation> SheetColumnDistributionInformations { get; set; }
    }
}
