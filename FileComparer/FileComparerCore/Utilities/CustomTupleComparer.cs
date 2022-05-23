using System;
using System.Collections.Generic;

namespace FileComparerCore.Utilities
{
    public class CustomTupleComparer : IComparer<Tuple<string, int>>
    {
        private List<string> suggestion;

        public CustomTupleComparer(List<string> suggestion)
        {
            this.suggestion = suggestion;
        }

        public int Compare(Tuple<string, int> x, Tuple<string, int> y)
        {
            if(suggestion.IndexOf(x.Item1) < suggestion.IndexOf(y.Item1))
            {
                return -1;
            } 
            else if(suggestion.IndexOf(x.Item1) > suggestion.IndexOf(y.Item1))
            {
                return 1;
            }
            return 0;
        }
    }
}
