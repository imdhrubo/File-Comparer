using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DiffPlex.DiffBuilder.Model;
using FileComparerCore.Models;
using OfficeOpenXml;
using OfficeOpenXml.Style;

namespace FileComparerCore.Utilities
{
    public class ExcelHelper
    {
        public void WriteToExcel(ExcelPackage filePackage, Summary summary, bool isCompare)
        {
            Logger.Log("WriteToExcel() start (Summary)");
            var sheet = filePackage.Workbook.Worksheets.Add(Constants.SummaryResultTabName);
            filePackage.Workbook.Worksheets.MoveToStart(Constants.SummaryResultTabName);
            WriteSummary(sheet, summary, isCompare);
            sheet.Cells.AutoFitColumns();
            Logger.Log("WriteToExcel() end (Summary)");
        }

        public void WriteToExcel(ExcelPackage reportFilePackage, List<ColumnGroup> columnGroups)
        {
            Logger.Log("WriteToExcel() start (ColumnListSingleFile)");
            var sheet = reportFilePackage.Workbook.Worksheets.Add(Constants.ReportColumnTabName);
            WriteColumnList(sheet, columnGroups);
            sheet.Cells.AutoFitColumns();
            Logger.Log("WriteToExcel() end (ColumnListSingleFile)");
        }

        public void WriteToExcel(ExcelPackage filePackage, FileDiff fileDiff)
        {
            Logger.Log("WriteToExcel() start (columndiff)");
            var sheet = filePackage.Workbook.Worksheets.Add(Constants.ReportColumnResultTabName);
            WriteBoldText(sheet, 1, 1, "Sheet Difference:");
            WriteDiffLine(sheet, "Standard File Sheets", 4, 1, fileDiff.SheetDiff.OldText.Lines);
            WriteDiffLine(sheet, "Compared File Sheets", 4, 2, fileDiff.SheetDiff.NewText.Lines);
            var nextRowNumber = Math.Max(fileDiff.SheetDiff.NewText.Lines.Count, fileDiff.SheetDiff.OldText.Lines.Count) + 10;
            if (!fileDiff.ColumnDiffs.Any())
            {
                WriteBoldText(sheet, nextRowNumber, 1, "No matching sheet detected");
            }
            else
            {
                WriteBoldText(sheet, nextRowNumber, 1, "Column Differences:");
                foreach (var columnDiff in fileDiff.ColumnDiffs)
                {
                    nextRowNumber += 2;
                    WriteBoldText(sheet, nextRowNumber, 1, "Sheet Name: " + columnDiff.SheetName);
                    nextRowNumber += 2;
                    WriteDiffLine(sheet, "Standard File Columns", nextRowNumber, 1, columnDiff.ColumnDifference.OldText.Lines);
                    WriteDiffLine(sheet, "Compared File Columns", nextRowNumber, 2, columnDiff.ColumnDifference.NewText.Lines);
                    nextRowNumber += Math.Max(columnDiff.ColumnDifference.NewText.Lines.Count, columnDiff.ColumnDifference.OldText.Lines.Count) + 2;
                }
            }
            sheet.Cells.AutoFitColumns();
            Logger.Log("WriteToExcel() end (columndiff)");
        }

