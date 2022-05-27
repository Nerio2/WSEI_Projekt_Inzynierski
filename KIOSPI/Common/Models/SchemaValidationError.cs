namespace Common.Models
{
    internal class SchemaValidationError : SchemaValidationResponse
    {
        public SchemaValidationError(string message) : base(message)
        {
        }
    }
}
