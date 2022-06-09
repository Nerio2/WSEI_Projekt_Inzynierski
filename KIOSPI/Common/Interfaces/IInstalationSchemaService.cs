using Common.Models;

namespace Common.Interfaces
{
    public interface IInstalationSchemaService
    {
        public InstallationSchema ReadInstallationSchema(string filePath);
    }
}