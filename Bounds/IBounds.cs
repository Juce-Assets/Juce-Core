using Juce.Core.Events.Generic;
using System;

namespace Juce.Core.Bounds
{
    public interface IBounds<T>
    {
        event GenericEvent<IBounds<T>, EventArgs> OnBoundsChanged;

        T ApplyBounds(T value);
    }
}
