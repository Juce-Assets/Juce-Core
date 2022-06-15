using System;

namespace Juce.Core.Observables.Variables
{
    public interface IObservableVariable<T> where T : IComparable
    {
        T Value { get; set; }

        event Action<T> OnChange;

        void Clear();
    }
}