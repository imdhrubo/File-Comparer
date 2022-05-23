using System;
using System.IO;
using FileComparerCore.Models;
using FileComparerCore.Utilities;
using Newtonsoft.Json;

namespace FileComparer.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                if (args.Length == 0)
                {
                    ProcessWithNoCmdArguments();
                }
                else
                {
                    ProcessWithCmdArguments(args);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        // Processes the files when the file path names are not provided from CMD , in this case 
        // the method reads from the default config json file path
        private static void ProcessWithNoCmdArguments()
        {
            Config config = JsonConvert.DeserializeObject<Config>(File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), Constants.ConfigJsonFileDefaultName)));
            var fileComparer = new FileComparerCore.FileComparer();
            var fileChecker = new FileChecker();

            var isFileValid = fileChecker.IsValid(config.StandardFilePath, config.CompareFilePath);
            if (isFileValid)
            {
                var result = fileComparer.CompareExcel(config, config.StandardFilePath,
                    config.CompareFilePath, config.ReportFilePath);
                if (result.IsSuccess)
                {
                    Console.WriteLine(Constants.SuccessMessage);
                }
                else
                {
                    Console.WriteLine(Constants.ErrorMessage);
                }
            }
            else
            {
                Console.WriteLine(Constants.InvalidFileMessage);
            }
        }

        // Processes the files when the file path names are provided from CMD 
        private static void ProcessWithCmdArguments(string[] args)
        {
            //This step will automatically cast the string args to a strongly typed object:
            FileCompareParam fileCompareParam = new FileCompareParam(args);

            var fileChecker = new FileChecker();
            var isFileValid = fileChecker.IsValid(fileCompareParam);
            if (isFileValid)
            {
                var config = new Config();
                if (string.IsNullOrWhiteSpace(fileCompareParam.ConfigJsonFilePath))
                {
                    config = JsonConvert.DeserializeObject<Config>(File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), Constants.ConfigJsonFileDefaultName)));
                }
                else
                {
                    config = JsonConvert.DeserializeObject<Config>(File.ReadAllText(fileCompareParam.ConfigJsonFilePath));
                }
                var result = ProcessFiles(config, fileCompareParam);
                if (result.IsSuccess)
                {
                    Console.WriteLine(Constants.SuccessMessage);
                }
                else
                {
                    Console.WriteLine(Constants.ErrorMessage);
                }
            }
            else
            {
                Console.WriteLine(Constants.InvalidFileMessage);
            }
        }

        private static CompareExcelResult ProcessFiles(Config config, FileCompareParam fileCompareParam)
        {
            var reportFilePath = fileCompareParam.ReportFilePath ?? config.ReportFilePath;
            if (fileCompareParam.StandardFilePath != null && fileCompareParam.CompareFilePath != null)
            {
                return ProcessFiles(config, fileCompareParam.StandardFilePath, fileCompareParam.CompareFilePath, reportFilePath);
            }
            return ProcessFiles(config, reportFilePath);
        }

        // Processes the Excel files when they are provided from CMD argument
        private static CompareExcelResult ProcessFiles(Config config, string standardFilePath, string compareFilePath, string reportFilePath)
        {
            var fileComparer = new FileComparerCore.FileComparer();
            return fileComparer.CompareExcel(config, standardFilePath,
                                       compareFilePath, reportFilePath);
        }

        // Processes the Excel files when they are read from the Config Json File passed as CMD argument
        private static CompareExcelResult ProcessFiles(Config config, string reportFilePath)
        {
            var fileComparer = new FileComparerCore.FileComparer();
            return fileComparer.CompareExcel(config, config.StandardFilePath,
                                    config.CompareFilePath, reportFilePath);
        }
    }
}
