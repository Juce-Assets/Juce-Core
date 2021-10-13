using Juce.Core.Events.Generic;
using System;

namespace Juce.Core.Stats
{
    public interface IStat<T>
    {
        event GenericEvent<IStat<T>, EventArgs> OnModifiedValueChanged;

        T BaseValue { get; set; }
        T ModifiedValue { get; }

        void Add(StatModifier<T> statModifier);
        void Remove(StatModifier<T> statModifier);
    }
}
