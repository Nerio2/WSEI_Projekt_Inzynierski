namespace Instaler.Helpers
{
    internal class WindowsVersionsHelper : IWindowsVersionsHelper
    {
        public WindowsVersion GetWindowsVersionFromBuildVersion(int buildVersion)
                => buildVersion >= (int)WindowsVersion.Windows11 ?
                WindowsVersion.Windows11 :
                buildVersion >= (int)WindowsVersion.Windows10 ?
                WindowsVersion.Windows10 :
                buildVersion >= (int)WindowsVersion.Windows81 ?
                WindowsVersion.Windows81 :
                buildVersion >= (int)WindowsVersion.Windows8 ?
                WindowsVersion.Windows8 :
                buildVersion >= (int)WindowsVersion.Windows7 ?
                WindowsVersion.Windows7 :
                WindowsVersion.WindowsVista;
    }
    public enum WindowsVersion
    {
        WindowsVista = 6002,
        Windows7 = 7601,
        Windows8 = 9200,
        Windows81 = 9600,
        Windows10 = 10240,
        Windows11 = 22000
    }
}
