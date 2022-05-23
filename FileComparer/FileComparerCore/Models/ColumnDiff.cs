using DiffPlex.DiffBuilder.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileComparerCore.Models
{
    public class ColumnDiff
    {
        public string SheetName { get; set; }
        public SideBySideDiffModel ColumnDifference { get; set; }
    }
}
