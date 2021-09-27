using Juce.Core.Events.Generic;
using System;

namespace Juce.Core.Bounds.Int
{
    public class MinMaxIntBounds : IBounds<int>
    {
        private readonly int minValue;
        private readonly int maxValue;

        public event GenericEvent<IBounds<int>, EventArgs> OnBoundsChanged;

        public MinMaxIntBounds(int minValue, int maxValue)
        {
            this.minValue = minValue;
            this.maxValue = maxValue;
        }

        public int ApplyBounds(int value)
        {
            value = Math.Max(value, minValue);
            value = Math.Min(value, maxValue);

            return value;
        }
    }
}
