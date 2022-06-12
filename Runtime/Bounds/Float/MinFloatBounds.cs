using Juce.Core.Events.Generic;
using System;

namespace Juce.Core.Bounds.Int
{
    public sealed class MinFloatBounds : IBounds<float>
    {
        private readonly float minValue;

        public event GenericEvent<IBounds<float>, EventArgs> OnBoundsChanged;

        public MinFloatBounds(float minValue)
        {
            this.minValue = minValue;
        }

        public float ApplyBounds(float value)
        {
            return Math.Max(value, minValue);
        }
    }
}
