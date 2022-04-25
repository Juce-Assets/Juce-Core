namespace Juce.Core.Logging
{
    public class LoggerOwner : ILoggerOwner
    {
        public string Name { get; }

        public LoggerOwner(string name)
        {
            Name = name;
        }
    }
}
