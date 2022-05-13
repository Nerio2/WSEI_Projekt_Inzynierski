namespace Common.Models
{
    //TODO: InstallationSchemaBuilder
    public class InstallationSchema
    {
        public string Name { get; set; }
        public string Author { get; set; }
        public string Description { get; set; }
        public Version InstallerVersion { get; set; }
        public Version SchemaVersion { get; set; }
        public IList<PrerequisiteSchema> Prerequisites { get; set; }
        public SystemRequirementsSchema SystemRequirements { get; set; } 
        public IList<FileToExecuteSchema> FilesToExecute { get; set; } 
    }
}
