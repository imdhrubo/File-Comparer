using System;
using System.Collections.Generic;
using FileComparerCore.Models;
using System.Linq;

namespace FileComparerCore.Utilities
{
    public class CustomColumnNameComparer : IComparer<Tuple<string, string>>
    {
        private List<ColumnGroup> suggestion;

        public CustomColumnNameComparer(List<ColumnGroup> suggestion)
        {
            this.suggestion = suggestion;
        }

        public int Compare(Tuple<string, string> lhs, Tuple<string, string> rhs)
        {
            // first sorts by sheet name. after that if the sheet names are equal, sorts by their sequence in the input sheets

            if(lhs.Item1.CompareTo(rhs.Item1) == -1)
            {
                return -1;
            }
            else if (lhs.Item1.CompareTo(rhs.Item1) == 1)
            {
                return 1;
            }
            else
            {
                var columns = suggestion.FirstOrDefault(x => x.Sheet == lhs.Item1).Columns;
                if (columns.IndexOf(lhs.Item2) < columns.IndexOf(rhs.Item2))
                {
                    return -1;
                }
                else if (columns.IndexOf(lhs.Item2) > columns.IndexOf(rhs.Item2))
                {
                    return 1;
                }
                return 0;
            }
        }
    }
}
