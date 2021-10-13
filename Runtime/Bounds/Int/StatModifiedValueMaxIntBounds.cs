using Juce.Core.Events.Generic;
using Juce.Core.Stats;
using System;

namespace Juce.Core.Bounds.Int
{
    public class StatModifiedValueMaxIntBounds : IBounds<int>
    {
        private readonly IStat<int> stat;

        public event GenericEvent<IBounds<int>, EventArgs> OnBoundsChanged;

        public StatModifiedValueMaxIntBounds(IStat<int> stat)
        {
            this.stat = stat;

            stat.OnModifiedValueChanged += OnStatModifiedValueChanged;
        }

        public int ApplyBounds(int value)
        {
            return Math.Min(value, stat.ModifiedValue);
        }

        private void OnStatModifiedValueChanged(IStat<int> stat, EventArgs eventArgs)
        {
            OnBoundsChanged?.Invoke(this, eventArgs);
        }
    }
}
