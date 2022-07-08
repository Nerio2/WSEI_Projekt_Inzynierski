using Instaler.logger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Instaler
{
    public static class Program
    {
        public static InstallerAction InstallerActualAction;

        private static InstallerLogger _logger;

        public enum InstallerAction
        {
            Installation,
            Uninstallation
        }

        public static void Main(string[] args)
        {
            
        }
    }
}
