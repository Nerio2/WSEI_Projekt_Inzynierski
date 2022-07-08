using System;

namespace Instaler.Helpers
{
    internal interface IRegistrySearchHelper
    {
        Version GetProgramVersion(string upgradeCode);
        int GetWindowsBuildNumber();
        string GetHKLMRegistryValue(string path, string key);
    }
}