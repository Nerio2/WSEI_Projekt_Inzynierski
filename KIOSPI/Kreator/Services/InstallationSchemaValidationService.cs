using Common.Models;
using Kreator.Interfaces;

namespace Kreator.Services
{
    public class InstallationSchemaValidationService : IInstallationSchemaValidationService
    {
        //todo logger

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
            foreach (PrerequisiteSchema prerequisite in prerequisites)
                foreach (SchemaValidationResponse prerequisiteMainPropertiesValidation in prerequisite.ValidateMainProperties())
                    yield return prerequisiteMainPropertiesValidation;
        }

        private IEnumerable<SchemaValidationResponse> ValidateSystemRequirements(SystemRequirementsSchema systemRequirements)
        {
            foreach (SchemaValidationResponse systemRequirementsValidation in systemRequirements.ValidateMainProperties())
                yield return systemRequirementsValidation;
            foreach (SchemaValidationResponse registryKeysValidationResponse in ValidateRegistryKeys(systemRequirements.RegistryKeys))
                yield return registryKeysValidationResponse;
        }

        private IEnumerable<SchemaValidationResponse> ValidateRegistryKeys(IList<RegistryKeySchema> registryKeys)
        {
            foreach (RegistryKeySchema registryKey in registryKeys)
                foreach (SchemaValidationResponse registryKeyPropertiesValidation in registryKey.ValidateMainProperties())
                    yield return registryKeyPropertiesValidation;
        }

        private IEnumerable<SchemaValidationResponse> ValidateFilesToExecute(IList<FileToExecuteSchema> files)
        {
            foreach (FileToExecuteSchema file in files)
                foreach (SchemaValidationResponse fileMainPropertiesValidation in file.ValidateMainProperties())
                    yield return fileMainPropertiesValidation;
        }
    }
}
