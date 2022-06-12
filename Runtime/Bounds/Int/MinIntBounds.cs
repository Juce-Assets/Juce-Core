using Juce.Core.Events.Generic;
using System;

namespace Juce.Core.Bounds.Int
{
    public sealed class MinIntBounds : IBounds<int>
    {
        private readonly int minValue;

        public event GenericEvent<IBounds<int>, EventArgs> OnBoundsChanged;

        public MinIntBounds(int minValue)
        {
            this.minValue = minValue;
        }

        public int ApplyBounds(int value)
        {
            return Math.Max(value, minValue);
        }
    }
}
