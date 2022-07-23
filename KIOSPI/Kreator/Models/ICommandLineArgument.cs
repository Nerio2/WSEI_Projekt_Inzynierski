namespace Kreator.Models
{
    public interface ICommandLineArgument
    {
        bool IsRequired { get; }

        string ArgumentName { get; }
        bool IsInvoked(string[] args);

        void ReadValue(string[] args);
    }
}