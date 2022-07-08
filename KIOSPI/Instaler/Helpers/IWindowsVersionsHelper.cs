namespace Instaler.Helpers
{
    internal interface IWindowsVersionsHelper
    {
        WindowsVersion GetWindowsVersionFromBuildVersion(int buildVersion);
    }
}