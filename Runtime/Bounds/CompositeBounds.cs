using Juce.Core.Events.Generic;
using System;

namespace Juce.Core.Bounds
{
    public sealed class CompositeBounds<T> : IBounds<T>
    {
        private readonly IBounds<T>[] bounds;

        public event GenericEvent<IBounds<T>, EventArgs> OnBoundsChanged;

        public CompositeBounds(IBounds<T>[] bounds)
        {
            this.bounds = bounds;

            foreach (IBounds<T> bound in bounds)
            {
                bound.OnBoundsChanged += OnCompositeBoundsChanged;
            }
        }

        public T ApplyBounds(T value)
        {
            foreach (IBounds<T> bound in bounds)
            {
                value = bound.ApplyBounds(value);
            }

            return value;
        }

        private void OnCompositeBoundsChanged(IBounds<T> bounds, EventArgs eventArgs)
        {
            OnBoundsChanged?.Invoke(this, eventArgs);
        }
    }
}
