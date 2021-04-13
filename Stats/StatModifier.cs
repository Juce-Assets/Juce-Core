using Juce.Core.Events.Generic;
using System;

namespace Juce.Core.Stats
{
    public class StatModifier
    {
        public event GenericEvent<StatModifier, EventArgs> OnChanged;

        private StatModificationType statModificationType;
        private int modificationValue;

        public StatModifier(StatModificationType statModificationType, int modificationValue)
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

        public int ModificationValue
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
