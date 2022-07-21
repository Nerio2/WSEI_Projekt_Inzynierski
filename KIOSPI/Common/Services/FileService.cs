
using Common.Interfaces;
using System.IO;

namespace Common.Services
{
    public class FileService : IFileService
    {
        public bool Exists(string filePath)
        {
            return File.Exists(filePath);
        }
    }
}