        public List<RowMatchSummary> WriteToExcel(ExcelPackage filePackage, FileRowDiff fileRowDiff)
        {
            Logger.Log("WriteToExcel() start (rowDiff)");
            List<RowMatchSummary> rowsMatchSummary = new List<RowMatchSummary>();

            foreach (var rowDiff in fileRowDiff.RowDiffs)
            {
                var sheet = filePackage.Workbook.Worksheets.Add(Constants.ReportRowResultTabName + " - " + rowDiff.SheetName);
                int rowIndex = 1;
                if (rowDiff.ColumnNames.Any())
                {
                    var count = 1;
                    foreach (var column in rowDiff.ColumnNames)
                    {
                        WriteBoldText(sheet, rowIndex, count, column);
                        count++;
                    }
                    rowIndex++;
                }
                foreach (var value in rowDiff.MissingRows)
                {
                    var contents = value.Split(Constants.Parser.ToCharArray(), StringSplitOptions.RemoveEmptyEntries).ToList();
                    WriteRowDiff(sheet, rowIndex++, contents, Constants.Missing);
                }
                foreach (var value in rowDiff.SurplusRows)
                {
                    var contents = value.Split(Constants.Parser.ToCharArray(), StringSplitOptions.RemoveEmptyEntries).ToList();
                    WriteRowDiff(sheet, rowIndex++, contents, Constants.Surplus);
                }
                foreach (var value in rowDiff.DuplicateRows)
                {
                    var contents = value.Item1.Split(Constants.Parser.ToCharArray(), StringSplitOptions.RemoveEmptyEntries).ToList();
                    WriteRowDiff(sheet, rowIndex++, contents, Constants.Duplicate + " (" + value.Item2 + ")");
                }
                foreach (var value in rowDiff.CommonRows)
                {
                    var contents = value.Split(Constants.Parser.ToCharArray(), StringSplitOptions.RemoveEmptyEntries).ToList();
                    WriteRowDiff(sheet, rowIndex++, contents, Constants.Match);
                }
                rowIndex += 3;
                sheet.Cells[sheet.Dimension.Address].AutoFilter = true;
                var rowMatchSummary = new RowMatchSummary
                {
                    SheetName = rowDiff.SheetName,
                    MatchCount = rowDiff.CommonRows.Count,
                    MissingCount = rowDiff.MissingRows.Count,
                    SurplusCount = rowDiff.SurplusRows.Count,
                    DuplicateCount = rowDiff.DuplicateRows.Count
                };
                rowsMatchSummary.Add(rowMatchSummary);
                WriteStatisticsForRowDiff(sheet, 1, rowDiff.ColumnNames.Count + 5, rowMatchSummary);
                sheet.Cells.AutoFitColumns();
            }
            Logger.Log("WriteToExcel() end (rowDiff)");
            return rowsMatchSummary;
        }

        public List<DataMatchSummary> WriteToExcel(ExcelPackage filePackage, FileSelectedColumnDiff fileSelectedColumnDiff)
        {
            Logger.Log("WriteToExcel() start (selectedColumnDiff)");
            var dataMatches = new List<DataMatchSummary>();
            foreach (var selectedColumnDiff in fileSelectedColumnDiff.SelectedColumnDiffs)
            {
                var counter = 0;
                foreach (var selectedColumn in selectedColumnDiff.SelectedColumns)
                {
                    var selectedColumnDiffMatchCount = 0;
                    var selectedColumnDiffNoMatchCount = 0;
                    selectedColumnDiff.TabName = selectedColumn;
                    selectedColumnDiff.UniqueKeyEndIndex = selectedColumnDiff.UniqueKeys.Count;
                    selectedColumnDiff.SelectedColumnIndex = selectedColumnDiff.UniqueKeys.Count + counter;
                    counter++;
                    var sheet = filePackage.Workbook.Worksheets.Add(selectedColumnDiff.SheetName + " - " + selectedColumnDiff.TabName);
                    int rowIndex = 1;
                    if (selectedColumnDiff.ColumnNames.Any())
                    {
                        var count = 1;
                        foreach (var column in selectedColumnDiff.ColumnNames)
                        {
                            WriteBoldText(sheet, rowIndex, count, column);
                            count++;
                        }
                        rowIndex++;
                    }
                    for (int i = 0; i < selectedColumnDiff.CommonRowsStandard.Count; i++)
                    {
                        var standard = selectedColumnDiff.CommonRowsStandard[i].Split(Constants.Parser.ToCharArray(),
                            StringSplitOptions.RemoveEmptyEntries).ToList();
                        List<string> content = new List<string>();
                        content = standard.GetRange(0, selectedColumnDiff.UniqueKeyEndIndex);
                        content.Add(standard[selectedColumnDiff.SelectedColumnIndex]);
                        var comparer = selectedColumnDiff.CommonRowsComparer[i].Split(Constants.Parser.ToCharArray(),
                            StringSplitOptions.RemoveEmptyEntries).ToList();
                        content.Add(comparer[selectedColumnDiff.SelectedColumnIndex]);
                        var result = WriteRowDiffAndGetStatus(sheet, rowIndex++, content);
                        if (result == RowStatus.Match)
                        {
                            selectedColumnDiffMatchCount++;
                        }
                        else if (result == RowStatus.NoMatch)
                        {
                            selectedColumnDiffNoMatchCount++;
                        }
                    }
                    rowIndex += 3;
                    sheet.Cells[sheet.Dimension.Address].AutoFilter = true;
                    DataMatchSummary dataMatchSummary = new DataMatchSummary
                    {
                        SheetName = selectedColumnDiff.SheetName,
                        MatchCount = selectedColumnDiffMatchCount,
                        NoMatchCount = selectedColumnDiffNoMatchCount,
                        SelectedColumns = selectedColumnDiff.TabName
                    };
                    dataMatches.Add(dataMatchSummary);
                    WriteStatisticsForSelectedColumnDiff(sheet, 1, selectedColumnDiff.ColumnNames.Count + 5, dataMatchSummary);
                    sheet.Cells.AutoFitColumns();
                }
            }
            Logger.Log("WriteToExcel() end (selectedColumnDiff)");
            return dataMatches;
        }

