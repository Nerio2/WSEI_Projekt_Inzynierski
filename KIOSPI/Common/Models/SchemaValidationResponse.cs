namespace Common.Models
{
    public abstract class SchemaValidationResponse
    {
        public string Message = "";

        public SchemaValidationResponse(string message)
        {
            Message = message;
        }

        public override string ToString()
        {
            return this.GetType().Name + " " + Message;
        }
    }
}
