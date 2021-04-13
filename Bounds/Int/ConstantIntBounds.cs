using Juce.Core.Events.Generic;
using System;

namespace Juce.Core.Bounds.Int
{
    public class ConstantIntBounds : IBounds<int>
    {
        private readonly int constantValue;

        public event GenericEvent<IBounds<int>, EventArgs> OnBoundsChanged;

        public ConstantIntBounds(int constantValue)
        {
            this.constantValue = constantValue;
        }

        public int ApplyBounds(int value)
        {
            return constantValue;
        }
    }
}
