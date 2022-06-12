using System;

namespace Juce.Core.Observables
{
    public sealed class ObservableEvent<TSender, TEventArgs>
    {
        public event Action<TSender, TEventArgs> OnExecute;

        public void Execute(TSender sender, TEventArgs eventArgs)
        {
            OnExecute?.Invoke(sender, eventArgs);
        }

        public void Clear()
        {
            OnExecute = null;
        }
    }
}
