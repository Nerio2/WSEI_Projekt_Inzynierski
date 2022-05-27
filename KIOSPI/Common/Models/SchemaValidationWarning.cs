namespace Common.Models
{
    internal class SchemaValidationWarning : SchemaValidationResponse
    {
        public SchemaValidationWarning(string message) : base(message)
        {
        }
    }
}
