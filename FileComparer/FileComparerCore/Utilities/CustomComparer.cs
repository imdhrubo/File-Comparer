using System;
using System.Collections.Generic;

namespace FileComparerCore.Utilities
{
    public class CustomComparer : IEqualityComparer<Tuple<string, int>>
    {
        public bool Equals(Tuple<string, int> x, Tuple<string, int> y)
        {
            return x.Item1 == y.Item1;
        }

        public int GetHashCode(Tuple<string, int> obj)
        {
            return 0;
        }
    }
}
