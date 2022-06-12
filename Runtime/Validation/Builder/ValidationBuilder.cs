using Juce.Core.Validation.Data;
using Juce.Core.Validation.Enums;
using Juce.Core.Validation.Results;
using System.Collections.Generic;

namespace Juce.Core.Validation.Builder
{
    public sealed class ValidationBuilder : IValidationBuilder
    {
        private readonly List<IValidationLog> validationLogs = new List<IValidationLog>();
        private ValidationResultType validationResultType = ValidationResultType.Success;

        public ValidationBuilder()
        {

        }

        public ValidationBuilder(IValidationResult nestedResult)
        {
            validationResultType &= nestedResult.ValidationResultType;

            validationLogs.AddRange(nestedResult.ValidationLogs);
        }

        public void LogError(string logMessage)
        {
            validationLogs.Add(new ValidationLog(ValidationLogType.Error, logMessage));
            validationResultType = ValidationResultType.Error;
        }

        public void LogWarning(string logMessage)
        {
            validationLogs.Add(new ValidationLog(ValidationLogType.Warning, logMessage));
        }

        public void LogInfo(string logMessage)
        {
            validationLogs.Add(new ValidationLog(ValidationLogType.Info, logMessage));
        }

        public IValidationResult Build()
        {
            return new ValidationResult(validationResultType, validationLogs);
        }
    }
}
