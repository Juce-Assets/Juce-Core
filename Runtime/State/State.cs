using System;
using System.Collections.Generic;
using Juce.Core.Bounds;
using Juce.Core.Events.Generic;

namespace Juce.Core.State
{
    public class State<T> : IState<T>
    {
        private T value;
        private readonly IBounds<T> bounds;

        public T Value
        {
            get => value;
            set => SetValue(value);
        }

        public event GenericEvent<IState<T>, StateValueChangedEvent<T>> OnValueChanged;

        public State(T value, IBounds<T> bounds)
        {
            this.bounds = bounds;

            bounds.OnBoundsChanged += OnBoundsChanged;

            SetValue(value);
        }

        public void Refresh()
        {
            SetValue(value);
        }

        private void SetValue(T newValue)
        {
            newValue = bounds.ApplyBounds(newValue);

            bool equal = EqualityComparer<T>.Default.Equals(value, newValue);

            if (equal)
            {
                return;
            }

            T oldValue = value;

            value = newValue;

            OnValueChanged?.Invoke(this, new StateValueChangedEvent<T>(oldValue, value));
        }

        private void OnBoundsChanged(IBounds<T> bounds, EventArgs eventArgs)
        {
            Refresh();
        }
    }
}
