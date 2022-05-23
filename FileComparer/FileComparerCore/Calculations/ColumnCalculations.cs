using System.Collections.Generic;
using System.Linq;
using DiffPlex;
using DiffPlex.DiffBuilder;
using FileComparerCore.Models;
using FileComparerCore.Utilities;
using OfficeOpenXml;

namespace FileComparerCore.Calculations
{
    public class ColumnCalculations
    {
        public List<SheetMatchSummary> CheckColumnsAndGenerateExcel(ExcelPackage standardFilePackage, ExcelPackage compareFilePackage, ExcelPackage reportFilePackage)
        {
            var filesheetDiff = CheckColumns(standardFilePackage, compareFilePackage);
            new ExcelHelper().WriteToExcel(reportFilePackage, filesheetDiff.FileDiff);
            return filesheetDiff.SheetsMatchSummary;
        }

        public void CheckColumnsAndGenerateExcelSingleFile(ExcelPackage standardFilePackage, ExcelPackage reportFilePackage)
        {

            var columnGroups = CheckColumnsSingleFile(standardFilePackage);
            new ExcelHelper().WriteToExcel(reportFilePackage, columnGroups);
        }

        private List<ColumnGroup> CheckColumnsSingleFile(ExcelPackage excelPackage)
        {
            Logger.Log("CheckColumnsSingleFile() start");
            var sheetNames = excelPackage.Workbook.Worksheets.Select(x => x.Name).ToList();
            var columnGroups = new List<ColumnGroup>();
            foreach (var sheet in sheetNames)
            {
                var currentSheet = excelPackage.Workbook.Worksheets[sheet];
                var columnCount = currentSheet.Dimension?.End?.Column;
                var columnNames = new List<string>();
                for (int i = 1; i <= columnCount; i++)
                {
                    columnNames.Add(currentSheet.Cells[1, i].Value?.ToString());
                }
                columnNames.RemoveAll(item => item == null);
                var columnGroup = new ColumnGroup
                {
                    Sheet = sheet,
                    Columns = columnNames
                };
                columnGroups.Add(columnGroup);
            }
            Logger.Log("CheckColumnsSingleFile() end");
            return columnGroups;
        }

        private FileSheetDiff CheckColumns(ExcelPackage standardFilePackage, ExcelPackage compareFilePackage)
        {
            Logger.Log("CheckColumns() start");
            var fileDiff = new FileDiff();
            var diffBuilder = new SideBySideDiffBuilder(new Differ());
            var sheetsMatchSummary = new List<SheetMatchSummary>();
            var standardFileSheetNames = standardFilePackage.Workbook.Worksheets.Select(x => x.Name).ToList();
            var compareFileSheetNames = compareFilePackage.Workbook.Worksheets.Select(x => x.Name).ToList();

            fileDiff.SheetDiff = diffBuilder.BuildDiffModel(string.Join("\n", standardFileSheetNames),
                string.Join("\n", compareFileSheetNames), false);
            var commonSheetNames = standardFileSheetNames.Intersect(compareFileSheetNames).ToList();
            var standardSheet = standardFileSheetNames.Except(compareFileSheetNames).ToList();
            var compareSheet = compareFileSheetNames.Except(standardFileSheetNames).ToList();
            sheetsMatchSummary.AddRange(GetMissingSheetStatus(standardSheet));
            sheetsMatchSummary.AddRange(GetMissingSheetStatus(compareSheet));
            fileDiff.ColumnDiffs = new List<ColumnDiff>();
            foreach (var name in commonSheetNames)
            {
                var standardWorksheet = standardFilePackage.Workbook.Worksheets[name];
                var compareWorksheet = compareFilePackage.Workbook.Worksheets[name];
                var standardWorksheetColumns = GetColumnNames(standardWorksheet);
                var compareWorksheetColumns = GetColumnNames(compareWorksheet);
                var sheetMatchSummary = new SheetMatchSummary();
                sheetMatchSummary.SheetName = name;
                var isColumnMatch = standardWorksheetColumns.All(compareWorksheetColumns.Contains)
                    && standardWorksheetColumns.Count == compareWorksheetColumns.Count;
                var matchResult = isColumnMatch ? sheetMatchSummary.Status = Constants.ColumnMatchStatus : sheetMatchSummary.Status = Constants.ColumnNoMatchStatus;
                sheetsMatchSummary.Add(sheetMatchSummary);
                if (standardWorksheetColumns.Any() && compareWorksheetColumns.Any())
                {
                    var columnDiff = diffBuilder.BuildDiffModel(string.Join("\n", standardWorksheetColumns),
                    string.Join("\n", compareWorksheetColumns), false);
                    fileDiff.ColumnDiffs.Add(new ColumnDiff
                    {
                        SheetName = name,
                        ColumnDifference = columnDiff
                    });
                }
            }
            Logger.Log("CheckColumns() end");
            return new FileSheetDiff { FileDiff = fileDiff, SheetsMatchSummary = sheetsMatchSummary };
        }

        private List<SheetMatchSummary> GetMissingSheetStatus(List<string> sheet)
        {
            var sheetsMatchSummary = new List<SheetMatchSummary>();
            var sheetMatchSummary = new SheetMatchSummary();
            foreach (var item in sheet)
            {
                sheetMatchSummary.SheetName = item;
                sheetMatchSummary.Status = Constants.ColumnMatchNotAvailableStatus;
            }
            return sheetsMatchSummary;
        }

        private List<string> GetColumnNames(ExcelWorksheet excelWorksheet)
        {
            var columnCount = excelWorksheet.Dimension.End.Column;
            var columnNames = new List<string>();

            for (int i = 1; i <= columnCount; i++)
            {
                columnNames.Add(excelWorksheet.Cells[1, i].Value?.ToString());
            }
            columnNames.RemoveAll(item => item == null);
            return columnNames;
        }
    }
}
