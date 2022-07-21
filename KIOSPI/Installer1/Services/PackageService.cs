using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Compression;

namespace Instaler.Services
{
    internal class PackageService
    {
        private static string _unpackDirectory = "";

        public void Unpack(string packageDirectory = "package.zip")
        {
            ZipFile.ExtractToDirectory(packageDirectory, _unpackDirectory);
        }
    }
}
