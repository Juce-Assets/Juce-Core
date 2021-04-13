using Juce.Core.Events.Generic;
using System;

namespace Juce.Core.Bounds
{
    public class NopBounds<T> : IBounds<T>
    {
        public event GenericEvent<IBounds<T>, EventArgs> OnBoundsChanged;

        public T ApplyBounds(T value)
        {
            return value;
        }
    }
}
