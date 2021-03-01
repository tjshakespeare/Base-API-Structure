using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Base_API_Structure.Services
{
    public interface IFileService
    {
        public (string fileType, byte[] archiveData, string archiveName) GetFile(string subDirectory);
        public void SaveFile(List<IFormFile> files, string subDirectory);
        public string ConvertSize(long bytes);
    }
}
