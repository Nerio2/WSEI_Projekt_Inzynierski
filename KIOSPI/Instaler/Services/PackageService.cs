using System;
using System.IO;
using System.IO.Compression;

namespace Instaler.Services
{
    internal class PackageService
    {
        private static string _unpackDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), Path.Combine(Program.AppName, "package"));

        public string Unpack(string packageDirectory = "package.zip")
        {
            try { Directory.Delete(_unpackDirectory, true); } catch (Exception) { }
            ZipFile.ExtractToDirectory(packageDirectory, _unpackDirectory);
            return _unpackDirectory;
        }
    }
}
