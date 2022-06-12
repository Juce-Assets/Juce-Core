using Juce.Core.Validation.Data;
using Juce.Core.Validation.Enums;
using System.Collections.Generic;

namespace Juce.Core.Validation.Results
{
    public interface IValidationResult 
    {
        public ValidationResultType ValidationResultType { get; }
        public IReadOnlyList<IValidationLog> ValidationLogs { get; }
    }
}