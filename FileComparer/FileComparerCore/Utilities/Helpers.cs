using System;
using System.Collections.Generic;
using System.Linq;
using OfficeOpenXml;

namespace FileComparerCore.Utilities
{
    public static class Helpers
    {
        public static List<string> GetRowsAsSingleLine(ExcelWorksheet excelWorksheet, IEnumerable<string> columns)
        {
            var columnsList = columns.ToList();
            var columnIndices = GetUniqueKeyIndexes(excelWorksheet, columnsList);
            var columnCount = excelWorksheet.Dimension.End.Column;
            var start = excelWorksheet.Dimension.Start;
            var end = excelWorksheet.Dimension.End;
            List<string> rows = new List<string>();

            for (int row = (start.Row) + 1; row <= end.Row; row++)
            {
                string uniqueKeyValue = "";
                foreach (var columnIndex in columnIndices)
                {
                    var content = excelWorksheet.Cells[row, columnIndex]?.Text;
                    content = string.IsNullOrWhiteSpace(content) ? Constants.BlankCellValue : content;
                    uniqueKeyValue += content + Constants.Parser;
                }
                rows.Add(uniqueKeyValue);
            }
            return rows;
        }

        public static List<Tuple<string, int>> GetRowsAsSingleLineWithIndex(ExcelWorksheet excelWorksheet, List<string> columns)
        {
            int index = 0;
            List<Tuple<string, int>> rowsWithIndex = new List<Tuple<string, int>>();
            var rows = GetRowsAsSingleLine(excelWorksheet, columns);
            rows.Sort();
            foreach (var row in rows)
            {
                index++;
                rowsWithIndex.Add(new Tuple<string, int>(row, index));
            }
            return rowsWithIndex;
        }

        private static List<int> GetUniqueKeyIndexes(ExcelWorksheet excelWorksheet, List<string> uniqueKeys)
        {
            var columnCount = excelWorksheet.Dimension.End.Column;
            var uniqueColTuples = new List<Tuple<string, int>>();

            for (int i = 1; i <= columnCount; i++)
            {
                var cellValue = excelWorksheet.Cells[1, i].Value?.ToString();
                if (uniqueKeys.Contains(cellValue, StringComparer.InvariantCultureIgnoreCase))
                {
                    uniqueColTuples.Add(new Tuple<string, int>(cellValue, i));
                }
            }
            uniqueColTuples.Sort(new CustomTupleComparer(uniqueKeys));
            return uniqueColTuples.Select(x => x.Item2).ToList();
        }
    }
}
