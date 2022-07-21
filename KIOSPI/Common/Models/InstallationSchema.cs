using System;
using System.Collections.Generic;

namespace Common.Models
{
    //TODO: InstallationSchemaBuilder?
    public class InstallationSchema
    {
        public static string DefaultFileName = "InstallationSchema.json";
        public string Name { get; set; }
        public string Author { get; set; }
        public string Description { get; set; }
        public Version InstallerVersion { get; set; }
        public Version SchemaVersion { get; set; }
        public IList<PrerequisiteSchema> Prerequisites { get; set; }
        public SystemRequirementsSchema SystemRequirements { get; set; }
        public IList<ExecutableFileSchema> FilesToExecute { get; set; }

        public IEnumerable<SchemaValidationResponse> ValidateMainProperties()
        {
            if (string.IsNullOrEmpty(Name))
            {
                yield return new SchemaValidationWarning("Schema Name is empty");
            }

            if (string.IsNullOrEmpty(Author))
            {
                yield return new SchemaValidationWarning("Schema Author is empty");
            }

            if (InstallerVersion == null)
            {
                yield return new SchemaValidationError("Installer version is missing! Please fill proper version of product you want to install.");
            }
        }
    }
}
