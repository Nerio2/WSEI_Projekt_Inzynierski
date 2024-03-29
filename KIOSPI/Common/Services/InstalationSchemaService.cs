﻿using Common.Interfaces;
using Common.Models;
using Newtonsoft.Json;
using System.IO;

namespace Common.Services
{
    public class InstalationSchemaService : IInstalationSchemaService
    {
        public InstallationSchema ReadInstallationSchema(string filePath)
        {
            return JsonConvert.DeserializeObject<InstallationSchema>(File.ReadAllText(filePath));
        }
    }
}
