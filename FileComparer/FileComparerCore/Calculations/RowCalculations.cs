using System;
using System.Collections.Generic;
using System.Linq;
using FileComparerCore.Models;
using FileComparerCore.Utilities;
using OfficeOpenXml;

namespace FileComparerCore.Calculations
{
    public class RowCalculations
    {
        public List<RowMatchSummary> CheckRowsAndGenerateExcel(ExcelPackage standardFilePackage, ExcelPackage compareFilePackage, ExcelPackage reportFilePath, List<ColumnGroup> uniqueKeys)
        {
            var fileRowDiff = CheckRows(standardFilePackage, compareFilePackage, uniqueKeys);
            List<RowMatchSummary> result = new ExcelHelper().WriteToExcel(reportFilePath, fileRowDiff);
            return result;
        }

        public List<DataMatchSummary> CheckRowsOnSelectedColumnsAndGenerateExcel(ExcelPackage standardFilePackage, ExcelPackage compareFilePackage, ExcelPackage reportFilePath, List<ColumnGroup> uniqueKeys, List<ColumnGroup> selectedColumns)
        {
            var fileSelectedColumnDiff = CheckRowsOnSelectedColumn(standardFilePackage, compareFilePackage, uniqueKeys, selectedColumns);
            List<DataMatchSummary> result = new ExcelHelper().WriteToExcel(reportFilePath, fileSelectedColumnDiff);
            return result;
        }

        private FileRowDiff CheckRows(ExcelPackage standardFilePackage, ExcelPackage compareFilePackage, List<ColumnGroup> uniqueKeys)
        {
            Logger.Log("CheckRows() start");
            var fileRowDiff = new FileRowDiff()
            {
                RowDiffs = new List<RowDiff>()
            };
            var standardFileSheetNames = standardFilePackage.Workbook.Worksheets.Select(x => x.Name).ToList();
            var compareFileSheetNames = compareFilePackage.Workbook.Worksheets.Select(x => x.Name).ToList();
            var commonSheetNames = standardFileSheetNames.Intersect(compareFileSheetNames).ToList();
            foreach (var sheet in commonSheetNames)
            {
                var uniqueKey = uniqueKeys.FirstOrDefault(x => x.Sheet == sheet);
                if (!string.IsNullOrEmpty(uniqueKey?.Sheet))
                {
                    List<string> uniqueColumns = new List<string>();
                    var rowDiff = new RowDiff();
                    rowDiff.SheetName = sheet;
                    var standardWorksheet = standardFilePackage.Workbook.Worksheets[sheet];
                    var comparatorWorksheet = compareFilePackage.Workbook.Worksheets[sheet];
                    uniqueColumns.AddRange(uniqueKey?.Columns);
                    var standardRows = Helpers.GetRowsAsSingleLine(standardWorksheet, uniqueColumns);
                    var compareRows = Helpers.GetRowsAsSingleLine(comparatorWorksheet, uniqueColumns);
                    rowDiff.ColumnNames = GetColumnNames(uniqueColumns);
                    rowDiff.MissingRows = standardRows.Except(compareRows)?.Distinct().ToList();
                    rowDiff.SurplusRows = compareRows.Except(standardRows)?.Distinct().ToList();
                    rowDiff.DuplicateRows = GetDuplicateRows(standardRows, compareRows);
                    rowDiff.CommonRows = standardRows.Intersect(compareRows)?.Except(rowDiff.DuplicateRows.Select(x => x.Item1))?.Distinct().ToList();
                    fileRowDiff.RowDiffs.Add(rowDiff);
                }
            }
            Logger.Log("CheckRows() end");
            return fileRowDiff;
        }

