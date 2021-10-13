
using Juce.Core.Bounds;

namespace Juce.Core.Stats
{
    public class IntStat : Stat<int>
    {
        public IntStat(int baseValue, IBounds<int> statBounds) : base(baseValue, statBounds, new IntStatValueCalculator())
        {

        }
    }
}
