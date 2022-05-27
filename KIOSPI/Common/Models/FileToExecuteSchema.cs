namespace Common.Models
{
    public class FileToExecuteSchema
    {
        public string FileName { get; set; }
        public string? DefaultCommandLineArgumeters { get; set; }
        public string? FilePath { get; set; }
        public int Order { get; set; }

        public IEnumerable<SchemaValidationResponse> ValidateMainProperties()
        {
            if (string.IsNullOrEmpty(FileName))
            {
                yield return new SchemaValidationError("Files to execute: FileName is missing! Please fill name of the file you want to execute.");
            }
        }
    }
}
