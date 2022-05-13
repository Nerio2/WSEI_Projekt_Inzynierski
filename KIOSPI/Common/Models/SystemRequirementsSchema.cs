namespace Common.Models
{
    public class SystemRequirementsSchema
    {
        public string WindowsVersion { get; set; }
        public IList<RegistryKeySchema>? RegistryKeys { get; set; }
    }
}
