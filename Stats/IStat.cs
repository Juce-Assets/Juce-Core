using Juce.Core.Events.Generic;
using System;

namespace Juce.Core.Stats
{
    public interface IStat
    {
        event GenericEvent<IStat, EventArgs> OnModifiedValueChanged;

        int BaseValue { get; set; }
        int ModifiedValue { get; }

        void Add(StatModifier statModifier);
        void Remove(StatModifier statModifier);
    }
}
