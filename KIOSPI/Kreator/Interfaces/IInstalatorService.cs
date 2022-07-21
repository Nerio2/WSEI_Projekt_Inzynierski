using Common.Models;
using Kreator.Models;

namespace Kreator.Interfaces
{
    internal interface IInstalatorService
    {
        void CreateInstallationPackage(List<ExecutableFilePackage> files, string outputPath);

        void CreateInstalator(string outputPath);

        ExecutableFilePackage PrepareInstallerSchemaFile(InstallationSchema schemaBase);
    }
}