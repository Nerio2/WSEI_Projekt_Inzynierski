using System.Diagnostics;

namespace Instaler.Services
{
    public class FileExecutorService
    {
        public int ExecuteFile(string filePath, string cliArguments = "", int timeoutInMiliseconds = 60 * 60 * 1000)
        {
            Process process = Process.Start(filePath, cliArguments);
            process.WaitForExit(timeoutInMiliseconds);
            return process.ExitCode;
        }
    }
}
