using Juce.Core.Bounds;
using System;
using System.Collections.Generic;

namespace Juce.Core.Stats
{
    public class FloatStatValueCalculator : IStatValueCalculator<float>
    {
        public float Calculate(
            float baseValue,
            IReadOnlyList<StatModifier<float>> statModifiers,
            IBounds<float> statBounds
            )
        {
            float accumulatedValue = baseValue;

            var minCap = float.MinValue;
            var maxCap = float.MaxValue;

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
