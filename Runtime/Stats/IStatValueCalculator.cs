using Juce.Core.Bounds;
using System.Collections.Generic;

namespace Juce.Core.Stats
{
    public interface IStatValueCalculator<T>
    {
        T Calculate(
            T baseValue,
            IReadOnlyList<StatModifier<T>> statModifiers,
            IBounds<T> statBounds
            );
    }
}
