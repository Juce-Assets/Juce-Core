using System;

namespace Juce.Core.Observables.Events
{
    public interface IObservableEvent<TSender, TEventArgs>
    {
        event Action<TSender, TEventArgs> OnExecute;

        void Execute(TSender sender, TEventArgs eventArgs);
        void Clear();
    }
}
