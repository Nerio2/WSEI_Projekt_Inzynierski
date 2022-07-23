
using System.Reflection;
using System.Text;

namespace Kreator.Models
{
    public class CommandLineArgument<T> : ICommandLineArgument
    {
        public bool IsRequired { get; }
        public string ArgumentName { get; }
        public T value;
        private T DefaultValue;
        public string[] invokers;
        public string Description;
        private bool readValue;

        public CommandLineArgument(string[] invokers, bool isRequired, string description, string argumentName, bool readValue = true)
        {
            this.invokers = invokers;
            this.IsRequired = isRequired;
            this.Description = description;
            ArgumentName = argumentName;
            this.readValue = readValue;
        }

        public override string ToString()
        {
            StringBuilder text = new StringBuilder();
            text.Append(String.Join('\t', invokers));
            text.Append(IsRequired ? "REQUIRED " : "\t");
            text.Append(Description);
            return text.ToString();
        }

        public void ReadValue(string[] args)
        {
            if (!readValue)
            {
                return;
            }
            for (int i = 0; i < args.Length - 1; i++)
            {
                foreach (string invoker in invokers)
                {
                    if (invoker.Equals(args[i]))
                    {
                        value = (T)Activator.CreateInstance(typeof(T), args[i + 1]);
                    }
                }
            }
            if (DefaultValue != null)
                value = DefaultValue; //todo obsluga numerycznych
            value = default(T);
        }

        public bool IsInvoked(string[] args)
        {
            return invokers.Any(invoker => args.Contains(invoker));
        }
    }
}
