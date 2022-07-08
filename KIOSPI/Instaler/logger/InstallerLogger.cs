using System;
using System.IO;
using System.Linq;

namespace Instaler.logger
{
    public class InstallerLogger
    {
        private string _logFileName;

        private const string _logFileRuntimeExtension = ".log.temp";

        private const string _logFileExtension = ".log";

        private const string _defaultDrive = @"C:\";

        private FileStream _fileStream;

        private string GetRuntimeFullFilePath()
            => GetLogFilePath() + _logFileName + _logFileRuntimeExtension;

        private string GetFullFilePath()
            => GetLogFilePath() + _logFileName + _logFileExtension;

        private string GetLogFilePath()
            => GetDrive() + @"Users\" + Environment.UserName + @"\AppData\Local\Temp\";

        private string GetDrive()
            => Environment.GetLogicalDrives().Any(drive => drive.Equals(_defaultDrive)) ? _defaultDrive : Environment.GetLogicalDrives()[0];

        private string _logMessagePrefix = DateTime.Now.ToString("yyyy-MM-dd_HH:mm:ss:fff") + " ";
        private string _logMessageSuffix = Environment.NewLine;

        public InstallerLogger()
        {
            //todo nie kiospi tylko nazwa ze schematu
            _logFileName = "KIOSPI_" + Program.InstallerActualAction + "_" + DateTime.Now.ToString("yyyy-MM-dd_HH:mm:ss:fff");
            string path = GetRuntimeFullFilePath().Replace('\\', '/');
            _fileStream = new FileStream(path, FileMode.Create);
        }

        ~InstallerLogger()
        {
            _fileStream.Dispose();
            File.Move(GetRuntimeFullFilePath(), GetFullFilePath());
        }

        private void Log(string message)
        {
            using (var sw = new StreamWriter(_fileStream))
            {
                sw.WriteLine(_logMessagePrefix + message + _logMessageSuffix);
            }
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
