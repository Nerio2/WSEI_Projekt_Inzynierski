using System;
using System.IO;
using System.Linq;

namespace Instaler.logger
{
    public class InstallerLogger
    {
        private string _runtimeLogFileName;

        private const string _runtimeLogFileNamePrefix = "KIOSPI_";

        private const string _logFileRuntimeExtension = ".log.temp";

        private const string _logFileExtension = ".log";

        private const string _defaultDrive = @"C:\";

        private string GetRuntimeFullFilePath()
            => GetLogFilePath() + _runtimeLogFileName + _logFileRuntimeExtension;

        private string GetFullFilePath(string logFileName)
            => GetLogFilePath() + logFileName + _logFileExtension;

        private string GetLogFilePath()
            => GetDrive() + @"Users\" + Environment.UserName + @"\AppData\Local\Temp\KIOSPI\";

        private string GetDrive()
            => Environment.GetLogicalDrives().Any(drive => drive.Equals(_defaultDrive)) ? _defaultDrive : Environment.GetLogicalDrives()[0];

        private string _logMessagePrefix => DateTime.Now.ToString("yyyy-MM-dd_HH:mm:ss:fff") + " ";
        private string _logMessageSuffix = Environment.NewLine;

        public InstallerLogger()
        {
            _runtimeLogFileName = _runtimeLogFileNamePrefix + Program.InstallerActualAction + "_" + DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss-fff");
            string path = GetRuntimeFullFilePath();
            Directory.CreateDirectory(Path.GetDirectoryName(path));
            var fs = File.Create(path);
            fs.Dispose();
        }

        ~InstallerLogger()
        {
            string logFileName = _runtimeLogFileName.Replace(_runtimeLogFileNamePrefix, Program.AppName + "_");
            File.Move(GetRuntimeFullFilePath(), GetFullFilePath(logFileName));
        }

        private void Log(string message)
        {
            File.AppendAllText(GetRuntimeFullFilePath(), _logMessagePrefix + message + _logMessageSuffix);
        }

        public void LogInfo(string message)
        {
            Log("[INFO] " + message);
        }

        public void LogWarning(string message)
        {
            Log("[WARNING] " + message);
        }

        public void LogError(string message, Exception exception = null)
        {
            if (exception != null)
            {
                message += exception.Message + Environment.NewLine + exception.StackTrace;
            }
            Log("[ERROR] " + message);
        }
    }
}
