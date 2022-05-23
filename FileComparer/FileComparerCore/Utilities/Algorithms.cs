using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileComparerCore.Utilities
{
    public static class Algorithms
    {
        public static int BinarySearchFirstIndex(this List<string> list, string item)
        {
            return BinarySearchFirstIndex(list, 0, list.Count - 1, item, list.Count);
        }

        public static int BinarySearchLastIndex(this List<string> list, string item)
        {
            return BinarySearchLastIndex(list, 0, list.Count - 1, item, list.Count);
        }

        private static int BinarySearchFirstIndex(List<string> list, int low, int high, string item, int n)
        {
            if (high >= low)
            {
                int mid = low + (high - low) / 2;

                if ((mid == 0 || item.CompareTo(list[mid - 1]) > 0) && list[mid] == item)
                    return mid;
                else if (item.CompareTo(list[mid]) > 0)
                    return BinarySearchFirstIndex(list, mid + 1, high, item, n);
                else
                    return BinarySearchFirstIndex(list, low, mid - 1, item, n);
            }
            return -1;
        }

        private static int BinarySearchLastIndex(List<string> list, int low, int high, string item, int n)
        {
            if (high >= low)
            {
                int mid = low + (high - low) / 2;
                if ((mid == n - 1 || item.CompareTo(list[mid + 1]) < 0) && list[mid] == item)
                    return mid;
                else if (item.CompareTo(list[mid]) < 0)
                    return BinarySearchLastIndex(list, low, mid - 1, item, n);
                else
                    return BinarySearchLastIndex(list, mid + 1, high, item, n);
            }
            return -1;
        }
    }
}
