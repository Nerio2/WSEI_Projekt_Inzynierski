using Common.Models;

namespace Kreator.Models
{
    internal class ExecutableFilePackage
    {
        public string FilePath { get; set; }
        public string? Content { get; set; } = null;
        public bool InMemory { get; set; } = false;

        public ExecutableFilePackage(ExecutableFileSchema schema)
        {
            FilePath = schema.FilePath;
        }

        public ExecutableFilePackage(string filePath)
        {
            FilePath = filePath;
        }

        public ExecutableFilePackage(string name, string content)
        {
            FilePath = name;
            Content = content;
            InMemory = true;
        }

        public void CreateFile(string directory)
        {
            if (Content != null)
            {
                using (var sw = File.CreateText(Path.Combine(directory, FilePath)))
                {
                    sw.Write(Content);
                }
            }
        }
    }
}
