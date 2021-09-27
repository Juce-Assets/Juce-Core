using System;

namespace Juce.Core.Time
{
    public class Signal<TSender, TData> : ISignal<TSender, TData>
    {
        public event Action<TSender, TData> OnTriggered;

        public void Trigger(TSender sender, TData data)
        {
            OnTriggered?.Invoke(sender, data);
        }

        public void CleanUp()
        {
            OnTriggered = null;
        }
    }

    public class Signal<TSender> : ISignal<TSender>
    {
        public event Action<TSender> OnTriggered;

        public void Trigger(TSender sender)
        {
            OnTriggered?.Invoke(sender);
        }

        public void CleanUp()
        {
            OnTriggered = null;
        }
    }
}