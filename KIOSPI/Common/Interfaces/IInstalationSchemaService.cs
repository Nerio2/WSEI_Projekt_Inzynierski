using Common.Models;

namespace Common.Interfaces
{
    public interface IInstalationSchemaService
    {
        InstallationSchema ReadInstallationSchema(string filePath);
    }
}