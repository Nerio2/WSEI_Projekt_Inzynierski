using Kreator.Interfaces;
using Kreator.Models;

namespace Kreator.Services
{
    internal class InstalatorService : IInstalatorService
    {
        //todo logger
        public void CreateInstalator(string packagePath, string schemaPath)
        {
            //todo zip
            throw new NotImplementedException();
        }

        public void CreateInstallationPackage(List<ExecutableFilePackage> files, string outputPath)
        {
            //todo instalator
            throw new NotImplementedException();
        }
    }
}
