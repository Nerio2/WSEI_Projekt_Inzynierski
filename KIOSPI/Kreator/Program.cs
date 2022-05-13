using Common.Models;
using Newtonsoft.Json;
using System.Text.Json;

namespace Kreator
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var schema = new InstallationSchema()
            {
                Author = "Autor",
                Description = "Opis",
                InstallerVersion = new Version(1, 2, 3, 4),
                SchemaVersion = new Version(2, 1, 3, 1),
                Name = "Nazwa",
                Prerequisites = new List<PrerequisiteSchema>()
                {
                    new PrerequisiteSchema()
                    {
                        Name="Nazwa",
                        RequiredVersion=new Version(1,5,3,6),
                        UpgradeCode="{f3d05053-6f2b-4dc4-8a58-7229f0bfaf04}"
                    },
                    new PrerequisiteSchema()
                    {
                        Name="PrzykladowyProgram",
                        RequiredVersion=new Version(6,8,3,0),
                        UpgradeCode="{1da34eb4-4ea5-422d-a66c-8ba80ae2b5c1}"
                    }
                },
                FilesToExecute = new List<FileToExecuteSchema>()
                {
                    new FileToExecuteSchema()
                    {
                        Order=1,
                        FileName="Installer.exe",
                        DefaultCommandLineArgumeters="--silent -r -u",
                        FilePath="C:/build/"
                    },
                    new FileToExecuteSchema()
                    {
                        Order=2,
                        FileName="SomeScript.ps1",
                        DefaultCommandLineArgumeters="-FirstRun True -Project Installer",
                        FilePath="C:/scripts/"
                    }
                },
                SystemRequirements = new SystemRequirementsSchema()
                {
                    WindowsVersion = "10",
                    RegistryKeys = new List<RegistryKeySchema>()
                    {
                        new RegistryKeySchema()
                        {
                            Path=@"HKEY_CURRENT_USER\Microsoft\Windows\CurrentVersion\Explorer\StartupApproved\Run",
                            Key="OneDriveSetup",
                            Value="true"
                        },
                        new RegistryKeySchema()
                        {
                            Path=@"HKEY_LOCAL_MACHINE\SOFTWARE\Clients\StartMenuInternet\Google Chrome\InstallInfo",
                            Key="IconsVisible",
                            Value="1"
                        }
                    }
                }
            };
            string json = JsonConvert.SerializeObject(schema);
        }
    }
}