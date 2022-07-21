using Common.Models;
using Kreator.Helpers;
using Kreator.Interfaces;
using Kreator.Models;
using System.IO.Compression;

namespace Kreator.Services
{
    internal class InstalatorService : IInstalatorService
    {
        private static string _zipName = "package.zip";
        
        //todo logger
        public void CreateInstalator(string outputPath)
        {
            //todo instalator
            throw new NotImplementedException();
        }

        public void CreateInstallationPackage(List<ExecutableFilePackage> files, string outputPath)
        {
            using (var tempDirectory = new TempDirectory())
            {
                foreach (var file in files)
                {
                    if (file.InMemory)
                    {
                        //todo retest
                        file.CreateFile(tempDirectory.Path);
                    }
                    else
                    {
                        File.Copy(file.FilePath, Path.Combine(tempDirectory.Path, Path.GetFileName(file.FilePath)));
                    }

                }
                ZipFile.CreateFromDirectory(tempDirectory.Path, Path.Combine(outputPath, _zipName));
            }
        }

        public ExecutableFilePackage PrepareInstallerSchemaFile(InstallationSchema schemaBase)
        {
            //TODO installation schema for installer
            throw new NotImplementedException();
        }
    }
}
