using Common.Interfaces;
using Common.Services;
using Common.Models;
using Kreator.Interfaces;
using Kreator.Models;
using Kreator.Services;

namespace Kreator
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Hiho");

            //todo logger

            //todo dependency injection?
            Console.WriteLine("Preparing solution...");
            IInstalationSchemaService instalationSchemaService = new InstalationSchemaService();
            IInstallationSchemaValidationService schemaValidationService = new InstallationSchemaValidationService();
            IInstalatorService instalatorService = new InstalatorService();
            Console.WriteLine("Preparing solution - DONE");


            Console.WriteLine("Reading command line arguments...");
            CommandLineSettings cliSettings = new CommandLineSettings(args);
            Console.WriteLine("Reading command line arguments - DONE");
            if (cliSettings.EndApplication)
            {
                if (!string.IsNullOrEmpty(cliSettings.EndApplicationErrorMessage))
                {
                    Console.WriteLine(cliSettings.EndApplicationErrorMessage);
                }
                return;
            }


            Console.WriteLine("Reading instalation schema...");
            InstallationSchema schema = instalationSchemaService.ReadInstallationSchema(cliSettings.SchemaPath);
            Console.WriteLine("Reading instalation schema - DONE");


            Console.WriteLine("Walidating instalation schema...");
            foreach (SchemaValidationResponse response in schemaValidationService.ValidateSchema(schema))
            {
                Console.WriteLine(response); //todo podzial na warning, error, co z nimi robic + logi
            }
            Console.WriteLine("Walidating instalation schema - DONE");


            Console.WriteLine("Preparing zip...");
            List<ExecutableFilePackage> zipFiles = new List<ExecutableFilePackage>();
            Console.WriteLine("Preparing instalation schema for installer..."); //debug
            zipFiles.Add(instalatorService.PrepareInstallerSchemaFile(schema));
            Console.WriteLine("Preparing instalation schema for installer - DONE");//debug
            foreach (var file in schema.FilesToExecute)
            {
                zipFiles.Add(new ExecutableFilePackage(file));
            }
            instalatorService.CreateInstallationPackage(zipFiles, cliSettings.OutputPath); //todo output path
            Console.WriteLine("Preparing zip - DONE");


            Console.WriteLine("Creating installer...");
            instalatorService.CreateInstalator(cliSettings.OutputPath); //todo output path
            Console.WriteLine("Creating installer - DONE");
        }
    }
}