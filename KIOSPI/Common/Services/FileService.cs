
using Common.Interfaces;

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
