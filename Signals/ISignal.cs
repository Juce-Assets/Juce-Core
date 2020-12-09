using System;

namespace Juce.CoreUnity.Time
{
    public interface ISignal<T>
    {
        event Action<T> OnTriggered;

        void Trigger(T data);
    }
}