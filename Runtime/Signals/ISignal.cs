using System;

namespace Juce.Core.Time
{
    public interface ISignal<TSender, TData>
    {
        event Action<TSender, TData> OnTriggered;

        void Trigger(TSender sender, TData data);
        void CleanUp();
    }

    public interface ISignal<TSender>
    {
        event Action<TSender> OnTriggered;

        void Trigger(TSender sender);
        void CleanUp();
    }
}