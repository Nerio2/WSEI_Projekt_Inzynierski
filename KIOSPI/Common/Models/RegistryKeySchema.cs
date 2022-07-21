using System.Collections.Generic;

namespace Common.Models
{
    public class RegistryKeySchema
    {
        public string Path { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
        public string ErrorMessage { get; set; }

        public IEnumerable<SchemaValidationResponse> ValidateMainProperties()
        {
            if (string.IsNullOrEmpty(Path))
            {
                yield return new SchemaValidationError("System requirements: Registry Keys: Path is missing. Please fill path of key you want to check.");
            }

            if (string.IsNullOrEmpty(Key))
            {
                yield return new SchemaValidationError("System requirements: Registry Keys: Key is empty.");
            }

            if (string.IsNullOrEmpty(Value))
            {
                yield return new SchemaValidationError("System requirements: Registry Keys: Value is empty.");
            }

            if (string.IsNullOrEmpty(ErrorMessage))
            {
                yield return new SchemaValidationError("System requirements: Registry Keys: ErrorMessage is empty.");
            }
        }
    }
}
