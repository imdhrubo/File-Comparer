using System.Collections.Generic;
using System.Linq;
using FileComparerCore.Models;
using FileComparerCore.Utilities;
using OfficeOpenXml;

namespace FileComparerCore.Calculations
{
    public class GroupCalculations
    {
        public void CheckGroupsAndGeneratedExcel(ExcelPackage standardFilePackage, ExcelPackage compareFilePackage, ExcelPackage reportFilePackage,
            List<ColumnGroup> columnGroups)
        {
            var groupInformations = CheckGroups(standardFilePackage, compareFilePackage, columnGroups);
            new ExcelHelper().WriteToExcel(reportFilePackage, groupInformations, true);
        }

        public void CheckGroupsAndGeneratedExcelSingleFile(ExcelPackage standardFilePackage, ExcelPackage reportFilePackage,
           List<ColumnGroup> columnGroups)
        {
            var groupInformations = CheckGroupsSingleFile(standardFilePackage, columnGroups);
            new ExcelHelper().WriteToExcel(reportFilePackage, groupInformations, false);
        }

        private List<GroupInformation> CheckGroups(ExcelPackage standardFilePackage, ExcelPackage compareFilePackage, List<ColumnGroup> columnGroups)
        {
            Logger.Log("CheckGroups() start");
            var groupInformations = new List<GroupInformation>();
            var standardFileSheetNames = standardFilePackage.Workbook.Worksheets.Select(x => x.Name).ToList();
            var compareFileSheetNames = compareFilePackage.Workbook.Worksheets.Select(x => x.Name).ToList();
            var commonSheetNames = standardFileSheetNames.Intersect(compareFileSheetNames).ToList();
            foreach (var sheet in commonSheetNames)
            {
                var groupColumns = columnGroups.FirstOrDefault(x => x.Sheet == sheet);

                if (!string.IsNullOrWhiteSpace(groupColumns?.Sheet))
                {
                    var groupInformation = new GroupInformation
                    {
                        SheetName = sheet,
                        GroupRowInformations = new SortedList<string, RowCount>()
                    };
                    var standardWorksheet = standardFilePackage.Workbook.Worksheets[sheet];
                    var comparatorWorksheet = compareFilePackage.Workbook.Worksheets[sheet];
                    var standardRows = Helpers.GetRowsAsSingleLine(standardWorksheet, groupColumns.Columns);
                    var compareRows = Helpers.GetRowsAsSingleLine(comparatorWorksheet, groupColumns.Columns);
                    AddGroupInformation(standardRows, groupInformation, true);
                    AddGroupInformation(compareRows, groupInformation, false);
                    groupInformation.ColumnNames = GetColumnNames(groupColumns.Columns, true);
                    groupInformations.Add(groupInformation);
                }
            }
            Logger.Log("CheckGroups() end");
            return groupInformations;
        }

        private List<GroupInformation> CheckGroupsSingleFile(ExcelPackage standardFilePackage, List<ColumnGroup> columnGroups)
        {
            Logger.Log("CheckGroups() start");
            var groupInformations = new List<GroupInformation>();
            var standardFileSheetNames = standardFilePackage.Workbook.Worksheets.Select(x => x.Name).ToList();
            foreach (var sheet in standardFileSheetNames)
            {
                var groupColumns = columnGroups.FirstOrDefault(x => x.Sheet == sheet);

                if (!string.IsNullOrWhiteSpace(groupColumns?.Sheet))
                {
                    var groupInformation = new GroupInformation
                    {
                        SheetName = sheet,
                        GroupRowInformations = new SortedList<string, RowCount>()
                    };
                    var standardWorksheet = standardFilePackage.Workbook.Worksheets[sheet];
                    var standardRows = Helpers.GetRowsAsSingleLine(standardWorksheet, groupColumns.Columns);
                    AddGroupInformation(standardRows, groupInformation, true);
                    groupInformation.ColumnNames = GetColumnNames(groupColumns.Columns, false);
                    groupInformations.Add(groupInformation);
                }
            }
            Logger.Log("CheckGroups() end");
            return groupInformations;
        }

        private void AddGroupInformation(List<string> rows, GroupInformation groupInformation, bool isStandard)
        {
            foreach (var row in rows)
            {
                if (groupInformation.GroupRowInformations.ContainsKey(row))
                {
                    if (isStandard)
                    {
                        groupInformation.GroupRowInformations[row].CountInStandard++;
                    }
                    else
                    {
                        groupInformation.GroupRowInformations[row].CountInCompare++;
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
                    groupInformation.GroupRowInformations.Add(row, new RowCount(standardCount, compareCount));
                }
            }
        }

        private List<string> GetColumnNames(List<string> passedColumns, bool isCompare)
        {
            var columnNames = new List<string>();
            columnNames.AddRange(passedColumns);
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
