using Common.Interfaces;
using Common.Models;
using Newtonsoft.Json;

namespace Common.Services
{
    internal class InstalationSchemaService : IInstalationSchemaService
    {
        public InstallationSchema ReadInstallationSchema(string filePath)
        {
            return JsonConvert.DeserializeObject<InstallationSchema>(filePath);
        }
    }
}
