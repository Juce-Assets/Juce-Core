using Juce.Core.Events.Generic;
using System;

namespace Juce.Core.Stats
{
    public class StatModifier<T>
    {
        public event GenericEvent<StatModifier<T>, EventArgs> OnChanged;

        private StatModificationType statModificationType;
        private T modificationValue;

        public StatModifier(StatModificationType statModificationType, T modificationValue)
        {
            this.statModificationType = statModificationType;
            this.modificationValue = modificationValue;
        }

        public StatModificationType StatModificationType
        {
            get => statModificationType;
            set
            {
                statModificationType = value;
                OnChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        public T ModificationValue
        {
            get => modificationValue;
            set
            {
                modificationValue = value;
                OnChanged?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}
