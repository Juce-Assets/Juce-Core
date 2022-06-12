using Juce.Core.Validation.Enums;

namespace Juce.Core.Validation.Data
{
    public interface IValidationLog
    {
        public ValidationLogType ValidationLogType { get; }
        public string LogMessage { get; }
    }
}
