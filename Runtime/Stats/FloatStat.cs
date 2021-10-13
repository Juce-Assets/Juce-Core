
using Juce.Core.Bounds;

namespace Juce.Core.Stats
{
    public class FloatStat : Stat<float>
    {
        public FloatStat(float baseValue, IBounds<float> statBounds) : base(baseValue, statBounds, new FloatStatValueCalculator())
        {

        }
    }
}
