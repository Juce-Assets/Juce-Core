using System;

namespace Juce.CoreUnity.Time
{
    public interface ISignal<T>
    {
        event Action<T> OnTriggered;

        void Trigger(T data);
    }

    public interface ISignal
    {
        event Action OnTriggered;

        void Trigger();
    }
}