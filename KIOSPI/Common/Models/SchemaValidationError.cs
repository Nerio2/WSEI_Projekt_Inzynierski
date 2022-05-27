namespace Common.Models
{
    public class SchemaValidationError : SchemaValidationResponse
    {
        public SchemaValidationError(string message) : base(message)
        {
        }
    }
}