        public void WriteToExcel(ExcelPackage filePackage, List<GroupInformation> groupInformations, bool isCompare)
        {
            Logger.Log("WriteToExcel() start (groupInformations)");
            foreach (var groupInformation in groupInformations)
            {
                var sheet = filePackage.Workbook.Worksheets.Add(Constants.ReportGroupResultTabName + " - " +
                    groupInformation.SheetName);
                int rowIndex = 1;
                int colIndex = 1;
                if (groupInformation.ColumnNames.Any())
                {
                    var count = 1;
                    foreach (var column in groupInformation.ColumnNames)
                    {
                        WriteBoldText(sheet, rowIndex, count, column);
                        count++;
                    }
                    rowIndex++;
                }
                foreach (var groupRowInformation in groupInformation.GroupRowInformations)
                {
                    var contents = groupRowInformation.Key.
                        Split(Constants.Parser.ToCharArray(), StringSplitOptions.RemoveEmptyEntries).ToList();
                    colIndex = 1;
                    foreach (var content in contents)
                    {
                        sheet.Cells[rowIndex, colIndex++].Value = content;
                    }
                    sheet.Cells[rowIndex, colIndex++].Value = groupRowInformation.Value.CountInStandard;
                    if (isCompare)
                    {
                        sheet.Cells[rowIndex, colIndex++].Value = groupRowInformation.Value.CountInCompare;
                        sheet.Cells[rowIndex, colIndex++].Value = groupRowInformation.Value.CountInCompare -
                            groupRowInformation.Value.CountInStandard;
                    }
                    rowIndex++;
                }
                sheet.Cells[sheet.Dimension.Address].AutoFilter = true;
                rowIndex += 2;
                colIndex = groupInformation.ColumnNames.Count - (isCompare ? 3 : 1);
                WriteBoldText(sheet, rowIndex, colIndex++, Constants.Total);
                sheet.Cells[rowIndex, colIndex++].Value = groupInformation.GroupRowInformations.Sum(x => x.Value.CountInStandard);
                if (isCompare)
                {
                    sheet.Cells[rowIndex, colIndex++].Value = groupInformation.GroupRowInformations.Sum(x => x.Value.CountInCompare);
                    sheet.Cells[rowIndex, colIndex++].Value = groupInformation.GroupRowInformations.Sum(x => x.Value.CountInCompare
                        - x.Value.CountInStandard);
                }
                sheet.Cells.AutoFitColumns();
            }
            Logger.Log("WriteToExcel() end (groupInformations)");
        }

