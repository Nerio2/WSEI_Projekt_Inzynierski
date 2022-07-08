using Common.Interfaces;
using Common.Services;
using Common.Models;
using Kreator.Interfaces;

namespace Kreator.Services
{
    public class InstallationSchemaValidationService : IInstallationSchemaValidationService
    {
        //todo logger
        private IFileService _fileService = new FileService();

        public List<SchemaValidationResponse> ValidateSchema(InstallationSchema installationSchema)
        {
            List<SchemaValidationResponse> validationResults = new List<SchemaValidationResponse>();
            validationResults.AddRange(ValidateMainSchema(installationSchema));
            validationResults.AddRange(ValidatePrerequisites(installationSchema.Prerequisites));
            validationResults.AddRange(ValidateSystemRequirements(installationSchema.SystemRequirements));
            validationResults.AddRange(ValidateFilesToExecute(installationSchema.FilesToExecute));
            return validationResults;
        }

        private IEnumerable<SchemaValidationResponse> ValidateMainSchema(InstallationSchema schema)
        {
            return schema.ValidateMainProperties();
        }

        private IEnumerable<SchemaValidationResponse> ValidatePrerequisites(IList<PrerequisiteSchema> prerequisites)
        {
            if (prerequisites != null)
                foreach (PrerequisiteSchema prerequisite in prerequisites)
                    foreach (SchemaValidationResponse prerequisiteMainPropertiesValidation in prerequisite.ValidateMainProperties())
                        yield return prerequisiteMainPropertiesValidation;
        }

        private IEnumerable<SchemaValidationResponse> ValidateSystemRequirements(SystemRequirementsSchema systemRequirements)
        {
            if (systemRequirements != null)
            {
                foreach (SchemaValidationResponse systemRequirementsValidation in systemRequirements.ValidateMainProperties())
                    yield return systemRequirementsValidation;
                foreach (SchemaValidationResponse registryKeysValidationResponse in ValidateRegistryKeys(systemRequirements.RegistryKeys))
                    yield return registryKeysValidationResponse;
            }
        }

        private IEnumerable<SchemaValidationResponse> ValidateRegistryKeys(IList<RegistryKeySchema> registryKeys)
        {
            if (registryKeys != null)
                foreach (RegistryKeySchema registryKey in registryKeys)
                    foreach (SchemaValidationResponse registryKeyPropertiesValidation in registryKey.ValidateMainProperties())
                        yield return registryKeyPropertiesValidation;
        }

        private IEnumerable<SchemaValidationResponse> ValidateFilesToExecute(IList<ExecutableFileSchema> files)
        {
            List<int> orderNumbers = new List<int>(files.Count);
            foreach (ExecutableFileSchema file in files)
            {
                if (orderNumbers.Any(number => number == file.Order))
                {
                    yield return new SchemaValidationError($"Files to execute: Order <{file.Order}> is duplicated!");
                }
                orderNumbers.Add(file.Order);

                if (string.IsNullOrEmpty(file.FilePath) || !_fileService.Exists(file.FilePath))
                {
                    yield return new SchemaValidationError($"Files to execute: FilePath <{file.FilePath}> does not exists!");
                }
            }
        }
    }
}
