using System.Collections.Generic;
using System.Linq;
using FileComparerCore.Models;
using FileComparerCore.Utilities;
using OfficeOpenXml;

namespace FileComparerCore.Calculations
{
    public class DistributionCalculations
    {
        public void CheckDistributionAndGeneratedExcel(ExcelPackage standardFilePackage, ExcelPackage compareFilePackage, ExcelPackage reportFilePackage,
            List<ColumnGroup> columnGroups)
        {
            var distributionInformation = CheckDistributions(standardFilePackage, compareFilePackage, columnGroups);
            new ExcelHelper().WriteToExcel(reportFilePackage, distributionInformation, true);
        }

        public void CheckDistributionAndGeneratedExcelSingleFile(ExcelPackage standardFilePackage, ExcelPackage reportFilePackage,
            List<ColumnGroup> columnGroups)
        {
            var distributionInformation = CheckDistributionsSingleFile(standardFilePackage, columnGroups);
            new ExcelHelper().WriteToExcel(reportFilePackage, distributionInformation, false);
        }

        private DistributionInformation CheckDistributions(ExcelPackage standardFilePackage, ExcelPackage compareFilePackage, List<ColumnGroup> columnGroups)
        {
            Logger.Log("CheckDistributions() start");
            var distributionInformation = new DistributionInformation
            {
                ColumnNames = new List<string>(),
                SheetDistributionInformations = new List<SheetDistributionInformation>()
            };
            var standardFileSheetNames = standardFilePackage.Workbook.Worksheets.Select(x => x.Name).ToList();
            var compareFileSheetNames = compareFilePackage.Workbook.Worksheets.Select(x => x.Name).ToList();
            var commonSheetNames = standardFileSheetNames.Intersect(compareFileSheetNames).ToList();
            foreach (var sheet in commonSheetNames)
            {
                var columnGroup = columnGroups.FirstOrDefault(x => x.Sheet == sheet);

                if (!string.IsNullOrWhiteSpace(columnGroup?.Sheet))
                {
                    var sheetDistributionInformation = new SheetDistributionInformation
                    {
                        SheetName = sheet,
                        SheetColumnDistributionInformations = new List<SheetColumnDistributionInformation>()
                    };
                    var standardWorksheet = standardFilePackage.Workbook.Worksheets[sheet];
                    var comparatorWorksheet = compareFilePackage.Workbook.Worksheets[sheet];
                    foreach (var column in columnGroup.Columns)
                    {
                        var standardRows = Helpers.GetRowsAsSingleLine(standardWorksheet, new string[] { column });
                        var compareRows = Helpers.GetRowsAsSingleLine(comparatorWorksheet, new string[] { column });
                        var sheetColumnDistributionInformation = new SheetColumnDistributionInformation
                        {
                            ColumnName = column,
                            RowInformations = new SortedList<string, RowCount>()
                        };
                        AddDistributionInformation(standardRows, sheetColumnDistributionInformation, true);
                        AddDistributionInformation(compareRows, sheetColumnDistributionInformation, false);
                        sheetDistributionInformation.SheetColumnDistributionInformations.Add(sheetColumnDistributionInformation);
                    }
                    distributionInformation.SheetDistributionInformations.Add(sheetDistributionInformation);
                    distributionInformation.ColumnNames = GetColumnNames(true);
                }
            }
            Logger.Log("CheckDistributions() end");
            return distributionInformation;
        }

        private DistributionInformation CheckDistributionsSingleFile(ExcelPackage standardFilePackage, List<ColumnGroup> columnGroups)
        {
            Logger.Log("CheckDistributions() start");
            var distributionInformation = new DistributionInformation
            {
                ColumnNames = new List<string>(),
                SheetDistributionInformations = new List<SheetDistributionInformation>()
            };
            var standardFileSheetNames = standardFilePackage.Workbook.Worksheets.Select(x => x.Name).ToList();
            foreach (var sheet in standardFileSheetNames)
            {
                var columnGroup = columnGroups.FirstOrDefault(x => x.Sheet == sheet);

                if (!string.IsNullOrWhiteSpace(columnGroup?.Sheet))
                {
                    var sheetDistributionInformation = new SheetDistributionInformation
                    {
                        SheetName = sheet,
                        SheetColumnDistributionInformations = new List<SheetColumnDistributionInformation>()
                    };
                    var standardWorksheet = standardFilePackage.Workbook.Worksheets[sheet];
                    foreach (var column in columnGroup.Columns)
                    {
                        var standardRows = Helpers.GetRowsAsSingleLine(standardWorksheet, new string[] { column });
                        var sheetColumnDistributionInformation = new SheetColumnDistributionInformation
                        {
                            ColumnName = column,
                            RowInformations = new SortedList<string, RowCount>()
                        };
                        AddDistributionInformation(standardRows, sheetColumnDistributionInformation, true);
                        sheetDistributionInformation.SheetColumnDistributionInformations.Add(sheetColumnDistributionInformation);
                    }
                    distributionInformation.SheetDistributionInformations.Add(sheetDistributionInformation);
                    distributionInformation.ColumnNames = GetColumnNames(false);
                }
            }
            Logger.Log("CheckDistributions() end");
            return distributionInformation;
        }

        private void AddDistributionInformation(List<string> rows, SheetColumnDistributionInformation sheetColumnDistributionInformation,
            bool isStandard = true)
        {
            foreach (var row in rows)
            {
                if (sheetColumnDistributionInformation.RowInformations.ContainsKey(row))
                {
                    if (isStandard)
                    {
                        sheetColumnDistributionInformation.RowInformations[row].CountInStandard++;
                    }
                    else
                    {
                        sheetColumnDistributionInformation.RowInformations[row].CountInCompare++;
                    }
                }
                else
                {
                    int standardCount, compareCount;
                    if (isStandard)
                    {
                        standardCount = 1;
                        compareCount = 0;
                    }
                    else
                    {
                        standardCount = 0;
                        compareCount = 1;
                    }
                    sheetColumnDistributionInformation.RowInformations.Add(row, new RowCount(standardCount, compareCount));
                }
            }
        }

        private List<string> GetColumnNames(bool isCompare)
        {
            var columnNames = new List<string>();
            columnNames.Add(Constants.ColumnName);
            columnNames.Add(Constants.Value);
            columnNames.Add(Constants.StandardFile);
            if (isCompare)
            {
                columnNames.Add(Constants.CompareFile);
                columnNames.Add(Constants.Difference);
            }
            return columnNames;
        }
    }
}
