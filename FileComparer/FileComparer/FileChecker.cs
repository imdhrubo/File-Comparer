using System;
using System.IO;
namespace FileComparer.ConsoleApp
{
    public class FileChecker
    {
        // validates the existence of the any provided file path passed from CMD argument. In case of 
        // null file path provided, we are considering it valid while checking
        public bool IsValid(FileCompareParam fileCompareParam)
        {
            try
            {
                //This step will do type checking and validation and throw error in case of missing param
                fileCompareParam.CheckParams();
                return (fileCompareParam.StandardFilePath == null
                        || File.Exists(Path.Combine(Directory.GetCurrentDirectory(), fileCompareParam.StandardFilePath)))
                    && (fileCompareParam.CompareFilePath == null
                        || File.Exists(Path.Combine(Directory.GetCurrentDirectory(), fileCompareParam.CompareFilePath)))
                    && (fileCompareParam.ConfigJsonFilePath == null
                        || File.Exists(Path.Combine(Directory.GetCurrentDirectory(), fileCompareParam.ConfigJsonFilePath)));
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        // Validates the existence of both absolute & relative file path
        public bool IsValid(string standardFilePath, string compareFilePath)
        {
            return File.Exists(Path.Combine(Directory.GetCurrentDirectory(), standardFilePath))
                   && File.Exists(Path.Combine(Directory.GetCurrentDirectory(), compareFilePath));
        }
    }
}
