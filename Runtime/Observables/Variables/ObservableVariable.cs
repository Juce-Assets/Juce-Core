using System;

namespace Juce.Core.Observables.Variables
{
    public sealed class ObservableVariable<T> : IObservableVariable<T> where T : IComparable
    {
        private T value;

        public T Value
        {
            get => value;

            set
            {
                if (this.value.Equals(value))
                {
                    return;
                }

                this.value = value;

                OnChange?.Invoke(value);
            }
        }

        public event Action<T> OnChange;

        public void Clear()
        {
            OnChange = null;
        }
    }
}