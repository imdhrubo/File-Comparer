using System;
using System.IO;
using System.Linq;
using System.Timers;
using FileComparerCore.Utilities;

namespace FileComparer.Web.Helpers
{
    public class FolderDelete
    {
        private readonly string Interval;
        private readonly string DirectoryAge;
        private readonly string Path;
        public FolderDelete(string path, string interval, string directoryAge)
        {
            this.Path = path;
            this.Interval = interval;
            this.DirectoryAge = directoryAge;
            Timer timer = new Timer(double.Parse(Interval))
            {
                Enabled = true,
                AutoReset = true
            };
            timer.Elapsed += Timer_Elapsed;
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            DeleteTempFolders();
        }

        private void DeleteTempFolders()
        {
            try
            {
                Logger.Log("DeleteTempFolders() start");
                Directory.EnumerateDirectories(Path)
               .Select(d => new DirectoryInfo(d))
               .Where(d => d.CreationTime < DateTime.Now.Subtract(TimeSpan.FromMilliseconds(double.Parse(DirectoryAge))))
               .ToList()
               .ForEach(d => d.Delete(true));
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message);
                Logger.Log("DeleteTempFolders() end");
            }
        }
    }
}
