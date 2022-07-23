using Common.Models;
using Instaler.Helpers;
using Instaler.logger;
using Instaler.Services;
using System;
using System.IO;
using System.Reflection;

namespace Instaler
{
    public static class Program
    {
        public static InstallerAction InstallerActualAction;

        private static InstallerLogger _logger;

        public static string AppName = "KIOSPI";
        public enum InstallerAction
        {
            Installation,
            Uninstallation
        }

        public enum ExitCodes
        {
            Sucess = 0,

            UnexpectedError = 100,
            MissingFile = 101,
            WindowsVersionTooLow = 101,
            RegistryEntryRequired = 102,
            PrerequisiteRequired = 103
        }

        public static void Main(string[] args)
        {
            _logger = new InstallerLogger();

            _logger.LogInfo("Starting installation...");

            //todo dependency injection?
            PackageService packageService = new PackageService();
            FileExecutorService fileExecutorService = new FileExecutorService();
            IRegistrySearchHelper registrySearchHelper = new RegistrySearchHelper();
            IWindowsVersionsHelper windowsVersionsHelper = new WindowsVersionsHelper();

            _logger.LogInfo("Unpacking installation package...");
            string package = packageService.Unpack();
            _logger.LogInfo("Installation package path: " + package);
            _logger.LogInfo("Unpacking installation package - DONE");

            _logger.LogInfo("Reading installation schema...");

            string schemaPath = Path.Combine(package, InstallationSchema.DefaultFileName);

            _logger.LogInfo("Loading Newtonsoft dll...");
            var DLL = Assembly.LoadFile(Path.Combine(package, "Newtonsoft.Json.dll"));
            var type = DLL.GetType("Newtonsoft.Json.JsonConvert");
            var deserializeMethod = type.GetMethod("DeserializeObject", new Type[] { typeof(string), typeof(Type) });
            InstallationSchema schema = (InstallationSchema)deserializeMethod
                .Invoke(null, new object[] { File.ReadAllText(schemaPath), typeof(InstallationSchema) });
            _logger.LogInfo("Loading Newtonsoft dll - DONE");

            AppName = schema.Name;
            _logger.LogInfo("Reading installation schema - DONE");

            if (schema.SystemRequirements != null)
            {
                _logger.LogInfo("Validating system requirements..."); //todo retest
                WindowsVersion requiredWindows = windowsVersionsHelper.GetWindowsVersionFromBuildVersion(schema.SystemRequirements.WindowsVersion);
                _logger.LogInfo("Windows required version: " + requiredWindows.ToString());
                WindowsVersion currentWindows = windowsVersionsHelper.GetWindowsVersionFromBuildVersion(registrySearchHelper.GetWindowsBuildNumber());
                _logger.LogInfo("Windows current version: " + currentWindows.ToString());
                if (requiredWindows > currentWindows)
                {
                    _logger.LogError("Windows version is too low.");
                    Environment.ExitCode = (int)ExitCodes.WindowsVersionTooLow;
                    return;
                }
                else
                {
                    _logger.LogInfo("Windows version is ok.");
                }

                foreach (RegistryKeySchema reg in schema.SystemRequirements.RegistryKeys)
                {
                    if (!reg.Value.Equals(registrySearchHelper.GetHKLMRegistryValue(reg.Path, reg.Key)))
                    {
                        _logger.LogError(reg.ErrorMessage);
                        Environment.ExitCode = (int)ExitCodes.RegistryEntryRequired;
                        return;
                    }
                }
                _logger.LogInfo("Validating system requirements - DONE");
            }
            if (schema.Prerequisites != null)
            {
                _logger.LogInfo("Checking prerequisites..."); //todo retest
                foreach (PrerequisiteSchema prerequisite in schema.Prerequisites)
                {
                    Version currentVersion = registrySearchHelper.GetProgramVersion(prerequisite.UpgradeCode);
                    if (currentVersion == null || prerequisite.RequiredVersion > currentVersion)
                    {
                        string program = string.IsNullOrEmpty(prerequisite.Name) ? prerequisite.UpgradeCode : prerequisite.Name;
                        _logger.LogError($"Program {program} v {prerequisite.RequiredVersion} is required.");
                        Environment.ExitCode = (int)ExitCodes.PrerequisiteRequired;
                        return;
                    }
                }
                _logger.LogInfo("Checking prerequisites - DONE");
            }

            _logger.LogInfo("Validating installation files...");
            foreach (ExecutableFileSchema file in schema.FilesToExecute)
            {
                if (!File.Exists(Path.Combine(package, file.FilePath)))
                {
                    _logger.LogError($"File {file.FilePath} does not exists in the package!");
                    Environment.ExitCode = (int)ExitCodes.MissingFile;
                    return;
                }
            }
            _logger.LogInfo("Validating installation files - DONE");

            _logger.LogInfo("Executing installation files...");
            foreach (ExecutableFileSchema file in schema.FilesToExecute)
            {
                _logger.LogInfo($"Starting {file.FilePath}...");
                int exitCode = fileExecutorService.ExecuteFile(Path.Combine(package, file.FilePath), file.DefaultCommandLineArgumeters);
                if (exitCode != 0)
                {
                    _logger.LogError($"{file.FilePath} installation failed with exit code {exitCode}.");
                    //todo obsluga bledow
                }
                else
                {
                    _logger.LogInfo($"{file.FilePath} executed sucessfully.");
                }
            }
            _logger.LogInfo("Executing installation files - DONE");


            _logger.LogInfo("Installation - DONE");
            _logger.LogInfo("Installation exit code " + Environment.ExitCode);

            //todo cleanup?
            return;
        }
    }
}
