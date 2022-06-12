using Juce.Core.Validation.Results;

namespace Juce.Core.Validation.Builder
{
    public interface IValidationBuilder
    {
        void LogError(string logMessage);
        void LogWarning(string logMessage);
        void LogInfo(string logMessage);
        IValidationResult Build();
    }
}