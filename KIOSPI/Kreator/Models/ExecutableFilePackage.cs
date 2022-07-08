using Common.Models;

namespace Kreator.Models
{
    internal class ExecutableFilePackage
    {
        public string FilePath { get; set; }

        public ExecutableFilePackage(ExecutableFileSchema schema)
        {
            FilePath = schema.FilePath;
        }
    }
}
