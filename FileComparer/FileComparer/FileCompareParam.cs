using ConsoleCommon;

namespace FileComparer.ConsoleApp
{
    public class FileCompareParam : ParamsObject
    {
        public FileCompareParam(string[] args)
            : base(args)
        { }

        [Switch("StandardFilePath")]
        public string StandardFilePath { get; set; }

        [Switch("CompareFilePath")]
        public string CompareFilePath { get; set; }

        [Switch("ConfigJsonFilePath")]
        public string ConfigJsonFilePath { get; set; }

        [Switch("ReportFilePath")]
        public string ReportFilePath { get; set; }
    }
}
