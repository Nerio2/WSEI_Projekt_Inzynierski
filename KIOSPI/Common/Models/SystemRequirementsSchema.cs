using System.Collections.Generic;

namespace Common.Models
{
    public class SystemRequirementsSchema
    {
        public int WindowsVersion { get; set; }
        public IList<RegistryKeySchema> RegistryKeys { get; set; }

        public IEnumerable<SchemaValidationResponse> ValidateMainProperties()
        {
            if (WindowsVersion == 0)
            {
                yield return new SchemaValidationWarning("System requirements: Windows minimum Version is empty.");
            }
        }
    }
}
