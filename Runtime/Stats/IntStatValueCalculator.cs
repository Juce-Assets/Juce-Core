using Juce.Core.Bounds;
using Juce.Core.Maths.Utils;
using System;
using System.Collections.Generic;

namespace Juce.Core.Stats
{
    public class IntStatValueCalculator : IStatValueCalculator<int>
    {
        public int Calculate(
            int baseValue,
            IReadOnlyList<StatModifier<int>> statModifiers,
            IBounds<int> statBounds
            )
        {
            int accumulatedValue = baseValue;

            var minCap = int.MinValue;
            var maxCap = int.MaxValue;

            foreach (var statModifier in statModifiers)
            {
                switch (statModifier.StatModificationType)
                {
                    case StatModificationType.MaximumPercentage:
                        maxCap = Math.Min(maxCap, MathsUtils.Percentage(baseValue, statModifier.ModificationValue));
                        break;
                    case StatModificationType.MinimumPercentage:
                        minCap = Math.Max(minCap, MathsUtils.Percentage(baseValue, statModifier.ModificationValue));
                        break;
                    case StatModificationType.AddPercentage:
                        accumulatedValue += MathsUtils.Percentage(baseValue, statModifier.ModificationValue);
                        break;
                    case StatModificationType.AddAbsolute:
                        accumulatedValue += statModifier.ModificationValue;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            var clampedValue = MathsUtils.Clamp(accumulatedValue, minCap, maxCap);

            return statBounds.ApplyBounds(clampedValue);
        }
    }
}