        public void WriteToExcel(ExcelPackage filePackage, DistributionInformation distributionInformation, bool isCompare)
        {
            Logger.Log("WriteToExcel() start (distributionInformation)");
            foreach (var sheetDistributionInformation in distributionInformation.SheetDistributionInformations)
            {
                var sheet = filePackage.Workbook.Worksheets.Add(Constants.ReportDistributionResultTabName + " - " +
                    sheetDistributionInformation.SheetName);
                int rowIndex = 1;
                int colIndex = 1;
                if (distributionInformation.ColumnNames.Any())
                {
                    var count = 1;
                    foreach (var column in distributionInformation.ColumnNames)
                    {
                        WriteBoldText(sheet, rowIndex, count, column);
                        count++;
                    }
                    rowIndex++;
                }
                foreach (var sheetColumnDistributionInformation in sheetDistributionInformation.SheetColumnDistributionInformations)
                {
                    foreach (var rowInformation in sheetColumnDistributionInformation.RowInformations)
                    {
                        var contents = rowInformation.Key.
                            Split(Constants.Parser.ToCharArray(), StringSplitOptions.RemoveEmptyEntries).ToList();
                        if (contents.Any())
                        {
                            colIndex = 1;
                            sheet.Cells[rowIndex, colIndex++].Value = sheetColumnDistributionInformation.ColumnName;
                            sheet.Cells[rowIndex, colIndex++].Value = contents[0];
                            sheet.Cells[rowIndex, colIndex++].Value = rowInformation.Value.CountInStandard;
                            if (isCompare)
                            {
                                sheet.Cells[rowIndex, colIndex++].Value = rowInformation.Value.CountInCompare;
                                sheet.Cells[rowIndex, colIndex++].Value = rowInformation.Value.CountInCompare -
                                    rowInformation.Value.CountInStandard;
                            }
                            rowIndex++;
                        }
                    }
                }
                sheet.Cells[sheet.Dimension.Address].AutoFilter = true;
                rowIndex += 2;
                sheet.Cells.AutoFitColumns();
            }
            Logger.Log("WriteToExcel() end (distributionInformation)");
        }

        public async Task<List<ColumnGroup>> GetColumnNames(string standardFilePath, string compareFilePath)
        {
            var columnGroups = new List<ColumnGroup>();
            try
            {
                FileInfo standardFile = new FileInfo(standardFilePath);
                FileInfo compareFile = new FileInfo(compareFilePath);
                await Task.Run(() =>
                {
                    using (ExcelPackage standardFilePackage = new ExcelPackage(standardFile))
                    using (ExcelPackage compareFilePackage = new ExcelPackage(compareFile))
                    {
                        var standardFileSheetNames = standardFilePackage.Workbook.Worksheets.Select(x => x.Name).ToList();
                        var compareFileSheetNames = compareFilePackage.Workbook.Worksheets.Select(x => x.Name).ToList();
                        var commonSheetNames = standardFileSheetNames.Intersect(compareFileSheetNames).ToList();
                        columnGroups = GetColumnGroups(standardFilePackage, commonSheetNames);
                    }
                });
                return columnGroups;
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message);
                return columnGroups;
            }
        }

