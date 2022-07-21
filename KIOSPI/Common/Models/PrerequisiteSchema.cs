using System;
using System.Collections.Generic;

namespace Common.Models
{
    public class PrerequisiteSchema
    {
        public string UpgradeCode { get; set; }
        public string Name { get; set; }
        public Version RequiredVersion { get; set; }

        public IEnumerable<SchemaValidationResponse> ValidateMainProperties()
        {
            if (string.IsNullOrEmpty(UpgradeCode))
            {
                yield return new SchemaValidationError("Prerequisites: Upgrade Code is missing! Please fill proper upgrade code of required prerequisite.");
            }

            if (string.IsNullOrEmpty(Name))
            {
                yield return new SchemaValidationWarning("Prereqiusites: Name is empty");
            }

            if (RequiredVersion == null)
            {
                yield return new SchemaValidationError("Prerequisites: Required version is missing! Please fill proper minimum version of prerequisite.");
            }
        }
    }
}