        private FileSelectedColumnDiff CheckRowsOnSelectedColumn(ExcelPackage standardFilePackage, ExcelPackage compareFilePackage,
                                                                List<ColumnGroup> uniqueKeys, List<ColumnGroup> selectedColumns)
        {
            Logger.Log("CheckRowsOnSelectedColumn() start");

            var fileSelectedColumnDiff = new FileSelectedColumnDiff()
            {
                SelectedColumnDiffs = new List<SelectedColumnDiff>()
            };

            var standardFileSheetNames = standardFilePackage.Workbook.Worksheets.Select(x => x.Name).ToList();
            var compareFileSheetNames = compareFilePackage.Workbook.Worksheets.Select(x => x.Name).ToList();
            var commonSheetNames = standardFileSheetNames.Intersect(compareFileSheetNames).ToList();
            foreach (var sheet in commonSheetNames)
            {
                var uniqueKey = uniqueKeys.FirstOrDefault(x => x.Sheet == sheet);
                var selectedColumn = selectedColumns.FirstOrDefault(x => x.Sheet == sheet);
                if (!string.IsNullOrEmpty(uniqueKey?.Sheet) && !string.IsNullOrEmpty(selectedColumn?.Sheet))
                {
                    List<string> columns = new List<string>();
                    List<string> uniqueColumns = new List<string>();
                    List<string> selectedColumnNames = new List<string>();
                    uniqueColumns.AddRange(uniqueKey?.Columns);
                    columns.AddRange(uniqueColumns);
                    selectedColumnNames.AddRange(selectedColumn?.Columns);
                    columns.AddRange(selectedColumnNames);
                    var selectedColumnDiff = new SelectedColumnDiff();
                    var standardWorksheet = standardFilePackage.Workbook.Worksheets[sheet];
                    var comparatorWorksheet = compareFilePackage.Workbook.Worksheets[sheet];
                    var standardRows = Helpers.GetRowsAsSingleLineWithIndex(standardWorksheet, uniqueColumns);
                    var standardRowsWithSelectedColumns = Helpers.GetRowsAsSingleLineWithIndex(standardWorksheet, columns);
                    var compareRows = Helpers.GetRowsAsSingleLineWithIndex(comparatorWorksheet, uniqueColumns);
                    var compareRowsWithSelectedColumns = Helpers.GetRowsAsSingleLineWithIndex(comparatorWorksheet, columns);
                    var commonRowsStandardPriority = standardRows.Intersect(compareRows, new CustomComparer()).Distinct().ToList();
                    var commonRowsComparerPriority = compareRows.Intersect(standardRows, new CustomComparer()).Distinct().ToList();
                    selectedColumnDiff.SheetName = sheet;
                    selectedColumnDiff.ColumnNames = GetColumnNamesWithSelectedColumns(uniqueColumns);
                    selectedColumnDiff.CommonRowsStandard = GetCommonRows(standardRowsWithSelectedColumns, commonRowsStandardPriority);
                    selectedColumnDiff.CommonRowsComparer = GetCommonRows(compareRowsWithSelectedColumns, commonRowsComparerPriority);
                    selectedColumnDiff.SelectedColumns = selectedColumnNames;
                    selectedColumnDiff.UniqueKeys = uniqueColumns;
                    fileSelectedColumnDiff.SelectedColumnDiffs.Add(selectedColumnDiff);
                }
            }
            Logger.Log("CheckRowsOnSelectedColumn() end");
            return fileSelectedColumnDiff;
        }

        private List<string> GetCommonRows(List<Tuple<string, int>> rowsWithSelectedColumn, List<Tuple<string, int>> commonRows)
        {
            List<string> rows = new List<string>();
            foreach (var item in rowsWithSelectedColumn)
            {
                var res = commonRows.BinarySearch(item, new CustomComparerBinarySearch());
                if (res >= 0)
                {
                    rows.Add(item.Item1);
                }
            }
            return rows;
        }

        private List<Tuple<string, int>> GetDuplicateRows(List<string> standardRows, List<string> compareRows)
        {
            var list = new List<Tuple<string, int>>();
            standardRows = standardRows.Distinct().ToList();
            compareRows.Sort();
            foreach (var row in standardRows)
            {
                int firstIndex = compareRows.BinarySearchFirstIndex(row);
                int lastIndex = compareRows.BinarySearchLastIndex(row);
                if (firstIndex != lastIndex)
                {
                    list.Add(new Tuple<string, int>(row, lastIndex - firstIndex + 1));
                }
            }
            return list;
        }

        private List<string> GetColumnNames(List<string> uniqueKeys)
        {
            var columnNames = new List<string>();
            columnNames.AddRange(uniqueKeys);
            columnNames.Add(Constants.Status);
            return columnNames;
        }

        private List<string> GetColumnNamesWithSelectedColumns(List<string> uniqueKeys)
        {
            var columnNames = new List<string>();
            columnNames.AddRange(uniqueKeys);
            columnNames.Add(Constants.StandardValue);
            columnNames.Add(Constants.ComparerValue);
            columnNames.Add(Constants.Status);
            return columnNames;
        }
    }
}
