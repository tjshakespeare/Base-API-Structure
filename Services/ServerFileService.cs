using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Threading.Tasks;

namespace Base_API_Structure.Services
{
    public class ServerFileService : IFileService
    {
        private readonly string ROOT_DIRECTORY = "D:\\webroot\\";
        public void SaveFile(List<IFormFile> files, string subDirectory)
        {
            subDirectory ??= string.Empty;
            var target = Path.Combine(ROOT_DIRECTORY, subDirectory);

            Directory.CreateDirectory(target);

            files.ForEach(async file =>
            {
                if (file.Length <= 0) return;
                var filePath = Path.Combine(target, file.FileName);
                using var stream = new FileStream(filePath, FileMode.Create);
                await file.CopyToAsync(stream);
            });
        }

        public string ConvertSize(long bytes)
        {
            var fileSize = new decimal(bytes);
            var kilobyte = new decimal(1024);
            var megabyte = new decimal(1024 * 1024);
            var gigabyte = new decimal(1024 * 1024 * 1024);

            return fileSize switch
            {
                var _ when fileSize < kilobyte => $"Less then 1KB",
                var _ when fileSize < megabyte => $"{Math.Round(fileSize / kilobyte, 0, MidpointRounding.AwayFromZero):##,###.##}KB",
                var _ when fileSize < gigabyte => $"{Math.Round(fileSize / megabyte, 2, MidpointRounding.AwayFromZero):##,###.##}MB",
                var _ when fileSize >= gigabyte => $"{Math.Round(fileSize / gigabyte, 2, MidpointRounding.AwayFromZero):##,###.##}GB",
                _ => "N/A",
            };
        }

        public (string fileType, byte[] archiveData, string archiveName) GetFile(string subDirectory)
        {
            var zipName = $"{DateTime.Now:dd_MM-yyyy-HH_mm_ss}.zip";

            var files = Directory.GetFiles(Path.Combine(ROOT_DIRECTORY, subDirectory)).ToList();

            using var memoryStream = new MemoryStream();
            using (var archive = new ZipArchive(memoryStream, ZipArchiveMode.Create, true))
            {
                files.ForEach(file =>
                {
                    var theFile = archive.CreateEntry(file);
                    using var streamWriter = new StreamWriter(theFile.Open());
                    streamWriter.Write(File.ReadAllText(file));

                });
            }

            return ("application/zip", memoryStream.ToArray(), zipName);
        }
    }
}
