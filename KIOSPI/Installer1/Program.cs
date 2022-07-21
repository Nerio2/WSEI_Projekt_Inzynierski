using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Instaler
{
    public static class Program
    {
        public static InstallerAction InstallerActualAction;

        public enum InstallerAction
        {
            Installation,
            Uninstallation
        }

        public static void Main(string[] args)
        {
            Console.WriteLine("hiho");
            _ = Console.ReadLine();
        }
    }
}
