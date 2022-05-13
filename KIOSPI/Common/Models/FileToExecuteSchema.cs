namespace Common.Models
{
    public class FileToExecuteSchema
    {
        public string FileName { get; set; }
        public string? DefaultCommandLineArgumeters { get; set; }
        public string? FilePath { get; set; }
        public int Order { get; set; }
    }
}
