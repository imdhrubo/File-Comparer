using FileComparerCore.Calculations;
using FileComparerCore.Models;
using FileComparerCore.Utilities;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FileComparerCore
{
    public class FileComparer
    {
        public CompareExcelResult CompareExcel(Config config, string standardFilePath, string compareFilePath, string reportFilePath)
        {
            try
            {
                Logger.Log("CompareExcel() start");
                Summary summary = new Summary()
                {
                    RowsMatchSummary = new List<RowMatchSummary>(),
                    DatasMatchSummary = new List<DataMatchSummary>(),
                    SheetsMatchSummary = new List<SheetMatchSummary>(),
                    FileMatchSummary = new FileMatchSummary()
                };
                var report = GetReportFilePath(reportFilePath);
                FileInfo standardFile = new FileInfo(standardFilePath);
                FileInfo compareFile = new FileInfo(compareFilePath);
                FileInfo reportFile = new FileInfo(report);
                using (ExcelPackage reportFilePacakage = new ExcelPackage(reportFile))
                using (ExcelPackage standardFilePackage = new ExcelPackage(standardFile))
                using (ExcelPackage compareFilePackage = new ExcelPackage(compareFile))
                {
                    summary.FileMatchSummary = GetFileMatchSummary(standardFilePackage, compareFilePackage);
                    if (config.CheckColumns)
                    {
                        var columnCalculations = new ColumnCalculations();
                        summary.SheetsMatchSummary = columnCalculations.CheckColumnsAndGenerateExcel(standardFilePackage, compareFilePackage, reportFilePacakage);
                    }
                    if (config.CheckRows)
                    {
                        var rowCalculations = new RowCalculations();
                        summary.RowsMatchSummary = rowCalculations.CheckRowsAndGenerateExcel(standardFilePackage, compareFilePackage, reportFilePacakage, config.UniqueKeys);
                    }
                    if (config.CheckOnSelectedColumns)
                    {
                        var rowCalculations = new RowCalculations();
                        summary.DatasMatchSummary = rowCalculations.CheckRowsOnSelectedColumnsAndGenerateExcel(standardFilePackage, compareFilePackage, reportFilePacakage,
                                                                                       config.UniqueKeys, config.SelectedColumns);
                    }
                    if (config.CheckGroups)
                    {
                        new GroupCalculations().CheckGroupsAndGeneratedExcel(standardFilePackage, compareFilePackage, reportFilePacakage,
                            config.SelectedGroups);
                    }
                    if (config.CheckDistribution)
                    {
                        new DistributionCalculations().CheckDistributionAndGeneratedExcel(standardFilePackage, compareFilePackage,
                            reportFilePacakage, config.SelectedDistributions);
                    }
                    new ExcelHelper().WriteToExcel(reportFilePacakage, summary, true);
                    reportFilePacakage.Save();
                }
                Logger.Log("CompareExcel end");
                return new CompareExcelResult
                {
                    IsSuccess = true,
                    ReportFilePath = report
                };
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message);
                Logger.Log("CompareExcel() end");
                return new CompareExcelResult
                {
                    IsSuccess = false,
                    ReportFilePath = null
                };
            }
        }

        public CompareExcelResult AnalyzeSingleFile(Config config, string standardFilePath, string reportFilePath)
        {
            try
            {
                Logger.Log("CompareExcel() start");
                Summary summary = new Summary()
                {
                    FileMatchSummary = new FileMatchSummary() // this will be file summary 
                };
                var report = GetReportFilePath(reportFilePath);
                FileInfo standardFile = new FileInfo(standardFilePath);
                FileInfo reportFile = new FileInfo(report);
                using (ExcelPackage reportFilePacakage = new ExcelPackage(reportFile))
                using (ExcelPackage standardFilePackage = new ExcelPackage(standardFile))
                {
                    summary.FileMatchSummary = GetFileMatchSummary(standardFilePackage);  
                    if(config.CheckColumns)
                    {
                        new ColumnCalculations().CheckColumnsAndGenerateExcelSingleFile(standardFilePackage, reportFilePacakage);
                    }
                    if (config.CheckGroups)
                    {
                        new GroupCalculations().CheckGroupsAndGeneratedExcelSingleFile(standardFilePackage, reportFilePacakage,
                            config.SelectedGroups);
                    }
                    if (config.CheckDistribution)
                    {
                        new DistributionCalculations().CheckDistributionAndGeneratedExcelSingleFile(standardFilePackage,
                            reportFilePacakage, config.SelectedDistributions);
                    }
                    new ExcelHelper().WriteToExcel(reportFilePacakage, summary, false);
                    reportFilePacakage.Save();
                }
                Logger.Log("CompareExcel end");
                return new CompareExcelResult
                {
                    IsSuccess = true,
                    ReportFilePath = report
                };
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message);
                Logger.Log("CompareExcel() end");
                return new CompareExcelResult
                {
                    IsSuccess = false,
                    ReportFilePath = null
                };
            }
        }

        private FileMatchSummary GetFileMatchSummary(ExcelPackage standardFilePackage, ExcelPackage compareFilePackage)
        {
            var fileMatchSummary = new FileMatchSummary();
            var standardFileSheetNames = standardFilePackage.Workbook.Worksheets.Select(x => x.Name).ToList();
            var compareFileSheetNames = compareFilePackage.Workbook.Worksheets.Select(x => x.Name).ToList();

            fileMatchSummary.StandardSheet = GetFileMetaData(standardFilePackage);
            fileMatchSummary.CompareSheet = GetFileMetaData(compareFilePackage);
            fileMatchSummary.StandardSheetInfoSummary = GetSheetInfo(standardFilePackage);
            fileMatchSummary.CompareSheetInfoSummary = GetSheetInfo(compareFilePackage);
            var result = (standardFileSheetNames.All(compareFileSheetNames.Contains)
                                       && standardFileSheetNames.Count == compareFileSheetNames.Count) ?
                                        fileMatchSummary.AreSheetsAllMatch = Constants.ColumnMatchStatus :
                                        fileMatchSummary.AreSheetsAllMatch = Constants.ColumnNoMatchStatus;

            fileMatchSummary.ExcelCompareUtility = GetFormattedDateAsString(DateTime.Now);
            return fileMatchSummary;
        }

        private FileMatchSummary GetFileMatchSummary(ExcelPackage standardFilePackage)
        {
            var fileMatchSummary = new FileMatchSummary();
            fileMatchSummary.StandardSheet = GetFileMetaData(standardFilePackage);
            fileMatchSummary.StandardSheetInfoSummary = GetSheetInfo(standardFilePackage);
            fileMatchSummary.ExcelCompareUtility = GetFormattedDateAsString(DateTime.Now);
            return fileMatchSummary;
        }

        private FileMetaData GetFileMetaData(ExcelPackage excelPackage)
        {
            var fileMetaData = new FileMetaData();
            fileMetaData.Name = excelPackage.File.Name;
            fileMetaData.FileSize = excelPackage.File.Length / 1024;
            fileMetaData.CreatedDate = GetFormattedDateAsString(excelPackage.File.CreationTime);
            fileMetaData.ModifiedDate = GetFormattedDateAsString(excelPackage.File.LastWriteTime);
            return fileMetaData;
        }

        private List<SheetInfoSummary> GetSheetInfo(ExcelPackage FilePackage)
        {
            List<SheetInfoSummary> sheetsInfoSummary = new List<SheetInfoSummary>();
            var fileSheetNames = FilePackage.Workbook.Worksheets.Select(x => x.Name).ToList();
            foreach (var sheetName in fileSheetNames)
            {
                var worksheet = FilePackage.Workbook.Worksheets[sheetName];
                SheetInfoSummary sheetInfoSummary = new SheetInfoSummary();
                sheetInfoSummary.SheetName = sheetName;
                sheetInfoSummary.RowCount = worksheet.Dimension.Rows - 1;
                sheetsInfoSummary.Add(sheetInfoSummary);
            }
            return sheetsInfoSummary;
        }

        private string GetFormattedDateAsString(DateTime dateTime)
        {
            return dateTime.ToString(Constants.DateTimeFormat).Replace(":", "-").Replace("/", "-").Replace("\\", "-");
        }

        private string GetReportFilePath(string reportFilePath)
        {
            if (Directory.Exists(reportFilePath))
            {
                reportFilePath = Path.Combine(reportFilePath,
                    GetFormattedDateAsString(DateTime.Now) + Constants.ExcelExtension);
            }
            Directory.CreateDirectory(Directory.GetParent(reportFilePath).FullName);
            return reportFilePath;
        }
    }
}
