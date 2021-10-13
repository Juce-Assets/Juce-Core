using Juce.Core.Bounds;
using Juce.Core.Events.Generic;
using System;
using System.Collections.Generic;

namespace Juce.Core.Stats
{
    public class Stat<T> : IStat<T>
    {
        private readonly List<StatModifier<T>> statModifiers = new List<StatModifier<T>>();
        private readonly IBounds<T> statBounds;
        private readonly IStatValueCalculator<T> statValueCalculator;

        private bool invalid = true;

        private T baseValue;
        private T modifiedValue;

        public event GenericEvent<IStat<T>, EventArgs> OnModifiedValueChanged;

        public T BaseValue
        {
            get => baseValue;
            set
            {
                baseValue = value;
                Invalidate();
            }
        }

        public T ModifiedValue
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

        public Stat(T baseValue, IBounds<T> statBounds, IStatValueCalculator<T> statValueCalculator)
        {
            this.baseValue = baseValue;
            this.statBounds = statBounds;
            this.statValueCalculator = statValueCalculator;

            statBounds.OnBoundsChanged += OnBoundsChanged;
        }

        private void Invalidate()
        {
            invalid = true;
            OnModifiedValueChanged?.Invoke(this, EventArgs.Empty);
        }

        public void Add(StatModifier<T> statModifier)
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

        public void Remove(StatModifier<T> statModifier)
        {
            if (!statModifiers.Remove(statModifier))
            {
                throw new InvalidOperationException($"Can't remove stat modification, of type '{statModifier.GetType().Name}', that is not present on the stat");
            }

            statModifier.OnChanged -= OnStatModifierChanged;

            Invalidate();

            OnModifiedValueChanged?.Invoke(this, EventArgs.Empty);
        }

        private T CalculateModifiedValue()
        {
            return statValueCalculator.Calculate(BaseValue, statModifiers, statBounds);
        }

        private void OnStatModifierChanged(StatModifier<T> statModifier, EventArgs eventArgs)
        {
            Invalidate();
        }

        private void OnBoundsChanged(IBounds<T> bounds, EventArgs eventArgs)
        {
            Invalidate();
        }
    }
}
