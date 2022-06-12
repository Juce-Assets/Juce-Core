using Juce.Core.Validation.Data;
using Juce.Core.Validation.Enums;
using System.Collections.Generic;

namespace Juce.Core.Validation.Results
{
    public sealed class ValidationResult : IValidationResult
    {
        public ValidationResultType ValidationResultType { get; }
        public IReadOnlyList<IValidationLog> ValidationLogs { get; }

        public ValidationResult(ValidationResultType validationResultType, IReadOnlyList<IValidationLog> validationLogs)
        {
            ValidationResultType = validationResultType;
            ValidationLogs = validationLogs;
        }
    }
}
