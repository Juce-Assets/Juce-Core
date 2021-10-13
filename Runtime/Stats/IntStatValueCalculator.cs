using Juce.Core.Bounds;
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
                        maxCap = Math.Min(maxCap, MathUtils.Percentage(baseValue, statModifier.ModificationValue));
                        break;
                    case StatModificationType.MinimumPercentage:
                        minCap = Math.Max(minCap, MathUtils.Percentage(baseValue, statModifier.ModificationValue));
                        break;
                    case StatModificationType.AddPercentage:
                        accumulatedValue += MathUtils.Percentage(baseValue, statModifier.ModificationValue);
                        break;
                    case StatModificationType.AddAbsolute:
                        accumulatedValue += statModifier.ModificationValue;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            var clampedValue = MathUtils.Clamp(accumulatedValue, minCap, maxCap);

            return statBounds.ApplyBounds(clampedValue);
        }
    }
}
