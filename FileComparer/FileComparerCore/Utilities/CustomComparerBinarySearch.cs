using System;
using System.Collections.Generic;

namespace FileComparerCore.Utilities
{
    public class CustomComparerBinarySearch : IComparer<Tuple<string, int>>
    {
        public int Compare(Tuple<string, int> x, Tuple<string, int> y)
        {
            return x.Item2.CompareTo(y.Item2);
        }
    }
}
