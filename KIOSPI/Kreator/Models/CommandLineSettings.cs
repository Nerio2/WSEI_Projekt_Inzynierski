namespace Kreator.Models
{
    public class CommandLineSettings
    {
        public bool EndApplication { get; set; } = false;
        public string? EndApplicationErrorMessage { get; set; }
        public string SchemaPath { get => schemaPath.value; }
        public string OutputPath { get => outputPath.value; }


        private CommandLineArgument<string> schemaPath = new CommandLineArgument<string>(new string[] { "-s", "--schema", "/schema" }, true,
            "Path to the installation schema.", "Schema path");

        private CommandLineArgument<string> outputPath = new CommandLineArgument<string>(new string[] { "-o", "--output", "/output" }, false,
            "Output path for the created installer. Default: Current_Directory/<Installer_Name>V<Version>", "Output path");

        private CommandLineArgument<bool> help = new CommandLineArgument<bool>(new string[] { "-h", "--help", "/help" }, false,
            "Displays help.", "Help", false);

        private List<ICommandLineArgument> supportedArgs
            => new List<ICommandLineArgument>() { schemaPath, outputPath, help };

        public CommandLineSettings(string[] args)
        {
            if (help.IsInvoked(args))
            {
                //todo help text
                Console.WriteLine("~~~ KIOSPI ~~~");
                Console.WriteLine("Invokers: \t Description:");
                foreach (var supportedArg in supportedArgs)
                {
                    Console.WriteLine(supportedArg.ToString());
                }
                EndApplication = true;
                return;
            }


            foreach (var arg in supportedArgs)
            {
                if (arg.IsRequired && !arg.IsInvoked(args))
                {
                    EndApplication = true;
                    EndApplicationErrorMessage = $"Missing Cli parameter: {arg.ArgumentName}";
                    return;
                }
            }

            foreach (var arg in supportedArgs)
            {
                arg.ReadValue(args);
            }

        }
    }
}
