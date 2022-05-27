using Kreator.Models;
using Common.Models;

namespace Kreator.Interfaces
{
    public interface IInstallationSchemaValidationService
    {
        public List<SchemaValidationResponse> ValidateSchema(InstallationSchema installationSchema);
    }
}