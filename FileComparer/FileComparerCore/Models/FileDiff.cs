using DiffPlex.DiffBuilder.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileComparerCore.Models
{
    public class FileDiff
    {
        public SideBySideDiffModel SheetDiff { get; set; }
        public List<ColumnDiff> ColumnDiffs { get; set; }
    }
}
