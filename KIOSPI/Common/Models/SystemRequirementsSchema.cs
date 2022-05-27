namespace Common.Models
{
    public class SystemRequirementsSchema
    {
        public string WindowsVersion { get; set; }
        public IList<RegistryKeySchema>? RegistryKeys { get; set; }

        public IEnumerable<SchemaValidationResponse> ValidateMainProperties()
        {
            if (string.IsNullOrEmpty(WindowsVersion))
            {
                yield return new SchemaValidationWarning("System requirements: Windows minimum Version is empty.");
            }
        }
    }
}
