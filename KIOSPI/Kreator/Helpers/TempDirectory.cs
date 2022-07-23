namespace Kreator.Helpers
{
    internal class TempDirectory : IDisposable
    {
        private static string _tempDirectory = "temp";
        public string Path => _tempDirectory;

        public TempDirectory()
        {
            if (Directory.Exists(_tempDirectory))
            {
                Directory.Delete(_tempDirectory, true);
            }
            Directory.CreateDirectory(_tempDirectory);
        }

        public void Dispose()
        {
            if (Directory.Exists(_tempDirectory))
            {
                Directory.Delete(_tempDirectory, true);
            }
        }
    }
}
