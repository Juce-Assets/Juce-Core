using Juce.Core.Bounds;
using Juce.Core.Events.Generic;
using System;
using System.Collections.Generic;

namespace Juce.Core.Stats
{
    public class Stat : IStat
    {
        private readonly List<StatModifier> statModifiers = new List<StatModifier>();
        private readonly IBounds<int> statBounds;

        private bool invalid = true;

        private int baseValue;
        private int modifiedValue;

        public event GenericEvent<IStat, EventArgs> OnModifiedValueChanged;

        public int BaseValue
        {
            get => baseValue;
            set
            {
                baseValue = value;
                Invalidate();
            }
        }

        public int ModifiedValue
        {
            get
            {
                if (invalid)
                {
                    modifiedValue = CalculateModifiedValue();

                    invalid = false;
                }

                return modifiedValue;
            }
        }

        public Stat(int baseValue, IBounds<int> statBounds)
        {
            this.baseValue = baseValue;
            this.statBounds = statBounds;

            statBounds.OnBoundsChanged += OnBoundsChanged;
        }

        private void Invalidate()
        {
            invalid = true;
            OnModifiedValueChanged?.Invoke(this, EventArgs.Empty);
        }

        public void Add(StatModifier statModifier)
        {
            if (statModifiers.Contains(statModifier))
            {
                throw new InvalidOperationException($"Stat already contains this modifier, of type '{statModifier.GetType().Name}'");
            }

            statModifiers.Add(statModifier);

            statModifier.OnChanged += OnStatModifierChanged;

            Invalidate();

            OnModifiedValueChanged?.Invoke(this, EventArgs.Empty);
        }

        public void Remove(StatModifier statModifier)
        {
            if (!statModifiers.Remove(statModifier))
            {
                throw new InvalidOperationException($"Can't remove stat modification, of type '{statModifier.GetType().Name}', that is not present on the stat");
            }

            statModifier.OnChanged -= OnStatModifierChanged;

            Invalidate();

            OnModifiedValueChanged?.Invoke(this, EventArgs.Empty);
        }

        private int CalculateModifiedValue()
        {
            var accumulatedValue = BaseValue;

            var minCap = int.MinValue;
            var maxCap = int.MaxValue;

            foreach (var statModifier in statModifiers)
            {
                switch (statModifier.StatModificationType)
                {
                    case StatModificationType.MaximumPercentage:
                        maxCap = Math.Min(maxCap, MathUtils.Percentage(BaseValue, statModifier.ModificationValue));
                        break;
                    case StatModificationType.MinimumPercentage:
                        minCap = Math.Max(minCap, MathUtils.Percentage(BaseValue, statModifier.ModificationValue));
                        break;
                    case StatModificationType.AddPercentage:
                        accumulatedValue += MathUtils.Percentage(BaseValue, statModifier.ModificationValue);
                        break;
                    case StatModificationType.AddAbsolute:
                        accumulatedValue += statModifier.ModificationValue;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            var clampedValue = MathUtils.Clamp(accumulatedValue, minCap, maxCap);

            return statBounds.ApplyBounds(clampedValue);
        }

        private void OnStatModifierChanged(StatModifier statModifier, EventArgs eventArgs)
        {
            Invalidate();
        }

        private void OnBoundsChanged(IBounds<int> bounds, EventArgs eventArgs)
        {
            Invalidate();
        }
    }
}