        public async Task<List<ColumnGroup>> GetColumnNames(string standardFilePath)
        {
            var columnGroups = new List<ColumnGroup>();
            try
            {
                FileInfo standardFile = new FileInfo(standardFilePath);
                await Task.Run(() =>
                {
                    using (ExcelPackage standardFilePackage = new ExcelPackage(standardFile))
                    {
                        var standardFileSheetNames = standardFilePackage.Workbook.Worksheets.Select(x => x.Name).ToList();
                        columnGroups = GetColumnGroups(standardFilePackage, standardFileSheetNames);
                    }
                });
                return columnGroups;
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message);
                return columnGroups;
            }
        }

        private List<ColumnGroup> GetColumnGroups(ExcelPackage excelPackage, List<string> sheetNames)
        {
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
            return columnGroups;
        }

        private void WriteColumnList(ExcelWorksheet sheet, List<ColumnGroup> columnGroups)
        {
            int rowIndex = 1;
            WriteBoldText(sheet, rowIndex, 1, "Columns:");
            foreach (var columnGroup in columnGroups)
            {
                rowIndex += 2;
                WriteBoldText(sheet, rowIndex++, 1, "Sheet Name: " + columnGroup.Sheet);
                foreach (var column in columnGroup.Columns)
                {
                    sheet.Cells[rowIndex++, 1].Value = column;
                }
            }
        }

        private void WriteDiffLine(ExcelWorksheet sheet, string title, int row, int column, List<DiffPiece> lines)
        {
            WriteBoldText(sheet, row, column, title);
            row++;
            foreach (var diffLine in lines)
            {
                switch (diffLine.Type)
                {
                    case ChangeType.Deleted:
                        SetBackGroundColor(sheet, row, column, Constants.BackgroundColorDeleted);
                        break;
                    case ChangeType.Imaginary:
                        SetBackGroundColor(sheet, row, column, Constants.BackgroundColorImaginary);
                        break;
                    case ChangeType.Inserted:
                        SetBackGroundColor(sheet, row, column, Constants.BackgroundColorInserted);
                        break;
                    case ChangeType.Modified:
                        SetBackGroundColor(sheet, row, column, Constants.BackgroundColorModified);
                        break;
                    case ChangeType.Unchanged:
                        SetBackGroundColor(sheet, row, column, Constants.BackgroundColorUnchanged);
                        break;
                }
                sheet.Cells[row, column].Value = "";
                if (diffLine.Type == ChangeType.Inserted || diffLine.Type == ChangeType.Deleted || diffLine.Type == ChangeType.Unchanged)
                {
                    sheet.Cells[row, column].Value += diffLine.Text;
                }
                else if (diffLine.Type == ChangeType.Modified)
                {
                    foreach (var subpiece in diffLine.SubPieces)
                    {
                        if (subpiece.Type == ChangeType.Imaginary)
                        {
                            continue;
                        }
                        sheet.Cells[row, column].Value += subpiece.Text;
                    }
                }
                row++;
            }
        }

        private void WriteSummary(ExcelWorksheet sheet, Summary summary, bool isCompare)
        {
            int rowIndex = 1, colIndex = 1;
            sheet.Cells[rowIndex, colIndex++].Value = "Excel_Compare_Utility";
            sheet.Cells[rowIndex, colIndex++].Value = summary.FileMatchSummary.ExcelCompareUtility;
            rowIndex += 2;
            colIndex = 1;
            sheet.Cells[rowIndex, colIndex++].Value = "Standard Sheet";
            sheet.Cells[rowIndex, colIndex++].Value = summary.FileMatchSummary.StandardSheet.Name;
            sheet.Cells[rowIndex, colIndex++].Value = summary.FileMatchSummary.StandardSheet.FileSize + "KB (File Size)";
            sheet.Cells[rowIndex, colIndex++].Value = summary.FileMatchSummary.StandardSheet.CreatedDate + "(Created Date)";
            sheet.Cells[rowIndex++, colIndex].Value = summary.FileMatchSummary.StandardSheet.ModifiedDate + "(Modified Date)";
            if (isCompare)
            {
                colIndex = 1;
                sheet.Cells[rowIndex, colIndex++].Value = "Compare Sheet";
                sheet.Cells[rowIndex, colIndex++].Value = summary.FileMatchSummary.CompareSheet.Name;
                sheet.Cells[rowIndex, colIndex++].Value = summary.FileMatchSummary.CompareSheet.FileSize + "KB (File Size)";
                sheet.Cells[rowIndex, colIndex++].Value = summary.FileMatchSummary.CompareSheet.CreatedDate + "(Created Date)";
                sheet.Cells[rowIndex, colIndex].Value = summary.FileMatchSummary.CompareSheet.ModifiedDate + "(Modified Date)";
            }
            rowIndex += 2;
            colIndex = 1;
            sheet.Cells[rowIndex, colIndex++].Value = "Standard Sheet Tabs";
            foreach (var item in summary.FileMatchSummary.StandardSheetInfoSummary)
            {
                sheet.Cells[rowIndex, colIndex++].Value = item.SheetName;
                sheet.Cells[rowIndex++, colIndex].Value = item.RowCount + " rows";
                colIndex = 2;
            }
            if (isCompare)
            {
                rowIndex += 2;
                colIndex = 1;
                sheet.Cells[rowIndex, colIndex++].Value = "Compare Sheet Tabs";
                foreach (var item in summary.FileMatchSummary.CompareSheetInfoSummary)
                {
                    sheet.Cells[rowIndex, colIndex++].Value = item.SheetName;
                    sheet.Cells[rowIndex++, colIndex].Value = item.RowCount + " rows";
                    colIndex = 2;
                }

                colIndex = 1;
                rowIndex++;
                sheet.Cells[rowIndex, colIndex++].Value = "Tabs All Match";
                sheet.Cells[rowIndex, colIndex].Value = summary.FileMatchSummary.AreSheetsAllMatch;
                rowIndex += 2;
                if (summary.SheetsMatchSummary.Count != 0)
                {
                    foreach (var item in summary.SheetsMatchSummary)
                    {
                        colIndex = 1;
                        sheet.Cells[rowIndex, colIndex++].Value = item.SheetName + " Columns Match";
                        sheet.Cells[rowIndex++, colIndex].Value = item.Status;
                    }
                }
                rowIndex += 2;
                colIndex = 1;
                if (summary.RowsMatchSummary.Count != 0)
                {
                    sheet.Cells[rowIndex++, colIndex].Value = "Row Matching";
                    foreach (var item in summary.RowsMatchSummary)
                    {
                        sheet.Cells[rowIndex, colIndex++].Value = item.SheetName;
                        sheet.Cells[rowIndex, colIndex++].Value = "Match";
                        sheet.Cells[rowIndex++, colIndex].Value = item.MatchCount;
                        colIndex = 2;
                        sheet.Cells[rowIndex, colIndex++].Value = "Missing";
                        sheet.Cells[rowIndex++, colIndex].Value = item.MissingCount;
                        colIndex = 2;
                        sheet.Cells[rowIndex, colIndex++].Value = "Surplus";
                        sheet.Cells[rowIndex++, colIndex].Value = item.SurplusCount;
                        colIndex = 2;
                        sheet.Cells[rowIndex, colIndex++].Value = "Duplicate";
                        sheet.Cells[rowIndex++, colIndex].Value = item.DuplicateCount;
                        rowIndex += 2;
                        colIndex = 1;
                    }
                    colIndex = 1;
                    rowIndex++;
                }
                if (summary.DatasMatchSummary.Count != 0)
                {
                    sheet.Cells[rowIndex++, colIndex].Value = "Data Matching";
                    foreach (var item in summary.DatasMatchSummary)
                    {
                        sheet.Cells[rowIndex++, colIndex].Value = item.SheetName;
                        sheet.Cells[rowIndex, colIndex++].Value = item.SelectedColumns;

                        sheet.Cells[rowIndex, colIndex++].Value = "Match";
                        sheet.Cells[rowIndex++, colIndex].Value = item.MatchCount;
                        colIndex = 2;
                        sheet.Cells[rowIndex, colIndex++].Value = "No Match";
                        sheet.Cells[rowIndex++, colIndex].Value = item.NoMatchCount;
                        rowIndex++;
                        colIndex = 1;
                    }
                }
            }
        }

        private void WriteRowDiff(ExcelWorksheet sheet, int rowIndex, List<string> contents, string status)
        {
            int colIndex = 1;
            foreach (var content in contents)
            {
                if (!string.IsNullOrWhiteSpace(content))
                {
                    sheet.Cells[rowIndex, colIndex].Value = content;
                    colIndex++;
                }
            }
            sheet.Cells[rowIndex, colIndex].Value = status;
            SetBackGroundColorByStatus(sheet, rowIndex, colIndex, status);
        }

        private RowStatus WriteRowDiffAndGetStatus(ExcelWorksheet sheet, int rowIndex, List<string> contents)
        {
            int colIndex = 1;
            foreach (var content in contents)
            {
                if (!string.IsNullOrWhiteSpace(content))
                {
                    sheet.Cells[rowIndex, colIndex].Value = content;
                    colIndex++;
                }
            }
            return SetStatus(sheet, rowIndex, colIndex);
        }

        private RowStatus SetStatus(ExcelWorksheet sheet, int rowIndex, int colIndex)
        {
            if (sheet.Cells[rowIndex, colIndex - 1]?.Value?.ToString() == sheet.Cells[rowIndex, colIndex - 2]?.Value?.ToString())
            {
                sheet.Cells[rowIndex, colIndex].Value = Constants.Match;
                SetBackGroundColorByStatus(sheet, rowIndex, colIndex, Constants.Match);
                return RowStatus.Match;
            }
            else
            {
                sheet.Cells[rowIndex, colIndex].Value = Constants.NoMatch;
                SetBackGroundColorByStatus(sheet, rowIndex, colIndex, Constants.NoMatch);
                return RowStatus.NoMatch;
            }
        }

        private void SetBackGroundColorByStatus(ExcelWorksheet sheet, int row, int column, string status)
        {
            var hexColor = "";
            if (status == Constants.Match)
            {
                hexColor = Constants.BackgroundColorUnchanged;
            }
            else if (status == Constants.Surplus)
            {
                hexColor = Constants.BackgroundColorInserted;
            }
            else if (status == Constants.Missing || status == Constants.NoMatch)
            {
                hexColor = Constants.BackgroundColorDeleted;
            }
            else if (status.Contains(Constants.Duplicate))
            {
                hexColor = Constants.BackgroundColorImaginary;
            }
            if (!string.IsNullOrWhiteSpace(hexColor))
            {
                SetBackGroundColor(sheet, row, column, hexColor);
            }
        }

        private void SetBackGroundColor(ExcelWorksheet sheet, int row, int column, string hexColor)
        {
            sheet.Cells[row, column].Style.Fill.PatternType = ExcelFillStyle.Solid;
            sheet.Cells[row, column].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(hexColor));
        }

        private void WriteBoldText(ExcelWorksheet sheet, int row, int column, string text)
        {
            sheet.Cells[row, column].Value = text;
            sheet.Cells[row, column].Style.Font.Bold = true;
        }

        private void WriteStatisticsForRowDiff(ExcelWorksheet sheet, int startingRow, int startingColumn, RowMatchSummary rowMatch)
        {
            WriteBoldText(sheet, startingRow, startingColumn, "Statistics:");
            startingRow += 2;
            WriteBoldText(sheet, startingRow, startingColumn, Constants.Match);
            WriteBoldText(sheet, startingRow + 1, startingColumn, Constants.Missing);
            WriteBoldText(sheet, startingRow + 2, startingColumn, Constants.Surplus);
            WriteBoldText(sheet, startingRow + 3, startingColumn, Constants.Duplicate);
            sheet.Cells[startingRow, startingColumn + 1].Value = rowMatch.MatchCount;
            sheet.Cells[startingRow + 1, startingColumn + 1].Value = rowMatch.MissingCount;
            sheet.Cells[startingRow + 2, startingColumn + 1].Value = rowMatch.SurplusCount;
            sheet.Cells[startingRow + 3, startingColumn + 1].Value = rowMatch.DuplicateCount;
        }

        private void WriteStatisticsForSelectedColumnDiff(ExcelWorksheet sheet, int startingRow, int startingColumn,
            DataMatchSummary dataMatch)
        {
            WriteBoldText(sheet, startingRow, startingColumn, "Statistics:");
            startingRow += 2;
            WriteBoldText(sheet, startingRow, startingColumn, Constants.Match);
            WriteBoldText(sheet, startingRow + 1, startingColumn, Constants.NoMatch);
            sheet.Cells[startingRow, startingColumn + 1].Value = dataMatch.MatchCount;
            sheet.Cells[startingRow + 1, startingColumn + 1].Value = dataMatch.NoMatchCount;
        }
    }
}
