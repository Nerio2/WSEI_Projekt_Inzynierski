using Common.Models;
using Kreator.Helpers;
using Kreator.Interfaces;
using Kreator.Models;
using System.Diagnostics;
using System.IO.Compression;

namespace Kreator.Services
{
    internal class InstalatorService : IInstalatorService
    {
        private static string _zipName = "package.zip";

        private static string _msBuildPath = @"C:\Program Files\Microsoft Visual Studio\2022\Community\Msbuild\Current\Bin\MSBuild.exe";
        private static string _sollutionPath = @"C:\WSEI_Projekt_Inzynierski\KIOSPI\KIOSPI.sln";
        private static string _msBuildCliArguments = "-p:configuration=InstallerRelease";

        private static string _installerOutputPath = @"C:\WSEI_Projekt_Inzynierski\KIOSPI\Instaler\bin\Release\";

        //todo logger
        public void CreateInstalator(string outputPath)
        {
            Process process = Process.Start(_msBuildPath, String.Join(' ', _sollutionPath, _msBuildCliArguments));
            if (process.ExitCode == 0)
            {
                File.Move(Directory.GetFiles(_installerOutputPath, "*.exe")[0], outputPath);
            }
            //todo obsluga bledow
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
