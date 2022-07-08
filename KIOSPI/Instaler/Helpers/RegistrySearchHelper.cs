using Microsoft.Win32;
using System;

namespace Instaler.Helpers
{
    internal class RegistrySearchHelper : IRegistrySearchHelper
    {
        private string _installedProgramsPath = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\";
        private string _installedx64ProgramsPath = @"SOFTWARE\WOW6432Node\Microsoft\Windows\CurrentVersion\Uninstall";

        private string _windowsInfoPath = @"SOFTWARE\Microsoft\Windows NT\CurrentVersion";

        public Version GetProgramVersion(string upgradeCode)
        {
            string version = GetHKLMRegistryValue(_installedProgramsPath, "DisplayVersion") ?? GetHKLMRegistryValue(_installedx64ProgramsPath, "DisplayVersion");
            if (version == null)
            {
                return null;
            }
            else
            {
                return new Version(version);
            }
        }

        public string GetHKLMRegistryValue(string path, string key)
        {
            var regKey = Registry.LocalMachine.OpenSubKey(path);
            var value = regKey.GetValue(key);
            return value.ToString();
        }

        public int GetWindowsBuildNumber()
            => Convert.ToInt32(GetHKLMRegistryValue(_windowsInfoPath, "CurrentBuild"));

    }
}
