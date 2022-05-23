using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using FileComparerCore.Models;
using FileComparerCore.Utilities;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FileComparer.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileComparerController : Controller
    {
        private readonly IHostingEnvironment HostingEnvironment;
        private const string StandardFilePath = "StandardFilePath";
        private const string ComparerFilePath = "ComparerFilePath";
        private const string ReportFilePath = "ReportFilePath";
        private const string UploadFolderPath = "UploadFolderPath";
        public FileComparerController(IHostingEnvironment hostingEnvironment)
        {
            this.HostingEnvironment = hostingEnvironment;
        }

        // POST api/<controller>
        // This method allows file size upto 100 MB combining two files
        [HttpPost("UploadFiles")]
        [RequestFormLimits(MultipartBodyLengthLimit = 104857600)]
        public async Task<IActionResult> UploadFiles([FromForm] IList<IFormFile> files)
        {
            try
            {
                Logger.Log("UploadFiles() start");
                List<string> filePaths = new List<string>();
                List<ColumnGroup> columnGroups = new List<ColumnGroup>();
                string uploadsFolder = Path.Combine(HostingEnvironment.WebRootPath, "tempfiles", Guid.NewGuid().ToString());
                Directory.CreateDirectory(uploadsFolder);
                HttpContext.Session.SetString(UploadFolderPath, uploadsFolder);
                foreach (IFormFile source in files)
                {
                    string filePath = Path.Combine(uploadsFolder, source.FileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        source.CopyTo(stream);
                    }
                    filePaths.Add(filePath);
                }
                if (filePaths.Count == 2)
                {
                    HttpContext.Session.SetString(StandardFilePath, filePaths[0]);
                    HttpContext.Session.SetString(ComparerFilePath, filePaths[1]);
                    columnGroups = await PopulateUniqueColumnsList(HttpContext.Session.GetString(StandardFilePath), HttpContext.Session.GetString(ComparerFilePath));
                }
                else
                {
                    HttpContext.Session.SetString(StandardFilePath, filePaths[0]);
                    columnGroups = await PopulateUniqueColumnsList(HttpContext.Session.GetString(StandardFilePath));
                }
                Logger.Log("UploadFiles() end");
                return Ok(columnGroups);
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message);
                Logger.Log("UploadFiles() end");
                return BadRequest();
            }
        }

        [HttpPost("Generate")]
        public IActionResult GenerateReport([FromBody] Config config)
        {
            try
            {
                Logger.Log("GenerateReport() start");
                CompareExcelResult result = new CompareExcelResult();
                var standardFile = HttpContext.Session.GetString(StandardFilePath);
                var comparerFile = HttpContext.Session.GetString(ComparerFilePath);
                if (!string.IsNullOrEmpty(comparerFile))
                {
                    result = new FileComparerCore.FileComparer().CompareExcel(config, standardFile, comparerFile, Path.Combine(HostingEnvironment.WebRootPath, HttpContext.Session.GetString(UploadFolderPath)));
                }
                else
                {
                    result = new FileComparerCore.FileComparer().AnalyzeSingleFile(config, standardFile, Path.Combine(HostingEnvironment.WebRootPath, HttpContext.Session.GetString(UploadFolderPath)));
                }
                if (result.IsSuccess)
                {
                    HttpContext.Session.SetString(ReportFilePath, result.ReportFilePath);
                    Logger.Log("GenerateReport() end");
                    return Ok(true);
                }
                else
                {
                    Logger.Log("GenerateReport() end");
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message);
                Logger.Log("GenerateReport() end");
                return BadRequest();
            }
        }

        [HttpGet("Download")]
        public virtual IActionResult Download()
        {
            try
            {
                Logger.Log("Download() start");
                var bytes = GetFileAsBytes();
                HttpContext.Session.SetString(StandardFilePath, "");
                HttpContext.Session.SetString(ComparerFilePath, "");
                Logger.Log("Download() end");
                return File(bytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", Path.Combine("Report", DateTime.Now.ToString(Constants.DateTimeFormat)
                    .Replace(":", "-").Replace("/", "-").Replace("\\", "-") + Constants.ExcelExtension));
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message);
                Logger.Log("Download() end");
                return BadRequest();
            }
        }

        private byte[] GetFileAsBytes()
        {
            string file = HttpContext.Session.GetString(ReportFilePath);
            string fullPath = Path.Combine(HostingEnvironment.WebRootPath, HttpContext.Session.GetString(UploadFolderPath), file);
            using (FileStream stream = new FileStream(fullPath, FileMode.Open, FileAccess.Read))
            {
                byte[] bytes = System.IO.File.ReadAllBytes(fullPath);          //Create a byte array of file stream length
                stream.Read(bytes, 0, System.Convert.ToInt32(stream.Length)); //Read block of bytes from stream into the byte array
                stream.Close();                                              //Close the File Stream
                return bytes;                                               //return the byte data
            }
        }

        private async Task<List<ColumnGroup>> PopulateUniqueColumnsList(string standardFilePath, string comparerFilePath = null)
        {
            List<ColumnGroup> columnGroups = new List<ColumnGroup>();
            if (!string.IsNullOrEmpty(comparerFilePath))
            {
                columnGroups = await new ExcelHelper().GetColumnNames(standardFilePath, comparerFilePath);
            }
            else
            {
                columnGroups = await new ExcelHelper().GetColumnNames(standardFilePath);
            }
            return columnGroups;
        }
    }
}
