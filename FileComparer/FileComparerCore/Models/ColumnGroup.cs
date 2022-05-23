using System.Collections.Generic;

namespace FileComparerCore.Models
{
    public class ColumnGroup
    {
        public string Sheet { get; set; }
        public List<string> Columns { get; set; }
    }
}
