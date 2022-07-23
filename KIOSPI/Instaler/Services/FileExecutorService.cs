using System.Diagnostics;
using System.IO;

namespace Instaler.Services
{
    public class FileExecutorService
    {
        private string _powershellScriptExtension = ".ps1";
        public int ExecuteFile(string filePath, string cliArguments = "", int timeoutInMiliseconds = 60 * 60 * 1000)
        {
            Process process;
            if (Path.GetExtension(filePath).Equals(_powershellScriptExtension))
            {
                process = Process.Start(@"powershell.exe", "&'" + filePath + "' " + cliArguments);
            }
            else
            {
                process = Process.Start(filePath, cliArguments);
            }
            process.WaitForExit(timeoutInMiliseconds);
            return process.ExitCode;
        }
    }
}
