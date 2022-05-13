namespace Common.Models
{
    public class PrerequisiteSchema
    {
        public string UpgradeCode { get; set; }
        public string Name { get; set; }
        public Version RequiredVersion { get; set; }
    }
}
