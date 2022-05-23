using System;
using System.Collections.Generic;
using System.Text;

namespace FileComparerCore.Models
{
    public class RowCount
    {
        public int CountInStandard { get; set; }
        public int CountInCompare { get; set; }

        public RowCount(int countInStandard, int countInCompare)
        {
            CountInStandard = countInStandard;
            CountInCompare = countInCompare;
        }
    }
}
