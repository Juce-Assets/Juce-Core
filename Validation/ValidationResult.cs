using System.Collections.Generic;

namespace Juce.Core.Validation
{
    public class ValidationResult
    {
        public ValidationResultType ValidationResultType { get; }
        public IReadOnlyList<ValidationLog> ValidationLogs { get; }

        public ValidationResult(ValidationResultType validationResultType, IReadOnlyList<ValidationLog> validationLogs)
        {
            ValidationResultType = validationResultType;
            ValidationLogs = validationLogs;
        }
    }
}
